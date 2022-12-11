// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Runtime.InteropServices;
using Xunit.Abstractions;

namespace RizaUnitTest
{
    public class EngineUnitTest : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private DllInfo _nativeDllInfo;

        private delegate int FunctionCallTest();
        private delegate uint Fnv1Hash32Test(string hash);
        private delegate ulong Fnv1Hash64Test(string hash);

        public EngineUnitTest(ITestOutputHelper output)
        {
            _output = output;

            var context = new DllContext();
            DirectoryInfo info = new DirectoryInfo(Environment.CurrentDirectory);
            while (true)
            {
                FileInfo? dll = info.GetFiles("EngineUnitTest.dll").FirstOrDefault();
                if (dll != null)
                {
                    context.FilePath = dll.FullName;
                    break;
                }

                info = info.Parent;
            }

            NativeLibraryManager.LoadNativeLibrary(context, out _nativeDllInfo);
        }

        [Fact]
        public void NativeCallTest()
        {
            Assert.NotEqual(Guid.Empty, _nativeDllInfo.Id);
            var d = NativeLibraryManager.GetNativeDelegate<FunctionCallTest>(_nativeDllInfo.Id);
            int result = d.Invoke();
            Assert.NotEqual(0, result);
        }

        [Theory]
        [InlineData("This is a NativeFnv1Hash32Test.")]
        [InlineData("413FFE65-30E9-4E7A-8D23-EA97A2B89E43")]
        public void NativeFnv1Hash32Test(string hash)
        {
            Assert.NotEqual(Guid.Empty, _nativeDllInfo.Id);
            var d = NativeLibraryManager.GetNativeDelegate<Fnv1Hash32Test>(_nativeDllInfo.Id);

            uint result = d.Invoke(hash);
            _output.WriteLine("{0} : {1}", hash, result);
            Assert.NotEqual<uint>(0, result);
        }

        [Theory]
        [InlineData("This is a NativeFnv1Hash64Test.")]
        [InlineData("CBA7C1E2-964C-4362-B8F2-A95B1076E31F")]
        public void NativeFnv1Hash64Test(string hash)
        {
            Assert.NotEqual(Guid.Empty, _nativeDllInfo.Id);
            var d = NativeLibraryManager.GetNativeDelegate<Fnv1Hash64Test>(_nativeDllInfo.Id);

            ulong result = d.Invoke(hash);
            _output.WriteLine("{0} : {1}", hash, result);
            Assert.NotEqual<ulong>(0, result);
        }


        public void Dispose()
        {
            NativeLibraryManager.FreeNativeLibrary(_nativeDllInfo.Id);
        }
    }
}
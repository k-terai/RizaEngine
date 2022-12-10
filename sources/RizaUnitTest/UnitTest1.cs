using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Runtime.InteropServices;
using Xunit.Abstractions;

namespace RizaUnitTest
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper output;

        delegate int FunctionCallTest();

        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test1()
        {
            NativeCallTest();
        }

        private void NativeCallTest()
        {
            string dllPath = string.Empty;
            DirectoryInfo info = new DirectoryInfo(Environment.CurrentDirectory);
            while (true)
            {
                FileInfo? dll = info.GetFiles("EngineUnitTest.dll").FirstOrDefault();
                if (dll != null)
                {
                    dllPath = dll.FullName;
                    break;
                }

                info = info.Parent;
            }

            IntPtr ptrLib = LoadLibrary(dllPath);
            IntPtr ptrAdd = GetProcAddress(ptrLib, "FunctionCallTest");
            FunctionCallTest d = (FunctionCallTest)Marshal.GetDelegateForFunctionPointer(ptrAdd, typeof(FunctionCallTest));
            int result = d.Invoke();
            Assert.Equal(7777, result);
        }

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr LoadLibrary(string lpFileName);
        [DllImport("kernel32", SetLastError = true)]
        internal static extern bool FreeLibrary(IntPtr hModule);
        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = false)]
        internal static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);
    }
}
// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#include "pch.h"
using namespace RizaEngine;

ENGINEUNITTEST_API int __stdcall FunctionCallTest()
{
    return 1; //NOTE: Return except 0 to check that native function called from managed side exactly.
}

ENGINEUNITTEST_API RizaEngine::uint32 __stdcall Fnv1Hash32Test(RizaEngine::ctstring string)
{
    return Fnv1Hash32(string);
}

ENGINEUNITTEST_API RizaEngine::uint64 __stdcall Fnv1Hash64Test(RizaEngine::ctstring string)
{
    return Fnv1Hash64(string);
}

// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.


#pragma once
#include"Platform.h"

#ifdef ENGINEUNITTEST_EXPORTS
#define ENGINEUNITTEST_API __declspec(dllexport)
#else
#define ENGINEUNITTEST_API __declspec(dllimport)
#endif


EXTERN_C ENGINEUNITTEST_API int __stdcall FunctionCallTest();

EXTERN_C ENGINEUNITTEST_API RizaEngine::uint32 __stdcall Fnv1Hash32Test(RizaEngine::ctstring string);

EXTERN_C ENGINEUNITTEST_API RizaEngine::uint64 __stdcall Fnv1Hash64Test(RizaEngine::ctstring string);
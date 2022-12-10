#pragma once

#define WIN32_LEAN_AND_MEAN             // Windows ヘッダーからほとんど使用されていない部分を除外する
// Windows ヘッダー ファイル
#include <windows.h>

#ifdef ENGINEUNITTEST_EXPORTS
#define ENGINEUNITTEST_API __declspec(dllexport)
#else
#define ENGINEUNITTEST_API __declspec(dllimport)
#endif


extern "C" ENGINEUNITTEST_API int __stdcall FunctionCallTest();
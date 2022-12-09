// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once

#if _WIN32 | _WIN64
#define WIN32_LEAN_AND_MEAN       
#include<Windows.h> //Windows only
#endif

#include<tchar.h>
#include<string>

namespace RizaEngine
{
	typedef TCHAR tchar;
	typedef std::basic_string<TCHAR> tstring;  //Auto convert string (unicode,multi byte)
	typedef LPTSTR ptstring;    // Pointer string.
	typedef LPCTSTR ctstring; 	// Const string.
	typedef signed char int8;
	typedef unsigned char uint8;
	typedef unsigned short int uint16;
	typedef signed short int int16;
	typedef unsigned int uint32;
	typedef signed int int32;
	typedef unsigned long long uint64;
	typedef signed long long int64;
}
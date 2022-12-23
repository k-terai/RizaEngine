// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#include "pch.h"
#include "Logger.h"

using namespace std;
using namespace RizaEngine;

LogCallback Logger::s_callback;

void Logger::Log(LogType type, ctstring fmt, ...)
{
	va_list args;
	va_start(args, fmt);
	LogVA(type, fmt, args);
	va_end(args);
}

bool Logger::IsFailureLog(HRESULT hr, ctstring fmt, ...)
{
	if (SUCCEEDED(hr))
	{
		return false;
	}

	tchar str;
	FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM |
		FORMAT_MESSAGE_IGNORE_INSERTS |
		FORMAT_MESSAGE_ALLOCATE_BUFFER,
		NULL,
		hr,
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&str,
		0,
		NULL);


	if (!fmt)
	{
		Log(LogType::Error, L"HRESULT: '%s'\n", str);
	}
	else
	{
		tchar buffer[c_bufferSize];
		va_list args;
		va_start(args, fmt);
		_vsnwprintf_s(buffer, c_bufferSize, _TRUNCATE, fmt, args);
		Log(LogType::Error, L"HRESULT: '%s' in context '%s'\n", str, buffer);
		va_end(args);
	}

	return true;
}

void Logger::LogVA(LogType type, ctstring fmt, va_list args)
{
	tchar buffer[c_bufferSize];

	_vsnwprintf_s(buffer, c_bufferSize, _TRUNCATE, fmt, args);
	if (s_callback)
	{
		s_callback(type, buffer);
	}
	else
	{
#ifdef UNICODE
		wprintf(buffer);
#else
		printf(buffer);
#endif

	}
}


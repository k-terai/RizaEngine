// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once

#include"Platform.h"
#include"Common.h"

namespace RizaEngine
{
	class Logger
	{
	public:
		static void SetCallback(LogCallback callback) { s_callback = callback; }
		static void Log(LogType type, ctstring fmt, ...);
		static bool IsFailureLog(HRESULT hr, ctstring fmt = NULL, ...);

		static void LogVA(LogType type, ctstring fmt, va_list args);

	private:
		static const int c_bufferSize = 2048;
		static LogCallback s_callback;
	};
}
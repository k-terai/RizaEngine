// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once

#include"Platform.h"
#include<functional>

namespace RizaEngine
{
	enum class LogType
	{
		Info,
		Warning,
		Error,
		Exception
	};

	using LogCallback = std::function<void(const LogType logType, const tchar* const buffer)>;
}
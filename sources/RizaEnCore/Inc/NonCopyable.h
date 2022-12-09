// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once
#include"RizaObject.h"

namespace RizaEngine
{
	class NonCopyable :public RizaObject
	{
	protected:
		NonCopyable() = default;
		virtual ~NonCopyable() = default;

	private:
		NonCopyable(const NonCopyable&) = delete;
		NonCopyable& operator=(const NonCopyable&) = delete;
	};
}
// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once
#include"Platform.h"

namespace RizaEngine 
{
	wrect GetRect(const whandle hwnd) 
	{
		wrect rect;
		SecureZeroMemory(&rect, sizeof wrect);
		GetWindowRect(hwnd, &rect);
		return rect;
	}
}
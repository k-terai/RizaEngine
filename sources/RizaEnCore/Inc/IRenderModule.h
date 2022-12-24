// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once
#include"IModule.h"

namespace RizaEngine
{
	class IRenderModule : public IModule
	{
	public:
		virtual ~IRenderModule() = default;

#if _RIZA_ENGINE_PLATFORM_WINDOWS
		virtual void CreateForwardSceneRender(const whandle hwnd) = 0;
#endif

	};
}
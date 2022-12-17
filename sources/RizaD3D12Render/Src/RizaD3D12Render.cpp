// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#include "pch.h"
#include "RizaD3D12Render.h"
#include"RenderModule.h"

namespace RizaEngine
{
	namespace RizaD3D12Render
	{
		RizaEngine::IRenderModule* const GetRenderModule()
		{
			static RenderModule module;
			return &module;
		}
	}
}
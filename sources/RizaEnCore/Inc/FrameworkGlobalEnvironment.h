// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once

namespace RizaEngine
{
	class IRenderModule;

	struct FrameworkGlobalEnvironment
	{
		IRenderModule* pRenderModule;

		void Reset() 
		{
			pRenderModule = nullptr;
		}
	};
}
// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once
#include"NonCopyable.h"
#include"d3d12common.h"

namespace RizaEngine
{
	class SceneRenderer : public NonCopyable
	{
	public:
		virtual void Render() = 0;
		SceneRenderer();
		virtual ~SceneRenderer() override;

	protected:
		uint32 m_frameCount;
		uint32 m_frameIndex;
		uint32 m_windowWidth;
		uint32 m_windowHeight;
	};
}
// Copyright(c) k - terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once
#include"IRenderModule.h"
#include"FrameworkGlobalEnvironment.h"
#include"SceneRendererManager.h"

namespace RizaEngine
{
	class RenderModule final : public IRenderModule
	{
	public:
		virtual ~RenderModule() override;
		virtual void Initialize(FrameworkGlobalEnvironment* const fge) override;
		virtual void Startup() override;
		virtual void Update() override;
		virtual void Terminate() override;

	private:
		SceneRendererManager m_sceneRendererMgr;
	};
}
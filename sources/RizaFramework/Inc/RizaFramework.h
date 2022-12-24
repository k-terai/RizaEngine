// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once
#include"NonCopyable.h"
#include"FrameworkGlobalEnvironment.h"
#include"IRenderModule.h"

namespace RizaEngine
{
	class RizaFramework final : public NonCopyable
	{
	public:
		inline static RizaFramework* const GetInstance()
		{
			return static_cast<RizaFramework*>(&s_instance);
		}

		bool Initialize();
		void Startup();
		void Update();
		void Terminate();

		inline IRenderModule* const GetRenderModule()
		{
			return m_globalEnvironment.pRenderModule;
		}

	private:
		RizaFramework();
		virtual ~RizaFramework() override;

	private:
		static RizaFramework s_instance; //Thread safe.
		FrameworkGlobalEnvironment m_globalEnvironment;
	};
}
// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once
#include"FrameworkGlobalEnvironment.h"

namespace RizaEngine
{
	class IModule
	{
	public:
		virtual ~IModule() = default;
		virtual void Initialize(FrameworkGlobalEnvironment* const fge) = 0;
		virtual void Startup() = 0;
		virtual void Update() = 0;
		virtual void Terminate() = 0;
	};
}
// Copyright(c) k - terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#include "pch.h"
#include "RenderModule.h"

using namespace std;
using namespace RizaEngine;

RenderModule::~RenderModule()
{
}

void RenderModule::Initialize(FrameworkGlobalEnvironment* const fge)
{
	fge->pRenderModule = this;
}

void RenderModule::Startup()
{
	int a = 0;
}

void RenderModule::Update()
{
	int a = 0;
}

void RizaEngine::RenderModule::Terminate()
{
	int a = 0;
}

// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#include "pch.h"
#include"RizaFramework.h"
#include"RizaD3D12Render.h"

using namespace std;
using namespace RizaEngine;

RizaFramework RizaFramework::s_instance;


RizaFramework::RizaFramework()
{
	SecureZeroMemory(&m_globalEnvironment, sizeof(FrameworkGlobalEnvironment));
}

RizaFramework::~RizaFramework()
{
	m_globalEnvironment.Reset();
}

bool RizaFramework::Initialize()
{
	//Render module.
	{
		IRenderModule* ptr = nullptr;

#if (defined(_WIN32) || defined(WINAPI_FAMILY)) && !(defined(_XBOX_ONE) && defined(_TITLE)) && !defined(_GAMING_XBOX)
		ptr = RizaD3D12Render::GetRenderModule();
#endif

		if (ptr != nullptr)
		{
			ptr->Initialize(&m_globalEnvironment);
		}

	}

	return true;
}

void RizaEngine::RizaFramework::Startup()
{
	m_globalEnvironment.pRenderModule->Startup();
}

void RizaEngine::RizaFramework::Update()
{
	m_globalEnvironment.pRenderModule->Update();
}

void RizaEngine::RizaFramework::Terminate()
{
	m_globalEnvironment.pRenderModule->Terminate();
}


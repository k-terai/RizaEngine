// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#include "pch.h"
#include "GraphicsContext.h"

using namespace RizaEngine;
using namespace std;

GraphicsContext::GraphicsContext(const D3D12_COMMAND_LIST_TYPE type) : CommandContext(type)
{

}

GraphicsContext::~GraphicsContext()
{
	m_graphicsCommandList = nullptr;
}

bool RizaEngine::GraphicsContext::Initialize(CommandManager* const commandMgr)
{
	CommandContext::Initialize(commandMgr);
	m_graphicsCommandList = commandMgr->GetGraphicsCommandList();
	return true;
}

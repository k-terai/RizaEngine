// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#include "pch.h"
#include "CommandContext.h"

using namespace std;
using namespace RizaEngine;

CommandContext::CommandContext(const D3D12_COMMAND_LIST_TYPE type) : m_type(type)
{

}

CommandContext::~CommandContext()
{
	m_currentAllocator = nullptr;
}

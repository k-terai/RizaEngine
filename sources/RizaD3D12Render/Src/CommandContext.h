// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once
#include"d3d12common.h"
#include"NonCopyable.h"
#include"CommandManager.h"

namespace RizaEngine
{
	class CommandContext : public NonCopyable
	{
	public:
		CommandContext(const D3D12_COMMAND_LIST_TYPE type);
		virtual ~CommandContext();

		virtual bool Initialize(CommandManager* const commandMgr);


	protected:
		CommandManager* m_owningManager;
		CID3D12CommandAllocator* m_currentAllocator;
	private:
		const D3D12_COMMAND_LIST_TYPE m_type;
	};
}
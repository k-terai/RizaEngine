// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once
#include"d3d12common.h"
#include"NonCopyable.h"

namespace RizaEngine
{
	class CommandContext : public NonCopyable
	{
	public:
		CommandContext(const D3D12_COMMAND_LIST_TYPE type);
		virtual ~CommandContext();

		inline void SetCommandAllocator(CID3D12CommandAllocator* const allocator)
		{
			m_currentAllocator = allocator;
		}

	protected:
		CID3D12CommandAllocator* m_currentAllocator;
	private:
		const D3D12_COMMAND_LIST_TYPE m_type;
	};
}
// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once
#include"CommandContext.h"

namespace RizaEngine
{
	class GraphicsContext final : public CommandContext
	{
	public:
		GraphicsContext(const D3D12_COMMAND_LIST_TYPE type);
		virtual ~GraphicsContext() override;

		inline void SetGraphicsCommandList(CID3D12GraphicsCommandList* const graphicsCommandList)
		{
			m_graphicsCommandList = graphicsCommandList;
		}

	private:
		CID3D12GraphicsCommandList* m_graphicsCommandList;
	};
}
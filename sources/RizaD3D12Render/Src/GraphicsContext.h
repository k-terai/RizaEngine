// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once
#include"CommandContext.h"
#include"SimpleMath.h"

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

		inline void Begin()
		{
			m_currentAllocator->Reset();
			m_graphicsCommandList->Reset(m_currentAllocator, nullptr);
		}

		inline void End()
		{
			m_graphicsCommandList->Close();
		}

		inline void ResourceBarrier(CID3D12Resource* const pResource, const D3D12_RESOURCE_STATES stateBefore, const D3D12_RESOURCE_STATES stateAfter)
		{
			CD3DX12_RESOURCE_BARRIER barriers = CD3DX12_RESOURCE_BARRIER::Transition(pResource, stateBefore, stateAfter);
			m_graphicsCommandList->ResourceBarrier(1, &barriers);
		}

		inline void ClearRenderTargetView(const D3D12_CPU_DESCRIPTOR_HANDLE renderTargetView, const DirectX::SimpleMath::Color color, const uint32 numRects,
			const D3D12_RECT* pRects)
		{
			m_graphicsCommandList->ClearRenderTargetView(renderTargetView, color, numRects, pRects);
		}

	private:
		CID3D12GraphicsCommandList* m_graphicsCommandList;
	};
}
// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once
#include"NonCopyable.h"
#include"Platform.h"
#include"d3d12common.h"

namespace RizaEngine
{
	class CommandManager final : public NonCopyable
	{
	public:
		HRESULT Initialize(CID3D12Device* const device);

		inline CID3D12CommandAllocator* const GetCommandAllocator() 
		{
			return m_commandAllocator.Get();
		}

		inline CID3D12CommandQueue* const GetCommandQueue() 
		{
			return m_commandQueue.Get();
		}

		inline CID3D12GraphicsCommandList* const GetGraphicsCommandList() 
		{
			return m_graphicsCommandList.Get();
		}

		void WaitForPreviousFrame();

	private:
		HRESULT CreateCommandQueue();
		HRESULT CreateCommandAllocator();
		HRESULT CreateGraphicsCommandList();
		HRESULT CreateFence();

	private:
		CID3D12Device* m_device;
		ComPtr<CID3D12CommandQueue> m_commandQueue;
		ComPtr<CID3D12CommandAllocator> m_commandAllocator;
		ComPtr<CID3D12GraphicsCommandList> m_graphicsCommandList;
		ComPtr<CID3D12Fence> m_fence;
		handle m_fenceEvent;
		uint64 m_fenceValue;
	};
}
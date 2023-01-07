// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once
#include"SceneRenderer.h"
#include"GraphicsContext.h"

namespace RizaEngine
{
	class ForwardSceneRenderer final : public SceneRenderer
	{
	public:
		ForwardSceneRenderer();
		virtual ~ForwardSceneRenderer() override;
		virtual void Render() override;

		CHRESULT Initialize(CID3D12Device* const device, CIDXGIFactory* const factory, const whandle hwnd);

	private:
		CHRESULT InitCommandQueue();
		CHRESULT InitSwapChain(const whandle hwnd);
		CHRESULT CreateRenderTarget();
		CHRESULT CreateCommandAllocator();
		CHRESULT CreateCommandList();
		CHRESULT CreateFence();

		void WaitForPreviousFrame();

	private:
		CID3D12Device* m_device;
		CIDXGIFactory* m_factory;
		ComPtr<CIDXGISwapChain> m_swapChain;
		ComPtr<CID3D12CommandQueue> m_commandQueue;
		ComPtr<CID3D12CommandAllocator> m_commandAllocator;
		ComPtr<CID3D12GraphicsCommandList> m_commandList;
		ComPtr<CID3D12DescriptorHeap> m_rtvHeap;
		ComPtr<CID3D12Fence> m_fence;
		std::vector<ComPtr<CID3D12Resource>> m_renderTargets;
		std::unique_ptr<GraphicsContext> m_graphicsContext;
		uint32 m_rtvDescriptorSize;
		handle m_fenceEvent;
		uint64 m_fenceValue;
	};
}
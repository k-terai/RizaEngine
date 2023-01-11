// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once
#include"SceneRenderer.h"
#include"GraphicsContext.h"
#include"CommandManager.h"

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
		CHRESULT InitSwapChain(const whandle hwnd);
		CHRESULT CreateRenderTarget();

	private:
		CommandManager m_commandMgr;
		CID3D12Device* m_device;
		CIDXGIFactory* m_factory;
		ComPtr<CIDXGISwapChain> m_swapChain;
		ComPtr<CID3D12DescriptorHeap> m_rtvHeap;
		std::vector<ComPtr<CID3D12Resource>> m_renderTargets;
		std::unique_ptr<GraphicsContext> m_graphicsContext;
		uint32 m_rtvDescriptorSize;
	};
}
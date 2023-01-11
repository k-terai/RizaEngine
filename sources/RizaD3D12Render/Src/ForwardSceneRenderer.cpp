// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.


#include "pch.h"
#include "ForwardSceneRenderer.h"
#include"WindowsUtility.h"
#include"Logger.h"
#include"SimpleMath.h"

using namespace RizaEngine;
using namespace std;
using namespace DirectX::SimpleMath;

ForwardSceneRenderer::ForwardSceneRenderer()
{

}

ForwardSceneRenderer::~ForwardSceneRenderer()
{
	m_graphicsContext.reset();
	m_swapChain.Reset();
	m_rtvHeap.Reset();

	for (auto& r : m_renderTargets)
	{
		r.Reset();
	}
	m_renderTargets.clear();

	m_graphicsContext = nullptr;
	m_swapChain = nullptr;
	m_rtvHeap = nullptr;
	m_device = nullptr;
	m_factory = nullptr;
	m_windowHeight = 0;
	m_windowWidth = 0;
}

void ForwardSceneRenderer::Render()
{
	//NOTE: This code is entirely test.
	{
		m_graphicsContext->Begin();
		m_graphicsContext->ResourceBarrier(m_renderTargets[m_frameIndex].Get(), D3D12_RESOURCE_STATE_PRESENT, D3D12_RESOURCE_STATE_RENDER_TARGET);
		CD3DX12_CPU_DESCRIPTOR_HANDLE rtvHandle(m_rtvHeap->GetCPUDescriptorHandleForHeapStart(), m_frameIndex, m_rtvDescriptorSize);
		const float clearColor[] = { 0.0f, 0.2f, 0.4f, 1.0f };
		m_graphicsContext->ClearRenderTargetView(rtvHandle, Color::Color(1, 0, 1, 0), 0, nullptr);
		m_graphicsContext->ResourceBarrier(m_renderTargets[m_frameIndex].Get(), D3D12_RESOURCE_STATE_RENDER_TARGET, D3D12_RESOURCE_STATE_PRESENT);
		m_graphicsContext->End();
		m_graphicsContext->ExecuteCommandList();

		m_swapChain->Present(1, 0);
		m_commandMgr.WaitForPreviousFrame();

		m_frameIndex = m_swapChain->GetCurrentBackBufferIndex();
	}
}

CHRESULT RizaEngine::ForwardSceneRenderer::Initialize(CID3D12Device* const device, CIDXGIFactory* const factory, const whandle hwnd)
{
	m_graphicsContext = std::make_unique<GraphicsContext>(D3D12_COMMAND_LIST_TYPE_DIRECT);
	m_device = device;
	m_factory = factory;
	m_commandMgr.Initialize(m_device);
	m_graphicsContext->Initialize(&m_commandMgr);

	if (FAILED(InitSwapChain(hwnd)))
	{
		return S_FALSE;
	}

	if (FAILED(CreateRenderTarget()))
	{
		return S_FALSE;
	}

	return S_OK;
}


CHRESULT RizaEngine::ForwardSceneRenderer::InitSwapChain(const whandle hwnd)
{
	wrect rect = GetRect(hwnd);
	m_windowWidth = rect.right + rect.left;
	m_windowWidth = rect.bottom + rect.top;

	DXGI_SWAP_CHAIN_DESC1 swapChainDesc;
	SecureZeroMemory(&swapChainDesc, sizeof(DXGI_SWAP_CHAIN_DESC1));
	swapChainDesc.BufferCount = m_frameCount;
	swapChainDesc.Width = m_windowWidth;
	swapChainDesc.Height = m_windowHeight;
	swapChainDesc.Format = DXGI_FORMAT_R8G8B8A8_UNORM;
	swapChainDesc.BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT;
	swapChainDesc.SwapEffect = DXGI_SWAP_EFFECT_FLIP_DISCARD;
	swapChainDesc.SampleDesc.Count = 1;

	ComPtr<IDXGISwapChain1> swapChain;

	CHRESULT result = m_factory->CreateSwapChainForHwnd(
		m_commandMgr.GetCommandQueue(),
		hwnd,
		&swapChainDesc,
		nullptr,
		nullptr,
		&swapChain
	);
	if (Logger::IsFailureLog(result))
	{
		return result;
	}

	result = swapChain.As(&m_swapChain);
	if (Logger::IsFailureLog(result))
	{
		return result;
	}

	result = m_factory->MakeWindowAssociation(hwnd, DXGI_MWA_NO_ALT_ENTER);
	Logger::IsFailureLog(result);
	return result;
}

CHRESULT RizaEngine::ForwardSceneRenderer::CreateRenderTarget()
{
	CHRESULT result;

	// Create descriptor heaps.
	{
		D3D12_DESCRIPTOR_HEAP_DESC rtvHeapDesc = {};
		SecureZeroMemory(&rtvHeapDesc, sizeof(D3D12_DESCRIPTOR_HEAP_DESC));
		rtvHeapDesc.NumDescriptors = m_frameCount;
		rtvHeapDesc.Type = D3D12_DESCRIPTOR_HEAP_TYPE_RTV;
		rtvHeapDesc.Flags = D3D12_DESCRIPTOR_HEAP_FLAG_NONE;
		result = m_device->CreateDescriptorHeap(&rtvHeapDesc, IID_PPV_ARGS(&m_rtvHeap));
		if (Logger::IsFailureLog(result))
		{
			return result;
		}
		m_rtvDescriptorSize = m_device->GetDescriptorHandleIncrementSize(D3D12_DESCRIPTOR_HEAP_TYPE_RTV);
	}

	// Create frame resources.
	{
		CD3DX12_CPU_DESCRIPTOR_HANDLE rtvHandle(m_rtvHeap->GetCPUDescriptorHandleForHeapStart());

		m_renderTargets.resize(m_frameCount);

		// Create a RTV for each frame.
		for (uint32 n = 0; n < m_frameCount; n++)
		{
			result = m_swapChain->GetBuffer(n, IID_PPV_ARGS(&m_renderTargets[n]));
			if (Logger::IsFailureLog(result))
			{
				return result;
			}

			m_device->CreateRenderTargetView(m_renderTargets[n].Get(), nullptr, rtvHandle);
			rtvHandle.Offset(1, m_rtvDescriptorSize);
		}
	}

	return result;

}

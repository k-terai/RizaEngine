// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.


#include "pch.h"
#include "ForwardSceneRenderer.h"
#include"WindowsUtility.h"
#include"Logger.h"

using namespace RizaEngine;
using namespace std;

ForwardSceneRenderer::ForwardSceneRenderer()
{

}

ForwardSceneRenderer::~ForwardSceneRenderer()
{
	m_fence.Reset();
	m_graphicsContext.reset();
	m_commandList.Reset();
	m_commandAllocator.Reset();
	m_swapChain.Reset();
	m_commandQueue.Reset();
	m_rtvHeap.Reset();

	for (auto& r : m_renderTargets)
	{
		r.Reset();
	}
	m_renderTargets.clear();

	m_fence = nullptr;
	m_graphicsContext = nullptr;
	m_commandAllocator = nullptr;
	m_commandList = nullptr;
	m_swapChain = nullptr;
	m_commandQueue = nullptr;
	m_rtvHeap = nullptr;
	m_device = nullptr;
	m_factory = nullptr;
	m_windowHeight = 0;
	m_windowWidth = 0;
}

void ForwardSceneRenderer::Render()
{

}

CHRESULT RizaEngine::ForwardSceneRenderer::Initialize(CID3D12Device* const device, CIDXGIFactory* const factory, const whandle hwnd)
{
	m_graphicsContext = std::make_unique<GraphicsContext>(D3D12_COMMAND_LIST_TYPE_DIRECT);
	m_device = device;
	m_factory = factory;

	if (FAILED(InitCommandQueue()))
	{
		return S_FALSE;
	}

	if (FAILED(InitSwapChain(hwnd)))
	{
		return S_FALSE;
	}

	if (FAILED(CreateRenderTarget()))
	{
		return S_FALSE;
	}

	if (FAILED(CreateCommandAllocator()))
	{
		return S_FALSE;
	}


	if (FAILED(CreateCommandList()))
	{
		return S_FALSE;
	}


	if (FAILED(CreateFence()))
	{
		return S_FALSE;
	}

	return S_OK;
}

CHRESULT RizaEngine::ForwardSceneRenderer::InitCommandQueue()
{
	D3D12_COMMAND_QUEUE_DESC queueDesc = {};
	SecureZeroMemory(&queueDesc, sizeof(D3D12_COMMAND_QUEUE_DESC));
	queueDesc.Flags = D3D12_COMMAND_QUEUE_FLAG_NONE;
	queueDesc.Type = D3D12_COMMAND_LIST_TYPE_DIRECT;
	CHRESULT result = m_device->CreateCommandQueue(&queueDesc, IID_PPV_ARGS(&m_commandQueue));

	Logger::IsFailureLog(result);
	return result;
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

	CHRESULT result = m_factory->CreateSwapChainForHwnd(
		m_commandQueue.Get(),
		hwnd,
		&swapChainDesc,
		nullptr,
		nullptr,
		&m_swapChain
	);

	Logger::IsFailureLog(result);

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
		if (Logger::IsFailureLog(result) == false)
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
			if (Logger::IsFailureLog(result) == false)
			{
				return result;
			}

			m_device->CreateRenderTargetView(m_renderTargets[n].Get(), nullptr, rtvHandle);
			rtvHandle.Offset(1, m_rtvDescriptorSize);
		}
	}

	return result;

}

CHRESULT RizaEngine::ForwardSceneRenderer::CreateCommandAllocator()
{
	CHRESULT result = m_device->CreateCommandAllocator(D3D12_COMMAND_LIST_TYPE_DIRECT, IID_PPV_ARGS(&m_commandAllocator));

	if (Logger::IsFailureLog(result) == false)
	{
		m_graphicsContext->SetCommandAllocaator(m_commandAllocator.Get());
	}
	return result;
}

CHRESULT RizaEngine::ForwardSceneRenderer::CreateCommandList()
{
	CHRESULT result = m_device->CreateCommandList(0, D3D12_COMMAND_LIST_TYPE_DIRECT, m_commandAllocator.Get(), nullptr, IID_PPV_ARGS(&m_commandList));

	if (Logger::IsFailureLog(result) == false)
	{
		m_graphicsContext->SetGraphicsCommandList(m_commandList.Get());
	}
	return result;
}

CHRESULT ForwardSceneRenderer::CreateFence()
{
	CHRESULT result = m_device->CreateFence(0, D3D12_FENCE_FLAG_NONE, IID_PPV_ARGS(&m_fence));
	if (Logger::IsFailureLog(result))
	{
		return result;
	}

	m_fenceValue = 1;
	m_fenceEvent = CreateEvent(nullptr, FALSE, FALSE, nullptr);
	if (m_fenceEvent == nullptr)
	{
		return S_FALSE;
	}

	return result;
}

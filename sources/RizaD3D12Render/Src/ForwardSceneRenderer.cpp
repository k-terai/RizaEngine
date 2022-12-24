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

}

void ForwardSceneRenderer::Render()
{
	m_device = nullptr;
	m_factory = nullptr;
	m_windowHeight = 0;
	m_windowWidth = 0;
}

CHRESULT RizaEngine::ForwardSceneRenderer::Initialize(CID3D12Device* const device, CIDXGIFactory* const factory, const whandle hwnd)
{
	m_device = device;
	m_factory = factory;
	InitCommandQueue();
	InitSwapChain(hwnd);

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
	m_windowWidth = rect.bottom + -rect.top;

	DXGI_SWAP_CHAIN_DESC1 swapChainDesc;
	SecureZeroMemory(&swapChainDesc, sizeof(DXGI_SWAP_CHAIN_DESC));
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
	return result;
}

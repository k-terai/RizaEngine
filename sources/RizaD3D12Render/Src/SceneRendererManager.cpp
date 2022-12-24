// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#include "pch.h"
#include "SceneRendererManager.h"
#include "ForwardSceneRenderer.h"
#include"Logger.h"

using namespace std;
using namespace RizaEngine;

SceneRendererManager::SceneRendererManager() : m_debugController(nullptr), m_dxgiFactoryFlags(0), m_useWarpDevice(false)
{

}

SceneRendererManager::~SceneRendererManager()
{
	m_dxgiFactoryFlags = 0;

	m_debugController.Reset();
	m_debugController = nullptr;

	m_factory.Reset();
	m_factory = nullptr;

	m_device.Reset();
	m_device = nullptr;

	for (auto& r : m_sceneRenderers)
	{
		r.reset();
	}
	m_sceneRenderers.clear();
}

bool RizaEngine::SceneRendererManager::Initialize()
{
	CHRESULT result = EnableDebugLayer();
	if (result != S_OK)
	{
		return false;
	}

	result = CreateFactory();
	if (result != S_OK)
	{
		return false;
	}

	result = CreateDevice();
	if (result != S_OK)
	{
		return false;
	}
	m_sceneRenderers.reserve(8);
	return true;
}

bool RizaEngine::SceneRendererManager::CreateForwardSceneRenderer(const whandle hwnd)
{
	m_sceneRenderers.emplace_back(make_unique<ForwardSceneRenderer>());
	ForwardSceneRenderer* const ptr = reinterpret_cast<ForwardSceneRenderer*>(m_sceneRenderers.back().get());
	CHRESULT result = ptr->Initialize(m_device.Get(), m_factory.Get(), hwnd);

	return result == S_OK;
}

CHRESULT RizaEngine::SceneRendererManager::EnableDebugLayer()
{
#if defined(_DEBUG)
	HRESULT result = D3D12GetDebugInterface(IID_PPV_ARGS(&m_debugController));

	if (!Logger::IsFailureLog(result))
	{
		m_debugController->EnableDebugLayer();
		m_dxgiFactoryFlags |= DXGI_CREATE_FACTORY_DEBUG;
	}
#endif


	return result;
}

CHRESULT RizaEngine::SceneRendererManager::CreateFactory()
{
	HRESULT result = CreateDXGIFactory2(m_dxgiFactoryFlags, IID_PPV_ARGS(&m_factory));
	Logger::IsFailureLog(result);
	return result;
}

CHRESULT RizaEngine::SceneRendererManager::CreateDevice()
{
	CHRESULT result = S_FALSE;

	if (m_useWarpDevice)
	{
		ComPtr<IDXGIAdapter> warpAdapter;
		result = m_factory->EnumWarpAdapter(IID_PPV_ARGS(&warpAdapter));

		if (Logger::IsFailureLog(result))
		{
			return result;
		}

		D3D12CreateDevice(
			warpAdapter.Get(),
			D3D_FEATURE_LEVEL_11_0,
			IID_PPV_ARGS(&m_device));

		return result;
	}
	else
	{
		ComPtr<IDXGIAdapter1> hardwareAdapter;
		result = GetHardwareAdapter(m_factory.Get(), &hardwareAdapter);

		if (Logger::IsFailureLog(result))
		{
			return result;
		}

		result = D3D12CreateDevice(
			hardwareAdapter.Get(),
			D3D_FEATURE_LEVEL_11_0,
			IID_PPV_ARGS(&m_device));

		return result;
	}

}

CHRESULT RizaEngine::SceneRendererManager::GetHardwareAdapter(IDXGIFactory1* const pFactory, IDXGIAdapter1** ppAdapter, bool requestHighPerformanceAdapter)
{
	*ppAdapter = nullptr;
	ComPtr<IDXGIAdapter1> adapter;
	ComPtr<IDXGIFactory6> factory6;

	if (SUCCEEDED(pFactory->QueryInterface(IID_PPV_ARGS(&factory6))))
	{
		for (uint32 adapterIndex = 0;
			SUCCEEDED(factory6->EnumAdapterByGpuPreference(
				adapterIndex,
				requestHighPerformanceAdapter == true ? DXGI_GPU_PREFERENCE_HIGH_PERFORMANCE : DXGI_GPU_PREFERENCE_UNSPECIFIED,
				IID_PPV_ARGS(&adapter)));
			++adapterIndex)
		{
			DXGI_ADAPTER_DESC1 desc;
			adapter->GetDesc1(&desc);

			//TODO: Support software.
			if (desc.Flags & DXGI_ADAPTER_FLAG_SOFTWARE)
			{
				continue;
			}

			// Check to see whether the adapter supports D3D12.
			if (SUCCEEDED(D3D12CreateDevice(adapter.Get(), D3D_FEATURE_LEVEL_11_0, _uuidof(ID3D12Device), nullptr)))
			{
				break;
			}
		}
	}

	if (adapter.Get() == nullptr)
	{
		for (uint32 adapterIndex = 0; SUCCEEDED(pFactory->EnumAdapters1(adapterIndex, &adapter)); ++adapterIndex)
		{
			DXGI_ADAPTER_DESC1 desc;
			adapter->GetDesc1(&desc);

			if (desc.Flags & DXGI_ADAPTER_FLAG_SOFTWARE)
			{
				//TODO: Support software.
				continue;
			}

			// Check to see whether the adapter supports D3D12.
			if (SUCCEEDED(D3D12CreateDevice(adapter.Get(), D3D_FEATURE_LEVEL_11_0, _uuidof(ID3D12Device), nullptr)))
			{
				break;
			}
		}
	}

	*ppAdapter = adapter.Detach();

	return S_OK;
}

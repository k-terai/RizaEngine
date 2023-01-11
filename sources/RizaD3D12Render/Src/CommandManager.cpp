// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#include "pch.h"
#include "CommandManager.h"
#include "Logger.h"

using namespace std;
using namespace RizaEngine;

HRESULT CommandManager::Initialize(CID3D12Device* const device)
{
	m_device = device;

	HRESULT result = CreateCommandQueue();

	if (result != S_OK)
	{
		return result;
	}

	result = CreateCommandAllocator();
	if (result != S_OK)
	{
		return result;
	}

	result = CreateGraphicsCommandList();
	if (result != S_OK)
	{
		return result;
	}

	result = CreateFence();
	if (result != S_OK)
	{
		return result;
	}

	return result;
}

void RizaEngine::CommandManager::WaitForPreviousFrame()
{
	const uint64 fence = m_fenceValue;
	CHRESULT result = m_commandQueue->Signal(m_fence.Get(), fence);
	if (Logger::IsFailureLog(result))
	{
		return;
	}

	m_fenceValue++;

	if (m_fence->GetCompletedValue() < fence)
	{
		result = m_fence->SetEventOnCompletion(fence, m_fenceEvent);
		if (Logger::IsFailureLog(result))
		{
			return;
		}

		WaitForSingleObject(m_fenceEvent, INFINITE);
	}
}

HRESULT RizaEngine::CommandManager::CreateCommandQueue()
{
	D3D12_COMMAND_QUEUE_DESC queueDesc = {};
	SecureZeroMemory(&queueDesc, sizeof(D3D12_COMMAND_QUEUE_DESC));
	queueDesc.Flags = D3D12_COMMAND_QUEUE_FLAG_NONE;
	queueDesc.Type = D3D12_COMMAND_LIST_TYPE_DIRECT;
	CHRESULT result = m_device->CreateCommandQueue(&queueDesc, IID_PPV_ARGS(&m_commandQueue));

	Logger::IsFailureLog(result);
	return result;
}

HRESULT RizaEngine::CommandManager::CreateCommandAllocator()
{
	CHRESULT result = m_device->CreateCommandAllocator(D3D12_COMMAND_LIST_TYPE_DIRECT, IID_PPV_ARGS(&m_commandAllocator));

	Logger::IsFailureLog(result);
	return result;
}

HRESULT RizaEngine::CommandManager::CreateGraphicsCommandList()
{
	HRESULT result = m_device->CreateCommandList(0, D3D12_COMMAND_LIST_TYPE_DIRECT, m_commandAllocator.Get(), nullptr, IID_PPV_ARGS(&m_graphicsCommandList));

	if (Logger::IsFailureLog(result) == false)
	{
		//NOTE: Call close func because default state is record.
		m_graphicsCommandList->Close();
	}
	return result;
}

HRESULT RizaEngine::CommandManager::CreateFence()
{
	HRESULT result = m_device->CreateFence(0, D3D12_FENCE_FLAG_NONE, IID_PPV_ARGS(&m_fence));
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

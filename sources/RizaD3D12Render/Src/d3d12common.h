// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once

#include"d3dx12.h"
#include <wrl.h>
#include <dxgi1_6.h>
#include <D3Dcompiler.h>

using Microsoft::WRL::ComPtr;
using Microsoft::WRL::WeakRef;

namespace RizaEngine
{
	typedef ID3D12Debug CID3D12Debug;
	typedef IDXGIFactory4 CIDXGIFactory;
	typedef ID3D12Device CID3D12Device;
	typedef IDXGISwapChain3 CIDXGISwapChain;
	typedef ID3D12CommandQueue CID3D12CommandQueue;
	typedef ID3D12DescriptorHeap CID3D12DescriptorHeap;
	typedef ID3D12Resource CID3D12Resource;
	typedef ID3D12CommandAllocator CID3D12CommandAllocator;
	typedef ID3D12GraphicsCommandList CID3D12GraphicsCommandList;
	typedef ID3D12Fence CID3D12Fence;

	typedef HRESULT CHRESULT;
}
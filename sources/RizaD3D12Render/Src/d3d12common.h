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
	typedef IDXGISwapChain1 CIDXGISwapChain;
	typedef ID3D12CommandQueue CID3D12CommandQueue;

	typedef HRESULT CHRESULT;
}
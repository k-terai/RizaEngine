// Copyright(c) k - terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once
#include"NonCopyable.h"
#include"d3d12common.h"
#include"Platform.h"
#include"SceneRenderer.h"

#include<vector>
#include<memory>

namespace RizaEngine
{
	class SceneRendererManager final : public NonCopyable
	{
	public:
		SceneRendererManager();
		virtual ~SceneRendererManager() override;

		bool Initialize();
		bool CreateForwardSceneRenderer(const whandle hwnd);

		void Update();

	private:
		CHRESULT EnableDebugLayer();
		CHRESULT CreateFactory();
		CHRESULT CreateDevice();

		CHRESULT GetHardwareAdapter(IDXGIFactory1* const pFactory, IDXGIAdapter1** ppAdapter, bool requestHighPerformanceAdapter = false);


	private:
		std::vector<std::unique_ptr<SceneRenderer>> m_sceneRenderers;
		uint32 m_dxgiFactoryFlags;
		bool m_useWarpDevice;
		ComPtr<CID3D12Debug> m_debugController;
		ComPtr<CIDXGIFactory> m_factory;
		ComPtr<CID3D12Device> m_device;
	};
}
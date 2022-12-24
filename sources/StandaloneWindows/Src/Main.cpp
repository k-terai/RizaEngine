// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.


#ifndef WIN32_LEAN_AND_MEAN
#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers.
#endif

#include <windows.h>
#include <string>
#include <wrl.h>
#include <shellapi.h>
#include"RizaFramework.h"

using namespace std;
using namespace RizaEngine;

HWND hwnd;
bool useWarpDevice = false;
std::wstring title = L"StandaloneWindows";
int width = 1270;
int height = 720;

int Run(HINSTANCE hInstance, int nCmdShow);
LRESULT CALLBACK WindowProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);
void ParseCommandLineArgs(_In_reads_(argc) WCHAR* argv[], int argc);
void SetCustomWindowText(LPCWSTR text);


_Use_decl_annotations_
int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE, LPSTR, int nCmdShow)
{
	return Run(hInstance, nCmdShow);
}

int Run(HINSTANCE hInstance, int nCmdShow)
{

	// Parse the command line parameters
	int argc;
	LPWSTR* argv = CommandLineToArgvW(GetCommandLineW(), &argc);
	ParseCommandLineArgs(argv, argc);
	LocalFree(argv);

	// Initialize the window class.
	WNDCLASSEX windowClass = { 0 };
	windowClass.cbSize = sizeof(WNDCLASSEX);
	windowClass.style = CS_HREDRAW | CS_VREDRAW;
	windowClass.lpfnWndProc = WindowProc;
	windowClass.hInstance = hInstance;
	windowClass.hCursor = LoadCursor(NULL, IDC_ARROW);
	windowClass.lpszClassName = L"StandaloneWIndows";
	RegisterClassEx(&windowClass);

	RECT windowRect = { 0, 0, static_cast<LONG>(width), static_cast<LONG>(height) };
	AdjustWindowRect(&windowRect, WS_OVERLAPPEDWINDOW, FALSE);

	auto ptr = RizaEngine::RizaFramework::GetInstance();
	ptr->Initialize();

	// Create the window and store a handle to it.
	hwnd = CreateWindow(
		windowClass.lpszClassName,
		title.c_str(),
		WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT,
		CW_USEDEFAULT,
		windowRect.right - windowRect.left,
		windowRect.bottom - windowRect.top,
		nullptr,        // We have no parent window.
		nullptr,        // We aren't using menus.
		hInstance,
		nullptr);

	ShowWindow(hwnd, nCmdShow);
	ptr->Startup();
	ptr->GetRenderModule()->CreateForwardSceneRender(hwnd);

	MSG msg = {};
	while (msg.message != WM_QUIT)
	{
		if (PeekMessage(&msg, NULL, 0, 0, PM_REMOVE))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}

		ptr->Update();
	}

	ptr->Terminate();

	// Return this part of the WM_QUIT message to Windows.
	return static_cast<char>(msg.wParam);
}


LRESULT CALLBACK WindowProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	switch (message)
	{
	case WM_CREATE:
	{
		LPCREATESTRUCT pCreateStruct = reinterpret_cast<LPCREATESTRUCT>(lParam);
		SetWindowLongPtr(hWnd, GWLP_USERDATA, reinterpret_cast<LONG_PTR>(pCreateStruct->lpCreateParams));
	}
	return 0;

	case WM_KEYDOWN:

		return 0;

	case WM_KEYUP:

		return 0;

	case WM_PAINT:

		return 0;

	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}

	return DefWindowProc(hWnd, message, wParam, lParam);
}

void ParseCommandLineArgs(WCHAR* argv[], int argc)
{
	for (int i = 1; i < argc; ++i)
	{
		if (_wcsnicmp(argv[i], L"-warp", wcslen(argv[i])) == 0 ||
			_wcsnicmp(argv[i], L"/warp", wcslen(argv[i])) == 0)
		{
			useWarpDevice = true;
			title = title + L" (WARP)";
		}
	}
}

void SetCustomWindowText(LPCWSTR text)
{
	std::wstring windowText = title + L": " + text;
	SetWindowText(hwnd, windowText.c_str());
}

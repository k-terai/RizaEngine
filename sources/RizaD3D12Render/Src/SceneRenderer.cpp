// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#include "pch.h"
#include "SceneRenderer.h"

using namespace RizaEngine;
using namespace std;

SceneRenderer::SceneRenderer() : m_frameCount(2), m_windowWidth(0), m_windowHeight(0), m_frameIndex(0)
{

}


SceneRenderer::~SceneRenderer()
{
	m_frameCount = 2;
	m_windowWidth = 0;
	m_windowHeight = 0;
	m_frameIndex = 0;
}

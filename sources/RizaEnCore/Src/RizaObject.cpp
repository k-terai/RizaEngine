// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#include "pch.h"
#include "RizaObject.h"
#include "Hasher.h"

using namespace std;
using namespace RizaEngine;

RizaObject::RizaObject() : m_uniqueId(c_InvalidUniqueId)
{
	m_uniqueId = Fnv1Hash32(reinterpret_cast<uint8*>(this), 64 /*64 == 64Bit pointer size*/);
}

RizaObject::~RizaObject()
{
	m_uniqueId = c_InvalidUniqueId;
}

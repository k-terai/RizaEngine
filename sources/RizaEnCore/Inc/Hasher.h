// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once

#include"Platform.h"

namespace RizaEngine
{
	static const uint32_t c_Fnv_Basis_32 = 2166136261U;
	static const uint64_t c_Fnv_Basis_64 = 14695981039346656037U;
	static const uint32_t c_Fnv_Prime_32 = 16777619U;
	static const uint64_t c_Fnv_Prime_64 = 1099511628211LLU;

	uint32 Fnv1Hash32(ctstring string);
	uint32 Fnv1Hash32(uint8* const bytes, const size_t length);

	uint64 Fnv1Hash64(ctstring string);
	uint64 Fnv1Hash64(uint8* const bytes, const size_t length);

	uint32 Fnv1HashLowercase32(ctstring string);
	uint64 Fnv1HashLowercase64(ctstring string);
}
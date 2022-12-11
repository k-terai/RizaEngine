// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#include "pch.h"
#include "Hasher.h"

using namespace std;

namespace RizaEngine
{
	uint32 Fnv1Hash32(ctstring string)
	{
		auto hval = c_Fnv_Basis_32;
		auto current = (uint8*)string;

		while (*current != 0)
		{
			hval = hval + (hval << 1) + (hval << 4) + (hval << 7) + (hval << 8) + (hval << 24);
			hval = hval ^ (uint32)(*current);
			++current;
		}

		return hval;
	}

	uint32 Fnv1Hash32(uint8* const bytes, const size_t length)
	{
		auto hval = c_Fnv_Basis_32;
		auto current = bytes;

		while (*current != 0)
		{
			hval = hval + (hval << 1) + (hval << 4) + (hval << 7) + (hval << 8) + (hval << 24);
			hval = hval ^ (uint32)(*current);
			++current;
		}

		return hval;
	}

	uint64 Fnv1Hash64(ctstring string)
	{
		auto hval = c_Fnv_Basis_64;
		auto current = (uint8*)string;

		while (*current != 0)
		{
			hval = hval + (hval << 1) + (hval << 4) + (hval << 5) + (hval << 7) + (hval << 8) + (hval << 40);
			hval = hval ^ (uint32)(*current);
			++current;
		}

		return hval;
	}

	uint64 Fnv1Hash64(uint8* const bytes, const size_t length)
	{
		auto hval = c_Fnv_Basis_64;
		auto current = bytes;

		while (*current != 0)
		{
			hval = hval + (hval << 1) + (hval << 4) + (hval << 5) + (hval << 7) + (hval << 8) + (hval << 40);
			hval = hval ^ (uint64)(*current);
			++current;
		}

		return hval;
	}

	uint32 Fnv1HashLowercase32(ctstring string)
	{
		auto hval = c_Fnv_Basis_32;
		auto current = (unsigned char*)string;

		while (*current != 0)
		{
			auto val = *current;
			if (val >= 'A' && val <= 'Z')
			{
				val += 'a' - 'A';
			}
			hval = hval + (hval << 1) + (hval << 4) + (hval << 7) + (hval << 8) + (hval << 24);
			hval = hval ^ (uint32)(val);
			++current;
		}

		return hval;
	}

	uint64 Fnv1HashLowercase64(ctstring string)
	{
		auto hval = c_Fnv_Basis_64;
		auto current = (unsigned char*)string;

		while (*current != 0)
		{
			auto val = *current;
			if (val >= 'A' && val <= 'Z')
			{
				val += 'a' - 'A';
			}
			hval = hval + (hval << 1) + (hval << 4) + (hval << 5) + (hval << 7) + (hval << 8) + (hval << 40);
			hval = hval ^ (uint64)(val);
			++current;
		}

		return hval;
	}
}


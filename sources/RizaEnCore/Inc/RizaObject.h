// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma once
#include"Common.h"
#include"Platform.h"

namespace RizaEngine
{
	class RizaObject
	{
	public:
		RizaObject();
		~RizaObject();

#if _DEBUG
		void SetDebugName(ctstring name)
		{
			m_debugName = name;
		}
#endif

	private:
		uint32 m_uniqueId;

#if _DEBUG
		tstring m_debugName;
#endif
	};
}
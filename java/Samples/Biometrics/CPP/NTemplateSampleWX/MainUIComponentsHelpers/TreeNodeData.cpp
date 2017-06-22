#include "Precompiled.h"
#include "TreeNodeData.h"

using namespace Neurotec;

namespace Neurotec { namespace Samples { namespace MainUIComponentsHelpers
{
	TreeNodeData::TreeNodeData(NObject obj, int ObjLocation) : m_object(obj), m_objectId(ObjLocation)
	{
	}

	TreeNodeData::~TreeNodeData()
	{
	}

	NObject TreeNodeData::GetObject()
	{
		return m_object;
	}

	int TreeNodeData::GetObjectId()
	{
		return m_objectId;
	}
}}}

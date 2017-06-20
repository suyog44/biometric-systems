#ifndef TREE_NODE_DATA_H_INCLUDED
#define TREE_NODE_DATA_H_INCLUDED

namespace Neurotec { namespace Samples{ namespace MainUIComponentsHelpers
{
	class TreeNodeData : public wxTreeItemData
	{
	public:
		TreeNodeData(Neurotec::NObject object, int ObjLocation);
		virtual ~TreeNodeData();

		Neurotec::NObject GetObject();
		int GetObjectId();

	private:
		Neurotec::NObject m_object;
		int m_objectId;
	};
}}}
#endif

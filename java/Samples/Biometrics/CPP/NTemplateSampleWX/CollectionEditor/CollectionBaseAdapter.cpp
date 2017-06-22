#include "Precompiled.h"
#include "CollectionBaseAdapter.h"

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	CollectionBaseAdapter::CollectionBaseAdapter()
	{
	}

	CollectionBaseAdapter::~CollectionBaseAdapter()
	{
	}

	int CollectionBaseAdapter::GetCollectionId()
	{
		return m_collectionId;
	}

	wxString CollectionBaseAdapter::GetListViewString()
	{
		return m_listViewString;
	}

	void CollectionBaseAdapter::SetListViewString(wxString lstring)
	{
		m_listViewString = lstring;
	}

	void CollectionBaseAdapter::SetCollectionName(int id)
	{
		m_collectionId = id;
	}
}}}

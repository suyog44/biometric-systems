#ifndef CORE_COLLECTION_ADAPTER_H_INCLUDED
#define CORE_COLLECTION_ADAPTER_H_INCLUDED

#include "CollectionBaseAdapter.h"

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	class CoreCollectionAdapter : public CollectionBaseAdapter
	{
	public:
		CoreCollectionAdapter(const Neurotec::Biometrics::NFRecord& record);
		~CoreCollectionAdapter();

		void AddItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index);
		void UpdateItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index);
		void ShowItemCollectionInGrid(wxPropertyGridManager* grid, int index);
		void DeleteItem(int index);
		wxString PropertyValueChanged(wxString propertyName, int index, wxVariant valu);
		int GetItemsCount();
		void ShowCollectionInList(wxListCtrl* ctrl, int index, wxString listString);
		void SwapItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index1, wxString s1, wxString s2);

	private:
		Neurotec::Biometrics::NFRecord m_record;
	};
		
}}}

#endif

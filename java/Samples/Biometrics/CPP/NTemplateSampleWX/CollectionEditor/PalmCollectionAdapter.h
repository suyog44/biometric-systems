#ifndef PALM_COLLECTION_ADAPTER_H_INCLUDED
#define PALM_COLLECTION_ADAPTER_H_INCLUDED

#include "CollectionBaseAdapter.h"

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	class PalmCollectionAdapter : public CollectionBaseAdapter
	{
	public:
		PalmCollectionAdapter(const Neurotec::Biometrics::NFTemplate& templ);
		~PalmCollectionAdapter();

		void AddItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index);
		void UpdateItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index);
		void ShowItemCollectionInGrid(wxPropertyGridManager* grid, int index);
		void DeleteItem(int index);
		wxString PropertyValueChanged(wxString propertyName, int index, wxVariant value);
		int GetItemsCount();
		void ShowCollectionInList(wxListCtrl* ctrl, int index, wxString listString);
		void SwapItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index1, wxString s1, wxString s2);

	private:

		Neurotec::Biometrics::NFTemplate m_template;
		wxArrayString m_collectionString;
	};
}}}
#endif

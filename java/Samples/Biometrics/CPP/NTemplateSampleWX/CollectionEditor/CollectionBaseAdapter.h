#ifndef COLLECTION_BASE_ADAPTER_H_INCLUDED
#define COLLECTION_BASE_ADAPTER_H_INCLUDED

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	class CollectionBaseAdapter
	{
	public:

		CollectionBaseAdapter();
		virtual ~CollectionBaseAdapter();

		virtual void AddItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index) = 0;
		virtual void UpdateItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index) = 0;
		virtual void ShowItemCollectionInGrid(wxPropertyGridManager* grid, int index) = 0;
		virtual int GetItemsCount() = 0;
		virtual void DeleteItem(int index) = 0;
		virtual wxString PropertyValueChanged(wxString propertyName, int index, wxVariant value) = 0;
		virtual void ShowCollectionInList(wxListCtrl* ctrl, int index, wxString listString) = 0;
		virtual void SwapItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index1, wxString s1, wxString s2) = 0;
		void SetCollectionName(int id);
		int GetCollectionId();
		wxString GetListViewString();
		void SetListViewString(wxString lstring);

	protected:
		wxString m_listViewString;
		int m_positionIndex;
		wxString m_positionValue;
		int m_collectionId;
	};
}}}
#endif

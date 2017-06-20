#include "Precompiled.h"
#include "MinutiaeNeighborsCollectionAdapter.h"

using namespace Neurotec::Biometrics;

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	MinutiaeNeighborsAdapter::MinutiaeNeighborsAdapter(const NFRecord& record) : m_record(record)
	{
	}

	MinutiaeNeighborsAdapter::~MinutiaeNeighborsAdapter()
	{
	}

	void MinutiaeNeighborsAdapter::AddItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		grid = NULL;
		index = 0;
		ctrl = NULL;
	}

	void MinutiaeNeighborsAdapter::SwapItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index1, wxString s1, wxString s2)
	{
		ShowCollectionInList(ctrl, index1 - 1, s1);
		ShowCollectionInList(ctrl, index1, s2);
		grid = NULL;
	}

	void MinutiaeNeighborsAdapter::UpdateItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		m_listViewString = N_TMPL_MINUT_NGB_ARR;

		ShowCollectionInList(ctrl, index, m_listViewString);
		
		if (m_record.GetMinutiae().GetCount() > 0)
			ShowItemCollectionInGrid(grid, index);
	}
	void MinutiaeNeighborsAdapter::ShowItemCollectionInGrid(wxPropertyGridManager* grid, int index)
	{
		wxPropertyGrid *pgrid = grid->GetGrid();
		pgrid->Clear();
		pgrid->Append(new wxPropertyCategory(wxT("NFMinutiaNeighbors"), wxPG_LABEL));
		for (int i = 0; i < m_record.GetMinutiaeNeighbors().GetCount(index); i++)
		{
			pgrid->Append(new wxStringProperty(wxString::Format("[%i]", i), wxPG_LABEL,
				wxString::Format("{Index=%i, RidgeCount=%i}", m_record.GetMinutiaeNeighbors().Get(index, i).Index, m_record.GetMinutiaeNeighbors().Get(index, i).RidgeCount)))->Enable(false);
		}
	}

	void  MinutiaeNeighborsAdapter::DeleteItem(int index)
	{
		index = 0;
	}

	wxString MinutiaeNeighborsAdapter::PropertyValueChanged(wxString propertyName, int index, wxVariant value)
	{
		index = 0;
		return N_TMPL_MINUT_NGB_ARR;
	}

	void MinutiaeNeighborsAdapter::ShowCollectionInList(wxListCtrl* ctrl, int index, wxString listString)
	{
		wxListItem item;
		item.SetId(index);
		item.SetText(wxString::Format(wxT("%i"), index));

		ctrl->InsertItem(item);
		ctrl->SetItem(item, 1, listString);
	}

	int MinutiaeNeighborsAdapter::GetItemsCount()
	{
		return m_record.GetMinutiae().GetCount();
	}
}}}

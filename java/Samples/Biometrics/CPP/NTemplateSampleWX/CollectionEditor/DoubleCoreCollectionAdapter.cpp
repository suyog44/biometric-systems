#include "Precompiled.h"
#include "DoubleCoreCollectionAdapter.h"

using namespace Neurotec;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	DoubleCoreCollectionAdapter::DoubleCoreCollectionAdapter(const NFRecord& record) : m_record(record)
	{
	}

	DoubleCoreCollectionAdapter::~DoubleCoreCollectionAdapter()
	{
	}

	void DoubleCoreCollectionAdapter::AddItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		int x = 0;
		int y = 0;

		try
		{
			m_listViewString = wxString::Format(wxT("{X=%i, Y=%i}"), x, y);

			m_record.GetDoubleCores().Add(NFDoubleCore(0, 0));

			ShowCollectionInList(ctrl, index, m_listViewString);
			ShowItemCollectionInGrid(grid, index);
		}
		catch (NError& ex)
		{
			wxExceptionDlg::Show(ex);
		}
	}

	void DoubleCoreCollectionAdapter::SwapItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index1, wxString s1, wxString s2)
	{
		NFDoubleCore doublecore1 = m_record.GetDoubleCores().Get(index1 - 1);
		NFDoubleCore doublecore2 = m_record.GetDoubleCores().Get(index1);

		m_record.GetDoubleCores().RemoveAt(index1 - 1);
		m_record.GetDoubleCores().Insert(index1 - 1, doublecore2);
		m_record.GetDoubleCores().RemoveAt(index1);
		m_record.GetDoubleCores().Insert(index1, doublecore1);

		ShowCollectionInList(ctrl, index1 - 1, s1);
		ShowCollectionInList(ctrl, index1, s2);

		ShowItemCollectionInGrid(grid, index1 - 1);
	}

	void DoubleCoreCollectionAdapter::UpdateItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		int x = 0;
		int y = 0;
		int doubleCoreCount;

		NFDoubleCore doubleCore = m_record.GetDoubleCores().Get(index);

		doubleCoreCount = m_record.GetDoubleCores().GetCount();

		x = doubleCore.X;
		y = doubleCore.Y;

		m_listViewString = wxString::Format(wxT("{X=%i, Y=%i}"), x, y);
		ShowCollectionInList(ctrl, index, m_listViewString);

		if (doubleCoreCount > 0){
			ShowItemCollectionInGrid(grid, 0);
		}
	}

	void DoubleCoreCollectionAdapter::ShowItemCollectionInGrid(wxPropertyGridManager* grid, int index)
	{
		wxPropertyGrid *pgrid = grid->GetGrid();

		NFDoubleCore doubleCore = m_record.GetDoubleCores().Get(index);

		pgrid->Clear();
		pgrid->Append(new wxPropertyCategory(wxT("NFDoubleCore"), wxPG_LABEL));
		pgrid->Append(new wxFloatProperty(N_TMPL_X, wxPG_LABEL, doubleCore.X));
		pgrid->Append(new wxFloatProperty(N_TMPL_Y, wxPG_LABEL, doubleCore.Y));
	}

	wxString DoubleCoreCollectionAdapter::PropertyValueChanged(wxString propertyName, int index, wxVariant value)
	{
		int x, y;

		NFDoubleCore doubleCore = m_record.GetDoubleCores().Get(index);

		x = doubleCore.X;
		y = doubleCore.Y;

		if (propertyName == N_TMPL_X)
			x = value.GetInteger();
		if (propertyName == N_TMPL_Y)
			y = value.GetInteger();

		m_record.GetDoubleCores().Set(index, Neurotec::Biometrics::NFDoubleCore(x, y));

		return wxString::Format(wxT("{X=%i, Y=%i}"), x, y);
	}

	void DoubleCoreCollectionAdapter::ShowCollectionInList(wxListCtrl* ctrl, int index, wxString listString)
	{
		wxListItem item;
		item.SetId(index);
		item.SetText(wxString::Format(wxT("%i"), index));

		ctrl->InsertItem(item);
		ctrl->SetItem(item, 1, listString);
	}

	void  DoubleCoreCollectionAdapter::DeleteItem(int index)
	{
		m_record.GetDoubleCores().RemoveAt(index);
	}

	int DoubleCoreCollectionAdapter::GetItemsCount()
	{
		return m_record.GetDoubleCores().GetCount();
	}
}}}

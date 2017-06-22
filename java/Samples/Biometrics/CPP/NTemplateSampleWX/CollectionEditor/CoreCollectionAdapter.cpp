#include "Precompiled.h"
#include "CoreCollectionAdapter.h"

using namespace Neurotec;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	CoreCollectionAdapter::CoreCollectionAdapter(const NFRecord& record) : m_record(record)
	{
	}

	CoreCollectionAdapter::~CoreCollectionAdapter()
	{
	}

	void CoreCollectionAdapter::AddItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		int x = 0;
		int y = 0;
		int angle = 0;

		try
		{
			m_listViewString = wxString::Format(wxT("{X=%i, Y=%i, Angle=%i}"), x, y, angle);

			m_record.GetCores().Add(NFCore(0, 0, 0));

			ShowCollectionInList(ctrl, index, m_listViewString);
			ShowItemCollectionInGrid(grid, index);
			SetListViewString(m_listViewString);
		}
		catch (NError& ex)
		{
			wxExceptionDlg::Show(ex);
		}
	}

	void CoreCollectionAdapter::SwapItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index1, wxString s1, wxString s2)
	{
		NFCore core1 = m_record.GetCores().Get(index1 - 1);
		NFCore core2 = m_record.GetCores().Get(index1);

		m_record.GetCores().RemoveAt(index1 - 1);
		m_record.GetCores().Insert(index1 - 1, core2);

		m_record.GetCores().RemoveAt(index1);
		m_record.GetCores().Insert(index1, core1);

		ShowCollectionInList(ctrl, index1 - 1, s1);
		ShowCollectionInList(ctrl, index1, s2);

		ShowItemCollectionInGrid(grid, index1 - 1);
	}

	void CoreCollectionAdapter::UpdateItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		int x = 0;
		int y = 0;
		int angle = 0;
		int coreCount;

		NFCore core = m_record.GetCores().Get(index);

		coreCount = m_record.GetCores().GetCount();
		x = core.X;
		y = core.Y;
		angle = core.Angle;

		m_listViewString = wxString::Format(wxT("{X=%i, Y=%i, Angle=%i}"), x, y, angle);

		ShowCollectionInList(ctrl, index, m_listViewString);
		
		if (coreCount > 0)
			ShowItemCollectionInGrid(grid, 0);

		SetListViewString(m_listViewString);
	}

	void CoreCollectionAdapter::ShowItemCollectionInGrid(wxPropertyGridManager* grid, int index)
	{

		wxPropertyGrid *pgrid = grid->GetGrid();

		NFCore core = m_record.GetCores().Get(index);

		pgrid->Clear();
		pgrid->Append(new wxPropertyCategory(wxT("NFCore"), wxPG_LABEL));
		pgrid->Append(new wxFloatProperty(N_TMPL_ANGLE, wxPG_LABEL, core.Angle));
		pgrid->Append(new wxFloatProperty(N_TMPL_X, wxPG_LABEL, core.X));
		pgrid->Append(new wxFloatProperty(N_TMPL_Y, wxPG_LABEL, core.Y));
	}

	int CoreCollectionAdapter::GetItemsCount()
	{
		return m_record.GetCores().GetCount();
	}

	void  CoreCollectionAdapter::DeleteItem(int index)
	{
		m_record.GetCores().RemoveAt(index);
	}

	wxString CoreCollectionAdapter::PropertyValueChanged(wxString propertyName, int index, wxVariant value)
	{
		int angle;
		int x;
		int y;

		NFCore core = m_record.GetCores().Get(index);

		angle = core.Angle;
		x = core.X;
		y = core.Y;

		if (propertyName == N_TMPL_ANGLE)
			angle = value.GetInteger();
		if (propertyName == N_TMPL_X)
			x = value.GetInteger();
		if (propertyName == N_TMPL_Y)
			y = value.GetInteger();

		m_record.GetCores().Set(index, NFCore(x, y, angle));

		return wxString::Format(wxT("{X=%i, Y=%i, Angle=%i}"), x, y, angle);
	}

	void CoreCollectionAdapter::ShowCollectionInList(wxListCtrl* ctrl, int index, wxString listString)
	{
		wxListItem item;
		item.SetId(index);
		item.SetText(wxString::Format(wxT("%i"), index));

		ctrl->InsertItem(item);
		ctrl->SetItem(item, 1, listString);
	}
}}}

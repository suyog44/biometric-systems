#include "Precompiled.h"
#include "DeltaCollectionAdapter.h"

using namespace Neurotec;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	DeltaCollectionAdaptor::DeltaCollectionAdaptor(const NFRecord& record) : m_record(record)
	{
	}

	DeltaCollectionAdaptor::~DeltaCollectionAdaptor()
	{
	}

	void DeltaCollectionAdaptor::AddItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		int x = 0;
		int y = 0;
		int angle1 = 0;
		int angle2 = 0;
		int angle3 = 0;

		try
		{
			m_listViewString = wxString::Format(wxT("{X=%i, Y=%i, Angle1=%i, Angle2=%i, Angle3=%i}"), x, y, angle1, angle2, angle3);
			m_record.GetDeltas().Add(NFDelta(0, 0, 0, 0, 0));

			ShowCollectionInList(ctrl, index, m_listViewString);
			ShowItemCollectionInGrid(grid, index);
		}
		catch (NError& ex)
		{
			wxExceptionDlg::Show(ex);
		}
	}

	void DeltaCollectionAdaptor::SwapItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index1, wxString s1, wxString s2)
	{
		NFDelta delta1 = m_record.GetDeltas().Get(index1 - 1);
		NFDelta delta2 = m_record.GetDeltas().Get(index1);

		m_record.GetDeltas().RemoveAt(index1 - 1);
		m_record.GetDeltas().Insert(index1 - 1, delta2);
		m_record.GetDeltas().RemoveAt(index1);
		m_record.GetDeltas().Insert(index1, delta1);

		ShowCollectionInList(ctrl, index1 - 1, s1);
		ShowCollectionInList(ctrl, index1, s2);

		ShowItemCollectionInGrid(grid, index1 - 1);
	}
	void DeltaCollectionAdaptor::UpdateItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		int x = 0;
		int y = 0;
		int angle1 = 0;
		int angle2 = 0;
		int angle3 = 0;
		int deltaCount;

		NFDelta delta = m_record.GetDeltas().Get(index);
		deltaCount = m_record.GetDeltas().GetCount();

		x = delta.X;
		y = delta.Y;
		angle1 = delta.Angle1;
		angle2 = delta.Angle2;
		angle3 = delta.Angle3;

		m_listViewString = wxString::Format(wxT("{X=%i, Y=%i, Angle1=%i, Angle2=%i, Angle3=%i}"), x, y, angle1, angle2, angle3);

		ShowCollectionInList(ctrl, index, m_listViewString);

		if (deltaCount > 0)
			ShowItemCollectionInGrid(grid, 0);
	}
	void DeltaCollectionAdaptor::ShowItemCollectionInGrid(wxPropertyGridManager* grid, int index)
	{
		wxPropertyGrid *pgrid = grid->GetGrid();

		NFDelta delta = m_record.GetDeltas().Get(index);

		pgrid->Clear();
		pgrid->Append(new wxPropertyCategory(wxT("NFDelta"), wxPG_LABEL));
		pgrid->Append(new wxFloatProperty(N_TMPL_ANGLE1, wxPG_LABEL, delta.Angle1));
		pgrid->Append(new wxFloatProperty(N_TMPL_ANGLE2, wxPG_LABEL, delta.Angle2));
		pgrid->Append(new wxFloatProperty(N_TMPL_ANGLE3, wxPG_LABEL, delta.Angle3));
		pgrid->Append(new wxFloatProperty(N_TMPL_X, wxPG_LABEL, delta.X));
		pgrid->Append(new wxFloatProperty(N_TMPL_Y, wxPG_LABEL, delta.Y));
	}

	wxString DeltaCollectionAdaptor::PropertyValueChanged(wxString propertyName, int index, wxVariant value)
	{
		int x = 0;
		int y = 0;
		int angle1 = 0;
		int angle2 = 0;
		int angle3 = 0;

		NFDelta delta = m_record.GetDeltas().Get(index);

		angle1 = delta.Angle1;
		angle2 = delta.Angle2;
		angle3 = delta.Angle3;
		x = delta.X;
		y = delta.Y;

		if (propertyName == N_TMPL_ANGLE1)
			angle1 = value.GetInteger();
		if (propertyName == N_TMPL_ANGLE2)
			angle2 = value.GetInteger();
		if (propertyName == N_TMPL_ANGLE3)
			angle3 = value.GetInteger();
		if (propertyName == N_TMPL_X)
			x = value.GetInteger();
		if (propertyName == N_TMPL_Y)
			y = value.GetInteger();

		m_record.GetDeltas().Set(index, NFDelta(x, y, angle1, angle2, angle3));

		return wxString::Format(wxT("{X=%i, Y=%i, Angle1=%i, Angle2=%i, Angle3=%i}"), x, y, angle1, angle2, angle3);
	}

	void DeltaCollectionAdaptor::ShowCollectionInList(wxListCtrl* ctrl, int index, wxString listString)
	{
		wxListItem item;
		item.SetId(index);
		item.SetText(wxString::Format(wxT("%i"), index));

		ctrl->InsertItem(item);
		ctrl->SetItem(item, 1, listString);
	}

	void  DeltaCollectionAdaptor::DeleteItem(int index)
	{
		m_record.GetDeltas().RemoveAt(index);
	}

	int DeltaCollectionAdaptor::GetItemsCount()
	{
		return m_record.GetDeltas().GetCount();
	}
}}}

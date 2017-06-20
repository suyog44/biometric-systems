#include "Precompiled.h"
#include "MinutiaeCollectionAdapter.h"

using namespace Neurotec;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	MinutiaeCollectionAdapter::MinutiaeCollectionAdapter(const NFRecord& record) : m_record(record)
	{
	}

	MinutiaeCollectionAdapter::~MinutiaeCollectionAdapter()
	{
	}

	void MinutiaeCollectionAdapter::AddItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		int x = 0;
		int y = 0;
		NFMinutiaType type = NFMinutiaType();
		int quality = 0;
		int curvature = 0;
		int g = 0;

		try
		{
			m_listViewString = wxString::Format(wxT("{X=%i, Y=%i, Type=%i, Angle=%i, Quality=%i, Curvature=%i, G=%i}"), x, y, type, quality, curvature, g);

			m_record.GetMinutiae().Add(NFMinutia(0, 0, type, 0, 0, 0, 0));

			ShowCollectionInList(ctrl, index, m_listViewString);
			ShowItemCollectionInGrid(grid, index);
		}
		catch (NError& ex)
		{
			wxExceptionDlg::Show(ex);
		}
	}

	void MinutiaeCollectionAdapter::SwapItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index1, wxString s1, wxString s2)
	{
		NFMinutia minutiae1 = m_record.GetMinutiae().Get(index1 - 1);
		NFMinutia minutiae2 = m_record.GetMinutiae().Get(index1);

		m_record.GetMinutiae().RemoveAt(index1 - 1);
		m_record.GetMinutiae().Insert(index1 - 1, minutiae2);
		m_record.GetMinutiae().RemoveAt(index1);
		m_record.GetMinutiae().Insert(index1, minutiae1);

		ShowCollectionInList(ctrl, index1 - 1, s1);
		ShowCollectionInList(ctrl, index1, s2);

		ShowItemCollectionInGrid(grid, index1 - 1);
	}

	void MinutiaeCollectionAdapter::UpdateItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		int x = 0;
		int y = 0;
		int angle;
		NFMinutiaType type = NFMinutiaType();
		int quality = 0;
		int curvature = 0;
		int g = 0;
		int minutiaeCount;
		NFMinutia minutiae = m_record.GetMinutiae().Get(index);

		x = minutiae.X;
		y = minutiae.Y;
		angle = minutiae.Angle;
		type = minutiae.Type;
		quality = minutiae.Quality;
		curvature = minutiae.Curvature;
		g = minutiae.G;

		minutiaeCount = m_record.GetMinutiae().GetCount();

		m_listViewString = wxString::Format(wxT("{X=%i, Y=%i, Type=%i, Angle=%i, Quality=%i, Curvature=%i, G=%i}"), x, y, type, angle, quality, curvature, g);

		ShowCollectionInList(ctrl, index, m_listViewString);
	
		if (minutiaeCount > 0)
			ShowItemCollectionInGrid(grid, 0);
	}

	void MinutiaeCollectionAdapter::ShowItemCollectionInGrid(wxPropertyGridManager* grid, int index)
	{
		wxPropertyGrid *pgrid = grid->GetGrid();

		wxPGChoices minutiaTypes;
		NArrayWrapper<int> minutiaTypeValues = NEnum::GetValues(NBiometricTypes::NFMinutiaTypeNativeTypeOf());

		for (int i = 0; i < minutiaTypeValues.GetCount(); i++)
		{
			minutiaTypes.Add(NEnum::ToString(NBiometricTypes::NFMinutiaTypeNativeTypeOf(), minutiaTypeValues[i]), minutiaTypeValues[i]);
		}

		NFMinutia minutiae = m_record.GetMinutiae().Get(index);
		
		pgrid->Clear();
		pgrid->Append(new wxPropertyCategory(wxT("NFMinutia"), wxPG_LABEL));
		pgrid->Append(new wxFloatProperty(N_TMPL_X, wxPG_LABEL, minutiae.X));
		pgrid->Append(new wxFloatProperty(N_TMPL_Y, wxPG_LABEL, minutiae.Y));
		pgrid->Append(new wxEnumProperty(N_TMPL_TYPE, wxPG_LABEL, minutiaTypes))->SetChoiceSelection(minutiae.Type);
		pgrid->Append(new wxFloatProperty(N_TMPL_ANGLE, wxPG_LABEL, minutiae.Angle));
		pgrid->Append(new wxFloatProperty(N_TMPL_QUALITY, wxPG_LABEL, minutiae.Quality));
		pgrid->Append(new wxFloatProperty(N_TMPL_CURVATURE, wxPG_LABEL, minutiae.Curvature));
		pgrid->Append(new wxFloatProperty(N_TMPL_G, wxPG_LABEL, minutiae.G));
	}

	wxString MinutiaeCollectionAdapter::PropertyValueChanged(wxString propertyName, int index, wxVariant value)
	{
		int x;
		int y;
		int angle;
		NFMinutiaType type;
		int quality;
		int curvature;
		int g;
		NFMinutia minutiae = m_record.GetMinutiae().Get(index);

		type = minutiae.Type;
		angle = minutiae.Angle;
		quality = minutiae.Quality;
		curvature = minutiae.Curvature;
		g = minutiae.G;
		x = minutiae.X;
		y = minutiae.Y;

		if (propertyName == N_TMPL_ANGLE)
			angle = value.GetInteger();
		if (propertyName == N_TMPL_QUALITY)
			quality = value.GetInteger();
		if (propertyName == N_TMPL_CURVATURE)
			curvature = value.GetInteger();
		if (propertyName == N_TMPL_TYPE)
			type = (NFMinutiaType)value.GetInteger();
		if (propertyName == N_TMPL_G)
			g = value.GetInteger();
		if (propertyName == N_TMPL_X)
			x = value.GetInteger();
		if (propertyName == N_TMPL_Y)
			y = value.GetInteger();

		m_record.GetMinutiae().Set(index, Neurotec::Biometrics::NFMinutia(x, y, type, angle, quality, curvature, g));

		return wxString::Format(wxT("{X=%i, Y=%i, Type=%i, Angle=%i, Quality=%i, Curvature=%i, G=%i}"), x, y, angle, type, quality, curvature, g);
	}

	void MinutiaeCollectionAdapter::ShowCollectionInList(wxListCtrl* ctrl, int index, wxString listString)
	{
		wxListItem item;
		item.SetId(index);
		item.SetText(wxString::Format(wxT("%i"), index));

		ctrl->InsertItem(item);
		ctrl->SetItem(item, 1, listString);
	}

	void  MinutiaeCollectionAdapter::DeleteItem(int index)
	{
		m_record.GetMinutiae().RemoveAt(index);
	}

	int MinutiaeCollectionAdapter::GetItemsCount()
	{
		return m_record.GetMinutiae().GetCount();
	}
}}}

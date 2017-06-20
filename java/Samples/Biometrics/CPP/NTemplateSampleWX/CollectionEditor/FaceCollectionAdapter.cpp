#include "Precompiled.h"
#include "FaceCollectionAdapter.h"

using namespace Neurotec::Biometrics;

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	FaceCollectionAdapter::FaceCollectionAdapter(const NLTemplate & templ) : m_template(templ)
	{
	}

	FaceCollectionAdapter::~FaceCollectionAdapter()
	{
	}

	void FaceCollectionAdapter::AddItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		m_listViewString = N_TMPL_NLREC;
		m_template.GetRecords().Add(NLRecord());

		ShowCollectionInList(ctrl, index, m_listViewString);
		ShowItemCollectionInGrid(grid, index);
	}

	void FaceCollectionAdapter::SwapItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index1, wxString s1, wxString s2)
	{
		NLRecord rec1 = m_template.GetRecords().Get(index1 - 1);
		NLRecord rec2 = m_template.GetRecords().Get(index1);

		m_template.GetRecords().RemoveAt(index1 - 1);
		m_template.GetRecords().Insert(index1 - 1, rec2);

		m_template.GetRecords().RemoveAt(index1);
		m_template.GetRecords().Insert(index1, rec1);

		ShowCollectionInList(ctrl, index1 - 1, s1);
		ShowCollectionInList(ctrl, index1, s2);

		ShowItemCollectionInGrid(grid, index1 - 1);
	}

	void FaceCollectionAdapter::UpdateItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		int recordCount = m_template.GetRecords().GetCount();

		m_listViewString = N_TMPL_NLREC;

		ShowCollectionInList(ctrl, index, m_listViewString);

		if (recordCount > 0)
			ShowItemCollectionInGrid(grid, index);
	}

	void FaceCollectionAdapter::ShowItemCollectionInGrid(wxPropertyGridManager* grid, int index)
	{
		wxPropertyGrid *pgrid = grid->GetGrid();

		NLRecord record = m_template.GetRecords().Get(index);

		pgrid->Clear();
		pgrid->Append(new wxPropertyCategory(N_TMPL_NLREC, wxPG_LABEL));
		grid->Append(new wxIntProperty(N_TMPL_CBEFF_PRODUCT_TYPE, wxPG_LABEL, record.GetCbeffProductType()));
		grid->Append(new wxIntProperty(N_TMPL_FLAGS, wxPG_LABEL, record.GetFlags()))->Enable(false);
		grid->Append(new wxIntProperty(N_TMPL_QUALITY, wxPG_LABEL, record.GetQuality()));
	}

	int FaceCollectionAdapter::GetItemsCount()
	{
		return m_template.GetRecords().GetCount();
	}

	void  FaceCollectionAdapter::DeleteItem(int index)
	{
		m_template.GetRecords().RemoveAt(index);
	}

	wxString FaceCollectionAdapter::PropertyValueChanged(wxString propertyName, int index, wxVariant value)
	{
		NLRecord record = m_template.GetRecords().Get(index);

		int cbeffProductType = record.GetCbeffProductType();
		int flags = record.GetFlags();
		int quality = record.GetQuality();

		if (propertyName == N_TMPL_CBEFF_PRODUCT_TYPE)
			cbeffProductType = value.GetInteger();
		if (propertyName == N_TMPL_FLAGS)
			flags = value.GetInteger();
		if (propertyName == N_TMPL_QUALITY)
			quality = value.GetInteger();

		record.SetCbeffProductType(cbeffProductType);
		record.SetFlags(flags);
		record.SetQuality(quality);

		m_template.GetRecords().Set(index, record);

		return N_TMPL_NLREC;
	}

	void FaceCollectionAdapter::ShowCollectionInList(wxListCtrl* ctrl, int index, wxString listString)
	{
		wxListItem item;
		item.SetId(index);
		item.SetText(wxString::Format(wxT("%i"), index));

		ctrl->InsertItem(item);
		ctrl->SetItem(item, 1, listString);
	}
}}}

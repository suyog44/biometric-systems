#include "Precompiled.h"
#include "IrisCollectionAdapter.h"
#include "../RecordAddDialogs/NIrisRecordDlg.h"

using namespace Neurotec::Biometrics;

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	IrisCollectionAdapter::IrisCollectionAdapter(const NETemplate& templ) : m_template(templ)
	{
	}

	IrisCollectionAdapter::~IrisCollectionAdapter()
	{
	}

	void IrisCollectionAdapter::AddItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		NIrisRecordDlg addIRecDlg(NULL);

		if (addIRecDlg.ShowModal() == wxID_OK)
		{
			m_listViewString = N_TMPL_NEREC;
			m_template.GetRecords().Add(NERecord(addIRecDlg.GetWidth(), addIRecDlg.GetHeight()));

			ShowCollectionInList(ctrl, index, m_listViewString);
			ShowItemCollectionInGrid(grid, index);
		}
	}

	void IrisCollectionAdapter::SwapItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index1, wxString s1, wxString s2)
	{
		NERecord rec1 = m_template.GetRecords().Get(index1 - 1);
		NERecord rec2 = m_template.GetRecords().Get(index1);

		m_template.GetRecords().RemoveAt(index1 - 1);
		m_template.GetRecords().Insert(index1 - 1, rec2);

		m_template.GetRecords().RemoveAt(index1);
		m_template.GetRecords().Insert(index1, rec1);

		ShowCollectionInList(ctrl, index1 - 1, s1);
		ShowCollectionInList(ctrl, index1, s2);

		ShowItemCollectionInGrid(grid, index1 - 1);
	}

	void IrisCollectionAdapter::UpdateItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		int recordCount = m_template.GetRecords().GetCount();

		m_listViewString = wxString::Format(N_TMPL_NEREC);

		ShowCollectionInList(ctrl, index, m_listViewString);
	
		if (recordCount > 0){
			ShowItemCollectionInGrid(grid, 0);
		}
	}

	void IrisCollectionAdapter::ShowItemCollectionInGrid(wxPropertyGridManager* grid, int index)
	{
		wxPropertyGrid *pgrid = grid->GetGrid();

		wxPGChoices positions;

		NArrayWrapper<int> positionValues = NEnum::GetValues(NBiometricTypes::NEPositionNativeTypeOf());

		for (int i = 0; i < positionValues.GetCount(); i++)
		{
			positions.Add(NEnum::ToString(NBiometricTypes::NEPositionNativeTypeOf(), positionValues[i]), positionValues[i]);
		}

		NERecord record = m_template.GetRecords().Get(index);

		pgrid->Clear();
		pgrid->Append(new wxPropertyCategory(N_TMPL_NEREC, wxPG_LABEL));
		pgrid->Append(new wxIntProperty(N_TMPL_CBEFF_PRODUCT_TYPE, wxPG_LABEL, record.GetCbeffProductType()));
		pgrid->Append(new wxIntProperty(N_TMPL_FLAGS, wxPG_LABEL, record.GetFlags()))->Enable(false);
		pgrid->Append(new wxIntProperty(N_TMPL_HEIGHT, wxPG_LABEL, record.GetHeight()))->Enable(false);
		pgrid->Append(new wxEnumProperty(N_TMPL_POS, wxPG_LABEL, positions))->SetChoiceSelection(record.GetPosition());
		pgrid->Append(new wxIntProperty(N_TMPL_QUALITY, wxPG_LABEL, record.GetQuality()));
		pgrid->Append(new wxIntProperty(N_TMPL_WIDTH, wxPG_LABEL, record.GetWidth()))->Enable(false);
	}

	int IrisCollectionAdapter::GetItemsCount()
	{
		return m_template.GetRecords().GetCount();
	}

	void  IrisCollectionAdapter::DeleteItem(int index)
	{
		m_template.GetRecords().RemoveAt(index);
	}

	wxString IrisCollectionAdapter::PropertyValueChanged(wxString propertyName, int index, wxVariant value)
	{
		NERecord record = m_template.GetRecords().Get(index);

		int cbeffProductType = record.GetCbeffProductType();
		int quality = record.GetQuality();
		NEPosition position = record.GetPosition();

		if (propertyName == N_TMPL_CBEFF_PRODUCT_TYPE)
			cbeffProductType = value.GetInteger();
		if (propertyName == N_TMPL_POS)
			position = (NEPosition)value.GetInteger();
		if (propertyName == N_TMPL_QUALITY)
			quality = value.GetInteger();

		record.SetCbeffProductType(cbeffProductType);
		record.SetQuality(quality);
		record.SetPosition(position);

		return N_TMPL_NEREC;
	}

	void IrisCollectionAdapter::ShowCollectionInList(wxListCtrl* ctrl, int index, wxString listString)
	{
		wxListItem item;
		item.SetId(index);
		item.SetText(wxString::Format(wxT("%i"), index));

		ctrl->InsertItem(item);
		ctrl->SetItem(item, 1, listString);
	}
}}}

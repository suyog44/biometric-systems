#include "Precompiled.h"
#include "VoiceCollectionAdapter.h"

using namespace Neurotec::Biometrics;

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	VoiceCollectionAdapter::VoiceCollectionAdapter(const NSTemplate& templ) : m_template(templ)
	{
	}

	VoiceCollectionAdapter::~VoiceCollectionAdapter()
	{
	}

	void VoiceCollectionAdapter::AddItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		m_listViewString = N_TMPL_NSREC;
		m_template.GetRecords().Add(NSRecord());

		ShowCollectionInList(ctrl, index, m_listViewString);
		ShowItemCollectionInGrid(grid, index);
	}

	void VoiceCollectionAdapter::SwapItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index1, wxString s1, wxString s2)
	{
		NSRecord rec1 = m_template.GetRecords().Get(index1 - 1);
		NSRecord rec2 = m_template.GetRecords().Get(index1);

		m_template.GetRecords().RemoveAt(index1 - 1);
		m_template.GetRecords().Insert(index1 - 1, rec2);

		m_template.GetRecords().RemoveAt(index1);
		m_template.GetRecords().Insert(index1, rec1);

		ShowCollectionInList(ctrl, index1 - 1, s1);
		ShowCollectionInList(ctrl, index1, s2);

		ShowItemCollectionInGrid(grid, index1 - 1);
	}

	void VoiceCollectionAdapter::UpdateItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		int recordCount = m_template.GetRecords().GetCount();

		m_listViewString = N_TMPL_NSREC;

		ShowCollectionInList(ctrl, index, m_listViewString);
		
		if (recordCount > 0)
			ShowItemCollectionInGrid(grid, 0);
	}

	void VoiceCollectionAdapter::ShowItemCollectionInGrid(wxPropertyGridManager* grid, int index)
	{
		wxPropertyGrid *pgrid = grid->GetGrid();

		NSRecord record = m_template.GetRecords().Get(index);
		pgrid->Clear();
		pgrid->Append(new wxPropertyCategory(N_TMPL_NSREC, wxPG_LABEL));
		pgrid->Append(new wxIntProperty(N_TMPL_CBEFF_PRODUCT_TYPE, wxPG_LABEL, record.GetCbeffProductType()));
		pgrid->Append(new wxIntProperty(N_TMPL_FLAGS, wxPG_LABEL, record.GetFlags()))->Enable(false);
		pgrid->Append(new wxBoolProperty(N_TMPL_HAS_TEXT_DEP_FEATURES, wxPG_LABEL, record.GetHasTextDependentFeatures() != 0));
		pgrid->Append(new wxBoolProperty(N_TMPL_HAS_TEXT_INDEP_FEATURES, wxPG_LABEL, record.GetHasTextIndependentFeatures() != 0));
		pgrid->Append(new wxIntProperty(N_TMPL_PHRASE_ID, wxPG_LABEL, record.GetPhraseId()));
		pgrid->Append(new wxIntProperty(N_TMPL_QUALITY, wxPG_LABEL, record.GetQuality()));
		pgrid->Append(new wxIntProperty(N_TMPL_SNR, wxPG_LABEL, record.GetSnr()));
	}

	int VoiceCollectionAdapter::GetItemsCount()
	{
		return m_template.GetRecords().GetCount();
	}

	void  VoiceCollectionAdapter::DeleteItem(int index)
	{
		m_template.GetRecords().RemoveAt(index);
	}

	wxString VoiceCollectionAdapter::PropertyValueChanged(wxString propertyName, int index, wxVariant value)
	{
		NSRecord record = m_template.GetRecords().Get(index);
		int cbeffProductType = record.GetCbeffProductType();
		int hasTextDependentFeatures = record.GetHasTextDependentFeatures();
		int hasTextIndependentFeatures = record.GetHasTextIndependentFeatures();
		int phraseId = record.GetPhraseId();
		int quality = record.GetQuality();
		int snr = record.GetSnr();

		if (propertyName == N_TMPL_CBEFF_PRODUCT_TYPE)
			cbeffProductType = value.GetInteger();
		if (propertyName == N_TMPL_HAS_TEXT_DEP_FEATURES)
			hasTextDependentFeatures = value.GetInteger();
		if (propertyName == N_TMPL_HAS_TEXT_INDEP_FEATURES)
			hasTextIndependentFeatures = value.GetInteger();
		if (propertyName == N_TMPL_PHRASE_ID)
			phraseId = value.GetInteger();
		if (propertyName == N_TMPL_QUALITY)
			quality = value.GetInteger();
		if (propertyName == N_TMPL_SNR)
			snr = value.GetInteger();

		record.SetCbeffProductType(cbeffProductType);
		record.SetHasTextDependentFeatures(hasTextDependentFeatures);
		record.SetHasTextIndependentFeatures(hasTextIndependentFeatures);
		record.SetPhraseId(phraseId);
		record.SetQuality(quality);
		record.SetSnr(snr);

		m_template.GetRecords().Set(index, record);

		return N_TMPL_NSREC;
	}

	void VoiceCollectionAdapter::ShowCollectionInList(wxListCtrl* ctrl, int index, wxString listString)
	{
		wxListItem item;
		item.SetId(index);
		item.SetText(wxString::Format(wxT("%i"), index));

		ctrl->InsertItem(item);
		ctrl->SetItem(item, 1, listString);
	}
}}}

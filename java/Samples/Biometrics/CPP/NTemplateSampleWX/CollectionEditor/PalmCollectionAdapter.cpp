#include "Precompiled.h"
#include "PalmCollectionAdapter.h"
#include "NCollectionProperty.h"
#include "../RecordAddDialogs/NFingerRecordDlg.h"
#include "MultiChoiceProperty.h"

using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	PalmCollectionAdapter::PalmCollectionAdapter(const NFTemplate& templ) : m_template(templ)
	{
		m_collectionString.Add(wxT("(Collection)"));
	}

	PalmCollectionAdapter::~PalmCollectionAdapter()
	{
	}

	void PalmCollectionAdapter::AddItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		NFingerRecordDlg addFRecDlg(NULL, true);

		if (addFRecDlg.ShowModal() == wxID_OK)
		{
			m_listViewString = N_TMPL_NFREC;
			m_template.GetRecords().Add(NFRecord(true, addFRecDlg.GetWidth(), addFRecDlg.GetHeight(), addFRecDlg.GetHorizontalResolution(), addFRecDlg.GetVirticalResolution()));

			ShowCollectionInList(ctrl, index, m_listViewString);
			ShowItemCollectionInGrid(grid, index);
		}
	}

	void PalmCollectionAdapter::SwapItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index1, wxString s1, wxString s2)
	{
		NFRecord rec1 = m_template.GetRecords().Get(index1 - 1);
		NFRecord rec2 = m_template.GetRecords().Get(index1);

		m_template.GetRecords().RemoveAt(index1 - 1);
		m_template.GetRecords().Insert(index1 - 1, rec2);

		m_template.GetRecords().RemoveAt(index1);
		m_template.GetRecords().Insert(index1, rec1);

		ShowCollectionInList(ctrl, index1 - 1, s1);
		ShowCollectionInList(ctrl, index1, s2);

		ShowItemCollectionInGrid(grid, index1 - 1);
	}

	void PalmCollectionAdapter::UpdateItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		int recordCount = m_template.GetRecords().GetCount();

		m_listViewString = N_TMPL_NFREC;

		ShowCollectionInList(ctrl, index, m_listViewString);
		
		if (recordCount > 0)
			ShowItemCollectionInGrid(grid, 0);
	}

	void PalmCollectionAdapter::ShowItemCollectionInGrid(wxPropertyGridManager* grid, int index)
	{
		wxPropertyGrid *pgrid = grid->GetGrid();

		wxPGChoices impressionTypeChoices, patternClassChoices, positionChoices, ridgeCountsTypeChoices;
		wxArrayString minutiaFormatChoices;
		wxArrayString minutiaFormatSelectedValues;

		NArrayWrapper<int> impressionsValues = NEnum::GetValues(NBiometricTypes::NFImpressionTypeNativeTypeOf());
		NArrayWrapper<int> positionValues = NEnum::GetValues(NBiometricTypes::NFPositionNativeTypeOf());
		NArrayWrapper<int> patternClassValues = NEnum::GetValues(NBiometricTypes::NFPatternClassNativeTypeOf());
		NArrayWrapper<int> minutiaFormatValues = NEnum::GetValues(NBiometricTypes::NFMinutiaFormatNativeTypeOf());
		NArrayWrapper<int> ridgeCountsTypeValues = NEnum::GetValues(NBiometricTypes::NFRidgeCountsTypeNativeTypeOf());

		for (int i = 0; i < impressionsValues.GetCount(); i++)
		{
			if (NBiometricTypes::IsImpressionTypePalm((NFImpressionType)impressionsValues[i])) impressionTypeChoices.Add(NEnum::ToString(NBiometricTypes::NFImpressionTypeNativeTypeOf(), impressionsValues[i]), impressionsValues[i]);
		}

		for (int i = 0; i < positionValues.GetCount(); i++)
		{
			if (NBiometricTypes::IsPositionPalm((NFPosition)positionValues[i])) positionChoices.Add(NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), positionValues[i]), positionValues[i]);
		}

		for (int i = 0; i < patternClassValues.GetCount(); i++)
		{
			patternClassChoices.Add(NEnum::ToString(NBiometricTypes::NFPatternClassNativeTypeOf(), patternClassValues[i]), patternClassValues[i]);
		}

		for (int i = 0; i < minutiaFormatValues.GetCount(); i++)
		{
			minutiaFormatChoices.Add(NEnum::ToString(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), minutiaFormatValues[i]));
		}

		for (int i = 0; i < ridgeCountsTypeValues.GetCount(); i++)
		{
			ridgeCountsTypeChoices.Add(NEnum::ToString(NBiometricTypes::NFRidgeCountsTypeNativeTypeOf(), ridgeCountsTypeValues[i]), ridgeCountsTypeValues[i]);
		}

		NFRecord record = m_template.GetRecords().Get(index);

		int minutiaFormatValue = (int)record.GetMinutiaFormat();

		if ((nfmfHasCurvature | nfmfHasQuality) == minutiaFormatValue)
		{
			minutiaFormatSelectedValues.Add(NEnum::ToString(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), nfmfHasQuality));
			minutiaFormatSelectedValues.Add(NEnum::ToString(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), nfmfHasCurvature));
		}
		else if ((nfmfHasG | nfmfHasQuality) == minutiaFormatValue)
		{
			minutiaFormatSelectedValues.Add(NEnum::ToString(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), nfmfHasQuality));
			minutiaFormatSelectedValues.Add(NEnum::ToString(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), nfmfHasG));
		}
		else if ((nfmfHasG | nfmfHasCurvature) == minutiaFormatValue)
		{
			minutiaFormatSelectedValues.Add(NEnum::ToString(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), nfmfHasCurvature));
			minutiaFormatSelectedValues.Add(NEnum::ToString(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), nfmfHasG));
		}
		else if ((nfmfHasCurvature | nfmfHasQuality | nfmfHasG) == minutiaFormatValue)
		{
			minutiaFormatSelectedValues.Add(NEnum::ToString(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), nfmfHasQuality));
			minutiaFormatSelectedValues.Add(NEnum::ToString(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), nfmfHasCurvature));
			minutiaFormatSelectedValues.Add(NEnum::ToString(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), nfmfHasG));
		}
		else
		{
			minutiaFormatSelectedValues.Add(NEnum::ToString(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), minutiaFormatValue));
		}

		NTemplate ntemplate;
		ntemplate.SetPalms(m_template);

		pgrid->Clear();
		pgrid->Append(new wxPropertyCategory(wxT("NFRecord"), wxPG_LABEL));
		pgrid->Append(new wxIntProperty(N_TMPL_CBEFF_PRODUCT_TYPE, wxPG_LABEL, record.GetCbeffProductType()));
		pgrid->Append(new NCollectionProperty(N_TMPL_CORES, wxPG_LABEL, m_collectionString, ntemplate, PALM_CORE_COLLECTION, index));
		pgrid->LimitPropertyEditing(N_TMPL_CORES, true);
		pgrid->Append(new NCollectionProperty(N_TMPL_DELTAS, wxPG_LABEL, m_collectionString, ntemplate, PALM_DELTA_COLLECTION, index));
		pgrid->LimitPropertyEditing(N_TMPL_DELTAS, true);
		pgrid->Append(new NCollectionProperty(N_TMPL_DOUBLE_CORES, wxPG_LABEL, m_collectionString, ntemplate, PALM_DOUBLE_CORE_COLLECTION, index));
		pgrid->LimitPropertyEditing(N_TMPL_DOUBLE_CORES, true);
		pgrid->Append(new wxIntProperty(N_TMPL_FLAGS, wxPG_LABEL, record.GetFlags()))->Enable(false);
		pgrid->Append(new wxIntProperty(N_TMPL_G, wxPG_LABEL, record.GetG()));
		pgrid->Append(new wxIntProperty(N_TMPL_HEIGHT, wxPG_LABEL, record.GetHeight()))->Enable(false);
		pgrid->Append(new wxIntProperty(N_TMPL_HORIZ_RESOLUTION, wxPG_LABEL, record.GetHorzResolution()))->Enable(false);
		pgrid->Append(new wxEnumProperty(N_TMPL_IMPR_TYPE, wxPG_LABEL, impressionTypeChoices))->SetChoiceSelection(record.GetImpressionType());
		pgrid->Append(new NCollectionProperty(N_TMPL_MINUTIAE, wxPG_LABEL, m_collectionString, ntemplate, PALM_MINUTIAE_COLLECTION, index));
		pgrid->LimitPropertyEditing(N_TMPL_MINUTIAE, true);
		pgrid->Append(new NCollectionProperty(N_TMPL_MINUTIAE_NEIGHB, wxPG_LABEL, m_collectionString, ntemplate, PALM_MINUTIAE_NEIGHBOUR_COLLECTION, index));
		pgrid->LimitPropertyEditing(N_TMPL_MINUTIAE_NEIGHB, true);
		pgrid->Append(new MultiChoiceProperty(N_TMPL_MINUTIAE_FORMAT, wxPG_LABEL, minutiaFormatChoices, minutiaFormatSelectedValues));
		pgrid->Append(new wxEnumProperty(N_TMPL_PATTERN_CLS, wxPG_LABEL, patternClassChoices))->SetChoiceSelection(record.GetPatternClass());
		pgrid->Append(new wxEnumProperty(N_TMPL_POS, wxPG_LABEL, positionChoices))->SetChoiceSelection(record.GetPosition());
		pgrid->Append(new NCollectionProperty(N_TMPL_POSSIBLE_POS, wxPG_LABEL, m_collectionString, ntemplate, PALM_POSSIBLE_POSITION_COLLECTION, index));
		pgrid->LimitPropertyEditing(N_TMPL_POSSIBLE_POS, true);
		pgrid->Append(new wxIntProperty(N_TMPL_QUALITY, wxPG_LABEL, record.GetQuality()));
		pgrid->Append(new wxBoolProperty(N_TMPL_REQ_UPDATE, wxPG_LABEL, record.GetRequiresUpdate()));
		pgrid->Append(new wxEnumProperty(N_TMPL_RIDGE_CNT_TYPE, wxPG_LABEL, ridgeCountsTypeChoices))->SetChoiceSelection(record.GetRidgeCountsType());
		pgrid->Append(new wxIntProperty(N_TMPL_VERT_RESOLUTION, wxPG_LABEL, record.GetVertResolution()))->Enable(false);
		pgrid->Append(new wxIntProperty(N_TMPL_WIDTH, wxPG_LABEL, record.GetWidth()))->Enable(false);
	}

	int PalmCollectionAdapter::GetItemsCount()
	{
		return m_template.GetRecords().GetCount();
	}

	void  PalmCollectionAdapter::DeleteItem(int index)
	{
		m_template.GetRecords().RemoveAt(index);
	}

	wxString PalmCollectionAdapter::PropertyValueChanged(wxString propertyName, int index, wxVariant value)
	{
		NFRecord record = m_template.GetRecords().Get(index);

		int cbeffProductType = record.GetCbeffProductType();
		int g = record.GetG();
		NFImpressionType impressionType = record.GetImpressionType();
		NFMinutiaFormat minutiaFormat = record.GetMinutiaFormat();
		NFPatternClass patternClass = record.GetPatternClass();
		NFPosition position = record.GetPosition();
		int quality = record.GetQuality();
		bool requiresUpdate = record.GetRequiresUpdate();
		NFRidgeCountsType ridgeCountsType = record.GetRidgeCountsType();

		if (propertyName == N_TMPL_CBEFF_PRODUCT_TYPE)
			cbeffProductType = value.GetInteger();
		if (propertyName == N_TMPL_G)
			g = value.GetInteger();
		if (propertyName == N_TMPL_IMPR_TYPE)
			impressionType = (NFImpressionType)value.GetInteger();
		if (propertyName == N_TMPL_MINUTIAE_FORMAT)
		{
			wxStringTokenizer tokenizer(value.GetString(), ";");
			wxString minutiaFormatString;
			int count = 0;

			while (tokenizer.HasMoreTokens())
			{
				minutiaFormatString = tokenizer.GetNextToken();
				count += NEnum::Parse(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), minutiaFormatString);
			}

			minutiaFormat = (NFMinutiaFormat)count;
		}
		if (propertyName == N_TMPL_PATTERN_CLS)
			patternClass = (NFPatternClass)value.GetInteger();
		if (propertyName == N_TMPL_POS)
			position = (NFPosition)value.GetInteger();
		if (propertyName == N_TMPL_QUALITY)
			quality = value.GetInteger();
		if (propertyName == N_TMPL_REQ_UPDATE)
			requiresUpdate = value.GetBool();
		if (propertyName == N_TMPL_RIDGE_CNT_TYPE)
			ridgeCountsType = (NFRidgeCountsType)value.GetInteger();
		try
		{
			record.SetCbeffProductType(cbeffProductType);
			record.SetG(g);
			record.SetImpressionType(impressionType);
			record.SetMinutiaFormat(minutiaFormat);
			record.SetPatternClass(patternClass);
			record.SetPosition(position);
			record.SetQuality(quality);
			record.SetRequiresUpdate(requiresUpdate);
			record.SetRidgeCountsType(ridgeCountsType);

			m_template.GetRecords().Set(index, record);
		}
		catch (NError& ex)
		{
			wxExceptionDlg::Show(ex);
		}

		return wxT("NFRecord");
	}

	void PalmCollectionAdapter::ShowCollectionInList(wxListCtrl* ctrl, int index, wxString listString)
	{
		wxListItem item;
		item.SetId(index);
		item.SetText(wxString::Format(wxT("%i"), index));

		ctrl->InsertItem(item);
		ctrl->SetItem(item, 1, listString);
	}
}}}

#include "Precompiled.h"
#include "TemplateSamplePropertyGrid.h"
#include "../CollectionEditor/NCollectionProperty.h"
#include "../CollectionEditor/MultiChoiceProperty.h"

using namespace Neurotec;
using namespace Neurotec::Gui;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Samples::CollectionEditor;

namespace Neurotec { namespace Samples { namespace MainUIComponentsHelpers
{
	TemplateSamplePropertyGrid::TemplateSamplePropertyGrid(wxWindow *parent, wxWindowID id, const wxPoint& pos, const wxSize& size, long style) :
		wxPropertyGrid(parent, id, pos, size, style)
	{
		m_collectionString.Add(wxT("(Collection)"));
	}

	TemplateSamplePropertyGrid::~TemplateSamplePropertyGrid()
	{
	}

	void TemplateSamplePropertyGrid::UpdatePropertyGrid(Neurotec::NObject dataObject, int currentItemId)
	{
		CommitChangesFromEditor();
		UnfocusEditor();

		NTemplate nTemplate = NULL;
		NFTemplate nfTemplate = NULL;
		NLTemplate nlTemplate = NULL;
		NETemplate neTemplate = NULL;
		NSTemplate nsTemplate = NULL;
		NFRecord nfRecord = NULL;
		NLRecord nlRecord = NULL;
		NERecord neRecord = NULL;
		NSRecord nsRecord = NULL;

		wxString type = dataObject.ToString();

		if (type == N_TMPL_NTMPL) nTemplate = (NTemplate&)dataObject;
		else if (type == N_TMPL_NFTMPL)	nfTemplate = (NFTemplate&)dataObject;
		else if (type == N_TMPL_NLTMPL)	nlTemplate = (NLTemplate&)dataObject;
		else if (type == N_TMPL_NETMPL)	neTemplate = (NETemplate&)dataObject;
		else if (type == N_TMPL_NSTMPL)	nsTemplate = (NSTemplate&)dataObject;
		else if (type == N_TMPL_NFREC) nfRecord = (NFRecord&)dataObject;
		else if (type == N_TMPL_NLREC) nlRecord = (NLRecord&)dataObject;
		else if (type == N_TMPL_NEREC) neRecord = (NERecord&)dataObject;
		else if (type == N_TMPL_NSREC) nsRecord = (NSRecord&)dataObject;

		if (!nTemplate.IsNull())
		{
			Clear();
			Append(new wxStringProperty(N_TMPL_FLAGS, wxPG_LABEL, wxString::Format("%x", m_template.GetFlags()).MakeUpper()))->Enable(false);

			if (!m_template.GetFingers().IsNull())
			{
				fingerTemplateProperty = Append(new wxStringProperty(N_TMPL_FINGERS, wxPG_LABEL, N_TMPL_NFTMPL));
				fingerTemplateProperty->DeleteChildren();
				AppendIn(fingerTemplateProperty, new wxStringProperty(N_TMPL_FLAGS, wxPG_LABEL, wxString::Format("%x", m_template.GetFingers().GetFlags())))->Enable(false);
				AppendIn(fingerTemplateProperty, new wxBoolProperty(N_TMPL_ISPALM, wxPG_LABEL, m_template.GetFingers().IsPalm()))->Enable(false);
				AppendIn(fingerTemplateProperty, new NCollectionProperty(N_TMPL_RECORDS, wxPG_LABEL, m_collectionString, m_template, FINGER_COLLECTION));
			}
			else
			{
				fingerTemplateProperty = Append(new wxStringProperty(N_TMPL_FINGERS, wxPG_LABEL, N_TMPL_NO_WCHARS));
			}

			if (!m_template.GetPalms().IsNull())
			{
				palmTemplateProperty = Append(new wxStringProperty(N_TMPL_PALMS, wxPG_LABEL, N_TMPL_NFTMPL));
				palmTemplateProperty->DeleteChildren();
				AppendIn(palmTemplateProperty, new wxStringProperty(N_TMPL_FLAGS, wxPG_LABEL, wxString::Format("%x", m_template.GetPalms().GetFlags())))->Enable(false);
				AppendIn(palmTemplateProperty, new wxBoolProperty(N_TMPL_ISPALM, wxPG_LABEL, m_template.GetPalms().IsPalm()))->Enable(false);
				AppendIn(palmTemplateProperty, new NCollectionProperty(N_TMPL_RECORDS, wxPG_LABEL, m_collectionString, m_template, PALM_COLLECTION));
			}
			else
			{
				palmTemplateProperty = Append(new wxStringProperty(N_TMPL_PALMS, wxPG_LABEL, N_TMPL_NO_WCHARS));
			}

			if (!m_template.GetFaces().IsNull())
			{
				faceTemplateProperty = Append(new wxStringProperty(N_TMPL_FACES, wxPG_LABEL, N_TMPL_NLTMPL));
				faceTemplateProperty->DeleteChildren();
				AppendIn(faceTemplateProperty, new wxStringProperty(N_TMPL_FLAGS, wxPG_LABEL, wxString::Format("%x", m_template.GetFaces().GetFlags())))->Enable(false);
				AppendIn(faceTemplateProperty, new NCollectionProperty(N_TMPL_RECORDS, wxPG_LABEL, m_collectionString, m_template, FACE_COLLECTION));
			}
			else
			{
				faceTemplateProperty = Append(new wxStringProperty(N_TMPL_FACES, wxPG_LABEL, N_TMPL_NO_WCHARS));
			}

			if (!m_template.GetIrises().IsNull())
			{
				irisTemplateProperty = Append(new wxStringProperty(N_TMPL_IRISES, wxPG_LABEL, N_TMPL_NETMPL));
				irisTemplateProperty->DeleteChildren();
				AppendIn(irisTemplateProperty, new wxStringProperty(N_TMPL_FLAGS, wxPG_LABEL, wxString::Format("%x", m_template.GetIrises().GetFlags())))->Enable(false);
				AppendIn(irisTemplateProperty, new NCollectionProperty(N_TMPL_RECORDS, wxPG_LABEL, m_collectionString, m_template, IRIS_COLLECTION));
			}
			else
			{
				irisTemplateProperty = Append(new wxStringProperty(N_TMPL_IRISES, wxPG_LABEL, N_TMPL_NO_WCHARS));
			}

			if (!m_template.GetVoices().IsNull())
			{
				voiceTemplateProperty = Append(new wxStringProperty(N_TMPL_VOICES, wxPG_LABEL, N_TMPL_NSTMPL));
				voiceTemplateProperty->DeleteChildren();
				AppendIn(voiceTemplateProperty, new wxStringProperty(N_TMPL_FLAGS, wxPG_LABEL, wxString::Format("%x", m_template.GetVoices().GetFlags())))->Enable(false);
				AppendIn(voiceTemplateProperty, new NCollectionProperty(N_TMPL_RECORDS, wxPG_LABEL, m_collectionString, m_template, VOICE_COLLECTION));
			}
			else
			{
				voiceTemplateProperty = Append(new wxStringProperty(N_TMPL_VOICES, wxPG_LABEL, N_TMPL_NO_WCHARS));
			}

			ExpandAll(false);
		}

		if (!nfTemplate.IsNull())
		{
			if (!nfTemplate.IsPalm())
			{
				Clear();
				fingerTemplateProperty = Append(new wxStringProperty(N_TMPL_FINGERS, wxPG_LABEL, nfTemplate.GetNativeType().GetName()));
				AppendIn(fingerTemplateProperty, new wxStringProperty(N_TMPL_FLAGS, wxPG_LABEL, wxString::Format("%x", nfTemplate.GetFlags())))->Enable(false);
				AppendIn(fingerTemplateProperty, new wxBoolProperty(N_TMPL_ISPALM, wxPG_LABEL, nfTemplate.IsPalm()))->Enable(false);
				AppendIn(fingerTemplateProperty, new NCollectionProperty(N_TMPL_RECORDS, wxPG_LABEL, m_collectionString, m_template, FINGER_COLLECTION));
			}
			else
			{
				Clear();
				palmTemplateProperty = Append(new wxStringProperty(N_TMPL_PALMS, wxPG_LABEL, nfTemplate.GetNativeType().GetName()));
				AppendIn(palmTemplateProperty, new wxStringProperty(N_TMPL_FLAGS, wxPG_LABEL, wxString::Format("%x", nfTemplate.GetFlags())))->Enable(false);
				AppendIn(palmTemplateProperty, new wxBoolProperty(N_TMPL_ISPALM, wxPG_LABEL, nfTemplate.IsPalm()))->Enable(false);
				AppendIn(palmTemplateProperty, new NCollectionProperty(N_TMPL_RECORDS, wxPG_LABEL, m_collectionString, m_template, PALM_COLLECTION));
			}
		}

		if (!nlTemplate.IsNull())
		{
			Clear();
			faceTemplateProperty = Append(new wxStringProperty(N_TMPL_FACES, wxPG_LABEL, nlTemplate.GetNativeType().GetName()));
			AppendIn(faceTemplateProperty, new wxStringProperty(N_TMPL_FLAGS, wxPG_LABEL, wxString::Format("%x", nlTemplate.GetFlags())))->Enable(false);
			AppendIn(faceTemplateProperty, new NCollectionProperty(N_TMPL_RECORDS, wxPG_LABEL, m_collectionString, m_template, FACE_COLLECTION));
		}
		if (!neTemplate.IsNull())
		{
			Clear();
			irisTemplateProperty = Append(new wxStringProperty(N_TMPL_IRISES, wxPG_LABEL, neTemplate.GetNativeType().GetName()));
			AppendIn(irisTemplateProperty, new wxStringProperty(N_TMPL_FLAGS, wxPG_LABEL, wxString::Format("%x", neTemplate.GetFlags())))->Enable(false);
			AppendIn(irisTemplateProperty, new NCollectionProperty(N_TMPL_RECORDS, wxPG_LABEL, m_collectionString, m_template, IRIS_COLLECTION));
		}
		if (!nsTemplate.IsNull())
		{
			Clear();
			voiceTemplateProperty = Append(new wxStringProperty(N_TMPL_VOICES, wxPG_LABEL, nsTemplate.GetNativeType().GetName()));
			AppendIn(voiceTemplateProperty, new wxStringProperty(N_TMPL_FLAGS, wxPG_LABEL, wxString::Format("%x", nsTemplate.GetFlags())))->Enable(false);
			AppendIn(voiceTemplateProperty, new NCollectionProperty(N_TMPL_RECORDS, wxPG_LABEL, m_collectionString, m_template, VOICE_COLLECTION));
		}

		if (!nfRecord.IsNull())
		{
			Clear();
			ShowFingerOrPalmPropertyValuesInGrid(nfRecord, currentItemId);
		}

		if (!nlRecord.IsNull())
		{
			Clear();
			Append(new wxIntProperty(N_TMPL_CBEFF_PRODUCT_TYPE, wxPG_LABEL, nlRecord.GetCbeffProductType()));
			Append(new wxIntProperty(N_TMPL_FLAGS, wxPG_LABEL, nlRecord.GetFlags()))->Enable(false);
			Append(new wxIntProperty(N_TMPL_QUALITY, wxPG_LABEL, nlRecord.GetQuality()));
		}
		if (!neRecord.IsNull())
		{
			wxPGChoices irisPositionChoices;

			NArrayWrapper<int> positionValues = NEnum::GetValues(NBiometricTypes::NEPositionNativeTypeOf());

			for (int i = 0; i < positionValues.GetCount(); i++)
			{
				irisPositionChoices.Add(NEnum::ToString(NBiometricTypes::NEPositionNativeTypeOf(), positionValues[i]), positionValues[i]);
			}

			Clear();
			Append(new wxIntProperty(N_TMPL_CBEFF_PRODUCT_TYPE, wxPG_LABEL, neRecord.GetCbeffProductType()));
			Append(new wxIntProperty(N_TMPL_FLAGS, wxPG_LABEL, neRecord.GetFlags()))->Enable(false);
			Append(new wxIntProperty(N_TMPL_HEIGHT, wxPG_LABEL, neRecord.GetHeight()))->Enable(false);
			Append(new wxEnumProperty(N_TMPL_POS, wxPG_LABEL, irisPositionChoices))->SetChoiceSelection(neRecord.GetPosition());
			Append(new wxIntProperty(N_TMPL_QUALITY, wxPG_LABEL, neRecord.GetQuality()));
			Append(new wxIntProperty(N_TMPL_WIDTH, wxPG_LABEL, neRecord.GetWidth()))->Enable(false);
		}
		if (!nsRecord.IsNull())
		{
			Clear();
			Append(new wxIntProperty(N_TMPL_CBEFF_PRODUCT_TYPE, wxPG_LABEL, nsRecord.GetCbeffProductType()));
			Append(new wxIntProperty(N_TMPL_FLAGS, wxPG_LABEL, nsRecord.GetFlags()))->Enable(false);
			Append(new wxBoolProperty(N_TMPL_HAS_TEXT_DEP_FEATURES, wxPG_LABEL, nsRecord.GetHasTextDependentFeatures() != 0));
			Append(new wxBoolProperty(N_TMPL_HAS_TEXT_INDEP_FEATURES, wxPG_LABEL, nsRecord.GetHasTextIndependentFeatures() != 0));
			Append(new wxIntProperty(N_TMPL_PHRASE_ID, wxPG_LABEL, nsRecord.GetPhraseId()));
			Append(new wxIntProperty(N_TMPL_QUALITY, wxPG_LABEL, nsRecord.GetQuality()));
			Append(new wxIntProperty(N_TMPL_SNR, wxPG_LABEL, nsRecord.GetSnr()));
		}
	}

	void TemplateSamplePropertyGrid::ShowFingerOrPalmPropertyValuesInGrid(NFRecord record, int itemId)
	{
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
			if (NBiometricTypes::IsImpressionTypeFinger((NFImpressionType)impressionsValues[i]))
				impressionTypeChoices.Add(NEnum::ToString(NBiometricTypes::NFImpressionTypeNativeTypeOf(), impressionsValues[i]), impressionsValues[i]);
			if (NBiometricTypes::IsImpressionTypePalm((NFImpressionType)impressionsValues[i]))
				impressionTypeChoices.Add(NEnum::ToString(NBiometricTypes::NFImpressionTypeNativeTypeOf(), impressionsValues[i]), impressionsValues[i]);
		}

		for (int i = 0; i < positionValues.GetCount(); i++)
		{
			if (NBiometricTypes::IsPositionFinger((NFPosition)positionValues[i]))
				positionChoices.Add(NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), positionValues[i]), positionValues[i]);
			if (NBiometricTypes::IsPositionPalm((NFPosition)positionValues[i]))
				positionChoices.Add(NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), positionValues[i]), positionValues[i]);
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

		Append(new wxIntProperty(N_TMPL_CBEFF_PRODUCT_TYPE, wxPG_LABEL, record.GetCbeffProductType()));
		Append(new NCollectionProperty(N_TMPL_CORES, wxPG_LABEL, m_collectionString, m_template,
			NBiometricTypes::IsPositionFinger(record.GetPosition()) ? FINGER_CORE_COLLECTION : PALM_CORE_COLLECTION, itemId));
		LimitPropertyEditing(N_TMPL_CORES, true);
		Append(new NCollectionProperty(N_TMPL_DELTAS, wxPG_LABEL, m_collectionString, m_template,
			NBiometricTypes::IsPositionFinger(record.GetPosition()) ? FINGER_DELTA_COLLECTION : PALM_DELTA_COLLECTION, itemId));
		LimitPropertyEditing(N_TMPL_DELTAS, true);
		Append(new NCollectionProperty(N_TMPL_DOUBLE_CORES, wxPG_LABEL, m_collectionString, m_template,
			NBiometricTypes::IsPositionFinger(record.GetPosition()) ? FINGER_DOUBLE_CORE_COLLECTION : PALM_DOUBLE_CORE_COLLECTION, itemId));
		LimitPropertyEditing(N_TMPL_DOUBLE_CORES, true);
		Append(new wxIntProperty(N_TMPL_FLAGS, wxPG_LABEL, record.GetFlags()))->Enable(false);
		Append(new wxIntProperty(N_TMPL_G, wxPG_LABEL, record.GetG()));
		Append(new wxIntProperty(N_TMPL_HEIGHT, wxPG_LABEL, record.GetHeight()))->Enable(false);
		Append(new wxIntProperty(N_TMPL_HORIZ_RESOLUTION, wxPG_LABEL, record.GetHorzResolution()))->Enable(false);
		Append(new wxEnumProperty(N_TMPL_IMPR_TYPE, wxPG_LABEL, impressionTypeChoices))->SetChoiceSelection(record.GetImpressionType());
		Append(new NCollectionProperty(N_TMPL_MINUTIAE, wxPG_LABEL, m_collectionString, m_template,
			NBiometricTypes::IsPositionFinger(record.GetPosition()) ? FINGER_MINUTIAE_COLLECTION : PALM_MINUTIAE_COLLECTION, itemId));
		LimitPropertyEditing(N_TMPL_MINUTIAE, true);
		Append(new NCollectionProperty(N_TMPL_MINUTIAE_NEIGHB, wxPG_LABEL, m_collectionString, m_template,
			NBiometricTypes::IsPositionFinger(record.GetPosition()) ? FINGER_MINUTIAE_NEIGHBOUR_COLLECTION : PALM_MINUTIAE_NEIGHBOUR_COLLECTION, itemId));
		LimitPropertyEditing(N_TMPL_MINUTIAE_NEIGHB, true);
		Append(new MultiChoiceProperty(N_TMPL_MINUTIAE_FORMAT, wxPG_LABEL, minutiaFormatChoices, minutiaFormatSelectedValues));
		Append(new wxEnumProperty(N_TMPL_PATTERN_CLS, wxPG_LABEL, patternClassChoices))->SetChoiceSelection(record.GetPatternClass());
		Append(new wxEnumProperty(N_TMPL_POS, wxPG_LABEL, positionChoices))->SetChoiceSelection(record.GetPosition());
		Append(new NCollectionProperty(N_TMPL_POSSIBLE_POS, wxPG_LABEL, m_collectionString, m_template,
			NBiometricTypes::IsPositionFinger(record.GetPosition()) ? FINGER_POSSIBLE_POSITION_COLLECTION : PALM_POSSIBLE_POSITION_COLLECTION, itemId));
		LimitPropertyEditing(N_TMPL_POSSIBLE_POS, true);
		Append(new wxIntProperty(N_TMPL_QUALITY, wxPG_LABEL, record.GetQuality()));
		Append(new wxBoolProperty(N_TMPL_REQ_UPDATE, wxPG_LABEL, record.GetRequiresUpdate() != 0));
		Append(new wxEnumProperty(N_TMPL_RIDGE_CNT_TYPE, wxPG_LABEL, ridgeCountsTypeChoices))->SetChoiceSelection(record.GetRidgeCountsType());
		Append(new wxIntProperty(N_TMPL_VERT_RESOLUTION, wxPG_LABEL, record.GetVertResolution()))->Enable(false);
		Append(new wxIntProperty(N_TMPL_WIDTH, wxPG_LABEL, record.GetWidth()))->Enable(false);
	}

	void TemplateSamplePropertyGrid::PropertyGridValueChange(Neurotec::NObject dataObject, int currentItemId, wxString propertyName, wxVariant propertyValue, bool isPalm)
	{
		NTemplate nTemplate = NULL;
		NFTemplate nfTemplate = NULL;
		NLTemplate nlTemplate = NULL;
		NETemplate neTemplate = NULL;
		NSTemplate nsTemplate = NULL;
		NFRecord nfRecord = NULL;
		NLRecord nlRecord = NULL;
		NERecord neRecord = NULL;
		NSRecord nsRecord = NULL;
		wxString type = dataObject.GetNativeType().GetName();

		if (type == N_TMPL_NTMPL) nTemplate = (NTemplate&)dataObject;
		else if (type == N_TMPL_NFTMPL)	nfTemplate = (NFTemplate&)dataObject;
		else if (type == N_TMPL_NLTMPL)	nlTemplate = (NLTemplate&)dataObject;
		else if (type == N_TMPL_NETMPL)	neTemplate = (NETemplate&)dataObject;
		else if (type == N_TMPL_NSTMPL)	nsTemplate = (NSTemplate&)dataObject;
		else if (type == N_TMPL_NFREC) nfRecord = (NFRecord&)dataObject;
		else if (type == N_TMPL_NLREC) nlRecord = (NLRecord&)dataObject;
		else if (type == N_TMPL_NEREC) neRecord = (NERecord&)dataObject;
		else if (type == N_TMPL_NSREC) nsRecord = (NSRecord&)dataObject;

		if (currentItemId >= 0)
		{
			if (!nfRecord.IsNull())
			{
				if(isPalm)
				{
					PalmRecordPropertyValueChanged(propertyName, currentItemId, propertyValue);
				}
				else
				{
					FingerRecordPropertyValueChanged(propertyName, currentItemId, propertyValue);
				}
			}

			if (!nlRecord.IsNull())
				FaceRecordPropertyValueChanged(propertyName, currentItemId, propertyValue);

			if (!neRecord.IsNull())
				IrisRecordPropertyValueChanged(propertyName, currentItemId, propertyValue);

			if (!nsRecord.IsNull())
				VoiceRecordPropertyValueChanged(propertyName, currentItemId, propertyValue);
		}
	}

	void TemplateSamplePropertyGrid::FingerRecordPropertyValueChanged(wxString propertyName, int index, wxVariant value)
	{
		NFRecord fingerRecord = m_template.GetFingers().GetRecords().Get(index);

		try
		{
			if (propertyName == N_TMPL_CBEFF_PRODUCT_TYPE)
				fingerRecord.SetCbeffProductType(value.GetInteger());
			else if (propertyName == N_TMPL_G)
				fingerRecord.SetG(value.GetInteger());
			else if (propertyName == N_TMPL_IMPR_TYPE)
				fingerRecord.SetImpressionType((NFImpressionType)value.GetInteger());
			else if (propertyName == N_TMPL_MINUTIAE_FORMAT)
			{
				wxStringTokenizer tokenizer(value.GetString(), ";");
				wxString minutiaFormatString;
				int count = 0;

				while (tokenizer.HasMoreTokens())
				{
					minutiaFormatString = tokenizer.GetNextToken();
					count += NEnum::Parse(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), minutiaFormatString);
				}

				fingerRecord.SetMinutiaFormat((NFMinutiaFormat)count);
			}
			else if (propertyName == N_TMPL_PATTERN_CLS)
				fingerRecord.SetPatternClass((NFPatternClass)value.GetInteger());
			else if (propertyName == N_TMPL_POS)
				fingerRecord.SetPosition((NFPosition)value.GetInteger());
			else if (propertyName == N_TMPL_QUALITY)
				fingerRecord.SetQuality(value.GetInteger());
			else if (propertyName == N_TMPL_REQ_UPDATE)
				fingerRecord.SetRequiresUpdate(value.GetBool());
			else if (propertyName == N_TMPL_RIDGE_CNT_TYPE)
				fingerRecord.SetRidgeCountsType((NFRidgeCountsType)value.GetInteger());

			m_template.GetFingers().GetRecords().Set(index, fingerRecord);
		}
		catch (NError& ex)
		{
			wxExceptionDlg::Show(ex);
		}
		
		UpdatePropertyGrid(fingerRecord, index);
	}

	void TemplateSamplePropertyGrid::PalmRecordPropertyValueChanged(wxString propertyName, int index, wxVariant value)
	{
		NFRecord palmRecord = m_template.GetPalms().GetRecords().Get(index);

		try
		{
			if (propertyName == N_TMPL_CBEFF_PRODUCT_TYPE)
				palmRecord.SetCbeffProductType(value.GetInteger());
			else if (propertyName == N_TMPL_G)
				palmRecord.SetG(value.GetInteger());
			else if (propertyName == N_TMPL_IMPR_TYPE)
				palmRecord.SetImpressionType((NFImpressionType)value.GetInteger());
			else if (propertyName == N_TMPL_MINUTIAE_FORMAT)
			{
				wxStringTokenizer tokenizer(value.GetString(), ";");
				wxString minutiaFormatString;
				int count = 0;

				while (tokenizer.HasMoreTokens())
				{
					minutiaFormatString = tokenizer.GetNextToken();
					count += NEnum::Parse(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), minutiaFormatString);
				}

				palmRecord.SetMinutiaFormat((NFMinutiaFormat)count);
			}
			else if (propertyName == N_TMPL_PATTERN_CLS)
				palmRecord.SetPatternClass((NFPatternClass)value.GetInteger());
			else if (propertyName == N_TMPL_POS)
				palmRecord.SetPosition((NFPosition)value.GetInteger());
			else if (propertyName == N_TMPL_QUALITY)
				palmRecord.SetQuality(value.GetInteger());
			else if (propertyName == N_TMPL_REQ_UPDATE)
				palmRecord.SetRequiresUpdate(value.GetBool());
			else if (propertyName == N_TMPL_RIDGE_CNT_TYPE)
				palmRecord.SetRidgeCountsType((NFRidgeCountsType)value.GetInteger());

			m_template.GetPalms().GetRecords().Set(index, palmRecord);
		}
		catch (NError& ex)
		{
			wxExceptionDlg::Show(ex);
		}

		UpdatePropertyGrid(palmRecord, index);
	}

	void TemplateSamplePropertyGrid::FaceRecordPropertyValueChanged(wxString propertyName, int index, wxVariant value)
	{
		NLRecord faceRecord = m_template.GetFaces().GetRecords().Get(index);

		try
		{
			if (propertyName == N_TMPL_CBEFF_PRODUCT_TYPE)
				faceRecord.SetCbeffProductType(value.GetInteger());
			else if (propertyName == N_TMPL_FLAGS)
				faceRecord.SetFlags(value.GetInteger());
			else if (propertyName == N_TMPL_QUALITY)
				faceRecord.SetQuality(value.GetInteger());

			m_template.GetFaces().GetRecords().Set(index, faceRecord);
		}
		catch (NError& ex)
		{
			wxExceptionDlg::Show(ex);
		}

		UpdatePropertyGrid(faceRecord, index);
	}

	void TemplateSamplePropertyGrid::IrisRecordPropertyValueChanged(wxString propertyName, int index, wxVariant value)
	{
		NERecord irisRecord = m_template.GetIrises().GetRecords().Get(index);

		try
		{
			if (propertyName == N_TMPL_CBEFF_PRODUCT_TYPE)
				irisRecord.SetCbeffProductType(value.GetInteger());
			else if (propertyName == N_TMPL_POS)
				irisRecord.SetPosition((NEPosition)value.GetInteger());
			else if (propertyName == N_TMPL_QUALITY)
				irisRecord.SetQuality(value.GetInteger());

			m_template.GetIrises().GetRecords().Set(index, irisRecord);
		}
		catch (NError& ex)
		{
			wxExceptionDlg::Show(ex);
		}

		UpdatePropertyGrid(irisRecord, index);
	}

	void TemplateSamplePropertyGrid::VoiceRecordPropertyValueChanged(wxString propertyName, int index, wxVariant value)
	{
		NSRecord voiceRecord = m_template.GetVoices().GetRecords().Get(index);

		try
		{
			if (propertyName == N_TMPL_CBEFF_PRODUCT_TYPE)
				voiceRecord.SetCbeffProductType(value.GetInteger());
			else if (propertyName == N_TMPL_HAS_TEXT_DEP_FEATURES)
				voiceRecord.SetHasTextDependentFeatures(value.GetBool());
			else if (propertyName == N_TMPL_HAS_TEXT_INDEP_FEATURES)
				voiceRecord.SetHasTextIndependentFeatures(value.GetInteger());
			else if (propertyName == N_TMPL_PHRASE_ID)
				voiceRecord.SetPhraseId(value.GetInteger());
			else if (propertyName == N_TMPL_QUALITY)
				voiceRecord.SetQuality(value.GetInteger());
			else if (propertyName == N_TMPL_SNR)
				voiceRecord.SetSnr(value.GetInteger());

			m_template.GetVoices().GetRecords().Set(index, voiceRecord);
		}
		catch (NError& ex)
		{
			wxExceptionDlg::Show(ex);
		}

		UpdatePropertyGrid(voiceRecord, index);
	}

	void TemplateSamplePropertyGrid::PropertyGridValueChanging(wxPropertyGridEvent& event)
	{
		wxString propertyName = event.GetProperty()->GetName();
		wxVariant pendingValue = event.GetValue();

		if (propertyName == N_TMPL_G || propertyName == N_TMPL_QUALITY || propertyName == N_TMPL_SNR) ValidatePropertyGridValues(pendingValue, 255, event);
		else if (propertyName == N_TMPL_CBEFF_PRODUCT_TYPE) ValidatePropertyGridValues(pendingValue, N_UINT16_MAX, event);
		else if (propertyName == N_TMPL_RIDGE_CNT_TYPE) ValidatePropertyGridValues(pendingValue, 6, event);
		else if (propertyName == N_TMPL_MINUTIAE_FORMAT) ValidatePropertyGridStrings(pendingValue, event);
	}

	void TemplateSamplePropertyGrid::ValidatePropertyGridStrings(wxVariant pendingValue, wxPropertyGridEvent& event)
	{
		wxStringTokenizer tokenizer(pendingValue.GetString(), ";");
		wxString minutiaFormatString;
		int count = 0;

		while (tokenizer.HasMoreTokens())
		{
			minutiaFormatString = tokenizer.GetNextToken();
			count += NEnum::Parse(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), minutiaFormatString);
		}

		NEnum::ToString(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), count);

		if (count == 0 && minutiaFormatString != (wxString)NEnum::ToString(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), nfmfNone))
		{
			event.Veto();
			event.SetValidationFailureBehavior(wxPG_VFB_STAY_IN_PROPERTY |
				wxPG_VFB_BEEP |
				wxPG_VFB_SHOW_MESSAGEBOX);
			return;
		}
	}

	void TemplateSamplePropertyGrid::ValidatePropertyGridValues(wxVariant pendingValue, long maxValue, wxPropertyGridEvent& event)
	{
		if (!pendingValue.IsNull())
		{
			long longPendingValue;
			if (!pendingValue.GetString().ToLong(&longPendingValue))
			{
				event.Veto();
				event.SetValidationFailureBehavior(wxPG_VFB_STAY_IN_PROPERTY |
					wxPG_VFB_BEEP |
					wxPG_VFB_SHOW_MESSAGEBOX);
				return;
			}
			else if (longPendingValue < 0 || longPendingValue > maxValue)
			{
				event.Veto();
				event.SetValidationFailureBehavior(wxPG_VFB_STAY_IN_PROPERTY |
					wxPG_VFB_BEEP |
					wxPG_VFB_SHOW_MESSAGEBOX);
				return;
			}
		}
	}

	void TemplateSamplePropertyGrid::SetTemplate(NTemplate nTemplate)
	{
		m_template = nTemplate;
	}
}}}

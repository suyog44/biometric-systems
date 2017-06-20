#include "Precompiled.h"

#include <Common/OpenSubjectDialog.h>

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Standards;

namespace Neurotec { namespace Samples
{

OpenSubjectDialog::ListItem OpenSubjectDialog::m_owners[] =
	{
		{ CBEFF_BO_NOT_FOR_USE, wxT("Auto detect") },
		{ CBEFF_BO_NEUROTECHNOLOGIJA, wxT("Neurotechnologija") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, wxT("IncitsTCM1Biometrics") },
		{ CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, wxT("IsoIecJtc1SC37Biometrics") },
	};

OpenSubjectDialog::FormatItem OpenSubjectDialog::m_formats[] =
	{
		//CBEFF_BO_NEUROTECHNOLOGIJA
		{ CBEFF_BO_NEUROTECHNOLOGIJA, CBEFF_BDBFI_NEUROTECHNOLOGIJA_NFRECORD_1, wxT("NFRecord1") },
		{ CBEFF_BO_NEUROTECHNOLOGIJA, CBEFF_BDBFI_NEUROTECHNOLOGIJA_NFRECORD_2, wxT("NFRecord2") },
		{ CBEFF_BO_NEUROTECHNOLOGIJA, CBEFF_BDBFI_NEUROTECHNOLOGIJA_NFRECORD_3, wxT("NFRecord3") },
		{ CBEFF_BO_NEUROTECHNOLOGIJA, CBEFF_BDBFI_NEUROTECHNOLOGIJA_NLRECORD_1, wxT("NLRecord1") },
		{ CBEFF_BO_NEUROTECHNOLOGIJA, CBEFF_BDBFI_NEUROTECHNOLOGIJA_NLRECORD_2, wxT("NFRecord2") },
		{ CBEFF_BO_NEUROTECHNOLOGIJA, CBEFF_BDBFI_NEUROTECHNOLOGIJA_NLRECORD_3, wxT("NFRecord3") },
		{ CBEFF_BO_NEUROTECHNOLOGIJA, CBEFF_BDBFI_NEUROTECHNOLOGIJA_NERECORD_1, wxT("NERecord1") },
		{ CBEFF_BO_NEUROTECHNOLOGIJA, CBEFF_BDBFI_NEUROTECHNOLOGIJA_NSRECORD_1, wxT("NSRecord1") },
		{ CBEFF_BO_NEUROTECHNOLOGIJA, CBEFF_BDBFI_NEUROTECHNOLOGIJA_NFTEMPLATE, wxT("NFTemplate") },
		{ CBEFF_BO_NEUROTECHNOLOGIJA, CBEFF_BDBFI_NEUROTECHNOLOGIJA_NLTEMPLATE, wxT("NLTemplate") },
		{ CBEFF_BO_NEUROTECHNOLOGIJA, CBEFF_BDBFI_NEUROTECHNOLOGIJA_NETEMPLATE, wxT("NETemplate") },
		{ CBEFF_BO_NEUROTECHNOLOGIJA, CBEFF_BDBFI_NEUROTECHNOLOGIJA_NSTEMPLATE, wxT("NSTemplate") },
		{ CBEFF_BO_NEUROTECHNOLOGIJA, CBEFF_BDBFI_NEUROTECHNOLOGIJA_NTEMPLATE, wxT("NTemplate") },

		//CBEFF_BO_INCITS_TC_M1_BIOMETRICS
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_FINGER_MINUTIAE_N, wxT("FingerMinutiaeN") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_FINGER_MINUTIAE_X, wxT("FingerMinutiaeX") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_FINGER_MINUTIAE_U, wxT("FingerMinutiaeU") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_FINGER_PATTERN_N, wxT("FingerPaternN") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_FINGER_IMAGE, wxT("FingerImage") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_FACE_IMAGE, wxT("FaceImage") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_IRIS_RECTILINEAR, wxT("IrisRectilinear") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_IRIS_POLAR, wxT("IrisPolar") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_SIGNATURE_SIGN_RAW_DATA_N, wxT("SignatureSignRawDataN") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_SIGNATURE_SIGN_RAW_DATA_X, wxT("SignatureSignRawDataX") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_SIGNATURE_SIGN_COMMON_FEATURE_DATA_N, wxT("SignatureSignCommonFeatureDataN") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_SIGNATURE_SIGN_COMMON_FEATURE_DATA_X, wxT("SignatureSignCommonFeatureDataX") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_SIGNATURE_SIGN_RAW_AND_COMMON_FEATURE_DATA_N, wxT("SignatureSignRawAndCommonFeatureDataN") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_SIGNATURE_SIGN_RAW_AND_COMMON_FEATURE_DATA_X, wxT("SignatureSignRawAndCommonFeatureDataX") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_HAND_GEOMETRY_N, wxT("HandGeometryN") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_HAND_GEOMETRY_X, wxT("HandGeometryX") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_BIOMETRIC_FUSION_DATA, wxT("BiometricFusionData") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_WSQ_IMAGE, wxT("WsqImage") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_BMP_IMAGE, wxT("BmpImage") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_JPEG_IMAGE, wxT("JpegImage") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_JPEG2000_IMAGE, wxT("Jpeg2000Image") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_TIFF_IMAGE, wxT("TiffImage") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_GIF89A_IMAGE, wxT("Gif89aImage") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_PNG_IMAGE, wxT("PngImage") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_WAV_AUDIO, wxT("WavAudio") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_MPEG_MEDIA, wxT("MpegMedia") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_MPEG1_PART_3_MEDIA, wxT("Mpeg1Part3Media") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_AVI_MEDIA, wxT("AviMedia") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_VRML_3D_OBJECT_DATA, wxT("Vrml3DObjectData") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_NIST_ITL_1_2000_TYPE_4_RECORD, wxT("NistItl1_2000Type4Record") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_NIST_ITL_1_2000_TYPE_10_RECORD, wxT("NistItl1_2000Type10Record") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_NIST_ITL_1_2000_TYPE_13_RECORD, wxT("NistItl1_2000Type13Record") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_NIST_ITL_1_2000_TYPE_14_RECORD, wxT("NistItl1_2000Type14Record") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_NIST_ITL_1_2000_TYPE_15_RECORD, wxT("NistItl1_2000Type15Record") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_NIST_ITL_1_2000_TYPE_16_RECORD, wxT("NistItl1_2000Type16Record") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_NIST_ITL_1_200X_RECORD_COLLECTION_FOR_TENPRINT_CAPTURE, wxT("NistItl1_200XRecordCollectionForTenPrintCapture") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_GENERIC_FBI_EFTS_RECORD, wxT("GenericFbiEftsRecord") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_NIEM, wxT("Niem") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_NIST_ITL1_2007_TYPE_10_RECORD, wxT("NistItl1_2007Type10Record") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_NIST_ITL1_2007_TYPE_14_RECORD, wxT("NistItl1_2007Type14Record") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_NIST_ITL1_2007_TYPE_17_RECORD, wxT("NistItl1_2007Type17Record") },
		{ CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_EBTS, wxT("Ebts") },

		//CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_MINUTIAE_RECORD_N, wxT("FingerMinutiaeRecordN")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_MINUTIAE_RECORD_X, wxT("FingerMinutiaeRecordX")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_MINUTIAE_CARD_NORMAL_V, wxT("FingerMinutiaeCardNormalV")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_MINUTIAE_CARD_NORMAL_N, wxT("FingerMinutiaeCardNormalN")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_MINUTIAE_CARD_COMPACT_V, wxT("FingerMinutiaeCardCompactV")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_MINUTIAE_CARD_COMPACT_N, wxT("FingerMinutiaeCardCompactN")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_IMAGE, wxT("FingerImage")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FACE_IMAGE, wxT("FaceImage")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_IRIS_IMAGE_RECTILINEAR, wxT("IrisImageRectilinear")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_PATTERN_SPECTRAL_QUANTIZED_CO_SINUSOIDAL_TRIPLET, wxT("FingerPatternSpectralQuantizedCoSinusoidalTriplet")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_IRIS_IMAGE_POLAR, wxT("IrisImagePolar")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_PATTERN_SPECTRAL_DISCRETE_FOURIER_TRANSFORM, wxT("FingerPatternSpectralDiscreteFourierTransform")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_PATTERN_SPECTRAL_GABOR_FILTER, wxT("FingerPatternSpectralGaborFilter")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_SIGNATURE_SIGN_TIME_SERIES_FULL, wxT("SignatureSignTimeSeriesFull")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_SIGNATURE_SIGN_TIME_SERIES_COMPACT, wxT("SignatureSignTimeSeriesCompact")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_PATTERN_SKELETAL_DATA_RECORD, wxT("FingerPatternSkeletalDataRecord")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_PATTERN_SKELETAL_DATA_CARD_NORMAL, wxT("FingerPatternSkeletalDataCardNormal")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_PATTERN_SKELETAL_DATA_CARD_COMPACT, wxT("FingerPatternSkeletalDataCardCompact")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_VASCULAR_IMAGE_DATA, wxT("VascularImageData")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_HAND_GEOMETRY_SILHOUETTE, wxT("HandGeometrySilhouette")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_MINUTIAE_CARD_COMPACT_V_H, wxT("FingerMinutiaeCardCompactVH")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_MINUTIAE_CARD_COMPACT_R_H, wxT("FingerMinutiaeCardCompactRH")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_MINUTIAE_CARD_NORMAL_V_NH, wxT("FingerMinutiaeCardNormalVNH")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_MINUTIAE_CARD_NORMAL_R_NH, wxT("FingerMinutiaeCardNormalRNH")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_MINUTIAE_RECORD_FORMAT, wxT("FingerMinutiaeRecordFormat")},
		{CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_SIGNATURE_SIGN_TIME_SERIES_COMPRESSION, wxT("SignatureSignTimeSeriesCompression")}
	};

OpenSubjectDialog::OpenSubjectDialog(wxWindow *parent, wxWindowID id, wxString title) : wxDialog(parent, id, title)
{
	m_filePath = wxEmptyString;

	CreateGuiElements();
	RegisterGuiEvents();

	ListOwners();
}

OpenSubjectDialog::~OpenSubjectDialog()
{
	UnregisterGuiEvents();
}

void OpenSubjectDialog::OnOkClick(wxCommandEvent&)
{
	EndModal(wxID_OK);
}

void OpenSubjectDialog::OnCancelClick(wxCommandEvent&)
{
	EndModal(wxID_CANCEL);
}

void OpenSubjectDialog::OnOpenFileClick(wxCommandEvent&)
{
	wxFileDialog openFileDialog(this, wxT("Select subject template file"), wxEmptyString, wxEmptyString,
		wxEmptyString, wxFD_OPEN | wxFD_FILE_MUST_EXIST);

	openFileDialog.ShowModal();

	m_filePath = openFileDialog.GetPath();

	m_txtFileName->SetValue(m_filePath);
	m_txtFileName->SetToolTip(m_filePath);

	m_btnOk->Enable(m_filePath != wxEmptyString);
}

void OpenSubjectDialog::OnOwnerSelect(wxCommandEvent&)
{
	ListTypes();
}

wxString OpenSubjectDialog::GetFilePath()
{
	return m_filePath;
}

NUShort OpenSubjectDialog::GetFormatOwner()
{
	NUShort result = 0;

	int selection = m_choiceFormatOwner->GetSelection();

	if (selection == -1)
	{
		return result;
	}

	ListItem *listItem = (ListItem *)m_choiceFormatOwner->GetClientData(selection);

	return listItem->value;
}

NUShort OpenSubjectDialog::GetFormatType()
{
	NUShort result = 0;

	int selection = m_choiceFormatType->GetSelection();

	if (selection == -1)
	{
		return result;
	}

	FormatItem *formatType = (FormatItem *)m_choiceFormatType->GetClientData(selection);

	return formatType->value;
}

void OpenSubjectDialog::ListOwners()
{
	m_choiceFormatOwner->Clear();

	int countOwners = sizeof(m_owners) / sizeof(m_owners[0]);

	for (int i = 0; i < countOwners; i++)
	{
		m_choiceFormatOwner->Append(m_owners[i].name, &m_owners[i]);
	}

	m_choiceFormatOwner->SetSelection(0);
	ListTypes();
}

void OpenSubjectDialog::ListTypes()
{
	m_choiceFormatType->Clear();

	int selection = m_choiceFormatOwner->GetSelection();

	if (selection == -1)
	{
		return;
	}

	ListItem *listItem = (ListItem *)m_choiceFormatOwner->GetClientData(selection);

	int formatCount = sizeof(m_formats) / sizeof(m_formats[0]);

	for (int i = 0; i < formatCount; i++)
	{
		if (m_formats[i].owner == listItem-> value)
		{
			m_choiceFormatType->Append(m_formats[i].name, &m_formats[i]);
		}
	}

	if (m_choiceFormatType->GetCount() > 0)
	{
		m_choiceFormatType->Select(0);
	}
}

void OpenSubjectDialog::RegisterGuiEvents()
{
	m_btnOk->Connect(wxEVT_BUTTON, wxCommandEventHandler(OpenSubjectDialog::OnOkClick), NULL, this);
	m_btnCancel->Connect(wxEVT_BUTTON, wxCommandEventHandler(OpenSubjectDialog::OnCancelClick), NULL, this);
	m_btnOpenFile->Connect(wxEVT_BUTTON, wxCommandEventHandler(OpenSubjectDialog::OnOpenFileClick), NULL, this);
	m_choiceFormatOwner->Connect(wxEVT_COMMAND_CHOICE_SELECTED, wxCommandEventHandler(OpenSubjectDialog::OnOwnerSelect), NULL, this);
}

void OpenSubjectDialog::UnregisterGuiEvents()
{
	m_btnOk->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(OpenSubjectDialog::OnOkClick), NULL, this);
	m_btnCancel->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(OpenSubjectDialog::OnCancelClick), NULL, this);
	m_btnOpenFile->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(OpenSubjectDialog::OnOpenFileClick), NULL, this);
	m_choiceFormatOwner->Disconnect(wxEVT_COMMAND_CHOICE_SELECTED, wxCommandEventHandler(OpenSubjectDialog::OnOwnerSelect), NULL, this);
}

void OpenSubjectDialog::CreateGuiElements()
{
	wxBoxSizer *mainSizer = new wxBoxSizer(wxVERTICAL);
	this->SetSizer(mainSizer);

	wxGridBagSizer *sizer = new wxGridBagSizer(5, 5);
	mainSizer->Add(sizer, 1, wxALL | wxEXPAND, 5);

	wxStaticText *text = new wxStaticText(this, wxID_ANY, wxT("File name:"));
	sizer->Add(text, wxGBPosition(0, 0), wxGBSpan(1, 1), wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT);

	m_txtFileName = new wxTextCtrl(this, wxID_ANY);
	m_txtFileName->SetMinSize(wxSize(250, -1));
	m_txtFileName->Disable();
	sizer->Add(m_txtFileName, wxGBPosition(0, 1), wxGBSpan(1, 2), wxALL | wxEXPAND);

	m_btnOpenFile = new wxButton(this, wxID_ANY, wxT("Select"));
	sizer->Add(m_btnOpenFile, wxGBPosition(0, 3), wxGBSpan(1, 1));

	text = new wxStaticText(this, wxID_ANY, wxT("Format owner:"));
	sizer->Add(text, wxGBPosition(1, 0), wxGBSpan(1, 1), wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT);

	m_choiceFormatOwner = new wxChoice(this, wxID_ANY);
	sizer->Add(m_choiceFormatOwner, wxGBPosition(1, 1), wxGBSpan(1, 3), wxALL | wxEXPAND);

	text = new wxStaticText(this, wxID_ANY, wxT("Format type:"));
	sizer->Add(text, wxGBPosition(2, 0), wxGBSpan(1, 1), wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT);

	m_choiceFormatType = new wxChoice(this, wxID_ANY);
	sizer->Add(m_choiceFormatType, wxGBPosition(2, 1), wxGBSpan(1, 3), wxALL | wxEXPAND);

	m_btnOk = new wxButton(this, wxID_ANY, wxT("OK"));
	m_btnOk->Disable();
	sizer->Add(m_btnOk, wxGBPosition(3, 2), wxGBSpan(1, 1), wxALL | wxALIGN_RIGHT);

	m_btnCancel = new wxButton(this, wxID_ANY, wxT("Cancel"));
	sizer->Add(m_btnCancel, wxGBPosition(3, 3), wxGBSpan(1, 1), wxALL);

	this->Layout();
	this->Fit();
	this->Center();
}

}}


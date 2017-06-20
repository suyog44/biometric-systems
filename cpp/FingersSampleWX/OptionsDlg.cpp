#include "Precompiled.h"
#include "OptionsDlg.h"

using namespace Neurotec::Biometrics;

namespace Neurotec { namespace Samples
{

// Enrollment
#define NPS_FINGERS_MINIMAL_MINUTIA_COUNT wxT("Fingers.MinimalMinutiaCount")
#define NPS_FINGERS_TEMPLATE_SIZE wxT("Fingers.TemplateSize")
#define NPS_FINGERS_QUALITY_THREASHOLD wxT("Fingers.QualityThreshold")
#define NPS_FINGERS_FAST_EXTRACTION wxT("Fingers.FastExtraction")
#define NPS_FINGERS_RETURN_BINARIZED_IMAGE wxT("Fingers.ReturnBinarizedImage")
#define NPS_FINGERS_EXTRACTED_RIDGE_COUNTS wxT("Fingers.ExtractedRidgeCounts")

// Matching
#define NPS_MATCHING_THRESHOLD wxT("Matching.Threshold")
#define NPS_FINGERS_MAXIMAL_ROTATION wxT("Fingers.MaximalRotation")
#define NPS_FINGERS_MATCHING_SPEED wxT("Fingers.MatchingSpeed")
#define NPS_MATCHING_MAXIMAL_RESULT_COUNT wxT("Matching.MaximalResultCount")
#define NPS_MATCHING_FIRST_RESULT_ONLY wxT("Matching.FirstResultOnly")

const double FARLogRatio = 12;

struct EnumOptionEntry
{
	const wxChar *text;
	int value;
};

const EnumOptionEntry enumNTemplateSize[] =
{
	{ wxT("Compact"), ntsCompact },
	{ wxT("Small"), ntsSmall },
	{ wxT("Medium"), ntsMedium },
	{ wxT("Large"), ntsLarge },
	{ NULL, 0 }
};

const EnumOptionEntry enumNFRidgeCountsType[] =
{
	{ wxT("None"), nfrctNone },
	{ wxT("Four neighbors"), nfrctFourNeighbors },
	{ wxT("Eight neighbors"), nfrctEightNeighbors },
	{ wxT("Four neighbors with indexes"), nfrctFourNeighborsWithIndexes },
	{ wxT("Eight neighbors with indexes"), nfrctEightNeighborsWithIndexes },
	{ NULL, 0 }
};

const EnumOptionEntry enumNMatchingSpeed[] =
{
	{ wxT("Low"), nmsLow },
	{ wxT("Medium"), nmsMedium },
	{ wxT("High"), nmsHigh },
	{ NULL, 0 }
};

BEGIN_EVENT_TABLE(OptionsDlg,wxDialog)
	EVT_CLOSE(OptionsDlg::OnClose)
	EVT_BUTTON(wxID_RESET, OptionsDlg::OnReset)
	EVT_BUTTON(wxID_OK, OptionsDlg::OnOK)
END_EVENT_TABLE()

OptionsDlg::OptionsDlg(wxWindow *parent, ::Neurotec::Biometrics::Client::NBiometricClient biometricClient, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize &size, long style)
: wxDialog(parent, id, title, position, size, style), m_biometricClient(biometricClient)
{
	CreateGUIControls();

	if (biometricClient.GetHandle())
	{
		m_biometricClient.CaptureProperties(m_properties);
		UpdateGui();
	}
}

OptionsDlg::~OptionsDlg()
{
}

void OptionsDlg::CreateGUIControls()
{
	this->SetSizeHints(450, 480);

	wxBoxSizer* bSizer1;
	bSizer1 = new wxBoxSizer(wxVERTICAL);

	wxNotebook *notebook = new wxNotebook(this, wxID_ANY);
	notebook->AddPage(CreateEnrollPage(notebook), wxT("Enrollment"));
	notebook->AddPage(CreateMatchingPage(notebook), wxT("Matching"));
	notebook->Layout();
	bSizer1->Add(notebook, 1, wxEXPAND, 5);

	m_sdbSizer1 = new wxStdDialogButtonSizer();
	m_sdbSizer1OK = new wxButton(this, wxID_OK);
	m_sdbSizer1->AddButton(m_sdbSizer1OK);
	m_sdbSizer1Cancel = new wxButton(this, wxID_CANCEL);
	m_sdbSizer1->AddButton(m_sdbSizer1Cancel);
	wxButton *btnReset = new wxButton(this, wxID_RESET, wxT("Reset"));
	m_sdbSizer1->Add(btnReset);
	m_sdbSizer1->Realize();

	bSizer1->Add(m_sdbSizer1, 0, wxEXPAND, 5);

	this->SetSizer(bSizer1);
	this->Layout();
	Center();
}

wxWindow* OptionsDlg::CreateEnrollPage(wxWindow *parent)
{
	wxScrolledWindow* tabPage = new wxScrolledWindow(parent, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxHSCROLL | wxVSCROLL);
	wxBoxSizer *boxSizer;
	boxSizer = new wxBoxSizer(wxVERTICAL);
	tabPage->SetSizer(boxSizer);

	wxStaticBoxSizer *staticBoxSizer;
	wxGridSizer *sizer;
	wxStaticText *staticText;

	// general
	staticBoxSizer = new wxStaticBoxSizer(new wxStaticBox(tabPage, wxID_ANY, wxEmptyString), wxVERTICAL);
	boxSizer->Add(staticBoxSizer, 0, wxEXPAND, 5);
	sizer = new wxFlexGridSizer(0, 2, 0, 0);
	staticBoxSizer->Add(sizer, 0, wxEXPAND, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Minimum minutia count:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);
	m_spinMinMinutiaCount = new wxSpinCtrl(tabPage);
	m_spinMinMinutiaCount->SetRange(1, NFR_MAX_FINGER_MINUTIA_COUNT);
	sizer->Add(m_spinMinMinutiaCount, 0, wxALL, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Quality threshold:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);
	m_sliderQualityThreshold = new wxSlider(tabPage, wxID_ANY, 0, 0, 100,
		wxDefaultPosition, wxDefaultSize, wxHORIZONTAL | wxSL_LABELS);
	sizer->Add(m_sliderQualityThreshold, 0, wxALL | wxEXPAND, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Template size:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT | wxALL, 5);
	m_comboTemplateSize = new wxComboBox(tabPage, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize,
		GetEnumStrings(enumNTemplateSize), wxCB_DROPDOWN | wxCB_READONLY);
	sizer->Add(m_comboTemplateSize, 0, wxALL | wxEXPAND, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Extracted ridge counts:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT | wxALL, 5);
	m_comboExtractedRidgeCounts = new wxComboBox(tabPage, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize,
		GetEnumStrings(enumNFRidgeCountsType), wxCB_DROPDOWN | wxCB_READONLY);
	sizer->Add(m_comboExtractedRidgeCounts, 0, wxALL | wxEXPAND, 5);

	sizer->AddSpacer(1);
	m_cbReturnBinarizedImage = new wxCheckBox(tabPage, wxID_ANY, wxT("Return binarized image"));
	sizer->Add(m_cbReturnBinarizedImage, 0, wxALL | wxEXPAND, 5);

	sizer->AddSpacer(1);
	m_cbFastExtraction = new wxCheckBox(tabPage, wxID_ANY, wxT("Fast Extraction"));
	sizer->Add(m_cbFastExtraction, 0, wxALL | wxEXPAND, 5);

	tabPage->SetScrollRate(5, 5);
	tabPage->Layout();

	return tabPage;
}

wxWindow* OptionsDlg::CreateMatchingPage(wxWindow *parent)
{
	wxScrolledWindow* tabPage = new wxScrolledWindow(parent, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxHSCROLL | wxVSCROLL);
	wxBoxSizer *boxSizer;
	boxSizer = new wxBoxSizer(wxVERTICAL);
	tabPage->SetSizer(boxSizer);

	wxStaticBoxSizer *staticBoxSizer;
	wxGridSizer *sizer;
	wxStaticText *staticText;

	staticBoxSizer = new wxStaticBoxSizer(new wxStaticBox(tabPage, wxID_ANY, wxEmptyString), wxVERTICAL);
	boxSizer->Add(staticBoxSizer, 0, wxEXPAND, 5);
	sizer = new wxFlexGridSizer(0, 2, 0, 0);
	staticBoxSizer->Add(sizer, 0, wxEXPAND, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("FAR:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT | wxALL, 5);
	wxArrayString farValues;
	farValues.Add(wxT("0.1%"));
	farValues.Add(wxT("0.01%"));
	farValues.Add(wxT("0.001%"));
	farValues.Add(wxT("0.0001%"));
	m_comboFAR = new wxComboBox(tabPage, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, farValues);
	sizer->Add(m_comboFAR, 0, wxALL | wxEXPAND, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Maximal rotation:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT | wxALL, 5);
	m_sliderMaximalRotation = new wxSlider(tabPage, wxID_ANY, 0, 0, 180,
		wxDefaultPosition, wxDefaultSize, wxHORIZONTAL | wxSL_LABELS | wxSL_AUTOTICKS);
	sizer->Add(m_sliderMaximalRotation, 0, wxALL | wxEXPAND, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Matching speed:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT | wxALL, 5);
	m_comboMatchingSpeed = new wxComboBox(tabPage, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize,
		GetEnumStrings(enumNMatchingSpeed), wxCB_DROPDOWN | wxCB_READONLY);
	sizer->Add(m_comboMatchingSpeed, 0, wxALL | wxEXPAND, 5);
	
	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Maximal Result Count:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);
	m_spinMaximalResultCount = new wxSpinCtrl(tabPage);
	m_spinMaximalResultCount->SetRange(1, N_INT32_MAX);
	sizer->Add(m_spinMaximalResultCount, 0, wxALL, 5);

	sizer->AddSpacer(1);
	m_cbFirstResultOnly = new wxCheckBox(tabPage, wxID_ANY, wxT("First Result Only"));
	sizer->Add(m_cbFirstResultOnly, 0, wxALL | wxEXPAND, 5);

	tabPage->SetScrollRate(5, 5);
	tabPage->Layout();

	return tabPage;
}

void OptionsDlg::OnReset(wxCommandEvent&)
{
	if (m_properties.Contains(NPS_FINGERS_MINIMAL_MINUTIA_COUNT))
	{
		m_properties.Set(NPS_FINGERS_MINIMAL_MINUTIA_COUNT, GetDefaultPropertyValue(NPS_FINGERS_MINIMAL_MINUTIA_COUNT));
	}

	if (m_properties.Contains(NPS_FINGERS_QUALITY_THREASHOLD))
	{
		m_properties.Set(NPS_FINGERS_QUALITY_THREASHOLD, GetDefaultPropertyValue(NPS_FINGERS_QUALITY_THREASHOLD));
	}

	if (m_properties.Contains(NPS_FINGERS_TEMPLATE_SIZE))
	{
		m_properties.Set(NPS_FINGERS_TEMPLATE_SIZE, GetDefaultPropertyValue(NPS_FINGERS_TEMPLATE_SIZE));
	}

	if (m_properties.Contains(NPS_FINGERS_EXTRACTED_RIDGE_COUNTS))
	{
		m_properties.Set(NPS_FINGERS_EXTRACTED_RIDGE_COUNTS, GetDefaultPropertyValue(NPS_FINGERS_EXTRACTED_RIDGE_COUNTS));
	}

	m_properties.Set(NPS_FINGERS_RETURN_BINARIZED_IMAGE, NValue(true));

	if (m_properties.Contains(NPS_FINGERS_FAST_EXTRACTION))
	{
		m_properties.Set(NPS_FINGERS_FAST_EXTRACTION, GetDefaultPropertyValue(NPS_FINGERS_FAST_EXTRACTION));
	}

	if (m_properties.Contains(NPS_MATCHING_THRESHOLD))
	{
		m_properties.Set(NPS_MATCHING_THRESHOLD, GetDefaultPropertyValue(NPS_MATCHING_THRESHOLD));
	}

	if (m_properties.Contains(NPS_FINGERS_MAXIMAL_ROTATION))
	{
		m_properties.Set(NPS_FINGERS_MAXIMAL_ROTATION, GetDefaultPropertyValue(NPS_FINGERS_MAXIMAL_ROTATION));
	}

	if (m_properties.Contains(NPS_FINGERS_MATCHING_SPEED))
	{
		m_properties.Set(NPS_FINGERS_MATCHING_SPEED, GetDefaultPropertyValue(NPS_FINGERS_MATCHING_SPEED));
	}

	if (m_properties.Contains(NPS_MATCHING_MAXIMAL_RESULT_COUNT))
	{
		m_properties.Set(NPS_MATCHING_MAXIMAL_RESULT_COUNT, GetDefaultPropertyValue(NPS_MATCHING_MAXIMAL_RESULT_COUNT));
	}

	if (m_properties.Contains(NPS_MATCHING_FIRST_RESULT_ONLY))
	{
		m_properties.Set(NPS_MATCHING_FIRST_RESULT_ONLY, GetDefaultPropertyValue(NPS_MATCHING_FIRST_RESULT_ONLY));
	}

	UpdateGui();
}

void OptionsDlg::OnClose(wxCloseEvent&)
{
	Destroy();
}

void OptionsDlg::UpdateGui()
{
	if (m_properties.Contains(NPS_FINGERS_MINIMAL_MINUTIA_COUNT))
		m_spinMinMinutiaCount->SetValue(m_properties.Get(NPS_FINGERS_MINIMAL_MINUTIA_COUNT).ToInt32());
	else
		m_spinMinMinutiaCount->SetValue(GetDefaultPropertyValue(NPS_FINGERS_MINIMAL_MINUTIA_COUNT).ToInt32());
	
	if (m_properties.Contains(NPS_FINGERS_QUALITY_THREASHOLD))
		m_sliderQualityThreshold->SetValue(m_properties.Get(NPS_FINGERS_QUALITY_THREASHOLD).ToByte());
	else
		m_sliderQualityThreshold->SetValue(GetDefaultPropertyValue(NPS_FINGERS_QUALITY_THREASHOLD).ToByte());

	if (m_properties.Contains(NPS_FINGERS_TEMPLATE_SIZE))
		m_comboTemplateSize->SetSelection(EnumValueToIndex(enumNTemplateSize, m_properties.Get(NPS_FINGERS_TEMPLATE_SIZE).ToInt32()));
	else
		m_comboTemplateSize->SetSelection(EnumValueToIndex(enumNTemplateSize, GetDefaultPropertyValue(NPS_FINGERS_TEMPLATE_SIZE).ToInt32()));

	if (m_properties.Contains(NPS_FINGERS_EXTRACTED_RIDGE_COUNTS))
		m_comboExtractedRidgeCounts->SetSelection(EnumValueToIndex(enumNFRidgeCountsType, m_properties.Get(NPS_FINGERS_EXTRACTED_RIDGE_COUNTS).ToInt32()));
	else
		m_comboExtractedRidgeCounts->SetSelection(EnumValueToIndex(enumNFRidgeCountsType, GetDefaultPropertyValue(NPS_FINGERS_EXTRACTED_RIDGE_COUNTS).ToInt32()));

	if (m_properties.Contains(NPS_FINGERS_RETURN_BINARIZED_IMAGE))
		m_cbReturnBinarizedImage->SetValue(m_properties.Get(NPS_FINGERS_RETURN_BINARIZED_IMAGE).ToBoolean());
	else
		m_cbReturnBinarizedImage->SetValue(GetDefaultPropertyValue(NPS_FINGERS_RETURN_BINARIZED_IMAGE).ToBoolean());

	if (m_properties.Contains(NPS_FINGERS_FAST_EXTRACTION))
		m_cbFastExtraction->SetValue(m_properties.Get(NPS_FINGERS_FAST_EXTRACTION).ToBoolean());
	else
		m_cbFastExtraction->SetValue(GetDefaultPropertyValue(NPS_FINGERS_FAST_EXTRACTION).ToBoolean());

	if (m_properties.Contains(NPS_MATCHING_THRESHOLD))
		m_comboFAR->SetValue(MatchingThresholdToFARString(m_properties.Get(NPS_MATCHING_THRESHOLD).ToDouble()));
	else
		m_comboFAR->SetValue(MatchingThresholdToFARString(GetDefaultPropertyValue(NPS_MATCHING_THRESHOLD).ToDouble()));

	if (m_properties.Contains(NPS_FINGERS_MAXIMAL_ROTATION))
		m_sliderMaximalRotation->SetValue(m_properties.Get(NPS_FINGERS_MAXIMAL_ROTATION).ToSingle());
	else
		m_sliderMaximalRotation->SetValue(GetDefaultPropertyValue(NPS_FINGERS_MAXIMAL_ROTATION).ToSingle());

	if (m_properties.Contains(NPS_FINGERS_MATCHING_SPEED))
		m_comboMatchingSpeed->SetSelection(EnumValueToIndex(enumNMatchingSpeed, m_properties.Get(NPS_FINGERS_MATCHING_SPEED).ToInt32()));
	else
		m_comboMatchingSpeed->SetSelection(EnumValueToIndex(enumNMatchingSpeed, GetDefaultPropertyValue(NPS_FINGERS_MATCHING_SPEED).ToInt32()));

	if (m_properties.Contains(NPS_MATCHING_MAXIMAL_RESULT_COUNT))
		m_spinMaximalResultCount->SetValue(m_properties.Get(NPS_MATCHING_MAXIMAL_RESULT_COUNT).ToInt32());
	else
		m_spinMaximalResultCount->SetValue(GetDefaultPropertyValue(NPS_MATCHING_MAXIMAL_RESULT_COUNT).ToInt32());

	if (m_properties.Contains(NPS_MATCHING_FIRST_RESULT_ONLY))
		m_cbFirstResultOnly->SetValue(m_properties.Get(NPS_MATCHING_FIRST_RESULT_ONLY).ToBoolean());
	else
		m_cbFirstResultOnly->SetValue(GetDefaultPropertyValue(NPS_MATCHING_FIRST_RESULT_ONLY).ToBoolean());
}

void OptionsDlg::SaveOptions()
{
	wxConfigBase *config = wxConfigBase::Get();
	if (m_properties.GetHandle())
	{
		wxString propertiesString = m_properties.ToString();
		config->Write(wxT("BiometricClient/Properties"), propertiesString);
	}
}

void OptionsDlg::SaveOptions(::Neurotec::Biometrics::Client::NBiometricClient biometricClient)
{
	wxConfigBase *config = wxConfigBase::Get();
	if (biometricClient.GetHandle())
	{
		NPropertyBag properties;
		biometricClient.CaptureProperties(properties);
		wxString propertiesString = properties.ToString();
		config->Write(wxT("BiometricClient/Properties"), propertiesString);
	}
}

void OptionsDlg::LoadOptions(::Neurotec::Biometrics::Client::NBiometricClient biometricClient)
{
	wxConfigBase *config = wxConfigBase::Get();
	wxString propertiesString;
	if (!biometricClient.GetHandle() || !config->Read(wxT("BiometricClient/Properties"), &propertiesString))
		return;

	if (propertiesString != wxEmptyString)
	{
		NPropertyBag propertyBag(NPropertyBag::Parse(propertiesString));
		propertyBag.ApplyTo(biometricClient);
	}
}

int OptionsDlg::EnumValueToIndex(const EnumOptionEntry *entries, int value)
{
	int minDiff = 999999;
	int bestIndex = 0;
	int index = 0;
	while (entries->text)
	{
		int diff = abs(value - entries->value);
		if (diff < minDiff)
		{
			minDiff = diff;
			bestIndex = index;
		}

		entries++;
		index++;
	}

	return bestIndex;
}

int OptionsDlg::EnumIndexToValue(const EnumOptionEntry *entries, int index)
{
	if (index < 0)
	{
		index = 0;
	}

	return entries[index].value;
}

wxArrayString OptionsDlg::GetEnumStrings(const EnumOptionEntry *entries)
{
	wxArrayString values;
	while (entries->text)
	{
		values.Add(entries->text);
		entries++;
	}

	return values;
}

void OptionsDlg::OnOK(wxCommandEvent&)
{
	int matchingThreshold = FARStringToMatchingThreshold(m_comboFAR->GetValue());
	if (matchingThreshold < 0)
	{
		wxMessageBox(wxT("Matching FAR value is invalid. Reseting to default."));
		NValue value = GetDefaultPropertyValue(NPS_MATCHING_THRESHOLD);
		m_comboFAR->SetValue(MatchingThresholdToFARString(value.ToDouble()));
		return;
	}

	{
		NValue defaultValue = GetDefaultPropertyValue(NPS_FINGERS_MINIMAL_MINUTIA_COUNT);
		NValue value(m_spinMinMinutiaCount->GetValue());

		if (!defaultValue.Equals(value) || m_properties.Contains(NPS_FINGERS_MINIMAL_MINUTIA_COUNT))
			m_properties.Set(NPS_FINGERS_MINIMAL_MINUTIA_COUNT, value);
	}

	{
		NValue defaultValue = GetDefaultPropertyValue(NPS_FINGERS_QUALITY_THREASHOLD);
		NValue value((char)m_sliderQualityThreshold->GetValue());

		if (!defaultValue.Equals(value) || m_properties.Contains(NPS_FINGERS_QUALITY_THREASHOLD))
			m_properties.Set(NPS_FINGERS_QUALITY_THREASHOLD, value);
	}

	{
		NValue defaultValue = GetDefaultPropertyValue(NPS_FINGERS_TEMPLATE_SIZE);
		NValue value(EnumIndexToValue(enumNTemplateSize, m_comboTemplateSize->GetSelection()));

		if (!defaultValue.Equals(value) || m_properties.Contains(NPS_FINGERS_TEMPLATE_SIZE))
			m_properties.Set(NPS_FINGERS_TEMPLATE_SIZE, value);
	}

	{
		NValue defaultValue = GetDefaultPropertyValue(NPS_FINGERS_EXTRACTED_RIDGE_COUNTS);
		NValue value(EnumIndexToValue(enumNFRidgeCountsType, m_comboExtractedRidgeCounts->GetSelection()));

		if (!defaultValue.Equals(value) || m_properties.Contains(NPS_FINGERS_EXTRACTED_RIDGE_COUNTS))
			m_properties.Set(NPS_FINGERS_EXTRACTED_RIDGE_COUNTS, value);
	}

	{
		NValue defaultValue = GetDefaultPropertyValue(NPS_FINGERS_RETURN_BINARIZED_IMAGE);
		NValue value(m_cbReturnBinarizedImage->GetValue());

		if (!defaultValue.Equals(value) || m_properties.Contains(NPS_FINGERS_RETURN_BINARIZED_IMAGE))
			m_properties.Set(NPS_FINGERS_RETURN_BINARIZED_IMAGE, value);
	}

	{
		NValue defaultValue = GetDefaultPropertyValue(NPS_FINGERS_FAST_EXTRACTION);
		NValue value(m_cbFastExtraction->GetValue());

		if (!defaultValue.Equals(value) || m_properties.Contains(NPS_FINGERS_FAST_EXTRACTION))
			m_properties.Set(NPS_FINGERS_FAST_EXTRACTION, value);
	}

	{
		NValue defaultValue = GetDefaultPropertyValue(NPS_MATCHING_THRESHOLD);
		NValue value(FARStringToMatchingThreshold(m_comboFAR->GetValue()));

		if (!defaultValue.Equals(value) || m_properties.Contains(NPS_MATCHING_THRESHOLD))
			m_properties.Set(NPS_MATCHING_THRESHOLD, value);
	}

	{
		NValue defaultValue = GetDefaultPropertyValue(NPS_FINGERS_MAXIMAL_ROTATION);
		NValue value(m_sliderMaximalRotation->GetValue());

		if (!defaultValue.Equals(value) || m_properties.Contains(NPS_FINGERS_MAXIMAL_ROTATION))
			m_properties.Set(NPS_FINGERS_MAXIMAL_ROTATION, value);
	}

	{
		NValue defaultValue = GetDefaultPropertyValue(NPS_FINGERS_MATCHING_SPEED);
		NValue value(EnumIndexToValue(enumNMatchingSpeed, m_comboMatchingSpeed->GetSelection()));

		if (!defaultValue.Equals(value) || m_properties.Contains(NPS_FINGERS_MATCHING_SPEED))
			m_properties.Set(NPS_FINGERS_MATCHING_SPEED, value);
	}

	{
		NValue defaultValue = GetDefaultPropertyValue(NPS_MATCHING_MAXIMAL_RESULT_COUNT);
		NValue value(m_spinMaximalResultCount->GetValue());

		if (!defaultValue.Equals(value) || m_properties.Contains(NPS_MATCHING_MAXIMAL_RESULT_COUNT))
			m_properties.Set(NPS_MATCHING_MAXIMAL_RESULT_COUNT, value);
	}

	{
		NValue defaultValue = GetDefaultPropertyValue(NPS_MATCHING_FIRST_RESULT_ONLY);
		NValue value(m_cbFirstResultOnly->GetValue());

		if (!defaultValue.Equals(value) || m_properties.Contains(NPS_MATCHING_FIRST_RESULT_ONLY))
			m_properties.Set(NPS_MATCHING_FIRST_RESULT_ONLY, value);
	}

	SaveOptions();

	m_properties.ApplyTo(m_biometricClient);

	EndModal(wxID_OK);
}

NValue OptionsDlg::GetDefaultPropertyValue(const wxString& name)
{
	::Neurotec::NType type = ::Neurotec::Biometrics::NBiometricEngine::NativeTypeOf();
	::Neurotec::Reflection::NPropertyInfo info = type.GetDeclaredProperty(name);
	return info.GetDefaultValue();
}

double OptionsDlg::MatchingThresholdToFAR(int th)
{
	if(th < 0) th = 0;
	return pow(10.0, -th / FARLogRatio);
}

int OptionsDlg::FARToMatchingThreshold(double f)
{
	if(f > 1) f = 1;
	else if(f <= 0.0) f = 1E-100;
	return (int)wxRound(-log10(f) * FARLogRatio);
}

wxString OptionsDlg::MatchingThresholdToFARString(int matchingThreshold)
{
	NDouble value = MatchingThresholdToFAR(matchingThreshold);
	value *= 100.0;
	wxString stringValue = wxString::Format(wxT("%.9f"), value);
	// remove trailing zeros
	int trailingCount = 0;
	int position = (int) stringValue.Length() - 1;
	while (position >= 0 &&
		stringValue[position] == wxT('0'))
	{
		trailingCount++;
		position--;
	}
	if (position >= 0 &&
		!wxIsdigit(stringValue[position]))
	{
		trailingCount++;
		position--;
	}
	stringValue.RemoveLast(trailingCount);
	stringValue.Append(wxT("%"));
	return stringValue;
}

int OptionsDlg::FARStringToMatchingThreshold(const wxString& farString)
{
	wxString str = farString;
	str.Replace(wxT("%"), wxT(""));
	double value;
	if (!str.ToDouble(&value))
	{
		return -1;
	}
	else
	{
		value /= 100.0;
		return FARToMatchingThreshold(value);
	}
}

}}

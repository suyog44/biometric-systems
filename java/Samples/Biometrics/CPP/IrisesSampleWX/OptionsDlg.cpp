#include "Precompiled.h"
#include "OptionsDlg.h"

using namespace Neurotec::Biometrics;

namespace Neurotec { namespace Samples
{

//Enroll
#define NPS_IRISES_INNER_BOUNDARY_FROM wxT("Irises.InnerBoundaryFrom")
#define NPS_IRISES_INNER_BOUNDARY_TO wxT("Irises.InnerBoundaryTo")
#define NPS_IRISES_OUTER_BOUNDARY_FROM wxT("Irises.OuterBoundaryFrom")
#define NPS_IRISES_OUTER_BOUNDARY_TO wxT("Irises.OuterBoundaryTo")
#define NPS_IRISES_QUALITY_THREASHOLD wxT("Irises.QualityThreshold")

//Identify
#define NPS_IRISES_MATCHING_THRESHOLD wxT("Matching.Threshold")
#define NPS_IRISES_MATCHING_SPEED wxT("Irises.MatchingSpeed")
#define NPS_IRISES_MAXIMAL_ROTATION wxT("Irises.MaximalRotation")
#define NPS_MATCHING_MAXIMAL_RESULT_COUNT wxT("Matching.MaximalResultCount")
#define NPS_MATCHING_FIRST_RESULT_ONLY wxT("Matching.FirstResultOnly")

const double FARLogRatio = 12;

struct EnumOptionEntry
{
	const wxChar *text;
	int value;
};

const EnumOptionEntry enumNemSpeed[] =
{
	{ wxT("Low"), nmsLow },
	{ wxT("Medium"), nmsMedium },
	{ wxT("High"), nmsHigh },
	{ NULL, 0 }
};

BEGIN_EVENT_TABLE(OptionsDlg,wxDialog)
	EVT_CLOSE(OptionsDlg::OnClose)
	EVT_BUTTON(wxID_RESET, OptionsDlg::OnDefault)
	EVT_BUTTON(wxID_OK, OptionsDlg::OnOK)
END_EVENT_TABLE()

OptionsDlg::OptionsDlg(wxWindow *parent, Neurotec::Biometrics::Client::NBiometricClient biometricClient, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize &size, long style) :
	wxDialog(parent, id, title, position, size, style),
	m_biometricClient(biometricClient)
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
	this->SetSizeHints(300, 300);

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

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Inner boundary radius from:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);
	m_spinInnerBoundaryFrom = new wxSpinCtrl(tabPage);
	m_spinInnerBoundaryFrom->SetRange(10, 256);
	sizer->Add(m_spinInnerBoundaryFrom, 0, wxALL, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Inner boundary radius to:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);
	m_spinInnerBoundaryTo = new wxSpinCtrl(tabPage);
	m_spinInnerBoundaryTo->SetRange(10, 256);
	sizer->Add(m_spinInnerBoundaryTo, 0, wxALL, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Outer boundary radius from:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);
	m_spinOuterBoundaryFrom = new wxSpinCtrl(tabPage);
	m_spinOuterBoundaryFrom->SetRange(10, 256);
	sizer->Add(m_spinOuterBoundaryFrom, 0, wxALL, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Outer boundary radius to:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);
	m_spinOuterBoundaryTo = new wxSpinCtrl(tabPage);
	m_spinOuterBoundaryTo->SetRange(10, 256);
	sizer->Add(m_spinOuterBoundaryTo, 0, wxALL, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Quality threshold:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);

	m_sliderQualityThreshold = new wxSlider(tabPage, wxID_ANY, 0, 0, 100, wxDefaultPosition, wxDefaultSize, wxHORIZONTAL | wxSL_LABELS);
	sizer->Add(m_sliderQualityThreshold, 0, wxALL | wxEXPAND, 5);

	tabPage->SetScrollRate(5, 5);
	tabPage->Layout();

	return tabPage;
}

void OptionsDlg::OnOK(wxCommandEvent &/*event*/)
{
	int matchingThreshold = FARStringToMatchingThreshold(m_comboFAR->GetValue());
	if (matchingThreshold < 0)
	{
		wxMessageBox(wxT("Matching FAR value is invalid. Reseting to default."));
		NValue value = GetDefaultPropertyValue(NPS_IRISES_MATCHING_THRESHOLD);
		m_comboFAR->SetValue(MatchingThresholdToFARString(value.ToDouble()));
		return;
	}

	NValue defaultValue = NValue(GetDefaultPropertyValue(NPS_IRISES_INNER_BOUNDARY_FROM));
	NValue value = NValue(m_spinInnerBoundaryFrom->GetValue());

	if (!defaultValue.Equals(value) || m_properties.Contains(NPS_IRISES_INNER_BOUNDARY_FROM))
		m_properties.Set(NPS_IRISES_INNER_BOUNDARY_FROM, value);

	defaultValue = NValue(GetDefaultPropertyValue(NPS_IRISES_INNER_BOUNDARY_TO));
	value = NValue(m_spinInnerBoundaryTo->GetValue());

	if (!defaultValue.Equals(value) || m_properties.Contains(NPS_IRISES_INNER_BOUNDARY_TO))
		m_properties.Set(NPS_IRISES_INNER_BOUNDARY_TO, value);

	defaultValue = NValue(GetDefaultPropertyValue(NPS_IRISES_OUTER_BOUNDARY_FROM));
	value = NValue(m_spinOuterBoundaryFrom->GetValue());

	if (!defaultValue.Equals(value) || m_properties.Contains(NPS_IRISES_OUTER_BOUNDARY_FROM))
		m_properties.Set(NPS_IRISES_OUTER_BOUNDARY_FROM, value);

	defaultValue = NValue(GetDefaultPropertyValue(NPS_IRISES_OUTER_BOUNDARY_TO));
	value = NValue(m_spinOuterBoundaryTo->GetValue());

	if (!defaultValue.Equals(value) || m_properties.Contains(NPS_IRISES_OUTER_BOUNDARY_TO))
		m_properties.Set(NPS_IRISES_OUTER_BOUNDARY_TO, value);

	defaultValue = NValue(GetDefaultPropertyValue(NPS_IRISES_QUALITY_THREASHOLD));
	value = NValue((char)m_sliderQualityThreshold->GetValue());

	if (!defaultValue.Equals(value) || m_properties.Contains(NPS_IRISES_QUALITY_THREASHOLD))
		m_properties.Set(NPS_IRISES_QUALITY_THREASHOLD, value);

	defaultValue = NValue(GetDefaultPropertyValue(NPS_IRISES_MATCHING_THRESHOLD));
	value = NValue(FARStringToMatchingThreshold(m_comboFAR->GetValue()));

	if (!defaultValue.Equals(value) || m_properties.Contains(NPS_IRISES_MATCHING_THRESHOLD))
		m_properties.Set(NPS_IRISES_MATCHING_THRESHOLD, value);

	defaultValue = NValue(GetDefaultPropertyValue(NPS_IRISES_MAXIMAL_ROTATION));
	value = NValue(m_sliderMaximalRotation->GetValue());

	if (!defaultValue.Equals(value) || m_properties.Contains(NPS_IRISES_MAXIMAL_ROTATION))
		m_properties.Set(NPS_IRISES_MAXIMAL_ROTATION, value);

	defaultValue = NValue(GetDefaultPropertyValue(NPS_IRISES_MATCHING_SPEED));
	value = NValue(EnumIndexToValue(enumNemSpeed, m_comboMatchingSpeed->GetSelection()));

	if (!defaultValue.Equals(value) || m_properties.Contains(NPS_IRISES_MATCHING_SPEED))
		m_properties.Set(NPS_IRISES_MATCHING_SPEED, value);

	defaultValue = NValue(GetDefaultPropertyValue(NPS_MATCHING_MAXIMAL_RESULT_COUNT));
	value = NValue(m_spinMaximalResultCount->GetValue());

	if (!defaultValue.Equals(value) || m_properties.Contains(NPS_MATCHING_MAXIMAL_RESULT_COUNT))
		m_properties.Set(NPS_MATCHING_MAXIMAL_RESULT_COUNT, value);

	defaultValue = NValue(GetDefaultPropertyValue(NPS_MATCHING_FIRST_RESULT_ONLY));
	value = NValue(m_cbFirstResultOnly->GetValue());

	if (!defaultValue.Equals(value) || m_properties.Contains(NPS_MATCHING_FIRST_RESULT_ONLY))
		m_properties.Set(NPS_MATCHING_FIRST_RESULT_ONLY, value);

	m_properties.ApplyTo(m_biometricClient);
	EndModal(wxID_OK);
}

void OptionsDlg::OnDefault(wxCommandEvent &/*event*/)
{
	if (m_properties.Contains(NPS_IRISES_INNER_BOUNDARY_FROM))
	{
		m_properties.Set(NPS_IRISES_INNER_BOUNDARY_FROM, GetDefaultPropertyValue(NPS_IRISES_INNER_BOUNDARY_FROM));
	}

	if (m_properties.Contains(NPS_IRISES_INNER_BOUNDARY_TO))
	{
		m_properties.Set(NPS_IRISES_INNER_BOUNDARY_TO, GetDefaultPropertyValue(NPS_IRISES_INNER_BOUNDARY_TO));
	}

	if (m_properties.Contains(NPS_IRISES_OUTER_BOUNDARY_FROM))
	{
		m_properties.Set(NPS_IRISES_OUTER_BOUNDARY_FROM, GetDefaultPropertyValue(NPS_IRISES_OUTER_BOUNDARY_FROM));
	}

	if (m_properties.Contains(NPS_IRISES_OUTER_BOUNDARY_TO))
	{
		m_properties.Set(NPS_IRISES_OUTER_BOUNDARY_TO, GetDefaultPropertyValue(NPS_IRISES_OUTER_BOUNDARY_TO));
	}

	if (m_properties.Contains(NPS_IRISES_QUALITY_THREASHOLD))
	{
		m_properties.Set(NPS_IRISES_QUALITY_THREASHOLD, GetDefaultPropertyValue(NPS_IRISES_QUALITY_THREASHOLD));
	}

	if (m_properties.Contains(NPS_IRISES_MATCHING_THRESHOLD))
	{
		m_properties.Set(NPS_IRISES_MATCHING_THRESHOLD, GetDefaultPropertyValue(NPS_IRISES_MATCHING_THRESHOLD));
	}

	if (m_properties.Contains(NPS_IRISES_MAXIMAL_ROTATION))
	{
		m_properties.Set(NPS_IRISES_MAXIMAL_ROTATION, GetDefaultPropertyValue(NPS_IRISES_MAXIMAL_ROTATION));
	}

	if (m_properties.Contains(NPS_IRISES_MATCHING_SPEED))
	{
		m_properties.Set(NPS_IRISES_MATCHING_SPEED, GetDefaultPropertyValue(NPS_IRISES_MATCHING_SPEED));
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
		NPropertyBag propertyBag = NPropertyBag::Parse(propertiesString);
		propertyBag.ApplyTo(biometricClient);
	}
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
		GetEnumStrings(enumNemSpeed), wxCB_DROPDOWN | wxCB_READONLY);
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

void OptionsDlg::UpdateGui()
{
	if (m_properties.Contains(NPS_IRISES_INNER_BOUNDARY_FROM))
		m_spinInnerBoundaryFrom->SetValue(m_properties.Get(NPS_IRISES_INNER_BOUNDARY_FROM).ToInt32());
	else
		m_spinInnerBoundaryFrom->SetValue(GetDefaultPropertyValue(NPS_IRISES_INNER_BOUNDARY_FROM).ToInt32());

	if (m_properties.Contains(NPS_IRISES_INNER_BOUNDARY_TO))
		m_spinInnerBoundaryTo->SetValue(m_properties.Get(NPS_IRISES_INNER_BOUNDARY_TO).ToInt32());
	else
		m_spinInnerBoundaryTo->SetValue(GetDefaultPropertyValue(NPS_IRISES_INNER_BOUNDARY_TO).ToInt32());

	if (m_properties.Contains(NPS_IRISES_OUTER_BOUNDARY_FROM))
		m_spinOuterBoundaryFrom->SetValue(m_properties.Get(NPS_IRISES_OUTER_BOUNDARY_FROM).ToInt32());
	else
		m_spinOuterBoundaryFrom->SetValue(GetDefaultPropertyValue(NPS_IRISES_OUTER_BOUNDARY_FROM).ToInt32());

	if (m_properties.Contains(NPS_IRISES_OUTER_BOUNDARY_TO))
		m_spinOuterBoundaryTo->SetValue(m_properties.Get(NPS_IRISES_OUTER_BOUNDARY_TO).ToInt32());
	else
		m_spinOuterBoundaryTo->SetValue(GetDefaultPropertyValue(NPS_IRISES_OUTER_BOUNDARY_TO).ToInt32());

	if (m_properties.Contains(NPS_IRISES_QUALITY_THREASHOLD))
		m_sliderQualityThreshold->SetValue(m_properties.Get(NPS_IRISES_QUALITY_THREASHOLD).ToByte());
	else
		m_sliderQualityThreshold->SetValue(GetDefaultPropertyValue(NPS_IRISES_QUALITY_THREASHOLD).ToByte());

	if (m_properties.Contains(NPS_IRISES_MATCHING_THRESHOLD))
		m_comboFAR->SetValue(MatchingThresholdToFARString(m_properties.Get(NPS_IRISES_MATCHING_THRESHOLD).ToDouble()));
	else
		m_comboFAR->SetValue(MatchingThresholdToFARString(GetDefaultPropertyValue(NPS_IRISES_MATCHING_THRESHOLD).ToDouble()));

	if (m_properties.Contains(NPS_IRISES_MAXIMAL_ROTATION))
		m_sliderMaximalRotation->SetValue(m_properties.Get(NPS_IRISES_MAXIMAL_ROTATION).ToInt32());
	else
		m_sliderMaximalRotation->SetValue(GetDefaultPropertyValue(NPS_IRISES_MAXIMAL_ROTATION).ToInt32());

	if (m_properties.Contains(NPS_IRISES_MATCHING_SPEED))
		m_comboMatchingSpeed->SetSelection(EnumValueToIndex(enumNemSpeed,m_properties.Get(NPS_IRISES_MATCHING_SPEED).ToInt32()));
	else
		m_comboMatchingSpeed->SetSelection(EnumValueToIndex(enumNemSpeed,GetDefaultPropertyValue(NPS_IRISES_MATCHING_SPEED).ToInt32()));

	if (m_properties.Contains(NPS_MATCHING_MAXIMAL_RESULT_COUNT))
		m_spinMaximalResultCount->SetValue(m_properties.Get(NPS_MATCHING_MAXIMAL_RESULT_COUNT).ToInt32());
	else
		m_spinMaximalResultCount->SetValue(GetDefaultPropertyValue(NPS_MATCHING_MAXIMAL_RESULT_COUNT).ToInt32());

	if (m_properties.Contains(NPS_MATCHING_FIRST_RESULT_ONLY))
		m_cbFirstResultOnly->SetValue(m_properties.Get(NPS_MATCHING_FIRST_RESULT_ONLY).ToBoolean());
	else
		m_cbFirstResultOnly->SetValue(GetDefaultPropertyValue(NPS_MATCHING_FIRST_RESULT_ONLY).ToBoolean());
}

void OptionsDlg::OnClose(wxCloseEvent &/*event*/)
{
	Destroy();
}

NValue OptionsDlg::GetDefaultPropertyValue(const NStringWrapper & name)
{
	NType type = NBiometricEngine::NativeTypeOf();
	Reflection::NPropertyInfo info = type.GetDeclaredProperty(name);
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

}}

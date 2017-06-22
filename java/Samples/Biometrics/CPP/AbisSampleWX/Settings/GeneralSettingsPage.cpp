#include "Precompiled.h"

#include <Settings/GeneralSettingsPage.h>

using namespace ::Neurotec::Biometrics;
using namespace ::Neurotec::Biometrics::Client;

namespace Neurotec { namespace Samples
{

const double FARLogRatio = 12;

GeneralSettingsPage::GeneralSettingsPage(wxWindow *parent, wxWindowID winid) : BaseSettingsPage(parent, winid)
{
	CreateGUIControls();
	RegisterGuiEvents();
}

GeneralSettingsPage::~GeneralSettingsPage()
{
	UnregisterGuiEvents();
}

void GeneralSettingsPage::OnMatchingThresholdChanged(wxCommandEvent&)
{
	int matchingThreshold = FARStringToMatchingThreshold(m_choiceMatchingTreshold->GetStringSelection());
	if (matchingThreshold < 0)
	{
		wxMessageBox(wxT("Matching FAR value is invalid. Reseting to default."));
		NValue value = GetDefaultPropertyValue(wxT("Matching.Threshold"));
		double tr = value.ToDouble();
		m_choiceMatchingTreshold->SetStringSelection(MatchingThresholdToFARString(tr));
		matchingThreshold = FARToMatchingThreshold(tr);
	}
	m_biometricClient.SetMatchingThreshold(matchingThreshold);
}

void GeneralSettingsPage::OnMaximalResultCountChanged(wxSpinEvent&)
{
	m_biometricClient.SetMatchingMaximalResultCount(m_spinMaximalResultsCount->GetValue());
}

void GeneralSettingsPage::OnReturnMatchingDetailsChanged(wxCommandEvent&)
{
	m_biometricClient.SetMatchingWithDetails(m_chkRetunMatchingDetails->GetValue());
}

void GeneralSettingsPage::OnFirstResultOnlyChanged(wxCommandEvent&)
{
	m_biometricClient.SetFirstResultOnly(m_chkFirstResultOnly->GetValue());
}

void GeneralSettingsPage::Load()
{
	m_spinMaximalResultsCount->SetValue(m_biometricClient.GetMatchingMaximalResultCount());
	m_chkFirstResultOnly->SetValue(m_biometricClient.GetFirstResultOnly());
	m_chkRetunMatchingDetails->SetValue(m_biometricClient.GetMatchingWithDetails());
	m_choiceMatchingTreshold->SetStringSelection(MatchingThresholdToFARString(m_biometricClient.GetMatchingThreshold()));
}

void GeneralSettingsPage::Reset()
{
	m_biometricClient.ResetProperty(wxT("Matching.Threshold"));
	m_biometricClient.ResetProperty(wxT("Matching.MaximalResultCount"));
	m_biometricClient.SetMatchingWithDetails(true);
	m_biometricClient.ResetProperty(wxT("Matching.FirstResultOnly"));

	this->Load();
}

double GeneralSettingsPage::MatchingThresholdToFAR(int threshold)
{
	if(threshold < 0)
		threshold = 0;

	return pow(10.0, -threshold / FARLogRatio);
}

int GeneralSettingsPage::FARToMatchingThreshold(double f)
{
	if (f > 1)
	{
		f = 1;
	}
	else if(f <= 0.0)
	{
		f = 1E-100;
	}

	return (int)wxRound(-log10(f) * FARLogRatio);
}

wxString GeneralSettingsPage::MatchingThresholdToFARString(int matchingThreshold)
{
	NDouble value = MatchingThresholdToFAR(matchingThreshold) * 100.0;
	wxString stringValue = wxString::Format(wxT("%.9f"), value);

	int trailingCount = 0;
	int position = (int) stringValue.Length() - 1;

	while (position >= 0 && stringValue[position] == wxT('0'))
	{
		trailingCount++;
		position--;
	}

	if (position >= 0 && !wxIsdigit(stringValue[position]))
	{
		trailingCount++;
		position--;
	}

	stringValue.RemoveLast(trailingCount);
	stringValue.Append(wxT("%"));

	return stringValue;
}

int GeneralSettingsPage::FARStringToMatchingThreshold(const wxString& farString)
{
	wxString str = farString;

	str.Replace(wxT("%"), wxT(""));

	double value;

	if (!str.ToDouble(&value))
		return -1;

	return FARToMatchingThreshold(value / 100.0);
}

NValue GeneralSettingsPage::GetDefaultPropertyValue(const wxString& name)
{
	::Neurotec::NType type = ::Neurotec::Biometrics::NBiometricEngine::NativeTypeOf();
	::Neurotec::Reflection::NPropertyInfo info = type.GetDeclaredProperty(name);
	return info.GetDefaultValue();
}

void GeneralSettingsPage::RegisterGuiEvents()
{
	m_choiceMatchingTreshold->Connect(wxEVT_CHOICE, wxCommandEventHandler(GeneralSettingsPage::OnMatchingThresholdChanged), NULL, this);
	m_spinMaximalResultsCount->Connect(wxEVT_SPINCTRL, wxSpinEventHandler(GeneralSettingsPage::OnMaximalResultCountChanged), NULL, this);
	m_chkRetunMatchingDetails->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(GeneralSettingsPage::OnReturnMatchingDetailsChanged), NULL, this);
	m_chkFirstResultOnly->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(GeneralSettingsPage::OnFirstResultOnlyChanged), NULL, this);
}

void GeneralSettingsPage::UnregisterGuiEvents()
{
	m_choiceMatchingTreshold->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(GeneralSettingsPage::OnMatchingThresholdChanged), NULL, this);
	m_spinMaximalResultsCount->Disconnect(wxEVT_SPINCTRL, wxSpinEventHandler(GeneralSettingsPage::OnMaximalResultCountChanged), NULL, this);
	m_chkRetunMatchingDetails->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(GeneralSettingsPage::OnReturnMatchingDetailsChanged), NULL, this);
	m_chkFirstResultOnly->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(GeneralSettingsPage::OnFirstResultOnlyChanged), NULL, this);
}

void GeneralSettingsPage::CreateGUIControls()
{
	wxFlexGridSizer *sizer = new wxFlexGridSizer(4, 2, 5, 5);

	wxArrayString thresholdValues;
	thresholdValues.Add(wxT("0.1%"));
	thresholdValues.Add(wxT("0.01%"));
	thresholdValues.Add(wxT("0.001%"));
	thresholdValues.Add(wxT("0.0001%"));
	m_choiceMatchingTreshold = new wxChoice(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, thresholdValues);

	m_spinMaximalResultsCount = new wxSpinCtrl(this, wxID_ANY);
	m_spinMaximalResultsCount->SetRange(1, N_INT32_MAX);

	m_chkRetunMatchingDetails = new wxCheckBox(this, wxID_ANY, wxT("Return matching details"));
	m_chkFirstResultOnly = new wxCheckBox(this, wxID_ANY, wxT("First result only"));

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Matching treshold:")), 0, wxALL | wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL, 0);
	sizer->Add(m_choiceMatchingTreshold, 0, wxALL | wxEXPAND | wxALIGN_LEFT, 0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Maximal results count:")), 0, wxALL | wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL, 0);
	sizer->Add(m_spinMaximalResultsCount, 0, wxALL | wxEXPAND | wxALIGN_LEFT, 0);

	sizer->AddSpacer(0);
	sizer->Add(m_chkRetunMatchingDetails, 0, wxALL | wxALIGN_LEFT, 0);

	sizer->AddSpacer(0);
	sizer->Add(m_chkFirstResultOnly, 0, wxALL | wxALIGN_LEFT, 0);

	this->SetSizer(sizer, true);
	this->Layout();
}

}}


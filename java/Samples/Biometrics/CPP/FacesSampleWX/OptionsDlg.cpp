#include "Precompiled.h"
#include "OptionsDlg.h"

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Gui;

namespace Neurotec { namespace Samples
{

BEGIN_EVENT_TABLE(OptionsDlg,wxDialog)
	EVT_CLOSE(OptionsDlg::OnClose)
	EVT_BUTTON(wxID_RESET, OptionsDlg::OnDefault)
	EVT_BUTTON(wxID_OK, OptionsDlg::OnOK)
	EVT_CHECKBOX(wxID_ANY, OptionsDlg::OnCheckBoxClicked)
END_EVENT_TABLE()

const double FARLogRatio = 12;

OptionsDlg::OptionsDlg(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize &size, long style)
: wxDialog(parent, id, title, position, size, style)
{
	CreateGUIControls();
}

OptionsDlg::~OptionsDlg()
{
}

void OptionsDlg::CreateGUIControls()
{
	this->SetSizeHints(500, 550);

	wxBoxSizer* bSizer1;
	bSizer1 = new wxBoxSizer(wxVERTICAL);

	wxNotebook *notebook = new wxNotebook(this, wxID_ANY);
	notebook->AddPage(CreateExtractionPage(notebook), wxT("Extraction"));
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

wxWindow* OptionsDlg::CreateExtractionPage(wxWindow *parent)
{
	bool isActivated = Neurotec::Licensing::NLicense::IsComponentActivated(wxT("Biometrics.FaceSegmentation"));

	wxScrolledWindow* tabPage = new wxScrolledWindow(parent, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxHSCROLL | wxVSCROLL);
	wxBoxSizer *boxSizer;
	boxSizer = new wxBoxSizer(wxVERTICAL);
	tabPage->SetSizer(boxSizer);

	wxStaticBoxSizer *staticBoxSizer;
	wxGridSizer *sizer;
	wxStaticText *staticText;

	staticBoxSizer = new wxStaticBoxSizer(new wxStaticBox(tabPage, wxID_ANY, wxT("Extraction")), wxVERTICAL);
	boxSizer->Add(staticBoxSizer, 0, wxEXPAND, 5);
	sizer = new wxFlexGridSizer(0, 2, 0, 0);
	staticBoxSizer->Add(sizer, 0, wxEXPAND, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Template size:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);

	wxString templateSizes [] = { wxT("Large"), wxT("Medium"), wxT("Small") };
	m_comboTemplateSize = new wxComboBox(tabPage, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, 3, templateSizes, wxCB_DROPDOWN | wxCB_READONLY);
	sizer->Add(m_comboTemplateSize, 0, wxALL | wxEXPAND, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Minimal ocular distance:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);

	m_spinMinIOD = new wxSpinCtrl(tabPage);
	m_spinMinIOD->SetRange(8, 16384);
	sizer->Add(m_spinMinIOD, 0, wxALL, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Face confidence threshold:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);

	m_spinConfidenceThreshold = new wxSpinCtrl(tabPage, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, 16384|wxALIGN_RIGHT, 0, 100);
	sizer->Add(m_spinConfidenceThreshold, 1, wxALL | wxEXPAND, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Maximal Roll:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);

	m_spinMaxRoll = new wxSpinCtrl(tabPage, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, 16384|wxALIGN_RIGHT, 0, 180);
	sizer->Add(m_spinMaxRoll, 0, wxALL | wxEXPAND, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Maximal Yaw:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);

	m_spinMaxYaw = new wxSpinCtrl(tabPage, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, 16384|wxALIGN_RIGHT, 0, 90);
	sizer->Add(m_spinMaxYaw, 0, wxALL | wxEXPAND, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Quality threshold:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);

	m_spinQualityThreshold = new wxSpinCtrl(tabPage, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, 16384|wxALIGN_RIGHT, 0, 100);
	sizer->Add(m_spinQualityThreshold, 0, wxALL | wxEXPAND, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Liveness Mode:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);

	m_choiceLivenessMode = new wxChoice(tabPage, wxID_ANY, wxDefaultPosition, wxDefaultSize);
	NArrayWrapper<NInt> livenessValues = NEnum::GetValues(NBiometricTypes::NLivenessModeNativeTypeOf());
	for (NArrayWrapper<NInt>::iterator it = livenessValues.begin(); it != livenessValues.end(); it++)
	{
		m_choiceLivenessMode->Append(NEnum::ToString(NBiometricTypes::NLivenessModeNativeTypeOf(), *it));
	}
	sizer->Add(m_choiceLivenessMode, 0, wxALL | wxEXPAND, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Liveness threshold:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT | wxALL, 5);

	m_spinLivenessThreshold = new wxSpinCtrl(tabPage, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, 16384|wxALIGN_RIGHT, 0, 100);
	sizer->Add(m_spinLivenessThreshold, 0, wxALL | wxEXPAND, 5);

	m_chbDetectAllFeatures = new wxCheckBox(tabPage, wxID_ANY, wxT("Detect all feature points"));
	m_chbDetectAllFeatures->Enable(isActivated);
	sizer->Add(m_chbDetectAllFeatures, 0, wxALL | wxEXPAND, 5);
	sizer->AddSpacer(1);

	m_chbDetectBaseFeatures = new wxCheckBox(tabPage, wxID_ANY, wxT("Detect base feature points"));
	m_chbDetectBaseFeatures->Enable(isActivated);
	sizer->Add(m_chbDetectBaseFeatures, 0, wxALL | wxEXPAND, 5);
	sizer->AddSpacer(1);

	m_chbDetermineGender = new wxCheckBox(tabPage, wxID_ANY, wxT("Determine gender"));
	m_chbDetermineGender->Enable(isActivated);
	sizer->Add(m_chbDetermineGender, 0, wxALL | wxEXPAND, 5);
	sizer->AddSpacer(1);

	m_chbDetermineAge = new wxCheckBox(tabPage, wxID_ANY, wxT("Determine age"));
	m_chbDetermineAge->Enable(isActivated);
	sizer->Add(m_chbDetermineAge, 0, wxALL | wxEXPAND, 5);
	sizer->AddSpacer(1);

	m_chbDetectProperties = new wxCheckBox(tabPage, wxID_ANY, wxT("Detect properties"));
	m_chbDetectProperties->Enable(isActivated);
	sizer->Add(m_chbDetectProperties, 0, wxALL | wxEXPAND, 5);
	sizer->AddSpacer(1);

	m_chbRecognizeEmotion = new wxCheckBox(tabPage, wxID_ANY, wxT("Recognize emotion"));
	m_chbRecognizeEmotion->Enable(isActivated);
	sizer->Add(m_chbRecognizeEmotion, 0, wxALL | wxEXPAND, 5);
	sizer->AddSpacer(1);

	m_chbRecognizeExpression = new wxCheckBox(tabPage, wxID_ANY, wxT("Recognize expression"));
	m_chbRecognizeExpression->Enable(isActivated);
	sizer->Add(m_chbRecognizeExpression, 0, wxALL | wxEXPAND, 5);
	sizer->AddSpacer(1);

	m_chbCreateThumbnail = new wxCheckBox(tabPage, wxID_ANY, wxT("Create thumbnail image"));
	sizer->Add(m_chbCreateThumbnail, 0, wxALL | wxEXPAND, 5);
	sizer->AddSpacer(1);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Thumbnail image width:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT | wxALL, 5);

	m_spinThumbnailWidth = new wxSpinCtrl(tabPage, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, 16384|wxALIGN_RIGHT, 30, 10000);
	sizer->Add(m_spinThumbnailWidth, 0, wxALL | wxEXPAND, 5);

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

	staticBoxSizer = new wxStaticBoxSizer(new wxStaticBox(tabPage, wxID_ANY, wxT("Matching")), wxVERTICAL);
	boxSizer->Add(staticBoxSizer, 0, wxEXPAND, 5);
	sizer = new wxFlexGridSizer(0, 2, 0, 0);
	staticBoxSizer->Add(sizer, 0, wxEXPAND, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Matching speed:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);

	wxString speed [] = { wxT("High"), wxT("Medium"), wxT("Low") };
	m_comboMatchingSpeed = new wxComboBox(tabPage, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, 3, speed, wxCB_DROPDOWN | wxCB_READONLY);
	sizer->Add(m_comboMatchingSpeed, 0, wxALL | wxEXPAND, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Matching threshold:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);

	wxArrayString values;
	values.Add(wxT("0.1%"));
	values.Add(wxT("0.01%"));
	values.Add(wxT("0.001%"));
	values.Add(wxT("0.0001%"));
	m_comboMatchingThreshold = new wxComboBox(tabPage, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, values);
	sizer->Add(m_comboMatchingThreshold, 0, wxALL | wxEXPAND, 5);

	staticText = new wxStaticText(tabPage, wxID_ANY, wxT("Maximal results count:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	sizer->Add(staticText, 0, wxALIGN_RIGHT|wxALL, 5);

	m_spinMaxResults = new wxSpinCtrl(tabPage, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, 16384|wxALIGN_RIGHT, 0, wxINT32_MAX);
	sizer->Add(m_spinMaxResults, 1, wxALL | wxEXPAND, 5);

	m_chbFirstResultOnly = new wxCheckBox(tabPage, wxID_ANY, wxT("First result only"));
	sizer->Add(m_chbFirstResultOnly, 0, wxALL | wxEXPAND, 5);
	sizer->AddSpacer(1);

	tabPage->SetScrollRate(5, 5);
	tabPage->Layout();

	return tabPage;
}

void OptionsDlg::OnDefault(wxCommandEvent &/*event*/)
{
	if (m_biometricClient.GetHandle())
	{
		DefaultOptions();
		LoadOptions();
	}
}

void OptionsDlg::OnClose(wxCloseEvent &/*event*/)
{
	Destroy();
}

void OptionsDlg::OnOK(wxCommandEvent &/*event*/)
{
	try
	{
		SaveOptions();
		EndModal(wxID_OK);
	}
	catch(NError& ex)
	{
		wxExceptionDlg::Show(ex);
	}
}

void OptionsDlg::OnCheckBoxClicked(wxCommandEvent& /*event*/)
{
	EnableControls();
}

void OptionsDlg::EnableControls()
{
	NLivenessMode mode = (NLivenessMode)NEnum::Parse(NBiometricTypes::NLivenessModeNativeTypeOf(), m_choiceLivenessMode->GetStringSelection());
	m_biometricClient.SetFacesLivenessMode(mode);
	m_spinLivenessThreshold->Enable(mode != nlmNone);

	bool createThumbnail = m_chbCreateThumbnail->GetValue();
	m_spinThumbnailWidth->Enable(createThumbnail);
}

void OptionsDlg::SetBiometricClient( ::Neurotec::Biometrics::Client::NBiometricClient biometricClient)
{
	m_biometricClient = biometricClient;
	LoadOptions();
}

::Neurotec::Biometrics::Client::NBiometricClient OptionsDlg::GetBiometricClient()
{
	return m_biometricClient;
}

double OptionsDlg::MatchingThresholdToFAR(int th)
{
	if(th < 0) th = 0;
	return pow(10.0, -th / FARLogRatio);
}

wxString OptionsDlg::MatchingThresholdToFARString(int matchingThreshold)
{
	double value = MatchingThresholdToFAR(matchingThreshold);
	value = value * 100.0;
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

void OptionsDlg::LoadOptions()
{
	if (m_biometricClient.GetHandle())
	{
		m_comboMatchingThreshold->SetValue(MatchingThresholdToFARString(m_biometricClient.GetMatchingThreshold()));
		m_spinMaxResults->SetValue(m_biometricClient.GetMatchingMaximalResultCount());
		m_chbFirstResultOnly->SetValue(m_biometricClient.GetFirstResultOnly());
		
		NTemplateSize size = m_biometricClient.GetFacesTemplateSize();
		wxString sizeString = NEnum::ToString(NBiometricEngineTypes::NTemplateSizeNativeTypeOf(), size);
		m_comboTemplateSize->SetValue(sizeString);

		NMatchingSpeed speed = m_biometricClient.GetFacesMatchingSpeed();
		wxString speedString = NEnum::ToString(NBiometricEngineTypes::NMatchingSpeedNativeTypeOf(), speed);
		m_comboMatchingSpeed->SetValue(speedString);

		m_spinMinIOD->SetValue(m_biometricClient.GetFacesMinimalInterOcularDistance());
		m_spinConfidenceThreshold->SetValue(m_biometricClient.GetFacesConfidenceThreshold());
		m_spinMaxRoll->SetValue(m_biometricClient.GetFacesMaximalRoll());
		m_spinMaxYaw->SetValue(m_biometricClient.GetFacesMaximalYaw());
		m_spinQualityThreshold->SetValue(m_biometricClient.GetFacesQualityThreshold());
		m_choiceLivenessMode->SetStringSelection(NEnum::ToString(NBiometricTypes::NLivenessModeNativeTypeOf(), m_biometricClient.GetFacesLivenessMode()));
		m_spinLivenessThreshold->SetValue(m_biometricClient.GetFacesLivenessThreshold());
		m_chbDetectAllFeatures->SetValue(m_biometricClient.GetFacesDetectAllFeaturePoints() && m_chbDetectAllFeatures->IsEnabled());
		m_chbDetectBaseFeatures->SetValue(m_biometricClient.GetFacesDetectBaseFeaturePoints() && m_chbDetectBaseFeatures->IsEnabled());
		m_chbDetermineGender->SetValue(m_biometricClient.GetFacesDetermineGender() && m_chbDetermineGender->IsEnabled());
		m_chbDetermineAge->SetValue(m_biometricClient.GetFacesDetermineAge() && m_chbDetermineAge->IsEnabled());
		m_chbDetectProperties->SetValue(m_biometricClient.GetFacesDetectProperties() && m_chbDetectProperties->IsEnabled());
		m_chbRecognizeEmotion->SetValue(m_biometricClient.GetFacesRecognizeEmotion() && m_chbRecognizeEmotion->IsEnabled());
		m_chbRecognizeExpression->SetValue(m_biometricClient.GetFacesRecognizeExpression() && m_chbRecognizeExpression->IsEnabled());
		m_chbCreateThumbnail->SetValue(m_biometricClient.GetFacesCreateThumbnailImage());
		m_spinThumbnailWidth->SetValue(m_biometricClient.GetFacesThumbnailImageWidth());
	}
	EnableControls();
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

int OptionsDlg::FARToMatchingThreshold(double f)
{
	if(f > 1) f = 1;
	else if(f <= 0.0) f = 1E-100;
	return (int)wxRound(-log10(f) * FARLogRatio);
}

void OptionsDlg::SaveOptions()
{
	if (m_biometricClient.GetHandle())
	{
		int matchingThreshold = FARStringToMatchingThreshold(m_comboMatchingThreshold->GetValue());
		if (matchingThreshold < 0)
		{
			m_comboMatchingThreshold->SetValue(MatchingThresholdToFARString(m_biometricClient.GetMatchingThreshold()));
			LoadOptions();
			throw wxSampleException(wxT("Matching threshold value is invalid. Reseting to initial value"));
		}
		else
		{
			m_biometricClient.SetMatchingThreshold(matchingThreshold);
		}

		m_biometricClient.SetMatchingMaximalResultCount(m_spinMaxResults->GetValue());
		m_biometricClient.SetFirstResultOnly(m_chbFirstResultOnly->GetValue());

		NTemplateSize size = (NTemplateSize)NEnum::Parse(NBiometricEngineTypes::NTemplateSizeNativeTypeOf(), m_comboTemplateSize->GetValue());
		m_biometricClient.SetFacesTemplateSize(size);

		NMatchingSpeed speed = (NMatchingSpeed)NEnum::Parse(NBiometricEngineTypes::NMatchingSpeedNativeTypeOf(), m_comboMatchingSpeed->GetValue());
		m_biometricClient.SetFacesMatchingSpeed(speed);

		m_biometricClient.SetFacesMinimalInterOcularDistance(m_spinMinIOD->GetValue());
		m_biometricClient.SetFacesConfidenceThreshold(m_spinConfidenceThreshold->GetValue());
		m_biometricClient.SetFacesMaximalRoll(m_spinMaxRoll->GetValue());
		m_biometricClient.SetFacesMaximalYaw(m_spinMaxYaw->GetValue());
		m_biometricClient.SetFacesQualityThreshold(m_spinQualityThreshold->GetValue());
		m_biometricClient.SetFacesLivenessMode((NLivenessMode)NEnum::Parse(NBiometricTypes::NLivenessModeNativeTypeOf(), m_choiceLivenessMode->GetStringSelection()));
		m_biometricClient.SetFacesLivenessThreshold(m_spinLivenessThreshold->GetValue());
		m_biometricClient.SetFacesDetectAllFeaturePoints(m_chbDetectAllFeatures->GetValue());
		m_biometricClient.SetFacesDetectBaseFeaturePoints(m_chbDetectBaseFeatures->GetValue());
		m_biometricClient.SetFacesDetermineGender(m_chbDetermineGender->GetValue());
		m_biometricClient.SetFacesDetermineAge(m_chbDetermineAge->GetValue());
		m_biometricClient.SetFacesDetectProperties(m_chbDetectProperties->GetValue());
		m_biometricClient.SetFacesRecognizeEmotion(m_chbRecognizeEmotion->GetValue());
		m_biometricClient.SetFacesRecognizeExpression(m_chbRecognizeExpression->GetValue());
		m_biometricClient.SetFacesCreateThumbnailImage(m_chbCreateThumbnail->GetValue());
		m_biometricClient.SetFacesThumbnailWidth(m_spinThumbnailWidth->GetValue());
	}
}

void OptionsDlg::DefaultOptions()
{
	if (!m_biometricClient.IsNull())
	{
		m_biometricClient.ResetProperty(wxT("Faces.TemplateSize"));
		m_biometricClient.ResetProperty(wxT("Faces.MatchingSpeed"));
		m_biometricClient.ResetProperty(wxT("Faces.MinimalInterOcularDistance"));
		m_biometricClient.ResetProperty(wxT("Faces.ConfidenceThreshold"));
		m_biometricClient.ResetProperty(wxT("Faces.MaximalRoll"));
		m_biometricClient.ResetProperty(wxT("Faces.MaximalYaw"));
		m_biometricClient.ResetProperty(wxT("Faces.QualityThreshold"));
		m_biometricClient.ResetProperty(wxT("Faces.LivenessMode"));
		m_biometricClient.ResetProperty(wxT("Faces.LivenessThreshold"));
		m_biometricClient.ResetProperty(wxT("Faces.DetectBaseFeaturePoints"));
		m_biometricClient.ResetProperty(wxT("Faces.CreateThumbnailImage"));
		m_biometricClient.ResetProperty(wxT("Faces.ThumbnailImageWidth"));
		m_biometricClient.ResetProperty(wxT("Matching.Threshold"));
		m_biometricClient.ResetProperty(wxT("Matching.MaximalResultCount"));
		m_biometricClient.ResetProperty(wxT("Matching.FirstResultOnly"));
		m_biometricClient.SetFacesDetectAllFeaturePoints(m_chbDetectAllFeatures->IsEnabled());
		m_biometricClient.SetFacesDetermineGender(m_chbDetermineGender->IsEnabled());
		m_biometricClient.SetFacesDetermineAge(m_chbDetermineAge->IsEnabled());
		m_biometricClient.SetFacesDetectProperties(m_chbDetectProperties->IsEnabled());
		m_biometricClient.SetFacesRecognizeExpression(m_chbRecognizeExpression->IsEnabled());
		m_biometricClient.SetFacesRecognizeEmotion(m_chbRecognizeEmotion->IsEnabled());
		m_biometricClient.SetFacesCreateThumbnailImage(true);
		LoadOptions();
	}
}

}}

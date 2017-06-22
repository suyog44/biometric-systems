#include "Precompiled.h"

#include <SubjectEditor/VoiceView.h>

#include <Settings/SettingsManager.h>

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;

namespace Neurotec { namespace Samples
{

VoiceView::VoiceView(wxWindow *parent, wxWindowID winid) : wxPanel(parent, winid)
{
	CreateGUIControls();
}

VoiceView::~VoiceView()
{
}

void VoiceView::SetVoice(NVoice voice)
{
	if (voice.IsNull())
		return;

	m_lblPhraseId->SetLabelText(wxString::Format(wxT("%d"), voice.GetPhraseId()));
	m_lblPhrase->SetLabelText(wxT("N/A"));

	int phrasesCount = 0;
	SettingsManager::Phrase * phrases = SettingsManager::GetPhrases(&phrasesCount);
	for (int i = 0; i < phrasesCount; i++)
	{
		if (phrases[i].GetId() == voice.GetPhraseId())
		{
			m_lblPhrase->SetLabelText(phrases[i].GetPhrase());
			break;
		}
	}
	if (phrases)
	{
		delete[] phrases;
		phrases = NULL;
	}

	if (voice.GetObjects().GetCount() > 0)
	{
		NSAttributes attributes = voice.GetObjects()[0];
		NByte quality = attributes.GetQuality();

		switch(quality)
		{
		case N_BIOMETRIC_QUALITY_FAILED:
			m_lblQuality->SetLabelText(wxT("Failed to determine quality"));
			break;
		case N_BIOMETRIC_QUALITY_UNKNOWN:
			m_lblQuality->SetLabelText(wxT("N/A"));
			break;
		default:
			m_lblQuality->SetLabelText(wxString::Format(wxT("%d"), quality));
			break;
		};

		NTimeSpan startTime = attributes.GetVoiceStart();
		NTimeSpan duration = attributes.GetVoiceDuration();

		bool hasTimespan = startTime != NTimeSpan(0, 0, 0, 0, 0) || duration != NTimeSpan(0, 0, 0, 0, 0);

		if (hasTimespan)
		{
			m_lblStart->SetLabelText(startTime.ToString());
			m_lblDuration->SetLabelText(duration.ToString());
		}
		else
		{
			m_lblDuration->SetLabelText(wxT("N/A"));
			m_lblStart->SetLabelText(wxT("N/A"));
		}
	}
	else
	{
		m_lblDuration->SetLabelText(wxT("N/A"));
		m_lblStart->SetLabelText(wxT("N/A"));
		m_lblQuality->SetLabelText(wxT("N/A"));
	}

	this->Layout();
}

void VoiceView::CreateGUIControls()
{
	wxBoxSizer *sizer = new wxBoxSizer(wxVERTICAL);
	this->SetSizer(sizer, true);

	wxFlexGridSizer *szFlexGrid = new wxFlexGridSizer(5, 2, 5, 5);
	sizer->Add(szFlexGrid, 1, wxALL);

	wxFont boldFont;
	boldFont.SetWeight(wxFONTWEIGHT_BOLD);
	boldFont.SetPointSize(10);

	wxStaticText *text = new wxStaticText(this, wxID_ANY, wxT("Phrase id:"), wxDefaultPosition, wxDefaultSize, 0);
	szFlexGrid->Add(text, 0, wxALIGN_RIGHT | wxALL);

	m_lblPhraseId = new wxStaticText(this, wxID_ANY, wxEmptyString);
	m_lblPhraseId->SetFont(boldFont);
	szFlexGrid->Add(m_lblPhraseId, 0, wxALL);

	text = new wxStaticText(this, wxID_ANY, wxT("Phrase:"), wxDefaultPosition, wxDefaultSize, 0);
	szFlexGrid->Add(text, 0, wxALIGN_RIGHT | wxALL);

	m_lblPhrase = new wxStaticText(this, wxID_ANY, wxEmptyString);
	m_lblPhrase->SetFont(boldFont);
	szFlexGrid->Add(m_lblPhrase, 0, wxALL);

	text = new wxStaticText(this, wxID_ANY, wxT("Quality:"), wxDefaultPosition, wxDefaultSize, 0);
	szFlexGrid->Add(text, 0, wxALIGN_RIGHT | wxALL);

	m_lblQuality = new wxStaticText(this, wxID_ANY, wxEmptyString);
	m_lblQuality->SetFont(boldFont);
	szFlexGrid->Add(m_lblQuality, 0, wxALL);

	text = new wxStaticText(this, wxID_ANY, wxT("Voice start:"), wxDefaultPosition, wxDefaultSize, 0);
	szFlexGrid->Add(text, 0, wxALIGN_RIGHT | wxALL);

	m_lblStart = new wxStaticText(this, wxID_ANY, wxEmptyString);
	m_lblStart->SetFont(boldFont);
	szFlexGrid->Add(m_lblStart, 0, wxALL);

	text = new wxStaticText(this, wxID_ANY, wxT("Voice duration:"), wxDefaultPosition, wxDefaultSize, 0);
	szFlexGrid->Add(text, 0, wxALIGN_RIGHT | wxALL);

	m_lblDuration = new wxStaticText(this, wxID_ANY, wxEmptyString);
	m_lblDuration->SetFont(boldFont);
	szFlexGrid->Add(m_lblDuration, 0, wxALL);

	this->Layout();
}

}}

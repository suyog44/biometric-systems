#include "Precompiled.h"

#include <Common/MatchingResultView.h>

using namespace Neurotec::Biometrics;

namespace Neurotec { namespace Samples
{

MatchingResultView::MatchingResultView(wxWindow *parent, wxWindowID winid) :
	wxPanel(parent, winid),
	m_matchingResult(NULL),
	m_matchingThreshold(0)
{
	m_linkEnabled = false;
	m_linkPressedCallback = NULL;
	m_callbackParam = NULL;
	m_isLinkActive = false;

	CreateGuiElements();
	RegisterGuiEvents();
}

MatchingResultView::~MatchingResultView()
{
	UnregisterGuiEvents();
}

void MatchingResultView::SetMatchingThreshold(int value)
{
	m_matchingThreshold = value;
}

void MatchingResultView::SetIsLinkActive(bool value)
{
	m_isLinkActive = value;
}

bool MatchingResultView::IsLinkActive()
{
	return m_isLinkActive;
}

void MatchingResultView::SetMatchingResult(NMatchingResult matchingResult)
{
	m_matchingResult = matchingResult;

	if (!matchingResult.IsNull())
	{
		m_lblLink->SetLabelText(wxString::Format(wxT("Matched with '%s', score = %d"), (wxString)m_matchingResult.GetId(), m_matchingResult.GetScore()));
		m_lblDetails->SetLabelText(MatchingResultToString(m_matchingResult));
	}

	this->Layout();
}

wxString MatchingResultView::MatchingResultToString(const ::Neurotec::Biometrics::NMatchingResult matchingResult)
{
	NMatchingDetails details = matchingResult.GetMatchingDetails();

	wxString result = wxEmptyString;

	if (details.IsNull())
	{
		return result;
	}

	int index;
	int score;
	wxString bellowThreshold = wxT(" (Below matching threshold)");
	if ((details.GetBiometricType() & nbtFace) == nbtFace)
	{
		result += wxString::Format(wxT("Face match details: score = %d\n"), details.GetFacesScore());

		NMatchingDetails::FaceCollection faces = details.GetFaces();
		for (int i = 0; i < faces.GetCount(); i++)
		{
			NLMatchingDetails faceDetails = faces[i];
			index = faceDetails.GetMatchedIndex();
			if (index != -1)
			{
				score = faceDetails.GetScore();
				result += wxString::Format(wxT("    face index %d: matched with index %d, score = %d%s\n"), i, index, score, score < m_matchingThreshold ? bellowThreshold : (wxString)wxEmptyString);
			}
			else
			{
				result += wxString::Format(wxT("    face index %d: doesn't match\n"), i);
			}
		}
	}

	if ((details.GetBiometricType() & nbtFinger) == nbtFinger)
	{
		result += wxString::Format(wxT("Fingerprint match details: score = %d\n"), details.GetFingersScore());

		NMatchingDetails::FingerCollection fingers = details.GetFingers();
		for (int i = 0; i < fingers.GetCount(); i++)
		{
			NFMatchingDetails fingerDetails = fingers[i];
			index = fingerDetails.GetMatchedIndex();
			if (index != -1)
			{
				score = fingerDetails.GetScore();
				result += wxString::Format(wxT("    fingerprint index %d: matched with index %d, score = %d%s\n"), i, index, score, score < m_matchingThreshold ? bellowThreshold : (wxString)wxEmptyString);
			}
			else
			{
				result += wxString::Format(wxT("    fingerprint index %d: doesn't match\n"), i);
			}
		}
	}

	if ((details.GetBiometricType() & nbtIris) == nbtIris)
	{
		result += wxString::Format(wxT("Irises match details: score = %d\n"), details.GetIrisesScore());

		NMatchingDetails::IrisCollection irises = details.GetIrises();
		for (int i = 0; i < irises.GetCount(); i++)
		{
			NEMatchingDetails irisesDetails = irises[i];
			index = irisesDetails.GetMatchedIndex();
			if (index != -1)
			{
				score = irisesDetails.GetScore();
				result += wxString::Format(wxT("    irises index %d: matched with index %d, score = %d%s\n"), i, index, score, score < m_matchingThreshold ? bellowThreshold : (wxString)wxEmptyString);
			}
			else
			{
				result += wxString::Format(wxT("    irises index %d: doesn't match\n"), i);
			}
		}
	}

	if ((details.GetBiometricType() & nbtPalm) == nbtPalm)
	{
		result += wxString::Format(wxT("Palmprint match details: score = %d\n"), details.GetPalmsScore());

		NMatchingDetails::PalmCollection palms = details.GetPalms();
		for (int i = 0; i < palms.GetCount(); i++)
		{
			NFMatchingDetails palmsDetails = palms[i];
			index = palmsDetails.GetMatchedIndex();
			if (index != -1)
			{
				score = palmsDetails.GetScore();
				result += wxString::Format(wxT("    palmprint index %d: matched with index %d, score = %d%s\n"), i, index, score, score < m_matchingThreshold ? bellowThreshold : (wxString)wxEmptyString);
			}
			else
			{
				result += wxString::Format(wxT("    palmprint index %d: doesn't match\n"), i);
			}
		}
	}

	if ((details.GetBiometricType() & nbtVoice) == nbtVoice)
	{
		result += wxString::Format(wxT("Voice match details: score = %d\n"), details.GetVoicesScore());

		NMatchingDetails::VoiceCollection voices = details.GetVoices();
		for (int i = 0; i < voices.GetCount(); i++)
		{
			NSMatchingDetails voiceDetails = voices[i];
			index = voiceDetails.GetMatchedIndex();
			if (index != -1)
			{
				score = voiceDetails.GetScore();
				result += wxString::Format(wxT("    voices index %d: matched with index %d, score = %d%s\n"), i, index, score, score < m_matchingThreshold ? bellowThreshold : (wxString)wxEmptyString);
			}
			else
			{
				result += wxString::Format(wxT("    voices index %d: doesn't match\n"), i);
			}
		}
	}

	if ((details.GetBiometricType() & (nbtFinger | nbtPalm | nbtIris | nbtVoice | nbtFace)) == nbtNone)
	{
		result += wxString::Format(wxT(" score = %d"), details.GetScore());
	}

	return result;
}

void MatchingResultView::OnHyperlinkClick(wxHyperlinkEvent&)
{
	if (!m_isLinkActive)
	{
		return;
	}

	if (m_linkPressedCallback != NULL)
	{
		(*m_linkPressedCallback)(m_matchingResult, m_callbackParam);
	}
}

void MatchingResultView::SetLinkPressedCallback(MatchingResultView::LinkPressedCallback callback, void *param)
{
	m_linkPressedCallback = callback;
	m_callbackParam = param;
}

void MatchingResultView::RegisterGuiEvents()
{
	m_lblLink->Connect(wxEVT_COMMAND_HYPERLINK, wxHyperlinkEventHandler(MatchingResultView::OnHyperlinkClick), NULL, this);
}

void MatchingResultView::UnregisterGuiEvents()
{
	m_lblLink->Disconnect(wxEVT_COMMAND_HYPERLINK, wxHyperlinkEventHandler(MatchingResultView::OnHyperlinkClick), NULL, this);
}

void MatchingResultView::CreateGuiElements()
{
	wxBoxSizer *sizer = new wxBoxSizer(wxVERTICAL);
	this->SetSizer(sizer);

	m_lblLink = new wxHyperlinkCtrl(this, wxID_ANY, wxEmptyString, wxT("#"), wxDefaultPosition, wxDefaultSize, wxHL_DEFAULT_STYLE);
	m_lblLink->SetToolTip(wxEmptyString);
	sizer->Add(m_lblLink, 0, wxALL);

	m_lblDetails = new wxStaticText(this, wxID_ANY, wxEmptyString);
	sizer->Add(m_lblDetails, 0, wxLEFT, 10);

	this->Layout();
}

}}


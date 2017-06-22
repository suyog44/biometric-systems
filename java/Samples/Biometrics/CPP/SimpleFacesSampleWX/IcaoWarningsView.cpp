#include "Precompiled.h"
#include "IcaoWarningsView.h"

using namespace Neurotec;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Collections;

namespace Neurotec { namespace Samples {

DEFINE_EVENT_TYPE(EVT_ICAO_WARNINGS);
BEGIN_EVENT_TABLE(IcaoWarningsView, wxPanel)
	EVT_COMMAND(wxID_ANY, EVT_ICAO_WARNINGS, IcaoWarningsView::OnEvent)
END_EVENT_TABLE()

IcaoWarningsView::IcaoWarningsView(wxWindow* parent, wxWindowID id, const wxPoint& pos, const wxSize& size, long style)
	: wxPanel(parent, id, pos, size, style),
	m_face(NULL),
	m_attributes(NULL),
	m_noWarningsColor(0x00, 0x64, 0x00),
	m_warningsColor(*wxRED),
	m_indeterminateColor(0xFF, 0xA5, 0x00)
{
	wxBoxSizer* bSizer1;
	bSizer1 = new wxBoxSizer( wxVERTICAL );

	m_textFaceDetected = new wxStaticText( this, wxID_ANY, wxT("Face Detected"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textFaceDetected->Wrap( -1 );
	m_textFaceDetected->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textFaceDetected, 0, wxALL, 2 );

	m_textExpression = new wxStaticText( this, wxID_ANY, wxT("Expression"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textExpression->Wrap( -1 );
	m_textExpression->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textExpression, 0, wxALL, 2 );

	m_textDarkGlasses = new wxStaticText( this, wxID_ANY, wxT("Dark Glasses"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textDarkGlasses->Wrap( -1 );
	m_textDarkGlasses->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textDarkGlasses, 0, wxALL, 2 );

	m_textBlink = new wxStaticText( this, wxID_ANY, wxT("Blink"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textBlink->Wrap( -1 );
	m_textBlink->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textBlink, 0, wxALL, 2 );

	m_textMouthOpen = new wxStaticText( this, wxID_ANY, wxT("Mouth Open"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textMouthOpen->Wrap( -1 );
	m_textMouthOpen->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textMouthOpen, 0, wxALL, 2 );

	m_textRoll = new wxStaticText( this, wxID_ANY, wxT("Roll"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textRoll->Wrap( -1 );
	m_textRoll->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textRoll, 0, wxALL, 2 );

	m_textYaw = new wxStaticText( this, wxID_ANY, wxT("Yaw"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textYaw->Wrap( -1 );
	m_textYaw->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textYaw, 0, wxALL, 2 );

	m_textPitch = new wxStaticText( this, wxID_ANY, wxT("Pitch"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textPitch->Wrap( -1 );
	m_textPitch->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textPitch, 0, wxALL, 2 );

	m_textTooClose = new wxStaticText( this, wxID_ANY, wxT("Too Close"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textTooClose->Wrap( -1 );
	m_textTooClose->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textTooClose, 0, wxALL, 2 );

	m_textTooFar = new wxStaticText( this, wxID_ANY, wxT("Too Far"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textTooFar->Wrap( -1 );
	m_textTooFar->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textTooFar, 0, wxALL, 2 );

	m_textTooNorth = new wxStaticText( this, wxID_ANY, wxT("Too North"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textTooNorth->Wrap( -1 );
	m_textTooNorth->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textTooNorth, 0, wxALL, 2 );

	m_textTooSouth = new wxStaticText( this, wxID_ANY, wxT("Too South"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textTooSouth->Wrap( -1 );
	m_textTooSouth->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textTooSouth, 0, wxALL, 2 );

	m_textTooWest = new wxStaticText( this, wxID_ANY, wxT("Too West"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textTooWest->Wrap( -1 );
	m_textTooWest->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textTooWest, 0, wxALL, 2 );

	m_textTooEast = new wxStaticText( this, wxID_ANY, wxT("Too East"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textTooEast->Wrap( -1 );
	m_textTooEast->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textTooEast, 0, wxALL, 2 );

	m_textSharpness = new wxStaticText( this, wxID_ANY, wxT("Sharpness"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textSharpness->Wrap( -1 );
	m_textSharpness->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textSharpness, 0, wxALL, 2 );

	m_textGrayscaleDensity = new wxStaticText( this, wxID_ANY, wxT("Grayscale Density"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textGrayscaleDensity->Wrap( -1 );
	m_textGrayscaleDensity->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textGrayscaleDensity, 0, wxALL, 2 );

	m_textSaturation = new wxStaticText( this, wxID_ANY, wxT("Saturation"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textSaturation->Wrap( -1 );
	m_textSaturation->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textSaturation, 0, wxALL, 2 );

	m_textBackgroundUni = new wxStaticText( this, wxID_ANY, wxT("Background Unitformity"), wxDefaultPosition, wxDefaultSize, 0 );
	m_textBackgroundUni->Wrap( -1 );
	m_textBackgroundUni->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );

	bSizer1->Add( m_textBackgroundUni, 0, wxALL, 2 );

	this->SetSizer( bSizer1 );
	this->Layout();
}

IcaoWarningsView::~IcaoWarningsView()
{
	SetFace(NULL);
}

void IcaoWarningsView::SetFace(const NFace & face)
{
	if (face != m_face)
	{
		UnsubscribeFromFaceEvents();
		m_face = face;
		SubscribeToFaceEvents();
		UpdateUI();
		Refresh();
	}
}

void IcaoWarningsView::SetNoWarningsColor(const wxColour & value)
{
	m_noWarningsColor = value;
}

void IcaoWarningsView::SetWarningColor(const wxColour & value)
{
	m_warningsColor = value;
}

void IcaoWarningsView::SetIndeterminateColor(const wxColour & value)
{
	m_indeterminateColor = value;
}

std::vector<wxStaticText*> IcaoWarningsView::GetLabels() const
{
	std::vector<wxStaticText*> result;
	result.push_back(m_textFaceDetected);
	result.push_back(m_textExpression);
	result.push_back(m_textDarkGlasses);
	result.push_back(m_textBlink);
	result.push_back(m_textMouthOpen);
	result.push_back(m_textRoll);
	result.push_back(m_textYaw);
	result.push_back(m_textPitch);
	result.push_back(m_textTooClose);
	result.push_back(m_textTooFar);
	result.push_back(m_textTooNorth);
	result.push_back(m_textTooSouth);
	result.push_back(m_textTooWest);
	result.push_back(m_textTooEast);
	result.push_back(m_textSharpness);
	result.push_back(m_textGrayscaleDensity);
	result.push_back(m_textSaturation);
	result.push_back(m_textBackgroundUni);
	return result;
}

wxColor IcaoWarningsView::GetColorForConfidence(NIcaoWarnings warnings, NIcaoWarnings flag, NByte confidence) const
{
	if ((warnings & flag) == flag)
	{
		return confidence <= 100 ? m_warningsColor : m_indeterminateColor;
	}
	return m_noWarningsColor;
}

wxColour IcaoWarningsView::GetColorForFlags(NIcaoWarnings warnings, NIcaoWarnings flag) const
{
	return (warnings & flag) == flag ? m_warningsColor : m_noWarningsColor;
}

wxColour IcaoWarningsView::GetColorForFlags(NIcaoWarnings warnings, NIcaoWarnings flag1, NIcaoWarnings flag2) const
{
	return (warnings & flag1) == flag1 || (warnings & flag2) == flag2 ? m_warningsColor : m_noWarningsColor;
}

wxString IcaoWarningsView::GetConfidenceString(const wxString & name, NByte value)
{
	wxString str = value <= 100 ? (wxString)NTypes::UInt8ToString(value) : wxString("N/A");
	return wxString::Format("%s: %s", name.c_str(), str.c_str());
}

void IcaoWarningsView::UpdateUI()
{
	if (!m_attributes.IsNull())
	{
		NIcaoWarnings warnings = m_attributes.GetIcaoWarnings();
		if ((warnings & niwFaceNotDetected) == niwFaceNotDetected)
		{
			std::vector<wxStaticText*> labels = GetLabels();
			for (std::vector<wxStaticText*>::iterator it = labels.begin(); it != labels.end(); it++)
			{
				(*it)->SetForegroundColour(m_indeterminateColor);
			}
		}
		else
		{
			m_textFaceDetected->SetForegroundColour(m_noWarningsColor);
			m_textExpression->SetForegroundColour(GetColorForConfidence(warnings, niwExpression, m_attributes.GetExpressionConfidence()));
			m_textDarkGlasses->SetForegroundColour(GetColorForConfidence(warnings, niwDarkGlasses, m_attributes.GetDarkGlassesConfidence()));
			m_textBlink->SetForegroundColour(GetColorForConfidence(warnings, niwBlink, m_attributes.GetBlinkConfidence()));
			m_textMouthOpen->SetForegroundColour(GetColorForConfidence(warnings, niwMouthOpen, m_attributes.GetMouthOpenConfidence()));
			m_textRoll->SetForegroundColour(GetColorForFlags(warnings, niwRollLeft, niwRollRight));
			m_textYaw->SetForegroundColour(GetColorForFlags(warnings, niwYawLeft, niwYawRight));
			m_textPitch->SetForegroundColour(GetColorForFlags(warnings, niwPitchDown, niwPitchUp));
			m_textTooClose->SetForegroundColour(GetColorForFlags(warnings, niwTooNear));
			m_textTooFar->SetForegroundColour(GetColorForFlags(warnings, niwTooFar));
			m_textTooNorth->SetForegroundColour(GetColorForFlags(warnings, niwTooNorth));
			m_textTooSouth->SetForegroundColour(GetColorForFlags(warnings, niwTooSouth));
			m_textTooWest->SetForegroundColour(GetColorForFlags(warnings, niwTooWest));
			m_textTooEast->SetForegroundColour(GetColorForFlags(warnings, niwTooEast));

			m_textSharpness->SetForegroundColour(GetColorForFlags(warnings, niwSharpness));
			m_textSharpness->SetLabel(GetConfidenceString("Sharpness", m_attributes.GetSharpness()));
			m_textSaturation->SetForegroundColour(GetColorForFlags(warnings, niwSaturation));
			m_textSaturation->SetLabel(GetConfidenceString("Saturation", m_attributes.GetSaturation()));
			m_textGrayscaleDensity->SetForegroundColour(GetColorForFlags(warnings, niwGrayscaleDensity));
			m_textGrayscaleDensity->SetLabel(GetConfidenceString("Grayscale Density", m_attributes.GetGrayscaleDensity()));
			m_textBackgroundUni->SetForegroundColour(GetColorForFlags(warnings, niwBackgroundUniformity));
			m_textBackgroundUni->SetLabel(GetConfidenceString("Background Uniformity", m_attributes.GetBackgroundUniformity()));
		}
	}
	else
	{
		std::vector<wxStaticText*> labels = GetLabels();
		for (std::vector<wxStaticText*>::iterator it = labels.begin(); it != labels.end(); it++)
		{
			(*it)->SetForegroundColour(m_indeterminateColor);
		}
	}
	Refresh();
}

void IcaoWarningsView::UnsubscribeFromFaceEvents()
{
	if (!m_face.IsNull())
	{
		m_face.GetObjects().RemoveCollectionChangedCallback(&IcaoWarningsView::OnCollectionChangedCallback, this);
	}
	if (!m_attributes.IsNull())
	{
		m_attributes.RemovePropertyChangedCallback(&IcaoWarningsView::OnAttributesPropertyChangedCallback, this);
	}
}

void IcaoWarningsView::SubscribeToFaceEvents()
{
	if (!m_face.IsNull())
	{
		m_face.GetObjects().AddCollectionChangedCallback(&IcaoWarningsView::OnCollectionChangedCallback, this);
		if (m_face.GetObjects().GetCount() != 0)
		{
			m_attributes = m_face.GetObjects()[0];
			m_attributes.AddPropertyChangedCallback(&IcaoWarningsView::OnAttributesPropertyChangedCallback, this);
		}
	}
}

void IcaoWarningsView::OnAttributesPropertyChangedCallback(NObject::PropertyChangedEventArgs args)
{
	if (args.GetPropertyName() == N_T("IcaoWarnings"))
	{
		IcaoWarningsView * view = static_cast<IcaoWarningsView*>(args.GetParam());
		wxCommandEvent evt(EVT_ICAO_WARNINGS, ID_UPDATE_WARNINGS);
		wxPostEvent(view, evt);
	}
}

void IcaoWarningsView::OnCollectionChangedCallback(CollectionChangedEventArgs<NLAttributes> args)
{
	IcaoWarningsView * view = static_cast<IcaoWarningsView*>(args.GetParam());
	wxCommandEvent evt(EVT_ICAO_WARNINGS, ID_UPDATE_ATTRIBUTES);
	wxPostEvent(view, evt);
}

void IcaoWarningsView::OnEvent(wxCommandEvent & event)
{
	int id = event.GetId();
	if (id == ID_UPDATE_ATTRIBUTES)
	{
		if (!m_face.IsNull())
		{
			NLAttributes newAttributes = NULL;
			if (m_face.GetObjects().GetCount() > 0)
			{
				newAttributes = m_face.GetObjects()[0];
			}
			if (newAttributes != m_attributes)
			{
				if (!m_attributes.IsNull())
				{
					m_attributes.RemovePropertyChangedCallback(&IcaoWarningsView::OnAttributesPropertyChangedCallback, this);
					m_attributes = NULL;
				}
				m_attributes = newAttributes;
				if (!m_attributes.IsNull())
				{
					m_attributes.AddPropertyChangedCallback(&IcaoWarningsView::OnAttributesPropertyChangedCallback, this);
				}
			}
		}
	}
	UpdateUI();
}

}}

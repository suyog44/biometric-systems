#ifndef ICAO_WARNINGS_VIEW_H_INCLUDED
#define ICAO_WARNINGS_VIEW_H_INCLUDED

namespace Neurotec { namespace Samples {

class IcaoWarningsView: public wxPanel
{
private:
		wxStaticText* m_textFaceDetected;
		wxStaticText* m_textExpression;
		wxStaticText* m_textDarkGlasses;
		wxStaticText* m_textBlink;
		wxStaticText* m_textMouthOpen;
		wxStaticText* m_textRoll;
		wxStaticText* m_textYaw;
		wxStaticText* m_textPitch;
		wxStaticText* m_textTooClose;
		wxStaticText* m_textTooFar;
		wxStaticText* m_textTooNorth;
		wxStaticText* m_textTooSouth;
		wxStaticText* m_textTooWest;
		wxStaticText* m_textTooEast;
		wxStaticText* m_textSharpness;
		wxStaticText* m_textGrayscaleDensity;
		wxStaticText* m_textSaturation;
		wxStaticText* m_textBackgroundUni;

private:
	::Neurotec::Biometrics::NFace m_face;
	::Neurotec::Biometrics::NLAttributes m_attributes;
	wxColour m_noWarningsColor;
	wxColour m_warningsColor;
	wxColour m_indeterminateColor;

private:
	std::vector<wxStaticText*> GetLabels() const;
	wxColour GetColorForConfidence(::Neurotec::Biometrics::NIcaoWarnings warnings, ::Neurotec::Biometrics::NIcaoWarnings flag, ::Neurotec::NByte confidence) const;
	wxColour GetColorForFlags(::Neurotec::Biometrics::NIcaoWarnings warnings, ::Neurotec::Biometrics::NIcaoWarnings flag) const;
	wxColour GetColorForFlags(::Neurotec::Biometrics::NIcaoWarnings warnings, ::Neurotec::Biometrics::NIcaoWarnings flag1, ::Neurotec::Biometrics::NIcaoWarnings flag2) const;
	static wxString GetConfidenceString(const wxString & name, ::Neurotec::NByte value);
	void UpdateUI();
	void UnsubscribeFromFaceEvents();
	void SubscribeToFaceEvents();

	static void OnAttributesPropertyChangedCallback(::Neurotec::NObject::PropertyChangedEventArgs args);
	static void OnCollectionChangedCallback(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Biometrics::NLAttributes> args);

	void OnEvent(wxCommandEvent & ev);

	enum
	{
		ID_UPDATE_WARNINGS,
		ID_UPDATE_ATTRIBUTES
	};

public:
	IcaoWarningsView(wxWindow* parent, wxWindowID id = wxID_ANY, const wxPoint& pos = wxDefaultPosition, const wxSize& size = wxSize( 191,368 ), long style = wxTAB_TRAVERSAL);
	~IcaoWarningsView();

	void SetFace(const ::Neurotec::Biometrics::NFace & face);
	void SetNoWarningsColor(const wxColour & value);
	void SetWarningColor(const wxColour & value);
	void SetIndeterminateColor(const wxColour & value);

	DECLARE_EVENT_TABLE();
};

}}

#endif

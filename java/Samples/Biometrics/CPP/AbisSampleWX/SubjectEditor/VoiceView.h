#ifndef VOICE_VIEW_H_INCLUDED
#define VOICE_VIEW_H_INCLUDED

namespace Neurotec { namespace Samples
{

class VoiceView : public wxPanel
{
public:
	VoiceView(wxWindow *parent, wxWindowID winid = wxID_ANY);

	virtual ~VoiceView();

	void SetVoice(::Neurotec::Biometrics::NVoice voice);

private:
	void CreateGUIControls();

private:
	wxStaticText *m_lblPhraseId;
	wxStaticText *m_lblPhrase;
	wxStaticText *m_lblQuality;
	wxStaticText *m_lblStart;
	wxStaticText *m_lblDuration;
};

}}

#endif


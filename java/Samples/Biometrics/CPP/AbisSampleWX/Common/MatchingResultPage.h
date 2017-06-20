#ifndef MATCHING_RESULT_PAGE_H_INCLUDED
#define MATCHING_RESULT_PAGE_H_INCLUDED

#include <Common/ImageView.h>
#include <Common/TabPage.h>
#include <Common/ImageView.h>
#include <Common/SchemaPropertyGridCtrl.h>

#include <SubjectEditor/VoiceView.h>

namespace Neurotec { namespace Samples
{

class MatchingResultPage : public TabPage
{
public:
	MatchingResultPage(wxWindow *parent, wxWindowID winid, ::Neurotec::Biometrics::Client::NBiometricClient& biometricClient);

	virtual ~MatchingResultPage();
	void SetParameters(::Neurotec::Biometrics::NSubject probeSubject, ::Neurotec::Biometrics::NSubject galerySubject);

private:
	::Neurotec::Biometrics::Gui::wxNFingerView * ShowFinger(::Neurotec::Biometrics::NFrictionRidge target, wxStaticText *label);
	::Neurotec::Biometrics::Gui::wxNIrisView * ShowIris(::Neurotec::Biometrics::NIris target, wxStaticText *label);
	::Neurotec::Biometrics::Gui::wxNFaceView * ShowFace(::Neurotec::Biometrics::NFace target, wxStaticText *label);
	VoiceView * ShowVoice(::Neurotec::Biometrics::NVoice target, wxStaticText *label);

private:
	virtual void OnSelectPage();
	void OnThread(wxCommandEvent &event);
	void OnBiometricSelect(wxCommandEvent& event);
	void RegisterGuiEvents();
	void UnregisterGuiEvents();
	void CreateGuiElements();

	static void OnGetCompleted(::Neurotec::EventArgs args);
	static int RecordIndexToFaceIndex(int index, wxArrayInt & recordCounts);

private:
	enum {
		ID_EVENT_GET_COMPLETED
	};

	template <class T1, class T2>
	class MatchedPair
	{
	public:
		MatchedPair(T1 first, T1 second, T2 details);
		virtual ~MatchedPair();

		void SetFirst(T1 first);
		void SetSecond(T1 second);
		void SetDetails(T2 details);
		virtual wxString GetType();
		T1 GetFirst();
		T1 GetSecond();
		T2 GetDetails();
	protected:
		wxString m_type;
	private:
		T1 m_first;
		T1 m_second;
		T2 m_details;
	};

	class MatchedFinger : public MatchedPair<Neurotec::Biometrics::NFrictionRidge, Neurotec::Biometrics::NFMatchingDetails>
	{
	public:
		MatchedFinger(::Neurotec::Biometrics::NFrictionRidge f1, ::Neurotec::Biometrics::NFrictionRidge f2, ::Neurotec::Biometrics::NFMatchingDetails dt);
		virtual wxString ToString();
	};

	class MatchedIris : public MatchedPair<Neurotec::Biometrics::NIris, Neurotec::Biometrics::NEMatchingDetails>
	{
	public:
		MatchedIris(::Neurotec::Biometrics::NIris f1, ::Neurotec::Biometrics::NIris f2, ::Neurotec::Biometrics::NEMatchingDetails dt);
		virtual wxString ToString();
	};

	class MatchedFace : public MatchedPair<Neurotec::Biometrics::NFace, Neurotec::Biometrics::NLMatchingDetails>
	{
	public:
		MatchedFace(::Neurotec::Biometrics::NFace f1, ::Neurotec::Biometrics::NFace f2, ::Neurotec::Biometrics::NLMatchingDetails dt);
		virtual wxString ToString();
	};

	class MatchedVoice : public MatchedPair<Neurotec::Biometrics::NVoice, Neurotec::Biometrics::NSMatchingDetails>
	{
	public:
		MatchedVoice(::Neurotec::Biometrics::NVoice f1, ::Neurotec::Biometrics::NVoice f2, ::Neurotec::Biometrics::NSMatchingDetails dt);
		virtual wxString ToString();
	};

private:
	bool m_isSubjectLoaded;

	::Neurotec::Biometrics::Client::NBiometricClient& m_biometricClient;
	::Neurotec::Biometrics::NSubject m_probeSubject;
	::Neurotec::Biometrics::NSubject m_galerySubject;
	::Neurotec::Biometrics::NMatchingResult m_matchingResult;

	SchemaPropertyGrid * m_propertyGrid;
	wxStaticText * m_lblInfo;
	wxChoice * m_choiceBiometric;
	ImageView * m_thumbnail;
	wxStaticText * m_lblProbe;
	wxStaticText * m_lblGalery;
	wxBoxSizer * m_leftSizer;
	wxBoxSizer * m_rightSizer;
	wxWindow * m_leftWindow;
	wxWindow * m_rightWindow;
	wxStaticText * m_lblProbeTitle;
	wxStaticText * m_lblGaleryTitle;
	wxStaticText * m_lblMatchedBiometrics;
};

}}

#endif

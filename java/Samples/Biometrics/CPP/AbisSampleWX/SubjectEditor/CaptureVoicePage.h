#ifndef CAPTURE_VOICE_PAGE_H_INCLUDED
#define CAPTURE_VOICE_PAGE_H_INCLUDED

#include <Common/StatusPanel.h>
#include <Common/BusyIndicator.h>

#include <Settings/SettingsManager.h>

#include <SubjectEditor/ModalityPage.h>

namespace Neurotec { namespace Samples
{

class CaptureVoicePage : public ModalityPage
{

public:
	CaptureVoicePage(::Neurotec::Biometrics::Client::NBiometricClient& biometricClient, ::Neurotec::Biometrics::NSubject& subject,
		SubjectEditorPageInterface& subjectEditorPageInterface, wxWindow *parent, wxWindowID winid = wxID_ANY);

	virtual ~CaptureVoicePage();
	virtual void SetIsBusy(bool value);

	void OnNavigatedTo();
	void OnNavigatingFrom();

private:

	void LoadPhrases();
	void UpdateStatus(::Neurotec::Biometrics::NBiometricStatus status);
	void EnableControls();
	void DisableControls();

	void OnSourceChanged(wxCommandEvent& event);
	void OnEditClick(wxCommandEvent& event);
	void OnFinishClick(wxCommandEvent& event);
	void OnStartClick(wxCommandEvent& event);
	void OnStopClick(wxCommandEvent& event);
	void OnForceClick(wxCommandEvent& event);
	void OnOpenFileClick(wxCommandEvent& event);
	void OnThread(wxCommandEvent &event);

	void RegisterGuiEvents();
	void UnregisterGuiEvents();
	void CreateGUIControls();

	static void CaptureAsyncCompletedCallback(::Neurotec::EventArgs args);

private:
	enum
	{
		ID_EVENT_ENABLE_CONTROLS,
		ID_EVENT_CAPTURE_COMPLETED
	};

	wxChoice *m_choicePhraseId;
	wxButton *m_btnEdit;
	wxButton *m_btnStart;
	wxButton *m_btnStop;
	wxButton *m_btnForce;
	wxButton *m_btnOpenFile;
	wxButton *m_btnFinish;
	wxRadioButton *m_radioSoundFile;
	wxRadioButton *m_radioMicrophone;
	wxCheckBox *m_chbCaptureAutomatically;
	StatusPanel *m_statusPanel;
	BusyIndicator *m_busyIndicator;
	wxBoxSizer *m_statusSizer;
	::Neurotec::Biometrics::Gui::wxNVoiceView * m_voiceView;

	Neurotec::Biometrics::NSubject m_newSubject;
	Neurotec::Biometrics::NVoice m_voice;

	SettingsManager::Phrase *m_phrases;
	int m_phraseCount;
};

}}

#endif

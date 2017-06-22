#ifndef CAPTURE_IRISES_PAGE_H_INCLUDED
#define CAPTURE_IRISES_PAGE_H_INCLUDED

#include <Common/StatusPanel.h>
#include <Common/BusyIndicator.h>

#include <SubjectEditor/ModalityPage.h>

namespace Neurotec { namespace Samples
{

class CaptureIrisesPage : public ModalityPage
{

public:
	CaptureIrisesPage(::Neurotec::Biometrics::Client::NBiometricClient& biometricClient, ::Neurotec::Biometrics::NSubject& subject,
		SubjectEditorPageInterface& subjectEditorPageInterface, wxWindow *parent, wxWindowID winid = wxID_ANY);

	virtual ~CaptureIrisesPage();
	virtual void SetIsBusy(bool value);

	void OnNavigatedTo();
	void OnNavigatingFrom();

private:
	void SelectScannerSource();
	void SelectFileSource();
	bool ResetScanner();
	void UpdateStatus(::Neurotec::Biometrics::NBiometricStatus status);

	void OnSourceChange(wxCommandEvent& event);
	void OnCaptureClick(wxCommandEvent& event);
	void OnForceClick(wxCommandEvent& event);
	void OnCancelClick(wxCommandEvent& event);
	void OnFinishClick(wxCommandEvent& event);
	void OnThread(wxCommandEvent &event);

	void EnableControls();
	void DisableControls();

	void RegisterGuiEvents();
	void UnregisterGuiEvents();
	void CreateGUIControls();

	static void OnIrisPropertyChangedCallback(::Neurotec::NObject::PropertyChangedEventArgs args);
	static void OnBiometricClientPropertyChangedCallback(::Neurotec::NObject::PropertyChangedEventArgs args);
	static void CaptureAsyncCompletedCallback(::Neurotec::EventArgs args);

private:
	enum
	{
		ID_EVENT_CAPTURE_FINISHED,
		ID_EVENT_SCANNER_CHANGED
	};

	::Neurotec::Biometrics::NSubject m_newSubject;
	::Neurotec::Biometrics::NIris m_iris;

	::Neurotec::Biometrics::Gui::wxNIrisView *m_irisView;
	wxButton *m_btnOpenImage;
	wxButton *m_btnCapture;
	wxButton *m_btnCancel;
	wxButton *m_btnFinish;
	wxButton *m_btnForce;
	wxChoice *m_choicePosition;
	wxChoice *m_choiceScannerPosition;
	wxRadioButton *m_radioScanner;
	wxRadioButton *m_radioFile;
	wxCheckBox *m_chbCaptureAutomatically;
	StatusPanel *m_statusPanel;
	BusyIndicator *m_busyIndicator;
	wxBoxSizer *m_statusSizer;
	::Neurotec::Gui::wxNViewZoomSlider * m_zoomSlider;
};

}}

#endif


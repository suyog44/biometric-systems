#ifndef CAPTURE_FACE_PAGE_H_INCLUDED
#define CAPTURE_FACE_PAGE_H_INCLUDED

#include <Common/StatusPanel.h>
#include <Common/BusyIndicator.h>
#include <Common/GeneralizeProgressView.h>
#include <Common/IcaoWarningsView.h>
#include <Common/SubjectTreeWidget.h>
#include <SubjectEditor/ModalityPage.h>

namespace Neurotec { namespace Samples
{

class CaptureFacePage : public ModalityPage
{

public:
	CaptureFacePage(::Neurotec::Biometrics::Client::NBiometricClient& biometricClient, ::Neurotec::Biometrics::NSubject& subject,
		SubjectEditorPageInterface& subjectEditorPageInterface, wxWindow *parent, wxWindowID winid = wxID_ANY);

	virtual ~CaptureFacePage();
	virtual void OnNavigatedTo();
	virtual void OnNavigatingFrom();
	virtual void SetIsBusy(bool value);

private:
	void OnCaptureClick(wxCommandEvent& event);
	void OnFinishClick(wxCommandEvent& event);
	void OnStartClick(wxCommandEvent& event);
	void OnCancelClick(wxCommandEvent& event);
	void OnEndClick(wxCommandEvent& event);
	void OnRepeatClick(wxCommandEvent& event);
	void OnThread(wxCommandEvent &event);
	void OnRadioButtonCheckedChanged(wxCommandEvent & event);
	void OnMirrorHorizontallyCheckedChanged(wxCommandEvent & event);
	void OnCheckIcaoComplianceCheckedChanged(wxCommandEvent & event);
	void OnSubjectTreePropertyChanged(wxCommandEvent & event);

	void CreateGUIControls();
	void RegisterGuiEvents();
	void UnregisterGuiEvents();

	static void OnCreateTemplateAsyncCompleted(::Neurotec::EventArgs args);
	static void OnFacePropertyChangedCallback(::Neurotec::NObject::PropertyChangedEventArgs args);
	static void OnBiometricClientPropertyChangedCallback(::Neurotec::NObject::PropertyChangedEventArgs args);
	static void OnCurrentBiometricCompletedCallback(::Neurotec::EventArgs args);

	void EnableControls();
	void UpdateWithTaskResult(::Neurotec::Biometrics::NBiometricStatus status);
	void PrepareViews(bool isCapturing, bool checkIcao, bool isOk = true);
	void OnFaceCaptureDeviceChanged();
	void OnCurrentBiometricCompleted(::Neurotec::Biometrics::NBiometricStatus status);
	void OnCurrentBiometricChanged(::Neurotec::Biometrics::NFace & face);
	void OnFaceStatusChanged(::Neurotec::Biometrics::NBiometricStatus status);

private:
	enum
	{
		ID_RADIO_CAMERA = wxID_HIGHEST + 1,
		ID_RADIO_VIDEO,
		ID_RADIO_IMAGE,
		ID_CHECK_ICAO,
		ID_SUBJECT_TREE
	};

	enum
	{
		ID_EVENT_STATUS_CHANGED,
		ID_EVENT_CURRENT_BIOMETRIC_CHANGED,
		ID_EVENT_CURRENT_BIOMETRIC_COMPLETED,
		ID_EVENT_CAMERA_CHANGED,
		ID_EVENT_CAPTURE_FINISHED
	};

	::Neurotec::Biometrics::NSubject m_newSubject;
	::Neurotec::Biometrics::NFace m_currentBiometric;

	bool m_isExtractStarted;
	::Neurotec::NInt m_sessionId;
	wxString m_titlePrefix;

	::Neurotec::Biometrics::Gui::wxNFaceView * m_faceView;
	IcaoWarningsView * m_icaoView;
	SubjectTreeWidget * m_subjectTreeControl;
	wxButton *m_btnCapture;
	wxButton *m_btnCancel;
	wxButton *m_btnForceStart;
	wxButton *m_btnForceEnd;
	wxButton *m_btnFinish;
	wxButton *m_btnRepeat;
	wxCheckBox *m_chbCheckIcao;
	wxCheckBox *m_chbStream;
	wxCheckBox *m_chbManual;
	wxCheckBox *m_chbWithGeneralization;
	wxCheckBox *m_chbMirrorHorizontally;
	wxRadioButton *m_radioCamera;
	wxRadioButton *m_radioImageFile;
	wxRadioButton *m_radioVideoFile;
	StatusPanel *m_statusPanel;
	BusyIndicator *m_busyIndicator;
	wxBoxSizer *m_statusSizer;
	GeneralizeProgressView * m_generalizationView;
	::Neurotec::Gui::wxNViewZoomSlider * m_zoomSlider;
};

}}

#endif


#ifndef CAPTURE_PALM_PAGE_H_INCLUDED
#define CAPTURE_PALM_PAGE_H_INCLUDED

#include <Common/StatusPanel.h>
#include <Common/BusyIndicator.h>
#include <Common/SubjectTreeWidget.h>
#include <Common/GeneralizeProgressView.h>

#include <SubjectEditor/ModalityPage.h>
#include <SubjectEditor/PalmSelector.h>

namespace Neurotec { namespace Samples
{

class CapturePalmPage : public ModalityPage
{

public:
	CapturePalmPage(::Neurotec::Biometrics::Client::NBiometricClient& biometricClient, ::Neurotec::Biometrics::NSubject& subject,
		SubjectEditorPageInterface& subjectEditorPageInterface, wxWindow *parent, wxWindowID winid = wxID_ANY);

	virtual ~CapturePalmPage();
	virtual void OnNavigatedTo();
	virtual void OnNavigatingFrom();
	virtual void SetIsBusy(bool value);

private:
	void OnPalmScannerChanged();

	void OnCancelClick(wxCommandEvent &event);

	void OnForceClick(wxCommandEvent &event);

	void OnOpenClick(wxCommandEvent &event);

	void OnFinishClick(wxCommandEvent &event);

	void OnCaptureClick(wxCommandEvent &event);

	void OnSourceSelect(wxCommandEvent& event);

	void OnPositionSelect(wxCommandEvent& event);

	void OnShowProcessedImageClick(wxCommandEvent& event);

	void OnThread(wxCommandEvent &event);

	void EnableControls();

	void RegisterGuiEvents();

	void UnregisterGuiEvents();

	void CreateGUIControls();

	static void OnBiometricClientPropertyChangedCallback(::Neurotec::NObject::PropertyChangedEventArgs args);

	static void OnPalmPropertyChangedCallback(::Neurotec::NObject::PropertyChangedEventArgs args);

	static void OnCreateTemplateCompletedCallback(::Neurotec::EventArgs args);
	static void OnCaptureCompletedCallback(::Neurotec::EventArgs args);

	static void OnPalmSelectorMouseSelect(void *param, ::Neurotec::Biometrics::NFPosition position);

	void OnPalmTreeSelectionChanged(wxCommandEvent & event);

private:
	enum {
		ID_EVENT_SCANNER_CHANGED,
		ID_EVENT_CAPTURE_FINISHED,
		ID_EVENT_CREATE_TEMPLATE_COMPLETED,
		ID_EVENT_STATUS_CHANGED,
		ID_EVENT_CURRENT_BIOMETRIC_CHANGED,
	};

	wxRadioButton *m_radioScanner;
	wxRadioButton *m_radioFile;
	wxChoice *m_choiceImpression;
	wxChoice *m_choicePosition;
	wxButton *m_btnOpen;
	wxButton *m_btnCapture;
	wxButton *m_btnCancel;
	wxButton *m_btnForce;
	wxButton *m_btnFinish;
	::Neurotec::Biometrics::Gui::wxNPalmView *m_view;
	PalmSelector *m_palmSelector;
	StatusPanel *m_statusPanel;
	BusyIndicator *m_busyIndicator;
	wxBoxSizer *m_statusSizer;
	wxCheckBox *m_chbShowBinarizedImage;
	wxCheckBox *m_chbCaptureAutomatically;
	wxCheckBox *m_chbWithGeneralization;
	SubjectTreeWidget *m_palmTreeWidget;
	GeneralizeProgressView * m_generalizeProgressView;
	::Neurotec::Gui::wxNViewZoomSlider * m_zoomSlider;

	::Neurotec::Biometrics::NPalm m_currentBiometric;
	::Neurotec::Biometrics::NSubject m_newSubject;
	::Neurotec::NInt m_sessionId;
	::std::vector< ::Neurotec::Biometrics::NPalm> m_nowCapturing;
	wxString m_titlePrefix;
};

}}

#endif


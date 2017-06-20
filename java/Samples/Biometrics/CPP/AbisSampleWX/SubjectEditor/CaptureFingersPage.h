#ifndef CAPTURE_FINGERS_PAGE_H_INCLUDED
#define CAPTURE_FINGERS_PAGE_H_INCLUDED

#include <SubjectEditor/ModalityPage.h>
#include <SubjectEditor/FingerSelector.h>

#include <Common/StatusPanel.h>
#include <Common/BusyIndicator.h>
#include <Common/SubjectTreeWidget.h>
#include <Common/GeneralizeProgressView.h>

namespace Neurotec { namespace Samples
{

class CaptureFingersPage : public ModalityPage
{
public:
	class Scenario
	{
	private:
		struct Tuple
		{
			::Neurotec::Biometrics::NFPosition position;
			::Neurotec::Biometrics::NFImpressionType impression;
		};

		static ::Neurotec::Biometrics::NFPosition PlainFingers[];
		static ::Neurotec::Biometrics::NFPosition Slaps[];
		static ::Neurotec::Biometrics::NFPosition SlapsSeparatteThumbs[];

	private:
		wxString m_name;
		std::vector<Tuple> m_items;
		bool m_hasSlaps;
		bool m_hasRolled;
		bool m_isUnknownFingers;

	public:
		Scenario(wxString name);

		wxString GetName();
		bool HasSlaps();
		bool HasRolled();
		bool IsUnknownFingers();

		std::vector< ::Neurotec::Biometrics::NFinger > GetFingers(int sessionId = -1, int generalizationCount = 1);
		std::vector<Neurotec::Biometrics::NFPosition> GetPositions();
		static std::vector<Scenario> GetAvailableScenarios();

	private:
		void SetHasSlaps(bool value);
		void SetHasRolled(bool value);
		void SetIsUnknownFingers(bool value);
	};

public:
	CaptureFingersPage(::Neurotec::Biometrics::Client::NBiometricClient& biometricClient, ::Neurotec::Biometrics::NSubject& subject,
		SubjectEditorPageInterface& subjectEditorPageInterface, wxWindow *parent, wxWindowID winid = wxID_ANY);

	virtual ~CaptureFingersPage();
	virtual void SetIsBusy(bool value);

	void OnNavigatedTo();
	void OnNavigatingFrom();

private:
	void UpdateMissingPositions();
	void CopyMissingFingerPositions(::Neurotec::Biometrics::NSubject & dst, ::Neurotec::Biometrics::NSubject & src);

	void UpdateShowBinarized();
	void OnFingerScannerChanged();
	void OnRadioButtonToggle();
	void EnableControls();
	void ShowHint();
	void ListSupportedScenarios();
	void UpdateImpressionTypes(::Neurotec::Biometrics::NFPosition position, bool isRolled);
	void OnSelectedPositionChanged(Scenario * scenario, ::Neurotec::Biometrics::NFPosition position);
	void OnSelectedPositionChanged(Scenario * scenario, ::Neurotec::Biometrics::NFPosition position, bool rolled);
	Scenario * GetCurrentScenario();
	void OnCurrentBiometricChanged(::Neurotec::Biometrics::NFinger & current);
	void OnCaptureCompleted(::Neurotec::NAsyncOperation & operation);
	void OnCreateTemplateCompleted(::Neurotec::NAsyncOperation & operation);

	void OnSourceSelected(wxCommandEvent& event);
	void OnScenarioSelected(wxCommandEvent& event);
	void OnFinishClick(wxCommandEvent& event);
	void OnRepeatClick(wxCommandEvent& event);
	void OnSkipClick(wxCommandEvent& event);
	void OnCancelClick(wxCommandEvent& event);
	void OnForceClick(wxCommandEvent& event);
	void OnOpenFileClick(wxCommandEvent& event);
	void OnStartCapturingClick(wxCommandEvent& event);
	void OnShowProcessedImageClick(wxCommandEvent& event);
	void OnThread(wxCommandEvent &event);
	void OnGeneralizeProgressViewSelectionChanged(wxCommandEvent & event);
	void OnFingerTreeSelectionChanged(wxCommandEvent & event);
	void OnTimerTick(wxTimerEvent & event);
	void OnFingerSelectorFingerClick(wxCommandEvent & event);

	void RegisterGuiEvents();
	void UnregisterGuiEvents();
	void CreateGUIControls();

	static void CaptureAsyncCompletedCallback(::Neurotec::EventArgs args);
	static void CreateTemplateAsyncCompletedCallback(::Neurotec::EventArgs args);
	static void BiometricClientPropertyChangedCallback(::Neurotec::NObject::PropertyChangedEventArgs args);
	static void BiometricPropertyChangedCallback(::Neurotec::NObject::PropertyChangedEventArgs args);
	static void BiometricClientCurrentBiometricCompletedCallback(::Neurotec::EventArgs args);

private:
	enum
	{
		ID_EVENT_BIOMETRIC_STATUS_CHANGED,
		ID_EVENT_BIOMETRIC_CHANGED,
		ID_EVENT_SCANNER_CHANGED,
		ID_EVENT_CAPTURE_COMPLETED,
		ID_EVENT_CREATE_TEMPLATE_COMPLETED,
		ID_EVENT_REPEAT_CAPTURE,
	};

	wxRadioButton *m_radioScanner;
	wxRadioButton *m_radioFile;
	wxRadioButton *m_radioTenPrintCard;
	FingerSelector *m_fingerSelector;
	wxButton *m_btnStart;
	wxButton *m_btnRepeat;
	wxButton *m_btnSkip;
	wxButton *m_btnCancel;
	wxButton *m_btnOpenFile;
	wxButton *m_btnFinish;
	wxButton *m_btnForce;
	wxChoice *m_choiceScenario;
	wxChoice *m_choiceImpression;
	SubjectTreeWidget *m_fingerTreeWidget;
	wxStaticText *m_lblHint;
	wxCheckBox *m_chbShowBinarizedImage;
	wxCheckBox *m_chbCaptureAutomatically;
	::Neurotec::Biometrics::Gui::wxNFingerView *m_fingerView;
	StatusPanel *m_statusPanel;
	BusyIndicator *m_busyIndicator;
	wxBoxSizer *m_statusSizer;
	GeneralizeProgressView * m_generalizeProgressView;
	wxCheckBox * m_chbWithGeneralization;
	wxTimer * m_timer;
	::Neurotec::Gui::wxNViewZoomSlider * m_horizontalSlider;

	wxSizer * m_sizerCenter;

	bool m_captureNeedsAction;
	wxString m_titlePrefix;
	::Neurotec::NInt m_sessionId;
	::Neurotec::Biometrics::NSubject m_newSubject;
	::Neurotec::Biometrics::NBiometric m_currentBiometric;
	::std::vector<Scenario> m_supportedScenarios;
	::std::vector< ::Neurotec::Biometrics::NFinger > m_nowCapturing;
};

}}

#endif


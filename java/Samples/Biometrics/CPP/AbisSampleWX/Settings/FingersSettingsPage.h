#ifndef FINGERS_SETTINGS_PAGE_H_INCLUDED
#define FINGERS_SETTINGS_PAGE_H_INCLUDED

#include <Settings/BaseSettingsPage.h>

namespace Neurotec { namespace Samples
{

class FingersSettingsPage : public BaseSettingsPage
{
public:
	FingersSettingsPage(wxWindow *parent, wxWindowID winid = wxID_ANY);
	virtual ~FingersSettingsPage();

	void Initialize(::Neurotec::Biometrics::Client::NBiometricClient biometricClient);
	void Load();
	void Reset();

private:
	void OnFingerScannerChanged(wxCommandEvent& event);
	void OnConnectButtonClicked(wxCommandEvent& event);
	void OnDisconnectButtonClicked(wxCommandEvent& event);
	void OnTemplateSizeChanged(wxCommandEvent& event);
	void OnMatchingSpeedChanged(wxCommandEvent& event);
	void OnMaximalRotationChanged(wxSpinEvent& event);
	void OnQualityThresholdChanged(wxSpinEvent& event);
	void OnGenRecordCountChanged(wxSpinEvent& event);
	void OnFastExtractionChanged(wxCommandEvent& event);
	void OnReturnProcessedImageChanged(wxCommandEvent& event);
	void OnDeterminPatternClassChanged(wxCommandEvent& event);
	void OnCalculateNfiqChanged(wxCommandEvent & event);
	void OnCheckForDuplicatesChanged(wxCommandEvent& event);
	void OnThread(wxCommandEvent& event);

	void UpdateDevicesList();
	void RegisterGuiEvents();
	void UnregisterGuiEvents();
	void CreateGUIControls();
	int CheckIfSelectedDeviceIsDisconnectable();

	static void OnDevicesCollectionChanged(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Devices::NDevice> args);

private:
	enum
	{
		ID_EVT_UPDATE_DEVICES
	};

	wxChoice *m_choiceFingerScanner;
	wxButton *m_btnConnect;
	wxButton *m_btnDisconnect;
	wxChoice *m_choiceTemplateSize;
	wxChoice *m_choiceMatchingSpeed;
	wxSpinCtrl *m_spinMaximalRotation;
	wxSpinCtrl *m_spinQualityThreshold;
	wxCheckBox *m_chkFastExtraction;
	wxCheckBox *m_chkReturnBinarizedImage;
	wxSpinCtrl *m_spinGenRecordCount;
	wxCheckBox *m_chkCheckForDuplicates;
	wxCheckBox *m_chkCalculateNfiq;
	wxCheckBox *m_chkDeterminePatternClass;
};

}}

#endif


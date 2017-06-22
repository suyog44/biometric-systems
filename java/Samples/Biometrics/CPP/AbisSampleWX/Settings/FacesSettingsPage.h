#ifndef FACES_SETTINGS_PAGE_H_INCLUDED
#define FACES_SETTINGS_PAGE_H_INCLUDED

#include <Settings/BaseSettingsPage.h>

using namespace ::Neurotec::Devices;

namespace Neurotec { namespace Samples
{

class FacesSettingsPage : public BaseSettingsPage
{
public:
	FacesSettingsPage(wxWindow *parent, wxWindowID winid = wxID_ANY);

	virtual ~FacesSettingsPage();

	void Initialize(::Neurotec::Biometrics::Client::NBiometricClient biometricClient);

	void Load();

	void Reset();

private:
	void OnCameraChanged(wxCommandEvent& event);

	void OnConnectButtonClicked(wxCommandEvent& event);

	void OnDisconnectButtonClicked(wxCommandEvent& event);

	void OnFormatChanged(wxCommandEvent& event);

	void OnTemplateSizeChanged(wxCommandEvent& event);

	void OnMatchingSpeedChanged(wxCommandEvent& event);

	void OnMinimalInterOcularDistanceChanged(wxSpinEvent& event);

	void OnConfidenceThresholdChanged(wxSpinEvent& event);

	void OnMaximalRollChanged(wxSpinEvent& event);

	void OnMaximalYawChanged(wxSpinEvent& event);

	void OnQualityThresholdChanged(wxSpinEvent& event);

	void OnGeneralizationRecordCountChnaged(wxSpinEvent& event);

	void OnLivenessModeChanged(wxCommandEvent& event);

	void OnLivenessThresholdChanged(wxSpinEvent& event);

	void OnDetectAllFeaturePointsChanged(wxCommandEvent& event);

	void OnDetectBaseFeaturePointsChanged(wxCommandEvent& event);

	void OnDetermineGenderChanged(wxCommandEvent& event);

	void OnDetermineAgeChanged(wxCommandEvent& event);

	void OnDetectPropertiesChanged(wxCommandEvent& event);

	void OnRecognizeExpressionChanged(wxCommandEvent& event);

	void OnRecognizeEmotionChanged(wxCommandEvent& event);

	void OnCreateThumbnailImageChanged(wxCommandEvent& event);

	void OnWidthChanged(wxSpinEvent& event);

	void OnThread(wxCommandEvent& event);

	static void OnDevicesCollectionChanged(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Devices::NDevice> args);

	void UpdateDevicesList();

	void UpdateCaptureFormats();

	void RegisterGuiEvents();

	void UnregisterGuiEvents();

	void CreateGUIControls();

	int CheckIfSelectedDeviceIsDisconnectable();

	void DisconnectFromSelectedDevice();

private:
	enum
	{
		ID_EVT_UPDATE_DEVICES
	};

	wxChoice *m_choiceCamera;
	wxButton *m_btnConnect;
	wxButton *m_btnDisconnect;
	wxChoice *m_choiceFormat;
	wxChoice *m_choiceTemplateSize;
	wxChoice *m_choiceMatchingSpeed;
	wxChoice *m_choiceLivenessMode;
	wxSpinCtrl *m_spinMinimalInterOcularDistance;
	wxSpinCtrl *m_spinConfidenceThreshold;
	wxSpinCtrl *m_spinMaximalRoll;
	wxSpinCtrl *m_spinMaximalYaw;
	wxSpinCtrl *m_spinQualityThreshold;
	wxSpinCtrl *m_spinLivenessThreshold;
	wxCheckBox *m_chkDetectAllFeaturePoints;
	wxCheckBox *m_chkDetectBaseFeaturePoints;
	wxCheckBox *m_chkDetermineGender;
	wxCheckBox *m_chkDetermineAge;
	wxCheckBox *m_chkDetectProperties;
	wxCheckBox *m_chkRecognizeExpression;
	wxCheckBox *m_chkRecognizeEmotion;
	wxCheckBox *m_chkCreateThumbnailImage;
	wxSpinCtrl *m_spinWidth;
	wxSpinCtrl *m_spinGenRecordCount;
};

}}

#endif


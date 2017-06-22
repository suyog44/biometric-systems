#ifndef IRISES_SETTINGS_PAGE_H_INCLUDED
#define IRISES_SETTINGS_PAGE_H_INCLUDED

#include <Settings/BaseSettingsPage.h>

namespace Neurotec { namespace Samples
{

class IrisesSettingsPage : public BaseSettingsPage
{
public:
	IrisesSettingsPage(wxWindow *parent, wxWindowID winid = wxID_ANY);

	virtual ~IrisesSettingsPage();

	void Initialize(::Neurotec::Biometrics::Client::NBiometricClient biometricClient);

	void Load();

	void Reset();

private:
	void OnIrisScannerChanged(wxCommandEvent& event);

	void OnTemplateSizeChanged(wxCommandEvent& event);

	void OnMatchingSpeedChanged(wxCommandEvent& event);

	void OnMaximalRotationChanged(wxSpinEvent& event);

	void OnQualityThresholdChanged(wxSpinEvent& event);

	void OnFastExtractionChanged(wxCommandEvent& event);

	void OnThread(wxCommandEvent& event);

	void UpdateDevicesList();

	void RegisterGuiEvents();

	void UnregisterGuiEvents();

	void CreateGUIControls();

	static void OnDevicesCollectionChanged(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Devices::NDevice> args);

private:
	enum
	{
		ID_EVT_UPDATE_DEVICES
	};

	wxChoice *m_choiceIrisScanner;
	wxChoice *m_choiceTemplateSize;
	wxChoice *m_choiceMatchingSpeed;
	wxSpinCtrl *m_spinMaximalRotation;
	wxSpinCtrl *m_spinQualityThreshold;
	wxCheckBox *m_chkFastExtraction;
};

}}

#endif


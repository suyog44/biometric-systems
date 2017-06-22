#ifndef VOICES_SETTINGS_PAGE_H_INCLUDED
#define VOICES_SETTINGS_PAGE_H_INCLUDED

#include <Settings/BaseSettingsPage.h>

namespace Neurotec { namespace Samples
{

class VoicesSettingsPage : public BaseSettingsPage
{
public:
	VoicesSettingsPage(wxWindow *parent, wxWindowID winid = wxID_ANY);

	virtual ~VoicesSettingsPage();

	void Initialize(::Neurotec::Biometrics::Client::NBiometricClient biometricClient);

	void Load();

	void Reset();

private:
	void OnMicrophoneChanged(wxCommandEvent& event);

	void OnFormatChanged(wxCommandEvent& event);

	void OnUniquePhrasesOnlyChanged(wxCommandEvent& event);

	void OnExtractTextDependentFeaturesChanged(wxCommandEvent& event);

	void OnExtractTextIndependentFeaturesChanged(wxCommandEvent& event);

	void OnMaximalLoadedFileSizeChanged(wxCommandEvent& event);

	void OnThread(wxCommandEvent& event);

	void UpdateDevicesList();

	void UpdateCaptureFormats();

	void RegisterGuiEvents();

	void UnregisterGuiEvents();

	void CreateGUIControls();

	static void OnDevicesCollectionChanged(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Devices::NDevice> args);

private:
	enum
	{
		ID_EVT_UPDATE_DEVICES
	};

	wxChoice *m_choiceMicrophone;
	wxChoice *m_choiceFormat;
	wxCheckBox *m_chkUniquePhrasesOnly;
	wxCheckBox *m_chkExtractTextDependentFeatures;
	wxCheckBox *m_chkExtractTextIndependentFeatures;
	wxSpinCtrlDouble *m_scdMaxLoadedFileSize;
};

}}

#endif


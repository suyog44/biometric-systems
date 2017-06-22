#ifndef CAPTURE_ICAO_COMPLIANT_IMAGE_H_INCLUDED
#define CAPTURE_ICAO_COMPLIANT_IMAGE_H_INCLUDED

#include "IcaoWarningsView.h"

namespace Neurotec { namespace Samples {

class CaptureIcaoCompliantImage : public wxPanel
{
public:
	CaptureIcaoCompliantImage(wxWindow *parent, Neurotec::Biometrics::Client::NBiometricClient &biometricClient);
	~CaptureIcaoCompliantImage();

private:
	wxButton *m_buttonSaveTemplate;
	wxButton *m_buttonSaveImage;
	wxComboBox *m_comboBoxCameras;
	wxButton *m_buttonRefreshList;
	wxButton *m_buttonStart;
	wxButton *m_buttonStop;
	wxButton *m_buttonForce;
	wxStaticText *m_staticTextStatus;

	Neurotec::Biometrics::Gui::wxNFaceView *m_faceView;
	IcaoWarningsView * m_icaoView;

	Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
	Neurotec::Biometrics::NSubject m_subject;
	Neurotec::Biometrics::NFace m_segmentedFace;

	void CreateGUIControls();
	void OnButtonSaveTemplateClick(wxCommandEvent &event);
	void OnButtonSaveImageClick(wxCommandEvent &event);
	void OnButtonRefreshScannersClick(wxCommandEvent &event);
	void OnButtonStartClick(wxCommandEvent &event);
	void OnButtonStopClick(wxCommandEvent &event);
	void OnButtonForceClick(wxCommandEvent &event);
	void OnComboBoxCamerasChange(wxCommandEvent &event);
	void UpdateCameraList();
	void UpdateControls(bool isBusy);
	static void OnCaptureCompletedCallback(EventArgs);
	void OnCaptureCompleted(wxCommandEvent &event);

	enum
	{
		ID_BUTTON_SAVE_IMAGE = wxID_HIGHEST + 1,
		ID_BUTTON_SAVE_TEMPLATE,
		ID_BUTTON_REFRESH_SCANNERS,
		ID_BUTTON_START,
		ID_BUTTON_STOP,
		ID_BUTTON_FORCE,
		ID_COMBOBOX_CAMERAS
	};

	DECLARE_EVENT_TABLE();
};

}}

#endif

#ifndef ENROLL_FROM_CAMERA_H_INCLUDED
#define ENROLL_FROM_CAMERA_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		class EnrollFromCamera : public wxPanel
		{
		public:
			EnrollFromCamera(wxWindow *parent, Neurotec::Biometrics::Client::NBiometricClient &biometricClient);

		private:
			wxButton *m_buttonSaveTemplate;
			wxButton *m_buttonSaveImage;
			wxComboBox *m_comboBoxCameras;
			wxButton *m_buttonRefreshList;
			wxButton *m_buttonStart;
			wxButton *m_buttonStop;
			wxButton *m_buttonStartExtraction;
			wxCheckBox *m_checkBoxCaptureAutomatically;
			wxCheckBox *m_checkBoxCheckLiveness;
			wxStaticText *m_staticTextStatus;

			Neurotec::Biometrics::Gui::wxNFaceView *m_faceView;
			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_subject;

			void CreateGUIControls();
			void OnButtonSaveTemplateClick(wxCommandEvent &event);
			void OnButtonSaveImageClick(wxCommandEvent &event);
			void OnButtonRefreshScannersClick(wxCommandEvent &event);
			void OnButtonStartClick(wxCommandEvent &event);
			void OnButtonStopClick(wxCommandEvent &event);
			void OnButtonStartExtractionClick(wxCommandEvent &event);
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
				ID_BUTTON_START_EXTRACTION,
				ID_COMBOBOX_CAMERAS
			};

			DECLARE_EVENT_TABLE();
		};
	}
}
#endif

#ifndef ENROLL_FROM_IMAGE_H_INCLUDED
#define ENROLL_FROM_IMAGE_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		class EnrollFromImage : public wxPanel
		{
		public:
			EnrollFromImage(wxWindow *parent, Neurotec::Biometrics::Client::NBiometricClient &biometricClient);

		private:
			enum
			{
				ID_BUTTON_OPEN_IMAGE = wxID_HIGHEST + 1,
				ID_BUTTON_EXTRACT,
				ID_BUTTON_SAVE_TEMPLATE
			};

			wxButton *m_buttonOpenImage;
			wxButton *m_buttonExtract;
			wxComboBox *m_comboBoxRollAngle;
			wxComboBox *m_comboBoxYawAngle;
			wxStaticText *m_staticTextStatus;
			wxButton *m_buttonSaveTemplate;
			wxString m_stringFileName;
			bool m_isSegmentationActivated;

			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_subject;
			Neurotec::Biometrics::Gui::wxNFaceView *m_faceView;

			void LoadAngleCmb(int maxValue, float selectedItem, wxComboBox *comboBox);
			void CreateGUIControls();
			void OnButtonOpenImageClick(wxCommandEvent &event);
			void OnButtonExtractClick(wxCommandEvent &event);
			void OnButtonSaveTemplateClick(wxCommandEvent &event);
			void SetBiometricClientParams();
			void ExtractTemplate(const wxString &filename);
			static void OnCreateTemplateCompletedCallback(Neurotec::EventArgs args);
			void OnCreateTemplateCompleted(wxCommandEvent &event);
			void InitializeBiometricParams();
			DECLARE_EVENT_TABLE();
		};
	}
}
#endif

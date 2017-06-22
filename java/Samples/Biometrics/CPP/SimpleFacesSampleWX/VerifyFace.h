#ifndef VERIFY_FACE_H_INCLUDED
#define VERIFY_FACE_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		class VerifyFace : public wxPanel
		{
		public:
			VerifyFace(wxWindow *parent, Neurotec::Biometrics::Client::NBiometricClient &biometricClient);

		private:
			wxComboBox *m_comboBoxMatchingFar;
			wxButton *m_buttonDefaultFar;
			wxButton *m_buttonOpenImage1;
			wxButton *m_buttonOpenImage2;
			wxButton *m_buttonClearImages;
			wxButton *m_buttonVerify;
			wxStaticText *m_staticTextScore;
			wxStaticText *staticTextTemplateNameLeft;
			wxStaticText *staticTextTemplateNameRight;
			int m_defaultFar;

			Neurotec::Biometrics::Gui::wxNFaceView *m_faceView1;
			Neurotec::Biometrics::Gui::wxNFaceView *m_faceView2;
			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_subject1;
			Neurotec::Biometrics::NSubject m_subject2;

			void CreateGUIControls();
			void EnableVerifyButton();
			bool IsSubjectValid(Neurotec::Biometrics::NSubject &subject);
			wxString OpenImageTemplate(Neurotec::Biometrics::Gui::wxNFaceView* faceView, Neurotec::Biometrics::NSubject& subject);
			static void OnCreateTemplateCompletedCallback(EventArgs args);
			static void OnVerifyCompletedCallback(EventArgs args);
			void OnButtonOpenImage1Click(wxCommandEvent &event);
			void OnButtonOpenImage2Click(wxCommandEvent &event);
			void OnComboBoxMatchingFarChange(wxCommandEvent &event);
			void OnButtonDefaultFarClick(wxCommandEvent &event);
			void OnButtonClearImagesClick(wxCommandEvent &event);
			void OnButtonVerifyClick(wxCommandEvent &event);
			void OnCreateTemplateCompleted(wxCommandEvent& event);
			void OnVerifyCompleted(wxCommandEvent &event);
			void InitializeBiometricParams();

			enum
			{
				ID_BUTTON_OPEN_IMAGE_1 = wxID_HIGHEST,
				ID_BUTTON_OPEN_IMAGE_2,
				ID_BUTTON_CLEAR_IMAGES,
				ID_BUTTON_DEFAULT_FAR,
				ID_BUTTON_VERIFY,
				ID_COMBOBOX_FAR
			};
			DECLARE_EVENT_TABLE();
		};
	}
}

#endif

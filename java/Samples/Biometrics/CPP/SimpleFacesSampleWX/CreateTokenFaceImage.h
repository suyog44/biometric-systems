#ifndef CREATE_TOKEN_FACE_IMAGE_H_INCLUDED
#define CREATE_TOKEN_FACE_IMAGE_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{

		class CreateTokenFaceImage : public wxPanel
		{
		public:
			CreateTokenFaceImage(wxWindow* parent, Neurotec::Biometrics::Client::NBiometricClient &biometricClient);

		private:
			wxButton *m_buttonSaveTokenImage;
			wxButton *m_buttonOpenImage;
			wxStaticText *m_staticTextQuality;
			wxStaticText *m_staticTextSharpness;
			wxStaticText *m_staticTextUniformity;
			wxStaticText *m_staticTextDensity;

			Neurotec::Biometrics::Gui::wxNFaceView *m_faceViewOriginal;
			Neurotec::Biometrics::Gui::wxNFaceView *m_faceViewToken;
			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_subject;

			void CreateGUIControls();
			void OnButtonOpenImageClick(wxCommandEvent &event);
			void OnButtonSaveTokenImageClick(wxCommandEvent &event);
			static void OnCreateTokenImageCompletedCallback(EventArgs args);
			void ShowTokenAttributes();
			void ShowAttributeLabels(bool);
			void OnCreateTokenImageCompleted(wxCommandEvent &event);

			enum
			{
				ID_BUTTON_OPEN_IMAGE = wxID_HIGHEST + 1,
				ID_BUTTON_SAVE_TOKEN_IMAGE
			};

			DECLARE_EVENT_TABLE();
		};
	}
}
#endif

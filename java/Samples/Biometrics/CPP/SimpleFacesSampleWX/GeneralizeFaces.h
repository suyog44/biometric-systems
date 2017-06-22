#ifndef GENERALIZE_FACES_H_INCLUDED
#define GENERALIZE_FACES_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		class GeneralizeFaces : public wxPanel
		{
		public:
			GeneralizeFaces(wxWindow *parent, Neurotec::Biometrics::Client::NBiometricClient &biometricClient);

		private:
			wxButton *m_buttonOpenImages;
			wxStaticText *m_staticTextImageCount;
			wxStaticText *m_staticTextStatus;
			wxButton *m_buttonSaveTemplate;

			Neurotec::Biometrics::Gui::wxNFaceView *m_faceView;
			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_subject;

			void CreateGUIControls();
			void OnButtonOpenImagesClick(wxCommandEvent &event);
			void OnButtonSaveTemplateClick(wxCommandEvent &event);
			static void OnGeneralizeFacesCompletedCallback(Neurotec::EventArgs args);
			void OnGeneralizeFacesCompleted(wxCommandEvent &event);

			DECLARE_EVENT_TABLE();
		};

		enum
		{
			ID_BUTTON_OPEN_IMAGES = wxID_HIGHEST + 1,
			ID_BUTTON_SAVE_TEMPLATE
		};
	}
}
#endif

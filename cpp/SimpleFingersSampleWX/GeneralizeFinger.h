#ifndef GENERALIZE_FINGER_H_INCLUDED
#define GENERALIZE_FINGER_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		class GeneralizeFinger : public wxPanel
		{

		public:
			GeneralizeFinger(wxWindow *parent, const Neurotec::Biometrics::Client::NBiometricClient & biometricClient, wxWindowID id = wxID_ANY,
				const wxPoint & pos = wxDefaultPosition, const wxSize & size = wxDefaultSize, long style = wxTAB_TRAVERSAL, const wxString & name = wxPanelNameStr);
			~GeneralizeFinger();

		private:
			enum
			{
				ID_BUTTON_OPEN_IMAGES = wxID_HIGHEST + 1,
				ID_BUTTON_SAVE_TEMPLATE,
				ID_CHECKBOX_SHOW_BINARIZED_IMAGE
			};

			void OnButtonOpenImagesClick(wxCommandEvent & event);
			void OnCheckBoxShowBinarizedImageClick(wxCommandEvent & event);
			void OnButtonSaveTemplateClick(wxCommandEvent & event);
			void CreateGUIControls();
			void OnGeneralizeCompleted(wxCommandEvent & event);
			static void OnGeneralizeCompletedCallback(const EventArgs & args);

			Neurotec::Biometrics::Gui::wxNFingerView *m_fingerView;
			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_subject;

			wxButton *m_buttonSaveTemplate;
			wxBitmapButton *m_buttonOpenImages;
			wxCheckBox *m_checkBoxShowBinarizedImage;
			wxStaticText *m_staticTextStatus;
			wxStaticText *m_staticTextNoOfImages;

			DECLARE_EVENT_TABLE();
		};
	}
}
#endif

#ifndef VERIFY_FINGER_H_INCLUDED
#define VERIFY_FINGER_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		class VerifyFinger : public wxPanel
		{
		public:
			VerifyFinger(wxWindow *parent, const Neurotec::Biometrics::Client::NBiometricClient & biometricClient, wxWindowID id = wxID_ANY, const wxPoint & pos = wxDefaultPosition,
				const wxSize & size = wxDefaultSize, long style = wxTAB_TRAVERSAL, const wxString & name = wxPanelNameStr);
			~VerifyFinger();

		private:
			enum
			{
				ID_BITMAPBUTTON_OPEN_IMAGE_LEFT = wxID_HIGHEST + 1,
				ID_BITMAPBUTTON_OPEN_IMAGE_RIGHT,
				ID_CHECKBOX_SHOW_BINARIZED_LEFT,
				ID_CHECKBOX_SHOW_BINARIZED_RIGHT,
				ID_BUTTON_VERIFY,
				ID_BUTTON_CLEAR_IMG,
				ID_BUTTON_DEFAULT_FAR,
				ID_COMBOBOX_FAR
			};

			void OnCreateTemplateCompleted(wxCommandEvent & event);
			void OnVerifyCompleted(wxCommandEvent & event);
			void OpenTemplateOrImage(Neurotec::Biometrics::Gui::wxNFingerView *fingerView, Neurotec::Biometrics::NSubject *subject, wxStaticText *label);
			void OnButtonOpenLeftClick(wxCommandEvent & event);
			void OnButtonOpenRightClick(wxCommandEvent & event);
			void OnCheckBoxShowProsessedImageLeftClick(wxCommandEvent & event);
			void OnCheckBoxShowProsessedImageRightClick(wxCommandEvent & event);
			void OnButtonVerifyClick(wxCommandEvent & event);
			void OnButtonClearImagesClick(wxCommandEvent & event);
			void OnButtonDefaultFARClick(wxCommandEvent & event);
			void OnComboBoxFARChange(wxCommandEvent & event);
			void CreateGUIControls();
			void EnableCheckBoxes();
			void EnableVerifyButton();
			bool IsSubjectValid(const Neurotec::Biometrics::NSubject & subject);

			void InitializeBiometricParams();
			static void OnCreateTemplateCompletedCallback(const Neurotec::EventArgs & args);
			static void OnVerifyCompletedCallback(const Neurotec::EventArgs & args);

			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_subjectLeft;
			Neurotec::Biometrics::NSubject m_subjectRight;
			Neurotec::Biometrics::NSubject m_subjectCurrent;
			int m_defaultFar;

			Neurotec::Biometrics::Gui::wxNFingerView* m_fingerViewLeft;
			Neurotec::Biometrics::Gui::wxNFingerView* m_fingerViewRight;
			wxButton *m_buttonImageLeft;
			wxButton *m_buttonImageRight;
			wxButton *m_buttonVerify;
			wxButton *m_buttonClearImg;
			wxButton *m_buttonDefaultFAR;
			wxStaticText *m_staticTextMatchedTemplateScore;
			wxCheckBox *m_checkBoxShowBinarizedImageLeft;
			wxCheckBox *m_checkBoxShowBinarizedImageRight;
			wxComboBox *m_comboBoxFAR;
			wxStaticText *m_staticTextRightImagePath;
			wxStaticText *m_staticTextLeftImagePath;

			DECLARE_EVENT_TABLE();
		};
	}
}
#endif

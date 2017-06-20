#ifndef IDENTIFY_FINGER_H_INCLUDED
#define IDENTIFY_FINGER_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		class IdentifyFinger : public wxPanel
		{
		public:
			IdentifyFinger(wxWindow *parent, const Neurotec::Biometrics::Client::NBiometricClient & biometricClient, wxWindowID id = wxID_ANY,
				const wxPoint & pos = wxDefaultPosition, const wxSize & size = wxDefaultSize, long style = wxTAB_TRAVERSAL, const wxString & name = wxPanelNameStr);
			~IdentifyFinger();

		private:
			enum
			{
				ID_BUTTON_OPEN_TEMPLATE = wxID_HIGHEST + 1,
				ID_BUTTON_OPEN_IMAGE,
				ID_BUTTON_IDENTYFY,
				ID_BUTTON_DEFAULT_FAR,
				ID_BUTTON_DEFAULT_FINGER_QUALITY_THRESHOLD,
				ID_SPINCTRL_FINGER_QUALITY_THRESHOLD,
				ID_COMBOBOX_FAR,
				ID_CHECKBOX_SHOW_BINARIZED_IMAGE
			};

			void OnExtractionCompleted(wxCommandEvent & event);
			void OnEnrollCompleted(wxCommandEvent & event);
			void OnIdentifyCompleted(wxCommandEvent & event);
			void OnButtonOpenTemplateOrImageClicked(wxCommandEvent & event);
			void OnButtonOpenTemplatesClicked(wxCommandEvent & event);
			void OnButtonIdentifyClick(wxCommandEvent & event);
			void OnButtonDefaultThresholdClicked(wxCommandEvent & event);
			void OnButtonDefaultFARClicked(wxCommandEvent & event);
			void OnComboBoxFARChange(wxCommandEvent & event);
			void OnCkeckBoxShowBinarizedImageClick(wxCommandEvent & event);
			void OnSpinControlThresholdChange(wxCommandEvent & event);
			void CreateGUIControls();
			bool IsSubjectValid(const Neurotec::Biometrics::NSubject & subject);
			static void OnEnrollCompletedCallback(const EventArgs & args);
			static void OnExtractionCompletedCallback(const EventArgs & args);
			static void OnIdentifyCompletedCallback(const EventArgs & args);

			Neurotec::Biometrics::Gui::wxNFingerView *m_fingerView;
			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_subjectToIdentify;
			Neurotec::Biometrics::NBiometricTask m_enrollTask;
			int m_defaultQualityThreshold;
			int m_defaultFar;

			wxArrayString m_subjectIds;
			wxButton *m_buttonIdentify;
			wxButton *m_buttonOpenImage;
			wxCheckBox *m_checkBoxShowBinarizedImage;
			wxSpinCtrl *m_spinCtrlThreshold;
			wxComboBox *m_comboBoxFAR;
			wxButton *m_buttonDefaultFAR;
			wxButton *m_buttonDefaultThreshold;
			wxListCtrl *m_listResults;
			wxStaticText *m_staticTextTemplatesLoaded;

			DECLARE_EVENT_TABLE();
		};
	}
}
#endif

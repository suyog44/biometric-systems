#ifndef ENROLL_FROM_IMAGE_H_INCLUDED
#define ENROLL_FROM_IMAGE_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		class EnrollFromImage : public wxPanel
		{

		public:
			EnrollFromImage(wxWindow *parent, const Neurotec::Biometrics::Client::NBiometricClient & biometricClient, wxWindowID id = wxID_ANY,
				const wxPoint & pos = wxDefaultPosition, const wxSize & size = wxDefaultSize, long style = wxTAB_TRAVERSAL, const wxString & name = wxPanelNameStr);

		private:
			enum
			{
				ID_BUTTON_OPEN_IMAGE = wxID_HIGHEST + 1,
				ID_BUTTON_EXRACT_FEATURES,
				ID_BUTTON_DEFAULT_FINGER_QUALITY_THRESHOLD,
				ID_BUTTON_SAVE_IMAGE,
				ID_BUTTON_SAVE_TEMPLATE,
				ID_CHECKBOX_SHOW_BINARIZED_IMAGE,
				ID_SPINCTRL_FINGER_QUALITY_THRESHOLD
			};

			static void OnFeaturesExtractionCompletedCallback(const Neurotec::EventArgs & args);
			void OnButtonOpenImageClick(wxCommandEvent & event);
			void OnButtonExractFeaturesClick(wxCommandEvent & event);
			void OnButtonDefaultClick(wxCommandEvent & event);
			void OnButtonSaveImageClick(wxCommandEvent & event);
			void OnButtonSaveTemplateClick(wxCommandEvent & event);
			void OnCheckBoxShowBinarizedImageClick(wxCommandEvent & event);
			void OnFingerQualityThresholdChanged(wxCommandEvent & event);
			void ExtractFeatures();
			void CreateGUIControls();
			void OnFeatureExtractionCompleted(wxCommandEvent & event);

			Neurotec::Biometrics::Gui::wxNFingerView *m_fingerViewOriginalImage;
			Neurotec::Biometrics::Gui::wxNFingerView *m_fingerViewBinarizedImage;
			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_subject;
			NByte m_defaultQualityThreshold;
			Neurotec::Images::NImage m_image;

			wxButton *m_buttonExtractFeatures;
			wxSpinCtrl *m_spinCtrlFingerQualityThreshold;
			wxButton *m_buttonDefaultFinngerQualityThreshold;
			wxButton *m_buttonSaveImage;
			wxButton *m_buttonSaveTemplate;
			wxCheckBox *m_checkBoxShowBinarizedImage;
			wxStaticText *m_staticTextQuality;

			DECLARE_EVENT_TABLE();
		};
	}
}
#endif

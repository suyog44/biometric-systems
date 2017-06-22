#ifndef IDENTIFY_IRIS_H_INCLUDED
#define IDENTIFY_IRIS_H_INCLUDED
#include "LicensePanel.h"

namespace Neurotec
{
	namespace Samples
	{
		class IdentifyIris :public wxPanel
		{
		public:
			IdentifyIris(wxWindow *parent, const Neurotec::Biometrics::Client::NBiometricClient & biometricClient);
			~IdentifyIris();

		private:
			enum
			{
				ID_BUTTON_OPEN_TEMPLATE,
				ID_BUTTON_OPEN_IMAGE,
				ID_BUTTON_IDENTIFY,
				ID_BUTTON_DEFAULT_FAR,
				ID_COMBOBOX_FAR
			};

			void CreateGUIControls();
			bool IsSubjectValid(const Neurotec::Biometrics::NSubject & subject);
			void OnButtonOpenTemplatesClick(wxCommandEvent& event);
			void OnButtonOpenTemplatesOrImageClick(wxCommandEvent& event);
			void OnButtonIdentifyClick(wxCommandEvent& event);
			void OnButtonDefaultFARClick(wxCommandEvent& event);
			void OnComboBoxFARChange(wxCommandEvent& event);
			void OnEnrollCompleted(wxCommandEvent& event);
			void OnCreateTemplateCompleted(wxCommandEvent& event);
			void OnIdentifyCompleted(wxCommandEvent& event);
			static void OnEnrollCompletedCallback(const Neurotec::EventArgs & args);
			static void OnIdentifyCompletedCallback(const Neurotec::EventArgs & args);
			static void OnCreateTemplateCompletedCallback(const Neurotec::EventArgs & args);

			wxButton *m_buttonOpenImage;
			wxBitmapButton *m_buttonOpenTemplate;
			wxButton *m_buttonIdentify;
			wxButton *m_buttonDefaultMatchingFAR;
			wxStaticText *m_staticTextNumberOfTemplates;
			wxStaticText *m_staticTextImageInfo;
			wxComboBox *m_comboMatchingFAR;
			wxListCtrl *m_listCtrlResults;
			Neurotec::Biometrics::Gui::wxNIrisView *m_irisView;
			Neurotec::Gui::wxNViewZoomSlider *m_zoomSlider;

			wxArrayString m_selectedFileNames;
			int m_defaultFar;
			bool m_hasEnrolledSubjects;

			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_subject;
			DECLARE_EVENT_TABLE();
		};
	}
}

#endif // IDENTIFY_IRIS_H_INCLUDED

#ifndef VERIFY_IRIS_H_INCLUDED
#define VERIFY_IRIS_H_INCLUDED

#include "LicensePanel.h"

namespace Neurotec
{
	namespace Samples
	{
		class VerifyIris :public wxPanel
		{
		public:
			VerifyIris(wxWindow *parent, const Neurotec::Biometrics::Client::NBiometricClient & biometricClient);
			~VerifyIris();

		private:
			enum
			{
				ID_BUTTON_OPEN_IMAGE1 = wxID_HIGHEST + 10,
				ID_BUTTON_OPEN_IMAGE2,
				ID_BUTTON_DEFAULT_FAR,
				ID_BUTTON_CLEAR,
				ID_BUTTON_VERIFY,
				ID_COMBOBOX_FAR
			};

			void OnButtonOpenIrisLeftClick(wxCommandEvent& event);
			void OnButtonOpenIrisRightClick(wxCommandEvent& event);
			void OnButtonClearImageClick(wxCommandEvent& event);
			void OnButtonDefaultFARClick(wxCommandEvent& event);
			void OnComboBoxMatchingFARChange(wxCommandEvent& event);
			void OnButtonVerifyClick(wxCommandEvent& event);
			void OnCreateTemplateCompleted(wxCommandEvent& event);
			void OnVerifyCompleted(wxCommandEvent& event);
			static void OnCreateTemplateCompletedCallback(const EventArgs & args);
			static void OnVerifyCompletedCallback(const EventArgs & args);
			void CreateGUIControls();
			wxString OpenTemplateOrImage(Neurotec::Biometrics::Gui::wxNIrisView *irisView, Neurotec::Biometrics::NSubject *subject);
			void EnableVerifyButton();
			bool IsSubjectValid(const Neurotec::Biometrics::NSubject & subject);
			void InitializeBiometricParams();

			wxButton *m_buttonDefaultFAR;
			wxButton *m_buttonVerify;
			wxBitmapButton *m_buttonOpenIris1;
			wxBitmapButton *m_buttonOpenIris2;
			wxButton *m_buttonClearImages;
			wxStaticText *m_staticTxtLeftInfo;
			wxStaticText *m_staticTextRightInfo;
			wxStaticText *m_staticTextScore;
			wxComboBox *m_comboFAR;
			Neurotec::Biometrics::Gui::wxNIrisView *m_irisView1;
			Neurotec::Biometrics::Gui::wxNIrisView *m_irisView2;

			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_subject1;
			Neurotec::Biometrics::NSubject m_subject2;
			int m_defaultFar;

			DECLARE_EVENT_TABLE();
		};
	}
}

#endif // VERIFY_IRIS_H_INCLUDED

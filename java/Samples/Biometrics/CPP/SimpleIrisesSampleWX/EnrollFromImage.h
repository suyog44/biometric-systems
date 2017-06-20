#ifndef ENROLL_FROM_IMAGE_H_INCLUDED
#define ENROLL_FROM_IMAGE_H_INCLUDED

#include "LicensePanel.h"

namespace Neurotec
{
	namespace Samples
	{
		class EnrollFromImage : public wxPanel
		{
		public:
			EnrollFromImage(wxWindow *parent, const Neurotec::Biometrics::Client::NBiometricClient & biometricClient);
			~EnrollFromImage();

		private:
			enum
			{
				ID_BUTTON_OPEN_IMAGE = wxID_HIGHEST,
				ID_BUTTON_SAVE_TEMPLATE,
			};

			void OnButtonOpenImageClick(wxCommandEvent& event);
			void OnButtonSaveTemplateClick(wxCommandEvent& event);
			void OnCreateTemplateCompleted(wxCommandEvent& event);
			static void OnCreateTemplateCompletedCallback(const Neurotec::EventArgs & args);
			void CreateGUIControls();

			Neurotec::Biometrics::Gui::wxNIrisView *m_irisView;
			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_subject;

			wxButton* m_buttonSaveTemplate;
			wxButton* m_buttonOpenImage;
			wxStaticText *m_staticTxtQuality;
			Neurotec::Gui::wxNViewZoomSlider *m_zoomSlider;

			DECLARE_EVENT_TABLE();
		};
	}
}

#endif // ENROLL_FROM_IMAGE_H_INCLUDED

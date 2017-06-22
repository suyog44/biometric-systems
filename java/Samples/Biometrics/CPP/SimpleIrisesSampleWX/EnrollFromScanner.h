#ifndef ENROLL_FROM_SCANNER_H_INCLUDED
#define ENROLL_FROM_SCANNER_H_INCLUDED
#include "LicensePanel.h"

namespace Neurotec
{
	namespace Samples
	{
		class EnrollFromScanner :public wxPanel
		{

		public:
			EnrollFromScanner(wxWindow *parent, const Neurotec::Biometrics::Client::NBiometricClient & biometricClient);
			~EnrollFromScanner();

		private:
			enum
			{
				ID_BUTTON_SCAN = wxID_HIGHEST,
				ID_BUTTON_CANCEL,
				ID_BUTTON_REFRESH,
				ID_RADIOBUTTON_RIGHT_IRIS,
				ID_RADIOBUTTON_LEFT_IRIS,
				ID_BUTTON_SAVE_IMAGE,
				ID_BUTTON_SAVE_TEMPLATE,
				ID_SCANNAR_SELECTED,
				ID_BUTTON_FORCE,
				ID_CHECKBOX_AUTO_SCAN,
			};

			void OnButtonSaveImageClick(wxCommandEvent& event);
			void OnButtonSaveTemplateClick(wxCommandEvent& event);
			void OnButtonScanClick(wxCommandEvent& event);
			void OnButtonCancelClick(wxCommandEvent& event);
			void OnButtonRefreshClick(wxCommandEvent &event);
			void OnListScannerSelectionChange(wxCommandEvent& event);
			void OnButtonForceClick(wxCommandEvent& event);
			void UpdateControlStatus(bool capturing);
			static void OnCaptureCompletedCallback(const Neurotec::EventArgs & args);
			void OnCaptureCompleted(wxCommandEvent& event);
			void CreateGUIControls();
			void UpdateScannersList();

			wxListBox *m_listboxScanners;
			wxButton *m_buttonScan;
			wxButton *m_buttonCancel;
			wxButton *m_buttonRefreshList;
			wxButton *m_buttonForce;
			wxCheckBox *m_checkBxScanAutomatically;
			wxRadioButton *m_radioButtonRightIris;
			wxRadioButton *m_radioButtonLeftIris;
			wxStaticText *m_staticTxtStatus;
			wxButton *m_buttonSaveImage;
			wxButton *m_buttonSaveTemplate;
			Neurotec::Gui::wxNViewZoomSlider *m_zoomSlider;

			Neurotec::Biometrics::Gui::wxNIrisView *m_irisView;
			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_subject;

			DECLARE_EVENT_TABLE();
		};
	}
}

#endif // ENROLL_FROM_SCANNER_H_INCLUDED

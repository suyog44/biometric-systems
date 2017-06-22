#ifndef ENROLL_FROM_SCANNER_H_INCLUDED
#define ENROLL_FROM_SCANNER_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		class EnrollFromScanner : public wxPanel
		{

		public:
			EnrollFromScanner(wxWindow *parent, const Neurotec::Biometrics::Client::NBiometricClient & biometricClient, wxWindowID id = wxID_ANY,
				const wxPoint & pos = wxDefaultPosition, const wxSize & size = wxDefaultSize, long style = wxTAB_TRAVERSAL, const wxString & name = wxPanelNameStr);
			~EnrollFromScanner();

		private:
			enum
			{
				ID_LISTBOX_SCANNERS = wxID_HIGHEST + 1,
				ID_BUTTON_SCAN,
				ID_BUTTON_CANCEL,
				ID_BUTTON_REFRESH_SCANNERS,
				ID_BUTTON_SAVE_IMAGE,
				ID_BUTTON_SAVE_TEMPLATE,
				ID_CHECKBOX_SHOW_BINARIZED_IMAGE,
				ID_BUTTON_FORCE,
				ID_CHECKBOX_EXTRACT_AUTOMATICALLY
			};

			void OnButtonScanClick(wxCommandEvent & event);
			void OnScannerChanged(wxCommandEvent & event);
			void OnCheckBoxExtractAutomaticallyClick(wxCommandEvent & event);
			void OnButtonCancelClick(wxCommandEvent & event);
			void OnButtonForceClick(wxCommandEvent & event);
			void OnButtonRefreshScannersClick(wxCommandEvent & event);
			void OnButtonSaveImageClick(wxCommandEvent & event);
			void OnButtonSaveTemplateClick(wxCommandEvent & event);
			void OnCheckBoxShowBinarizedImageClick(wxCommandEvent & event);
			void EnableControls(bool isCapturing);
			void CreateGUIControls();
			void RefreshScannerList();
			void OnCaptureCompleted(wxCommandEvent & event);
			void OnFingerPropertyChanged(wxCommandEvent & event);
			static void OnFingerPropertyChangedCallback(const Neurotec::Biometrics::NFinger::PropertyChangedEventArgs & evtArgs);
			static void OnCaptureCompletedCallback(const EventArgs & args);

			Neurotec::Biometrics::Gui::wxNFingerView* m_fingerView;
			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_subject;
			Neurotec::Biometrics::NFinger m_finger;

			wxButton *m_buttonSaveImage;
			wxButton *m_buttonSaveTemplate;
			wxButton *m_buttonScan;
			wxButton *m_buttonRefresh;
			wxButton *m_buttonCancel;
			wxButton *m_buttonForce;
			wxListBox *m_listBoxScaners;
			wxCheckBox *m_checkBoxShowBinarizedImage;
			wxCheckBox *m_checkBoxScanAuto;
			wxStaticText *m_staticTextFingerQuality;

			DECLARE_EVENT_TABLE();
		};
	}
}
#endif

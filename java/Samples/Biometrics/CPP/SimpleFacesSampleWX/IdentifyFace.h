#ifndef IDENTIFY_FACE_H_INCLUDED
#define IDENTIFY_FACE_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		class IdentifyFace : public wxPanel
		{
		public:
			IdentifyFace(wxWindow *parent, Neurotec::Biometrics::Client::NBiometricClient &biometricClient);

		private:
			wxStaticText *m_staticTextTemplatesCount;
			wxStaticText *m_staticTextFileName;
			wxButton *m_buttonOpenTemplate;
			wxButton *m_buttonOpenImage;
			wxButton *m_buttonIdentify;
			wxComboBox *m_comboBoxFar;
			wxButton *m_buttonDefaultFar;
			wxListCtrl *m_listCtrlListView;
			int m_defaultFar;

			Neurotec::Biometrics::Gui::wxNFaceView *m_faceView;
			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_subject;
			Neurotec::Biometrics::NBiometricTask m_enrollTask;

			void CreateGUIControls();
			void OnButtonOpenTemplatesClick(wxCommandEvent &event);
			void OnButtonOpenImageClick(wxCommandEvent &event);
			void OnButtonIdentifyClick(wxCommandEvent &event);
			void OnComboBoxFarChange(wxCommandEvent &event);
			void OnButtonDefaultFarClick(wxCommandEvent &event);
			static void OnEnrollCompletedCallback(EventArgs args);
			static void OnExtractCompletedCallback(EventArgs args);
			static void OnIdentifyCompletedCallback(EventArgs args);
			void OnEnrollCompleted(wxCommandEvent &event);
			void OnExtractCompleted(wxCommandEvent &event);
			void OnIdentifyCompleted(wxCommandEvent &event);
			void InitializeBiometricParams();

			enum
			{
				ID_BUTTON_OPEN_TEMPLATE = wxID_HIGHEST + 1,
				ID_BUTTON_OPEN_IMAGE,
				ID_BUTTON_IDENTIFY,
				ID_BUTTON_DEFAULT_FAR,
				ID_COMBOBOX_FAR
			};
			DECLARE_EVENT_TABLE();
		};
	}
}

#endif

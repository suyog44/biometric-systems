#ifndef DETECT_FACES_H_INCLUDED
#define DETECT_FACES_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		class DetectFaces : public wxPanel
		{
		public:
			DetectFaces(wxWindow *parent, Neurotec::Biometrics::Client::NBiometricClient &biometricClient);

		private:
			enum
			{
				ID_BUTTON_DETECTFEATURES = wxID_HIGHEST + 1,
				ID_BUTTON_OPENIMAGE
			};

			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::Gui::wxNFaceView *m_faceView;
			Neurotec::Images::NImage m_faceImage;

			wxButton *m_buttonOpenImage;
			wxButton *m_buttonDetectFacialFeatures;
			wxComboBox *m_comboBoxRollAngle;
			wxComboBox *m_comboBoxYawAngle;
			bool m_isSegmentationActivated;

			void InitializeBiometricParams();
			void LoadAngleCmb(int maxValue, float selectedItem, wxComboBox *comboBox);
			void CreateGUIControls();
			void SetBiometricClientParams();
			void DetectFace();
			void OnButtonOpenImageClick(wxCommandEvent &event);
			void OnButtonDetectFacialFeaturesClick(wxCommandEvent &event);
			static void OnDetectFacesCompletedCallback(Neurotec::EventArgs args);
			void OnDetectFacesCompleted(wxCommandEvent &event);

			DECLARE_EVENT_TABLE();
		};
	}
}

#endif

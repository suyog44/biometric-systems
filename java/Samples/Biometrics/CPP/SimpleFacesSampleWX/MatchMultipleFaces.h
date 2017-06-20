#ifndef MATCH_MULTIPLE_FACES_H_INCLUDED
#define MATCH_MULTIPLE_FACES_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		class MatchMultipleFaces : public wxPanel
		{
		public:
			MatchMultipleFaces(wxWindow* parent, Neurotec::Biometrics::Client::NBiometricClient &biometricClient);

		private:
			Neurotec::Biometrics::Gui::wxNFaceView *m_faceViewReference;
			Neurotec::Biometrics::Gui::wxNFaceView *m_faceViewMultiFace;
			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_referenceSubject;
			Neurotec::Biometrics::NSubject m_multipleFacesSubject;
			Neurotec::Biometrics::NBiometricTask m_enrollmentTask;
			wxButton* m_buttonOpenMultifaceImage;
			wxButton* m_buttonOpenReferenceImage;

			void CreateGUIControls();
			void EnrollMultipleFaceSubject();
			void MatchFaces();
			static void OnExtractReferenceCompletedCallback(EventArgs args);
			static void OnExtractMultifaceCompletedCallback(EventArgs args);
			static void OnEnrollCompletedCallback(EventArgs args);
			static void OnIdentifyCompletedCallback(EventArgs args);
			void OnButtonOpenReferenceImageClick(wxCommandEvent &event);
			void OnButtonOpenMultifaceImageClick(wxCommandEvent &event);
			void OnExtractReferenceCompleted(wxCommandEvent &event);
			void OnExtractMultifaceCompleted(wxCommandEvent &event);
			void OnEnrollCompleted(wxCommandEvent &event);
			void OnIdentifyCompleted(wxCommandEvent &event);
			enum
			{
				ID_BUTTON_OPEN_REFERENCE_IMAGE,
				ID_BUTTON_OPEN_MULTIFACE_IMAGE
			};

			DECLARE_EVENT_TABLE();
		};
	}
}
#endif

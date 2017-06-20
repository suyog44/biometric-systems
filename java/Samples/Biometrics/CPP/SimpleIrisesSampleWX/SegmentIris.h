#ifndef SEGMENT_IRIS_H_INCLUDED
#define SEGMENT_IRIS_H_INCLUDED

#include "LicensePanel.h"

namespace Neurotec
{
	namespace Samples
	{
		class SegmentIris :public wxPanel
		{
		public:
			SegmentIris(wxWindow *parent, const Neurotec::Biometrics::Client::NBiometricClient & biometricClient);
			~SegmentIris();

		private:
			enum
			{
				ID_BUTTON_OPEN_IMAGE = wxID_HIGHEST + 1,
				ID_BUTTON_SAVE_SEGMENTED_IMAGE,
				ID_BUTTON_SEGMENT,
			};

			void OnButtonOpenImageClick(wxCommandEvent& event);
			void OnButtonSegmentClick(wxCommandEvent& event);
			void OnButtonSaveImageClick(wxCommandEvent& event);
			void OnSegmentCompleted(wxCommandEvent& event);
			static void OnSegmentCompletedCallback(const Neurotec::EventArgs & args);
			void CreateGUIControls();
			void ClearSegmentationData();

			wxButton *m_buttonSegmentIris;
			wxButton *m_buttonSaveImage;
			wxTextCtrl *m_textCtrlQuality;
			wxTextCtrl *m_textCtrlPupilToIrisRatio;
			wxTextCtrl *m_textCtrlUsableIrisArea;
			wxTextCtrl *m_textCtrlGrayScaleUtilisation;
			wxTextCtrl *m_textCtrlIrisSclareContrast;
			wxTextCtrl *m_textCtrlIrisPupilContrast;
			wxTextCtrl *m_textCtrlIrisPupilConcentricity;
			wxTextCtrl *m_textCtrlPupilBoundaryCircularity;
			wxTextCtrl *m_textCtrlSharpness;
			wxTextCtrl *m_textCtrlInterlance;
			wxTextCtrl *m_textCtrlMargineAdequacy;
			Neurotec::Biometrics::Gui::wxNIrisView *m_irisView;
			Neurotec::Biometrics::Gui::wxNIrisView *m_irisViewSegmented;
			Neurotec::Gui::wxNViewZoomSlider *m_zoomSliderOriginal;
			Neurotec::Gui::wxNViewZoomSlider *m_zoomSliderSegmented;

			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_subject;
			Neurotec::Biometrics::NIris m_iris;
			Neurotec::Biometrics::NIris m_segmentedIris;

			DECLARE_EVENT_TABLE();
		};
	}
}

#endif // SEGMENT_IRIS_H_INCLUDED

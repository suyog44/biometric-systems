#ifndef SEGMENT_FINGERPRINTS_H_INCLUDED
#define SEGMENT_FINGERPRINTS_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		class SegmentFingerprints : public wxPanel
		{
		public:
			SegmentFingerprints(wxWindow *parent, const Neurotec::Biometrics::Client::NBiometricClient & biometricClient, wxWindowID id = wxID_ANY, const wxPoint & pos = wxDefaultPosition,
				const wxSize & size = wxDefaultSize, long style = wxTAB_TRAVERSAL, const wxString & name = wxPanelNameStr);

		private:
			enum
			{
				ID_BUTTON_OPEN_IMAGE = wxID_HIGHEST + 1,
				ID_BUTTON_SEGMENT,
				ID_BUTTON_SAVE_SEGMENTS,
				ID_LISTBOX_POSITION
			};

			void OnButtonOpenImageClick(wxCommandEvent & event);
			void OnButtonSegmentClick(wxCommandEvent & event);
			void OnButtonSaveSegmentsClick(wxCommandEvent & event);
			void OnListboxPositionChange(wxCommandEvent & event);
			static void OnSegmentationCompletedCallback(const EventArgs & args);
			void ShowSegments();
			void ClearSegments();
			void Segment();
			void CreateGUIControls();
			void OnSegmentCompleted(wxCommandEvent & event);

			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			Neurotec::Biometrics::NSubject m_subject;
			Neurotec::Images::NImage m_image;

			Neurotec::Gui::wxNViewZoomSlider* m_zoomSlider;
			Neurotec::Biometrics::Gui::wxNFingerView* m_fingerView;
			Neurotec::Biometrics::Gui::wxNFingerView *m_subFingerView[4];
			wxStaticText *m_subFingerViewInfo[4][3];
			wxButton* m_buttonSaveImage;
			wxListBox* m_listBoxPosition;
			wxCheckListBox* m_checkListBoxMissingPositions;
			wxButton* m_buttonSegment;
			wxStaticText *m_staticTextSegmntStatus;
			DECLARE_EVENT_TABLE();
		};
	}
}
#endif

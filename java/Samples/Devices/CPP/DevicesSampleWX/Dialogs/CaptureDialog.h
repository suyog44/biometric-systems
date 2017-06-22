#ifndef CAPTURE_DIALOG_H_INCLUDED
#define CAPTURE_DIALOG_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			class CaptureDialog;

			class CapturingThread : public wxThread
			{
			public:
				CapturingThread(CaptureDialog *dialog);
				virtual ~CapturingThread();

			protected:
				virtual ExitCode Entry();
				virtual void OnExit();

			private:
				CaptureDialog *m_captureDialog;
			};

			class CaptureDialog : public wxDialog
			{
			public:
				CaptureDialog(wxWindow *parent, const wxWindowID id = 1, const wxString &title = wxEmptyString, const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = wxDEFAULT_DIALOG_STYLE | wxRESIZE_BORDER | wxCLIP_CHILDREN);
				virtual ~CaptureDialog();

				void OnButtonCloseClick(wxCommandEvent &evt);
				void OnCustomizeFormatButtonClicked(wxCommandEvent &evt);
				void OnClose(wxCloseEvent &event);
				void StartCaptureTask();
				void WaitForCaptureToFinish();
				Neurotec::Devices::NDevice GetDevice();
				void SetDevice(const Neurotec::Devices::NDevice &device);
				void SetGatherImages(bool gatherImages);
				void OnCaptureDialogShown();
				bool IsGatherImages();

			protected:
				wxImage m_bitmap, m_finalBitmap;
				wxMutex m_statuslock;

				enum
				{
					ID_FORCE_BTN,
					ID_CUSTOMIZE_BTN,
					ID_CANCEL_DIALOG,
					ID_FORMATS_COMBO_BOX,
					ID_BITMAP_BOX,
				};

				wxBoxSizer *m_boxSizerStatusTextContainerHbox;
				wxGenericStaticBitmap *m_captureImageWindow;
				wxPanel *m_panelPreview;
				wxComboBox *m_cmbFormats;
				wxButton *m_btnCustomize;
				wxButton *m_btnForce;
				wxTextCtrl *m_txtCtrlStatus;
				wxButton *m_buttonCancel;

				virtual void OnDeviceChanged();
				virtual void OnStatusChanged();
				virtual void OnMediaFormatChanged(const Neurotec::Media::NMediaFormat &mediaFormat);
				virtual void OnCaptureStarted();
				virtual void OnCaptureFinished();
				virtual void CancelCapture();
				virtual void OnCapture();
				virtual bool IsValidDeviceType(const Neurotec::NType &value);
				virtual void OnSelectedIndexChanged(wxCommandEvent &event);

				bool OnImage(const Neurotec::Images::NImage &image, const wxString &userStatus, const wxString &imageName, bool isFinal);
				bool IsEnableForcedCapture();
				bool IsCapturing();
				bool HasFinal();
				bool IsAutoCaptureStart();
				bool IsCancellationPending();
				void CheckIsBusy();
				void AddMediaFormats(const Neurotec::NArrayWrapper<Neurotec::Media::NMediaFormat> &mediaFormats, const Neurotec::Media::NMediaFormat &currentFormat);
				void SetAutoCaptureStart(bool autoCaptureStart);
				void SetEnableForcedCapture(bool show);
				void WriteParameter(wxXmlDocument doc, const wxString key, wxVariant parameter);
				void OnWriteScanParameters(wxXmlDocument doc);
				wxRect GetPictureArea();

				friend class CapturingThread;
				CapturingThread *m_thread;
				wxString m_errorMessage;

			private:
				bool m_isCapturing;
				bool m_gatherImages;
				bool m_forceCapture;
				bool m_autoCaptureStart;
				bool m_imageShowStarted;
				bool m_suppressMediaFormatEvents;
				int m_fps;
				int m_timestampsCount;
				int m_imageCount;
				wxStopWatch m_stopWatch;
				wxString m_userStatus, m_finalUserStatus;
				long m_lastReportTime;
				wxString m_imagesPath;
				Neurotec::Devices::NDevice m_device;

				void CreateGUIControls();
				void OnForceButtonClicked(wxCommandEvent &event);
				void StartThread();
				void StartGatherImageThread();
				wxString CreateGuid();

				DECLARE_EVENT_TABLE();
			};
		}
	}
}
#endif

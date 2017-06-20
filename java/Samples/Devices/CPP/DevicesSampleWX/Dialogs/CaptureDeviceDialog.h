#ifndef CAPTURE_DEVICE_DIALOG_H_INCLUDED
#define CAPTURE_DEVICE_DIALOG_H_INCLUDED

#include <Dialogs/CaptureDialog.h>

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			class CaptureDeviceDialog : public CaptureDialog
			{
			public:
				CaptureDeviceDialog(wxWindow *parent, const wxWindowID id = 1, const wxString &title = wxEmptyString, const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = wxDEFAULT_DIALOG_STYLE | wxRESIZE_BORDER);
				virtual ~CaptureDeviceDialog();

			protected:
				void OnCapture();
				virtual void OnStartingCapture() = 0;
				virtual void OnFinishingCapture() = 0;
				void CancelCapture();
				void OnMediaFormatChanged(const Neurotec::Media::NMediaFormat &mediaFormat);
				virtual bool OnObtainSample();
				static void DeviceCapturingChangedCallback(const Neurotec::EventArgs &args);
			private:
				wxMutex m_nextMediaFormatLock;
				Neurotec::Media::NMediaFormat m_nextMediaFormat;
			};
		}
	}
}

#endif

#ifndef BIOMETRIC_DEVICE_DIALOG_H_INCLUDED
#define BIOMETRIC_DEVICE_DIALOG_H_INCLUDED

#include <Dialogs/CaptureDialog.h>

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			class BiometricDeviceDialog : public CaptureDialog
			{
			public:
				BiometricDeviceDialog(wxWindow *parent, const wxWindowID id = wxID_ANY, const wxString &title = wxEmptyString, const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = wxDEFAULT_DIALOG_STYLE | wxRESIZE_BORDER);
				virtual ~BiometricDeviceDialog();
				bool IsAutomatic();
				void SetAutomatic(bool automatic);
				int GetTimeout();
				void SetTimeout(int timeout);

			protected:
				bool IsValidDeviceType(const NType &value);
				void CancelCapture();
				void OnWriteScanParameters(wxXmlDocument doc);

			private:
				bool m_automatic;
				int m_timeout;
				wxMutex m_mutexThreadTermination;
			};
		}
	}
}

#endif

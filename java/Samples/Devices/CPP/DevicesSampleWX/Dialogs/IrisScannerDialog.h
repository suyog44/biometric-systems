#ifndef IRIS_SCANNER_DIALOG_H_INCLUDED
#define IRIS_SCANNER_DIALOG_H_INCLUDED

#include <Dialogs/BiometricDeviceDialog.h>

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			class IrisScannerDialog : public BiometricDeviceDialog
			{
			public:
				IrisScannerDialog(wxWindow *parent, const wxWindowID id = 1, const wxString &title = wxEmptyString, const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = wxDEFAULT_DIALOG_STYLE | wxRESIZE_BORDER);
				virtual ~IrisScannerDialog();

				Neurotec::Biometrics::NEPosition GetPosition();
				void SetPosition(const Neurotec::Biometrics::NEPosition &position);
				Neurotec::NArrayWrapper<Neurotec::Biometrics::NEPosition> GetMissingPositions();
				void SetMissingPositions(const Neurotec::NArrayWrapper<Neurotec::Biometrics::NEPosition> &missingPositions);
				Neurotec::Biometrics::NIris GetCurrentBiometric();

			protected:
				bool IsValidDeviceType(const Neurotec::NType &value);
				void OnCapture();
				void OnWriteScanParameters(wxXmlDocument doc);

			private:
				bool OnImage(const Neurotec::Biometrics::NIris &biometric, bool isFinal);
				static void DeviceCapturePreviewCallback(const Neurotec::Devices::NBiometricDevice::CapturePreviewEventArgs &arg);

				Neurotec::Biometrics::NEPosition m_position;
				Neurotec::NArrayWrapper<Neurotec::Biometrics::NEPosition> m_missingPositions;
				Neurotec::Biometrics::NIris m_currentBiometric;

			};
		}
	}
}
#endif

#ifndef FSCANNER_DIALOG_H_INCLUDED
#define FSCANNER_DIALOG_H_INCLUDED

#include <Dialogs/BiometricDeviceDialog.h>

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			class FScannerDialog : public BiometricDeviceDialog
			{
			public:
				FScannerDialog(wxWindow *parent, const wxWindowID id = 1, const wxString &title = wxEmptyString, const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = wxDEFAULT_DIALOG_STYLE | wxRESIZE_BORDER);
				virtual ~FScannerDialog();

				Neurotec::Biometrics::NFImpressionType GetImpressionType();
				void SetImpressionType(const Neurotec::Biometrics::NFImpressionType &type);
				Neurotec::Biometrics::NFPosition GetPosition();
				void SetPosition(const Neurotec::Biometrics::NFPosition &position);
				std::vector<Neurotec::Biometrics::NFPosition> GetMissingPositions();
				void SetMissingPositions(const std::vector<Neurotec::Biometrics::NFPosition> &missingPositions);

			protected:
				bool IsValidDeviceType(const Neurotec::NType &value);
				void OnCapture();
				void OnWriteScanParameters(wxXmlDocument doc);

			private:
				bool OnImage(const Neurotec::Biometrics::NFrictionRidge &biometric, bool isFinal);
				static void DeviceCapturePreviewCallback(const Neurotec::Devices::NBiometricDevice::CapturePreviewEventArgs &arg);

				Neurotec::Biometrics::NFImpressionType m_impressionType;
				Neurotec::Biometrics::NFPosition m_position;
				std::vector<Neurotec::Biometrics::NFPosition> m_missingPositions;
			};
		}
	}
}

#endif

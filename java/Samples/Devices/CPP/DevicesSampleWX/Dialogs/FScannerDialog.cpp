#include "Precompiled.h"
#include <Dialogs/FScannerDialog.h>

using namespace Neurotec::Biometrics;
using namespace Neurotec::Devices;
using namespace Neurotec::Images;

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			FScannerDialog::FScannerDialog(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize &size, long style) :
				BiometricDeviceDialog(parent, id, title, position, size, style)
			{
				OnDeviceChanged();
			}

			FScannerDialog::~FScannerDialog()
			{
			}

			// Overridden function Called by the capture Thread
			void FScannerDialog::OnCapture()
			{
				NDevice device = GetDevice();
				NFScanner fScanner = NObjectDynamicCast<NFScanner>(device);

				fScanner.AddCapturePreviewCallback(&FScannerDialog::DeviceCapturePreviewCallback, this);
				try
				{
					NFrictionRidge biometric = NFrictionRidge::FromPosition(m_position);
					NSubject subject;

					if (m_missingPositions.size() > 0)
					{
						for (unsigned int i = 0; i < m_missingPositions.size(); i++)
						{
							subject.GetMissingFingers().Add(m_missingPositions[i]);
						}
					}
					if (biometric.GetNativeType() == NFinger::NativeTypeOf())
					{
						subject.GetFingers().Add(NObjectDynamicCast<NFinger>(biometric));
					}
					else
					{
						subject.GetPalms().Add(NObjectDynamicCast<NPalm>(biometric));
					}
					biometric.SetImpressionType(m_impressionType);
					if (!IsAutomatic())
					{
						biometric.SetCaptureOptions(nbcoManual);
					}
					NBiometricStatus status = fScanner.Capture(biometric, GetTimeout());
					if (status != nbsCanceled)
					{
						OnImage(biometric, true);
					}
				}
				catch (NError &ex)
				{
					m_errorMessage = ex.ToString();
				}
				fScanner.RemoveCapturePreviewCallback(&FScannerDialog::DeviceCapturePreviewCallback, this);
			}

			void FScannerDialog::DeviceCapturePreviewCallback(const NBiometricDevice::CapturePreviewEventArgs &args)
			{
				NFrictionRidge biometrc = NObjectDynamicCast<NFrictionRidge>(args.GetBiometric());
				FScannerDialog * dlg = static_cast<FScannerDialog*>(args.GetParam());
				bool force = dlg->OnImage(biometrc, false);
				NBiometricStatus status = biometrc.GetStatus();
				if (!dlg->IsAutomatic() && (status == nbsOk || !NBiometricTypes::IsBiometricStatusFinal(status)))
				{
					biometrc.SetStatus(force ? nbsOk : nbsBadObject);
				}
			}

			bool FScannerDialog::OnImage(const NFrictionRidge &biometric, bool isFinal)
			{
				wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), biometric.GetStatus());
				for (int i = 0; i < biometric.GetObjects().GetCount(); i++)
				{
					statusString.Append("\n");
					wxString position = NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), biometric.GetObjects().Get(i).GetPosition());
					wxString status = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), biometric.GetObjects().Get(i).GetStatus());
					statusString.Append(wxString::Format("\t%s: %s", position, status));
				}
				wxString status = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), biometric.GetStatus() != nbsNone ? biometric.GetStatus() : nbsOk);

				NImage image = NULL;
				if (!biometric.IsNull())
				{
					image = biometric.GetImage();
				}
				else
				{
					return false;
				}

				return CaptureDialog::OnImage(image, statusString, status, isFinal);
			}

			void FScannerDialog::OnWriteScanParameters(wxXmlDocument doc)
			{
				NDevice device = GetDevice();
				CaptureDialog::OnWriteScanParameters(doc);
				WriteParameter(doc, "ImpressionType", (int)GetImpressionType());
				WriteParameter(doc, "Position", (int)GetPosition());

				if (GetMissingPositions().size() > 0)
				{
					for (unsigned int i = 0; i < GetMissingPositions().size(); i++)
					{
						WriteParameter(doc, "Missing", (int)GetMissingPositions()[i]);
					}
				}
			}

			bool FScannerDialog::IsValidDeviceType(const NType &type)
			{
				return BiometricDeviceDialog::IsValidDeviceType(type) && NFScanner::NativeTypeOf().IsAssignableFrom(type);
			}

			NFImpressionType FScannerDialog::GetImpressionType()
			{
				return m_impressionType;
			}

			void FScannerDialog::SetImpressionType(const NFImpressionType &type)
			{
				m_impressionType = type;
			}

			NFPosition FScannerDialog::GetPosition()
			{
				return m_position;
			}

			void FScannerDialog::SetPosition(const NFPosition &position)
			{
				m_position = position;
			}

			std::vector<NFPosition> FScannerDialog::GetMissingPositions()
			{
				return m_missingPositions;
			}

			void FScannerDialog::SetMissingPositions(const std::vector<NFPosition> &missingPositions)
			{
				m_missingPositions = missingPositions;
			}
		}
	}
}

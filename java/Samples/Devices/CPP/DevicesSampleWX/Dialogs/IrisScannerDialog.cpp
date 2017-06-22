#include "Precompiled.h"
#include <Dialogs/IrisScannerDialog.h>

using namespace Neurotec::Biometrics;
using namespace Neurotec::Devices;
using namespace Neurotec::Geometry;

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			IrisScannerDialog::IrisScannerDialog(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize &size, long style) :
				BiometricDeviceDialog(parent, id, title, position, size, style), m_missingPositions((NInt)0)
			{
				OnDeviceChanged();
			}

			IrisScannerDialog::~IrisScannerDialog()
			{
			}

			bool IrisScannerDialog::OnImage(const NIris &biometric, bool isFinal)
			{
				wxString str = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), biometric.GetStatus());
				for (int i = 0; i < biometric.GetObjects().GetCount(); i++)
				{
					NEAttributes attr = biometric.GetObjects().Get(i);
					str.Append("\n");
					NRect boundingRect = attr.GetBoundingRect();
					wxString status = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), attr.GetStatus());
					wxString position = NEnum::ToString(NBiometricTypes::NEPositionNativeTypeOf(), attr.GetPosition());
					str.Append(wxString::Format("\t%s: %s (Position: {X=%i, Y=%i, Width=%i, Height=%i})", position, status, boundingRect.X, boundingRect.Y, boundingRect.Width, boundingRect.Height));
				}
				wxString status = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), biometric.GetStatus());
				return CaptureDialog::OnImage(biometric.GetImage(), str, status, isFinal);
			}

			bool IrisScannerDialog::IsValidDeviceType(const NType &type)
			{
				return BiometricDeviceDialog::IsValidDeviceType(type) && NFScanner::NativeTypeOf().IsAssignableFrom(type);
			}

			void IrisScannerDialog::OnCapture()
			{
				NDevice device = GetDevice();
				NIrisScanner irisScanner = NObjectDynamicCast<NIrisScanner>(device);
				irisScanner.AddCapturePreviewCallback(&IrisScannerDialog::DeviceCapturePreviewCallback, this);

				try
				{
					NSubject subject;

					if (m_missingPositions.GetCount() > 0)
					{
						for (int i = 0; i < m_missingPositions.GetCount(); i++)
						{
							subject.GetMissingEyes().Add(m_missingPositions.Get(i));
						}
					}

					NIris iris;
					iris.SetPosition(m_position);
					if (!IsAutomatic())
					{
						iris.SetCaptureOptions(nbcoManual);
					}

					m_currentBiometric = iris;
					irisScanner.Capture(iris, GetTimeout());
					OnImage(iris, true);
				}
				catch (NError &ex)
				{
					m_errorMessage = ex.ToString();
				}
				irisScanner.RemoveCapturePreviewCallback(&IrisScannerDialog::DeviceCapturePreviewCallback, this);
			}

			NEPosition IrisScannerDialog::GetPosition()
			{
				return m_position;
			}

			void IrisScannerDialog::SetPosition(const NEPosition &position)
			{
				m_position = position;
			}

			NArrayWrapper<NEPosition> IrisScannerDialog::GetMissingPositions()
			{
				return m_missingPositions;
			}

			void IrisScannerDialog::SetMissingPositions(const NArrayWrapper<NEPosition> &missingPositions)
			{
				m_missingPositions = missingPositions;
			}

			void IrisScannerDialog::DeviceCapturePreviewCallback(const NBiometricDevice::CapturePreviewEventArgs &args)
			{
				IrisScannerDialog * dlg = static_cast<IrisScannerDialog*>(args.GetParam());

				NIris biometrc = dlg->GetCurrentBiometric();

				bool force = dlg->OnImage(biometrc, false);
				if (!dlg->IsAutomatic())
				{
					biometrc.SetStatus(force ? nbsOk : nbsBadObject);
				}
			}

			void IrisScannerDialog::OnWriteScanParameters(wxXmlDocument doc)
			{
				NDevice device = GetDevice();
				CaptureDialog::OnWriteScanParameters(doc);
				WriteParameter(doc, "Position", (int)GetPosition());

				if (GetMissingPositions().GetCount() > 0)
				{
					for (int i = 0; i < GetMissingPositions().GetCount(); i++)
					{
						WriteParameter(doc, "Missing", (int)GetMissingPositions().Get(i));
					}
				}
			}

			NIris IrisScannerDialog::GetCurrentBiometric()
			{
				return m_currentBiometric;
			}
		}
	}
}

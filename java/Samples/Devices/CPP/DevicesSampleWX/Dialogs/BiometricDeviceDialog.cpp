#include "Precompiled.h"
#include <Dialogs/BiometricDeviceDialog.h>

using namespace std;
using namespace Neurotec::Devices;
using namespace Neurotec::Gui;

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			BiometricDeviceDialog::BiometricDeviceDialog(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize& size, long style) :
				CaptureDialog(parent, id, title, position, size, style)
			{
			}

			BiometricDeviceDialog::~BiometricDeviceDialog()
			{
			}

			void BiometricDeviceDialog::CancelCapture()
			{
				NDevice device = GetDevice();
				if (device.IsAvailable())
				{
					NObjectDynamicCast<NBiometricDevice>(device).Cancel();
				}
				wxMutexLocker lock(m_mutexThreadTermination);
				if (lock.IsOk() && m_thread != NULL)
				{
					m_thread->Wait();
					delete m_thread;
					m_thread = NULL;
				}
			}

			void BiometricDeviceDialog::OnWriteScanParameters(wxXmlDocument doc)
			{
				NDevice device = GetDevice();
				CaptureDialog::OnWriteScanParameters(doc);
				WriteParameter(doc, "Modality", (int)NObjectDynamicCast<NBiometricDevice>(device).GetBiometricType());
			}

			bool BiometricDeviceDialog::IsAutomatic()
			{
				return m_automatic;
			}

			void BiometricDeviceDialog::SetAutomatic(bool automatic)
			{
				m_automatic = automatic;
			}

			int BiometricDeviceDialog::GetTimeout()
			{
				return m_timeout;
			}

			void BiometricDeviceDialog::SetTimeout(int timeout)
			{
				m_timeout = timeout;
			}

			bool BiometricDeviceDialog::IsValidDeviceType(const NType &type)
			{
				return CaptureDialog::IsValidDeviceType(type) && NBiometricDevice::NativeTypeOf().IsAssignableFrom(type);
			}

		}
	}
}

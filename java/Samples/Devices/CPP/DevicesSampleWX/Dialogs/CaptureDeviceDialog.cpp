#include "Precompiled.h"
#include <Dialogs/CaptureDeviceDialog.h>

using namespace Neurotec::Devices;
using namespace Neurotec::Images;
using namespace Neurotec::Media;
using namespace Neurotec::Gui;

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			CaptureDeviceDialog::CaptureDeviceDialog(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize &size, long style) :
				CaptureDialog(parent, id, title, position, size, style), m_nextMediaFormat(NULL)
			{
				SetAutoCaptureStart(true);
				m_btnCustomize->Enable(true);
			}

			CaptureDeviceDialog::~CaptureDeviceDialog()
			{
			}

			void CaptureDeviceDialog::OnCapture()
			{
				NDevice device = GetDevice();
				NCaptureDevice captureDevice = NObjectDynamicCast<NCaptureDevice>(device);

				captureDevice.AddIsCapturingChangedCallback(&CaptureDeviceDialog::DeviceCapturingChangedCallback, this);
				OnStartingCapture();

				bool stoppedCapturing = false;
				try
				{
					captureDevice.StartCapturing();
				}
				catch (NError &ex)
				{
					captureDevice.RemoveIsCapturingChangedCallback(&CaptureDeviceDialog::DeviceCapturingChangedCallback, this);
					wxMessageBox("Error: " + ex.ToString(), "DevicesSampleWX");
					m_errorMessage = ex.ToString();
					OnFinishingCapture();
					OnCaptureFinished();
					return;
				}

				bool sampleObtained = true;
				try
				{
					if (!captureDevice.GetCurrentFormat().IsNull())
					{
						NMediaFormat currentFormat = captureDevice.GetCurrentFormat();
						NArrayWrapper<NMediaFormat> formats = captureDevice.GetFormats();
						AddMediaFormats(formats, currentFormat);
					}
					while (sampleObtained && !IsCancellationPending())
					{
						sampleObtained = false;
						wxMutexLocker lock(m_nextMediaFormatLock);
						if (lock.IsOk())
						{
							if (!m_nextMediaFormat.IsNull())
							{
								try
								{
									captureDevice.SetCurrentFormat(m_nextMediaFormat);
								}
								catch (NError &ex)
								{
									wxMessageBox("Error: " + ex.ToString(), "DevicesSampleWX");
									m_errorMessage = ex.ToString();
								}
								m_nextMediaFormat = NULL;
							}
						}
						sampleObtained = OnObtainSample();
					}
				}
				catch (NError &ex)
				{
					m_errorMessage = ex.ToString();
				}
				if (sampleObtained && captureDevice.IsAvailable())
				{
					captureDevice.StopCapturing();
					stoppedCapturing = true;
				}
				captureDevice.RemoveIsCapturingChangedCallback(&CaptureDeviceDialog::DeviceCapturingChangedCallback, this);
				OnFinishingCapture();
				if (!stoppedCapturing) OnCaptureFinished();
			}

			void CaptureDeviceDialog::CancelCapture()
			{
				CaptureDialog::CancelCapture();
				NDevice device = GetDevice();
				if (device.IsAvailable())
				{
					NObjectDynamicCast<NCaptureDevice>(device).StopCapturing();
				}
			}

			void CaptureDeviceDialog::OnMediaFormatChanged(const NMediaFormat &mediaFormat)
			{
				wxMutexLocker lock(m_nextMediaFormatLock);
				if (lock.IsOk())
				{
					m_nextMediaFormat = mediaFormat;
				}
			}

			bool CaptureDeviceDialog::OnObtainSample()
			{
				NThrowNotImplementedException();
			}

			void CaptureDeviceDialog::DeviceCapturingChangedCallback(const EventArgs &args)
			{
				CaptureDeviceDialog * dlg = static_cast<CaptureDeviceDialog*>(args.GetParam());
				NDevice device = dlg->GetDevice();
				NCaptureDevice captureDevice = NObjectDynamicCast<NCaptureDevice>(device);

				if (dlg->GetDevice().IsAvailable() && captureDevice.IsCapturing())
				{
					dlg->OnCaptureStarted();
				}
				else
				{
					dlg->OnCaptureFinished();
				}
			}
		}
	}
}

#include "Precompiled.h"
#include <Dialogs/MicrophoneDialog.h>

using namespace Neurotec::Devices;
using namespace Neurotec::Sound;
using namespace Neurotec::Sound::Processing;

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			MicrophoneDialog::MicrophoneDialog(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize &size, long style) :
				CaptureDeviceDialog(parent, id, title, position, size, style)
			{
				CreateGUIControls();
				OnDeviceChanged();
				SetEnableForcedCapture(false);
			}

			MicrophoneDialog::~MicrophoneDialog()
			{
			}

			void MicrophoneDialog::CreateGUIControls()
			{
				m_gaugeSoundLevel = new wxGauge(m_panelPreview, ID_SOUND_LEVEL_GAUGE, 100, wxDefaultPosition, wxSize(640, 20));
				m_captureImageWindow->Show(false);
				m_panelPreview->GetSizer()->Add(m_gaugeSoundLevel, 0, wxALL | wxEXPAND, 5);
				m_panelPreview->GetSizer()->AddStretchSpacer(2);
				m_panelPreview->Layout();
			}

			void MicrophoneDialog::OnSoundSample(const NSoundBuffer &soundBuffer)
			{
				wxMutexLocker lock(m_statuslock);
				if (lock.IsOk())
				{
					m_soundLevel = NSoundProc::GetSoundLevel(soundBuffer);
				}
				CallAfter(&MicrophoneDialog::OnStatusChanged);
			}

			void MicrophoneDialog::OnStatusChanged()
			{
				{
					wxMutexLocker lock(m_statuslock);
					if (lock.IsOk())
					{
						int level = (int)(m_soundLevel * 100);
						m_gaugeSoundLevel->SetValue(level);
						m_gaugeSoundLevel->Show(IsCapturing());
					}
				}
				CaptureDeviceDialog::OnStatusChanged();
			}

			bool MicrophoneDialog::IsValidDeviceType(const NType &type)
			{
				return CaptureDeviceDialog::IsValidDeviceType(type) && NMicrophone::NativeTypeOf().IsAssignableFrom(type);
			}

			bool MicrophoneDialog::OnObtainSample()
			{
				NDevice device = GetDevice();
				NMicrophone microphone = NObjectDynamicCast<NMicrophone>(device);
				NSoundBuffer soundSample = microphone.GetSoundSample();
				if (!soundSample.IsNull())
				{
					OnSoundSample(soundSample);
					return true;
				}
				return false;
			}

			void MicrophoneDialog::OnStartingCapture()
			{
			}

			void MicrophoneDialog::OnFinishingCapture()
			{
			}
		}
	}
}

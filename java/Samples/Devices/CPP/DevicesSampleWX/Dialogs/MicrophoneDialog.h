#ifndef MICROPHONE_DIALOG_H_INCLUDED
#define MICROPHONE_DIALOG_H_INCLUDED

#include <Dialogs/CaptureDeviceDialog.h>

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			class MicrophoneDialog : public CaptureDeviceDialog
			{
			public:
				MicrophoneDialog(wxWindow *parent, const wxWindowID id = 1, const wxString &title = wxEmptyString, const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = wxDEFAULT_DIALOG_STYLE | wxRESIZE_BORDER);
				virtual ~MicrophoneDialog();

			protected:
				void OnStatusChanged();
				bool IsValidDeviceType(const Neurotec::NType &value);
				bool OnObtainSample();
				virtual void OnStartingCapture();
				virtual void OnFinishingCapture();

			private:
				enum
				{
					ID_SOUND_LEVEL_GAUGE,
				};

				double m_soundLevel;
				wxGauge *m_gaugeSoundLevel;

				void CreateGUIControls();
				void OnSoundSample(const Neurotec::Sound::NSoundBuffer &soundBuffer);
			};
		}
	}
}
#endif

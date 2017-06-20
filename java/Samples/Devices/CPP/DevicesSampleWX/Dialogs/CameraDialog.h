#ifndef CAMERA_DEVICE_DIALOG_H_INCLUDED
#define CAMERA_DEVICE_DIALOG_H_INCLUDED

#include <Dialogs/CaptureDeviceDialog.h>

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			class CameraDialog : public CaptureDeviceDialog
			{
			public:
				CameraDialog(wxWindow *parent, const wxWindowID id = 1, const wxString &title = "", const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = wxDEFAULT_DIALOG_STYLE | wxRESIZE_BORDER | wxCLIP_CHILDREN);
				virtual ~CameraDialog();

				void SetClickFocus(bool focusValue);
				bool IsClickFocus();

			protected:
				void CreateCameraGUIControls();
				bool IsValidDeviceType(const Neurotec::NType &type);
				bool OnObtainSample();
				void OnStartingCapture();
				void OnFinishingCapture();
				void OnDeviceChanged();
				void OnCaptureFinished();

			private:
				void OnCameraStatusChanged();
				void OnBitmapBoxPaint(wxPaintEvent &event);
				void OnBitmapBoxClick(wxMouseEvent &event);
				void OnFocusButtonClicked(wxCommandEvent &event);
				void OnResetFocusButtonClicked(wxCommandEvent &event);
				void OnForceButtonClicked(wxCommandEvent &event);
				static void DeviceStillCapturedCallback(const Neurotec::Devices::NCamera::StillCapturedEventArgs &args);

				enum
				{
					ID_RESET_BTN,
					ID_FOCUS_BTN,
				};

				wxRect m_RectfocusRegion;
				wxButton *m_btnFocus;
				wxButton *m_btnReset;
				wxCheckBox *m_cbFocus;
				wxStaticText *m_stcTextCameraStatus;

				Neurotec::Devices::NCameraStatus m_cameraStatus;

				DECLARE_EVENT_TABLE();
			};
		}
	}
}
#endif

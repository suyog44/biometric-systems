#include "Precompiled.h"
#include <Dialogs/CameraDialog.h>

using namespace Neurotec;
using namespace Neurotec::Gui;
using namespace Neurotec::Devices;
using namespace Neurotec::Images;
using namespace Neurotec::Geometry;

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			BEGIN_EVENT_TABLE(CameraDialog, CaptureDeviceDialog)
				EVT_BUTTON(ID_FOCUS_BTN, CameraDialog::OnFocusButtonClicked)
				EVT_BUTTON(ID_RESET_BTN, CameraDialog::OnResetFocusButtonClicked)
				EVT_BUTTON(ID_FORCE_BTN, CameraDialog::OnForceButtonClicked)
				EVT_CLOSE(CameraDialog::OnClose)
			END_EVENT_TABLE()

			CameraDialog::CameraDialog(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize &size, long style) :
				CaptureDeviceDialog(parent, id, title, position, size, style), m_cameraStatus(ncsNone)
			{
				CreateCameraGUIControls();
				OnDeviceChanged();
				OnCameraStatusChanged();
				m_captureImageWindow->Bind(wxEVT_LEFT_DOWN, &CameraDialog::OnBitmapBoxClick, this);
				m_captureImageWindow->Bind(wxEVT_PAINT, &CameraDialog::OnBitmapBoxPaint, this);
			}

			CameraDialog::~CameraDialog()
			{
			}

			void CameraDialog::CreateCameraGUIControls()
			{
				wxBoxSizer *boxSizerCameraSpecificComponentsVbox = new wxBoxSizer(wxVERTICAL);

				m_btnFocus = new wxButton(this, ID_FOCUS_BTN, "Focus", wxDefaultPosition, wxDefaultSize);
				m_btnReset = new wxButton(this, ID_RESET_BTN, "Reset", wxDefaultPosition, wxDefaultSize);
				m_cbFocus = new wxCheckBox(this, wxID_ANY, "Focus", wxDefaultPosition, wxSize(55, 17));
				m_stcTextCameraStatus = new wxStaticText(this, wxID_ANY, "", wxDefaultPosition, wxDefaultSize);
				boxSizerCameraSpecificComponentsVbox->Add(m_btnFocus, 0, wxTOP, 5);
				boxSizerCameraSpecificComponentsVbox->Add(m_btnReset, 0, wxTOP, 5);
				boxSizerCameraSpecificComponentsVbox->Add(m_cbFocus, 0, wxTOP, 5);
				boxSizerCameraSpecificComponentsVbox->Add(m_stcTextCameraStatus, 0);
				m_boxSizerStatusTextContainerHbox->Add(boxSizerCameraSpecificComponentsVbox, 0);
				Layout();
			}

			void CameraDialog::OnCameraStatusChanged()
			{
				if (m_cameraStatus == ncsNone)
				{
					m_stcTextCameraStatus->SetLabelText(wxEmptyString);
				}
				else
				{
					m_stcTextCameraStatus->SetLabelText(NEnum::ToString(NCamera::NCameraStatusNativeTypeOf(), m_cameraStatus));
				}
			}

			bool CameraDialog::IsValidDeviceType(const NType &type)
			{
				return CaptureDeviceDialog::IsValidDeviceType(type) && NCamera::NDeviceTypeNativeTypeOf().IsAssignableFrom(type);
			}

			void CameraDialog::OnDeviceChanged()
			{
				CaptureDeviceDialog::OnDeviceChanged();
				NDevice device = GetDevice();
				if (device.IsNull() || (device.GetDeviceType() & ndtCamera) != ndtCamera) return;
				NCamera camera = NObjectDynamicCast<NCamera>(device);
				SetEnableForcedCapture(!camera.IsNull() && camera.IsStillCaptureSupported());
				bool isFocusSupported = !camera.IsNull() && camera.IsFocusSupported();
				m_btnReset->Show(isFocusSupported);
				m_btnFocus->Show(isFocusSupported);
				m_cbFocus->Show(!camera.IsNull() && camera.IsFocusRegionSupported());
				Layout();
			}

			void CameraDialog::OnStartingCapture()
			{
				NCamera camera = NObjectDynamicCast<NCamera>(GetDevice());
				camera.AddStillCapturedCallback(&CameraDialog::DeviceStillCapturedCallback, this);
			}

			void CameraDialog::OnFinishingCapture()
			{
				NDevice device = GetDevice();
				NCamera camera = NObjectDynamicCast<NCamera>(device);
				camera.RemoveStillCapturedCallback(&CameraDialog::DeviceStillCapturedCallback, this);
			}

			void CameraDialog::DeviceStillCapturedCallback(const NCamera::StillCapturedEventArgs &args)
			{
				CameraDialog * form = static_cast<CameraDialog*>(args.GetParam());
				if (!form->HasFinal())
				{
					NImage image = NImage::FromStream(args.GetStream());
					form->OnImage(image, wxEmptyString, wxEmptyString, true);
					form->CallAfter(&CameraDialog::CancelCapture);
				}
			}

			bool CameraDialog::OnObtainSample()
			{
				NDevice device = GetDevice();
				NCamera camera = NObjectDynamicCast<NCamera>(device);
				NImage image = camera.GetFrame();

				if (!image.IsNull())
				{
					NRectF focusRegoin;
					if (camera.GetFocusRegion(&focusRegoin))
					{
						m_RectfocusRegion = wxRect(focusRegoin.X, focusRegoin.Y, focusRegoin.Width, focusRegoin.Height);
					}
					OnImage(image, wxEmptyString, wxEmptyString, false);
					return true;
				}
				m_captureImageWindow->SetBitmap(wxNullBitmap);
				return false;
			}

			void CameraDialog::OnCaptureFinished()
			{
				m_RectfocusRegion.SetWidth(0);
				m_RectfocusRegion.SetHeight(0);
				CaptureDeviceDialog::OnCaptureFinished();
			}

			void CameraDialog::SetClickFocus(bool focusValue)
			{
				m_cbFocus->SetValue(focusValue);
			}

			bool CameraDialog::IsClickFocus()
			{
				return  m_cbFocus->IsChecked() && m_cbFocus->IsShown();
			}

			void CameraDialog::OnBitmapBoxPaint(wxPaintEvent &event)
			{
				try
				{
					event.Skip();
					if (event.GetId() == ID_BITMAP_BOX)
					{
						wxRect focusRegion;
						focusRegion = m_RectfocusRegion;
						if (focusRegion.IsEmpty()) return;

						wxRect area = GetPictureArea();
						wxPaintDC dc(this->m_captureImageWindow);
						dc.DrawRectangle(area.GetX() + focusRegion.GetX() * area.GetWidth(), area.GetY() + focusRegion.GetY() * area.GetHeight(),
							focusRegion.GetWidth() * area.GetWidth(), focusRegion.GetHeight() * area.GetHeight());
					}
				}
				catch (NError &e)
				{
					wxExceptionDlg::Show(e);
				}
			}

			void CameraDialog::OnBitmapBoxClick(wxMouseEvent &event)
			{
				try
				{
					event.Skip();
					if (IsClickFocus())
					{
						wxRect area = GetPictureArea();
						NDevice device = GetDevice();
						NCamera camera = NObjectDynamicCast<NCamera>(device);
						if (!camera.IsNull() && camera.IsFocusRegionSupported())
						{
							wxRect focusRegion = m_RectfocusRegion;
							float w = focusRegion.IsEmpty() ? focusRegion.GetWidth() : 0.1f;
							float h = focusRegion.IsEmpty() ? focusRegion.GetHeight() : 0.1f;
							NRectF region = NRectF((event.GetX() - area.GetX()) / (float)area.GetWidth() - w / 2, (event.GetY() - area.GetY()) / (float)area.GetHeight() - h / 2, w, h);
							camera.SetFocusRegion(&region);
							if (camera.IsFocusSupported())
							{
								m_cameraStatus = camera.Focus();
								OnCameraStatusChanged();
							}
						}
					}
				}
				catch (NError &e)
				{
					wxExceptionDlg::Show(e);
				}
			}

			void CameraDialog::OnFocusButtonClicked(wxCommandEvent &WXUNUSED(event))
			{
				try
				{
					NDevice device = GetDevice();
					NCamera camera = NObjectDynamicCast<NCamera>(device);
					m_cameraStatus = camera.Focus();
					OnCameraStatusChanged();
				}
				catch (NError &e)
				{
					wxExceptionDlg::Show(e);
				}
			}

			void CameraDialog::OnResetFocusButtonClicked(wxCommandEvent &WXUNUSED(event))
			{
				try
				{
					NDevice device = GetDevice();
					NCamera camera = NObjectDynamicCast<NCamera>(device);
					camera.ResetFocus();
					m_cameraStatus = ncsNone;
					OnCameraStatusChanged();
				}
				catch (NError &e)
				{
					wxExceptionDlg::Show(e);
				}
			}

			void CameraDialog::OnForceButtonClicked(wxCommandEvent &event)
			{
				try
				{
					NDevice device = GetDevice();
					NCamera camera = NObjectDynamicCast<NCamera>(device);
					m_cameraStatus = camera.CaptureStill();
					OnCameraStatusChanged();
				}
				catch (NError &e)
				{
					wxExceptionDlg::Show(e);
				}
				event.Skip();
			}

		}
	}
}

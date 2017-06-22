#include "Precompiled.h"
#include <Dialogs/CaptureDialog.h>
#include <Dialogs/CustomizeFormatDialog.h>
#include <DevicesSampleForm.h>
#include <stdlib.h>

#ifdef WIN32
#pragma comment(lib, "rpcrt4.lib")
#include <windows.h>
#include <iostream>
#else
#include <uuid/uuid.h>
#endif

using namespace std;

using namespace Neurotec::Images;
using namespace Neurotec::Devices;
using namespace Neurotec::Media;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			BEGIN_EVENT_TABLE(CaptureDialog, wxDialog)
				EVT_BUTTON(ID_FORCE_BTN, CaptureDialog::OnForceButtonClicked)
				EVT_BUTTON(ID_CUSTOMIZE_BTN, CaptureDialog::OnCustomizeFormatButtonClicked)
				EVT_BUTTON(ID_CANCEL_DIALOG, CaptureDialog::OnButtonCloseClick)
				EVT_COMBOBOX(ID_FORMATS_COMBO_BOX, CaptureDialog::OnSelectedIndexChanged)
				EVT_CLOSE(CaptureDialog::OnClose)
			END_EVENT_TABLE()

			CaptureDialog::CaptureDialog(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize &size, long style) :
			wxDialog(parent, id, title, position, size, style), m_thread(NULL), m_forceCapture(false), m_autoCaptureStart(false), m_imageShowStarted(false), m_fps(0), m_timestampsCount(0), m_device(NULL)
			{
				CreateGUIControls();
			}

			CaptureDialog::~CaptureDialog()
			{
			}

			void CaptureDialog::CreateGUIControls()
			{
				wxBoxSizer *m_boxSizerMain = new wxBoxSizer(wxVERTICAL);

				m_panelPreview = new wxPanel(this, wxID_ANY, wxDefaultPosition, wxSize(650, 350));
				wxBoxSizer *boxSizerPreview = new wxBoxSizer(wxVERTICAL);
				boxSizerPreview->AddStretchSpacer(1);

				wxBoxSizer *boxSizerPreviewInternal = new wxBoxSizer(wxHORIZONTAL);
				boxSizerPreviewInternal->AddStretchSpacer(1);
				m_captureImageWindow = new wxGenericStaticBitmap(m_panelPreview, ID_BITMAP_BOX, wxNullBitmap, wxDefaultPosition);
			#ifndef __WXMAC__
				m_captureImageWindow->SetDoubleBuffered(true);
			#endif
				boxSizerPreviewInternal->Add(m_captureImageWindow, 0, wxEXPAND);
				boxSizerPreviewInternal->AddStretchSpacer();

				boxSizerPreview->Add(boxSizerPreviewInternal, 0, wxEXPAND);
				boxSizerPreview->AddStretchSpacer(1);

				m_panelPreview->SetSizer(boxSizerPreview);
				m_boxSizerMain->Add(m_panelPreview, 1, wxEXPAND | wxALL, 10);

				wxBoxSizer *boxSizerControls = new wxBoxSizer(wxHORIZONTAL);
				m_cmbFormats = new wxComboBox(this, ID_FORMATS_COMBO_BOX, wxEmptyString, wxDefaultPosition, wxDefaultSize, wxArrayString(), wxCB_READONLY);
				boxSizerControls->Add(m_cmbFormats, 1, wxEXPAND | wxRIGHT, 5);
				m_btnCustomize = new wxButton(this, ID_CUSTOMIZE_BTN, "Customize...", wxDefaultPosition, wxDefaultSize);
				m_btnCustomize->Enable(false);
				boxSizerControls->Add(m_btnCustomize, 0, wxEXPAND | wxRIGHT, 5);
				m_btnForce = new wxButton(this, ID_FORCE_BTN, "Force", wxDefaultPosition, wxDefaultSize);
				boxSizerControls->Add(m_btnForce, 0, wxRIGHT | wxEXPAND, 5);

				m_boxSizerMain->Add(boxSizerControls, 0, wxEXPAND | wxLEFT | wxBOTTOM | wxRIGHT, 10);

				m_boxSizerStatusTextContainerHbox = new wxBoxSizer(wxHORIZONTAL);
				wxBoxSizer *boxSizerCancelButtonContainerVbox = new wxBoxSizer(wxVERTICAL);

				m_txtCtrlStatus = new  wxTextCtrl(this, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, wxTE_MULTILINE | wxTE_READONLY);
				m_buttonCancel = new wxButton(this, ID_CANCEL_DIALOG, "Cancel", wxDefaultPosition, wxDefaultSize);
				m_boxSizerStatusTextContainerHbox->Add(m_txtCtrlStatus, 1, wxRIGHT | wxEXPAND, 5);
				boxSizerCancelButtonContainerVbox->Add(m_buttonCancel, 0);

				m_boxSizerStatusTextContainerHbox->Add(boxSizerCancelButtonContainerVbox, 0, wxTOP | wxALIGN_BOTTOM, 90);
				m_boxSizerMain->Add(m_boxSizerStatusTextContainerHbox, 0, wxEXPAND | wxLEFT | wxBOTTOM | wxRIGHT, 10);

				SetSizer(m_boxSizerMain);
				Fit();
				SetTitle("Capture Form");
				Centre();
			}

			void CaptureDialog::OnDeviceChanged()
			{
				SetTitle(m_device.IsNull() ? (wxString)"No device" : (wxString)m_device.GetDisplayName());
				OnStatusChanged();
			}

			void CaptureDialog::OnStatusChanged()
			{
				wxMutexLocker lock(m_statuslock);
				if (lock.IsOk())
				{
					wxImage image;
					wxString theUserStatus;
					m_txtCtrlStatus->Clear();

					if (m_isCapturing)
					{
						m_txtCtrlStatus->AppendText(wxString::Format("Capturing (%i fps)", m_fps));
						image = m_bitmap;
						theUserStatus = m_userStatus;
					}
					else
					{
						m_txtCtrlStatus->AppendText("Finished");
						if (!m_errorMessage.IsEmpty())
						{
							m_txtCtrlStatus->AppendText(m_errorMessage);
						}
						image = m_finalBitmap;
						theUserStatus = m_userStatus;
					}

					if (image.IsOk())
					{
						double horizontalRatio = (double)m_panelPreview->GetSize().GetX() / (double)image.GetWidth();
						double verticalRatio = (double)m_panelPreview->GetSize().GetY() / (double)image.GetHeight();
						double aspectRatio = horizontalRatio > verticalRatio ? verticalRatio : horizontalRatio;
						wxSize imageSize = image.GetSize();
						image = image.Scale(image.GetWidth() * aspectRatio > 1 ? image.GetWidth() * aspectRatio : 1, image.GetHeight() * aspectRatio > 1 ? image.GetHeight() * aspectRatio : 1, wxIMAGE_QUALITY_BILINEAR);

						wxBitmap bmp = image;
						m_captureImageWindow->SetBitmap(bmp);
						m_panelPreview->Layout();
						m_imageShowStarted = true;

						m_txtCtrlStatus->AppendText(wxString::Format(" (%ix%i)", imageSize.GetWidth(), imageSize.GetHeight()));
					}
					if (!theUserStatus.IsNull())
					{
						m_txtCtrlStatus->AppendText(": ");
						m_txtCtrlStatus->AppendText(theUserStatus);
					}

					m_txtCtrlStatus->AppendText("\n");
					m_btnForce->Enable(m_isCapturing);
					m_buttonCancel->SetLabelText(m_isCapturing ? "Cancel" : "Close");
				}
			}

			void CaptureDialog::CheckIsBusy()
			{
				if (m_thread != NULL)
				{
					if (m_thread->IsRunning())
						NThrowInvalidOperationException("Capturing is running");
				}
			}

			void CaptureDialog::CancelCapture()
			{
				WaitForCaptureToFinish();
			}

			bool CaptureDialog::IsCancellationPending()
			{
				return m_thread->TestDestroy();
			}

			void CaptureDialog::WaitForCaptureToFinish()
			{
				if (m_thread != NULL)
				{
					m_thread->Delete();
					delete m_thread;
					m_thread = NULL;
				}
			}

			void CaptureDialog::OnCaptureDialogShown()
			{
				if (m_device.IsNull()) return;

				if (m_gatherImages)
				{
					wxString s = CreateGuid();
					m_imagesPath = wxString::Format("%s_%s/%s", (wxString)m_device.GetMake(), (wxString)m_device.GetModel(), s);
					wxFileName::Mkdir(m_imagesPath, wxS_DIR_DEFAULT, wxPATH_MKDIR_FULL);

					m_imageCount = 0;

					wxXmlDocument doc;
					wxXmlNode *root = new  wxXmlNode(wxXML_ELEMENT_NODE, "Scan");
					doc.SetRoot(root);
					doc.Save(m_imagesPath + "/" + "ScanInfo.xml");
				}
				if (!m_autoCaptureStart)
					OnCaptureStarted();

				StartThread();
			}

			void CaptureDialog::OnButtonCloseClick(wxCommandEvent &WXUNUSED(event))
			{
				Close();
			}

			void CaptureDialog::OnClose(wxCloseEvent &event)
			{
				CancelCapture();
				event.Skip();
			}

			bool CaptureDialog::IsValidDeviceType(const NType &WXUNUSED(value))
			{
				return true;
			}

			void CaptureDialog::OnCaptureStarted()
			{
				m_isCapturing = true;
				CallAfter(&CaptureDialog::OnStatusChanged);
			}

			void CaptureDialog::OnCaptureFinished()
			{
				m_isCapturing = false;
				CallAfter(&CaptureDialog::OnStatusChanged);
			}

			bool CaptureDialog::OnImage(const NImage &image, const wxString &userStatus, const wxString &imageName, bool isFinal)
			{
				{
					wxMutexLocker lock(m_statuslock);
					if (lock.IsOk())
					{
						if (!isFinal)
						{
							long elapsed = m_stopWatch.Time();
							m_timestampsCount++;
							if (elapsed - m_lastReportTime > 300)
							{
								long s = elapsed - m_lastReportTime;
								m_fps = 1000 * m_timestampsCount / s;
								m_lastReportTime = elapsed;
								m_timestampsCount = 0;
							}
						}

						if (m_gatherImages && !image.IsNull())
						{
							wxString s1 = wxString::Format("%s%s.png", isFinal ? (wxString)"Final" : (wxString::Format("%08i", m_imageCount++)), imageName.IsEmpty() ? (wxString)wxEmptyString : "_" + imageName);
							image.Save(m_imagesPath + "/" + s1);
						}

						if (isFinal)
						{
							m_finalBitmap = image.IsNull() ? wxImage() : image.ToBitmap();
							m_finalUserStatus = userStatus;
						}
						else
						{
							m_bitmap = image.IsNull() ? wxImage() : image.ToBitmap();
							m_userStatus = userStatus;
						}
					}
				}
				CallAfter(&CaptureDialog::OnStatusChanged);
				return m_forceCapture;
			}

			void CaptureDialog::OnCapture()
			{
				NThrowNotImplementedException();
			}

			void CaptureDialog::AddMediaFormats(const NArrayWrapper<NMediaFormat> &mediaFormats, const NMediaFormat &currentFormat)
			{
				if (mediaFormats.GetCount() <= 0) NThrowArgumentException("mediaFormats");
				m_suppressMediaFormatEvents = true;

				for (int i = 0; i != mediaFormats.GetCount(); i++)
				{
					NMediaFormat format = mediaFormats.Get(i);
					m_cmbFormats->Append(format.ToString(), new ObjectClientData(format));
				}

				if (!currentFormat.IsNull())
				{
					for (unsigned int i = 0; i < m_cmbFormats->GetCount(); i++)
					{
						NMediaFormat format = NObjectDynamicCast<NMediaFormat>(((ObjectClientData*)m_cmbFormats->GetClientObject(i))->GetObject());
						if (format.Equals(currentFormat))
						{
							m_cmbFormats->SetSelection(i);
							break;
						}
					}
				}
				m_suppressMediaFormatEvents = false;
			}

			// Overridden in child classes to change the media format.
			void CaptureDialog::OnMediaFormatChanged(const NMediaFormat &WXUNUSED(mediaFormat))
			{
			}

			wxRect CaptureDialog::GetPictureArea()
			{
				wxImage bmp = m_finalBitmap.IsOk() ? m_finalBitmap : m_bitmap;
				int frameWidth = !bmp.IsOk() ? 0 : bmp.GetWidth();
				int frameHeight = !bmp.IsOk() ? 0 : bmp.GetHeight();
				wxSize cs = m_captureImageWindow->GetClientSize();
				float zoom = 1;
				if (frameWidth != 0 && frameHeight != 0)
					zoom = wxMin(cs.GetWidth() / (float)frameWidth, cs.GetHeight() / (float)frameHeight);
				float sx = frameWidth * zoom;
				float sy = frameHeight * zoom;

				return wxRect((int)wxRound((cs.GetWidth() - sx) / 2), (int)wxRound((cs.GetHeight() - sy) / 2), (int)wxRound(sx), (int)wxRound(sy));
			}

			bool CaptureDialog::IsCapturing()
			{
				return m_isCapturing;
			}

			bool CaptureDialog::HasFinal()
			{
				return m_finalBitmap.IsOk();
			}

			bool CaptureDialog::IsAutoCaptureStart()
			{
				return m_autoCaptureStart;
			}

			void CaptureDialog::SetAutoCaptureStart(bool autoCaptureStart)
			{
				m_autoCaptureStart = autoCaptureStart;
			}

			bool CaptureDialog::IsEnableForcedCapture()
			{
				return m_btnForce->IsShown();
			}

			void CaptureDialog::SetEnableForcedCapture(bool show)
			{
				m_btnForce->Show(show);
				Layout();
			}

			NDevice CaptureDialog::GetDevice()
			{
				return m_device;
			}

			void CaptureDialog::SetDevice(const NDevice &device)
			{
				if (!m_device.Equals(device))
				{
					if (!m_device.IsNull())
					{
						NType deviceType = m_device.GetNativeType();
						if (!IsValidDeviceType(deviceType))
						{
							NThrowArgumentException("Invalid NDevice type");
						}
					}
					CheckIsBusy();
					m_device = device;
					CallAfter(&CaptureDialog::OnDeviceChanged);
				}
			}

			bool CaptureDialog::IsGatherImages()
			{
				return m_gatherImages;
			}

			void CaptureDialog::SetGatherImages(bool gatherImages)
			{
				if (m_gatherImages != gatherImages)
				{
					CheckIsBusy();
					m_gatherImages = gatherImages;
				}
			}

			void CaptureDialog::WriteParameter(wxXmlDocument doc, const wxString key, wxVariant parameter)
			{
				wxXmlNode *node = new  wxXmlNode(wxXML_ELEMENT_NODE, "Parameter");
				wxXmlAttribute *attribute = new  wxXmlAttribute("Name", key);
				node->SetAttributes(attribute);
				node->SetContent(parameter.GetString());
				doc.SetRoot(node);
			}

			void CaptureDialog::OnWriteScanParameters(wxXmlDocument doc)
			{
			}

			// Code Related to Capturing Thread
			CapturingThread::CapturingThread(CaptureDialog *dialog) : wxThread(wxTHREAD_JOINABLE), m_captureDialog(dialog)
			{
			}

			CapturingThread::~CapturingThread()
			{
			}

			void CaptureDialog::StartThread()
			{
				m_thread = new CapturingThread(this);
				if (m_thread->Run() != wxTHREAD_NO_ERROR)
				{
					wxLogError("Can't create the thread!");
					wxMessageBox("Can not start the capture thread", "DevicesSampleWX Error");
					m_thread = NULL;
				}
			}

			wxThread::ExitCode CapturingThread::Entry()
			{
				m_captureDialog->m_stopWatch.Start();
				m_captureDialog->OnCapture();
				return 0;
			}

			void CapturingThread::OnExit()
			{
				if (!m_captureDialog->m_autoCaptureStart)
					m_captureDialog->OnCaptureFinished();
				m_captureDialog = NULL;
			}

			void CaptureDialog::OnForceButtonClicked(wxCommandEvent &WXUNUSED(event))
			{
				m_forceCapture = true;
			}

			void CaptureDialog::OnSelectedIndexChanged(wxCommandEvent &WXUNUSED(event))
			{
				m_imageShowStarted = false;
				NMediaFormat mediaFormat = NULL;
				if (m_suppressMediaFormatEvents) return;

				int selection = m_cmbFormats->GetSelection();
				if (selection != -1)
				{
					ObjectClientData * data = static_cast<ObjectClientData*>(m_cmbFormats->GetClientObject(selection));
					mediaFormat = NObjectDynamicCast<NMediaFormat>(data->GetObject());
				}
				OnMediaFormatChanged(mediaFormat);
			}

			void CaptureDialog::OnCustomizeFormatButtonClicked(wxCommandEvent &WXUNUSED(event))
			{
				int selection = m_cmbFormats->GetSelection();
				if (selection != -1)
				{
					CustomizeFormatDialog customizeFormatDlg(this);
					ObjectClientData * data = static_cast<ObjectClientData*>(m_cmbFormats->GetClientObject(selection));
					NMediaFormat selectedFormat = NObjectDynamicCast<NMediaFormat>(data->GetObject());
					if (selectedFormat.IsNull())
					{
						NDevice device = GetDevice();
						if ((device.GetDeviceType() & ndtCamera) == ndtCamera)
							selectedFormat = NVideoFormat();
						else if ((device.GetDeviceType() & ndtMicrophone) == ndtMicrophone)
							selectedFormat = NAudioFormat();
						else NThrowNotImplementedException();
					}

					NMediaFormat customFormat = customizeFormatDlg.CustomizeFormat(selectedFormat);
					if (customizeFormatDlg.ShowModal() == wxID_OK)
					{
						if (!customFormat.Equals(selectedFormat))
						{
							int i = m_cmbFormats->Append(customFormat.ToString(), new ObjectClientData(customFormat));
							m_cmbFormats->SetSelection(i);
							wxCommandEvent ev = wxCommandEvent();
							OnSelectedIndexChanged(ev);
						}
					}
				}
			}

			wxString CaptureDialog::CreateGuid()
			{
				wxString randomString;

#ifdef WIN32
				UUID uuid;
				UuidCreate(&uuid);
				char *str = NULL;
				UuidToStringA(&uuid, (RPC_CSTR*)&str);
				randomString << str;
				RpcStringFreeA((RPC_CSTR*)&str);
#else
				uuid_t t;
				uuid_generate(t);

				char ch[37];
				memset(ch, 0, 37);
				uuid_unparse(t, ch);
				randomString << ch;
#endif
				return randomString;
			}
		}
	}
}

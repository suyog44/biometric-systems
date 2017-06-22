#include "Precompiled.h"
#include "EnrollFromCamera.h"
#include "Resources/SaveIcon.xpm"
#include "LicensePanel.h"

using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;
using namespace Neurotec::IO;
using namespace Neurotec::Devices;
using namespace Neurotec::Images;

namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(EVT_ENROLLFROMCAMERAPAGE_CAPTURE_COMPLETED);
		BEGIN_EVENT_TABLE(EnrollFromCamera, wxPanel)
			EVT_BUTTON(ID_BUTTON_SAVE_IMAGE, EnrollFromCamera::OnButtonSaveImageClick)
			EVT_BUTTON(ID_BUTTON_SAVE_TEMPLATE, EnrollFromCamera::OnButtonSaveTemplateClick)
			EVT_BUTTON(ID_BUTTON_REFRESH_SCANNERS, EnrollFromCamera::OnButtonRefreshScannersClick)
			EVT_BUTTON(ID_BUTTON_START, EnrollFromCamera::OnButtonStartClick)
			EVT_BUTTON(ID_BUTTON_STOP, EnrollFromCamera::OnButtonStopClick)
			EVT_BUTTON(ID_BUTTON_START_EXTRACTION, EnrollFromCamera::OnButtonStartExtractionClick)
			EVT_COMBOBOX(ID_COMBOBOX_CAMERAS, EnrollFromCamera::OnComboBoxCamerasChange)
			EVT_COMMAND(wxID_ANY, EVT_ENROLLFROMCAMERAPAGE_CAPTURE_COMPLETED, EnrollFromCamera::OnCaptureCompleted)
		END_EVENT_TABLE()

		EnrollFromCamera::EnrollFromCamera(wxWindow *parent, NBiometricClient &biometricClient) : wxPanel(parent), m_biometricClient(biometricClient), m_subject(NULL)
		{
			CreateGUIControls();
			UpdateCameraList();
			UpdateControls(false);
		}
		
		void EnrollFromCamera::CreateGUIControls()
		{
			wxBoxSizer *mainBox = new wxBoxSizer(wxVERTICAL);

			LicensePanel *licensePanel = new LicensePanel(this);
			licensePanel->RefreshComponentsStatus("Biometrics.FaceExtraction,Devices.Cameras", wxEmptyString);
			mainBox->Add(licensePanel, 0, wxEXPAND | wxALL, 2);

			wxStaticBoxSizer *staticBoxSizerCameras = new wxStaticBoxSizer(wxVERTICAL, this, "Cameras");
			m_comboBoxCameras = new wxComboBox(this, ID_COMBOBOX_CAMERAS, wxEmptyString, wxDefaultPosition, wxDefaultSize, 0, 0, wxCB_READONLY);
			staticBoxSizerCameras->Add(m_comboBoxCameras, 0, wxEXPAND | wxALL, 2);
			wxBoxSizer *camerasButtonSizer = new wxBoxSizer(wxHORIZONTAL);
			m_buttonRefreshList = new wxButton(this, ID_BUTTON_REFRESH_SCANNERS, "Refresh List");
			m_buttonStart = new wxButton(this, ID_BUTTON_START, "Start Capturing");
			m_buttonStop = new wxButton(this, ID_BUTTON_STOP, "Stop");
			m_checkBoxCaptureAutomatically = new wxCheckBox(this, wxID_ANY, "Capture automatically");
			m_checkBoxCheckLiveness = new wxCheckBox(this, wxID_ANY, "Check liveness");
			camerasButtonSizer->Add(m_buttonRefreshList, 0,  wxALIGN_CENTER | wxALL, 2);
			camerasButtonSizer->Add(m_buttonStart, 0, wxALIGN_CENTER | wxALL, 2);
			camerasButtonSizer->Add(m_buttonStop, 0, wxALIGN_CENTER | wxALL, 2);
			camerasButtonSizer->Add(m_checkBoxCaptureAutomatically, 0, wxALIGN_CENTER | wxALL, 2);
			camerasButtonSizer->Add(m_checkBoxCheckLiveness, 0, wxALIGN_CENTER | wxALL, 2);
			staticBoxSizerCameras->Add(camerasButtonSizer, 0, wxALL, 2);
			mainBox->Add(staticBoxSizerCameras, 0, wxEXPAND | wxALL, 2);

			wxNViewZoomSlider* zoomSlider = new wxNViewZoomSlider(this);
			m_faceView = new wxNFaceView(this);
			m_faceView->SetBackgroundColour(GetBackgroundColour());
			mainBox->Add(m_faceView, 1, wxEXPAND | wxALL, 2);

			wxBoxSizer* bottomButtonSizer = new wxBoxSizer(wxHORIZONTAL);
			m_buttonStartExtraction = new wxButton(this, ID_BUTTON_START_EXTRACTION, "Start Extraction");
			m_staticTextStatus = new wxStaticText(this, wxID_ANY, wxEmptyString);
			zoomSlider->SetView(m_faceView);
			m_buttonSaveTemplate = new wxButton(this, ID_BUTTON_SAVE_TEMPLATE, "Save Template");
			m_buttonSaveTemplate->SetBitmap(saveIcon_xpm);
			m_buttonSaveTemplate->SetToolTip("Save extracted template to a file");
			m_buttonSaveImage = new wxButton(this, ID_BUTTON_SAVE_IMAGE, "Save Image");
			m_buttonSaveImage->SetBitmap(saveIcon_xpm);
			m_buttonSaveImage->SetToolTip("Save captured face image to a file");
			bottomButtonSizer->Add(m_buttonStartExtraction, 0, wxALIGN_CENTER | wxALL, 2);
			bottomButtonSizer->Add(m_staticTextStatus, 0, wxALIGN_CENTER | wxALL, 2);
			bottomButtonSizer->AddStretchSpacer();
			bottomButtonSizer->Add(zoomSlider, 0, wxALIGN_CENTER | wxALL, 2);
			bottomButtonSizer->Add(m_buttonSaveTemplate, 0, wxALIGN_CENTER | wxALL, 2);
			bottomButtonSizer->Add(m_buttonSaveImage, 0, wxALIGN_CENTER | wxALL, 2);
			mainBox->Add(bottomButtonSizer, 0, wxEXPAND | wxALL, 2);

			SetBackgroundColour(GetParent()->GetBackgroundColour());
			SetSizer(mainBox);
		}

		void EnrollFromCamera::UpdateControls(bool capturing)
		{
			bool hasTemplate = !capturing && !m_subject.IsNull() && m_subject.GetStatus() == nbsOk;
			m_buttonSaveImage->Enable(hasTemplate);
			m_buttonSaveTemplate->Enable(hasTemplate);
			m_buttonStart->Enable(!capturing && m_comboBoxCameras->GetCount() > 0);
			m_buttonRefreshList->Enable(!capturing);
			m_buttonStop->Enable(capturing);
			m_comboBoxCameras->Enable(!capturing && m_comboBoxCameras->GetCount() > 0);
			m_buttonStartExtraction->Enable(capturing && !m_checkBoxCaptureAutomatically->IsChecked());
			m_checkBoxCaptureAutomatically->Enable(!capturing);
			m_checkBoxCheckLiveness->Enable(!capturing);
		}

		void EnrollFromCamera::OnButtonStartClick(wxCommandEvent& WXUNUSED(event))
		{
			if (m_biometricClient.GetFaceCaptureDevice().IsNull())
			{
				wxMessageBox("Please select camera from the list");
				return;
			}
			// Set face capture from stream
			m_subject = NSubject();
			NFace face;
			if (!m_checkBoxCaptureAutomatically->IsChecked())
			{
				face.SetCaptureOptions((NBiometricCaptureOptions)(nbcoStream | nbcoManual));
			}
			else
			{
				face.SetCaptureOptions(nbcoStream);
			}
			// Set face liveness check mode
			if (m_checkBoxCheckLiveness->IsChecked())
			{
				m_biometricClient.SetFacesLivenessMode(nlmPassiveAndActive);
			}
			else
			{
				m_biometricClient.SetFacesLivenessMode(nlmNone);
			}
			m_subject.GetFaces().Add(face);
			NAsyncOperation operation = m_biometricClient.CaptureAsync(m_subject);
			m_faceView->SetFace(face);
			operation.AddCompletedCallback(&EnrollFromCamera::OnCaptureCompletedCallback, this);
			m_staticTextStatus->SetLabel(wxEmptyString);
			UpdateControls(true);
		}

		void EnrollFromCamera::OnCaptureCompletedCallback(EventArgs args)
		{
			EnrollFromCamera *panel = reinterpret_cast<EnrollFromCamera*>(args.GetParam());
			wxCommandEvent ev(EVT_ENROLLFROMCAMERAPAGE_CAPTURE_COMPLETED);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void EnrollFromCamera::OnCaptureCompleted(wxCommandEvent& event)
		{
			NBiometricStatus status = m_subject.GetStatus();
			wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
			m_staticTextStatus->SetLabel(statusString);
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				if (status != nbsCanceled  && status != nbsOk)
				{
					m_staticTextStatus->SetForegroundColour(wxColour(155, 0, 0));
					m_subject.GetFaces().Get(0).SetImage(NULL);
					NAsyncOperation operation = m_biometricClient.CaptureAsync(m_subject);
					operation.AddCompletedCallback(&EnrollFromCamera::OnCaptureCompletedCallback, this);
				}
				else
				{
					m_staticTextStatus->SetForegroundColour(wxColour(0, 155, 0));
					UpdateControls(false);
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
				UpdateControls(false);
			}
		}

		void EnrollFromCamera::OnButtonStartExtractionClick(wxCommandEvent& WXUNUSED(event))
		{
			m_staticTextStatus->SetLabel("Extracting ...");
			m_biometricClient.ForceStart();
		}

		void EnrollFromCamera::UpdateCameraList()
		{
			try
			{
				m_comboBoxCameras->Clear();
				NArrayWrapper<NDevice> devices = m_biometricClient.GetDeviceManager().GetDevices().GetAll();
				for (int i = 0; i < devices.GetCount(); i++)
				{
					m_comboBoxCameras->Append(devices[i].GetDisplayName(), new wxStringClientData(devices[i].GetId()));
				}
				if (devices.GetCount() > 0)
				{
					if (m_biometricClient.GetFaceCaptureDevice().IsNull())
					{
						m_comboBoxCameras->SetSelection(0);
						m_biometricClient.SetFaceCaptureDevice(NObjectDynamicCast<NCamera>(devices[0]));
					}
					else
					{
						m_comboBoxCameras->SetSelection(m_comboBoxCameras->FindString(m_biometricClient.GetFaceCaptureDevice().GetDisplayName()));
					}
				}
			}
			catch (NError& e)
			{
				wxExceptionDlg::Show(e);
			}
			UpdateControls(false);
		}

		void EnrollFromCamera::OnButtonSaveTemplateClick(wxCommandEvent& WXUNUSED(event))
		{
			wxFileDialog wxfdSaveTemplateDialog(this, "Save Template", wxEmptyString, wxEmptyString, wxFileSelectorDefaultWildcardStr, wxFD_SAVE | wxFD_OVERWRITE_PROMPT);
			if (wxfdSaveTemplateDialog.ShowModal() == wxID_OK)
			{
				try
				{
					NFile::WriteAllBytes(wxfdSaveTemplateDialog.GetPath(), m_subject.GetTemplateBuffer());
				}
				catch (NError& e)
				{
					wxExceptionDlg::Show(e);
				}
			}
		}

		void EnrollFromCamera::OnButtonSaveImageClick(wxCommandEvent& WXUNUSED(event))
		{
			wxFileDialog wxfdSaveImageDialog(this, "Save Image", wxEmptyString, wxEmptyString, Common::GetSaveFileFilterString(), wxFD_SAVE | wxFD_OVERWRITE_PROMPT);
			if (wxfdSaveImageDialog.ShowModal() == wxID_OK)
			{
				try
				{
					NImage image = m_subject.GetFaces().Get(0).GetImage();
					image.Save(wxfdSaveImageDialog.GetPath());
				}
				catch (NError& e)
				{
					wxExceptionDlg::Show(e);
				}
			}
		}

		void EnrollFromCamera::OnButtonRefreshScannersClick(wxCommandEvent& WXUNUSED(event))
		{
			UpdateCameraList();
		}

		void EnrollFromCamera::OnButtonStopClick(wxCommandEvent& WXUNUSED(event))
		{
			m_biometricClient.Cancel();
			UpdateControls(false);
		}

		void EnrollFromCamera::OnComboBoxCamerasChange(wxCommandEvent& WXUNUSED(event))
		{
			NDevice device = NULL;
			if (m_comboBoxCameras->GetSelection() >= 0)
			{
				wxStringClientData* clientData = reinterpret_cast<wxStringClientData *>(m_comboBoxCameras->GetClientObject(m_comboBoxCameras->GetSelection()));
				if (clientData != NULL)
				{
					wxString scannerId = clientData->GetData();
					NDeviceManager::NDeviceCollection devices = m_biometricClient.GetDeviceManager().GetDevices();
					device = devices.Get(scannerId);
				}
			}
			m_biometricClient.SetFaceCaptureDevice(NObjectDynamicCast<NCamera>(device));
		}
	}
}

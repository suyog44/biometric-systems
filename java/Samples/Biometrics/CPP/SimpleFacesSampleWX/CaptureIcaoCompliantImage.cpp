#include "Precompiled.h"
#include "CaptureIcaoCompliantImage.h"
#include "LicensePanel.h"
#include "Resources/SaveIcon.xpm"

using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;
using namespace Neurotec::IO;
using namespace Neurotec::Devices;
using namespace Neurotec::Images;

namespace Neurotec { namespace Samples {

DEFINE_EVENT_TYPE(EVT_ICAO_CAPTURE);
BEGIN_EVENT_TABLE(CaptureIcaoCompliantImage, wxPanel)
	EVT_BUTTON(ID_BUTTON_SAVE_IMAGE, CaptureIcaoCompliantImage::OnButtonSaveImageClick)
	EVT_BUTTON(ID_BUTTON_SAVE_TEMPLATE, CaptureIcaoCompliantImage::OnButtonSaveTemplateClick)
	EVT_BUTTON(ID_BUTTON_REFRESH_SCANNERS, CaptureIcaoCompliantImage::OnButtonRefreshScannersClick)
	EVT_BUTTON(ID_BUTTON_START, CaptureIcaoCompliantImage::OnButtonStartClick)
	EVT_BUTTON(ID_BUTTON_STOP, CaptureIcaoCompliantImage::OnButtonStopClick)
	EVT_BUTTON(ID_BUTTON_FORCE, CaptureIcaoCompliantImage::OnButtonForceClick)
	EVT_COMBOBOX(ID_COMBOBOX_CAMERAS, CaptureIcaoCompliantImage::OnComboBoxCamerasChange)
	EVT_COMMAND(wxID_ANY, EVT_ICAO_CAPTURE, CaptureIcaoCompliantImage::OnCaptureCompleted)
END_EVENT_TABLE()

CaptureIcaoCompliantImage::CaptureIcaoCompliantImage(wxWindow *parent, NBiometricClient &biometricClient)
	: wxPanel(parent),
	m_biometricClient(biometricClient),
	m_subject(NULL),
	m_segmentedFace(NULL)
{
	CreateGUIControls();
	UpdateCameraList();
	UpdateControls(false);
}

CaptureIcaoCompliantImage::~CaptureIcaoCompliantImage()
{
	m_faceView->SetFace(NULL);
	m_icaoView->SetFace(NULL);
}

void CaptureIcaoCompliantImage::CreateGUIControls()
{
	wxBoxSizer *mainBox = new wxBoxSizer(wxVERTICAL);

	LicensePanel *licensePanel = new LicensePanel(this);
	licensePanel->RefreshComponentsStatus("Biometrics.FaceExtraction,Biometrics.FaceSegmentsDetection,Devices.Cameras", wxEmptyString);
	mainBox->Add(licensePanel, 0, wxEXPAND | wxALL, 2);

	wxStaticBoxSizer *staticBoxSizerCameras = new wxStaticBoxSizer(wxVERTICAL, this, "Cameras");
	m_comboBoxCameras = new wxComboBox(this, ID_COMBOBOX_CAMERAS, wxEmptyString, wxDefaultPosition, wxDefaultSize, 0, 0, wxCB_READONLY);
	staticBoxSizerCameras->Add(m_comboBoxCameras, 0, wxEXPAND | wxALL, 2);
	wxBoxSizer *camerasButtonSizer = new wxBoxSizer(wxHORIZONTAL);
	m_buttonRefreshList = new wxButton(this, ID_BUTTON_REFRESH_SCANNERS, "Refresh List");
	m_buttonStart = new wxButton(this, ID_BUTTON_START, "Start Capturing");
	m_buttonStop = new wxButton(this, ID_BUTTON_STOP, "Stop");
	camerasButtonSizer->Add(m_buttonRefreshList, 0,  wxALIGN_CENTER | wxALL, 2);
	camerasButtonSizer->Add(m_buttonStart, 0, wxALIGN_CENTER | wxALL, 2);
	camerasButtonSizer->Add(m_buttonStop, 0, wxALIGN_CENTER | wxALL, 2);
	staticBoxSizerCameras->Add(camerasButtonSizer, 0, wxALL, 2);
	mainBox->Add(staticBoxSizerCameras, 0, wxEXPAND | wxALL, 2);

	wxBoxSizer * centerBox = new wxBoxSizer(wxHORIZONTAL);
	m_icaoView = new IcaoWarningsView(this);
	centerBox->Add(m_icaoView, 0, wxEXPAND | wxALL, 2);

	wxNViewZoomSlider* zoomSlider = new wxNViewZoomSlider(this);
	m_faceView = new wxNFaceView(this);
	m_faceView->SetBackgroundColour(GetBackgroundColour());
	m_faceView->SetShowAge(false);
	m_faceView->SetShowEmotions(false);
	m_faceView->SetShowExpression(false);
	m_faceView->SetShowGender(false);
	m_faceView->SetShowProperties(false);
	centerBox->Add(m_faceView, 1, wxEXPAND | wxALL, 2);

	mainBox->Add(centerBox, 1, wxEXPAND | wxALL, 2);

	wxBoxSizer* bottomButtonSizer = new wxBoxSizer(wxHORIZONTAL);
	m_buttonForce = new wxButton(this, ID_BUTTON_FORCE, "Force");
	m_staticTextStatus = new wxStaticText(this, wxID_ANY, wxEmptyString);
	zoomSlider->SetView(m_faceView);
	m_buttonSaveTemplate = new wxButton(this, ID_BUTTON_SAVE_TEMPLATE, "Save Template");
	m_buttonSaveTemplate->SetBitmap(saveIcon_xpm);
	m_buttonSaveTemplate->SetToolTip("Save extracted template to a file");
	m_buttonSaveImage = new wxButton(this, ID_BUTTON_SAVE_IMAGE, "Save Image");
	m_buttonSaveImage->SetBitmap(saveIcon_xpm);
	m_buttonSaveImage->SetToolTip("Save captured face image to a file");
	bottomButtonSizer->Add(m_buttonForce, 0, wxALIGN_CENTER | wxALL, 2);
	bottomButtonSizer->Add(m_staticTextStatus, 0, wxALIGN_CENTER | wxALL, 2);
	bottomButtonSizer->AddStretchSpacer();
	bottomButtonSizer->Add(zoomSlider, 0, wxALIGN_CENTER | wxALL, 2);
	bottomButtonSizer->Add(m_buttonSaveTemplate, 0, wxALIGN_CENTER | wxALL, 2);
	bottomButtonSizer->Add(m_buttonSaveImage, 0, wxALIGN_CENTER | wxALL, 2);
	mainBox->Add(bottomButtonSizer, 0, wxEXPAND | wxALL, 2);

	SetBackgroundColour(GetParent()->GetBackgroundColour());
	SetSizer(mainBox);
}

void CaptureIcaoCompliantImage::UpdateControls(bool capturing)
{
	bool hasTemplate = !capturing && !m_subject.IsNull() && m_subject.GetStatus() == nbsOk;
	m_buttonSaveImage->Enable(hasTemplate);
	m_buttonSaveTemplate->Enable(hasTemplate);
	m_buttonStart->Enable(!capturing && m_comboBoxCameras->GetCount() > 0);
	m_buttonRefreshList->Enable(!capturing);
	m_buttonForce->Enable(capturing);
	m_buttonStop->Enable(capturing);
	m_comboBoxCameras->Enable(!capturing && m_comboBoxCameras->GetCount() > 0);
}

void CaptureIcaoCompliantImage::OnButtonStartClick(wxCommandEvent& WXUNUSED(event))
{
	if (m_biometricClient.GetFaceCaptureDevice().IsNull())
	{
		wxMessageBox("Please select camera from the list");
		return;
	}
	
	// Set face capture from stream
	m_subject = NSubject();
	NFace face;
	face.SetCaptureOptions(nbcoStream);
	m_subject.GetFaces().Add(face);

	m_biometricClient.SetFacesCheckIcaoCompliance(true);

	m_faceView->SetFace(face);
	m_icaoView->SetFace(face);

	NBiometricTask task = m_biometricClient.CreateTask((NBiometricOperations)(nboCapture | nboSegment | nboCreateTemplate), m_subject);
	NAsyncOperation operation = m_biometricClient.PerformTaskAsync(task);
	operation.AddCompletedCallback(&CaptureIcaoCompliantImage::OnCaptureCompletedCallback, this);

	m_staticTextStatus->SetLabel(wxEmptyString);
	UpdateControls(true);
}

void CaptureIcaoCompliantImage::OnCaptureCompletedCallback(EventArgs args)
{
	CaptureIcaoCompliantImage *panel = reinterpret_cast<CaptureIcaoCompliantImage*>(args.GetParam());
	wxCommandEvent ev(EVT_ICAO_CAPTURE);
	ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
	wxPostEvent(panel, ev);
}

void CaptureIcaoCompliantImage::OnCaptureCompleted(wxCommandEvent& event)
{
	NBiometricStatus status = m_subject.GetStatus();
	wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
	m_staticTextStatus->SetLabel(statusString);
	NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
	if (operation.GetError().IsNull())
	{
		if (status == nbsOk)
		{
			m_segmentedFace = m_subject.GetFaces()[1];
			m_faceView->SetFace(m_segmentedFace);
			m_icaoView->SetFace(m_segmentedFace);
		}

		m_staticTextStatus->SetLabel(NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status));
		m_staticTextStatus->SetForegroundColour(status == nbsOk ? wxColor(0x00, 0x64, 0x00) : *wxRED);
		UpdateControls(false);
	}
	else
	{
		wxExceptionDlg::Show(operation.GetError());
		m_staticTextStatus->SetLabel(wxEmptyString);
		UpdateControls(false);
	}
}

void CaptureIcaoCompliantImage::OnButtonForceClick(wxCommandEvent& WXUNUSED(event))
{
	m_biometricClient.Force();
}

void CaptureIcaoCompliantImage::UpdateCameraList()
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

void CaptureIcaoCompliantImage::OnButtonSaveTemplateClick(wxCommandEvent& WXUNUSED(event))
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

void CaptureIcaoCompliantImage::OnButtonSaveImageClick(wxCommandEvent& WXUNUSED(event))
{
	wxFileDialog wxfdSaveImageDialog(this, "Save Image", wxEmptyString, wxEmptyString, Common::GetSaveFileFilterString(), wxFD_SAVE | wxFD_OVERWRITE_PROMPT);
	if (wxfdSaveImageDialog.ShowModal() == wxID_OK)
	{
		try
		{
			NImage image = m_segmentedFace.GetImage();
			image.Save(wxfdSaveImageDialog.GetPath());
		}
		catch (NError& e)
		{
			wxExceptionDlg::Show(e);
		}
	}
}

void CaptureIcaoCompliantImage::OnButtonRefreshScannersClick(wxCommandEvent& WXUNUSED(event))
{
	UpdateCameraList();
}

void CaptureIcaoCompliantImage::OnButtonStopClick(wxCommandEvent& WXUNUSED(event))
{
	m_biometricClient.Cancel();
	UpdateControls(false);
}

void CaptureIcaoCompliantImage::OnComboBoxCamerasChange(wxCommandEvent& WXUNUSED(event))
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

}}

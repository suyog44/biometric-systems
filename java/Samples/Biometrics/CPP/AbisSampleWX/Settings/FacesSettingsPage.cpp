#include "Precompiled.h"

#include <Settings/FacesSettingsPage.h>
#include <Settings/ConnectToDeviceForm.h>
#include <Settings/SettingsManager.h>
#include <Common/LicensingTools.h>

using namespace ::Neurotec::Biometrics;
using namespace ::Neurotec::Biometrics::Client;
using namespace ::Neurotec::Devices;
using namespace ::Neurotec::Media;
using namespace ::Neurotec::Gui;

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_FACE_SETTINGS_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_FACE_SETTINGS_THREAD, wxCommandEvent);

FacesSettingsPage::FacesSettingsPage(wxWindow *parent, wxWindowID winid) : BaseSettingsPage(parent, winid)
{
	CreateGUIControls();
	RegisterGuiEvents();
}

FacesSettingsPage::~FacesSettingsPage()
{
	m_biometricClient.GetDeviceManager().GetDevices().RemoveCollectionChangedCallback(&FacesSettingsPage::OnDevicesCollectionChanged, this);
	UnregisterGuiEvents();
}

int FacesSettingsPage::CheckIfSelectedDeviceIsDisconnectable()
{
	try
	{
		NDeviceManager deviceManager = m_biometricClient.GetDeviceManager();
		wxStringClientData * clientData = reinterpret_cast<wxStringClientData*>(m_choiceCamera->GetClientObject(m_choiceCamera->GetSelection()));
		if (clientData != NULL)
		{
			int i;
			for (i = 0; i < deviceManager.GetDevices().GetCount(); i++)
			{
				NDevice device = deviceManager.GetDevices().Get(i);
				if ((wxString)device.GetId() == clientData->GetData())
				{
					if(device.IsDisconnectable())
						m_btnDisconnect->Enable();
					else
						m_btnDisconnect->Disable();
					m_biometricClient.SetFaceCaptureDevice(NObjectDynamicCast<NCamera>(device));
					return i;
				}
			}
			if(i == 0)
				m_btnDisconnect->Disable();
		}
	}
	catch(NError& ex)
	{
		wxExceptionDlg::Show(ex);
		return -1;
	}
	return -1;
}

void FacesSettingsPage::Initialize(NBiometricClient biometricClient)
{
	BaseSettingsPage::Initialize(biometricClient);
	NDeviceManager deviceManager = m_biometricClient.GetDeviceManager();
	deviceManager.GetDevices().AddCollectionChangedCallback(&FacesSettingsPage::OnDevicesCollectionChanged, this);

	bool isActivated = LicensingTools::CanDetectFaceSegments(biometricClient.GetLocalOperations());
	if (!isActivated)
	{
		m_chkDetectAllFeaturePoints->Enable(false);
		m_chkDetectBaseFeaturePoints->Enable(false);
		m_chkDetermineGender->Enable(false);
		m_chkDetermineAge->Enable(false);
		m_chkRecognizeEmotion->Enable(false);
		m_chkDetectProperties->Enable(false);
		m_chkRecognizeExpression->Enable(false);

		m_chkDetectAllFeaturePoints->SetLabel(m_chkDetectAllFeaturePoints->GetLabel() + wxT(" (Not activated)"));
		m_chkDetectBaseFeaturePoints->SetLabel(m_chkDetectBaseFeaturePoints->GetLabel() + wxT(" (Not activated)"));
		m_chkDetermineGender->SetLabel(m_chkDetermineGender->GetLabel() + wxT(" (Not activated)"));
		m_chkDetermineAge->SetLabel(m_chkDetermineAge->GetLabel() + wxT(" (Not activated)"));
		m_chkRecognizeEmotion->SetLabel(m_chkRecognizeEmotion->GetLabel() + wxT(" (Not activated)"));
		m_chkDetectProperties->SetLabel(m_chkDetectProperties->GetLabel() + wxT(" (Not activated)"));
		m_chkRecognizeExpression->SetLabel(m_chkRecognizeExpression->GetLabel() + wxT(" (Not activated)"));
	}
}

void FacesSettingsPage::OnThread(wxCommandEvent& event)
{
	int id = event.GetId();

	switch(id)
	{
	case ID_EVT_UPDATE_DEVICES:
		{
			UpdateDevicesList();

			wxCommandEvent cmdEvent(wxEVT_CHOICE, m_choiceCamera->GetId());
			wxPostEvent(m_choiceCamera, cmdEvent);
			break;
		}
	default:
		break;
	};
}

void FacesSettingsPage::UpdateDevicesList()
{
	m_choiceCamera->Clear();

	NDeviceManager::NDeviceCollection devices = m_biometricClient.GetDeviceManager().GetDevices();

	for (int i = 0; i < devices.GetCount(); i++)
	{
		NDevice device = devices.Get(i);

		if (device.GetDeviceType() & ndtCamera)
		{
			m_choiceCamera->Append(device.GetDisplayName(), new wxStringClientData(device.GetId()));
		}
	}

	NCamera selectedCamera = m_biometricClient.GetFaceCaptureDevice();
	if (selectedCamera.IsNull())
	{
		m_btnDisconnect->Disable();
		return;
	}

	for (unsigned int i = 0; i < m_choiceCamera->GetCount(); i++)
	{
		wxStringClientData * selectionData = reinterpret_cast<wxStringClientData *>(m_choiceCamera->GetClientObject(i));
		if (selectionData == NULL) continue;

		wxString id = selectionData->GetData();
		if ((wxString)selectedCamera.GetId() == id)
		{
			m_choiceCamera->SetSelection(i);
			break;
		}
	}

	CheckIfSelectedDeviceIsDisconnectable();
}

void FacesSettingsPage::UpdateCaptureFormats()
{
	m_choiceFormat->Clear();

	NCamera camera = m_biometricClient.GetFaceCaptureDevice();
	if (camera.IsNull())
	{
		m_choiceFormat->Clear();
		return;
	}

	NArrayWrapper<NMediaFormat> formats = camera.GetFormats();
	for (int i = 0; i < formats.GetCount(); i++)
	{
		m_choiceFormat->Append(formats[i].ToString());
	}

	if (camera.GetCurrentFormat().IsNull())
		return;

	m_choiceFormat->SetStringSelection(camera.GetCurrentFormat().ToString());
}

void FacesSettingsPage::Load()
{
	m_choiceTemplateSize->SetStringSelection(NEnum::ToString(NBiometricEngineTypes::NTemplateSizeNativeTypeOf(), m_biometricClient.GetFacesTemplateSize()));
	m_choiceMatchingSpeed->SetStringSelection(NEnum::ToString(NBiometricEngineTypes::NMatchingSpeedNativeTypeOf(), m_biometricClient.GetFacesMatchingSpeed()));
	m_spinMinimalInterOcularDistance->SetValue(m_biometricClient.GetFacesMinimalInterOcularDistance());
	m_spinConfidenceThreshold->SetValue(m_biometricClient.GetFacesConfidenceThreshold());
	m_spinMaximalRoll->SetValue(m_biometricClient.GetFacesMaximalRoll());
	m_spinMaximalYaw->SetValue(m_biometricClient.GetFacesMaximalYaw());
	m_spinQualityThreshold->SetValue(m_biometricClient.GetFacesQualityThreshold());
	m_choiceLivenessMode->SetStringSelection(NEnum::ToString(NBiometricTypes::NLivenessModeNativeTypeOf(), m_biometricClient.GetFacesLivenessMode()));
	m_spinLivenessThreshold->SetValue(m_biometricClient.GetFacesLivenessThreshold());
	m_chkDetectAllFeaturePoints->SetValue(m_biometricClient.GetFacesDetectAllFeaturePoints() && m_chkDetectAllFeaturePoints->IsEnabled());
	m_chkDetectBaseFeaturePoints->SetValue(m_biometricClient.GetFacesDetectBaseFeaturePoints() && m_chkDetectBaseFeaturePoints->IsEnabled());
	m_chkDetermineGender->SetValue(m_biometricClient.GetFacesDetermineGender() && m_chkDetermineGender->IsEnabled());
	m_chkDetermineAge->SetValue(m_biometricClient.GetFacesDetermineAge() && m_chkDetermineAge->IsEnabled());
	m_chkDetectProperties->SetValue(m_biometricClient.GetFacesDetectProperties() && m_chkDetectProperties->IsEnabled());
	m_chkRecognizeExpression->SetValue(m_biometricClient.GetFacesRecognizeExpression() && m_chkRecognizeExpression->IsEnabled());
	m_chkRecognizeEmotion->SetValue(m_biometricClient.GetFacesRecognizeEmotion() && m_chkRecognizeEmotion->IsEnabled());
	m_chkCreateThumbnailImage->SetValue(m_biometricClient.GetFacesCreateThumbnailImage());
	m_spinWidth->Enable(m_chkCreateThumbnailImage->IsChecked());
	m_spinWidth->SetValue(m_biometricClient.GetFacesThumbnailImageWidth());
	m_spinGenRecordCount->SetValue(SettingsManager::GetFacesGeneralizationRecordCount());

	try
	{
		UpdateDevicesList();
		UpdateCaptureFormats();
	}
	catch(NError & error)
	{
		wxExceptionDlg::Show(error);
	}
}

void FacesSettingsPage::Reset()
{
	m_biometricClient.ResetProperty(wxT("Faces.TemplateSize"));
	m_biometricClient.ResetProperty(wxT("Faces.MatchingSpeed"));
	m_biometricClient.ResetProperty(wxT("Faces.MinimalInterOcularDistance"));
	m_biometricClient.ResetProperty(wxT("Faces.ConfidenceThreshold"));
	m_biometricClient.ResetProperty(wxT("Faces.MaximalRoll"));
	m_biometricClient.ResetProperty(wxT("Faces.MaximalYaw"));
	m_biometricClient.ResetProperty(wxT("Faces.QualityThreshold"));
	m_biometricClient.ResetProperty(wxT("Faces.QualityThreshold"));
	m_biometricClient.ResetProperty(wxT("Faces.LivenessThreshold"));
	m_biometricClient.SetFacesDetectAllFeaturePoints(m_chkDetectAllFeaturePoints->IsEnabled());
	m_biometricClient.ResetProperty(wxT("Faces.DetectBaseFeaturePoints"));
	m_biometricClient.SetFacesDetermineGender(m_chkDetermineGender->IsEnabled());
	m_biometricClient.SetFacesDetermineAge(m_chkDetermineAge->IsEnabled());
	m_biometricClient.SetFacesDetectProperties(m_chkDetectProperties->IsEnabled());
	m_biometricClient.SetFacesRecognizeExpression(m_chkRecognizeExpression->IsEnabled());
	m_biometricClient.SetFacesRecognizeEmotion(m_chkRecognizeEmotion->IsEnabled());
	m_biometricClient.ResetProperty(wxT("Faces.ThumbnailImageWidth"));
	m_biometricClient.SetFacesCreateThumbnailImage(true);

	Load();

	m_spinGenRecordCount->SetValue(3);
	wxSpinEvent empty;
	OnGeneralizationRecordCountChnaged(empty);
}

void FacesSettingsPage::OnCameraChanged(wxCommandEvent&)
{	
	CheckIfSelectedDeviceIsDisconnectable();
	UpdateCaptureFormats();
}

void FacesSettingsPage::OnConnectButtonClicked(wxCommandEvent& e)
{
	NDeviceManager deviceManager = m_biometricClient.GetDeviceManager();
	ConnectToDeviceForm * connectForm = new ConnectToDeviceForm(this);
	if(connectForm->ShowModal() == wxID_OK)
	{
		NDevice * newDevice = NULL;
		try
		{
			newDevice = new NDevice(deviceManager.ConnectToDevice(connectForm->GetSelectedPlugin(), connectForm->GetProperties()));
			UpdateDevicesList();
			m_choiceCamera->SetSelection(m_choiceCamera->GetCount() - 1);

			if(!m_choiceCamera->HasClientUntypedData())
			{
				wxExceptionDlg::Show(wxT("Failed to create connection to device using specified connection details"));
				return;
			}

			OnCameraChanged(e);

			int deviceId = CheckIfSelectedDeviceIsDisconnectable();
			if(newDevice->GetId() != deviceManager.GetDevices().Get(deviceId).GetId())
			{
				if(newDevice != NULL)
				{
					deviceManager.DisconnectFromDevice(*newDevice);
					delete newDevice;
				}

				wxExceptionDlg::Show("Failed to create connection to device using specified connection details");
			}
			delete newDevice;
		}
		catch(NError& er)
		{
			if(newDevice != NULL)
			{
				deviceManager.DisconnectFromDevice(*newDevice);
				delete newDevice;
			}

			wxExceptionDlg::Show(er);
		}
	}
}

void FacesSettingsPage::OnDisconnectButtonClicked(wxCommandEvent& e)
{
	try
	{
		NDeviceManager deviceManager = m_biometricClient.GetDeviceManager();
		wxStringClientData * cameraId = reinterpret_cast<wxStringClientData *>(m_choiceCamera->GetClientObject(m_choiceCamera->GetSelection()));

		if (cameraId != NULL)
		{
			for (int i = 0; i < deviceManager.GetDevices().GetCount(); i++)
			{
				NDevice device = deviceManager.GetDevices().Get(i);
				if ((wxString)device.GetId() == cameraId->GetData())
				{
					deviceManager.DisconnectFromDevice(device);
					UpdateDevicesList();
					OnCameraChanged(e);
					break;
				}
			}
		}
	}
	catch(NError& er)
	{
		wxExceptionDlg::Show(er);
	}
}

void FacesSettingsPage::OnFormatChanged(wxCommandEvent&)
{
	NCamera camera = m_biometricClient.GetFaceCaptureDevice();
	if (camera.IsNull()) return;

	wxString selection = m_choiceFormat->GetStringSelection();
	if (selection.IsEmpty()) return;

	try
	{
		NArrayWrapper<NMediaFormat> formats = camera.GetFormats();

		for (int i = 0; i < formats.GetCount(); i++)
		{
			NMediaFormat format = formats[i];
			if ((wxString)format.ToString() == selection)
			{
				try
				{
					camera.SetCurrentFormat(format);
				}
				catch(NError& ex)
				{
					wxExceptionDlg::Show(ex);
				}

				break;
			}
		}
	}
	catch(NError& ex)
	{
		wxExceptionDlg::Show(ex);
	}
}

void FacesSettingsPage::OnTemplateSizeChanged(wxCommandEvent&)
{
	m_biometricClient.SetFacesTemplateSize((NTemplateSize)NEnum::Parse(NBiometricEngineTypes::NTemplateSizeNativeTypeOf(), m_choiceTemplateSize->GetStringSelection()));
}

void FacesSettingsPage::OnMatchingSpeedChanged(wxCommandEvent&)
{
	m_biometricClient.SetFacesMatchingSpeed((NMatchingSpeed)NEnum::Parse(NBiometricEngineTypes::NMatchingSpeedNativeTypeOf(), m_choiceMatchingSpeed->GetStringSelection()));
}

void FacesSettingsPage::OnMinimalInterOcularDistanceChanged(wxSpinEvent&)
{
	m_biometricClient.SetFacesMinimalInterOcularDistance(m_spinMinimalInterOcularDistance->GetValue());
}

void FacesSettingsPage::OnConfidenceThresholdChanged(wxSpinEvent&)
{
	m_biometricClient.SetFacesConfidenceThreshold(m_spinConfidenceThreshold->GetValue());
}

void FacesSettingsPage::OnMaximalRollChanged(wxSpinEvent&)
{
	m_biometricClient.SetFacesMaximalRoll(m_spinMaximalRoll->GetValue());
}

void FacesSettingsPage::OnMaximalYawChanged(wxSpinEvent&)
{
	m_biometricClient.SetFacesMaximalYaw(m_spinMaximalYaw->GetValue());
}

void FacesSettingsPage::OnQualityThresholdChanged(wxSpinEvent&)
{
	m_biometricClient.SetFacesQualityThreshold(m_spinQualityThreshold->GetValue());
}

void FacesSettingsPage::OnLivenessModeChanged(wxCommandEvent&)
{
	NLivenessMode mode = (NLivenessMode)NEnum::Parse(NBiometricTypes::NLivenessModeNativeTypeOf(), m_choiceLivenessMode->GetStringSelection());
	m_biometricClient.SetFacesLivenessMode(mode);
	m_spinLivenessThreshold->Enable(mode != nlmNone);
}

void FacesSettingsPage::OnLivenessThresholdChanged(wxSpinEvent&)
{
	m_biometricClient.SetFacesLivenessThreshold(m_spinLivenessThreshold->GetValue());
}

void FacesSettingsPage::OnDetectAllFeaturePointsChanged(wxCommandEvent&)
{
	m_biometricClient.SetFacesDetectAllFeaturePoints(m_chkDetectAllFeaturePoints->GetValue());
}

void FacesSettingsPage::OnDetectBaseFeaturePointsChanged(wxCommandEvent&)
{
	m_biometricClient.SetFacesDetectBaseFeaturePoints(m_chkDetectBaseFeaturePoints->GetValue());
}

void FacesSettingsPage::OnDetermineGenderChanged(wxCommandEvent&)
{
	m_biometricClient.SetFacesDetermineGender(m_chkDetermineGender->GetValue());
}

void FacesSettingsPage::OnDetermineAgeChanged(wxCommandEvent&)
{
	m_biometricClient.SetFacesDetermineAge(m_chkDetermineAge->GetValue());
}

void FacesSettingsPage::OnDetectPropertiesChanged(wxCommandEvent&)
{
	m_biometricClient.SetFacesDetectProperties(m_chkDetectProperties->GetValue());
}

void FacesSettingsPage::OnRecognizeExpressionChanged(wxCommandEvent&)
{
	m_biometricClient.SetFacesRecognizeExpression(m_chkRecognizeExpression->GetValue());
}

void FacesSettingsPage::OnRecognizeEmotionChanged(wxCommandEvent&)
{
	m_biometricClient.SetFacesRecognizeEmotion(m_chkRecognizeEmotion->GetValue());
}

void FacesSettingsPage::OnCreateThumbnailImageChanged(wxCommandEvent&)
{
	m_biometricClient.SetFacesCreateThumbnailImage(m_chkCreateThumbnailImage->GetValue());
	m_spinWidth->Enable(m_chkCreateThumbnailImage->IsChecked());
}

void FacesSettingsPage::OnWidthChanged(wxSpinEvent&)
{
	m_biometricClient.SetFacesThumbnailWidth(m_spinWidth->GetValue());
}

void FacesSettingsPage::OnDevicesCollectionChanged(Collections::CollectionChangedEventArgs<NDevice> args)
{
	FacesSettingsPage *faceSettingsPage = reinterpret_cast<FacesSettingsPage *>(args.GetParam());
	wxCommandEvent event(wxEVT_FACE_SETTINGS_THREAD, ID_EVT_UPDATE_DEVICES);
	wxPostEvent(faceSettingsPage, event);
}

void FacesSettingsPage::OnGeneralizationRecordCountChnaged(wxSpinEvent&)
{
	SettingsManager::SetFacesGeneralizationRecordCount(m_spinGenRecordCount->GetValue());
}

void FacesSettingsPage::RegisterGuiEvents()
{
	this->Bind(wxEVT_FACE_SETTINGS_THREAD, &FacesSettingsPage::OnThread, this);

	m_choiceCamera->Connect(wxEVT_CHOICE, wxCommandEventHandler(FacesSettingsPage::OnCameraChanged), NULL, this);
	m_btnConnect->Connect(wxEVT_BUTTON, wxCommandEventHandler(FacesSettingsPage::OnConnectButtonClicked), NULL, this);
	m_btnDisconnect->Connect(wxEVT_BUTTON, wxCommandEventHandler(FacesSettingsPage::OnDisconnectButtonClicked), NULL, this);
	m_choiceFormat->Connect(wxEVT_CHOICE, wxCommandEventHandler(FacesSettingsPage::OnFormatChanged), NULL, this);
	m_choiceTemplateSize->Connect(wxEVT_CHOICE, wxCommandEventHandler(FacesSettingsPage::OnTemplateSizeChanged), NULL, this);
	m_choiceMatchingSpeed->Connect(wxEVT_CHOICE, wxCommandEventHandler(FacesSettingsPage::OnMatchingSpeedChanged), NULL, this);
	m_spinMinimalInterOcularDistance->Connect(wxEVT_SPINCTRL, wxSpinEventHandler(FacesSettingsPage::OnMinimalInterOcularDistanceChanged), NULL, this);
	m_spinConfidenceThreshold->Connect(wxEVT_SPINCTRL, wxSpinEventHandler(FacesSettingsPage::OnConfidenceThresholdChanged), NULL, this);
	m_spinMaximalRoll->Connect(wxEVT_SPINCTRL, wxSpinEventHandler(FacesSettingsPage::OnMaximalRollChanged), NULL, this);
	m_spinMaximalYaw->Connect(wxEVT_SPINCTRL, wxSpinEventHandler(FacesSettingsPage::OnMaximalYawChanged), NULL, this);
	m_spinQualityThreshold->Connect(wxEVT_SPINCTRL, wxSpinEventHandler(FacesSettingsPage::OnQualityThresholdChanged), NULL, this);
	m_choiceLivenessMode->Connect(wxEVT_CHOICE, wxCommandEventHandler(FacesSettingsPage::OnLivenessModeChanged), NULL, this);
	m_spinLivenessThreshold->Connect(wxEVT_SPINCTRL, wxSpinEventHandler(FacesSettingsPage::OnLivenessThresholdChanged), NULL, this);
	m_chkDetectAllFeaturePoints->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(FacesSettingsPage::OnDetectAllFeaturePointsChanged), NULL, this);
	m_chkDetectBaseFeaturePoints->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(FacesSettingsPage::OnDetectBaseFeaturePointsChanged), NULL, this);
	m_chkDetermineGender->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(FacesSettingsPage::OnDetermineGenderChanged), NULL, this);
	m_chkDetermineAge->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(FacesSettingsPage::OnDetermineAgeChanged), NULL, this);
	m_chkDetectProperties->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(FacesSettingsPage::OnDetectPropertiesChanged), NULL, this);
	m_chkRecognizeExpression->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(FacesSettingsPage::OnRecognizeExpressionChanged), NULL, this);
	m_chkRecognizeEmotion->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(FacesSettingsPage::OnRecognizeEmotionChanged), NULL, this);
	m_chkCreateThumbnailImage->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(FacesSettingsPage::OnCreateThumbnailImageChanged), NULL, this);
	m_spinWidth->Connect(wxEVT_SPINCTRL, wxSpinEventHandler(FacesSettingsPage::OnWidthChanged), NULL, this);
	m_spinGenRecordCount->Connect(wxEVT_SPINCTRL, wxSpinEventHandler(FacesSettingsPage::OnGeneralizationRecordCountChnaged), NULL, this);
}

void FacesSettingsPage::UnregisterGuiEvents()
{
	this->Unbind(wxEVT_FACE_SETTINGS_THREAD, &FacesSettingsPage::OnThread, this);

	m_choiceCamera->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(FacesSettingsPage::OnCameraChanged), NULL, this);
	m_btnConnect->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(FacesSettingsPage::OnConnectButtonClicked), NULL, this);
	m_btnDisconnect->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(FacesSettingsPage::OnDisconnectButtonClicked), NULL, this);
	m_choiceFormat->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(FacesSettingsPage::OnFormatChanged), NULL, this);
	m_choiceTemplateSize->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(FacesSettingsPage::OnTemplateSizeChanged), NULL, this);
	m_choiceMatchingSpeed->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(FacesSettingsPage::OnMatchingSpeedChanged), NULL, this);
	m_spinMinimalInterOcularDistance->Disconnect(wxEVT_SPINCTRL, wxSpinEventHandler(FacesSettingsPage::OnMinimalInterOcularDistanceChanged), NULL, this);
	m_spinConfidenceThreshold->Disconnect(wxEVT_SPINCTRL, wxSpinEventHandler(FacesSettingsPage::OnConfidenceThresholdChanged), NULL, this);
	m_spinMaximalRoll->Disconnect(wxEVT_SPINCTRL, wxSpinEventHandler(FacesSettingsPage::OnMaximalRollChanged), NULL, this);
	m_spinMaximalYaw->Disconnect(wxEVT_SPINCTRL, wxSpinEventHandler(FacesSettingsPage::OnMaximalYawChanged), NULL, this);
	m_spinQualityThreshold->Disconnect(wxEVT_SPINCTRL, wxSpinEventHandler(FacesSettingsPage::OnQualityThresholdChanged), NULL, this);
	m_choiceLivenessMode->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(FacesSettingsPage::OnLivenessModeChanged), NULL, this);
	m_spinLivenessThreshold->Disconnect(wxEVT_SPINCTRL, wxSpinEventHandler(FacesSettingsPage::OnLivenessThresholdChanged), NULL, this);
	m_chkDetectAllFeaturePoints->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(FacesSettingsPage::OnDetectAllFeaturePointsChanged), NULL, this);
	m_chkDetectBaseFeaturePoints->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(FacesSettingsPage::OnDetectBaseFeaturePointsChanged), NULL, this);
	m_chkDetermineGender->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(FacesSettingsPage::OnDetermineGenderChanged), NULL, this);
	m_chkDetermineAge->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(FacesSettingsPage::OnDetermineAgeChanged), NULL, this);
	m_chkDetectProperties->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(FacesSettingsPage::OnDetectPropertiesChanged), NULL, this);
	m_chkRecognizeExpression->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(FacesSettingsPage::OnRecognizeExpressionChanged), NULL, this);
	m_chkRecognizeEmotion->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(FacesSettingsPage::OnRecognizeEmotionChanged), NULL, this);
	m_chkCreateThumbnailImage->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(FacesSettingsPage::OnCreateThumbnailImageChanged), NULL, this);
	m_spinWidth->Disconnect(wxEVT_SPINCTRL, wxSpinEventHandler(FacesSettingsPage::OnWidthChanged), NULL, this);
	m_spinGenRecordCount->Disconnect(wxEVT_SPINCTRL, wxSpinEventHandler(FacesSettingsPage::OnGeneralizationRecordCountChnaged), NULL, this);
}

void FacesSettingsPage::CreateGUIControls()
{
	wxFlexGridSizer *sizer = new wxFlexGridSizer(21, 3, 5, 5);

	m_choiceCamera = new wxChoice(this, wxID_ANY, wxDefaultPosition, wxDefaultSize);
	m_choiceFormat = new wxChoice(this, wxID_ANY, wxDefaultPosition, wxDefaultSize);

	m_btnConnect = new wxButton(this, wxID_ANY, wxT("Connect"));
	m_btnDisconnect = new wxButton(this, wxID_ANY, wxT("Disconnect"));

	m_choiceTemplateSize = new wxChoice(this, wxID_ANY, wxDefaultPosition, wxDefaultSize);
	m_choiceTemplateSize->Append(NEnum::ToString(NBiometricEngineTypes::NTemplateSizeNativeTypeOf(), ntsCompact));
	m_choiceTemplateSize->Append(NEnum::ToString(NBiometricEngineTypes::NTemplateSizeNativeTypeOf(), ntsLarge));
	m_choiceTemplateSize->Append(NEnum::ToString(NBiometricEngineTypes::NTemplateSizeNativeTypeOf(), ntsMedium));
	m_choiceTemplateSize->Append(NEnum::ToString(NBiometricEngineTypes::NTemplateSizeNativeTypeOf(), ntsSmall));

	m_choiceMatchingSpeed = new wxChoice(this, wxID_ANY, wxDefaultPosition, wxDefaultSize);
	m_choiceMatchingSpeed->Append(NEnum::ToString(NBiometricEngineTypes::NMatchingSpeedNativeTypeOf(), nmsHigh));
	m_choiceMatchingSpeed->Append(NEnum::ToString(NBiometricEngineTypes::NMatchingSpeedNativeTypeOf(), nmsMedium));
	m_choiceMatchingSpeed->Append(NEnum::ToString(NBiometricEngineTypes::NMatchingSpeedNativeTypeOf(), nmsLow));

	m_spinMinimalInterOcularDistance = new wxSpinCtrl(this, wxID_ANY);
	m_spinMinimalInterOcularDistance->SetRange(8, 16384);

	m_spinConfidenceThreshold = new wxSpinCtrl(this, wxID_ANY);
	m_spinConfidenceThreshold->SetRange(0, 100);

	m_spinMaximalRoll = new wxSpinCtrl(this, wxID_ANY);
	m_spinMaximalRoll->SetRange(0, 180);

	m_spinMaximalYaw = new wxSpinCtrl(this, wxID_ANY);
	m_spinMaximalYaw->SetRange(0, 90);

	m_spinQualityThreshold = new wxSpinCtrl(this, wxID_ANY);
	m_spinQualityThreshold->SetRange(0, 100);

	m_spinGenRecordCount = new wxSpinCtrl(this, wxID_ANY);
	m_spinGenRecordCount->SetRange(3, 10);
	m_spinGenRecordCount->SetValue(3);

	m_choiceLivenessMode = new wxChoice(this, wxID_ANY, wxDefaultPosition, wxDefaultSize);
	NArrayWrapper<NInt> livenessValues = NEnum::GetValues(NBiometricTypes::NLivenessModeNativeTypeOf());
	for (NArrayWrapper<NInt>::iterator it = livenessValues.begin(); it != livenessValues.end(); it++)
	{
		m_choiceLivenessMode->Append(NEnum::ToString(NBiometricTypes::NLivenessModeNativeTypeOf(), *it));
	}

	m_spinLivenessThreshold = new wxSpinCtrl(this, wxID_ANY);
	m_spinLivenessThreshold->SetRange(0, 100);

	m_chkDetectAllFeaturePoints = new wxCheckBox(this, wxID_ANY, wxT("Detect all feature points"));
	m_chkDetectBaseFeaturePoints = new wxCheckBox(this, wxID_ANY, wxT("Detect base feature points"));
	m_chkDetermineGender = new wxCheckBox(this, wxID_ANY, wxT("Determine gender"));
	m_chkDetermineAge = new wxCheckBox(this, wxID_ANY, wxT("Determine age"));
	m_chkDetectProperties = new wxCheckBox(this, wxID_ANY, wxT("Detect properties"));
	m_chkRecognizeExpression = new wxCheckBox(this, wxID_ANY, wxT("Recognize expression"));
	m_chkRecognizeEmotion = new wxCheckBox(this, wxID_ANY, wxT("Recognize emotion"));
	m_chkCreateThumbnailImage = new wxCheckBox(this, wxID_ANY, wxT("Create thumbnail image"));

	m_spinWidth = new wxSpinCtrl(this, wxID_ANY);
	m_spinWidth->SetRange(30, N_INT32_MAX);

	wxFlexGridSizer * innerSizer = new wxFlexGridSizer(1,2,5,5);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Camera:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_choiceCamera, 0, wxALL | wxEXPAND, 0);
	innerSizer->Add(m_btnConnect, 0, wxALL | wxALIGN_LEFT, 0);
	innerSizer->Add(m_btnDisconnect, 0, wxALL | wxALIGN_LEFT, 0);
	sizer->Add(innerSizer, 0, wxALL | wxALIGN_LEFT, 0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Format:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_choiceFormat, 0, wxALL | wxEXPAND, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Template size:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_choiceTemplateSize, 0, wxALL | wxEXPAND, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Matching speed:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_choiceMatchingSpeed, 0, wxALL | wxEXPAND, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Minimal inter ocular distance:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_spinMinimalInterOcularDistance, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Confidence threshold:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_spinConfidenceThreshold, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Maximal roll:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_spinMaximalRoll, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Maximal yaw:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_spinMaximalYaw, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Quality threshold:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_spinQualityThreshold, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Gneralization record count:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_spinGenRecordCount, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Liveness Mode:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_choiceLivenessMode, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Liveness threshold:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_spinLivenessThreshold, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->AddSpacer(0);
	sizer->Add(m_chkDetectAllFeaturePoints, 0, wxALL | wxEXPAND, 0);
	sizer->AddSpacer(0);

	sizer->AddSpacer(0);
	sizer->Add(m_chkDetectBaseFeaturePoints, 0, wxALL | wxEXPAND, 0);
	sizer->AddSpacer(0);

	sizer->AddSpacer(0);
	sizer->Add(m_chkDetermineGender, 0, wxALL | wxEXPAND, 0);
	sizer->AddSpacer(0);

	sizer->AddSpacer(0);
	sizer->Add(m_chkDetermineAge, 0, wxALL | wxEXPAND, 0);
	sizer->AddSpacer(0);

	sizer->AddSpacer(0);
	sizer->Add(m_chkDetectProperties, 0, wxALL | wxEXPAND, 0);
	sizer->AddSpacer(0);

	sizer->AddSpacer(0);
	sizer->Add(m_chkRecognizeExpression, 0, wxALL | wxEXPAND, 0);
	sizer->AddSpacer(0);

	sizer->AddSpacer(0);
	sizer->Add(m_chkRecognizeEmotion, 0, wxALL | wxEXPAND, 0);
	sizer->AddSpacer(0);

	sizer->Add(m_chkCreateThumbnailImage, 0, wxALL | wxALIGN_RIGHT, 0);
	sizer->AddSpacer(0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Width:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_spinWidth, 0, wxALL, 0);
	sizer->AddSpacer(0);

	this->SetSizer(sizer, true);
	this->Layout();
}

}}


#include "Precompiled.h"

#include <Settings/FingersSettingsPage.h>
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

wxDECLARE_EVENT(wxEVT_FINGER_SETTINGS_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_FINGER_SETTINGS_THREAD, wxCommandEvent);

FingersSettingsPage::FingersSettingsPage(wxWindow *parent, wxWindowID winid) : BaseSettingsPage(parent, winid)
{
	CreateGUIControls();
	RegisterGuiEvents();
}

FingersSettingsPage::~FingersSettingsPage()
{
	m_biometricClient.GetDeviceManager().GetDevices().RemoveCollectionChangedCallback(&FingersSettingsPage::OnDevicesCollectionChanged, this);
	UnregisterGuiEvents();
}

int FingersSettingsPage::CheckIfSelectedDeviceIsDisconnectable()
{
	try
	{
		NDeviceManager deviceManager = m_biometricClient.GetDeviceManager();
		wxStringClientData * stringData = reinterpret_cast<wxStringClientData *>(m_choiceFingerScanner->GetClientObject(m_choiceFingerScanner->GetSelection()));
		if (stringData != NULL)
		{
			int i = 0;
			for (i = 0; i < deviceManager.GetDevices().GetCount(); i++)
			{
				NDevice device = deviceManager.GetDevices().Get(i);
				if ((wxString)device.GetId() == stringData->GetData())
				{
					if(device.IsDisconnectable())
						m_btnDisconnect->Enable();
					else
						m_btnDisconnect->Disable();
					m_biometricClient.SetFingerScanner(NObjectDynamicCast<NFScanner>(device));
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
	}
	return -1;
}

void FingersSettingsPage::Initialize(NBiometricClient biometricClient)
{
	BaseSettingsPage::Initialize(biometricClient);
	m_biometricClient.GetDeviceManager().GetDevices().AddCollectionChangedCallback(&FingersSettingsPage::OnDevicesCollectionChanged, this);

	m_chkCalculateNfiq->Enable(LicensingTools::CanAssessFingerQuality(biometricClient.GetLocalOperations()));
	if (!m_chkCalculateNfiq->IsEnabled())
		m_chkCalculateNfiq->SetLabel(m_chkCalculateNfiq->GetLabel() + wxT(" (Not activated)"));
	m_chkDeterminePatternClass->Enable(LicensingTools::CanDetectFingerSegments(biometricClient.GetLocalOperations()));
	if (!m_chkDeterminePatternClass->IsEnabled())
		m_chkDeterminePatternClass->SetLabel(m_chkDeterminePatternClass->GetLabel() + wxT(" (Not activated)"));
	NRemoteBiometricConnection remoteConnection = biometricClient.GetRemoteConnections().GetCount() > 0 ? biometricClient.GetRemoteConnections()[0] : NULL;
	NBiometricOperations operations = remoteConnection.IsNull() ? nboNone : remoteConnection.GetOperations();
	m_chkCheckForDuplicates->Enable(LicensingTools::CanFingerBeMatched(operations));
	if (!m_chkCheckForDuplicates->IsEnabled())
		m_chkCheckForDuplicates->SetLabel(m_chkCheckForDuplicates->GetLabel() + wxT(" (Not activated)"));
}

void FingersSettingsPage::OnThread(wxCommandEvent& event)
{
	int id = event.GetId();

	switch(id)
	{
	case ID_EVT_UPDATE_DEVICES:
		UpdateDevicesList();
		break;
	default:
		break;
	};
}

void FingersSettingsPage::UpdateDevicesList()
{
	m_choiceFingerScanner->Clear();

	NDeviceManager::NDeviceCollection devices = m_biometricClient.GetDeviceManager().GetDevices();
	for (int i = 0; i < devices.GetCount(); i++)
	{
		NDevice device = devices.Get(i);
		if (device.GetDeviceType() & ndtFingerScanner)
			m_choiceFingerScanner->Append(device.GetDisplayName(), new wxStringClientData(device.GetId()));
	}

	NFScanner scanner = m_biometricClient.GetFingerScanner();
	if (scanner.IsNull())
	{
		m_btnDisconnect->Disable();
		return;
	}

	for (unsigned int i = 0; i < m_choiceFingerScanner->GetCount(); i++)
	{
		wxStringClientData * clientData = reinterpret_cast<wxStringClientData *>(m_choiceFingerScanner->GetClientObject(i));
		if (clientData == NULL) continue;

		if ((wxString)scanner.GetId() == clientData->GetData())
		{
			m_choiceFingerScanner->SetSelection(i);
			break;
		}
	}
	CheckIfSelectedDeviceIsDisconnectable();
}

void FingersSettingsPage::Load()
{
	m_choiceTemplateSize->SetStringSelection(NEnum::ToString(NBiometricEngineTypes::NTemplateSizeNativeTypeOf(), m_biometricClient.GetFingersTemplateSize()));
	m_choiceMatchingSpeed->SetStringSelection(NEnum::ToString(NBiometricEngineTypes::NMatchingSpeedNativeTypeOf(), m_biometricClient.GetFingersMatchingSpeed()));
	m_spinMaximalRotation->SetValue(m_biometricClient.GetFingersMaximalRotation());
	m_spinQualityThreshold->SetValue(m_biometricClient.GetFingersQualityThreshold());
	m_chkFastExtraction->SetValue(m_biometricClient.GetFingersFastExtraction());
	m_chkReturnBinarizedImage->SetValue(m_biometricClient.GetFingersReturnBinarizedImage());
	m_chkDeterminePatternClass->SetValue(m_biometricClient.GetFingersDeterminePatternClass());
	m_chkCalculateNfiq->SetValue(m_biometricClient.GetFingersCalculateNfiq());
	m_chkCheckForDuplicates->SetValue(m_biometricClient.GetFingersCheckForDuplicatesWhenCapturing());

	m_spinGenRecordCount->SetValue(SettingsManager::GetFingersGeneralizationRecordCount());

	UpdateDevicesList();
}

void FingersSettingsPage::Reset()
{
	m_biometricClient.ResetProperty(wxT("Fingers.TemplateSize"));
	m_biometricClient.ResetProperty(wxT("Fingers.MatchingSpeed"));
	m_biometricClient.ResetProperty(wxT("Fingers.MaximalRotation"));
	m_biometricClient.ResetProperty(wxT("Fingers.QualityThreshold"));
	m_biometricClient.ResetProperty(wxT("Fingers.FastExtraction"));
	m_biometricClient.SetFingersReturnBinarizedImage(true);
	m_biometricClient.SetFingersDeterminePatternClass(m_chkDeterminePatternClass->IsEnabled());
	m_biometricClient.SetFingersCalculateNfiq(m_chkCalculateNfiq->IsEnabled());
	m_biometricClient.SetFingersCheckForDuplicatesWhenCapturing(m_chkCheckForDuplicates->IsEnabled());
	Load();

	m_spinGenRecordCount->SetValue(3);
	wxSpinEvent empty;
	OnGenRecordCountChanged(empty);
}

void FingersSettingsPage::OnFingerScannerChanged(wxCommandEvent&)
{
	unsigned int selection = m_choiceFingerScanner->GetSelection();

	wxStringClientData * clientData = reinterpret_cast<wxStringClientData *>(m_choiceFingerScanner->GetClientObject(selection));
	if (clientData == NULL) return;

	NDeviceManager deviceManager = m_biometricClient.GetDeviceManager();
	for (int i = 0; i < deviceManager.GetDevices().GetCount(); i++)
	{
		NDevice device = deviceManager.GetDevices().Get(i);
		if ((wxString)device.GetId() == clientData->GetData())
		{
			m_biometricClient.SetFingerScanner(NObjectDynamicCast<NFScanner>(device));
			break;
		}
	}

	CheckIfSelectedDeviceIsDisconnectable();
}

void FingersSettingsPage::OnConnectButtonClicked(wxCommandEvent& e)
{
	NDeviceManager deviceManager = m_biometricClient.GetDeviceManager();
	ConnectToDeviceForm * connectForm = new ConnectToDeviceForm(this);
	if(connectForm->ShowModal() == wxID_OK)
	{
		NDevice newDevice = NULL;
		try
		{
			newDevice = deviceManager.ConnectToDevice(connectForm->GetSelectedPlugin(), connectForm->GetProperties());
			UpdateDevicesList();
			m_choiceFingerScanner->SetSelection(m_choiceFingerScanner->GetCount() - 1);

			if(!m_choiceFingerScanner->HasClientUntypedData())
			{
				wxExceptionDlg::Show(wxT("Failed to create connection to device using specified connection details"));
				return;
			}

			OnFingerScannerChanged(e);

			int deviceId = CheckIfSelectedDeviceIsDisconnectable();
			if(newDevice.GetId() != deviceManager.GetDevices().Get(deviceId).GetId())
			{
				if(!newDevice.IsNull())
				{
					deviceManager.DisconnectFromDevice(newDevice);
					newDevice = NULL;
				}

				wxExceptionDlg::Show(wxT("Failed to create connection to device using specified connection details"));
			}
		}
		catch(NError& er)
		{
			if(!newDevice.IsNull())
			{
				deviceManager.DisconnectFromDevice(newDevice);
				newDevice = NULL;
			}

			wxExceptionDlg::Show(er);
		}
	}
}

void FingersSettingsPage::OnDisconnectButtonClicked(wxCommandEvent&)
{
	try
	{
		NDeviceManager deviceManager = m_biometricClient.GetDeviceManager();
		wxStringClientData * stringData = reinterpret_cast<wxStringClientData *>(m_choiceFingerScanner->GetClientObject(m_choiceFingerScanner->GetSelection()));

		if (stringData != NULL)
		{
			for (int i = 0; i < deviceManager.GetDevices().GetCount(); i++)
			{
				NDevice device = deviceManager.GetDevices().Get(i);
				if ((wxString)device.GetId() == stringData->GetData())
				{
					m_biometricClient.GetDeviceManager().DisconnectFromDevice(device);
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

void FingersSettingsPage::OnTemplateSizeChanged(wxCommandEvent&)
{
	m_biometricClient.SetFingersTemplateSize((NTemplateSize)NEnum::Parse(NBiometricEngineTypes::NTemplateSizeNativeTypeOf(), m_choiceTemplateSize->GetStringSelection()));
}

void FingersSettingsPage::OnMatchingSpeedChanged(wxCommandEvent&)
{
	m_biometricClient.SetFingersMatchingSpeed((NMatchingSpeed)NEnum::Parse(NBiometricEngineTypes::NMatchingSpeedNativeTypeOf(), m_choiceMatchingSpeed->GetStringSelection()));
}

void FingersSettingsPage::OnMaximalRotationChanged(wxSpinEvent&)
{
	m_biometricClient.SetFingersMaximalRotation(m_spinMaximalRotation->GetValue());
}

void FingersSettingsPage::OnQualityThresholdChanged(wxSpinEvent&)
{
	m_biometricClient.SetFingersQualityThreshold(m_spinQualityThreshold->GetValue());
}

void FingersSettingsPage::OnFastExtractionChanged(wxCommandEvent&)
{
	m_biometricClient.SetFingersFastExtraction(m_chkFastExtraction->GetValue());
}

void FingersSettingsPage::OnReturnProcessedImageChanged(wxCommandEvent&)
{
	m_biometricClient.SetFingersReturnBinarizedImage(m_chkReturnBinarizedImage->GetValue());
}

void FingersSettingsPage::OnCheckForDuplicatesChanged(wxCommandEvent&)
{
	m_biometricClient.SetFingersCheckForDuplicatesWhenCapturing(m_chkCheckForDuplicates->GetValue());
}

void FingersSettingsPage::OnGenRecordCountChanged(wxSpinEvent&)
{
	SettingsManager::SetFingersGeneralizationRecordCount(m_spinGenRecordCount->GetValue());
}

void FingersSettingsPage::OnDevicesCollectionChanged(Collections::CollectionChangedEventArgs<NDevice> args)
{
	FingersSettingsPage *fingersSettingsPage = reinterpret_cast<FingersSettingsPage *>(args.GetParam());
	wxCommandEvent event(wxEVT_FINGER_SETTINGS_THREAD, ID_EVT_UPDATE_DEVICES);
	wxPostEvent(fingersSettingsPage, event);
}

void FingersSettingsPage::OnDeterminPatternClassChanged(wxCommandEvent&)
{
	m_biometricClient.SetFingersDeterminePatternClass(m_chkDeterminePatternClass->GetValue());
}

void FingersSettingsPage::OnCalculateNfiqChanged(wxCommandEvent &)
{
	m_biometricClient.SetFingersCalculateNfiq(m_chkCalculateNfiq->GetValue());
}

void FingersSettingsPage::RegisterGuiEvents()
{
	this->Bind(wxEVT_FINGER_SETTINGS_THREAD, &FingersSettingsPage::OnThread, this);

	m_choiceFingerScanner->Connect(wxEVT_CHOICE, wxCommandEventHandler(FingersSettingsPage::OnFingerScannerChanged), NULL, this);
	m_btnConnect->Connect(wxEVT_BUTTON, wxCommandEventHandler(FingersSettingsPage::OnConnectButtonClicked), NULL, this);
	m_btnDisconnect->Connect(wxEVT_BUTTON, wxCommandEventHandler(FingersSettingsPage::OnDisconnectButtonClicked), NULL, this);
	m_choiceTemplateSize->Connect(wxEVT_CHOICE, wxCommandEventHandler(FingersSettingsPage::OnTemplateSizeChanged), NULL, this);
	m_choiceMatchingSpeed->Connect(wxEVT_CHOICE, wxCommandEventHandler(FingersSettingsPage::OnMatchingSpeedChanged), NULL, this);
	m_spinMaximalRotation->Connect(wxEVT_SPINCTRL, wxSpinEventHandler(FingersSettingsPage::OnMaximalRotationChanged), NULL, this);
	m_spinQualityThreshold->Connect(wxEVT_SPINCTRL, wxSpinEventHandler(FingersSettingsPage::OnQualityThresholdChanged), NULL, this);
	m_chkFastExtraction->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(FingersSettingsPage::OnFastExtractionChanged), NULL, this);
	m_chkReturnBinarizedImage->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(FingersSettingsPage::OnReturnProcessedImageChanged), NULL, this);
	m_spinGenRecordCount->Connect(wxEVT_SPINCTRL, wxSpinEventHandler(FingersSettingsPage::OnGenRecordCountChanged), NULL, this);
	m_chkCheckForDuplicates->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(FingersSettingsPage::OnCheckForDuplicatesChanged), NULL, this);
	m_chkCalculateNfiq->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(FingersSettingsPage::OnCalculateNfiqChanged), NULL, this);
	m_chkDeterminePatternClass->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(FingersSettingsPage::OnDeterminPatternClassChanged), NULL, this);
}

void FingersSettingsPage::UnregisterGuiEvents()
{
	this->Unbind(wxEVT_FINGER_SETTINGS_THREAD, &FingersSettingsPage::OnThread, this);

	m_choiceFingerScanner->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(FingersSettingsPage::OnFingerScannerChanged), NULL, this);
	m_btnConnect->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(FingersSettingsPage::OnConnectButtonClicked), NULL, this);
	m_btnDisconnect->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(FingersSettingsPage::OnDisconnectButtonClicked), NULL, this);
	m_choiceTemplateSize->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(FingersSettingsPage::OnTemplateSizeChanged), NULL, this);
	m_choiceMatchingSpeed->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(FingersSettingsPage::OnMatchingSpeedChanged), NULL, this);
	m_spinMaximalRotation->Disconnect(wxEVT_SPINCTRL, wxSpinEventHandler(FingersSettingsPage::OnMaximalRotationChanged), NULL, this);
	m_spinQualityThreshold->Disconnect(wxEVT_SPINCTRL, wxSpinEventHandler(FingersSettingsPage::OnQualityThresholdChanged), NULL, this);
	m_chkFastExtraction->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(FingersSettingsPage::OnFastExtractionChanged), NULL, this);
	m_chkReturnBinarizedImage->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(FingersSettingsPage::OnReturnProcessedImageChanged), NULL, this);
	m_spinGenRecordCount->Disconnect(wxEVT_SPINCTRL, wxSpinEventHandler(FingersSettingsPage::OnGenRecordCountChanged), NULL, this);
	m_chkCheckForDuplicates->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(FingersSettingsPage::OnCheckForDuplicatesChanged), NULL, this);
	m_chkCalculateNfiq->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(FingersSettingsPage::OnCalculateNfiqChanged), NULL, this);
	m_chkDeterminePatternClass->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(FingersSettingsPage::OnDeterminPatternClassChanged), NULL, this);
}

void FingersSettingsPage::CreateGUIControls()
{
	wxFlexGridSizer *sizer = new wxFlexGridSizer(11, 3, 5, 5);

	m_choiceFingerScanner = new wxChoice(this, wxID_ANY, wxDefaultPosition, wxDefaultSize);

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

	m_spinMaximalRotation = new wxSpinCtrl(this, wxID_ANY);
	m_spinMaximalRotation->SetRange(0, 180);

	m_spinQualityThreshold = new wxSpinCtrl(this, wxID_ANY);
	m_spinQualityThreshold->SetRange(0, 100);

	m_spinGenRecordCount = new wxSpinCtrl(this, wxID_ANY);
	m_spinGenRecordCount->SetRange(3, 10);
	m_spinGenRecordCount->SetValue(3);

	m_chkFastExtraction = new wxCheckBox(this, wxID_ANY, wxT("Fast extraction"));
	m_chkReturnBinarizedImage = new wxCheckBox(this, wxID_ANY, wxT("Return binarized image"));
	m_chkDeterminePatternClass = new wxCheckBox(this, wxID_ANY, wxT("Determine pattern class"));
	m_chkCalculateNfiq = new wxCheckBox(this, wxID_ANY, wxT("Calculate Nfiq"));

	m_chkCheckForDuplicates = new wxCheckBox(this, wxID_ANY, wxT("Check for duplicates when capturing"));

	wxFlexGridSizer * innerSizer = new wxFlexGridSizer(1,2,5,5);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Finger scanner:")), 0, wxALL | wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL, 0);
	sizer->Add(m_choiceFingerScanner, 0, wxALL | wxEXPAND, 0);
	innerSizer->Add(m_btnConnect, 0, wxALL | wxALIGN_LEFT, 0);
	innerSizer->Add(m_btnDisconnect, 0, wxALL | wxALIGN_LEFT, 0);
	sizer->Add(innerSizer, 0, wxALL | wxALIGN_LEFT, 0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Template size:")), 0, wxALL | wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL, 0);
	sizer->Add(m_choiceTemplateSize, 0, wxALL | wxEXPAND, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Matching speed:")), 0, wxALL | wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL, 0);
	sizer->Add(m_choiceMatchingSpeed, 0, wxALL | wxEXPAND, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Maximal rotation:")), 0, wxALL | wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL, 0);
	sizer->Add(m_spinMaximalRotation, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Quality threshold:")), 0, wxALL | wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL, 0);
	sizer->Add(m_spinQualityThreshold, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Generalization record count:")), 0, wxALL | wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL, 0);
	sizer->Add(m_spinGenRecordCount, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->AddSpacer(0);
	sizer->Add(m_chkFastExtraction, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->AddSpacer(0);
	sizer->Add(m_chkReturnBinarizedImage, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->AddSpacer(0);
	sizer->Add(m_chkDeterminePatternClass, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->AddSpacer(0);
	sizer->Add(m_chkCalculateNfiq, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->AddSpacer(0);
	sizer->Add(m_chkCheckForDuplicates, 0, wxALL, 0);
	sizer->AddSpacer(0);

	this->SetSizer(sizer, true);
	this->Layout();
}

}}


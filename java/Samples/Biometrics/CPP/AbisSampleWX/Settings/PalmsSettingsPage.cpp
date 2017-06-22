#include "Precompiled.h"

#include <Settings/PalmsSettingsPage.h>
#include <Settings/ConnectToDeviceForm.h>
#include <Settings/SettingsManager.h>

using namespace ::Neurotec::Biometrics;
using namespace ::Neurotec::Biometrics::Client;
using namespace ::Neurotec::Devices;
using namespace ::Neurotec::Gui;

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_PALM_SETTINS_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_PALM_SETTINS_THREAD, wxCommandEvent);

PalmsSettingsPage::PalmsSettingsPage(wxWindow *parent, wxWindowID winid) : BaseSettingsPage(parent, winid)
{
	CreateGUIControls();
	RegisterGuiEvents();
}

PalmsSettingsPage::~PalmsSettingsPage()
{
	m_biometricClient.GetDeviceManager().GetDevices().RemoveCollectionChangedCallback(&PalmsSettingsPage::OnDevicesCollectionChanged, this);
	UnregisterGuiEvents();
}

int PalmsSettingsPage::CheckIfSelectedDeviceIsDisconnectable()
{
	try
	{
		NDeviceManager deviceManager = m_biometricClient.GetDeviceManager();
		wxStringClientData * stringData = reinterpret_cast<wxStringClientData *>(m_choicePalmScanner->GetClientObject(m_choicePalmScanner->GetSelection()));

		if (stringData != NULL)
		{
			int i = 0;
			for (i = 0; i < deviceManager.GetDevices().GetCount(); i++)
			{
				NDevice device = deviceManager.GetDevices().Get(i);
				if ((wxString)device.GetId() == stringData->GetData())
				{
					m_btnDisconnect->Enable(device.IsDisconnectable());
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
		return -1;
	}
	return -1;
}

void PalmsSettingsPage::Initialize(NBiometricClient biometricClient)
{
	BaseSettingsPage::Initialize(biometricClient);
	m_biometricClient.GetDeviceManager().GetDevices().AddCollectionChangedCallback(&PalmsSettingsPage::OnDevicesCollectionChanged, (void *)this);
}

void PalmsSettingsPage::OnThread(wxCommandEvent& event)
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

void PalmsSettingsPage::UpdateDevicesList()
{
	m_choicePalmScanner->Clear();

	NDeviceManager::NDeviceCollection devices = m_biometricClient.GetDeviceManager().GetDevices();
	for (int i = 0; i < devices.GetCount(); i++)
	{
		NDevice device = devices.Get(i);
		if (device.GetDeviceType() & ndtPalmScanner)
			m_choicePalmScanner->Append(device.GetDisplayName(), new wxStringClientData(device.GetId()));
	}

	NFScanner scanner = m_biometricClient.GetPalmScanner();
	if (scanner.IsNull())
	{
		m_btnDisconnect->Disable();
		return;
	}

	for (unsigned int i = 0; i < m_choicePalmScanner->GetCount(); i++)
	{
		wxStringClientData * stringData = reinterpret_cast<wxStringClientData *>(m_choicePalmScanner->GetClientObject(i));
		if (stringData == NULL) continue;

		if ((wxString)scanner.GetId() == stringData->GetData())
		{
			m_choicePalmScanner->SetSelection(i);
			break;
		}
	}
	CheckIfSelectedDeviceIsDisconnectable();
}

void PalmsSettingsPage::Load()
{
	m_choiceTemplateSize->SetStringSelection(NEnum::ToString(NBiometricEngineTypes::NTemplateSizeNativeTypeOf(), m_biometricClient.GetPalmsTemplateSize()));
	m_choiceMatchingSpeed->SetStringSelection(NEnum::ToString(NBiometricEngineTypes::NMatchingSpeedNativeTypeOf(), m_biometricClient.GetPalmsMatchingSpeed()));
	m_spinMaximalRotation->SetValue(m_biometricClient.GetPalmsMaximalRotation());
	m_spinQualityThreshold->SetValue(m_biometricClient.GetPalmsQualityThreshold());
	m_chkReturnBinarizedImage->SetValue(m_biometricClient.GetPalmsReturnBinarizedImage());
	m_spinGenRecordCount->SetValue(SettingsManager::GetPalmsGeneralizationRecordCount());

	UpdateDevicesList();
}

void PalmsSettingsPage::Reset()
{
	m_biometricClient.ResetProperty(wxT("Palms.TemplateSize"));
	m_biometricClient.ResetProperty(wxT("Palms.MatchingSpeed"));
	m_biometricClient.ResetProperty(wxT("Palms.MaximalRotation"));
	m_biometricClient.ResetProperty(wxT("Palms.QualityThreshold"));
	m_biometricClient.ResetProperty(wxT("Palms.ReturnBinarizedImage"));
	Load();

	m_spinGenRecordCount->SetValue(3);
	wxSpinEvent empty;
	OnGenRecordCountChanged(empty);
}

void PalmsSettingsPage::OnPalmScannerChanged(wxCommandEvent&)
{
	unsigned int selection = m_choicePalmScanner->GetSelection();

	wxStringClientData * stringData = reinterpret_cast<wxStringClientData *>(m_choicePalmScanner->GetClientObject(selection));
	if (stringData == NULL) return;

	NDeviceManager::NDeviceCollection devices = m_biometricClient.GetDeviceManager().GetDevices();
	for (int i = 0; i < devices.GetCount(); i++)
	{
		NDevice device = devices.Get(i);
		if ((wxString)device.GetId() == stringData->GetData())
		{
			m_biometricClient.SetPalmScanner(NObjectDynamicCast<NFScanner>(device));
			break;
		}
	}
	CheckIfSelectedDeviceIsDisconnectable();
}

void PalmsSettingsPage::OnGenRecordCountChanged(wxSpinEvent&)
{
	SettingsManager::SetPalmsGeneralizationRecordCount(m_spinGenRecordCount->GetValue());
}

void PalmsSettingsPage::OnConnectButtonClicked(wxCommandEvent& e)
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
			m_choicePalmScanner->SetSelection(m_choicePalmScanner->GetCount() - 1);

			if(!m_choicePalmScanner->HasClientUntypedData())
			{
				wxExceptionDlg::Show(wxT("Failed to create connection to device using specified connection details"));
				return;
			}

			OnPalmScannerChanged(e);

			int deviceId = CheckIfSelectedDeviceIsDisconnectable();
			if(newDevice->GetId() != deviceManager.GetDevices().Get(deviceId).GetId())
			{
				if(newDevice != NULL)
				{
					deviceManager.DisconnectFromDevice(*newDevice);
					delete newDevice;
				}

				wxExceptionDlg::Show(wxT("Failed to create connection to device using specified connection details"));
			}
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

void PalmsSettingsPage::OnDisconnectButtonClicked(wxCommandEvent&)
{
	try
	{
		NDeviceManager deviceManager = m_biometricClient.GetDeviceManager();
		wxStringClientData * stringData = reinterpret_cast<wxStringClientData *>(m_choicePalmScanner->GetClientObject(m_choicePalmScanner->GetSelection()));

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

void PalmsSettingsPage::OnTemplateSizeChanged(wxCommandEvent&)
{
	m_biometricClient.SetPalmsTemplateSize((NTemplateSize)NEnum::Parse(NBiometricEngineTypes::NTemplateSizeNativeTypeOf(), m_choiceTemplateSize->GetStringSelection()));
}

void PalmsSettingsPage::OnMatchingSpeedChanged(wxCommandEvent&)
{
	m_biometricClient.SetPalmsMatchingSpeed((NMatchingSpeed)NEnum::Parse(NBiometricEngineTypes::NMatchingSpeedNativeTypeOf(), m_choiceMatchingSpeed->GetStringSelection()));
}

void PalmsSettingsPage::OnMaximalRotationChanged(wxSpinEvent&)
{
	m_biometricClient.SetPalmsMaximalRotation(m_spinMaximalRotation->GetValue());
}

void PalmsSettingsPage::OnQualityThresholdChanged(wxSpinEvent&)
{
	m_biometricClient.SetPalmsQualityThreshold(m_spinQualityThreshold->GetValue());
}

void PalmsSettingsPage::OnReturnProcessedImageChanged(wxCommandEvent&)
{
	m_biometricClient.SetPalmsReturnBinarizedImage(m_chkReturnBinarizedImage->GetValue());
}

void PalmsSettingsPage::OnDevicesCollectionChanged(Collections::CollectionChangedEventArgs<NDevice> args)
{
	PalmsSettingsPage *palmsSettingsPage = reinterpret_cast<PalmsSettingsPage *>(args.GetParam());
	wxCommandEvent event(wxEVT_PALM_SETTINS_THREAD, ID_EVT_UPDATE_DEVICES);
	wxPostEvent(palmsSettingsPage, event);
}

void PalmsSettingsPage::RegisterGuiEvents()
{
	this->Bind(wxEVT_PALM_SETTINS_THREAD, &PalmsSettingsPage::OnThread, this);

	m_choicePalmScanner->Connect(wxEVT_CHOICE, wxCommandEventHandler(PalmsSettingsPage::OnPalmScannerChanged), NULL, this);
	m_btnConnect->Connect(wxEVT_BUTTON, wxCommandEventHandler(PalmsSettingsPage::OnConnectButtonClicked), NULL, this);
	m_btnDisconnect->Connect(wxEVT_BUTTON, wxCommandEventHandler(PalmsSettingsPage::OnDisconnectButtonClicked), NULL, this);
	m_choiceTemplateSize->Connect(wxEVT_CHOICE, wxCommandEventHandler(PalmsSettingsPage::OnTemplateSizeChanged), NULL, this);
	m_choiceMatchingSpeed->Connect(wxEVT_CHOICE, wxCommandEventHandler(PalmsSettingsPage::OnMatchingSpeedChanged), NULL, this);
	m_spinMaximalRotation->Connect(wxEVT_SPINCTRL, wxSpinEventHandler(PalmsSettingsPage::OnMaximalRotationChanged), NULL, this);
	m_spinQualityThreshold->Connect(wxEVT_SPINCTRL, wxSpinEventHandler(PalmsSettingsPage::OnQualityThresholdChanged), NULL, this);
	m_chkReturnBinarizedImage->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(PalmsSettingsPage::OnReturnProcessedImageChanged), NULL, this);
	m_spinGenRecordCount->Connect(wxEVT_SPINCTRL, wxSpinEventHandler(PalmsSettingsPage::OnGenRecordCountChanged), NULL, this);
}

void PalmsSettingsPage::UnregisterGuiEvents()
{
	this->Unbind(wxEVT_PALM_SETTINS_THREAD, &PalmsSettingsPage::OnThread, this);

	m_choicePalmScanner->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(PalmsSettingsPage::OnPalmScannerChanged), NULL, this);
	m_btnConnect->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(PalmsSettingsPage::OnConnectButtonClicked), NULL, this);
	m_btnDisconnect->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(PalmsSettingsPage::OnDisconnectButtonClicked), NULL, this);
	m_choiceTemplateSize->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(PalmsSettingsPage::OnTemplateSizeChanged), NULL, this);
	m_choiceMatchingSpeed->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(PalmsSettingsPage::OnMatchingSpeedChanged), NULL, this);
	m_spinMaximalRotation->Disconnect(wxEVT_SPINCTRL, wxSpinEventHandler(PalmsSettingsPage::OnMaximalRotationChanged), NULL, this);
	m_spinQualityThreshold->Disconnect(wxEVT_SPINCTRL, wxSpinEventHandler(PalmsSettingsPage::OnQualityThresholdChanged), NULL, this);
	m_chkReturnBinarizedImage->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(PalmsSettingsPage::OnReturnProcessedImageChanged), NULL, this);
	m_spinGenRecordCount->Disconnect(wxEVT_SPINCTRL, wxSpinEventHandler(PalmsSettingsPage::OnGenRecordCountChanged), NULL, this);
}

void PalmsSettingsPage::CreateGUIControls()
{
	wxFlexGridSizer *sizer = new wxFlexGridSizer(7, 3, 5, 5);

	m_choicePalmScanner = new wxChoice(this, wxID_ANY, wxDefaultPosition, wxDefaultSize);

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

	m_chkReturnBinarizedImage = new wxCheckBox(this, wxID_ANY, wxT("Return binarized image"));

	wxFlexGridSizer * innerSizer = new wxFlexGridSizer(1,2,5,5);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Palm scanner:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_choicePalmScanner, 0, wxALL | wxEXPAND, 0);
	innerSizer->Add(m_btnConnect, 0, wxALL | wxALIGN_LEFT, 0);
	innerSizer->Add(m_btnDisconnect, 0, wxALL | wxALIGN_LEFT, 0);
	sizer->Add(innerSizer, 0, wxALL | wxALIGN_LEFT, 0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Template size:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_choiceTemplateSize, 0, wxALL | wxEXPAND, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Matching speed:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_choiceMatchingSpeed, 0, wxALL | wxEXPAND, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Maximal rotation:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_spinMaximalRotation, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Quality threshold:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_spinQualityThreshold, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Generalization record count:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_spinGenRecordCount, 0, wxALL, 0);
	sizer->AddSpacer(0);

	sizer->AddSpacer(0);
	sizer->Add(m_chkReturnBinarizedImage, 0, wxALL, 0);
	sizer->AddSpacer(0);

	this->SetSizer(sizer, true);
	this->Layout();
}

}}


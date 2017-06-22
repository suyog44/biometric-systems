#include "Precompiled.h"

#include <Settings/IrisesSettingsPage.h>

using namespace ::Neurotec::Biometrics;
using namespace ::Neurotec::Biometrics::Client;
using namespace ::Neurotec::Devices;

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_IRISES_SETTINGS_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_IRISES_SETTINGS_THREAD, wxCommandEvent);

IrisesSettingsPage::IrisesSettingsPage(wxWindow *parent, wxWindowID winid) : BaseSettingsPage(parent, winid)
{
	CreateGUIControls();
	RegisterGuiEvents();
}

IrisesSettingsPage::~IrisesSettingsPage()
{
	m_biometricClient.GetDeviceManager().GetDevices().RemoveCollectionChangedCallback(&IrisesSettingsPage::OnDevicesCollectionChanged, this);
	UnregisterGuiEvents();
}

void IrisesSettingsPage::Initialize(NBiometricClient biometricClient)
{
	BaseSettingsPage::Initialize(biometricClient);
	m_biometricClient.GetDeviceManager().GetDevices().AddCollectionChangedCallback(&IrisesSettingsPage::OnDevicesCollectionChanged, this);
}

void IrisesSettingsPage::OnThread(wxCommandEvent& event)
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

void IrisesSettingsPage::UpdateDevicesList()
{
	m_choiceIrisScanner->Clear();

	NDeviceManager::NDeviceCollection devices = m_biometricClient.GetDeviceManager().GetDevices();
	for (int i = 0; i < devices.GetCount(); i++)
	{
		NDevice device = devices.Get(i);
		if (device.GetDeviceType() & ndtIrisScanner)
		{
			m_choiceIrisScanner->Append(device.GetDisplayName(), new wxStringClientData(device.GetId()));
		}
	}

	NIrisScanner scanner = m_biometricClient.GetIrisScanner();
	if (scanner.IsNull()) return;

	for (unsigned int i = 0; i < m_choiceIrisScanner->GetCount(); i++)
	{
		wxStringClientData * stringData = reinterpret_cast<wxStringClientData *>(m_choiceIrisScanner->GetClientObject(i));
		if (stringData == NULL) continue;

		if ((wxString)scanner.GetId() == stringData->GetData())
		{
			m_choiceIrisScanner->SetSelection(i);
			break;
		}
	}
}

void IrisesSettingsPage::Load()
{
	m_choiceTemplateSize->SetStringSelection(NEnum::ToString(NBiometricEngineTypes::NTemplateSizeNativeTypeOf(), m_biometricClient.GetIrisesTemplateSize()));
	m_choiceMatchingSpeed->SetStringSelection(NEnum::ToString(NBiometricEngineTypes::NMatchingSpeedNativeTypeOf(), m_biometricClient.GetIrisesMatchingSpeed()));
	m_spinMaximalRotation->SetValue(m_biometricClient.GetIrisesMaximalRotation());
	m_spinQualityThreshold->SetValue(m_biometricClient.GetIrisesQualityThreshold());
	m_chkFastExtraction->SetValue(m_biometricClient.GetIrisesFastExtraction());

	UpdateDevicesList();
}

void IrisesSettingsPage::Reset()
{
	m_biometricClient.ResetProperty(wxT("Irises.TemplateSize"));
	m_biometricClient.ResetProperty(wxT("Irises.MatchingSpeed"));
	m_biometricClient.ResetProperty(wxT("Irises.MaximalRotation"));
	m_biometricClient.ResetProperty(wxT("Irises.QualityThreshold"));
	m_biometricClient.ResetProperty(wxT("Irises.FastExtraction"));
	Load();
}

void IrisesSettingsPage::OnIrisScannerChanged(wxCommandEvent&)
{
	unsigned int selection = m_choiceIrisScanner->GetSelection();

	wxStringClientData * stringData = reinterpret_cast<wxStringClientData *>(m_choiceIrisScanner->GetClientObject(selection));
	if (stringData == NULL) return;

	NDeviceManager::NDeviceCollection devices = m_biometricClient.GetDeviceManager().GetDevices();
	for (int i = 0; i < devices.GetCount(); i++)
	{
		NDevice device = devices.Get(i);
		if ((wxString)device.GetId() == stringData->GetData())
		{
			m_biometricClient.SetIrisScanner(NObjectDynamicCast<NIrisScanner>(device));
			break;
		}
	}
}

void IrisesSettingsPage::OnTemplateSizeChanged(wxCommandEvent&)
{
	m_biometricClient.SetIrisesTemplateSize((NTemplateSize)NEnum::Parse(NBiometricEngineTypes::NTemplateSizeNativeTypeOf(), m_choiceTemplateSize->GetStringSelection()));
}

void IrisesSettingsPage::OnMatchingSpeedChanged(wxCommandEvent&)
{
	m_biometricClient.SetIrisesMatchingSpeed((NMatchingSpeed)NEnum::Parse(NBiometricEngineTypes::NMatchingSpeedNativeTypeOf(), m_choiceMatchingSpeed->GetStringSelection()));
}

void IrisesSettingsPage::OnMaximalRotationChanged(wxSpinEvent&)
{
	m_biometricClient.SetIrisesMaximalRotation(m_spinMaximalRotation->GetValue());
}

void IrisesSettingsPage::OnQualityThresholdChanged(wxSpinEvent&)
{
	m_biometricClient.SetIrisesQualityThreshold(m_spinQualityThreshold->GetValue());
}

void IrisesSettingsPage::OnFastExtractionChanged(wxCommandEvent&)
{
	m_biometricClient.SetIrisesFastExtraction(m_chkFastExtraction->GetValue());
}

void IrisesSettingsPage::OnDevicesCollectionChanged(Collections::CollectionChangedEventArgs<NDevice> args)
{
	IrisesSettingsPage *irisesSettingsPage = reinterpret_cast<IrisesSettingsPage *>(args.GetParam());
	wxCommandEvent event(wxEVT_IRISES_SETTINGS_THREAD, ID_EVT_UPDATE_DEVICES);
	wxPostEvent(irisesSettingsPage, event);
}

void IrisesSettingsPage::RegisterGuiEvents()
{
	this->Bind(wxEVT_IRISES_SETTINGS_THREAD, &IrisesSettingsPage::OnThread, this);

	m_choiceIrisScanner->Connect(wxEVT_CHOICE, wxCommandEventHandler(IrisesSettingsPage::OnIrisScannerChanged), NULL, this);
	m_choiceTemplateSize->Connect(wxEVT_CHOICE, wxCommandEventHandler(IrisesSettingsPage::OnTemplateSizeChanged), NULL, this);
	m_choiceMatchingSpeed->Connect(wxEVT_CHOICE, wxCommandEventHandler(IrisesSettingsPage::OnMatchingSpeedChanged), NULL, this);
	m_spinMaximalRotation->Connect(wxEVT_SPINCTRL, wxSpinEventHandler(IrisesSettingsPage::OnMaximalRotationChanged), NULL, this);
	m_spinQualityThreshold->Connect(wxEVT_SPINCTRL, wxSpinEventHandler(IrisesSettingsPage::OnQualityThresholdChanged), NULL, this);
	m_chkFastExtraction->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(IrisesSettingsPage::OnFastExtractionChanged), NULL, this);
}

void IrisesSettingsPage::UnregisterGuiEvents()
{
	this->Unbind(wxEVT_IRISES_SETTINGS_THREAD, &IrisesSettingsPage::OnThread, this);

	m_choiceIrisScanner->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(IrisesSettingsPage::OnIrisScannerChanged), NULL, this);
	m_choiceTemplateSize->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(IrisesSettingsPage::OnTemplateSizeChanged), NULL, this);
	m_choiceMatchingSpeed->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(IrisesSettingsPage::OnMatchingSpeedChanged), NULL, this);
	m_spinMaximalRotation->Disconnect(wxEVT_SPINCTRL, wxSpinEventHandler(IrisesSettingsPage::OnMaximalRotationChanged), NULL, this);
	m_spinQualityThreshold->Disconnect(wxEVT_SPINCTRL, wxSpinEventHandler(IrisesSettingsPage::OnQualityThresholdChanged), NULL, this);
	m_chkFastExtraction->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(IrisesSettingsPage::OnFastExtractionChanged), NULL, this);
}

void IrisesSettingsPage::CreateGUIControls()
{
	wxFlexGridSizer *sizer = new wxFlexGridSizer(6, 2, 5, 5);

	m_choiceIrisScanner = new wxChoice(this, wxID_ANY, wxDefaultPosition, wxDefaultSize);

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

	m_chkFastExtraction = new wxCheckBox(this, wxID_ANY, wxT("Fast extraction"));

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Iris scanner:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_choiceIrisScanner, 0, wxALL | wxEXPAND, 0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Template size:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_choiceTemplateSize, 0, wxALL | wxEXPAND, 0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Matching speed:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_choiceMatchingSpeed, 0, wxALL | wxEXPAND, 0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Maximal rotation:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_spinMaximalRotation, 0, wxALL, 0);

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Quality threshold:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	sizer->Add(m_spinQualityThreshold, 0, wxALL, 0);

	sizer->AddSpacer(0);
	sizer->Add(m_chkFastExtraction, 0, wxALL, 0);

	this->SetSizer(sizer, true);
	this->Layout();
}

}}


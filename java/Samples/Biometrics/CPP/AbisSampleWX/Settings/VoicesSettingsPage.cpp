#include "Precompiled.h"

#include <Settings/VoicesSettingsPage.h>

using namespace ::Neurotec::Biometrics;
using namespace ::Neurotec::Biometrics::Client;
using namespace ::Neurotec::Devices;
using namespace ::Neurotec::Media;
using namespace ::Neurotec::Gui;

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_VOICE_SETTINGS_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_VOICE_SETTINGS_THREAD, wxCommandEvent);

VoicesSettingsPage::VoicesSettingsPage(wxWindow *parent, wxWindowID winid) : BaseSettingsPage(parent, winid)
{
	CreateGUIControls();
	RegisterGuiEvents();
}

VoicesSettingsPage::~VoicesSettingsPage()
{
	m_biometricClient.GetDeviceManager().GetDevices().RemoveCollectionChangedCallback(&VoicesSettingsPage::OnDevicesCollectionChanged, this);
	UnregisterGuiEvents();
}

void VoicesSettingsPage::Initialize(NBiometricClient biometricClient)
{
	BaseSettingsPage::Initialize(biometricClient);
	m_biometricClient.GetDeviceManager().GetDevices().AddCollectionChangedCallback(&VoicesSettingsPage::OnDevicesCollectionChanged, this);
}

void VoicesSettingsPage::OnThread(wxCommandEvent& event)
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

void VoicesSettingsPage::UpdateDevicesList()
{
	m_choiceMicrophone->Clear();

	wxString currentScannerId = wxEmptyString;

	NDeviceManager::NDeviceCollection devices = m_biometricClient.GetDeviceManager().GetDevices();

	for (int i = 0; i < devices.GetCount(); i++)
	{
		NDevice device = devices.Get(i);
		if (device.GetDeviceType() & ndtMicrophone)
			m_choiceMicrophone->Append(device.GetDisplayName(), new wxStringClientData(device.GetId()));
	}

	try
	{
		NMicrophone microphone = m_biometricClient.GetVoiceCaptureDevice();
		if (!microphone.IsNull())
		{
			currentScannerId = microphone.GetId();
		}
	}
	catch(NError& ex)
	{
		wxExceptionDlg::Show(ex);
	}

	for (unsigned int i = 0; i < m_choiceMicrophone->GetCount(); i++)
	{
		wxStringClientData * stringData = reinterpret_cast<wxStringClientData *>(m_choiceMicrophone->GetClientObject(i));
		if (stringData == NULL) continue;

		if (currentScannerId == stringData->GetData())
		{
			m_choiceMicrophone->SetSelection(i);
			break;
		}
	}
}

void VoicesSettingsPage::UpdateCaptureFormats()
{
	m_choiceFormat->Clear();

	NMicrophone microphone = m_biometricClient.GetVoiceCaptureDevice();
	if (microphone.IsNull()) return;

	try
	{
		NArrayWrapper<NMediaFormat> formats = microphone.GetFormats();
		for (int i = 0; i < formats.GetCount(); ++i)
		{
			m_choiceFormat->Append(formats[i].ToString());
		}

		NMediaFormat format = microphone.GetCurrentFormat();
		if (!format.IsNull())
		{
			m_choiceFormat->SetStringSelection(format.ToString());
		}
	}
	catch(NError & err)
	{
		wxExceptionDlg::Show(err);
	}
}

void VoicesSettingsPage::Load()
{
	m_chkUniquePhrasesOnly->SetValue(m_biometricClient.GetVoicesUniquePhrasesOnly());
	m_chkExtractTextDependentFeatures->SetValue(m_biometricClient.GetVoicesExtractTextDependentFeatures());
	m_chkExtractTextIndependentFeatures->SetValue(m_biometricClient.GetVoicesExtractTextIndependentFeatures());
	m_scdMaxLoadedFileSize->SetValue((double)m_biometricClient.GetVoicesMaximalLoadedFileSize() / (double)1048576);

	UpdateDevicesList();
	UpdateCaptureFormats();
}

void VoicesSettingsPage::Reset()
{
	m_biometricClient.ResetProperty(wxT("Voices.UniquePhrasesOnly"));
	m_biometricClient.ResetProperty(wxT("Voices.ExtractTextDependentFeatures"));
	m_biometricClient.ResetProperty(wxT("Voices.ExtractTextIndependentFeatures"));
	m_biometricClient.ResetProperty(wxT("Voices.MaximalLoadedFileSize"));
	Load();
}

void VoicesSettingsPage::OnMicrophoneChanged(wxCommandEvent&)
{
	unsigned int selection = m_choiceMicrophone->GetSelection();

	wxStringClientData * stringData = reinterpret_cast<wxStringClientData *>(m_choiceMicrophone->GetClientObject(selection));
	if (stringData == NULL) return;

	try
	{
		NDeviceManager::NDeviceCollection devices = m_biometricClient.GetDeviceManager().GetDevices();
		for (int i = 0; i < devices.GetCount(); ++i)
		{
			NDevice device = devices.Get(i);
			if ((wxString)device.GetId() == stringData->GetData())
			{
				m_biometricClient.SetVoiceCaptureDevice(NObjectDynamicCast<NMicrophone>(device));
				break;
			}
		}
	}
	catch(NError& ex)
	{
		wxExceptionDlg::Show(ex);
	}

	UpdateCaptureFormats();
}

void VoicesSettingsPage::OnFormatChanged(wxCommandEvent&)
{
	NMicrophone microphone = m_biometricClient.GetVoiceCaptureDevice();
	if (microphone.IsNull()) return;

	wxString selection = m_choiceFormat->GetStringSelection();
	if (selection.IsEmpty()) return;

	try
	{
		NArrayWrapper<NMediaFormat> formats = microphone.GetFormats();
		for (int i = 0; i < formats.GetCount(); ++i)
		{
			if ((wxString)formats[i].ToString() == selection)
			{
				microphone.SetCurrentFormat(formats[i]);
			}
		}
	}
	catch(NError& ex)
	{
		wxExceptionDlg::Show(ex);
	}
}

void VoicesSettingsPage::OnUniquePhrasesOnlyChanged(wxCommandEvent&)
{
	m_biometricClient.SetVoicesUniquePhrasesOnly(m_chkUniquePhrasesOnly->GetValue());
}

void VoicesSettingsPage::OnExtractTextDependentFeaturesChanged(wxCommandEvent&)
{
	m_biometricClient.SetVoicesExtractTextDependentFeatures(m_chkExtractTextDependentFeatures->GetValue());
}

void VoicesSettingsPage::OnExtractTextIndependentFeaturesChanged(wxCommandEvent&)
{
	m_biometricClient.SetVoicesExtractTextIndependentFeatures(m_chkExtractTextIndependentFeatures->GetValue());
}

void VoicesSettingsPage::OnMaximalLoadedFileSizeChanged(wxCommandEvent&)
{
	m_biometricClient.SetVoicesMaximalLoadedFileSize(m_scdMaxLoadedFileSize->GetValue() * 1048576);
}

void VoicesSettingsPage::OnDevicesCollectionChanged(Collections::CollectionChangedEventArgs<NDevice> args)
{
	VoicesSettingsPage *voicesSettingsPage = reinterpret_cast<VoicesSettingsPage *>(args.GetParam());
	wxCommandEvent event(wxEVT_VOICE_SETTINGS_THREAD, ID_EVT_UPDATE_DEVICES);
	wxPostEvent(voicesSettingsPage, event);
}

void VoicesSettingsPage::RegisterGuiEvents()
{
	this->Bind(wxEVT_VOICE_SETTINGS_THREAD, &VoicesSettingsPage::OnThread, this);

	m_choiceMicrophone->Connect(wxEVT_CHOICE, wxCommandEventHandler(VoicesSettingsPage::OnMicrophoneChanged), NULL, this);
	m_choiceFormat->Connect(wxEVT_CHOICE, wxCommandEventHandler(VoicesSettingsPage::OnFormatChanged), NULL, this);
	m_chkUniquePhrasesOnly->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(VoicesSettingsPage::OnUniquePhrasesOnlyChanged), NULL, this);
	m_chkExtractTextDependentFeatures->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(VoicesSettingsPage::OnExtractTextDependentFeaturesChanged), NULL, this);
	m_chkExtractTextIndependentFeatures->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(VoicesSettingsPage::OnExtractTextIndependentFeaturesChanged), NULL, this);
	m_scdMaxLoadedFileSize->Connect(wxEVT_SPINCTRLDOUBLE, wxCommandEventHandler(VoicesSettingsPage::OnMaximalLoadedFileSizeChanged), NULL, this);
}

void VoicesSettingsPage::UnregisterGuiEvents()
{
	this->Unbind(wxEVT_VOICE_SETTINGS_THREAD, &VoicesSettingsPage::OnThread, this);

	m_choiceMicrophone->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(VoicesSettingsPage::OnMicrophoneChanged), NULL, this);
	m_choiceFormat->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(VoicesSettingsPage::OnFormatChanged), NULL, this);
	m_chkUniquePhrasesOnly->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(VoicesSettingsPage::OnUniquePhrasesOnlyChanged), NULL, this);
	m_chkExtractTextDependentFeatures->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(VoicesSettingsPage::OnExtractTextDependentFeaturesChanged), NULL, this);
	m_chkExtractTextIndependentFeatures->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(VoicesSettingsPage::OnExtractTextIndependentFeaturesChanged), NULL, this);
	m_scdMaxLoadedFileSize->Disconnect(wxEVT_SPINCTRLDOUBLE, wxCommandEventHandler(VoicesSettingsPage::OnMaximalLoadedFileSizeChanged), NULL, this);
}

void VoicesSettingsPage::CreateGUIControls()
{
	wxBoxSizer *sizer = new wxBoxSizer(wxVERTICAL);
	wxFlexGridSizer *gridSizer = new wxFlexGridSizer(2, 2, 5, 5);

	m_choiceMicrophone = new wxChoice(this, wxID_ANY, wxDefaultPosition, wxDefaultSize);
	m_choiceFormat = new wxChoice(this, wxID_ANY, wxDefaultPosition, wxDefaultSize);
	m_chkUniquePhrasesOnly = new wxCheckBox(this, wxID_ANY, wxT("Unique phrases only"));
	m_chkExtractTextDependentFeatures = new wxCheckBox(this, wxID_ANY, wxT("Extract text dependent features"));
	m_chkExtractTextIndependentFeatures = new wxCheckBox(this, wxID_ANY, wxT("Extract text independent features"));
	m_scdMaxLoadedFileSize = new wxSpinCtrlDouble(this, wxID_ANY, wxEmptyString, wxDefaultPosition, wxSize(128, 20),
		wxALL | wxSP_ARROW_KEYS, 0, 1024);

	wxStaticText *lblInfo = new wxStaticText(this, wxID_ANY,
		wxT("Specifies whether each user says a unique phrase. ")
			wxT("Unchecking this option allows to use the same phrase for different users but false rejection rate (FRR) increases, ")
			wxT("thus it is recommended to lower matcher matching threshold (this parameter can be found in General parameters tab)."));

	lblInfo->Wrap(500);

	wxBoxSizer *boxSizer = new wxBoxSizer(wxHORIZONTAL);
	wxStaticText *lblMaxFileSize = new wxStaticText(this, wxID_ANY, wxT("Maximal loaded\r\n file size (MB):"));

	gridSizer->Add(new wxStaticText(this, wxID_ANY, wxT("Microphone:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	gridSizer->Add(m_choiceMicrophone, 0, wxALL | wxEXPAND, 0);

	gridSizer->Add(new wxStaticText(this, wxID_ANY, wxT("Format:")), 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 0);
	gridSizer->Add(m_choiceFormat, 0, wxALL | wxEXPAND, 0);
	sizer->Add(gridSizer, 0, wxALL | wxEXPAND, 5);
	sizer->Add(m_chkUniquePhrasesOnly, 0, wxALL | wxEXPAND, 5);
	sizer->Add(lblInfo, 0, wxBOTTOM | wxLEFT | wxEXPAND, 10);
	sizer->Add(m_chkExtractTextDependentFeatures, 0, wxALL | wxEXPAND, 5);
	sizer->Add(m_chkExtractTextIndependentFeatures, 0, wxALL | wxEXPAND, 5);

	boxSizer->Add(lblMaxFileSize, 0, wxALL, 5);
	boxSizer->Add(m_scdMaxLoadedFileSize, 0, wxALL | wxALIGN_CENTER_VERTICAL, 5);
	sizer->Add(boxSizer, 0, wxALL, 5);

	this->SetSizer(sizer, true);
	this->Layout();
}

}}


#include "Precompiled.h"

#include <Common/LicensingTools.h>

#include <Settings/SettingsPanel.h>

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;

namespace Neurotec { namespace Samples
{

SettingsPanel::SettingsPanel(wxWindow *parent, wxWindowID winid):
	TabPage(parent, winid),
	m_biometricClient(NULL),
	m_activePage(NULL),
	m_generalPage(NULL),
	m_fingersPage(NULL),
	m_facesPage(NULL),
	m_irisesPage(NULL),
	m_palmsPage(NULL),
	m_voicesPage(NULL)
{
	CreateGUIControls();
	RegisterGuiEvents();
}

SettingsPanel::~SettingsPanel()
{
	UnregisterGuiEvents();
}

void SettingsPanel::Initialize(NBiometricClient biometricClient)
{
	m_biometricClient = biometricClient;

	if (LicensingTools::CanCreateFingerTemplate(m_biometricClient.GetLocalOperations()))
	{
		m_fingersSelIndex = m_list->Append(wxT("Fingers"));

		m_fingersPage = new FingersSettingsPage(m_pagePanel, wxID_ANY);
		m_pageLayout->Add(m_fingersPage, 1, wxALL | wxEXPAND, 0);
		m_fingersPage->Hide();
	}

	if (LicensingTools::CanCreateFaceTemplate(m_biometricClient.GetLocalOperations()))
	{
		m_facesSelIndex = m_list->Append(wxT("Faces"));

		m_facesPage = new FacesSettingsPage(m_pagePanel, wxID_ANY);
		m_pageLayout->Add(m_facesPage, 1, wxALL | wxEXPAND, 0);
		m_facesPage->Hide();
	}

	if (LicensingTools::CanCreateIrisTemplate(m_biometricClient.GetLocalOperations()))
	{
		m_irisesSelIndex = m_list->Append(wxT("Irises"));

		m_irisesPage = new IrisesSettingsPage(m_pagePanel, wxID_ANY);
		m_pageLayout->Add(m_irisesPage, 1, wxALL | wxEXPAND, 0);
		m_irisesPage->Hide();
	}

	if (LicensingTools::CanCreatePalmTemplate(m_biometricClient.GetLocalOperations()))
	{
		m_palmsSelIndex = m_list->Append(wxT("Palms"));

		m_palmsPage = new PalmsSettingsPage(m_pagePanel, wxID_ANY);
		m_pageLayout->Add(m_palmsPage, 1, wxALL | wxEXPAND, 0);
		m_palmsPage->Hide();
	}

	if (LicensingTools::CanCreateVoiceTemplate(m_biometricClient.GetLocalOperations()))
	{
		m_voicesSelIndex = m_list->Append(wxT("Voices"));

		m_voicesPage = new VoicesSettingsPage(m_pagePanel, wxID_ANY);
		m_pageLayout->Add(m_voicesPage, 1, wxALL | wxEXPAND, 0);
		m_voicesPage->Hide();
	}

	m_generalPage->Initialize(m_biometricClient);

	if (m_fingersPage != NULL)
		m_fingersPage->Initialize(m_biometricClient);

	if (m_facesPage != NULL)
		m_facesPage->Initialize(m_biometricClient);

	if (m_irisesPage != NULL)
		m_irisesPage->Initialize(m_biometricClient);

	if (m_palmsPage != NULL)
		m_palmsPage->Initialize(m_biometricClient);

	if (m_voicesPage != NULL)
		m_voicesPage->Initialize(m_biometricClient);

	m_list->SetSelection(m_generalSelIndex);
	SetActivePage(m_generalSelIndex);

	LoadParameters();

	if (!m_biometricClient.IsNull())
	{
		m_biometricClient.CaptureProperties(m_properties);
	}
}

void SettingsPanel::SetActivePage(int page)
{
	if (m_activePage != NULL)
	{
		m_activePage->Hide();
		m_activePage = NULL;
	}

 	if (page == m_generalSelIndex)
 	{
 		m_activePage = dynamic_cast<BaseSettingsPage *>(m_generalPage);
 	}
 	else if (page == m_fingersSelIndex)
 	{
 		m_activePage = dynamic_cast<BaseSettingsPage *>(m_fingersPage);
 	}
 	else if (page == m_facesSelIndex)
 	{
 		m_activePage = dynamic_cast<BaseSettingsPage *>(m_facesPage);
 	}
 	else if (page == m_irisesSelIndex)
 	{
 		m_activePage = dynamic_cast<BaseSettingsPage *>(m_irisesPage);
 	}
 	else if (page == m_palmsSelIndex)
 	{
 		m_activePage = dynamic_cast<BaseSettingsPage *>(m_palmsPage);
 	}
 	else if (page == m_voicesSelIndex)
 	{
 		m_activePage = dynamic_cast<BaseSettingsPage *>(m_voicesPage);
 	}

	if (m_activePage != NULL)
	{
		m_activePage->Show();
		m_pageLayout->Layout();
	}
}

void SettingsPanel::LoadParameters()
{
	m_generalPage->Load();

	if (m_fingersPage != NULL)
		m_fingersPage->Load();

	if (m_facesPage != NULL)
		m_facesPage->Load();

	if (m_irisesPage != NULL)
		m_irisesPage->Load();

	if (m_palmsPage != NULL)
		m_palmsPage->Load();

	if (m_voicesPage != NULL)
		m_voicesPage->Load();
}

void SettingsPanel::OnDefaultClick(wxCommandEvent &)
{
	m_generalPage->Reset();

	if (m_fingersPage != NULL)
		m_fingersPage->Reset();

	if (m_facesPage != NULL)
		m_facesPage->Reset();

	if (m_irisesPage != NULL)
		m_irisesPage->Reset();

	if (m_palmsPage != NULL)
		m_palmsPage->Reset();

	if (m_voicesPage != NULL)
		m_voicesPage->Reset();
}

void SettingsPanel::OnOKClick(wxCommandEvent &)
{
	this->Close();
}

void SettingsPanel::OnCancelClick(wxCommandEvent &)
{
	m_properties.ApplyTo(m_biometricClient);
	this->Close();
}

void SettingsPanel::OnListSelectionChanged(wxCommandEvent&)
{
	SetActivePage(m_list->GetSelection());
}

void SettingsPanel::RegisterGuiEvents()
{
	m_list->Connect(wxEVT_COMMAND_LISTBOX_SELECTED, wxCommandEventHandler(SettingsPanel::OnListSelectionChanged), NULL, this);
	m_btnDefault->Connect(wxEVT_BUTTON, wxCommandEventHandler(SettingsPanel::OnDefaultClick), NULL, this);
	m_btnOK->Connect(wxEVT_BUTTON, wxCommandEventHandler(SettingsPanel::OnOKClick), NULL, this);
	m_btnCancel->Connect(wxEVT_BUTTON, wxCommandEventHandler(SettingsPanel::OnCancelClick), NULL, this);
}

void SettingsPanel::UnregisterGuiEvents()
{
	m_list->Disconnect(wxEVT_COMMAND_LISTBOX_SELECTED, wxCommandEventHandler(SettingsPanel::OnListSelectionChanged), NULL, this);
	m_btnDefault->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(SettingsPanel::OnDefaultClick), NULL, this);
	m_btnOK->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(SettingsPanel::OnOKClick), NULL, this);
	m_btnCancel->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(SettingsPanel::OnCancelClick), NULL, this);
}

void SettingsPanel::CreateGUIControls()
{
	wxBoxSizer *sizer = new wxBoxSizer(wxHORIZONTAL);
	wxBoxSizer *topSizer = new wxBoxSizer(wxVERTICAL);

	wxFlexGridSizer *controlsSizer = new wxFlexGridSizer(1, 3, 0, 0);
	controlsSizer->AddGrowableCol(0);
	controlsSizer->SetFlexibleDirection(wxBOTH);
	controlsSizer->SetNonFlexibleGrowMode(wxFLEX_GROWMODE_SPECIFIED);
	controlsSizer->SetHGap(5);

	m_list = new wxListBox(this, wxID_ANY);
	m_generalSelIndex = m_list->Append(wxT("General"));

	m_btnDefault = new wxButton(this, wxID_ANY, wxT("Default"));
	m_btnOK = new wxButton(this, wxID_ANY, wxT("OK"));
	m_btnCancel = new wxButton(this, wxID_ANY, wxT("Cancel"));

	m_pageLayout = new wxBoxSizer(wxVERTICAL);
	m_pagePanel = new wxPanel(this, wxID_ANY);
	m_pagePanel->SetSizer(m_pageLayout, true);

	controlsSizer->Add(m_btnDefault);
	controlsSizer->Add(m_btnOK);
	controlsSizer->Add(m_btnCancel);

	topSizer->Add(m_pagePanel, 1, wxALL | wxEXPAND, 5);
	topSizer->Add(controlsSizer, 0, wxALL | wxEXPAND, 5);

	sizer->Add(m_list, 0, wxALL | wxEXPAND, 5);
	sizer->Add(topSizer, 1, wxALL | wxEXPAND, 5);

	m_generalPage = new GeneralSettingsPage(m_pagePanel, wxID_ANY);
	m_pageLayout->Add(m_generalPage, 1, wxALL | wxEXPAND, 0);

	m_generalPage->Hide();

	this->SetSizer(sizer, true);
	this->Layout();
	this->Center();
}

}}


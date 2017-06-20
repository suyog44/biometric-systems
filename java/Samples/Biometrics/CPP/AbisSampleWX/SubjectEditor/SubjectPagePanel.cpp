#include "Precompiled.h"

#include <Common/LongActionDialog.h>
#include <Common/TabController.h>
#include <Common/TabPage.h>
#include <Common/DatabaseOperationPage.h>
#include <Common/LicensingTools.h>

#include <SubjectEditor/SubjectPagePanel.h>
#include <SubjectEditor/SubjectPage.h>

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Images;
using namespace Neurotec::Collections;

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_THREAD, wxCommandEvent);

SubjectPagePanel::SubjectPagePanel(NBiometricClient& biometricClient, NSubject subject, TabController *parent, wxWindowID winid)
	: TabPage(parent, winid), m_biometricClient(biometricClient)
{
	m_currentModalityPage = NULL;

	if (subject.IsNull())
		m_subject = NSubject();
	else
		m_subject = subject;

	CreateGUIControls();
	RegisterGuiEvents();

	m_treeCtrl->SetSubject(m_subject);
}

SubjectPagePanel::~SubjectPagePanel()
{
	if (m_subjectPage != NULL)
		m_subjectPage->Destroy();

	if (m_captureFingersPage != NULL)
		m_captureFingersPage->Destroy();

	if (m_captureFacePage != NULL)
		m_captureFacePage->Destroy();

	if (m_captureIrisesPage != NULL)
		m_captureIrisesPage->Destroy();

	if (m_capturePalmPage != NULL)
		m_capturePalmPage->Destroy();

	if (m_captureVoicePage != NULL)
		m_captureVoicePage->Destroy();

	if (m_previewPage != NULL)
		m_previewPage->Destroy();

	UnregisterGuiEvents();
}

void SubjectPagePanel::OnThread(wxCommandEvent &event)
{
	int id = event.GetId();

	switch(id)
	{
	case ID_EVENT_SELECT_FIRST_PAGE:
		{
			Node * node = m_treeCtrl->GetSubjectNode();
			m_treeCtrl->SetSelectedItem(node);
			break;
		}
	default:
		break;
	};
}

void SubjectPagePanel::OnClose()
{
	SelectPage(m_treeCtrl->GetSubjectNode());
}

void SubjectPagePanel::OnLeavePage()
{
	SelectPage(m_treeCtrl->GetSubjectNode());
}

void SubjectPagePanel::OnSelectPage()
{
	NBiometricType types = nbtNone;
	if (LicensingTools::CanCreateFingerTemplate(m_biometricClient.GetLocalOperations()))
		types = (NBiometricType)(types | nbtFinger);
	if (LicensingTools::CanCreateFaceTemplate(m_biometricClient.GetLocalOperations()))
		types = (NBiometricType)(types | nbtFace);
	if (LicensingTools::CanCreateIrisTemplate(m_biometricClient.GetLocalOperations()))
		types = (NBiometricType)(types | nbtIris);
	if (LicensingTools::CanCreatePalmTemplate(m_biometricClient.GetLocalOperations()))
		types = (NBiometricType)(types | nbtPalm);
	if (LicensingTools::CanCreateVoiceTemplate(m_biometricClient.GetLocalOperations()))
		types = (NBiometricType)(types | nbtVoice);
	m_treeCtrl->SetAllowNew(types);

	Node * node = m_treeCtrl->GetSubjectNode();
	m_treeCtrl->SetSelectedItem(node);
}

void SubjectPagePanel::SelectFirstPage()
{
	wxCommandEvent event(wxEVT_THREAD, ID_EVENT_SELECT_FIRST_PAGE);
	wxPostEvent(this, event);
}

void SubjectPagePanel::SelectPage(Node * selected)
{
	ModalityPage * modalityPanel = this->m_currentModalityPage;
	if (!selected || selected->IsSubjectNode())
	{
		modalityPanel = dynamic_cast<ModalityPage *>(this->m_subjectPage);
	}
	else
	{
		if (selected->IsBiometricNode())
		{
			this->m_previewPage->SetSelection(selected);
			modalityPanel = dynamic_cast<ModalityPage *>(this->m_previewPage);
		}
		else
		{
			switch (selected->GetBiometricType())
			{
				case nbtFace: modalityPanel = dynamic_cast<ModalityPage *>(this->m_captureFacePage); break;
				case nbtFinger: modalityPanel = dynamic_cast<ModalityPage *>(this->m_captureFingersPage); break;
				case nbtIris: modalityPanel = dynamic_cast<ModalityPage *>(this->m_captureIrisesPage); break;
				case nbtPalm: modalityPanel = dynamic_cast<ModalityPage *>(this->m_capturePalmPage); break;
				case nbtVoice: modalityPanel = dynamic_cast<ModalityPage *>(this->m_captureVoicePage); break;
				default: break;
			}
		}
	}

	if (modalityPanel != this->m_currentModalityPage || (selected && selected->IsBiometricNode()))
	{
		if (this->m_currentModalityPage != NULL)
		{
			this->m_currentModalityPage->OnNavigatingFrom();
			this->m_currentModalityPage->Hide();
			this->m_rightPanelLayout->Remove(0);
			this->m_currentModalityPage = NULL;
		}

		if (modalityPanel != NULL)
		{
			this->m_currentModalityPage = modalityPanel;
			this->m_rightPanelLayout->Add(this->m_currentModalityPage, 1, wxALL | wxEXPAND, 0);
			this->m_currentModalityPage->OnNavigatedTo();
			this->m_currentModalityPage->Show();
			this->m_rightPanelLayout->Layout();
		}
	}
}

void SubjectPagePanel::OnSubjectTreeSelectedItemChanged(wxCommandEvent & /*event*/)
{
	SelectPage(m_treeCtrl->GetSelectedItem());
}

void SubjectPagePanel::PerformOperation(NBiometricOperations operation)
{
	TabController *tabController = dynamic_cast<TabController *>(this->GetParent());

	wxString strOperation = wxEmptyString;

	switch(operation)
	{
	case nboVerify:
		strOperation = wxString::Format(wxT("Verify: %s"), (wxString)m_subject.GetId());
		break;
	case nboEnrollWithDuplicateCheck:
	case nboEnroll:
		strOperation = wxString::Format(wxT("Enroll: %s"), (wxString)m_subject.GetId());
		break;
	case nboUpdate:
		strOperation = wxString::Format(wxT("Update: %s"), (wxString)m_subject.GetId());
		break;
	case nboIdentify:
		strOperation = wxT("Identify");
		break;
	default:
		break;
	};

	if (strOperation.Length() > 30)
		strOperation = strOperation.SubString(0, 30) << wxT("...");

	DatabaseOperationPage *operationPage = new DatabaseOperationPage(dynamic_cast<wxWindow *>(tabController), wxID_ANY, m_biometricClient, m_subject, operation);
	tabController->AddPage(operationPage, strOperation, true);
}

void SubjectPagePanel::RegisterGuiEvents()
{
	this->Bind(wxEVT_THREAD, &SubjectPagePanel::OnThread, this);
	m_treeCtrl->Bind(wxEVT_TREE_SELECTED_ITEM_CHANGED, &SubjectPagePanel::OnSubjectTreeSelectedItemChanged, this);
}

void SubjectPagePanel::UnregisterGuiEvents()
{
	this->Unbind(wxEVT_THREAD, &SubjectPagePanel::OnThread, this);
}

void SubjectPagePanel::CreateGUIControls()
{
	//Layout
	wxBoxSizer *sizer = new wxBoxSizer(wxVERTICAL);

	//Splitter window
	wxSplitterWindow *splitterWindow = new wxSplitterWindow(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxSP_NOBORDER | wxSP_3DSASH | wxSP_LIVE_UPDATE);
	m_leftPanel = new wxPanel(splitterWindow, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxTAB_TRAVERSAL | wxSIMPLE_BORDER);
	m_rightPanel = new wxPanel(splitterWindow, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxTAB_TRAVERSAL);
	splitterWindow->SplitVertically(m_leftPanel, m_rightPanel, 200);
	sizer->Add(splitterWindow, 1, wxEXPAND | wxALL, 0);

	//Right panel
	m_rightPanelLayout = new wxBoxSizer(wxVERTICAL);
	m_rightPanel->SetSizer(m_rightPanelLayout, true);

	//Left panel
	wxSizer *leftPanelSizer = new wxBoxSizer(wxVERTICAL);
	m_leftPanel->SetSizer(leftPanelSizer, true);

	//Tree control
	m_treeCtrl = new SubjectTreeWidget(m_leftPanel, wxID_ANY);
	leftPanelSizer->Add(m_treeCtrl, 1, wxEXPAND | wxALL, 0);

	//Pages
	m_subjectPage = new SubjectPage(m_biometricClient, m_subject, *this, m_rightPanel);
	m_captureFingersPage = new CaptureFingersPage(m_biometricClient, m_subject, *this, m_rightPanel);
	m_captureFacePage = new CaptureFacePage(m_biometricClient, m_subject, *this, m_rightPanel);
	m_captureIrisesPage = new CaptureIrisesPage(m_biometricClient, m_subject, *this, m_rightPanel);
	m_capturePalmPage = new CapturePalmPage(m_biometricClient, m_subject, *this, m_rightPanel);
	m_captureVoicePage = new CaptureVoicePage(m_biometricClient, m_subject, *this, m_rightPanel);
	m_previewPage = new PreviewBiometricPage(m_biometricClient, m_subject, *this, m_rightPanel);

	m_subjectPage->Hide();
	m_captureFingersPage->Hide();
	m_captureFacePage->Hide();
	m_captureIrisesPage->Hide();
	m_capturePalmPage->Hide();
	m_captureVoicePage->Hide();
	m_previewPage->Hide();

	//Main panel
	this->Layout();
	this->SetSizer(sizer, true);
	this->Center();
}

}}

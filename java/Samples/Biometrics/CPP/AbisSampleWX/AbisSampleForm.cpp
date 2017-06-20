#include "Precompiled.h"
#include "AbisSampleWXVersionInfo.h"

#include <AbisSampleForm.h>

#include <SubjectEditor/SubjectPagePanel.h>

#include <Settings/SettingsPanel.h>
#include <Settings/SettingsManager.h>

#include <Common/ChangeDatabaseDialog.h>
#include <Common/OpenSubjectDialog.h>
#include <Common/FirstPage.h>
#include <Common/GetSubjectDialog.h>
#include <Common/EnrollDataSerializer.h>

#include <Resources/NewFileIcon.xpm>
#include <Resources/OpenFolderIcon.xpm>
#include <Resources/SettingsIcon.xpm>
#include <Resources/DatabaseIcon.xpm>
#include <Resources/GetSubjectIcon.xpm>

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NGui/Gui/Neurotechnology.xpm>
#else
	#include <Gui/Neurotechnology.xpm>
#endif

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Gui;
using namespace Neurotec::Licensing;
using namespace Neurotec::IO;

namespace Neurotec { namespace Samples
{

	const wxString licenses[] = 
	{
		wxT("Biometrics.FingerExtraction"),
		wxT("Biometrics.PalmExtraction"),
		wxT("Biometrics.FaceExtraction"),
		wxT("Biometrics.IrisExtraction"),
		wxT("Biometrics.VoiceExtraction"),
		wxT("Biometrics.FingerMatchingFast"),
		wxT("Biometrics.FingerMatching"),
		wxT("Biometrics.PalmMatchingFast"),
		wxT("Biometrics.PalmMatching"),
		wxT("Biometrics.VoiceMatching"),
		wxT("Biometrics.FaceMatchingFast"),
		wxT("Biometrics.FaceMatching"),
		wxT("Biometrics.IrisMatchingFast"),
		wxT("Biometrics.IrisMatching"),
		wxT("Biometrics.FingerQualityAssessment"),
		wxT("Biometrics.FingerSegmentation"),
		wxT("Biometrics.FingerSegmentsDetection"),
		wxT("Biometrics.PalmSegmentation"),
		wxT("Biometrics.FaceSegmentation"),
		wxT("Biometrics.IrisSegmentation"),
		wxT("Biometrics.VoiceSegmentation"),
		wxT("Biometrics.Standards.Fingers"),
		wxT("Biometrics.Standards.FingerTemplates"),
		wxT("Biometrics.Standards.Faces"),
		wxT("Biometrics.Standards.Irises"),
		wxT("Devices.Cameras"),
		wxT("Devices.FingerScanners"),
		wxT("Devices.IrisScanners"),
		wxT("Devices.PalmScanners"),
		wxT("Devices.Microphones"),
		wxT("Images.WSQ"),
		wxT("Media")
	};

BEGIN_EVENT_TABLE(AbisSampleForm, wxFrame)
	EVT_MENU(ID_MENU_CHANGE_DATABASE, AbisSampleForm::MenuChangeDatabaseClick)
	EVT_MENU(wxID_EXIT, AbisSampleForm::MenuExitClick)
	EVT_MENU(wxID_ABOUT, AbisSampleForm::MenuAboutClick)
	EVT_MENU(ID_MENU_NEW_SUBJECT, AbisSampleForm::MenuCreateSubjectClick)
	EVT_MENU(ID_MENU_OPEN_SUBJECT, AbisSampleForm::MenuOpenSubjectClick)
	EVT_MENU(ID_MENU_GET_SUBJECT, AbisSampleForm::MenuGetSubjectClick)
	EVT_MENU(ID_MENU_SETTINGS, AbisSampleForm::MenuSettingsClick)
	EVT_CLOSE(AbisSampleForm::OnClosing)
END_EVENT_TABLE()

AbisSampleForm::AbisSampleForm(wxWindow *parent, const wxWindowID id, const wxString &title)
	: wxFrame(parent, id, title), m_biometricClient(NULL)
{
	m_settingsPanel = NULL;
	m_settingsPanelState = TabPage::CLOSED;

	CreateGUIControls();
	RegisterGuiEvents();

	const wxString address = wxT("/local");
	const wxString port = wxT("5000");
	try
	{
		for (size_t i = 0; i < sizeof(licenses) / sizeof(licenses[0]); i++)
		{
			NLicense::ObtainComponents(address, port, licenses[i]);
		}
	}
	catch (NError& ex)
	{
		wxMessageBox(wxString::Format(wxT("Failed to obtain license for AbisSample!\n(Probably licensing service is not running. Use Activation Wizard to figure it out.). Reason: %s"), 
			((wxString)ex.GetMessage()).c_str()));
		Close(true);
	}

	ChangeDatabaseDialog changeDatabaseDialog(m_biometricClient, this, wxID_ANY, wxT("Connection settings"));

	if (changeDatabaseDialog.ShowModal() != wxID_OK)
	{
		Close(true);
		return;
	}
}

AbisSampleForm::~AbisSampleForm()
{
	UnregisterGuiEvents();

	m_tabbedPanel->DeleteAllPages();

	SettingsManager::SaveSettings(m_biometricClient);

	try
	{
		for (size_t i = 0; i < sizeof(licenses) / sizeof(licenses[0]); i++)
		{
			NLicense::ReleaseComponents(licenses[i]);
		}
	}
	catch (NError& ex)
	{
		wxExceptionDlg::Show(ex);
	}
}

void AbisSampleForm::About()
{
	wxAboutBox aboutBox(this, -1, ABIS_SAMPLE_WX_PRODUCT_NAME, ABIS_SAMPLE_WX_VERSION_STRING, ABIS_SAMPLE_WX_COPYRIGHT);
	aboutBox.ShowModal();
}

void AbisSampleForm::CreateSubject(const NSubject& subject)
{
	SubjectPagePanel *subjectPanel = new SubjectPagePanel(m_biometricClient, subject, m_tabbedPanel);
	m_tabbedPanel->AddPage(subjectPanel, wxT("Subject"), true);
}

void AbisSampleForm::OpenSubject()
{
	OpenSubjectDialog dialog(this, wxID_ANY, wxT("Open subject template"));

	if (dialog.ShowModal() != wxID_OK || dialog.GetFilePath() == wxEmptyString)
	{
		return;
	}

	try
	{
		NSubject subject = NSubject::FromFile(dialog.GetFilePath(), dialog.GetFormatOwner(), dialog.GetFormatType());
		SubjectPagePanel *subjectPanel = new SubjectPagePanel(m_biometricClient, subject, m_tabbedPanel);

		wxString subjectID = (wxString)subject.GetId();
		if (subjectID == wxEmptyString)
			subjectID = wxT("Subject");

		if (subjectID.Length() > 30)
			subjectID = subjectID.SubString(0, 30) << wxT("...");

		m_tabbedPanel->AddPage(subjectPanel, subjectID, true);
	}
	catch (NError& error)
	{
		wxExceptionDlg::Show(error);
	}
}

void AbisSampleForm::Settings()
{
	if (m_settingsPanelState != TabPage::CLOSED)
		return;

	m_settingsPanel = new SettingsPanel(m_tabbedPanel);
	m_settingsPanel->SetStateMonitorVariable(&m_settingsPanelState);
	m_settingsPanel->Initialize(m_biometricClient);

	m_tabbedPanel->AddPage(m_settingsPanel, wxT("Settings"), true);
}

void AbisSampleForm::GetSubject()
{
	GetSubjectDialog dialog(m_biometricClient, this, wxID_ANY);

	if (dialog.ShowModal() == wxID_OK)
	{
		try
		{
			CreateSubject(RecreateSubject(dialog.GetSubject()));
		}
		catch (NError& error)
		{
			wxExceptionDlg::Show(error);
		}
	}
}

void AbisSampleForm::ChangeDatabase()
{
	if (m_tabbedPanel->GetPageCount() > 0)
	{
		if (wxMessageDialog(this, wxT("Changing database will close all currently opened tabs. Do you want to continue?"),
			wxEmptyString, wxOK | wxCANCEL | wxICON_QUESTION | wxCENTRE).ShowModal() != wxID_OK)
		{
			return;
		}

		m_tabbedPanel->CloseAllPages();
	}

	ChangeDatabaseDialog changeDatabaseDialog(m_biometricClient, this, wxID_ANY, wxT("Connection settings"));
	changeDatabaseDialog.ShowModal();
	NBiometricOperations operations = m_biometricClient.GetLocalOperations();
	if (m_biometricClient.GetRemoteConnections().GetCount() > 0)
	{
		operations = (NBiometricOperations)((int)operations | (int)m_biometricClient.GetRemoteConnections().Get(0).GetOperations());
	}
	m_btnGetSubject->Enable((operations & nboGet) == nboGet);
}

void AbisSampleForm::MenuAboutClick(wxCommandEvent & /*event*/)
{
	About();
}

void AbisSampleForm::MenuCreateSubjectClick(wxCommandEvent & /*event*/)
{
	CreateSubject();
}

void AbisSampleForm::MenuOpenSubjectClick(wxCommandEvent & /*event*/)
{
	OpenSubject();
}

void AbisSampleForm::MenuChangeDatabaseClick(wxCommandEvent & /*event*/)
{
	ChangeDatabase();
}

void AbisSampleForm::MenuSettingsClick(wxCommandEvent & /*event*/)
{
	Settings();
}

void AbisSampleForm::MenuGetSubjectClick(wxCommandEvent & /*event*/)
{
	GetSubject();
}

void AbisSampleForm::MenuExitClick(wxCommandEvent & /*event*/)
{
	m_tabbedPanel->CloseAllPages();
	Close(false);
}

void AbisSampleForm::OnClosing(wxCloseEvent &)
{
	m_tabbedPanel->CloseAllPages();
	Destroy();
}

NSubject AbisSampleForm::RecreateSubject(const NSubject& subject)
{
	SampleDbSchema schema = SettingsManager::GetCurrentSchema();
	bool hasShema = !schema.IsEmpty();
	NSubject resultSubject = subject;
	wxArrayInt galeryRecords;

	if (hasShema)
	{
		NPropertyBag bag = subject.GetProperties();

		if (!(schema.enrollDataName.IsNull() || schema.enrollDataName.IsEmpty()) && bag.Contains(schema.enrollDataName))
		{
			NBuffer templateBuffer = subject.GetTemplateBuffer();
			NBuffer enrollData = bag.Get(schema.enrollDataName).ToObject<NBuffer>();
			resultSubject = EnrollDataSerializer::Deserialize(templateBuffer, enrollData, galeryRecords);

			std::vector<wxString> allProperties;
			for (NPropertyBag::iterator it = bag.begin(); it != bag.end(); it++)
			{
				allProperties.push_back(NString(it->hKey, false));
			}

			std::vector<wxString> allowedProperties;
			NBiographicDataSchema::ElementCollection biographic = schema.biographicData.GetElements();
			for (NBiographicDataSchema::ElementCollection::iterator it = biographic.begin(); it != biographic.end(); it++)
			{
				allowedProperties.push_back(it->GetName());
			}
			NBiographicDataSchema::ElementCollection custom = schema.customData.GetElements();
			for (NBiographicDataSchema::ElementCollection::iterator it = custom.begin(); it != custom.end(); it++)
			{
				allowedProperties.push_back(it->GetName());
			}

			for (std::vector<wxString>::iterator it = allProperties.begin(); it != allProperties.end(); it++)
			{
				if (std::find(allowedProperties.begin(), allowedProperties.end(), *it) == allowedProperties.end())
				{
					bag.Remove(*it);
				}
			}

			bag.ApplyTo(resultSubject);
			resultSubject.SetId(subject.GetId());
		}
		if (!(schema.genderDataName.IsNull() || schema.genderDataName.IsEmpty()) && bag.Contains(schema.genderDataName))
		{
			NString genderString = bag.Get(schema.genderDataName).ToString();
			resultSubject.SetProperty<NString>(schema.genderDataName, genderString);
		}
	}

	return resultSubject;
}

void AbisSampleForm::RegisterGuiEvents()
{
	m_btnNewSubject->Connect(wxEVT_BUTTON, wxCommandEventHandler(AbisSampleForm::MenuCreateSubjectClick), NULL, this);
	m_btnOpenSubject->Connect(wxEVT_BUTTON, wxCommandEventHandler(AbisSampleForm::MenuOpenSubjectClick), NULL, this);
	m_btnSettings->Connect(wxEVT_BUTTON, wxCommandEventHandler(AbisSampleForm::MenuSettingsClick), NULL, this);
	m_btnChangeDatabase->Connect(wxEVT_BUTTON, wxCommandEventHandler(AbisSampleForm::MenuChangeDatabaseClick), NULL, this);
	m_btnGetSubject->Connect(wxEVT_BUTTON, wxCommandEventHandler(AbisSampleForm::MenuGetSubjectClick), NULL, this);
}

void AbisSampleForm::UnregisterGuiEvents()
{
	m_btnNewSubject->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(AbisSampleForm::MenuCreateSubjectClick), NULL, this);
	m_btnOpenSubject->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(AbisSampleForm::MenuOpenSubjectClick), NULL, this);
	m_btnSettings->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(AbisSampleForm::MenuSettingsClick), NULL, this);
	m_btnChangeDatabase->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(AbisSampleForm::MenuChangeDatabaseClick), NULL, this);
}

void AbisSampleForm::CreateGUIControls()
{
	//Layout
	wxBoxSizer *sizer = new wxBoxSizer(wxVERTICAL);

	//Menu bar
	m_menuBar = new wxMenuBar();

	wxMenu *menuJobs = new wxMenu();

	wxMenuItem *mnuNewSubject = new wxMenuItem(menuJobs, ID_MENU_NEW_SUBJECT, wxT("&Create subject"), wxT("Create new subject"), wxITEM_NORMAL);
	mnuNewSubject->SetBitmap(wxImage(wxImage(newFileIcon_xpm)));
	wxMenuItem *mnuOpenSubject = new wxMenuItem(menuJobs, ID_MENU_OPEN_SUBJECT, wxT("&Open subject"), wxT("Open subject template"), wxITEM_NORMAL);
	mnuOpenSubject->SetBitmap(wxImage(wxImage(openFolderIcon_xpm)));

	menuJobs->Append(mnuNewSubject);
	menuJobs->Append(mnuOpenSubject);
	menuJobs->AppendSeparator();
	menuJobs->Append(wxID_EXIT);

	wxMenu *menuTools = new wxMenu();

	wxMenuItem *mnuSettings = new wxMenuItem(menuJobs, ID_MENU_SETTINGS, wxT("&Settings"), wxT("Settings"), wxITEM_NORMAL);
	mnuSettings->SetBitmap(wxImage(wxImage(settingsIcon_xpm)));

	wxMenuItem *mnuChangeDatabase = new wxMenuItem(menuJobs, ID_MENU_CHANGE_DATABASE, wxT("&Change database"), wxT("Change database"), wxITEM_NORMAL);
	mnuChangeDatabase->SetBitmap(wxImage(wxImage(databaseIcon_xpm)));

	menuTools->Append(mnuSettings);
	menuTools->Append(mnuChangeDatabase);

	wxMenu *menuHelp = new wxMenu();
	menuHelp->Append(wxID_ABOUT);

	m_menuBar->Append(menuJobs, wxT("&Jobs"));
	m_menuBar->Append(menuTools, wxT("&Tools"));
	m_menuBar->Append(menuHelp, wxT("&Help"));

	SetMenuBar(m_menuBar);

	//Tabbed panel
	m_tabbedPanel = new TabController(this, wxID_ANY);
	sizer->Add(m_tabbedPanel, 1, wxEXPAND | wxALL, 0);

	//Tool bar
	CreateToolBar(wxNO_BORDER | wxHORIZONTAL | wxTB_FLAT);
	m_toolBar = GetToolBar();

	m_btnNewSubject = new wxButton(m_toolBar, wxID_ANY, wxT("New subject"), wxDefaultPosition, wxDefaultSize, 0 | wxNO_BORDER);
	m_btnNewSubject->SetBitmap(wxImage(wxImage(newFileIcon_xpm)));

	m_btnOpenSubject = new wxButton(m_toolBar, wxID_ANY, wxT("Open subject"), wxDefaultPosition, wxDefaultSize, 0 | wxNO_BORDER);
	m_btnOpenSubject->SetBitmap(wxImage(wxImage(openFolderIcon_xpm)));

	m_btnGetSubject = new wxButton(m_toolBar, wxID_ANY, wxT("Get subject"), wxDefaultPosition, wxDefaultSize, 0 | wxNO_BORDER);
	m_btnGetSubject->SetBitmap(wxImage(wxImage(getSubjectIcon_xpm).Rescale(16, 16)));

	m_btnSettings = new wxButton(m_toolBar, wxID_ANY, wxT("Settings"), wxDefaultPosition, wxDefaultSize, 0 | wxNO_BORDER);
	m_btnSettings->SetBitmap(wxImage(wxImage(settingsIcon_xpm)));

	m_btnChangeDatabase = new wxButton(m_toolBar, wxID_ANY, wxT("Change database"), wxDefaultPosition, wxDefaultSize, 0 | wxNO_BORDER);
	m_btnChangeDatabase->SetBitmap(wxImage(wxImage(databaseIcon_xpm)));

	m_btnNewSubject->Fit();
	m_btnOpenSubject->Fit();
	m_btnSettings->Fit();
	m_btnChangeDatabase->Fit();

	m_toolBar->AddControl(m_btnNewSubject);
	m_toolBar->AddControl(m_btnOpenSubject);
	m_toolBar->AddControl(m_btnGetSubject);
	m_toolBar->AddControl(m_btnSettings);
	m_toolBar->AddControl(m_btnChangeDatabase);

	m_toolBar->Realize();
	SetToolBar(m_toolBar);

	FirstPage *firstPage = new FirstPage(this, wxID_ANY);
	m_tabbedPanel->AddPage(firstPage, wxT("Start page"), true);

	//Application
	this->Layout();
	this->SetSizer(sizer, true);
	this->SetIcon(Neurotechnology_XPM);
	this->SetTitle(wxT("Multibiometric Sample"));
	this->SetWindowStyleFlag(this->GetWindowStyleFlag() | wxFULL_REPAINT_ON_RESIZE);
	this->SetSize(1152, 800);
	this->Center();
}

}}

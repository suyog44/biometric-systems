#include "Precompiled.h"
#include "FacesSampleForm.h"

#include "EnrollDlg.h"
#include "FacesSampleWXVersionInfo.h"

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NGui/Gui/Neurotechnology.xpm>
#else
	#include <Gui/Neurotechnology.xpm>
#endif

using namespace Neurotec::IO;
using namespace Neurotec::Images;
using namespace Neurotec::Media;
using namespace Neurotec::Media::Processing;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Devices;
using namespace Neurotec::Gui;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Licensing;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Collections;

#define DEFAULT_CLIENT_PROPERTIES \
	wxT("Faces.CreateThumbnailImage=True;")\
	wxT("Faces.DetectAllFeaturePoints=True;")\
	wxT("Faces.DetermineGender=True;")\
	wxT("Faces.DetermineAge=True;")\
	wxT("Faces.DetectProperties=True;")\
	wxT("Faces.RecognizeExpression=True;")\
	wxT("Faces.RecognizeEmotion=True;")

namespace Neurotec { namespace Samples
{

DECLARE_EVENT_TYPE(wxEVT_EXTRACTION, -1)
DEFINE_EVENT_TYPE(wxEVT_EXTRACTION)
DEFINE_EVENT_TYPE(wxEVT_DEVICEPLUG)
DEFINE_EVENT_TYPE(wxEVT_CLIENT_ACTION_COMPLETED)

BEGIN_EVENT_TABLE(FacesSampleForm, wxFrame)
	EVT_CLOSE(FacesSampleForm::OnClose)
	EVT_MENU(ID_MNU_ENROLL, FacesSampleForm::MnuEnrollClick)
	EVT_MENU(ID_MNU_IDENTIFY, FacesSampleForm::MnuIdentifyClick)
	EVT_MENU(ID_MNU_CANCEL, FacesSampleForm::OnCancelAction)
	EVT_MENU(ID_MNU_CLEAR_LOG, FacesSampleForm::MnuClearLogClick)
	EVT_MENU(ID_MNU_CLEAR_DB, FacesSampleForm::MnuClearDatabaseClick)
#ifdef __WXMAC__
	EVT_MENU(wxID_PREFERENCES, FacesSampleForm::MnuOptionsClick)
	EVT_MENU(wxID_ABOUT, FacesSampleForm::MnuAboutClick)
	EVT_MENU(wxID_EXIT, FacesSampleForm::MnuExitClick)
#else
	EVT_MENU(ID_MNU_OPTIONS, FacesSampleForm::MnuOptionsClick)
	EVT_MENU(ID_MNU_ABOUT, FacesSampleForm::MnuAboutClick)
	EVT_MENU(ID_MNU_EXIT, FacesSampleForm::MnuExitClick)
#endif
	EVT_BUTTON(ID_BUTTON_ENROLL, FacesSampleForm::MnuEnrollClick)
	EVT_BUTTON(ID_BUTTON_IDENTIFY, FacesSampleForm::MnuIdentifyClick)

	EVT_COMMAND(wxID_ANY, wxEVT_CLIENT_ACTION_COMPLETED, FacesSampleForm::OnClientActionCompleted)
	EVT_COMBOBOX(ID_COMBO_SOURCE, FacesSampleForm::OnExtractionSourceSelected)
	EVT_COMBOBOX(ID_COMBO_MODE, FacesSampleForm::OnMediaFormatSelected)
	EVT_COMMAND(wxID_ANY, wxEVT_DEVICEPLUG, FacesSampleForm::OnSourcesChanged)
	EVT_RADIOBUTTON(wxID_ANY, FacesSampleForm::OnRadioButtonCheckedChanged)
END_EVENT_TABLE()

FacesSampleForm::FacesSampleForm(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize& size, long style)
	: wxFrame(parent, id, title, position, size, style),
	m_currentOperations(nboNone), m_checkIcao(false), m_fileIndex(0), m_cancel(false), m_close(false), m_restartCapture(false)
{
	CreateGUIControls();
	EnableControls();

	const wxString Address = wxT("/local");
	const wxString Port = wxT("5000");

	try
	{
		if(NLicense::ObtainComponents(Address, Port, wxT("Biometrics.FaceExtraction"))) AppendText(wxT("License for extractor successfully obtained!\n"));
		else AppendText(wxT("Failed to obtain license for extractor!\n"));

		if(NLicense::ObtainComponents(Address, Port, wxT("Biometrics.FaceMatching"))) AppendText(wxT("License for matcher successfully obtained!\n"));
		else AppendText(wxT("Failed to obtain license for matcher!\n"));

		if(NLicense::ObtainComponents(Address, Port, wxT("Biometrics.FaceMatchingFast"))) AppendText(wxT("License for fast matcher successfully obtained!\n"));
		if(NLicense::ObtainComponents(Address, Port, wxT("Biometrics.Standards.Faces"))) AppendText(wxT("License for biometric standards successfully obtained!\n"));
		if(NLicense::ObtainComponents(Address, Port, wxT("Biometrics.FaceSegmentsDetection"))) AppendText(wxT("License for face segmentation successfully obtained!\n"));
	}
	catch(NError& ex)
	{
		wxString msg = wxString::Format(wxT("Failed to obtain licenses for components. Error message: %s\n"), ((wxString)ex.ToString()).c_str());
		if (ex.GetCode() == N_E_IO)
			msg += wxT("(Probably licensing service is not running. Use Activation Wizard to figure it out.)\n\n");
		AppendText(msg);
	}

	wxConfigBase *config = wxConfigBase::Get();
	wxString savedSource = config->Read(wxT("MainWindow/Source"));
	wxString savedMode = config->Read(wxT("MainWindow/Mode"));
	wxString properties = wxEmptyString;
	config->Read(wxT("BiometricClient/Properties"), &properties, DEFAULT_CLIENT_PROPERTIES);

	try
	{
		wxString dbPath = wxSampleConfig::GetUserDataDir() + wxFileName::GetPathSeparator() + wxT("FacesV5.db");
		m_biometricClient.SetDatabaseConnectionToSQLite(dbPath);
		m_biometricClient.SetCustomDataSchema(NBiographicDataSchema::Parse(wxT("(Thumbnail blob)")));
		if (properties != wxEmptyString)
		{
			NPropertyBag propertyBag = NPropertyBag::Parse(properties);
			propertyBag.ApplyTo(m_biometricClient);
		}

		if (!NLicense::IsComponentActivated(wxT("Biometrics.FaceSegmentation")))
		{
			m_biometricClient.SetFacesDetectAllFeaturePoints(false);
			m_biometricClient.SetFacesDetectBaseFeaturePoints(false);
			m_biometricClient.SetFacesDetermineGender(false);
			m_biometricClient.SetFacesDetermineAge(false);
			m_biometricClient.SetFacesRecognizeEmotion(false);
			m_biometricClient.SetFacesDetectProperties(false);
			m_biometricClient.SetFacesRecognizeExpression(false);
		}

		m_biometricClient.SetMatchingWithDetails(true);
		m_biometricClient.SetUseDeviceManager(true);
		m_biometricClient.SetBiometricTypes(nbtFace);
		m_biometricClient.Initialize();
		m_biometricClient.GetDeviceManager().GetDevices().AddCollectionChangedCallback(&FacesSampleForm::OnDevicesCollectionChanged, this);

		wxCommandEvent empty;
		OnSourcesChanged(empty);
		if (savedSource != wxEmptyString) comboSource->SetValue(savedSource);
		if (comboSource->GetSelection() == -1) comboSource->SetSelection(0);

		wxCommandEvent cmd(wxEVT_COMMAND_COMBOBOX_SELECTED, ID_COMBO_SOURCE);
		if (savedMode != wxEmptyString && comboSource->GetValue() == savedSource)
		cmd.SetString(savedMode);

		wxPostEvent(this, cmd);
	}
	catch (NError& ex)
	{
		AppendText(wxString::Format(wxT("Failed to set up biometric client: %s\n"), ((wxString)ex.ToString()).c_str()));
		mnuCancel->Enable(false);
		mnuEnroll->Enable(false);
		mnuIdentify->Enable(false);
		comboSource->Enable(false);
		comboFormat->Enable(false);
		buttonIdentify->Enable(false);
		buttonEnroll->Enable(false);
		chbCheckForDuplicates->Enable(false);
		chbCheckIcao->Enable(false);
	}
}

FacesSampleForm::~FacesSampleForm()
{
	m_faceView->SetFace(NULL);
	m_icaoView->SetFace(NULL);
	if (!m_biometricClient.IsNull() && !m_biometricClient.GetDeviceManager().IsNull())
	{
		m_biometricClient.GetDeviceManager().GetDevices().RemoveCollectionChangedCallback(&FacesSampleForm::OnDevicesCollectionChanged, this);
		m_biometricClient = NULL;
	}
	ClearMediaFormats();

	NLicense::ReleaseComponents(wxT("Biometrics.FaceExtraction"));
	NLicense::ReleaseComponents(wxT("Biometrics.FaceMatching"));
	NLicense::ReleaseComponents(wxT("Biometrics.FaceMatchingFast"));
	NLicense::ReleaseComponents(wxT("Biometrics.Standards.Faces"));
	NLicense::ReleaseComponents(wxT("Biometrics.FaceSegmentsDetection"));
}

void FacesSampleForm::CreateGUIControls()
{
	CreateToolBar(wxNO_BORDER | wxHORIZONTAL | wxTB_FLAT, ID_TOOLBAR);
	wxToolBar *toolBar = GetToolBar();

	buttonEnroll = new wxButton(toolBar, ID_BUTTON_ENROLL, wxT("&Enroll"));
	buttonEnroll->SetHelpText(wxT("Enroll face image to the database"));
	toolBar->AddControl(buttonEnroll);

	chbCheckForDuplicates = new wxCheckBox(toolBar, wxID_ANY, wxT("Check for duplicates"));
	chbCheckForDuplicates->SetHelpText(wxT("Check for duplicate subject when enrolling to database"));
	toolBar->AddControl(chbCheckForDuplicates);

	chbCheckIcao = new wxCheckBox(toolBar, wxID_ANY, wxT("Check ICAO compliance"));
	toolBar->AddControl(chbCheckIcao);
	toolBar->AddSeparator();

	buttonIdentify = new wxButton(toolBar, ID_BUTTON_IDENTIFY, wxT("&Identify"));
	buttonIdentify->SetHelpText(wxT("Identify face in all database records"));
	toolBar->AddControl(buttonIdentify);

	toolBar->AddSeparator();

	comboSource = new wxComboBox(toolBar, ID_COMBO_SOURCE, wxEmptyString, wxDefaultPosition, wxDefaultSize, 0, 0, wxCB_READONLY);
	comboSource->SetSize(240, wxDefaultCoord);
	toolBar->AddControl(comboSource);

	comboFormat = new wxComboBox(toolBar, ID_COMBO_MODE, wxEmptyString, wxDefaultPosition, wxDefaultSize, 0, 0, wxCB_READONLY);
	toolBar->AddControl(comboFormat);

	CreateStatusBar();
	SetStatusText(wxT("Ready"), 0);

	menuBar = new wxMenuBar();
	wxMenu *ID_MNU_JOBS_Mnu_Obj = new wxMenu();
	mnuEnroll = ID_MNU_JOBS_Mnu_Obj->Append(ID_MNU_ENROLL, wxT("&Enroll\tCtrl-E"), wxT("Enroll face image to the database"), wxITEM_NORMAL);
	mnuIdentify = ID_MNU_JOBS_Mnu_Obj->Append(ID_MNU_IDENTIFY, wxT("&Identify\tCtrl-I"), wxT("Match face image to all database records"), wxITEM_NORMAL);
	ID_MNU_JOBS_Mnu_Obj->AppendSeparator();
	mnuCancel = ID_MNU_JOBS_Mnu_Obj->Append(ID_MNU_CANCEL, wxT("&Cancel\tDel"), wxT("Cancel any current task"), wxITEM_NORMAL);
	ID_MNU_JOBS_Mnu_Obj->AppendSeparator();
#ifdef __WXMAC__
	wxApp::s_macExitMenuItemId = wxID_EXIT;
	ID_MNU_JOBS_Mnu_Obj->Append(wxID_EXIT);
#else
	ID_MNU_JOBS_Mnu_Obj->Append(ID_MNU_EXIT, wxT("E&xit"), wxT("Exit from application"), wxITEM_NORMAL);
#endif
	menuBar->Append(ID_MNU_JOBS_Mnu_Obj, wxT("&Jobs"));

	wxMenu *ID_MNU_TOOLS_Mnu_Obj = new wxMenu();
	ID_MNU_TOOLS_Mnu_Obj->Append(ID_MNU_CLEAR_LOG, wxT("Clear &Log\tCtrl-L"), wxT("Clear log in the lower left window"), wxITEM_NORMAL);
	ID_MNU_TOOLS_Mnu_Obj->Append(ID_MNU_CLEAR_DB, wxT("Clear &Database\tCtrl-D"), wxT("Remove all face records from the database"), wxITEM_NORMAL);

#ifdef __WXMAC__
	wxApp::s_macPreferencesMenuItemId = wxID_PREFERENCES;
	ID_MNU_TOOLS_Mnu_Obj->Append(wxID_PREFERENCES);
#else
	ID_MNU_TOOLS_Mnu_Obj->AppendSeparator();
	ID_MNU_TOOLS_Mnu_Obj->Append(ID_MNU_OPTIONS, wxT("&Options...\tCtrl-O"), wxT("Open options dialog"), wxITEM_NORMAL);
#endif
	menuBar->Append(ID_MNU_TOOLS_Mnu_Obj, wxT("&Tools"));

	wxMenu *ID_MNU_HELP_Mnu_Obj = new wxMenu();
#ifdef __WXMAC__
	wxApp::s_macAboutMenuItemId = wxID_ABOUT;
	ID_MNU_HELP_Mnu_Obj->Append(wxID_ABOUT);
#else
	ID_MNU_HELP_Mnu_Obj->Append(ID_MNU_ABOUT, wxT("&About"), wxT("Open about dialog"), wxITEM_NORMAL);
#endif
	menuBar->Append(ID_MNU_HELP_Mnu_Obj, wxT("&Help"));
	SetMenuBar(menuBar);

	wxBoxSizer* mainSizer = new wxBoxSizer(wxVERTICAL);
	wxBoxSizer* centerSizer = new wxBoxSizer(wxHORIZONTAL);

	m_icaoView = new IcaoWarningsView(this);
	centerSizer->Add(m_icaoView, 0, wxALL|wxEXPAND, 5);
	m_icaoView->Hide();

	wxBoxSizer* centerRightSizer = new wxBoxSizer(wxVERTICAL);

	m_faceView = new wxNFaceView(this);
	centerRightSizer->Add(m_faceView, 1, wxALL|wxEXPAND, 5);

	wxBoxSizer* radioButtonSizer = new wxBoxSizer(wxHORIZONTAL);
	radioButtonSizer->Add(0, 0, 1, wxEXPAND, 5);

	m_radioOriginal = new wxRadioButton(this, wxID_ANY, wxT("Original"), wxDefaultPosition, wxDefaultSize, 0);
	m_radioOriginal->SetValue(true);
	radioButtonSizer->Add(m_radioOriginal, 0, wxALL, 5);

	m_radioSegmented = new wxRadioButton(this, wxID_ANY, wxT("Segmented"), wxDefaultPosition, wxDefaultSize, 0);
	radioButtonSizer->Add(m_radioSegmented, 0, wxALL, 5);

	radioButtonSizer->Add( 0, 0, 1, wxEXPAND, 5 );
	centerRightSizer->Add(radioButtonSizer, 0, wxEXPAND, 5);

	centerSizer->Add(centerRightSizer, 1, wxEXPAND, 5);

	mainSizer->Add(centerSizer, 10, wxEXPAND, 5);

	splitterWindowHor = new wxSplitterWindow(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxSP_3D | wxSP_LIVE_UPDATE);

	richTextCtrlLog = new wxRichTextCtrl(splitterWindowHor, wxID_ANY, wxT(""), wxDefaultPosition, wxDefaultSize, wxVSCROLL | wxHSCROLL | wxWANTS_CHARS);
	richTextCtrlLog->SetEditable(false);

	m_resultsView = new MatchingResultsView(splitterWindowHor);

	splitterWindowHor->SplitVertically(richTextCtrlLog, m_resultsView, 240);
	splitterWindowHor->SetSashGravity(0.5);
	splitterWindowHor->SetMinimumPaneSize(100);

	mainSizer->Add(splitterWindowHor, 4, wxEXPAND, 5);

	SetSizer(mainSizer);
	toolBar->Realize();
	SetTitle(FACES_SAMPLE_WX_TITLE);
	SetIcon(Neurotechnology_XPM);
	SetSize(860, 700);
	Center();
}

bool FacesSampleForm::ProcessEvent(wxEvent& event)
{
	try
	{
		return wxFrame::ProcessEvent(event);
	}
	catch (NError& ex)
	{
		wxExceptionDlg::Show(ex);
		return true;
	}
	catch (std::exception& ex)
	{
		wxExceptionDlg::Show(wxString(ex.what(), wxConvLibc));
		return true;
	}
}

void FacesSampleForm::OnDevicesCollectionChanged(Collections::CollectionChangedEventArgs<NDevice> args)
{
	wxCommandEvent ev(wxEVT_DEVICEPLUG, 0);
	if (args.GetAction() == nccaAdd)
	{
		wxString str = wxEmptyString;
		ev.SetId(ID_DEVICES_ADDED);
		for (int i = 0; i < args.GetNewItems().GetCount(); i++)
		{
			if (i != 0) str = str.Append(wxT(", "));
			str = str.Append(args.GetNewItems()[i].GetDisplayName());
		}
		ev.SetString(str);
	}
	else
	{
		ev.SetId(ID_DEVICES_REMOVED);
		wxString str = wxEmptyString;
		for (int i = 0; i < args.GetOldItems().GetCount(); i++)
		{
			if (i != 0) str = str.Append(wxT(", "));
			str = str.Append(args.GetOldItems()[i].GetDisplayName());
		}
		ev.SetString(str);
	}
	::wxPostEvent(static_cast<FacesSampleForm*>(args.GetParam()), ev);
}

void FacesSampleForm::OnSourcesChanged(wxCommandEvent &event)
{
	wxString selected = comboSource->GetValue();
	wxString name = event.GetString();
	if (name != wxEmptyString)
	{
		if(event.GetInt() == ID_DEVICES_ADDED) AppendText(wxString::Format(wxT("%s plugged\n"), name.c_str()));
		else if (event.GetInt() == ID_DEVICES_REMOVED) AppendText(wxString::Format(wxT("%s unplugged\n"), name.c_str()));
	}

	comboSource->Clear();

	NArrayWrapper<NDevice> devices = m_biometricClient.GetDeviceManager().GetDevices().GetAll();
	for (int i = 0; i < devices.GetCount(); i++)
	{
		comboSource->Append(devices[i].GetDisplayName());
	}
	if (devices.GetCount() > 0)
	{
		comboSource->SetValue(devices[0].GetDisplayName());
	}
	comboSource->Append(wxT("File"));
	comboSource->Append(wxT("Directory"));
	comboSource->SetValue(selected);
	if(comboSource->GetSelection() == -1)
	{
		comboSource->SetValue(wxT("File"));
	}
}

void FacesSampleForm::OnRadioButtonCheckedChanged(wxCommandEvent &)
{
	if (!m_shownSubject.IsNull() && m_shownSubject.GetFaces().GetCount() > 1)
	{
		bool original = m_radioOriginal->GetValue();
		NFace face = original ? m_shownSubject.GetFaces()[0] : m_shownSubject.GetFaces()[1];
		m_faceView->SetFace(face);
	}
}

void FacesSampleForm::OnClose(wxCloseEvent &event)
{
	wxConfigBase *config = wxConfigBase::Get();
	config->Write(wxT("MainWindow/Source"), comboSource->GetValue());
	config->Write(wxT("MainWindow/Mode"), comboFormat->GetValue());

	if (m_biometricClient.GetHandle())
	{
		NPropertyBag properties;
		m_biometricClient.CaptureProperties(properties);
		wxString propertiesString = properties.ToString();
		config->Write(wxT("BiometricClient/Properties"), propertiesString);

		if (!m_asyncOperations.empty())
		{
			m_cancel = true;
			m_close = true;
			CancelAction(true);
			event.Veto();
			return;
		}
	}

	Destroy();
}

void FacesSampleForm::OnCancelAction(wxCommandEvent &/*event*/)
{
	CancelAction(false);
}

void FacesSampleForm::MnuExitClick(wxCommandEvent &/*event*/)
{
	Close(false);
}

void FacesSampleForm::MnuIdentifyClick(wxCommandEvent &/*event*/)
{
	wxString fileName = wxEmptyString;
	m_currentOperations = nboIdentify;
	m_checkIcao = chbCheckIcao->GetValue();
	m_cancel = false;
	if (!IsFromCamera())
	{
		if (GetFileQueue(comboSource->GetValue() == wxT("Directory")))
		{
			AppendText(wxString::Format(wxT("starting identify for %d file(s) ...\n"), (int)m_files.size()));
			while((int)m_asyncOperations.size() < MAX_TASK_COUNT)
			{
				fileName = GetNextFile();
				if (fileName == wxEmptyString) break;
				StartCreateTemplateFromFile(fileName);
			}
		}
		EnableControls();
	}
	else
	{
		bool isIdle = m_asyncOperations.empty();
		if (isIdle)
			StartCreateTemplateFromCamera(false);
		else
		{
			if (m_biometricClient.GetFacesCheckIcaoCompliance() != m_checkIcao)
				RestartCapture();
			else
			{
				m_biometricClient.ForceStart();
				PrepareViews(true, m_checkIcao, false);
			}
		}
		EnableControls();
	}
}

void FacesSampleForm::MnuEnrollClick(wxCommandEvent &/*event*/)
{
	wxString fileName = wxEmptyString;
	m_currentOperations = chbCheckForDuplicates->GetValue() ? nboEnrollWithDuplicateCheck : nboEnroll;
	m_checkIcao = chbCheckIcao->GetValue();
	m_cancel = false;
	if (!IsFromCamera())
	{
		if (GetFileQueue(comboSource->GetValue() == wxT("Directory")))
		{
			AppendText(wxString::Format(wxT("starting enroll for %d file(s) ...\n"), (int)m_files.size()));
			while((int)m_asyncOperations.size() < MAX_TASK_COUNT)
			{
				fileName = GetNextFile();
				if (fileName == wxEmptyString) break;
				StartCreateTemplateFromFile(fileName);
			}
		}
		EnableControls();
	}
	else
	{
		bool isIdle = m_asyncOperations.empty();
		if (isIdle)
			StartCreateTemplateFromCamera(false);
		else
		{
			if (m_biometricClient.GetFacesCheckIcaoCompliance() != m_checkIcao)
				RestartCapture();
			else
			{
				m_biometricClient.ForceStart();
				PrepareViews(true, m_checkIcao, false);
			}
		}
		EnableControls();
	}
}

void FacesSampleForm::MnuOptionsClick(wxCommandEvent &/*event*/)
{
	OptionsDlg optionsDlg(this);
	optionsDlg.SetBiometricClient(m_biometricClient);
	try
	{
		if (optionsDlg.ShowModal() & wxID_OK)
		{
			// Options only applied to new tasks
			if (IsFromCamera())
			{
				if (!m_asyncOperations.empty())
				{
					m_biometricClient.Cancel();
					m_asyncOperations[0].Cancel(true);
				}
				StartCreateTemplateFromCamera();
				m_checkIcao = false;
				EnableControls(true);
			}
		}
	}
	catch(NError& ex)
	{
		wxExceptionDlg::Show(ex);
	}
}

void FacesSampleForm::MnuAboutClick(wxCommandEvent &/*event*/)
{
	wxAboutBox aboutBox(this, -1, FACES_SAMPLE_WX_PRODUCT_NAME, FACES_SAMPLE_WX_VERSION_STRING, FACES_SAMPLE_WX_COPYRIGHT);
	aboutBox.ShowModal();
}

void FacesSampleForm::MnuClearLogClick(wxCommandEvent &/*event*/)
{
	richTextCtrlLog->Clear();
}

void FacesSampleForm::MnuClearDatabaseClick(wxCommandEvent &/*event*/)
{
	CancelAction(true);
	NBiometricTask task = m_biometricClient.CreateTask(nboClear, NULL);
	OnAsyncOperationStarted(m_biometricClient.PerformTaskAsync(task));
}

void FacesSampleForm::OnExtractionSourceSelected(wxCommandEvent &event)
{
	int selectedIndex = comboSource->GetSelection();
	if (selectedIndex != -1)
	{
		m_biometricClient.Cancel(); // Cancel any currently running capture
		if (!IsFromCamera())
		{
			comboFormat->Clear();
			comboFormat->Enable(false);
			ClearMediaFormats();
		}
		else
		{
			NCamera camera = m_biometricClient.GetDeviceManager().GetDevices().Get(selectedIndex).GetHandle();

			if (!m_asyncOperations.empty()) m_asyncOperations[0].Cancel(true);
			comboFormat->Enable(true);
			comboFormat->Clear();
			ClearMediaFormats();

			int currentFormatIndex = -1;
			NArrayWrapper<NMediaFormat> formats = camera.GetFormats();
			NMediaFormat currentFormat = camera.GetCurrentFormat();
			for (int i = 0; i < formats.GetCount(); i++)
			{
				NMediaFormat item = formats[i];
				m_mediaFormats.push_back(item);
				if(item.Equals(currentFormat))
				{
					currentFormatIndex = i;
				}
				comboFormat->Append(item.ToString());
			}
			if (currentFormatIndex == -1 && currentFormat.GetHandle())
			{
				m_mediaFormats.push_back(currentFormat);
				currentFormatIndex = (int)m_mediaFormats.size() - 1;
				currentFormat = NULL;
			}

			m_biometricClient.SetFaceCaptureDevice(camera);
			wxString savedFormat = event.GetString();
			if (savedFormat != wxEmptyString)
			{
				comboFormat->SetValue(savedFormat);
			}
			if (currentFormatIndex != -1 && comboFormat->GetSelection() == -1)
			{
				comboFormat->SetSelection(currentFormatIndex);
			}

			if (comboFormat->GetSelection() != -1)
				camera.SetCurrentFormat(m_mediaFormats[comboFormat->GetSelection()]);
			StartCreateTemplateFromCamera();
			m_checkIcao = false;
			EnableControls(true);
		}
	}
}

void FacesSampleForm::OnMediaFormatSelected(wxCommandEvent &/*event*/)
{
	if (IsFromCamera())
	{
		int index = comboFormat->GetSelection();
		if (!m_asyncOperations.empty())
		{
			m_biometricClient.Cancel();
			m_asyncOperations[0].Cancel(true);
		}
		if (index != -1)
		{
			m_biometricClient.GetFaceCaptureDevice().SetCurrentFormat(m_mediaFormats[index]);
		}
		StartCreateTemplateFromCamera();
	}
}

void FacesSampleForm::OnEnrollCompleted( ::Neurotec::Biometrics::NBiometricTask enrollTask)
{
	bool successful = false;
	NBiometricTask::SubjectCollection subjects = enrollTask.GetSubjects();
	int count = subjects.GetCount();
	for (int i = 0; i < count; i++)
	{
		NSubject subject = subjects.Get(i);
		NBiometricStatus status = subject.GetStatus();
		wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
		wxString id = subject.GetId();
		successful = status == nbsOk;
		AppendTextLine(wxString::Format(wxT("enroll subject '%s' %s, status = %s"), id.c_str(), (successful ? "successful" : "failed"), statusString));
	}
}

wxString FacesSampleForm::GetIcaoWarningsString(const NLAttributes & attributes) const
{
	NIcaoWarnings warnings = attributes.GetIcaoWarnings();
	if (warnings == niwNone)
	{
		return "  ICAO Status: Ok\n";
	}
	else
	{
		wxString result = wxT("  ICAO Warnings: ");
		if ((warnings & niwFaceNotDetected) != 0)
			result = result.Append("Face not detected, ");
		if ((warnings & (niwRollLeft | niwRollRight)) != 0)
			result = result.Append("Roll, ");
		if ((warnings & (niwYawLeft | niwYawRight)) != 0)
			result = result.Append("Yaw, ");
		if ((warnings & (niwPitchUp | niwPitchDown)) != 0)
			result = result.Append("Pitch, ");
		if ((warnings & niwTooNear) != 0)
			result = result.Append("Too Close, ");
		if ((warnings & niwTooFar) != 0)
			result = result.Append("Too Far, ");
		if ((warnings & niwTooSouth) != 0)
			result = result.Append("Too South, ");
		if ((warnings & niwTooNorth) != 0)
			result = result.Append("Too North, ");
		if ((warnings & niwTooEast) != 0)
			result = result.Append("Too East, ");
		if ((warnings & niwTooWest) != 0)
			result = result.Append("Too West, ");
		if ((warnings & niwExpression) != 0)
			result = result.Append("Expression, ");
		if ((warnings & niwDarkGlasses) != 0)
			result = result.Append("Dark Glasses, ");
		if ((warnings & niwBlink) != 0)
			result = result.Append("Blink, ");
		if ((warnings & niwMouthOpen) != 0)
			result = result.Append("Mouth Open, ");
		if ((warnings & niwSharpness) != 0)
			result = result.Append(wxString::Format("Sharpness: %d, ", attributes.GetSharpness()));
		if ((warnings & niwBackgroundUniformity) != 0)
			result = result.Append(wxString::Format("Background Uniformity: %d, ", attributes.GetBackgroundUniformity()));
		if ((warnings & niwGrayscaleDensity) != 0)
			result = result.Append(wxString::Format("Grayscale Density: %d, ", attributes.GetGrayscaleDensity()));
		if ((warnings & niwSaturation) != 0)
			result = result.Append(wxString::Format("Saturation: %d", attributes.GetSaturation()));
		result = result.Append("\n");

		return result;
	}
}

void FacesSampleForm::OnCreateTemplateCompleted( ::Neurotec::Biometrics::NBiometricTask createTempalteTask)
{
	int facesCount;
	wxString id, statusString;
	NBiometricStatus status;
	NSubject subject = NULL;
	NFace face = NULL;
	NBiometricTask subTask = m_biometricClient.CreateTask(m_currentOperations, NULL);
	bool isEnroll = m_currentOperations == nboEnroll || m_currentOperations == nboEnrollWithDuplicateCheck;

	subject = createTempalteTask.GetSubjects().Get(0);
	face = subject.GetFaces()[0];
	m_faceView->SetFace(face);
	m_icaoView->SetFace(face);
	PrepareViews(true, m_checkIcao, true);

	NSubject::RelatedSubjectCollection relatedSubjects = subject.GetRelatedSubjects();
	facesCount = 1 + relatedSubjects.GetCount();

	m_shownSubject = subject;
	m_radioOriginal->SetValue(true);

	status = subject.GetStatus();
	id = subject.GetId();
	if (status == nbsOk && isEnroll)
	{
		NInt count = subject.GetFaces().GetCount();
		face = subject.GetFaces().Get(count - 1);
		NLAttributes attributes = face.GetObjects().Get(0);
		NImage thumbnail = attributes.GetThumbnail();

		if (id == wxEmptyString)
		{
			wxApp::GetInstance()->ProcessPendingEvents();
			EnrollDlg dlg(this, wxEmptyString, thumbnail);
			if (dlg.ShowModal() == wxID_OK)
			{
				id = dlg.GetUserId();
				subject.SetId(id);
			}
			else
			{
				EnableControls();
				StartCreateTemplateFromCamera();
				return;
			}
		}
	}
	else
	{
		EnableControls();
	}

	AppendTextLine(wxString::Format(wxT("detected %d face(s) in '%s':"), facesCount, id.c_str()));
	for (int i = 0; i < facesCount; i++)
	{
		bool successful = false;
		if (i > 0) subject = relatedSubjects.Get(i - 1);
		status = subject.GetStatus();
		statusString = "Liveness check failed";
		if (status != nbsTimeout) statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
		successful = status == nbsOk;
		AppendText(wxString::Format(wxT(" > create template %s, status = %s\n"), (successful ? "successful" : "failed"), statusString.c_str()));
		if (successful)
		{
			if (m_checkIcao && successful)
			{
				face = subject.GetFaces()[0];
				NLAttributes attributes = face.GetObjects()[0];
				AppendText(GetIcaoWarningsString(attributes));
			}

			if (i > 0)
			{
				wxString relatedFaceId = wxString::Format(wxT("%s #%d"), id.c_str(), i + 1);
				subject.SetId(relatedFaceId);
			}

			if (isEnroll)
			{
				NInt count = subject.GetFaces().GetCount();
				NFace face = subject.GetFaces().Get(count - 1);
				NLAttributes attributes = face.GetObjects().Get(0);
				NImage thumbnail = attributes.GetThumbnail();
				if (thumbnail.GetHandle())
				{
					NBuffer buffer = thumbnail.Save(NImageFormat::GetPng());
					subject.SetProperty(wxT("Thumbnail"), buffer);
				}
			}
			subTask.GetSubjects().Add(subject);
		}
	}
	if (subTask.GetSubjects().GetCount() > 0)
		OnAsyncOperationStarted(m_biometricClient.PerformTaskAsync(subTask));
	else if (!m_cancel && status != nbsCanceled && IsFromCamera())
	{
		// Create template from camera failed, restart capture
		EnableControls();
		StartCreateTemplateFromCamera();
	}
}

void FacesSampleForm::OnIdentifyCompleted( ::Neurotec::Biometrics::NBiometricTask identifyTask)
{
	NSubject subject;
	NBiometricStatus status;
	wxString statusString;
	wxString id;

	m_resultsView->Clear();
	for (int i = 0; i < identifyTask.GetSubjects().GetCount(); i++)
	{
		subject = identifyTask.GetSubjects().Get(i);
		if (i == 0)
		{
			NFace face = subject.GetFaces().Get(0);
			m_faceView->SetFace(face);
			m_icaoView->SetFace(face);
			PrepareViews(true, m_checkIcao, true);
		}
		status = subject.GetStatus();
		statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
		id = subject.GetId();
		AppendText(wxString::Format(wxT("identify subject '%s' completed, status = %s\n"), id.c_str(), statusString.c_str()));
		if (status == nbsOk)
		{
			NSubject::MatchingResultCollection results = subject.GetMatchingResults();
			int count = results.GetCount();
			AppendTextLine(wxString::Format(wxT("  %d matching subject(s) found:"), count));
			for (int j = 0; j < count; j++)
			{
				NImage thumbnail(NULL);
				NMatchingResult result = results.Get(j);
				int score = result.GetScore();
				wxString matchedId = result.GetId();
				AppendTextLine(wxString::Format(wxT("  > '%s' (score = %d)"), matchedId.c_str(), score));

				NSubject target;
				target.SetId(matchedId);
				status = m_biometricClient.Get(target);
				if (status != nbsOk)
				{
					statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					AppendTextLine(wxString::Format(wxT("  failed to retrieve subject '%s' from database, status = %s"), matchedId.c_str(), statusString.c_str()));
				}
				else if (target.GetProperties().Contains(wxT("Thumbnail")))
				{
					NBuffer buffer = target.GetProperty<NBuffer>(wxT("Thumbnail"));
					if (buffer.GetHandle())
						thumbnail = NImage::FromMemory(buffer, NImageFormat::GetPng());
				}
				wxBitmap bitmap = !thumbnail.IsNull() ? thumbnail.ToBitmap() : wxBitmap();
				m_resultsView->Add(i + 1, matchedId, score, bitmap);
			}
		}
	}
}

void FacesSampleForm::OnClientActionCompleted(wxCommandEvent &event)
{
	NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
	OnAsyncOperationCompleted(operation);
	m_resultsView->Clear();
	if (!operation.IsCanceled())
	{
		NError error = operation.GetError();
		if (error.GetHandle()) AppendText(wxString::Format(wxT("error occurred: %s\n"), ((wxString)error.ToString()).c_str()));
		else
		{
			NValue result = operation.GetResult();
			NBiometricTask task = result.ToObject(NBiometricTask::NativeTypeOf()).GetHandle();
			error = task.GetError();
			if (error.GetHandle()) AppendText(wxString::Format(wxT("error occurred: %s\n"), ((wxString)error.ToString()).c_str()));
			else
			{
				NBiometricOperations operations = task.GetOperations();
				if ((operations & nboCreateTemplate) != 0)
				{
					OnCreateTemplateCompleted(task);
				}
				else if (operations == nboEnroll || operations == nboEnrollWithDuplicateCheck)
				{
					OnEnrollCompleted(task);
					if (IsFromCamera())
					{
						StartCreateTemplateFromCamera();
						EnableControls(true);
					}
				}
				else if (operations == nboIdentify)
				{
					OnIdentifyCompleted(task);
					if (IsFromCamera())
					{
						StartCreateTemplateFromCamera();
						EnableControls(true);
					}
				}
				else if (operations == nboClear)
				{
					AppendText(wxT("\ndatabase cleared\n"));
					if (IsFromCamera())
					{
						StartCreateTemplateFromCamera();
						EnableControls(true);
					}
				}
			}
		}

		if (!m_cancel)
		{
			wxString fileName = wxEmptyString;
			while((int)m_asyncOperations.size() < MAX_TASK_COUNT)
			{
				fileName = GetNextFile();
				if (fileName == wxEmptyString) break;
				StartCreateTemplateFromFile(fileName);
			}
		}
	}

	if (m_asyncOperations.empty())
	{
		if (m_cancel)
		{
			AppendText(wxT(" done.\n"));
			PrepareViews(false, false, false);
		}
		if (m_restartCapture)
		{
			AppendText(wxT(" done.\n"));
			m_restartCapture = false;
			StartCreateTemplateFromCamera(false);
		}
		EnableControls();
	}

	if (m_close) Close();
}

void FacesSampleForm::AsyncOperationCompletedCallback(EventArgs args)
{
	FacesSampleForm * form = static_cast<FacesSampleForm*>(args.GetParam());
	wxCommandEvent ev(wxEVT_CLIENT_ACTION_COMPLETED);
	ev.SetClientData(args.GetObject<NAsyncOperation>().GetHandle()? args.GetObject<NAsyncOperation>().RefHandle() : NULL);
	wxPostEvent(form, ev);
}

struct NObjectCompare : public std::unary_function<NObject, bool>
{
	NObject target;
	explicit NObjectCompare(NAsyncOperation item) : target(item) { }
	bool operator() (NAsyncOperation arg) { return NObject::Equals(target, arg); }
};

void FacesSampleForm::OnAsyncOperationStarted( ::Neurotec::NAsyncOperation operation)
{
	operation.AddCompletedCallback(&FacesSampleForm::AsyncOperationCompletedCallback, this);
	m_asyncOperations.push_back(operation);
}

void FacesSampleForm::OnAsyncOperationCompleted( ::Neurotec::NAsyncOperation operation)
{
	std::vector<NAsyncOperation>::iterator it = std::find_if(m_asyncOperations.begin(), m_asyncOperations.end(), NObjectCompare(operation));
	if (it != m_asyncOperations.end())
		m_asyncOperations.erase(it);
}

void FacesSampleForm::StartCreateTemplateFromFile(wxString fileName)
{
	NSubject subject;
	m_biometricClient.SetFacesCheckIcaoCompliance(m_checkIcao);
	NBiometricOperations operation = m_checkIcao ? (NBiometricOperations)(nboSegment | nboCreateTemplate): nboCreateTemplate;
	NBiometricTask task = m_biometricClient.CreateTask(operation, subject);
	NFace face;
	face.SetFileName(fileName);
	subject.GetFaces().Add(face);
	subject.SetId(fileName);
	subject.SetMultipleSubjects(!m_checkIcao); // Find all faces if not checking ICAO compliance
	OnAsyncOperationStarted(m_biometricClient.PerformTaskAsync(task));
}

void FacesSampleForm::StartCreateTemplateFromCamera(bool manual)
{
	NSubject subject;
	NFace face;
	face.SetCaptureOptions(manual ? (NBiometricCaptureOptions)(nbcoManual | nbcoStream) : nbcoStream);
	subject.GetFaces().Add(face);
	m_biometricClient.SetFacesCheckIcaoCompliance(m_checkIcao);
	NBiometricOperations operations = m_checkIcao ? (NBiometricOperations)(nboSegment | nboCreateTemplate) : nboCreateTemplate;
	NBiometricTask task = m_biometricClient.CreateTask(operations, subject);
	m_faceView->SetFace(face);
	m_icaoView->SetFace(face);
	m_shownSubject = subject;
	OnAsyncOperationStarted(m_biometricClient.PerformTaskAsync(task));
	PrepareViews(!manual, m_checkIcao, false);
}

bool FacesSampleForm::IsFromCamera()
{
	wxString value = comboSource->GetValue();
	return value != wxT("File") && value != wxT("Directory");
}

void FacesSampleForm::PrepareViews(bool isBusy, bool checkIcao, bool fromFile)
{
	if (!checkIcao)
	{
		m_icaoView->Hide();
		m_faceView->SetShowIcaoArrows(false);
		m_faceView->SetShowAge(true);
		m_faceView->SetShowEmotions(true);
		m_faceView->SetShowExpression(true);
		m_faceView->SetShowGender(true);
		m_faceView->SetShowProperties(true);
	}
	else
	{
		bool hasFace = !m_faceView->GetFace().IsNull();
		if (fromFile)
		{
			m_icaoView->Show(hasFace);
			m_faceView->SetShowIcaoArrows(false);
			m_faceView->SetShowAge(true);
			m_faceView->SetShowEmotions(true);
			m_faceView->SetShowExpression(true);
			m_faceView->SetShowGender(true);
			m_faceView->SetShowProperties(true);
		}
		else
		{
			m_icaoView->Show(isBusy);
			m_faceView->SetShowIcaoArrows(isBusy);
			m_faceView->SetShowAge(!isBusy);
			m_faceView->SetShowEmotions(!isBusy);
			m_faceView->SetShowExpression(!isBusy);
			m_faceView->SetShowGender(!isBusy);
			m_faceView->SetShowProperties(!isBusy);
		}
	}

	Layout();
}

void FacesSampleForm::EnableControls(bool enable)
{
	wxString selected = comboSource->GetValue();
	bool fromCamera = IsFromCamera();
	comboSource->Enable(enable);
	comboFormat->Enable(enable && fromCamera);
	buttonEnroll->Enable(enable);
	buttonIdentify->Enable(enable);
	mnuIdentify->Enable(enable);
	mnuEnroll->Enable(enable);
	chbCheckIcao->Enable(enable);
	chbCheckForDuplicates->Enable(enable);

	bool showImageRadioButtons = enable && !m_shownSubject.IsNull() && m_shownSubject.GetFaces().GetCount() > 1;
	m_radioOriginal->Show(showImageRadioButtons);
	m_radioSegmented->Show(showImageRadioButtons);
	/*if (m_checkIcao)
	{
		m_icaoView->Show();
		m_faceView->SetShowIcaoArrows(!enable);
		m_faceView->SetShowAge(enable);
		m_faceView->SetShowEmotions(enable);
		m_faceView->SetShowExpression(enable);
		m_faceView->SetShowGender(enable);
		m_faceView->SetShowProperties(enable);
	}
	else
	{
		m_icaoView->Hide();
	}*/
	Layout();
}

void FacesSampleForm::EnableControls()
{
	EnableControls(m_asyncOperations.empty());
}

class DirTraverser : public wxDirTraverser
{
	public:
		DirTraverser(wxArrayString *files) { m_files = files; }
		~DirTraverser() { }

		wxDirTraverseResult OnFile(const wxString &filename)
		{
			m_files->Add(filename);
			return wxDIR_CONTINUE;
		}
		wxDirTraverseResult OnDir(const wxString &/*dirname*/)
		{
			return wxDIR_CONTINUE;
		}
	private:
		wxArrayString *m_files;
};

bool FacesSampleForm::GetFileQueue(bool directory)
{
	m_files.Clear();
	m_fileIndex = 0;
	if (directory)
	{
		wxDirDialog dirDialog(this, wxT("Choose a directory with face image(s) to extract"));
		if (dirDialog.ShowModal() == wxID_OK)
		{
			wxDir dir(dirDialog.GetPath());
			if (dir.IsOpened())
			{
				wxWindowDisabler disableAll;

				DirTraverser sink(&m_files);
				wxStringTokenizer tkz(Common::GetOpenFileFilter(), wxT(";"));
				while (tkz.HasMoreTokens())
				{
					dir.Traverse(sink, tkz.GetNextToken());
				}
				// remove duplicate entries
				m_files.Sort();
				size_t j = 0;
				size_t count = m_files.GetCount();
				for (size_t i = 1; i < count; ++i)
				{
					if (m_files[j].Cmp(m_files[i]))
					{
						++j;
						m_files[j] = m_files[i];
					}
				}
				++j;
				if (j < m_files.GetCount())
				{
					m_files.RemoveAt(j, m_files.GetCount() - j);
				}
				return true;
			}
		}
	}
	else
	{
		wxFileDialog openFileDialog(this, wxT("Choose face image(s) to extract"), wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, true), wxFD_OPEN | wxFD_FILE_MUST_EXIST | wxFD_MULTIPLE);
		if (openFileDialog.ShowModal() == wxID_OK)
		{
			openFileDialog.GetPaths(m_files);
			return true;
		}
	}
	return false;
}

void FacesSampleForm::RestartCapture()
{
	m_restartCapture = true;
	AppendText(wxT("Restarting with new parameters ..."));
	m_biometricClient.Cancel();
	for (std::vector<NAsyncOperation>::iterator it = m_asyncOperations.begin(); it != m_asyncOperations.end(); it++)
	{
		it->Cancel();
	}
}

void FacesSampleForm::CancelAction(bool force)
{
	if (!m_asyncOperations.empty())
	{
		wxMessageDialog msg(this, wxT("Do you really want to cancel any action in progress?"), wxT("Confirm cancel"), wxYES_NO);
		if (force || (msg.ShowModal() & wxYES))
		{
			m_cancel = true;
			AppendText(wxT("Cancelling ..."));
			m_biometricClient.Cancel();
			for (std::vector<NAsyncOperation>::iterator it = m_asyncOperations.begin(); it != m_asyncOperations.end(); it++)
			{
				it->Cancel();
			}
		}
	}
}

void FacesSampleForm::AppendTextLine(wxString text)
{
	AppendText(text + wxT("\n"));
}

void FacesSampleForm::AppendText(wxString text)
{
	richTextCtrlLog->AppendText(text);
	long pos = richTextCtrlLog->GetLastPosition();
	richTextCtrlLog->SetCaretPosition(pos);
	richTextCtrlLog->ShowPosition(pos);
	richTextCtrlLog->Update();
}

wxString FacesSampleForm::GetNextFile()
{
	if (m_fileIndex >= (int)m_files.Count()) return wxEmptyString;
	return m_files[m_fileIndex++];
}

void FacesSampleForm::ClearMediaFormats()
{
	m_mediaFormats.clear();
}
}}

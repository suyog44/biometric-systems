#include "Precompiled.h"
#include "FingersSampleForm.h"
#include "EnrollDlg.h"
#include "FingersSampleWXVersionInfo.h"

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NGui/Gui/Neurotechnology.xpm>
#else
	#include <Gui/Neurotechnology.xpm>
#endif

using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;
using namespace Neurotec::Images;
using namespace Neurotec::Media;
using namespace Neurotec::Media::Processing;
using namespace Neurotec::Devices;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Licensing;

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_OPERATION_COMPLETED, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_OPERATION_COMPLETED, wxCommandEvent);

wxDECLARE_EVENT(wxEVT_SCANNERS_COLLECTION_CHANGED, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_SCANNERS_COLLECTION_CHANGED, wxCommandEvent);

BEGIN_EVENT_TABLE(FingersSampleForm, wxFrame)
	EVT_CLOSE(FingersSampleForm::OnClose)
	EVT_MENU(ID_MNU_CANCEL, FingersSampleForm::OnCancelAction)
	EVT_MENU(ID_MNU_CLEAR_LOG, FingersSampleForm::MnuClearLogClick)
	EVT_MENU(ID_MNU_CLEAR_DB, FingersSampleForm::MnuClearDatabaseClick)
	EVT_MENU(ID_MNU_SAVE_IMAGE, FingersSampleForm::OnSaveImage)
#ifdef __WXMAC__
	EVT_MENU(wxID_EXIT, FingersSampleForm::MnuExitClick)
	EVT_MENU(wxID_PREFERENCES, FingersSampleForm::MnuOptionsClick)
	EVT_MENU(wxID_ABOUT, FingersSampleForm::MnuAboutClick)
#else
	EVT_MENU(ID_MNU_EXIT, FingersSampleForm::MnuExitClick)
	EVT_MENU(ID_MNU_OPTIONS, FingersSampleForm::MnuOptionsClick)
	EVT_MENU(ID_MNU_ABOUT, FingersSampleForm::MnuAboutClick)
#endif
	EVT_BUTTON(ID_BUTTON_ENROLL, FingersSampleForm::OnEnroll)
	EVT_BUTTON(ID_BUTTON_IDENTIFY, FingersSampleForm::OnIdentify)
	EVT_BUTTON(ID_BUTTON_VERIFY, FingersSampleForm::OnVerify)

	EVT_BUTTON(ID_BUTTON_SAVE_IMAGE, FingersSampleForm::OnSaveImage)

	EVT_LIST_COL_CLICK(ID_LISTCTRLRESULTS, FingersSampleForm::OnResultColumnClick)
	EVT_LIST_ITEM_SELECTED(ID_LISTCTRLRESULTS, FingersSampleForm::OnResultSelected)

	EVT_TIMER(ID_PROGRESS_PULSER, FingersSampleForm::OnProgressPulse)
END_EVENT_TABLE()

FingersSampleForm::FingersSampleForm(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize& size, long style)
	: wxFrame(parent, id, title, position, size, style),
	m_progressPulser(NULL),
	m_leftRecord(NULL),
	m_rightRecord(NULL),
	m_isClosing(false),
	m_isCancelling(false)
{
	CreateGUIControls();

	const wxString Address = wxT("/local");
	const wxString Port = wxT("5000");

	try
	{
		if(NLicense::ObtainComponents(Address, Port, wxT("Biometrics.FingerExtraction"))) AppendText(wxT("License for extractor successfully obtained!\n"));
		else AppendTextError(wxT("Failed to obtain licenses for extractor!\n"));

		if(NLicense::ObtainComponents(Address, Port, wxT("Biometrics.FingerMatching"))) AppendText(wxT("License for matcher successfully obtained!\n"));
		else AppendTextError(wxT("Failed to obtain license for matcher!\n"));

		if(NLicense::ObtainComponents(Address, Port, wxT("Biometrics.FingerMatchingFast"))) AppendText(wxT("License for fast mathcer successfully obtained!\n"));
		if(NLicense::ObtainComponents(Address, Port, wxT("Biometrics.Standards.Fingers"))) AppendText(wxT("License for successfully biometric standards obtained!\n"));
		if(NLicense::ObtainComponents(Address, Port, wxT("Images.WSQ"))) AppendText(wxT("License for WSQ successfully obtained!\n"));
	}
	catch(NError& ex)
	{
		AppendTextError(wxString::Format(wxT("Failed to obtain licenses for components: %s!\n"), ((wxString)ex.ToString()).c_str()));
	}

	wxString dbPath = wxSampleConfig::GetUserDataDir() + wxFileName::GetPathSeparator() + "FingersV5.db";
	m_biometricClient.SetDatabaseConnectionToSQLite(dbPath);
	m_biometricClient.SetBiometricTypes(nbtFinger);
#ifndef N_PRODUCT_NO_FINGER_SCANNERS
	m_biometricClient.SetUseDeviceManager(true);

	m_deviceManager = m_biometricClient.GetDeviceManager();
	m_deviceManager.GetDevices().AddCollectionChangedCallback(&FingersSampleForm::OnScannersCollectionChangedCallback, this);
#endif

	// Set "Matching.WithDetails" in order for the sample to get full matching details of matched fingerprints. NOTE: it slows down matching!
	m_biometricClient.SetMatchingWithDetails(true);
	m_biometricClient.SetFingersReturnBinarizedImage(true);

	try
	{
		OptionsDlg::LoadOptions(m_biometricClient);
		m_biometricClient.Initialize();
	}
	catch (NError& ex)
	{
		AppendTextError(wxT("Failed to initialize engine\n"));
		wxExceptionDlg::Show(ex);
	}

#ifndef N_PRODUCT_NO_FINGER_SCANNERS
	for (int i = 0; i < m_deviceManager.GetDevices().GetCount(); i++)
	{
		wxString name = m_deviceManager.GetDevices().Get(i).GetDisplayName();
		wxClientData * pData = new wxStringClientData(m_deviceManager.GetDevices().Get(i).GetId());

		m_comboSource->Append(wxString::Format(wxT("From scanner '%s'..."), name), pData);
		AppendText(wxString::Format(wxT("Scanner '%s' connected.\n"), name));
		if (i == 0)
		{
			m_sourceIdx = SOURCE_SCANNER;
			m_comboSource->SetSelection(m_sourceIdx);
		}
	}
#endif
}

FingersSampleForm::~FingersSampleForm()
{
	delete m_progressPulser;
	Clear();

	OptionsDlg::SaveOptions(m_biometricClient);

	NLicense::ReleaseComponents(wxT("Biometrics.FingerExtraction"));
	NLicense::ReleaseComponents(wxT("Biometrics.FingerMatching"));
	NLicense::ReleaseComponents(wxT("Biometrics.FingerMatchingFast"));
	NLicense::ReleaseComponents(wxT("Biometrics.Standards.Fingers"));
	NLicense::ReleaseComponents(wxT("Images.WSQ"));
}

void FingersSampleForm::CreateGUIControls()
{
	// toolbar
	CreateToolBar(wxNO_BORDER | wxHORIZONTAL | wxTB_FLAT, ID_TOOLBAR);
	wxToolBar *toolBar = GetToolBar();

	m_buttonEnroll = new wxButton(toolBar, ID_BUTTON_ENROLL, wxT("&Enroll"));
	m_buttonEnroll->SetHelpText(wxT("Enroll subject(s) to database"));
	toolBar->AddControl(m_buttonEnroll);

	m_chbCheckForDuplicates = new wxCheckBox(toolBar, wxID_ANY, wxT("&Check for duplicates"));
	m_chbCheckForDuplicates->SetHelpText(wxT("Check for duplicate subject (s) when enrolling to database"));
	toolBar->AddControl(m_chbCheckForDuplicates);

	toolBar->AddSeparator();

	m_buttonIdentify = new wxButton(toolBar, ID_BUTTON_IDENTIFY, wxT("&Identify"));
	m_buttonIdentify->SetHelpText(wxT("Identify subject"));
	toolBar->AddControl(m_buttonIdentify);

	m_buttonVerify = new wxButton(toolBar, ID_BUTTON_VERIFY, wxT("&Verify"));
	m_buttonVerify->SetHelpText(wxT("Verify subject"));
	toolBar->AddControl(m_buttonVerify);

	toolBar->AddSeparator();

	m_comboSource = new wxComboBox(toolBar, ID_COMBO_SOURCE, wxEmptyString, wxDefaultPosition, wxDefaultSize, 0, NULL, wxCB_READONLY);
	m_comboSource->SetHelpText(wxT("Sources of image(s) for subject(s)"));
	m_comboSource->Append(wxT("From file..."));
	m_comboSource->Append(wxT("From directory..."));
	m_sourceIdx = SOURCE_FILE;
	m_comboSource->SetSelection(m_sourceIdx);
	toolBar->AddControl(m_comboSource);

	toolBar->AddSeparator();

	m_buttonSaveImage = new wxButton(toolBar, ID_BUTTON_SAVE_IMAGE, wxT("&Save image..."));
	m_buttonSaveImage->SetHelpText(wxT("Save shown image to file"));
	toolBar->AddControl(m_buttonSaveImage);

	toolBar->Realize();

	// statusbar
	CreateStatusBar();
	SetStatusText(wxT("Ready"), 0);
	wxStatusBar *statusBar = GetStatusBar();
	m_progressGauge = new wxGauge(statusBar, wxID_ANY, 0, wxDefaultPosition, statusBar->GetSize());
	m_progressGauge->SetValue(0);
	m_progressGauge->Hide();
	m_progressPulser = new wxTimer(this, ID_PROGRESS_PULSER);

	// menubar
	wxMenuBar *menuBar = new wxMenuBar();

	wxMenu *menu = new wxMenu();
	menu->Append(ID_MNU_SAVE_IMAGE, wxT("&Save Image\tCtrl-s"), wxT("Save shown image to file"), wxITEM_NORMAL);
	menu->AppendSeparator();
	menu->Append(ID_MNU_CANCEL, wxT("&Cancel\tDel"), wxT("Cancel any action in progress"), wxITEM_NORMAL);
	menu->AppendSeparator();

#ifdef __WXMAC__
	wxApp::s_macExitMenuItemId = wxID_EXIT;
	menu->Append(wxID_EXIT);
#else
	menu->Append(ID_MNU_EXIT, wxT("E&xit"), wxT("Exit from application"), wxITEM_NORMAL);
#endif
	menuBar->Append(menu, wxT("&File"));

	menu = new wxMenu();
	menu->Append(ID_MNU_CLEAR_LOG, wxT("Clear &Log\tCtrl-l"), wxT("Clear log in the lower left window"), wxITEM_NORMAL);
	menu->Append(ID_MNU_CLEAR_DB, wxT("Clear &Database\tCtrl-r"), wxT("Remove all finger records from the database"), wxITEM_NORMAL);

#ifdef __WXMAC__
	wxApp::s_macPreferencesMenuItemId = wxID_PREFERENCES;
	menu->Append(wxID_PREFERENCES);
#else
	menu->AppendSeparator();
	menu->Append(ID_MNU_OPTIONS, wxT("&Options...\tCtrl-o"), wxT("Open options dialog"), wxITEM_NORMAL);
#endif
	menuBar->Append(menu, wxT("&Tools"));

	menu = new wxMenu();
#ifdef __WXMAC__
	wxApp::s_macAboutMenuItemId = wxID_ABOUT;
	menu->Append(wxID_ABOUT);
#else
	menu->Append(ID_MNU_ABOUT, wxT("&About"), wxT("Open about dialog"), wxITEM_NORMAL);
#endif
	menuBar->Append(menu, wxT("&Help"));
	SetMenuBar(menuBar);

	wxSplitterWindow *splitterMain = new wxSplitterWindow(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxSP_3D | wxSP_LIVE_UPDATE);

	wxSplitterWindow *splitterTop = new wxSplitterWindow(splitterMain, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxSP_3D | wxSP_LIVE_UPDATE);
	wxSplitterWindow *splitterBottom = new wxSplitterWindow(splitterMain, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxSP_3D | wxSP_LIVE_UPDATE);
	splitterMain->SplitHorizontally(splitterTop, splitterBottom, 490);
	splitterMain->GetWindow1()->SetMinSize(wxSize(100, 100));
	splitterMain->GetWindow2()->SetMinSize(wxSize(100, 200));
	splitterMain->SetMinimumPaneSize(100);
	splitterMain->SetSashGravity(1.0);

	m_fingerViewLeft = new wxNFingerView(splitterTop, ID_FINGER_VIEW_LEFT);
	m_fingerViewRight = new wxNFingerView(splitterTop, ID_FINGER_VIEW_RIGHT);
	splitterTop->SplitVertically(m_fingerViewLeft, m_fingerViewRight, -365);
	splitterTop->GetWindow1()->SetMinSize(wxSize(200, 200));
	splitterTop->GetWindow2()->SetMinSize(wxSize(200, 200));
	splitterTop->SetMinimumPaneSize(100);
	splitterTop->SetSashGravity(0.5);

	m_richTextCtrlLog = new wxRichTextCtrl(splitterBottom, wxID_ANY, wxT(""), wxDefaultPosition, wxDefaultSize, wxVSCROLL | wxHSCROLL | wxWANTS_CHARS);
	m_richTextCtrlLog->SetEditable(false);

	m_resultsList = new wxListCtrl(splitterBottom, ID_LISTCTRLRESULTS, wxDefaultPosition, wxDefaultSize, wxLC_REPORT | wxLC_HRULES | wxLC_SORT_DESCENDING | wxLC_SINGLE_SEL);
	m_resultsList->InsertColumn(0, wxT("Score"), wxLIST_FORMAT_LEFT, 60);
	m_resultsList->InsertColumn(1, wxT("ID"));
	m_resultsList->SetColumnWidth(1, 300);
	m_sortResultsDescending = true;

	splitterBottom->SplitVertically(m_richTextCtrlLog, m_resultsList, -365);
	splitterBottom->GetWindow1()->SetMinSize(wxSize(200, 200));
	splitterBottom->GetWindow2()->SetMinSize(wxSize(200, 200));
	splitterBottom->SetMinimumPaneSize(100);
	splitterBottom->SetSashGravity(0.5);

	SetTitle(FINGERS_SAMPLE_WX_TITLE);
	SetIcon(Neurotechnology_XPM);
	SetSize(700, 700);
	Center();

	this->Bind(wxEVT_SCANNERS_COLLECTION_CHANGED, &FingersSampleForm::OnScannersCollectionChanged, this);
	this->Bind(wxEVT_OPERATION_COMPLETED, &FingersSampleForm::OnOperationCompleted, this);
	this->Bind(wxEVT_MINUTIA_SELECTION_CHANGED, &FingersSampleForm::OnMinutiaSelectionChanged, this);
}

void FingersSampleForm::OnClose(wxCloseEvent &/*event*/)
{
	if (m_pendingOperations.empty())
		Destroy();

	m_isClosing = true;
	CancelPendingOperations();
}

void FingersSampleForm::OnProgressPulse(wxTimerEvent &/*event*/)
{
	m_progressGauge->Pulse();
}

void FingersSampleForm::OnOperationsDone()
{
	if (m_isCancelling)
	{
		m_isCancelling = false;
		AppendText(wxT("Operations is cancelled\n"));
	}

	if (m_progressGauge->IsShown())
	{
		m_progressPulser->Stop();
		m_progressGauge->Hide();
	}

	if (m_isClosing) Destroy();
}

void FingersSampleForm::AddPendingOperation(NAsyncOperation operation)
{
	if (operation.GetHandle())
	{
		operation.AddCompletedCallback(&FingersSampleForm::OnOperationCompletedCallback, this);

		m_pendingOperations.push_back(operation);

		if (!m_progressGauge->IsShown())
		{
			m_progressPulser->Start(50);
			m_progressGauge->Show();
		}
	}
}

void FingersSampleForm::RemovePendingOperation(NAsyncOperation operation)
{
	if (operation.GetHandle())
	{
		for (unsigned int i = 0; i < m_pendingOperations.size(); i++)
		{
			if (m_pendingOperations.at(i).Equals(operation))
			{
				m_pendingOperations.erase(m_pendingOperations.begin() + i);
				break;
			}
		}

		if (m_progressGauge->IsShown())
			m_progressGauge->Pulse();

		if (m_pendingOperations.empty())
			OnOperationsDone();
	}
}

void FingersSampleForm::CancelPendingOperations()
{
	if (m_pendingOperations.size() > 0)
	{
		m_isCancelling = true;
		m_biometricClient.Cancel();
		for (unsigned int i = 0; i < m_pendingOperations.size(); i++)
		{
			try
			{
				m_pendingOperations.at(i).Cancel();
			} catch(NError& ex)
			{
				wxString exceptionText = ex.ToString();
				AppendText(exceptionText);
			}
		}
	}
}

void FingersSampleForm::ProcessMatchingResults(::Neurotec::Biometrics::NSubject subject)
{
	for (int i = 0; i < subject.GetMatchingResults().GetCount(); i++)
	{
		MatchingResultsAdd(subject.GetMatchingResults().Get(i));
	}
}

bool FingersSampleForm::ProcessEvent(wxEvent& event)
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

void FingersSampleForm::Clear()
{
	CancelPendingOperations();

	wxApp::GetInstance()->ProcessPendingEvents();

	m_fingerViewLeft->Clear();
	m_fingerViewRight->Clear();
	MatchingResultsClear();

	m_matedMinutiae.clear();
}

void FingersSampleForm::BeforeProcessImage()
{
	int currentSourceIdx = m_comboSource->GetSelection();
	if (m_sourceIdx == currentSourceIdx)
		return;

	m_sourceIdx = currentSourceIdx;

	if (m_sourceIdx >= SOURCE_SCANNER)
	{
		wxString id = static_cast<wxStringClientData *>(m_comboSource->GetClientObject(m_sourceIdx))->GetData();

		if (m_deviceManager.GetDevices().Get(id).GetHandle())
			m_biometricClient.SetFingerScanner(m_deviceManager.GetDevices().Get(id).GetHandle());
	}

	Clear();
}

void FingersSampleForm::AppendText(wxString text, const wxColour &color)
{
	m_richTextCtrlLog->BeginTextColour(color);
	m_richTextCtrlLog->AppendText(text);
	m_richTextCtrlLog->EndTextColour();
	long pos = m_richTextCtrlLog->GetLastPosition();
	m_richTextCtrlLog->SetCaretPosition(pos);
	m_richTextCtrlLog->ShowPosition(pos);
	m_richTextCtrlLog->Update();
}

void FingersSampleForm::AppendTextError(wxString text)
{
	AppendText(text, wxColor(255, 0, 0));
}

class DirTraverser : public wxDirTraverser
{
	public:
		DirTraverser(wxProgressDialog *progressDlg, wxArrayString *files)
		{
			m_progressDlg = progressDlg;
			m_files = files;
		}

		~DirTraverser() { }

		wxDirTraverseResult OnFile(const wxString& filename)
		{
			m_files->Add(filename);
			if (m_progressDlg->Pulse(filename))
			{
				return wxDIR_CONTINUE;
			}
			else
			{
				return wxDIR_STOP;
			}
		}

		wxDirTraverseResult OnDir(const wxString& dirname)
		{
			if (m_progressDlg->Pulse(dirname))
			{
				return wxDIR_CONTINUE;
			}
			else
			{
				return wxDIR_STOP;
			}
		}
	private:
		wxProgressDialog *m_progressDlg;
		wxArrayString *m_files;
};

void FingersSampleForm::GetFileList(wxArrayString & fileList)
{
	if (m_sourceIdx == SOURCE_FILE)
	{
		wxFileDialog openFileDialog(this, wxT("Choose fingers image(s)"), wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, true), wxFD_OPEN | wxFD_FILE_MUST_EXIST | wxFD_MULTIPLE);
		if (openFileDialog.ShowModal() == wxID_OK)
			openFileDialog.GetPaths(fileList);

		return;
	}

	wxDirDialog dirDialog(this, wxT("Choose a directory with finger image(s)"));
	if (dirDialog.ShowModal() == wxID_OK)
	{
		wxProgressDialog progressDlg(wxT("Retrieving images"),
			wxT("Please wait, retrieving list of images..."), 100, this,
			wxPD_APP_MODAL | wxPD_CAN_ABORT);

		wxDir dir(dirDialog.GetPath());
		if (dir.IsOpened())
		{
			DirTraverser sink(&progressDlg, &fileList);
			wxStringTokenizer tkz(Common::GetOpenFileFilter(), wxT(";"));
			while (tkz.HasMoreTokens())
			{
				dir.Traverse(sink, tkz.GetNextToken());
				wxApp::GetInstance()->Yield();

				if (!progressDlg.Pulse())
				{
					return;
				}
			}
			// remove duplicate entries
			fileList.Sort();
			wxApp::GetInstance()->Yield();
			size_t j = 0;
			size_t count = fileList.GetCount();
			for (size_t i = 1; i < count; ++i)
			{
				if (fileList[j].Cmp(fileList[i]))
				{
					++j;
					fileList[j] = fileList[i];
				}
			}
			++j;
			if (j < fileList.GetCount())
			{
				fileList.RemoveAt(j, fileList.GetCount() - j);
			}
			wxApp::GetInstance()->Yield();
		}
	}
}

void FingersSampleForm::OnEnroll(wxCommandEvent&)
{
	BeforeProcessImage();

	MatchingResultsClear();

	NBiometricOperations operation = m_chbCheckForDuplicates->GetValue() ? nboEnrollWithDuplicateCheck : nboEnroll;
	switch(m_sourceIdx)
	{
	case SOURCE_FILE:
	case SOURCE_DIRECTORY:
		{
			wxArrayString fileList;
			GetFileList(fileList);

			if (fileList.IsEmpty())
				return;

			for (unsigned int i = 0; i < fileList.GetCount(); i++)
			{
				wxFileName filename(fileList[i]);

				NBiometricTask task = m_biometricClient.CreateTask(operation, NULL);

				NFinger finger;

				if (fileList.GetCount() == 1)
					m_fingerViewLeft->SetFinger(finger);

				finger.SetFileName(fileList[i]);
				finger.SetPosition(nfpUnknown);
				finger.SetImpressionType(nfitNonliveScanPlain);

				NSubject subject;

				subject.SetId(filename.GetName());
				subject.GetFingers().Add(finger);

				task.GetSubjects().Add(subject);

				AddPendingOperation(m_biometricClient.PerformTaskAsync(task));
			}
		}
		return;
	case SOURCE_SCANNER:
	default:
		{
			EnrollDlg enrollDlg(this, wxBitmap());
			wxString userId;

			if (enrollDlg.ShowModal() == wxID_OK)
				userId = enrollDlg.GetUserId();
			else
				return;

			NBiometricTask task = m_biometricClient.CreateTask(operation, NULL);

			NFinger finger;

			m_fingerViewLeft->SetFinger(finger);

			finger.SetPosition(nfpUnknown);

			NSubject subject;

			subject.SetId(userId);
			subject.GetFingers().Add(finger);

			task.GetSubjects().Add(subject);

			AddPendingOperation(m_biometricClient.PerformTaskAsync(task));
		}
		return;
	}
}

void FingersSampleForm::OnIdentify(wxCommandEvent &/*event*/)
{
	BeforeProcessImage();

	MatchingResultsClear();

	switch(m_sourceIdx)
	{
	case SOURCE_DIRECTORY:
		m_comboSource->SetSelection(SOURCE_FILE);
	case SOURCE_FILE:
		{
			wxFileDialog openFileDialog(this, wxT("Choose fingers image"), wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, true), wxFD_OPEN | wxFD_FILE_MUST_EXIST);
			if (openFileDialog.ShowModal() != wxID_OK)
				return;

			wxString file = openFileDialog.GetPath();

			if (file.IsEmpty())
				return;

			NBiometricTask task = m_biometricClient.CreateTask(nboIdentify, NULL);

			NFinger finger;

			m_fingerViewLeft->SetFinger(finger);

			finger.SetFileName(file);
			finger.SetPosition(nfpUnknown);
			finger.SetImpressionType(nfitNonliveScanPlain);

			NSubject subject;

			subject.GetFingers().Add(finger);

			subject.SetId(file);

			task.GetSubjects().Add(subject);

			AddPendingOperation(m_biometricClient.PerformTaskAsync(task));
		}
		return;
	case SOURCE_SCANNER:
	default:
		{
			NBiometricTask task = m_biometricClient.CreateTask(nboIdentify, NULL);

			NFinger finger;

			m_fingerViewLeft->SetFinger(finger);

			finger.SetPosition(nfpUnknown);

			NSubject subject;

			subject.GetFingers().Add(finger);

			task.GetSubjects().Add(subject);

			AddPendingOperation(m_biometricClient.PerformTaskAsync(task));
		}
		return;
	}
}

void FingersSampleForm::OnVerify(wxCommandEvent &/*event*/)
{
	BeforeProcessImage();

	MatchingResultsClear();

	switch(m_sourceIdx)
	{
	case SOURCE_DIRECTORY:
		m_comboSource->SetSelection(SOURCE_FILE);
	case SOURCE_FILE:
		{
			wxFileDialog openFileDialog(this, wxT("Choose fingers image"), wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, true), wxFD_OPEN | wxFD_FILE_MUST_EXIST);
			if (openFileDialog.ShowModal() != wxID_OK)
				return;

			wxString file = openFileDialog.GetPath();

			if (file.IsEmpty())
				return;

			wxFileName filename(file);

			NBiometricTask task = m_biometricClient.CreateTask(nboVerify, NULL);

			NFinger finger;

			m_fingerViewLeft->SetFinger(finger);

			finger.SetFileName(file);
			finger.SetPosition(nfpUnknown);
			finger.SetImpressionType(nfitNonliveScanPlain);

			NSubject subject;

			subject.GetFingers().Add(finger);

			subject.SetId(filename.GetName());

			task.GetSubjects().Add(subject);

			AddPendingOperation(m_biometricClient.PerformTaskAsync(task));
		}
		return;
	case SOURCE_SCANNER:
	default:
		{
			EnrollDlg enrollDlg(this, wxBitmap());
			wxString userId;

			if (enrollDlg.ShowModal() == wxID_OK)
				userId = enrollDlg.GetUserId();
			else
				return;

			NBiometricTask task = m_biometricClient.CreateTask(nboVerify, NULL);

			NFinger finger;

			m_fingerViewLeft->SetFinger(finger);

			finger.SetPosition(nfpUnknown);

			NSubject subject;

			subject.SetId(userId);
			subject.GetFingers().Add(finger);

			task.GetSubjects().Add(subject);

			AddPendingOperation(m_biometricClient.PerformTaskAsync(task));
		}
		return;
	}
}

void FingersSampleForm::OnCancelAction(wxCommandEvent&)
{
	wxMessageDialog msg(this, wxT("Do you really want to cancel any action in progress?"), wxT("Confirm cancel"),
		wxYES_NO);
	if (msg.ShowModal() == wxID_YES)
	{
		CancelPendingOperations();
	}
}

void FingersSampleForm::MnuExitClick(wxCommandEvent&)
{
	Close(true);
}

void FingersSampleForm::MnuOptionsClick(wxCommandEvent&)
{
	OptionsDlg optionsDlg(this, m_biometricClient);
	optionsDlg.ShowModal();
}

void FingersSampleForm::MnuAboutClick(wxCommandEvent&)
{
	wxAboutBox aboutBox(this, -1, FINGERS_SAMPLE_WX_PRODUCT_NAME, FINGERS_SAMPLE_WX_VERSION_STRING, FINGERS_SAMPLE_WX_COPYRIGHT);
	aboutBox.ShowModal();
}

void FingersSampleForm::MnuClearLogClick(wxCommandEvent&)
{
	m_richTextCtrlLog->Clear();
}

void FingersSampleForm::MnuClearDatabaseClick(wxCommandEvent&)
{
	wxMessageDialog msg(this, wxT("Do you really want to clear the database?"), wxT("Confirm cancel"),
		wxYES_NO);
	if (msg.ShowModal() == wxID_YES)
	{
		CancelPendingOperations();

		m_biometricClient.Clear();
		AppendText("Database cleared!");
	}
}

int wxCALLBACK FingersSampleForm::ResultsCompareFunction(wxIntPtr item1, wxIntPtr item2, wxIntPtr sortData)
{
	NMatchingResult res1((HNObject)(item1));
	NMatchingResult res2((HNObject)(item2));
	if (!sortData)
	{
		return res1.GetScore() - res2.GetScore();
	}
	else
	{
		return res2.GetScore() - res1.GetScore();
	}
}

void FingersSampleForm::OnResultColumnClick(wxListEvent &event)
{
	if (event.GetColumn() != 0)
	{
		return;
	}

	m_sortResultsDescending = !m_sortResultsDescending;
	m_resultsList->SortItems(ResultsCompareFunction, m_sortResultsDescending);
}

void FingersSampleForm::OnResultSelected(wxListEvent&)
{
	int selected = m_resultsList->GetNextItem(-1, wxLIST_NEXT_ALL, wxLIST_STATE_SELECTED);
	NMatchingDetails matchingDetails = NULL;
	NSubject subject;

	if (selected != -1)
	{
		subject.SetId(m_resultsList->GetItemText(selected, 1));

		if (m_biometricClient.Get(subject) != nbsOk)
		{
			NError ex = subject.GetError();
			wxString errorMessage = !ex.IsNull() ? ex.GetMessage() : NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), subject.GetStatus());
			AppendText(wxString::Format(wxT("Can't get subject '%s' from. (Error: %s)\n"),
				((wxString)subject.GetId()).c_str(),
				errorMessage.c_str()));
			return;
		}

		matchingDetails = NMatchingDetails((HNObject)m_resultsList->GetItemData(selected));
	}

	NFinger leftFinger = m_fingerViewLeft->GetFinger();
	NFinger finger = subject.GetFingers().Get(0);

	if (!finger.IsNull())
		m_fingerViewRight->SetFinger(finger);

	if (!leftFinger.IsNull() && !matchingDetails.IsNull())
	{
		m_matedMinutiae = wxNFingerView::GetMatedMinutiae(matchingDetails);

		SpanningTree spanningTree = wxNFingerView::CalculateSpanningTree(leftFinger, m_matedMinutiae);
		m_fingerViewLeft->SetSpanningTree(spanningTree);
		m_fingerViewRight->SetSpanningTree(wxNFingerView::InverseSpanningTree(leftFinger, matchingDetails, spanningTree));
	}
}

void FingersSampleForm::MatchingResultsAdd(::Neurotec::Biometrics::NMatchingResult result)
{
	long index = m_resultsList->InsertItem(m_resultsList->GetItemCount(), wxString::Format(wxT("%d"), result.GetScore()));
	m_resultsList->SetItem(index, 1, (wxString)result.GetId());
	m_resultsList->SetItemPtrData(index, (wxUIntPtr)(result.GetMatchingDetails().IsNull()? NULL : result.GetMatchingDetails().RefHandle()));
}

void FingersSampleForm::MatchingResultsClear()
{
	int itemCount = m_resultsList->GetItemCount();
	for (int i = 0; i < itemCount; i++)
	{
		NMatchingDetails matchingDetails((HNObject)m_resultsList->GetItemData(i), true);
	}

	m_resultsList->DeleteAllItems();
	m_fingerViewRight->Clear();
}

void FingersSampleForm::OnMinutiaSelectionChanged(wxCommandEvent& event)
{
	int fromIndex = event.GetInt();
	int toIndex = -1;
	switch (event.GetId())
	{
		case ID_FINGER_VIEW_LEFT:
			for (std::list<NIndexPair>::iterator it = m_matedMinutiae.begin(); it != m_matedMinutiae.end(); it++)
			{
				if (it->Index1 == fromIndex)
				{
					toIndex = it->Index2;
					break;
				}
			}
			m_fingerViewRight->SetHighlightedMinutia(toIndex);
			break;

		case ID_FINGER_VIEW_RIGHT:
			for (std::list<NIndexPair>::iterator it = m_matedMinutiae.begin(); it != m_matedMinutiae.end(); it++)
			{
				if (it->Index2 == fromIndex)
				{
					toIndex = it->Index1;
					break;
				}
			}
			m_fingerViewLeft->SetHighlightedMinutia(toIndex);
			break;
	}
}

void FingersSampleForm::OnSaveImage(wxCommandEvent&)
{
	NFinger finger = m_fingerViewLeft->GetFinger();
	NImage image(NULL);

	if (finger.GetHandle())
	{
		image = m_fingerViewLeft->GetShownImage() == wxNFingerView::ORIGINAL_IMAGE ? finger.GetImage() : finger.GetBinarizedImage();
	}

	if(image.GetHandle())
	{
		wxFileDialog dialog(this, wxT("Save image"), wxEmptyString, wxEmptyString, Common::GetSaveFileFilterString(), wxFD_SAVE);
		if(dialog.ShowModal() == wxID_OK)
		{
			try
			{
				image.Save(dialog.GetPath());
			}
			catch(NError& ex)
			{
				wxExceptionDlg::Show(ex);
			}
		}
	}
}

void FingersSampleForm::OnOperationCompleted(wxCommandEvent &event)
{
	NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);

	try
	{
		NValue result = operation.GetResult();

		RemovePendingOperation(operation);

		if (!result.GetHandle())
		{
			return;
		}

		NBiometricTask task = result.ToObject<NBiometricTask>();
		NSubject subject = task.GetSubjects().Get(0);
	#ifdef DEBUG
		std::cout <<"DEBUG: Operation: " << NEnum::ToString(NBiometricTask::NBiometricOperationsNativeTypeOf(), task.GetOperations())
			<< ", status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), subject.GetStatus())
			<< std::endl;
	#endif

		NBiometricOperations op = task.GetOperations();
		switch(op)
		{
		case nboEnrollWithDuplicateCheck:
		case nboEnroll:
			if (subject.GetStatus() == nbsOk)
			{
				AppendText(wxString::Format(wxT("Subject '%s' is enrolled!\n"), ((wxString)subject.GetId()).c_str()));
			}
			else
			{
				wxString message = wxString::Format(wxT("Enrollment of subject '%s' failed!"), (wxString)subject.GetId());
				switch(subject.GetStatus())
				{
				case nbsDuplicateId:
					AppendText(wxString::Format(wxT("%s (Reason: Subject with same id already exist in database)\n"), message));
					break;
				case nbsBadObject:
					AppendText(wxString::Format(wxT("%s (Reason: These is no fingerprint in image or bad quality)\n"), message));
					break;
				default:
					NError ex = subject.GetError();
					wxString errorMessage = ex.GetHandle() ? ex.GetMessage() : NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), subject.GetStatus());
					AppendText(wxString::Format(wxT("Enrollment of subject '%s' failed. (Error: %s)\n"),
								((wxString)subject.GetId()).c_str(),
								errorMessage.c_str()));
					break;
				}
			}
			break;
		case nboIdentify:
			switch(subject.GetStatus())
			{
			case nbsOk:
				AppendText(wxT("Subject is identified!\n"));
				ProcessMatchingResults(subject);
				break;
			case nbsMatchNotFound:
				AppendText(wxT("Subject is not identified!\n"));
				break;
			default:
				NError ex = subject.GetError();
				wxString errorMessage = ex.GetHandle() ? ex.GetMessage() : NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), subject.GetStatus());
				AppendText(wxString::Format(wxT("Identification failed. (Error: %s)\n"), errorMessage.c_str()));
				break;
			}
			break;
		case nboVerify:
			switch(subject.GetStatus())
			{
			case nbsOk:
				AppendText(wxString::Format(wxT("Subject '%s' is verified!\n"), ((wxString)subject.GetId()).c_str()));
				ProcessMatchingResults(subject);
				m_resultsList->SetItemState(0, wxLIST_STATE_SELECTED, wxLIST_STATE_SELECTED);
				break;
			case nbsMatchNotFound:
				AppendText(wxString::Format(wxT("Subject '%s' is not verified!\n"), ((wxString)subject.GetId()).c_str()));
				break;
			case nbsIdNotFound:
				AppendText(wxString::Format(wxT("Subject '%s' not found!\n"), ((wxString)subject.GetId()).c_str()));
				break;
			default:
				NError ex = subject.GetError();
				wxString errorMessage = ex.GetHandle() ? ex.GetMessage() : NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), subject.GetStatus());
				AppendText(wxString::Format(wxT("Verification of subject '%s' failed. (Error: %s)\n"),
					((wxString)subject.GetId()).c_str(), errorMessage.c_str()));
				break;
			}
			break;
		default:
			break;
		}
	}
	catch (NError& ex)
	{
		if (!operation.IsCanceled())
			wxExceptionDlg::Show(ex);

		RemovePendingOperation(operation);

		return;
	}
}

void FingersSampleForm::OnOperationCompletedCallback(EventArgs args)
{
	wxCommandEvent evtResult(wxEVT_OPERATION_COMPLETED);
	wxEvtHandler * pEvtHandler = (wxEvtHandler *)args.GetParam();
	evtResult.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
	wxPostEvent(pEvtHandler, evtResult);
}

void FingersSampleForm::OnScannersCollectionChanged(wxCommandEvent &event)
{
	switch(event.GetId())
	{
	case ID_SCANNER_ADDED:
		AppendText(wxString::Format(wxT("Scanner '%s' connected.\n"), event.GetString()));
		m_comboSource->Append(wxString::Format(wxT("From scanner '%s'..."), event.GetString()), event.GetClientObject());
		if (m_comboSource->GetCount() == SOURCE_SCANNER+1)
			m_comboSource->SetSelection(SOURCE_SCANNER);
		return;
	case ID_SCANNER_REMOVED:
		AppendText(wxString::Format(wxT("Scanner '%s' disconnected.\n"), event.GetString()));
		{
			wxString removeSourceStr = wxString::Format(wxT("From scanner '%s'..."), event.GetString());

			int idx = m_comboSource->FindString(removeSourceStr, true);

			if (idx == wxNOT_FOUND) return;

			wxString selectedSourceStr = m_comboSource->GetStringSelection();
			m_comboSource->Delete(idx);

			if (selectedSourceStr == removeSourceStr)
			{
				if (m_comboSource->GetCount() > SOURCE_SCANNER)
					m_comboSource->SetSelection(SOURCE_SCANNER);
				else
					m_comboSource->SetSelection(SOURCE_FILE);
			}
			else
				m_comboSource->SetStringSelection(selectedSourceStr);
		}
		return;
	default:
		return;
	}
}

void FingersSampleForm::OnScannersCollectionChangedCallback(Collections::CollectionChangedEventArgs<NDevice> args)
{
	wxEvtHandler * pEvtHandler = (wxEvtHandler *)args.GetParam();

	switch(args.GetAction())
	{
	case ::Neurotec::Collections::nccaAdd:
		for (int i = 0; i < args.GetNewItems().GetCount(); i++)
		{
			wxString name = args.GetNewItems()[i].GetDisplayName();
			wxClientData * pData = new wxStringClientData(args.GetNewItems()[i].GetId());

			wxCommandEvent evt(wxEVT_SCANNERS_COLLECTION_CHANGED, ID_SCANNER_ADDED);
			evt.SetString(name);
			evt.SetClientObject(pData);
			wxPostEvent(pEvtHandler, evt);
		}
		return;
	case ::Neurotec::Collections::nccaRemove:
		for (int i = 0; i < args.GetOldItems().GetCount(); i++)
		{
			wxString name = args.GetOldItems()[i].GetDisplayName();
			wxCommandEvent evt(wxEVT_SCANNERS_COLLECTION_CHANGED, ID_SCANNER_REMOVED);
			evt.SetString(name);
			wxPostEvent(pEvtHandler, evt);
		}
		return;
	default:
		return;
	}
}

}}

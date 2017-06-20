#include "Precompiled.h"

#include <SubjectEditor/CaptureVoicePage.h>
#include <SubjectEditor/EditPhrasesDialog.h>

#include <Settings/SettingsManager.h>

#include <Resources/PlayIcon.xpm>
#include <Resources/StopIcon.xpm>
#include <Resources/OpenFolderIcon.xpm>

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Gui;

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_CAPTURE_VOICE_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_CAPTURE_VOICE_THREAD, wxCommandEvent);

CaptureVoicePage::CaptureVoicePage(NBiometricClient& biometricClient, NSubject& subject, SubjectEditorPageInterface& subjectEditorPageInterface,
	wxWindow *parent, wxWindowID winid) : ModalityPage(biometricClient, subject, subjectEditorPageInterface, parent, winid)
{
	m_phrases = NULL;
	m_phraseCount = 0;
	m_newSubject = NULL;
	m_voice = NULL;

	CreateGUIControls();
	RegisterGuiEvents();
}

CaptureVoicePage::~CaptureVoicePage()
{
	UnregisterGuiEvents();

	if (m_phrases)
		delete[] m_phrases;
}

void CaptureVoicePage::SetIsBusy(bool value)
{
	if (value)
	{
		m_isIdle.Reset();
	}
	else
	{
		m_isIdle.Set();
	}
}

void CaptureVoicePage::OnThread(wxCommandEvent &event)
{
	int id = event.GetId();

	switch(id)
	{
	case ID_EVENT_CAPTURE_COMPLETED:
		{
			try
			{
				NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
				NValue result = operation.GetResult();
				NBiometricTask task = NObjectDynamicCast<NBiometricTask>(result.ToObject(NBiometricTask::NativeTypeOf()));
				NError exception = task.GetError();
				if (!exception.IsNull()) wxExceptionDlg::Show(exception);
				UpdateStatus(m_voice.GetStatus());
			}
			catch(NError & error)
			{
				UpdateStatus(nbsInternalError);
				wxExceptionDlg::Show(error);
			}

			SetIsBusy(false);
			EnableControls();
			break;
		}
	default:
		break;
	};
}

void CaptureVoicePage::OnNavigatedTo()
{
	EnableControls();
	m_statusPanel->Hide();
	m_chbCaptureAutomatically->SetValue(true);
	LoadPhrases();
	m_voiceView->SetVoice(NVoice(NULL));
	this->Layout();

	ModalityPage::OnNavigatedTo();
}

void CaptureVoicePage::OnNavigatingFrom()
{
	Cancel();

	if (!m_newSubject.IsNull())
	{
		if (m_newSubject.GetStatus() == nbsOk)
		{
			NArrayWrapper<NVoice> voices = m_newSubject.GetVoices().GetAll();
			m_newSubject.GetVoices().Clear();
			for (NArrayWrapper<NVoice>::iterator it = voices.begin(); it != voices.end(); it++)
			{
				m_subject.GetVoices().Add(*it);
			}
		}
	}

	m_newSubject = NULL;
	m_voice = NULL;
	m_voiceView->SetVoice(m_voice);

	ModalityPage::OnNavigatingFrom();
}

void CaptureVoicePage::UpdateStatus(::Neurotec::Biometrics::NBiometricStatus status)
{
	wxString strStatus = wxEmptyString;

	if (status == nbsOk)
	{
		strStatus = wxT("Extraction successful");
	}
	else
	{
		wxString strEnum = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
		strStatus = wxString::Format(wxT("Extraction failed: %s"), strEnum);
	}

	m_statusPanel->SetMessage(strStatus,
		(status == nbsOk || status == nbsNone)? StatusPanel::SUCCESS_MESSAGE : StatusPanel::ERROR_MESSAGE);
}

void CaptureVoicePage::LoadPhrases()
{
	if (m_phrases != NULL)
	{
		delete[] m_phrases;
		m_phrases = NULL;
	}

	m_phraseCount = 0;

	m_phrases = SettingsManager::GetPhrases(&m_phraseCount);

	m_choicePhraseId->Clear();

	if (m_phrases == NULL)
	{
		m_choicePhraseId->Enable(false);

		return;
	}

	m_choicePhraseId->Enable(true);

	for (int i = 0; i < m_phraseCount; i++)
	{
		m_choicePhraseId->Append(m_phrases[i].GetPhrase(), reinterpret_cast<void *>(&m_phrases[i]));
	}

	m_choicePhraseId->SetSelection(0);
}

void CaptureVoicePage::CaptureAsyncCompletedCallback(EventArgs args)
{
	CaptureVoicePage *page = reinterpret_cast<CaptureVoicePage *>(args.GetParam());
	wxCommandEvent event(wxEVT_CAPTURE_VOICE_THREAD, ID_EVENT_CAPTURE_COMPLETED);
	event.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
	wxPostEvent(page, event);
}

void CaptureVoicePage::OnStartClick(wxCommandEvent&)
{
	DisableControls();

	m_btnStop->Enable(true);
	m_btnForce->Enable(!m_chbCaptureAutomatically->IsChecked());

	m_voice = NVoice();
	m_newSubject = NSubject();

	if (!m_chbCaptureAutomatically->IsChecked())
	{
		m_voice.SetCaptureOptions(nbcoManual);
	}

	m_newSubject.GetVoices().Add(m_voice);

	int id = 0;
	if (m_choicePhraseId->GetSelection() > -1)
	{
		SettingsManager::Phrase *phrase = reinterpret_cast<SettingsManager::Phrase *>(
			m_choicePhraseId->GetClientData(m_choicePhraseId->GetSelection()));

		if (phrase != NULL)
		{
			id = phrase->GetId();
		}
	}

	m_voice.SetSoundBuffer(NULL);
	m_voice.SetFileName(wxEmptyString);
	m_voice.SetPhraseId(id);

	m_voiceView->SetVoice(m_voice);

	m_statusPanel->SetMessage(wxT("Extracting record. Please wait ..."), StatusPanel::INFO_MESSAGE);
	m_statusPanel->Show(true);

	this->Layout();

	NBiometricTask task = m_biometricClient.CreateTask((NBiometricOperations)(nboCapture | nboSegment | nboCreateTemplate), m_newSubject);
	NAsyncOperation operation = m_biometricClient.PerformTaskAsync(task);
	SetIsBusy(true);
	operation.AddCompletedCallback(&CaptureVoicePage::CaptureAsyncCompletedCallback, this);
}

void CaptureVoicePage::OnOpenFileClick(wxCommandEvent&)
{
	wxFileDialog openFileDialog(this, wxT("Choose audio file to extract"), wxEmptyString, wxEmptyString,
		wxEmptyString, wxFD_OPEN | wxFD_FILE_MUST_EXIST);

	if ((openFileDialog.ShowModal() != wxID_OK) || (openFileDialog.GetPath() == wxEmptyString))
		return;

	DisableControls();

	m_voice = NVoice();
	m_newSubject = NSubject();

	m_newSubject.GetVoices().Add(m_voice);

	int id = 0;
	if (m_choicePhraseId->GetSelection() > -1)
	{
		SettingsManager::Phrase *phrase = reinterpret_cast<SettingsManager::Phrase *>(
			m_choicePhraseId->GetClientData(m_choicePhraseId->GetSelection()));

		if (phrase != NULL)
		{
			id = phrase->GetId();
		}
	}

	m_voice.SetSoundBuffer(NULL);
	m_voice.SetFileName(openFileDialog.GetPath());
	m_voice.SetPhraseId(id);

	m_voiceView->SetVoice(m_voice);

	m_statusPanel->SetMessage(wxT("Extracting record. Please wait ..."), StatusPanel::INFO_MESSAGE);
	m_statusPanel->Show(true);

	this->Layout();

	NBiometricTask task = m_biometricClient.CreateTask((NBiometricOperations)(nboSegment | nboCreateTemplate), m_newSubject);
	NAsyncOperation operation = m_biometricClient.PerformTaskAsync(task);
	SetIsBusy(true);
	operation.AddCompletedCallback(&CaptureVoicePage::CaptureAsyncCompletedCallback, this);
}

void CaptureVoicePage::OnStopClick(wxCommandEvent&)
{
	m_biometricClient.Force();

	m_btnStop->Enable(false);
}

void CaptureVoicePage::OnForceClick(wxCommandEvent&)
{
	m_biometricClient.ForceStart();
	m_btnForce->Enable(false);
}

void CaptureVoicePage::OnEditClick(wxCommandEvent&)
{
	EditPhrasesDialog dialog(this, wxID_ANY, wxT("Phrases editor"));

	dialog.ShowModal();

	LoadPhrases();
}

void CaptureVoicePage::OnFinishClick(wxCommandEvent&)
{
	SelectFirstPage();
}

void CaptureVoicePage::OnSourceChanged(wxCommandEvent&)
{
	m_btnOpenFile->Enable(false);
	m_btnStart->Enable(false);
	m_btnStop->Enable(false);
	m_btnForce->Enable(false);
	m_chbCaptureAutomatically->Enable(false);

	if (m_radioMicrophone->GetValue())
	{
		m_btnStart->Enable(true);
		m_chbCaptureAutomatically->Enable(true);
	}
	else
	{
		m_btnOpenFile->Enable(true);
	}
}

void CaptureVoicePage::DisableControls()
{
	m_btnStop->Enable(false);
	m_btnStart->Enable(false);
	m_btnForce->Enable(false);
	m_btnOpenFile->Enable(false);
	m_radioMicrophone->Enable(false);
	m_radioSoundFile->Enable(false);
	m_choicePhraseId->Enable(false);
	m_btnEdit->Enable(false);
	m_chbCaptureAutomatically->Enable(false);
}

void CaptureVoicePage::EnableControls()
{
	DisableControls();

	if (m_choicePhraseId->GetCount() > 0)
	{
		m_choicePhraseId->Enable(true);
	}

	m_btnEdit->Enable(true);

	m_radioSoundFile->Enable(true);

	if (!m_biometricClient.GetVoiceCaptureDevice().IsNull() &&
		m_biometricClient.GetVoiceCaptureDevice().IsAvailable() &&
		(m_biometricClient.GetLocalOperations() & nboCreateTemplate) != 0)
	{
		m_radioMicrophone->Enable(true);
		m_radioMicrophone->SetValue(true);
	}
	else
	{
		m_radioSoundFile->SetValue(true);
	}

	if (m_radioMicrophone->GetValue())
	{
		m_btnStart->Enable(true);
		m_chbCaptureAutomatically->Enable(true);
	}
	else
	{
		m_btnOpenFile->Enable(true);
	}

	IsBusy() ? m_busyIndicator->Show() : m_busyIndicator->Hide();
	m_statusSizer->RecalcSizes();
}

void CaptureVoicePage::RegisterGuiEvents()
{
	this->Bind(wxEVT_CAPTURE_VOICE_THREAD, &CaptureVoicePage::OnThread, this);
	m_radioMicrophone->Connect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureVoicePage::OnSourceChanged), NULL, this);
	m_radioSoundFile->Connect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureVoicePage::OnSourceChanged), NULL, this);
	m_btnEdit->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureVoicePage::OnEditClick), NULL, this);
	m_btnFinish->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureVoicePage::OnFinishClick), NULL, this);
	m_btnStart->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureVoicePage::OnStartClick), NULL, this);
	m_btnStop->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureVoicePage::OnStopClick), NULL, this);
	m_btnForce->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureVoicePage::OnForceClick), NULL, this);
	m_btnOpenFile->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureVoicePage::OnOpenFileClick), NULL, this);
}

void CaptureVoicePage::UnregisterGuiEvents()
{
	m_radioMicrophone->Disconnect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureVoicePage::OnSourceChanged), NULL, this);
	m_radioSoundFile->Disconnect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureVoicePage::OnSourceChanged), NULL, this);
	m_btnEdit->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureVoicePage::OnEditClick), NULL, this);
	m_btnFinish->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureVoicePage::OnFinishClick), NULL, this);
	m_btnStart->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureVoicePage::OnStartClick), NULL, this);
	m_btnStop->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureVoicePage::OnStopClick), NULL, this);
	m_btnForce->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureVoicePage::OnForceClick), NULL, this);
	m_btnOpenFile->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureVoicePage::OnOpenFileClick), NULL, this);
	this->Unbind(wxEVT_CAPTURE_VOICE_THREAD, &CaptureVoicePage::OnThread, this);
}

void CaptureVoicePage::CreateGUIControls()
{
	wxBoxSizer *sizer = new wxBoxSizer(wxVERTICAL);
	this->SetSizer(sizer, true);

	wxStaticText *text = new wxStaticText(this, wxID_ANY, wxT("1. Please select secret phrase ID from the list:"));
	sizer->Add(text, 0, wxALL | wxEXPAND, 5);

	wxStaticBoxSizer *szStaticBoxSizer = new wxStaticBoxSizer(new wxStaticBox(this, wxID_ANY, wxT("Select phrase (Please answer the question )")), wxVERTICAL);
	sizer->Add(szStaticBoxSizer, 0, wxALL | wxEXPAND, 5);

	wxBoxSizer *szBox = new wxBoxSizer(wxHORIZONTAL);
	szStaticBoxSizer->Add(szBox, 0, wxALL | wxEXPAND, 0);

	text = new wxStaticText(this, wxID_ANY, wxT("Selected phrase ID:"));
	szBox->Add(text, 0, wxALL | wxALIGN_CENTER_VERTICAL, 5);

	m_choicePhraseId = new wxChoice(this, wxID_ANY);
	szBox->Add(m_choicePhraseId, 1, wxALL | wxEXPAND, 5);

	m_btnEdit = new wxButton(this, wxID_ANY, wxT("Edit"));
	szBox->Add(m_btnEdit, 0, wxALL, 5);

	text = new wxStaticText(this, wxID_ANY, wxT("Phrase Id:"));
	szStaticBoxSizer->Add(text, 0, wxALL, 5);

	wxFont font;
	font.SetWeight(wxFONTWEIGHT_BOLD);
	font.SetPointSize(10);

	text = new wxStaticText(this, wxID_ANY, wxT("Phrase should be secret answer to the selected question. Phrase duration should be at least"));
	text->SetFont(font);
	szStaticBoxSizer->Add(text, 0, wxALL, 5);

	text = new wxStaticText(this, wxID_ANY, wxT("about 6 seconds or 4 words."));
	text->SetFont(font);
	szStaticBoxSizer->Add(text, 0, wxRIGHT | wxLEFT, 5);

	text = new wxStaticText(this, wxID_ANY, wxT("2. Please select sound source:"));
	sizer->Add(text, 0, wxALL | wxEXPAND, 5);

	szStaticBoxSizer = new wxStaticBoxSizer(new wxStaticBox(this, wxID_ANY, wxT("Source")), wxVERTICAL);
	sizer->Add(szStaticBoxSizer, 0, wxALL | wxEXPAND, 5);

	szBox = new wxBoxSizer(wxHORIZONTAL);
	szStaticBoxSizer->Add(szBox, 0, wxALL | wxEXPAND);

	m_radioSoundFile = new wxRadioButton(this, wxID_ANY, wxT("Sound file"));
	szBox->Add(m_radioSoundFile, 1);

	m_btnOpenFile = new wxButton(this, wxID_ANY, wxT("Open file"));
	m_btnOpenFile->SetBitmap(wxImage(wxImage(openFolderIcon_xpm)));
	m_btnOpenFile->Fit();
	szBox->Add(m_btnOpenFile, 0);

	m_radioMicrophone = new wxRadioButton(this, wxID_ANY, wxT("Microphone"));
	szStaticBoxSizer->Add(m_radioMicrophone);

	szStaticBoxSizer = new wxStaticBoxSizer(new wxStaticBox(this, wxID_ANY, wxT("Capture")), wxVERTICAL);
	sizer->Add(szStaticBoxSizer, 0, wxALL | wxEXPAND, 5);

	m_chbCaptureAutomatically = new wxCheckBox(this, wxID_ANY, wxT("Capture automatically"));
	szStaticBoxSizer->Add(m_chbCaptureAutomatically, 0, wxALL | wxEXPAND, 5);

	szBox = new wxBoxSizer(wxHORIZONTAL);
	szStaticBoxSizer->Add(szBox, 0, wxALL | wxEXPAND, 5);

	m_btnStart = new wxButton(this, wxID_ANY, wxT("Start"));
	m_btnStart->SetBitmap(wxImage(wxImage(playIcon_xpm)));
	m_btnStart->Fit();
	szBox->Add(m_btnStart, 0, wxALL, 5);

	m_btnStop = new wxButton(this, wxID_ANY, wxT("Stop"));
	m_btnStop->SetBitmap(wxImage(wxImage(stopIcon_xpm)));
	m_btnStop->Fit();
	szBox->Add(m_btnStop, 0, wxALL, 5);

	m_btnForce = new wxButton(this, wxID_ANY, wxT("Force"));
	m_btnForce->Fit();
	szBox->Add(m_btnForce, 0, wxALL, 5);

	m_voiceView = new wxNVoiceView(this);
	szBox->Add(m_voiceView, 0, wxALL | wxEXPAND, 5);

	m_statusSizer = new wxBoxSizer(wxHORIZONTAL);
	sizer->Add(m_statusSizer, 0, wxLEFT | wxRIGHT | wxEXPAND, 5);

	m_busyIndicator = new BusyIndicator( this, wxID_ANY, wxDefaultPosition, wxSize(14, 14) );
	m_busyIndicator->Hide();
	m_statusSizer->Add( m_busyIndicator, 0, wxRIGHT, 5 );

	m_statusPanel = new StatusPanel(this, wxID_ANY);
	m_statusSizer->Add( m_statusPanel, 1, wxEXPAND, 5 );

	szBox = new wxBoxSizer(wxVERTICAL);
	sizer->Add(szBox, 1, wxALL | wxEXPAND);

	szBox->AddSpacer(0);

	m_btnFinish = new wxButton(this, wxID_ANY, wxT("Finish"));
	sizer->Add(m_btnFinish, 0, wxALIGN_RIGHT | wxALL, 5);

	this->Layout();
}

}}


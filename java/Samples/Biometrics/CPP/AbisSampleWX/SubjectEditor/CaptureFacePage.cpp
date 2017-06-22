#include "Precompiled.h"

#include <SubjectEditor/CaptureFacePage.h>
#include <Settings/SettingsManager.h>

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Devices;
using namespace Neurotec::Gui;
using namespace Neurotec::Images;

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_CAPTURE_FACE_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_CAPTURE_FACE_THREAD, wxCommandEvent);

CaptureFacePage::CaptureFacePage(NBiometricClient& biometricClient, NSubject& subject, SubjectEditorPageInterface& subjectEditorPageInterface, wxWindow *parent, wxWindowID winid) :
	ModalityPage(biometricClient, subject, subjectEditorPageInterface, parent, winid),
	m_newSubject(NULL),
	m_currentBiometric(NULL),
	m_isExtractStarted(false),
	m_sessionId(-1),
	m_titlePrefix(wxEmptyString)
{
	CreateGUIControls();
	RegisterGuiEvents();
}

CaptureFacePage::~CaptureFacePage()
{
	UnregisterGuiEvents();
	m_zoomSlider->SetView(NULL);
	m_currentBiometric = NULL;
	m_subject = NULL;
}

void CaptureFacePage::SetIsBusy(bool value)
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

void CaptureFacePage::OnNavigatedTo()
{
	m_sessionId = -1;
	for (int i = 0; i < m_subject.GetFaces().GetCount(); i++)
	{
		m_sessionId = std::max(m_sessionId, m_subject.GetFaces()[i].GetSessionId());
	}
	m_sessionId++;

	m_newSubject = NSubject();
	m_biometricClient.AddPropertyChangedCallback(&CaptureFacePage::OnBiometricClientPropertyChangedCallback, this);
	m_biometricClient.SetCurrentBiometricCompletedTimeout(5000);
	m_biometricClient.AddCurrentBiometricCompletedCallback(&CaptureFacePage::OnCurrentBiometricCompletedCallback, this);

	m_subjectTreeControl->Hide();
	m_icaoView->Hide();
	OnFaceCaptureDeviceChanged();
	m_statusPanel->Hide();
	m_generalizationView->Hide();

	bool mirrorHorizontally = SettingsManager::GetFacesMirrorHorizontally();
	m_chbMirrorHorizontally->SetValue(mirrorHorizontally);
	m_faceView->SetMirrorHorizontally(mirrorHorizontally);

	EnableControls();

	m_subjectTreeControl->SetSubject(m_newSubject);

	ModalityPage::OnNavigatedTo();
}

void CaptureFacePage::OnNavigatingFrom()
{
	Cancel();

	m_biometricClient.RemovePropertyChangedCallback(&CaptureFacePage::OnBiometricClientPropertyChangedCallback, this);
	m_biometricClient.RemoveCurrentBiometricCompletedCallback(&CaptureFacePage::OnCurrentBiometricCompletedCallback, this);
	m_biometricClient.SetCurrentBiometricCompletedTimeout(0);
	m_subjectTreeControl->SetSelectedItem(NULL);
	m_subjectTreeControl->SetSubject(NULL);
	m_icaoView->SetFace(NULL);
	m_faceView->Clear();

	if (m_newSubject.GetStatus() == nbsOk)
	{
		NArrayWrapper<NFace> faces = m_newSubject.GetFaces().GetAll();
		m_newSubject.Clear();
		for (NArrayWrapper<NFace>::iterator it = faces.begin(); it != faces.end(); it++)
		{
			m_subject.GetFaces().Add(*it);
		}
		m_newSubject = NULL;
		m_currentBiometric = NULL;
	}

	SettingsManager::SetFacesMirrorHorizontally(m_chbMirrorHorizontally->GetValue());

	ModalityPage::OnNavigatingFrom();
}

void CaptureFacePage::OnFaceStatusChanged(NBiometricStatus status)
{
	wxString format = m_isExtractStarted ? wxT("%sExtraction status: %s") : wxT("%sDetection status: %s");
	wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
	wxString msg = wxString::Format(format, m_titlePrefix, statusString);
	m_statusPanel->SetMessage(msg, (status == nbsOk || status == nbsNone)? StatusPanel::SUCCESS_MESSAGE : StatusPanel::ERROR_MESSAGE);
}

void CaptureFacePage::OnFaceCaptureDeviceChanged()
{
	try
	{
		NCamera device = m_biometricClient.GetFaceCaptureDevice();
		if (device.IsNull()|| !device.IsAvailable())
		{
			if (m_radioCamera->GetValue()) m_radioImageFile->SetValue(true);
			m_radioCamera->SetLabel(wxT("From camera (Not connected)"));
		}
		else
		{
			wxString displayName = device.GetDisplayName();
			m_radioCamera->SetLabel(wxString::Format(wxT("From camera (%s)"), displayName.c_str()));
		}
		EnableControls();
	}
	catch(NError & error)
	{
		wxExceptionDlg::Show(error);
	}
}

void CaptureFacePage::OnCurrentBiometricChanged(NFace & face)
{
	if (IsPageShown())
	{
		if (!face.IsNull()) m_faceView->SetFace(face);
		if (!face.IsNull() && m_chbCheckIcao->GetValue()) m_icaoView->SetFace(face);
		if (m_chbWithGeneralization->GetValue() && !face.IsNull())
		{
			int index = m_newSubject.GetFaces().IndexOf(face);
			m_generalizationView->SetSelected(face);
			m_titlePrefix = wxString::Format(wxT("Capturing face %d of %d. "), index + 1, SettingsManager::GetFacesGeneralizationRecordCount());
		}
		if (m_radioCamera->GetValue() || m_radioVideoFile->GetValue())
		{
			m_isExtractStarted = !m_chbManual->GetValue();
			if (!m_currentBiometric.IsNull()) m_currentBiometric.RemovePropertyChangedCallback(&CaptureFacePage::OnFacePropertyChangedCallback, this);
			m_currentBiometric = face;
			if (!m_currentBiometric.IsNull()) m_currentBiometric.AddPropertyChangedCallback(&CaptureFacePage::OnFacePropertyChangedCallback, this);
		}
		EnableControls();
	}
}

void CaptureFacePage::OnBiometricClientPropertyChangedCallback(NObject::PropertyChangedEventArgs args)
{
	CaptureFacePage * page = static_cast<CaptureFacePage *>(args.GetParam());
	wxString propertyName = args.GetPropertyName();
	if (propertyName == wxT("FaceCaptureDevice"))
	{
		wxPostEvent(page, wxCommandEvent(wxEVT_CAPTURE_FACE_THREAD, ID_EVENT_CAMERA_CHANGED));
	}
	else if (propertyName == wxT("CurrentBiometric"))
	{
		NBiometricClient client = args.GetObject<NBiometricClient>();
		NBiometric current = client.GetCurrentBiometric();
		wxCommandEvent evt(wxEVT_CAPTURE_FACE_THREAD, ID_EVENT_CURRENT_BIOMETRIC_CHANGED);
		evt.SetClientData(!current.IsNull() ? current.RefHandle() : NULL);
		wxPostEvent(page, evt);
	}
}

void CaptureFacePage::UpdateWithTaskResult(NBiometricStatus status)
{
	if (IsPageShown())
	{
		PrepareViews(false, m_chbCheckIcao->GetValue(), status == nbsOk);

		bool withGeneralization = m_chbWithGeneralization->GetValue();
		wxString statusString = wxT("Liveness check failed");
		if (status != nbsTimeout) statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
		wxString statusText = wxString::Format(wxT("Extraction status: %s"), statusString.c_str());
		m_statusPanel->SetMessage(statusText, status == nbsOk ? StatusPanel::SUCCESS_MESSAGE : StatusPanel::ERROR_MESSAGE);
		m_statusPanel->Show();
		if (withGeneralization && status == nbsOk)
		{
			NInt count = m_newSubject.GetFaces().GetCount();
			NFace generalized = m_newSubject.GetFaces()[count - 1];
			m_generalizationView->SetGeneralized(generalized);
			m_generalizationView->SetSelected(generalized);
		}
		m_generalizationView->SetEnableMouseSelection(true);
		EnableControls();
	}
}

void CaptureFacePage::PrepareViews(bool isCapturing, bool checkIcao, bool isOk)
{
	m_icaoView->Show(checkIcao);
	if (isCapturing)
	{
		m_faceView->SetShowAge(!checkIcao);
		m_faceView->SetShowEmotions(!checkIcao);
		m_faceView->SetShowExpression(!checkIcao);
		m_faceView->SetShowGender(!checkIcao);
		m_faceView->SetShowProperties(!checkIcao);
		m_faceView->SetShowIcaoArrows(true);
		m_subjectTreeControl->Hide();
	}
	else
	{
		m_faceView->SetShowAge(true);
		m_faceView->SetShowEmotions(true);
		m_faceView->SetShowExpression(true);
		m_faceView->SetShowGender(true);
		m_faceView->SetShowProperties(true);
		m_faceView->SetShowIcaoArrows(false);
		m_subjectTreeControl->Show(checkIcao && isOk);
		if (checkIcao)
		{
			m_subjectTreeControl->UpdateTree();

			NFace face = m_newSubject.GetFaces()[0];
			Node * node = m_subjectTreeControl->GetBiometricNode(face);
			std::vector<Node*> children = node->GetChildren();
			m_subjectTreeControl->SetSelectedItem(children.empty() ? node : children[0]);
		}
	}
	m_faceView->Refresh();
	Layout();
}

void CaptureFacePage::OnCurrentBiometricCompleted(NBiometricStatus status)
{
	if (IsPageShown())
	{
		bool allowRepeat = m_btnRepeat->IsShown() && status != nbsOk;
		if (!allowRepeat)
			m_biometricClient.Force();
		else
		{
			wxString format = wxT("%sExtraction status: %s");
			wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
			wxString msg = wxString::Format(format, m_titlePrefix, statusString);
			m_statusPanel->SetMessage(msg, StatusPanel::ERROR_MESSAGE);
		}
		m_btnRepeat->Enable(allowRepeat);
	}
}

void CaptureFacePage::OnCurrentBiometricCompletedCallback(EventArgs args)
{
	CaptureFacePage *page = reinterpret_cast<CaptureFacePage *>(args.GetParam());
	NBiometricClient client = args.GetObject<NBiometricClient>();
	NFace current = NObjectDynamicCast<NFace>(client.GetCurrentBiometric());
	NBiometricStatus status = current.GetStatus();
	if (status == nbsOk && current.GetObjects().GetCount() > 0)
	{
		NBiometricAttributes attributes = current.GetObjects()[0];
		NBiometric child = NObjectDynamicCast<NBiometric>(attributes.GetChild());
		if (!child.IsNull())
			status = child.GetStatus();
	}
	wxCommandEvent event(wxEVT_CAPTURE_FACE_THREAD, ID_EVENT_CURRENT_BIOMETRIC_COMPLETED);
	event.SetInt((int)current.GetStatus());
	wxPostEvent(page, event);
}

void CaptureFacePage::OnCreateTemplateAsyncCompleted(Neurotec::EventArgs args)
{
	CaptureFacePage * page = reinterpret_cast<CaptureFacePage *>(args.GetParam());
	wxCommandEvent event(wxEVT_CAPTURE_FACE_THREAD, ID_EVENT_CAPTURE_FINISHED);
	event.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
	wxPostEvent(page, event);
}

void CaptureFacePage::OnFacePropertyChangedCallback(NObject::PropertyChangedEventArgs args)
{
	CaptureFacePage * page = reinterpret_cast<CaptureFacePage *>(args.GetParam());
	if (args.GetPropertyName().Equals(N_T("Status")))
	{
		NFace face = args.GetObject<NFace>();
		wxCommandEvent evt(wxEVT_CAPTURE_FACE_THREAD, ID_EVENT_STATUS_CHANGED);
		evt.SetInt((int)face.GetStatus());
		wxPostEvent(page, evt);
	}
}

void CaptureFacePage::OnThread(wxCommandEvent &event)
{
	int id = event.GetId();
	try
	{
		switch(id)
		{
		case ID_EVENT_STATUS_CHANGED:
			{
				NBiometricStatus status = (NBiometricStatus)event.GetInt();
				OnFaceStatusChanged(status);
				break;
			}
		case ID_EVENT_CURRENT_BIOMETRIC_CHANGED:
			{
				NFace current((HNObject)event.GetClientData(), true);
				OnCurrentBiometricChanged(current);
				break;
			}
		case ID_EVENT_CURRENT_BIOMETRIC_COMPLETED:
			{
				NBiometricStatus status = (NBiometricStatus)event.GetInt();
				OnCurrentBiometricCompleted(status);
				break;
			}
		case ID_EVENT_CAPTURE_FINISHED:
			{
				try
				{
					NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
					NValue result = operation.GetResult();
					NBiometricTask task = NObjectDynamicCast<NBiometricTask>(result.ToObject(NBiometricTask::NativeTypeOf()));
					NError error = task.GetError();
					if (!error.IsNull()) wxExceptionDlg::Show(error);
					UpdateWithTaskResult(m_newSubject.GetStatus());
				}
				catch(NError & error)
				{
					UpdateWithTaskResult(nbsInternalError);
					wxExceptionDlg::Show(error);
				}

				SetIsBusy(false);
				EnableControls();
				break;
			}
		case ID_EVENT_CAMERA_CHANGED:
			{
				OnFaceCaptureDeviceChanged();
				break;
			}
		default:
			break;
		};
	}
	catch(NError & error)
	{
		wxExceptionDlg::Show(error);
	}
}

void CaptureFacePage::OnRadioButtonCheckedChanged(wxCommandEvent & event)
{
	bool checked = true;
	switch(event.GetId())
	{
	case ID_RADIO_CAMERA:
		checked = m_radioCamera->GetValue();
		break;
	case ID_RADIO_VIDEO:
		checked = m_radioVideoFile->GetValue();
		break;
	case ID_RADIO_IMAGE:
		checked = m_radioImageFile->GetValue();
		break;
	default:
		break;
	};

	if (checked)
		EnableControls();
}

void CaptureFacePage::OnCaptureClick(wxCommandEvent&)
{
	bool generalize = m_chbWithGeneralization->GetValue();
	bool fromFile = m_radioImageFile->GetValue();
	bool fromCamera = m_radioCamera->GetValue();
	bool checkIcao = m_chbCheckIcao->GetValue();
	int count = generalize ? SettingsManager::GetFacesGeneralizationRecordCount() : 1;
	NBiometricCaptureOptions options = nbcoNone;
	if (m_chbManual->GetValue()) options = nbcoManual;
	if (m_chbStream->GetValue()) options = (NBiometricCaptureOptions)(options | nbcoStream);

	m_statusPanel->SetMessage(wxEmptyString, StatusPanel::INFO_MESSAGE);
	m_titlePrefix = wxEmptyString;
	m_newSubject.Clear();
	m_faceView->Clear();
	m_generalizationView->Clear();
	m_generalizationView->SetEnableMouseSelection(false);
	m_generalizationView->Show(generalize);

	std::vector<wxString> selectedFiles;
	wxString title = fromFile ? wxT("Select image") : wxT("Select video file");
	wxString titleFormat = fromFile ? wxT("Select face image (%d out of %d)") : wxT("Select video file (%d out of %d)");
	wxString fileFilter = fromFile ? Common::GetOpenFileFilterString(true, true) : (wxString)wxEmptyString;
	if (m_radioImageFile->GetValue() || m_radioVideoFile->GetValue())
	{
		while ((int)selectedFiles.size() < count)
		{
			if (generalize) title = wxString::Format(titleFormat, (int)selectedFiles.size() + 1, count);
			wxFileDialog openFileDialog(this, title, wxEmptyString, wxEmptyString, fileFilter, wxFD_OPEN | wxFD_FILE_MUST_EXIST);
			if (openFileDialog.ShowModal() != wxID_OK) return;
			selectedFiles.push_back(openFileDialog.GetPath());
		}
	}

	int id = generalize ? m_sessionId : -1;
	for (int i = 0; i < count; i++)
	{
		NFace face;
		face.SetSessionId(id);
		face.SetFileName(!fromCamera ? selectedFiles[i].c_str() : wxEmptyString);
		face.SetCaptureOptions(options);
		m_newSubject.GetFaces().Add(face);
	}

	NFace first = m_newSubject.GetFaces()[0];
	m_faceView->SetFace(first);
	if (generalize)
	{
		NArrayWrapper<NFace> faces = m_newSubject.GetFaces().GetAll();
		m_generalizationView->SetBiometrics(faces);
		m_generalizationView->SetSelected(first);
	}

	m_icaoView->SetFace(m_newSubject.GetFaces()[0]);

	m_biometricClient.SetFacesCheckIcaoCompliance(checkIcao);
	NBiometricOperations operations = fromFile ? nboCreateTemplate : (NBiometricOperations)(nboCapture | nboCreateTemplate);
	if (checkIcao) operations = (NBiometricOperations)(operations | nboSegment);
	NBiometricTask biometricTask = m_biometricClient.CreateTask(operations, m_newSubject);
	m_statusPanel->SetMessage(fromFile ? wxT("Extracting template ...") : wxT("Starting capturing ..."), StatusPanel::INFO_MESSAGE);
	m_statusPanel->Show();
	SetIsBusy(true);
	NAsyncOperation operation = m_biometricClient.PerformTaskAsync(biometricTask);
	operation.AddCompletedCallback(&CaptureFacePage::OnCreateTemplateAsyncCompleted, this);
	EnableControls();
	PrepareViews(true, checkIcao);
	Layout();
}

void CaptureFacePage::OnFinishClick(wxCommandEvent&)
{
	SelectFirstPage();
}

void CaptureFacePage::OnStartClick(wxCommandEvent&)
{
	if (!m_chbCheckIcao->GetValue())
	{
		m_isExtractStarted = true;
		m_btnForceEnd->Enable(m_chbStream->GetValue());
		m_btnForceStart->Enable(false);
	}
	else if (m_chbManual->GetValue())
	{
		m_btnForceStart->SetLabel("Force");
		wxFont font = m_btnForceStart->GetFont();
		font.SetWeight(wxFONTWEIGHT_NORMAL);
		m_btnForceStart->SetFont(font);
	}
	m_biometricClient.Force();
}

void CaptureFacePage::OnEndClick(wxCommandEvent&)
{
	m_btnForceEnd->Enable(false);
	m_isExtractStarted = false;
	m_biometricClient.Force();
}

void CaptureFacePage::OnCancelClick(wxCommandEvent&)
{
	m_biometricClient.Cancel();
}

void CaptureFacePage::OnRepeatClick(wxCommandEvent&)
{
	m_biometricClient.Repeat();
}

void CaptureFacePage::OnMirrorHorizontallyCheckedChanged(wxCommandEvent&)
{
	m_faceView->SetMirrorHorizontally(m_chbMirrorHorizontally->GetValue());
	m_faceView->Refresh();
}

void CaptureFacePage::OnCheckIcaoComplianceCheckedChanged(wxCommandEvent & /*event*/)
{
	if (m_chbCheckIcao->GetValue())
	{
		m_chbStream->SetValue(m_chbStream->IsEnabled());
		m_chbManual->SetValue(false);
	}
}

void CaptureFacePage::OnSubjectTreePropertyChanged(wxCommandEvent & /*event*/)
{
	if (m_subjectTreeControl->IsShown())
	{
		Node * selected = m_subjectTreeControl->GetSelectedItem();
		if (selected)
		{
			std::vector<NBiometric> items = selected->GetItems();
			if (m_generalizationView->IsShown())
			{
				std::vector<NBiometric> allGeneralized = selected->GetAllGeneralized();
				std::vector<NBiometric> allItems = selected->GetAllItems();
				m_generalizationView->SetBiometrics(items);
				m_generalizationView->SetGeneralized(allGeneralized);
				m_generalizationView->SetSelected(allItems[0]);
			}
			else
			{
				NFace face = NObjectDynamicCast<NFace>(items[0]);
				m_faceView->SetFace(face);
				m_icaoView->SetFace(face);
			}
		}
	}
}

void CaptureFacePage::RegisterGuiEvents()
{
	this->Bind(wxEVT_CAPTURE_FACE_THREAD, &CaptureFacePage::OnThread, this);
	m_radioCamera->Connect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureFacePage::OnRadioButtonCheckedChanged), NULL, this);
	m_radioImageFile->Connect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureFacePage::OnRadioButtonCheckedChanged), NULL, this);
	m_radioVideoFile->Connect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureFacePage::OnRadioButtonCheckedChanged), NULL, this);
	m_btnCapture->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFacePage::OnCaptureClick), NULL, this);
	m_btnCancel->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFacePage::OnCancelClick), NULL, this);
	m_btnForceStart->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFacePage::OnStartClick), NULL, this);
	m_btnForceEnd->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFacePage::OnEndClick), NULL, this);
	m_btnRepeat->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFacePage::OnRepeatClick), NULL, this);
	m_btnFinish->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFacePage::OnFinishClick), NULL, this);
	m_chbMirrorHorizontally->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(CaptureFacePage::OnMirrorHorizontallyCheckedChanged), NULL, this);
	m_chbCheckIcao->Connect(wxEVT_CHECKBOX, wxCommandEventHandler(CaptureFacePage::OnCheckIcaoComplianceCheckedChanged), NULL, this);
	m_subjectTreeControl->Connect(wxEVT_TREE_SELECTED_ITEM_CHANGED, wxCommandEventHandler(CaptureFacePage::OnSubjectTreePropertyChanged), NULL, this);
}

void CaptureFacePage::UnregisterGuiEvents()
{
	m_radioCamera->Disconnect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureFacePage::OnRadioButtonCheckedChanged), NULL, this);
	m_radioImageFile->Disconnect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureFacePage::OnRadioButtonCheckedChanged), NULL, this);
	m_radioVideoFile->Disconnect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureFacePage::OnRadioButtonCheckedChanged), NULL, this);
	m_btnCapture->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFacePage::OnCaptureClick), NULL, this);
	m_btnCancel->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFacePage::OnCancelClick), NULL, this);
	m_btnForceStart->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFacePage::OnStartClick), NULL, this);
	m_btnForceEnd->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFacePage::OnEndClick), NULL, this);
	m_btnFinish->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFacePage::OnFinishClick), NULL, this);
	m_btnRepeat->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFacePage::OnRepeatClick), NULL, this);
	m_chbMirrorHorizontally->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(CaptureFacePage::OnMirrorHorizontallyCheckedChanged), NULL, this);
	m_chbCheckIcao->Disconnect(wxEVT_CHECKBOX, wxCommandEventHandler(CaptureFacePage::OnCheckIcaoComplianceCheckedChanged), NULL, this);
	m_subjectTreeControl->Disconnect(wxEVT_TREE_SELECTED_ITEM_CHANGED, wxCommandEventHandler(CaptureFacePage::OnSubjectTreePropertyChanged), NULL, this);
	this->Unbind(wxEVT_CAPTURE_FACE_THREAD, &CaptureFacePage::OnThread, this);
}

void CaptureFacePage::EnableControls()
{
	bool fromFile = m_radioImageFile->GetValue();
	bool canCancel = m_radioCamera->GetValue() || m_radioVideoFile->GetValue();
	bool isManual = m_chbManual->GetValue();
	bool isIdle = !IsBusy();
	bool isLocalCreate = (m_biometricClient.GetLocalOperations() & nboCreateTemplate) != 0;
	bool checkIcao = m_chbCheckIcao->GetValue();
	m_chbManual->Enable(!fromFile && isIdle);
	m_chbStream->Enable(!fromFile && isIdle && isLocalCreate);
	m_chbStream->SetValue(m_chbStream->GetValue() && isLocalCreate);
	m_chbCheckIcao->Enable(isIdle);
	m_radioCamera->Enable(!m_biometricClient.GetFaceCaptureDevice().IsNull() && isIdle);
	m_radioVideoFile->Enable(isIdle);
	m_radioImageFile->Enable(isIdle);
	m_btnCapture->Enable(isIdle);
	m_chbWithGeneralization->Enable(isIdle);
	m_btnCancel->Enable(!isIdle && canCancel);
	m_btnForceStart->Enable(!isIdle && ((isManual && !m_isExtractStarted) || checkIcao));
	m_btnForceEnd->Enable(false);
	m_btnCancel->Show(canCancel);
	m_btnForceEnd->Show(canCancel);
	m_btnForceStart->Show(canCancel);
	m_btnRepeat->Enable(false);
	m_btnRepeat->Show(m_chbWithGeneralization->GetValue() && canCancel);

	bool boldStart = m_btnForceStart->IsEnabled() && isManual;
	bool boldFinish = isIdle && m_newSubject.GetStatus() == nbsOk;
	wxFont font = m_btnForceStart->GetFont();
	font.SetWeight(boldStart ? wxFONTWEIGHT_BOLD : wxFONTWEIGHT_NORMAL);
	m_btnForceStart->SetFont(font);
	font = m_btnFinish->GetFont();
	font.SetWeight(boldFinish ? wxFONTWEIGHT_BOLD : wxFONTWEIGHT_NORMAL);
	m_btnForceStart->SetLabel(checkIcao && !isManual ? "Force" : "Start");
	m_btnFinish->SetFont(font);

	isIdle ? m_busyIndicator->Hide() : m_busyIndicator->Show();
	m_statusSizer->RecalcSizes();
}

void CaptureFacePage::CreateGUIControls()
{
	wxFlexGridSizer* fgSizer1;
	fgSizer1 = new wxFlexGridSizer( 0, 1, 0, 0 );
	fgSizer1->AddGrowableCol( 0 );
	fgSizer1->AddGrowableRow( 1 );
	fgSizer1->SetFlexibleDirection( wxBOTH );
	fgSizer1->SetNonFlexibleGrowMode( wxFLEX_GROWMODE_SPECIFIED );

	wxStaticBoxSizer* sbSizer1;
	sbSizer1 = new wxStaticBoxSizer( new wxStaticBox( this, wxID_ANY, wxT("Capture options") ), wxVERTICAL );

	wxGridBagSizer* gbSizer1;
	gbSizer1 = new wxGridBagSizer( 0, 0 );
	gbSizer1->SetFlexibleDirection( wxBOTH );
	gbSizer1->SetNonFlexibleGrowMode( wxFLEX_GROWMODE_SPECIFIED );

	m_radioCamera = new wxRadioButton( this, ID_RADIO_CAMERA, wxT("From camera"), wxDefaultPosition, wxDefaultSize, 0 );
	m_radioCamera->SetValue(true);
	gbSizer1->Add( m_radioCamera, wxGBPosition( 0, 0 ), wxGBSpan( 1, 1 ), wxALL, 5 );

	m_radioImageFile = new wxRadioButton( this, ID_RADIO_IMAGE, wxT("From image file"), wxDefaultPosition, wxDefaultSize, 0 );
	gbSizer1->Add( m_radioImageFile, wxGBPosition( 1, 0 ), wxGBSpan( 1, 1 ), wxALIGN_CENTER_VERTICAL|wxALL, 5 );

	m_radioVideoFile = new wxRadioButton( this, ID_RADIO_VIDEO, wxT("From video file"), wxDefaultPosition, wxDefaultSize, 0 );
	gbSizer1->Add( m_radioVideoFile, wxGBPosition( 2, 0 ), wxGBSpan( 1, 1 ), wxALIGN_CENTER_VERTICAL|wxALL, 5 );

	m_chbCheckIcao = new wxCheckBox(this, ID_CHECK_ICAO, wxT("Check ICAO Compliance"), wxDefaultPosition, wxDefaultSize, 0);
	m_chbCheckIcao->SetValue(true);
	gbSizer1->Add( m_chbCheckIcao, wxGBPosition( 0, 1 ), wxGBSpan( 1, 1 ), wxALIGN_CENTER_VERTICAL|wxALL, 5 );

	m_chbStream = new wxCheckBox( this, wxID_ANY, wxT("Stream"), wxDefaultPosition, wxDefaultSize, 0 );
	m_chbStream->SetValue(true);
	gbSizer1->Add( m_chbStream, wxGBPosition( 0, 2 ), wxGBSpan( 1, 1 ), wxALL, 5 );

	m_chbManual = new wxCheckBox( this, wxID_ANY, wxT("Manual"), wxDefaultPosition, wxDefaultSize, 0 );
	m_chbManual->SetValue(false);
	gbSizer1->Add( m_chbManual, wxGBPosition( 0, 3 ), wxGBSpan( 1, 1 ), wxALL, 5 );

	m_chbWithGeneralization = new wxCheckBox( this, wxID_ANY, wxT("With generalization"), wxDefaultPosition, wxDefaultSize, 0 );
	gbSizer1->Add( m_chbWithGeneralization, wxGBPosition( 1, 1 ), wxGBSpan( 1, 2 ), wxALL, 5 );

	m_btnCapture = new wxButton( this, wxID_ANY, wxT("Capture"), wxDefaultPosition, wxDefaultSize, 0 );
	m_btnCapture->SetFont( wxFont( wxNORMAL_FONT->GetPointSize(), 70, 90, 92, false, wxEmptyString ) );
	gbSizer1->Add( m_btnCapture, wxGBPosition( 2, 1 ), wxGBSpan( 1, 2 ), wxALL, 5 );

	sbSizer1->Add( gbSizer1, 1, wxEXPAND, 5 );

	fgSizer1->Add( sbSizer1, 1, wxEXPAND, 5 );

	wxBoxSizer * bs = new wxBoxSizer(wxHORIZONTAL);
	wxBoxSizer * bs2 = new wxBoxSizer(wxVERTICAL);

	m_icaoView = new IcaoWarningsView(this);
	bs2->Add(m_icaoView, 1, wxALL | wxEXPAND, 5);

	m_subjectTreeControl = new SubjectTreeWidget(this, ID_SUBJECT_TREE);
	m_subjectTreeControl->SetAllowNew(nbtNone);
	m_subjectTreeControl->SetAllowRemove(false);
	m_subjectTreeControl->SetShowBiometricsOnly(true);
	m_subjectTreeControl->SetMaxSize(wxSize(-1, 60));
	bs2->Add(m_subjectTreeControl, 0, wxALL | wxEXPAND, 5);

	bs->Add(bs2, 0, wxALL | wxEXPAND, 5);

	m_faceView = new wxNFaceView(this, wxID_ANY);
	m_faceView->EnableContextMenu(false);
	m_faceView->SetWindowStyle(wxSIMPLE_BORDER);
	m_faceView->SetBackgroundColour(wxNullColour);
	bs->Add(m_faceView, 1, wxALL | wxEXPAND, 5);

	fgSizer1->Add( bs, 0, wxALL|wxEXPAND, 5 );

	m_generalizationView = new GeneralizeProgressView(this, wxID_ANY);
	m_generalizationView->SetMinSize(wxSize(20, 20));
	m_generalizationView->SetView(m_faceView);
	m_generalizationView->SetIcaoView(m_icaoView);
	fgSizer1->Add( m_generalizationView, 0, wxALL|wxEXPAND, 5 );

	m_statusSizer = new wxBoxSizer(wxHORIZONTAL);
	fgSizer1->Add(m_statusSizer, 0, wxLEFT | wxRIGHT | wxEXPAND, 5);

	m_busyIndicator = new BusyIndicator( this, wxID_ANY, wxDefaultPosition, wxSize(14, 14) );
	m_busyIndicator->Hide();
	m_statusSizer->Add( m_busyIndicator, 0, wxRIGHT, 5 );

	m_statusPanel = new StatusPanel(this, wxID_ANY);
	m_statusSizer->Add( m_statusPanel, 1, wxEXPAND, 5 );

	wxFlexGridSizer* fgSizer2;
	fgSizer2 = new wxFlexGridSizer( 0, 6, 0, 0 );
	fgSizer2->AddGrowableCol( 0 );
	fgSizer2->AddGrowableCol( 5 );
	fgSizer2->SetFlexibleDirection( wxBOTH );
	fgSizer2->SetNonFlexibleGrowMode( wxFLEX_GROWMODE_SPECIFIED );

	fgSizer2->AddStretchSpacer(1);

	m_btnCancel = new wxButton( this, wxID_ANY, wxT("Cancel"), wxDefaultPosition, wxDefaultSize, 0 );
	fgSizer2->Add( m_btnCancel, 0, wxALL, 5 );

	m_btnForceStart = new wxButton( this, wxID_ANY, wxT("Start"), wxDefaultPosition, wxDefaultSize, 0 );
	fgSizer2->Add( m_btnForceStart, 0, wxALL, 5 );

	m_btnForceEnd = new wxButton( this, wxID_ANY, wxT("End"), wxDefaultPosition, wxDefaultSize, 0 );
	fgSizer2->Add( m_btnForceEnd, 0, wxALL, 5 );

	m_btnRepeat = new wxButton( this, wxID_ANY, wxT("Repeat"), wxDefaultPosition, wxDefaultSize, 0 );
	fgSizer2->Add( m_btnRepeat, 0, wxALL, 5 );

	fgSizer2->AddStretchSpacer(1);

	fgSizer1->Add( fgSizer2, 1, wxEXPAND, 5 );

	wxFlexGridSizer * fgSizer3 = new wxFlexGridSizer(0, 4, 0, 0);
	fgSizer3->AddGrowableCol(1);

	m_chbMirrorHorizontally = new wxCheckBox(this, wxID_ANY, wxT("Mirror view"));
	fgSizer3->Add(m_chbMirrorHorizontally, 0, wxALL, 5);

	m_zoomSlider = new wxNViewZoomSlider(this);
	m_zoomSlider->SetView(m_faceView);
	fgSizer3->Add(m_zoomSlider, 0, wxALL);

	fgSizer3->AddStretchSpacer(1);

	m_btnFinish = new wxButton( this, wxID_ANY, wxT("Finish"), wxDefaultPosition, wxDefaultSize, 0 );
	fgSizer3->Add( m_btnFinish, 0, wxALL, 5 );

	fgSizer1->Add(fgSizer3, 1, wxEXPAND, 5);

	this->SetSizer( fgSizer1 );
	this->Layout();
}

}}


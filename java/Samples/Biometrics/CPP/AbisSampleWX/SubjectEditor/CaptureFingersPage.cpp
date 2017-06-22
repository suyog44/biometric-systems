#include "Precompiled.h"

#include <SubjectEditor/CaptureFingersPage.h>
#include <Common/SubjectUtils.h>
#include <Settings/SettingsManager.h>

#include <Resources/OpenFolderIcon.xpm>
#include <Resources/NextIcon.xpm>
#include <Resources/RepeatIcon.xpm>

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Devices;
using namespace Neurotec::Images;
using namespace Neurotec::Gui;

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(ID_EVENT_BIOMETRIC_STATUS_CHANGED, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_CAPTURE_FINGERS_THREAD, wxCommandEvent);

CaptureFingersPage::CaptureFingersPage(NBiometricClient& biometricClient, NSubject& subject, SubjectEditorPageInterface& subjectEditorPageInterface, wxWindow *parent, wxWindowID winid) :
	ModalityPage(biometricClient, subject, subjectEditorPageInterface, parent, winid),
	m_captureNeedsAction(false),
	m_titlePrefix(wxEmptyString),
	m_sessionId(-1),
	m_newSubject(NULL),
	m_currentBiometric(NULL)
{
	m_supportedScenarios = Scenario::GetAvailableScenarios();

	CreateGUIControls();
	RegisterGuiEvents();
}

CaptureFingersPage::~CaptureFingersPage()
{
	UnregisterGuiEvents();
	m_horizontalSlider->SetView(NULL);
	delete m_timer;
}

void CaptureFingersPage::SetIsBusy(bool value)
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

void CaptureFingersPage::OnNavigatedTo()
{
	m_sessionId = 0;
	for (int i = 0; i < m_subject.GetFingers().GetCount(); i++)
	{
		m_sessionId = std::max(m_sessionId, m_subject.GetFingers()[i].GetSessionId());
	}

	m_biometricClient.SetCurrentBiometricCompletedTimeout(-1);
	m_biometricClient.AddCurrentBiometricCompletedCallback(&CaptureFingersPage::BiometricClientCurrentBiometricCompletedCallback, this);
	m_biometricClient.AddPropertyChangedCallback(&CaptureFingersPage::BiometricClientPropertyChangedCallback, this);

	m_newSubject = NSubject();
	CopyMissingFingerPositions(m_newSubject, m_subject);
	m_fingerTreeWidget->SetSubject(m_newSubject);

	NSubject::MissingFingerCollection missing = m_newSubject.GetMissingFingers();
	std::vector<NFPosition> missingPositions(missing.begin(), missing.end());
	m_fingerSelector->SetMissingPositions(missingPositions);

	m_statusPanel->SetMessage(wxEmptyString);
	m_statusPanel->Show(false);
	m_chbShowBinarizedImage->Show(m_biometricClient.GetFingersReturnBinarizedImage());
	m_generalizeProgressView->Show(false);
	OnFingerScannerChanged();
	OnRadioButtonToggle();

	ModalityPage::OnNavigatedTo();
}

void CaptureFingersPage::OnNavigatingFrom()
{
	Cancel();

	m_timer->Stop();
	m_nowCapturing.clear();
	m_biometricClient.RemoveCurrentBiometricCompletedCallback(&CaptureFingersPage::BiometricClientCurrentBiometricCompletedCallback, this);
	m_biometricClient.RemovePropertyChangedCallback(&CaptureFingersPage::BiometricClientPropertyChangedCallback, this);
	m_biometricClient.SetCurrentBiometricCompletedTimeout(0);

	UpdateMissingPositions();

	NArrayWrapper<NFinger> fingers = m_newSubject.GetFingers().GetAll();
	m_newSubject.Clear();
	for (NArrayWrapper<NFinger>::iterator it = fingers.begin(); it != fingers.end(); it++)
	{
		m_subject.GetFingers().Add(*it);
	}
	CopyMissingFingerPositions(m_subject, m_newSubject);
	m_newSubject = NULL;
	m_fingerTreeWidget->SetSubject(m_newSubject);
	m_fingerView->Clear();

	ModalityPage::OnNavigatingFrom();
}

CaptureFingersPage::Scenario * CaptureFingersPage::GetCurrentScenario()
{
	int selection = m_choiceScenario->GetSelection();
	if (selection != -1)
	{
		return reinterpret_cast<Scenario *>(m_choiceScenario->GetClientData(selection));
	}
	return NULL;
}

void CaptureFingersPage::EnableControls()
{
	bool fromFile = m_radioFile->GetValue();
	bool isBusy = IsBusy();
	NFScanner scanner = m_biometricClient.GetFingerScanner();
	Scenario * selected = GetCurrentScenario();
	if (!selected) selected = &m_supportedScenarios[0];

	m_btnStart->Show(!fromFile);
	m_btnOpenFile->Enable(fromFile && !isBusy);
	m_btnSkip->Enable(!fromFile && !selected->IsUnknownFingers() && isBusy);
	m_btnRepeat->Enable(!fromFile && isBusy);
	m_btnStart->Enable(!fromFile && !isBusy);
	m_btnCancel->Enable(!fromFile && isBusy);
	m_fingerSelector->Enable(!isBusy);
	m_choiceScenario->Enable(!isBusy);
	m_choiceImpression->Enable(!isBusy && fromFile);
	m_fingerTreeWidget->Enable(!isBusy);
	m_btnRepeat->Show(!fromFile);
	m_btnSkip->Show(!fromFile);
	m_btnForce->Show(!fromFile);
	m_btnCancel->Show(!fromFile);
	m_btnOpenFile->Show(fromFile);
	m_chbCaptureAutomatically->Enable(!fromFile && !isBusy);
	m_chbWithGeneralization->Enable(!isBusy);
	m_btnForce->Enable(isBusy && !m_chbCaptureAutomatically->GetValue());

	m_radioScanner->Enable(!scanner.IsNull() && scanner.IsAvailable() && !isBusy);
	m_radioFile->Enable(!isBusy);
	m_chbShowBinarizedImage->Enable(m_chbShowBinarizedImage->IsEnabled() && !isBusy);

	isBusy ? m_busyIndicator->Show() : m_busyIndicator->Hide();
	m_statusSizer->RecalcSizes();

	PostSizeEvent();
	Layout();
	Refresh();
}

void CaptureFingersPage::UpdateShowBinarized()
{
	NFinger finger = m_fingerView->GetFinger();
	m_chbShowBinarizedImage->Enable(!finger.IsNull() && !finger.GetBinarizedImage().IsNull());
}

void CaptureFingersPage::OnFingerScannerChanged()
{
	NFScanner device = m_biometricClient.GetFingerScanner();
	if (device.IsNull() || !device.IsAvailable())
	{
		if (m_radioScanner->GetValue())
		{
			m_radioFile->SetValue(true);
			OnRadioButtonToggle();
		}
		m_radioScanner->SetLabel(wxT("Scanner (Not connected)"));
	}
	else
	{
		try
		{
			wxString displayName = device.GetDisplayName();
			m_radioScanner->SetLabel(wxString::Format(wxT("Scanner (%s)"), displayName.c_str()));
		}
		catch(NError & error)
		{
			wxExceptionDlg::Show(error);
		}
	}
	EnableControls();
}

void CaptureFingersPage::OnRadioButtonToggle()
{
	ListSupportedScenarios();
	m_fingerSelector->SetAllowOnlyAmputateAction(m_radioScanner->GetValue());
	EnableControls();
	ShowHint();

}

void CaptureFingersPage::ShowHint()
{
	m_lblHint->Show(m_fingerSelector->IsShown());
	if (m_radioFile->GetValue())
	{
		m_lblHint->SetLabel(wxT("Hint: Click on finger to select it or mark as missing"));
	}
	else
	{
		m_lblHint->SetLabel(wxT("Hint: Click on finger to mark it as missing"));
	}
}

void CaptureFingersPage::ListSupportedScenarios()
{
	Scenario * scenario = GetCurrentScenario();
	bool supportsRolled = true;
	bool supportsSlaps = true;

	if (!m_radioFile->GetValue())
	{
		supportsRolled = false;
		supportsSlaps = false;
		try
		{
			NFScanner scanner = m_biometricClient.GetFingerScanner();
			NArrayWrapper<NFImpressionType> impressions = scanner.GetSupportedImpressionTypes();
			NArrayWrapper<NFPosition> positions = scanner.GetSupportedPositions();

			for (NArrayWrapper<NFImpressionType>::iterator it = impressions.begin(); it != impressions.end(); it++)
			{
				if (NBiometricTypes::IsImpressionTypeRolled(*it))
				{
					supportsRolled = true;
					break;
				}
			}
			for (NArrayWrapper<NFPosition>::iterator it = positions.begin(); it != positions.end(); it++)
			{
				if (NBiometricTypes::IsPositionFourFingers(*it))
				{
					supportsSlaps = true;
					break;
				}
			}
		}
		catch(NError & error)
		{
			wxExceptionDlg::Show(error);
		}
	}

	m_choiceScenario->Clear();
	for (std::vector<Scenario>::iterator it = m_supportedScenarios.begin(); it != m_supportedScenarios.end(); it++)
	{
		if (!supportsSlaps && it->HasSlaps()) continue;
		if (!supportsRolled && it->HasRolled()) continue;

		m_choiceScenario->Append(it->GetName(), &*it);
		if (scenario && scenario->GetName() == it->GetName())
			m_choiceScenario->SetStringSelection(it->GetName());
	}

	if (m_choiceScenario->GetSelection() == -1)
	{
		m_choiceScenario->SetSelection(0);
	}

	wxCommandEvent empty;
	OnScenarioSelected(empty);
	EnableControls();

}

void CaptureFingersPage::UpdateMissingPositions()
{
	if (!m_subject.IsNull())
	{
		std::vector<NFPosition> missing = m_fingerSelector->GetMissingPositions();
		m_subject.GetMissingFingers().Clear();
		for (std::vector<NFPosition>::iterator it = missing.begin(); it != missing.end(); it++)
		{
			m_subject.GetMissingFingers().Add(*it);
		}
	}
}

void CaptureFingersPage::CopyMissingFingerPositions(NSubject & dst, NSubject & src)
{
	dst.GetMissingFingers().Clear();
	NArrayWrapper<NFPosition> missing = src.GetMissingFingers().GetAll();
	for (NArrayWrapper<NFPosition>::iterator it = missing.begin(); it != missing.end(); it++)
	{
		dst.GetMissingFingers().Add(*it);
	}
}

void CaptureFingersPage::OnTimerTick(wxTimerEvent &)
{
	if (IsPageShown() && m_captureNeedsAction)
	{
		m_captureNeedsAction = false;
		m_biometricClient.Repeat();
	}
}

void CaptureFingersPage::OnFingerSelectorFingerClick(wxCommandEvent & event)
{
	OnSelectedPositionChanged(GetCurrentScenario(), (NFPosition)event.GetInt());
}

void CaptureFingersPage::OnCurrentBiometricChanged(NFinger & current)
{
	if (!m_currentBiometric.IsNull())
	{
		m_currentBiometric.RemovePropertyChangedCallback(&CaptureFingersPage::BiometricPropertyChangedCallback, this);
		m_currentBiometric = NULL;
	}
	if (!current.IsNull())
	{
		bool withGeneralization = m_chbWithGeneralization->GetValue();
		Node * node = m_fingerTreeWidget->GetBiometricNode(current);
		OnSelectedPositionChanged(GetCurrentScenario(), current.GetPosition());
		m_fingerTreeWidget->SetSelectedItem(node);
		if (withGeneralization)
		{
			std::vector<NBiometric> items = node->GetItems();
			std::vector<NBiometric> generalized = node->GetAllGeneralized();
			wxString positionString = NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), current.GetPosition());
			m_generalizeProgressView->SetBiometrics(items);
			m_generalizeProgressView->SetGeneralized(generalized);
			m_generalizeProgressView->SetSelected(current);
			m_generalizeProgressView->Show();

			std::vector<NBiometric>::iterator f = std::find(items.begin(), items.end(), current);
			int index = std::distance(items.begin(), f);
			m_titlePrefix = wxString::Format("Capturing %s (%d of %d). ", positionString.c_str(), index + 1, SettingsManager::GetFingersGeneralizationRecordCount());
		}
		m_fingerView->SetFinger(current);
		m_currentBiometric = current;
		m_currentBiometric.AddPropertyChangedCallback(&CaptureFingersPage::BiometricPropertyChangedCallback, this);
	}
	else
	{
		m_generalizeProgressView->Clear();
		m_statusPanel->Hide();
		m_titlePrefix = wxEmptyString;
	}
}

void CaptureFingersPage::OnCaptureCompleted(NAsyncOperation & operation)
{
	try
	{
		NBiometricStatus status = nbsInternalError;
		NValue value = operation.GetResult();
		NBiometricTask task = NObjectDynamicCast<NBiometricTask>(value.ToObject(NBiometricTask::NativeTypeOf()));
		NError exception = task.GetError();
		if (!exception.IsNull()) wxExceptionDlg::Show(exception);

		status = task.GetStatus();
		if (status != nbsOk)
		{
			std::vector<FingersGeneralizationGroup> groups = SubjectUtils::GetFingersGeneralizationGroups(m_nowCapturing);
			for (std::vector<FingersGeneralizationGroup>::iterator git = groups.begin(); git != groups.end(); git++)
			{
				bool failed = false;
				for (FingersGeneralizationGroup::iterator it = git->begin(); it != git->end(); it++)
				{
					if (it->GetStatus() != nbsOk)
					{
						failed = true;
						break;
					}
				}
				if (failed)
				{
					std::vector<NFinger> flattened = SubjectUtils::FlattenFingers(*git);
					for (std::vector<NFinger>::iterator it = flattened.begin(); it != flattened.end(); it++)
					{
						NInt index = m_newSubject.GetFingers().IndexOf(*it);
						if (index != -1) m_newSubject.GetFingers().RemoveAt(index);
					}
				}
			}
		}

		m_generalizeProgressView->SetEnableMouseSelection(true);
		if (status == nbsOk)
		{
			m_statusPanel->SetMessage(wxT("Fingers captured successfully"), StatusPanel::SUCCESS_MESSAGE);
			m_statusPanel->Show();
		}
		else
		{
			if (status == nbsCanceled)
				m_statusPanel->Hide();
			else
			{
				wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
				m_statusPanel->SetMessage(wxString::Format(wxT("Capture failed: %s"), statusString.c_str()), StatusPanel::ERROR_MESSAGE);
				m_statusPanel->Show();
			}
			m_fingerSelector->SetSelection(nfpUnknown);
		}

		m_fingerTreeWidget->UpdateTree();
		Node * selected = m_fingerTreeWidget->GetSelectedItem();
		if (selected && selected->IsGeneralizedNode())
		{
			std::vector<NBiometric> generalized = selected->GetAllGeneralized();
			std::vector<NBiometric> items = selected->GetItems();
			m_generalizeProgressView->SetBiometrics(items);
			m_generalizeProgressView->SetGeneralized(generalized);
			m_generalizeProgressView->SetSelected(generalized.empty() ? items[0] : generalized[0]);
			m_generalizeProgressView->Show(true);
		}
		else
		{
			m_generalizeProgressView->Clear();
			m_generalizeProgressView->Show(false);
			if (selected)
			{
				NBiometric first = selected->GetItems()[0];
				NFinger finger = NObjectDynamicCast<NFinger>(first);
				m_fingerView->SetFinger(finger);
			}
		}
	}
	catch(NError & error)
	{
		wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), nbsInternalError);
		m_statusPanel->SetMessage(wxString::Format(wxT("Capture failed: %s"), statusString.c_str()), StatusPanel::ERROR_MESSAGE);
		wxExceptionDlg::Show(error);
	}

	SetIsBusy(false);

	m_nowCapturing.clear();
	UpdateShowBinarized();
	EnableControls();
}

void CaptureFingersPage::OnCreateTemplateCompleted(NAsyncOperation & operation)
{
	try
	{
		NValue result = operation.GetResult();
		NBiometricTask task = NObjectDynamicCast<NBiometricTask>(result.ToObject(NBiometricTask::NativeTypeOf()));
		NError exception = task.GetError();
		if (!exception.IsNull()) wxExceptionDlg::Show(exception);

		NBiometricStatus status = task.GetStatus();
		if (status != nbsOk)
		{
			std::vector<NFinger> allItems = SubjectUtils::FlattenFingers(m_nowCapturing);
			for (std::vector<NFinger>::iterator it = allItems.begin(); it != allItems.end(); it++)
			{
				NInt index = m_newSubject.GetFingers().IndexOf(*it);
				if (index != -1) m_newSubject.GetFingers().RemoveAt(index);
			}
		}

		m_fingerTreeWidget->UpdateTree();
		m_generalizeProgressView->SetEnableMouseSelection(true);
		if (status != nbsOk)
		{
			wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
			m_fingerTreeWidget->SetSelectedItem(NULL);
			m_fingerView->SetFinger(m_nowCapturing[0]);
			m_statusPanel->SetMessage(wxString::Format(wxT("Extraction failed: %s"), statusString.c_str()), StatusPanel::ERROR_MESSAGE);
		}
		else
		{
			m_statusPanel->SetMessage(wxT("Finger extraction completed successfully"), StatusPanel::SUCCESS_MESSAGE);
			if (m_chbWithGeneralization->GetValue())
			{
				Node * node = m_fingerTreeWidget->GetBiometricNode(m_nowCapturing[0]);
				if (node)
				{
					std::vector<NBiometric> generalized = node->GetAllGeneralized();
					m_generalizeProgressView->SetGeneralized(generalized);
					m_generalizeProgressView->SetSelected(generalized[0]);
				}
			}
		}
		m_statusPanel->Show();
	}
	catch (NError & error)
	{
		wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), nbsInternalError);
		m_statusPanel->SetMessage(wxString::Format(wxT("Extraction failed: %s"), statusString.c_str()), StatusPanel::ERROR_MESSAGE);
		wxExceptionDlg::Show(error);
	}

	SetIsBusy(false);
	m_nowCapturing.clear();
	UpdateShowBinarized();
	EnableControls();
}

void CaptureFingersPage::OnThread(wxCommandEvent &event)
{
	int id = event.GetId();
	switch(id)
	{
	case ID_EVENT_BIOMETRIC_STATUS_CHANGED:
		{
			NBiometricStatus status = (NBiometricStatus)event.GetInt();
			wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
			wxString message = wxString::Format(wxT("%sStatus: %s"), m_titlePrefix.c_str(), statusString.c_str());
			m_statusPanel->SetMessage(message, status == nbsOk || status == nbsNone ? StatusPanel::SUCCESS_MESSAGE : StatusPanel::ERROR_MESSAGE);
			break;
		}
	case ID_EVENT_REPEAT_CAPTURE:
		{
			NBiometric biometric = m_biometricClient.GetCurrentBiometric();
			wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), biometric.GetStatus());
			m_captureNeedsAction = true;
			m_statusPanel->SetMessage(wxString::Format(wxT("Capturing failed: %s. Trying again ..."), statusString.c_str()));
			m_timer->StartOnce(3000);
			break;
		}
	case ID_EVENT_CAPTURE_COMPLETED:
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			OnCaptureCompleted(operation);
			break;
		}
	case ID_EVENT_CREATE_TEMPLATE_COMPLETED:
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			OnCreateTemplateCompleted(operation);
			break;
		}
	case ID_EVENT_BIOMETRIC_CHANGED:
		{
			NFinger current(static_cast<HNObject>(event.GetClientData()), true);
			OnCurrentBiometricChanged(current);
			break;
		}
	case ID_EVENT_SCANNER_CHANGED:
		{
			OnFingerScannerChanged();
			break;
		}
	default:
		break;
	};
}

void CaptureFingersPage::BiometricClientPropertyChangedCallback(NObject::PropertyChangedEventArgs args)
{
	wxString propertyName = args.GetPropertyName();
	CaptureFingersPage *page = reinterpret_cast<CaptureFingersPage *>(args.GetParam());
	if (propertyName == wxT("CurrentBiometric"))
	{
		NBiometric current = page->m_biometricClient.GetCurrentBiometric();
		wxCommandEvent event(wxEVT_CAPTURE_FINGERS_THREAD, ID_EVENT_BIOMETRIC_CHANGED);
		event.SetClientData(current.IsNull()? NULL : current.RefHandle());
		wxPostEvent(page, event);
	}
	else if (propertyName == "FingerScanner")
	{
		wxPostEvent(page, wxCommandEvent(wxEVT_CAPTURE_FINGERS_THREAD, ID_EVENT_SCANNER_CHANGED));
	}
}

void CaptureFingersPage::BiometricClientCurrentBiometricCompletedCallback(EventArgs args)
{
	CaptureFingersPage *page = reinterpret_cast<CaptureFingersPage *>(args.GetParam());
	NBiometricClient biometricClient = args.GetObject<NBiometricClient>();
	NBiometric currentBiometric = biometricClient.GetCurrentBiometric();
	NBiometricStatus status = currentBiometric.GetStatus();
	if (status == nbsOk || status == nbsSpoofDetected || status == nbsSourceError || status == nbsCaptureError)
	{
		biometricClient.Force();
	}
	else
	{
		wxCommandEvent evt(wxEVT_CAPTURE_FINGERS_THREAD, ID_EVENT_REPEAT_CAPTURE);
		wxPostEvent(page, evt);
	}
}

void CaptureFingersPage::BiometricPropertyChangedCallback(NObject::PropertyChangedEventArgs args)
{
	CaptureFingersPage *page = reinterpret_cast<CaptureFingersPage *>(args.GetParam());
	NBiometric biometric = args.GetObject<NBiometric>();
	wxString propertyName = args.GetPropertyName();
	if (propertyName == wxT("Status"))
	{
		wxCommandEvent evt(wxEVT_CAPTURE_FINGERS_THREAD, ID_EVENT_BIOMETRIC_STATUS_CHANGED);
		evt.SetInt(biometric.GetStatus());
		wxPostEvent(page, evt);
	}
}

void CaptureFingersPage::CaptureAsyncCompletedCallback(EventArgs args)
{
	CaptureFingersPage *page = static_cast<CaptureFingersPage *>(args.GetParam());
	wxCommandEvent event(wxEVT_CAPTURE_FINGERS_THREAD, ID_EVENT_CAPTURE_COMPLETED);
	event.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
	wxPostEvent(page, event);
}

void CaptureFingersPage::CreateTemplateAsyncCompletedCallback(EventArgs args)
{
	CaptureFingersPage *page = static_cast<CaptureFingersPage *>(args.GetParam());
	wxCommandEvent event(wxEVT_CAPTURE_FINGERS_THREAD, ID_EVENT_CREATE_TEMPLATE_COMPLETED);
	event.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
	wxPostEvent(page, event);
}

void CaptureFingersPage::OnFinishClick(wxCommandEvent&)
{
	SelectFirstPage();
}

void CaptureFingersPage::OnRepeatClick(wxCommandEvent&)
{
	m_captureNeedsAction = false;
	m_biometricClient.Repeat();
}

void CaptureFingersPage::OnSkipClick(wxCommandEvent&)
{
	m_captureNeedsAction = false;
	m_biometricClient.Skip();
}

void CaptureFingersPage::OnCancelClick(wxCommandEvent&)
{
	m_captureNeedsAction = false;
	m_biometricClient.Cancel();
	m_btnCancel->Enable(false);
}

void CaptureFingersPage::OnForceClick(wxCommandEvent&)
{
	m_biometricClient.Force();
	m_btnForce->Enable(false);
}

void CaptureFingersPage::OnOpenFileClick(wxCommandEvent&)
{
	UpdateMissingPositions();

	bool generalize = m_chbWithGeneralization->GetValue();
	int sessionId = generalize ? m_sessionId++ : -1;
	std::vector<wxString> files;
	int count = generalize ? SettingsManager::GetFingersGeneralizationRecordCount() : 1;

	m_nowCapturing.clear();
	m_generalizeProgressView->Clear();

	while (count > (int)files.size())
	{
		wxString title = generalize ? wxString::Format(wxT("Open image (%d of %d)"), files.size() + 1, count) : wxT("Open image");
		wxFileDialog openFileDialog(this, title, wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, true), wxFD_OPEN | wxFD_FILE_MUST_EXIST);
		if (openFileDialog.ShowModal() != wxID_OK) return;
		files.push_back(openFileDialog.GetPath());
	}

	NFImpressionType impression = (NFImpressionType)NEnum::Parse(NBiometricTypes::NFImpressionTypeNativeTypeOf(), m_choiceImpression->GetStringSelection());
	for (std::vector<wxString>::iterator it = files.begin(); it != files.end(); it++)
	{
		NFinger finger;
		finger.SetSessionId(sessionId);
		finger.SetFileName(*it);
		finger.SetPosition(m_fingerSelector->GetSelection());
		finger.SetImpressionType(impression);
		m_newSubject.GetFingers().Add(finger);
		m_nowCapturing.push_back(finger);
	}

	NFinger first = m_nowCapturing[0];
	if (generalize)
	{
		m_generalizeProgressView->SetBiometrics(m_nowCapturing);
		m_generalizeProgressView->SetSelected(first);
	}
	m_fingerView->SetFinger(first);
	m_fingerTreeWidget->UpdateTree();
	m_fingerTreeWidget->SetSelectedItem(m_fingerTreeWidget->GetBiometricNode(first));

	NBiometricOperations operations = nboCreateTemplate;
	if (m_biometricClient.GetFingersCalculateNfiq()) operations = (NBiometricOperations)(operations | nboAssessQuality);
	NBiometricTask biometricTask = m_biometricClient.CreateTask(operations, m_newSubject);
	NAsyncOperation operation = m_biometricClient.PerformTaskAsync(biometricTask);
	SetIsBusy(true);
	operation.AddCompletedCallback(&CaptureFingersPage::CreateTemplateAsyncCompletedCallback, this);
	m_statusPanel->SetMessage(wxT("Extracting template. Please wait ..."));
	m_statusPanel->Show(true);
	EnableControls();
}

void CaptureFingersPage::OnStartCapturingClick(wxCommandEvent&)
{
	UpdateMissingPositions();
	if (m_radioScanner->GetValue())
	{
		bool generalize = m_chbWithGeneralization->GetValue();
		int sessionId = generalize ? m_sessionId++ : -1;
		int count = generalize ? SettingsManager::GetFingersGeneralizationRecordCount() : 1;
		bool manual = !m_chbCaptureAutomatically->GetValue();

		m_nowCapturing.clear();
		m_generalizeProgressView->Clear();
		m_generalizeProgressView->SetEnableMouseSelection(false);

		Scenario * sc = GetCurrentScenario();
		std::vector<NFinger> fingers = sc->GetFingers(sessionId, count);
		NSubject::MissingFingerCollection missingFingers = m_newSubject.GetMissingFingers();
		for (std::vector<NFinger>::iterator it = fingers.begin(); it != fingers.end(); it++)
		{
			if (!missingFingers.Contains(it->GetPosition()))
			{
				if (manual) it->SetCaptureOptions(nbcoManual);
				m_nowCapturing.push_back(*it);
				m_newSubject.GetFingers().Add(*it);
			}
		}

		NBiometricOperations operations = (NBiometricOperations)(nboCapture | nboCreateTemplate);
		if (m_biometricClient.GetFingersCalculateNfiq()) operations = (NBiometricOperations)(operations | nboAssessQuality);
		NBiometricTask biometricTask = m_biometricClient.CreateTask(operations, m_newSubject);
		NAsyncOperation operation = m_biometricClient.PerformTaskAsync(biometricTask);
		SetIsBusy(true);
		operation.AddCompletedCallback(&CaptureFingersPage::CaptureAsyncCompletedCallback, this);
		m_statusPanel->SetMessage(wxT("Starting capturing from scanner ..."));
		m_statusPanel->Show();
		EnableControls();
	}
}

void CaptureFingersPage::OnSourceSelected(wxCommandEvent&)
{
	OnRadioButtonToggle();
}

void CaptureFingersPage::OnScenarioSelected(wxCommandEvent&)
{
	Scenario * selected = GetCurrentScenario();
	m_fingerSelector->SetSelection(nfpUnknown);
	m_fingerSelector->Show(!selected->IsUnknownFingers());
	std::vector<NFPosition> allowedPositions = selected->GetPositions();
	m_fingerSelector->SetAllowedPositions(allowedPositions);
	if (m_radioFile->GetValue())
	{
		NFPosition position = allowedPositions[0];
		OnSelectedPositionChanged(selected, position);
	}
	else
	{
		UpdateImpressionTypes(nfpUnknown, false);
	}
	ShowHint();
	EnableControls();
}

void CaptureFingersPage::OnGeneralizeProgressViewSelectionChanged(wxCommandEvent&)
{
	UpdateShowBinarized();
}

void CaptureFingersPage::OnFingerTreeSelectionChanged(wxCommandEvent &)
{
	Node * selection = m_fingerTreeWidget->GetSelectedItem();
	NFinger first = NULL;
	std::vector<NBiometric> items;
	if (selection)
	{
		items = selection->GetItems();
		first = NObjectDynamicCast<NFinger>(items[0]);
	}

	NFPosition position = !first.IsNull() ? first.GetPosition() : nfpUnknown;
	NFImpressionType impression = !first.IsNull() ? first.GetImpressionType() : nfitLiveScanPlain;
	m_generalizeProgressView->Clear();
	if (selection && selection->IsGeneralizedNode())
	{
		std::vector<NBiometric> generalized = selection->GetAllGeneralized();
		m_generalizeProgressView->SetBiometrics(items);
		m_generalizeProgressView->SetGeneralized(generalized);
		m_generalizeProgressView->SetSelected(!generalized.empty() ? generalized[0] : items[0]);
		m_generalizeProgressView->Show();
	}
	else
		m_generalizeProgressView->Hide();

	m_fingerView->SetFinger(first);
	OnSelectedPositionChanged(GetCurrentScenario(), position, NBiometricTypes::IsImpressionTypeRolled(impression));
	m_statusPanel->Show(m_statusPanel->IsShown() && IsBusy());
	UpdateShowBinarized();
}

void CaptureFingersPage::UpdateImpressionTypes(NFPosition position, bool isRolled)
{
	std::vector<NFImpressionType> impressions;
	if (m_radioFile->GetValue())
	{
		NArrayWrapper<NInt> values = NEnum::GetValues(NBiometricTypes::NFImpressionTypeNativeTypeOf());
		for (NArrayWrapper<NInt>::iterator it = values.begin(); it != values.end(); it++)
		{
			impressions.push_back((NFImpressionType)*it);
		}
	}
	else
	{
		try
		{
			NFScanner scanner = m_biometricClient.GetFingerScanner();
			NArrayWrapper<NFImpressionType> values = scanner.GetSupportedImpressionTypes();
			impressions = std::vector<NFImpressionType>(values.begin(), values.end());
		}
		catch(NError & error)
		{
			wxExceptionDlg::Show(error);
		}
	}

	m_choiceImpression->Clear();
	for (std::vector<NFImpressionType>::iterator it = impressions.begin(); it != impressions.end(); it++)
	{
		NFImpressionType value = *it;
		if (NBiometricTypes::IsImpressionTypeRolled(value) == isRolled &&
			NBiometricTypes::IsPositionCompatibleWith(position, value))
		{
			m_choiceImpression->Append(NEnum::ToString(NBiometricTypes::NFImpressionTypeNativeTypeOf(), value));
		}
	}

	if (m_choiceImpression->GetCount() > 0)
	{
		m_choiceImpression->Select(0);
	}
}

void CaptureFingersPage::OnSelectedPositionChanged(Scenario * scenario, NFPosition position)
{
	bool isRolled = scenario->HasRolled() && NBiometricTypes::IsPositionTheFinger(position);
	OnSelectedPositionChanged(scenario, position, isRolled);
}

void CaptureFingersPage::OnSelectedPositionChanged(Scenario * scenario, NFPosition position, bool isRolled)
{
	if (position == nfpUnknown && m_radioFile->GetValue())
	{
		position = scenario->GetPositions()[0];
		isRolled = scenario->HasRolled() && NBiometricTypes::IsPositionTheFinger(position);
	}
	m_fingerSelector->SetSelection(position);
	UpdateImpressionTypes(position, isRolled && scenario->HasRolled());
}

void CaptureFingersPage::OnShowProcessedImageClick(wxCommandEvent&)
{
	m_fingerView->SetShownImage(m_chbShowBinarizedImage->GetValue() ? wxNFingerView::PROCESSED_IMAGE : wxNFingerView::ORIGINAL_IMAGE);
}

void CaptureFingersPage::RegisterGuiEvents()
{
	this->Bind(wxEVT_CAPTURE_FINGERS_THREAD, &CaptureFingersPage::OnThread, this);
	m_radioFile->Connect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureFingersPage::OnSourceSelected), NULL, this);
	m_radioScanner->Connect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureFingersPage::OnSourceSelected), NULL, this);
	m_radioTenPrintCard->Connect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureFingersPage::OnSourceSelected), NULL, this);
	m_choiceScenario->Connect(wxEVT_COMMAND_CHOICE_SELECTED, wxCommandEventHandler(CaptureFingersPage::OnScenarioSelected), NULL, this);
	m_btnFinish->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFingersPage::OnFinishClick), NULL, this);
	m_btnRepeat->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFingersPage::OnRepeatClick), NULL, this);
	m_btnSkip->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFingersPage::OnSkipClick), NULL, this);
	m_btnCancel->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFingersPage::OnCancelClick), NULL, this);
	m_btnForce->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFingersPage::OnForceClick), NULL, this);
	m_btnOpenFile->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFingersPage::OnOpenFileClick), NULL, this);
	m_btnStart->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFingersPage::OnStartCapturingClick), NULL, this);
	m_chbShowBinarizedImage->Connect(wxEVT_COMMAND_CHECKBOX_CLICKED, wxCommandEventHandler(CaptureFingersPage::OnShowProcessedImageClick), NULL, this);
	m_generalizeProgressView->Connect(wxEVT_GEN_SELECTED_ITEM_CHANGED, wxCommandEventHandler(CaptureFingersPage::OnGeneralizeProgressViewSelectionChanged), NULL, this);
	m_fingerTreeWidget->Connect(wxEVT_TREE_SELECTED_ITEM_CHANGED, wxCommandEventHandler(CaptureFingersPage::OnFingerTreeSelectionChanged), NULL, this);
	m_fingerSelector->Connect(wxEVT_FINGER_SELECTOR_FINGER_SELECTED, wxCommandEventHandler(CaptureFingersPage::OnFingerSelectorFingerClick), NULL, this);
	this->Connect(wxEVT_TIMER, wxTimerEventHandler(CaptureFingersPage::OnTimerTick), NULL, this);
}

void CaptureFingersPage::UnregisterGuiEvents()
{
	m_radioFile->Disconnect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureFingersPage::OnSourceSelected), NULL, this);
	m_radioScanner->Disconnect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureFingersPage::OnSourceSelected), NULL, this);
	m_radioTenPrintCard->Disconnect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureFingersPage::OnSourceSelected), NULL, this);
	m_choiceScenario->Disconnect(wxEVT_COMMAND_CHOICE_SELECTED, wxCommandEventHandler(CaptureFingersPage::OnScenarioSelected), NULL, this);
	m_btnFinish->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFingersPage::OnFinishClick), NULL, this);
	m_btnRepeat->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFingersPage::OnRepeatClick), NULL, this);
	m_btnSkip->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFingersPage::OnSkipClick), NULL, this);
	m_btnCancel->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFingersPage::OnCancelClick), NULL, this);
	m_btnForce->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFingersPage::OnForceClick), NULL, this);
	m_btnOpenFile->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFingersPage::OnOpenFileClick), NULL, this);
	m_btnStart->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureFingersPage::OnStartCapturingClick), NULL, this);
	m_chbShowBinarizedImage->Disconnect(wxEVT_COMMAND_CHECKBOX_CLICKED, wxCommandEventHandler(CaptureFingersPage::OnShowProcessedImageClick), NULL, this);
	m_generalizeProgressView->Disconnect(wxEVT_GEN_SELECTED_ITEM_CHANGED, wxCommandEventHandler(CaptureFingersPage::OnGeneralizeProgressViewSelectionChanged), NULL, this);
	m_fingerTreeWidget->Disconnect(wxEVT_TREE_SELECTED_ITEM_CHANGED, wxCommandEventHandler(CaptureFingersPage::OnFingerTreeSelectionChanged), NULL, this);
	m_fingerSelector->Disconnect(wxEVT_FINGER_SELECTOR_FINGER_SELECTED, wxCommandEventHandler(CaptureFingersPage::OnFingerSelectorFingerClick), NULL, this);
	this->Unbind(wxEVT_CAPTURE_FINGERS_THREAD, &CaptureFingersPage::OnThread, this);
	this->Disconnect(wxEVT_TIMER, wxTimerEventHandler(CaptureFingersPage::OnTimerTick), NULL, this);
}

void CaptureFingersPage::CreateGUIControls()
{
	wxBoxSizer *sizer = new wxBoxSizer(wxHORIZONTAL);
	this->SetSizer(sizer, true);

	wxBoxSizer *szBox = new wxBoxSizer(wxVERTICAL);
	sizer->Add(szBox, 0, wxALL | wxEXPAND, 5);

	wxStaticBoxSizer *szStaticBox = new wxStaticBoxSizer(new wxStaticBox(this, wxID_ANY, wxT("Source")), wxVERTICAL);
	szBox->Add(szStaticBox, 0, wxALL | wxEXPAND, 5);

	m_radioScanner = new wxRadioButton(this, wxID_ANY, wxT("Scanner (Not connected)"));
	m_radioScanner->SetValue(true);
	szStaticBox->Add(m_radioScanner, 0, wxALL | wxEXPAND);

	m_radioFile = new wxRadioButton(this, wxID_ANY, wxT("File"));
	szStaticBox->Add(m_radioFile, 0, wxALL | wxEXPAND);

	m_radioTenPrintCard = new wxRadioButton(this, wxID_ANY, wxT("Ten print card"));
	szStaticBox->Add(m_radioTenPrintCard, 0, wxALL | wxEXPAND);

	m_radioTenPrintCard->Hide();

	m_fingerSelector = new FingerSelector(this, wxID_ANY);
	m_fingerSelector->SetMinSize(wxSize(250, 120));
	m_fingerSelector->SetWindowStyle(0 | wxNO_BORDER);
	szBox->Add(m_fingerSelector, 0, wxALL, 5);

	m_lblHint = new wxStaticText(this, wxID_ANY, wxT("Hint: Click on finger to select it or mark as missing"));
	szBox->Add(m_lblHint, 0, wxALL | wxEXPAND, 5);

	m_btnStart = new wxButton(this, wxID_ANY, wxT("Start capturing"));
	szBox->Add(m_btnStart, 0, wxALL, 5);

	m_btnOpenFile = new wxButton(this, wxID_ANY, wxT("Open image"));
	m_btnOpenFile->SetBitmap(wxImage(wxImage(openFolderIcon_xpm)));
	m_btnOpenFile->Fit();
	szBox->Add(m_btnOpenFile, 0, wxALL, 5);

	wxBoxSizer *szList = new wxBoxSizer(wxVERTICAL);
	szBox->Add(szList, 1, wxALL | wxEXPAND, 0);

	m_fingerTreeWidget = new SubjectTreeWidget(this, wxID_ANY);
	m_fingerTreeWidget->SetMinSize(wxSize(240, -1));
	m_fingerTreeWidget->SetShowBiometricsOnly(true);
	szList->Add(m_fingerTreeWidget, 1, wxALL | wxEXPAND);

	m_sizerCenter = szBox = new wxBoxSizer(wxVERTICAL);
	sizer->Add(szBox, 1, wxALL | wxEXPAND, 5);

	szStaticBox = new wxStaticBoxSizer(new wxStaticBox(this, wxID_ANY, wxT("Options")), wxVERTICAL);
	szBox->Add(szStaticBox, 0, wxALL, 5);

	wxFlexGridSizer *szFlexGrid = new wxFlexGridSizer(4, 2, 5, 5);
	szFlexGrid->AddGrowableCol(1);
	szStaticBox->Add(szFlexGrid, 1, wxALL | wxEXPAND);

	wxStaticText *text = new wxStaticText(this, wxID_ANY, wxT("Scenario"));
	szFlexGrid->Add(text, 0, wxALL | wxALIGN_CENTER_VERTICAL);

	m_choiceScenario = new wxChoice(this, wxID_ANY);
	szFlexGrid->Add(m_choiceScenario, 1, wxALL | wxEXPAND);

	text = new wxStaticText(this, wxID_ANY, wxT("Impression"));
	szFlexGrid->Add(text, 0, wxALL | wxALIGN_CENTER_VERTICAL);

	m_choiceImpression = new wxChoice(this, wxID_ANY);
	szFlexGrid->Add(m_choiceImpression, 1, wxALL | wxEXPAND);

	szFlexGrid->AddSpacer(0);

	m_chbCaptureAutomatically = new wxCheckBox(this, wxID_ANY, wxT("Capture automatically"));
	m_chbCaptureAutomatically->SetValue(true);
	szFlexGrid->Add(m_chbCaptureAutomatically, 0, wxALIGN_CENTER_VERTICAL);

	szFlexGrid->AddSpacer(0);

	m_chbWithGeneralization = new wxCheckBox(this, wxID_ANY, wxT("With generalization"));
	szFlexGrid->Add(m_chbWithGeneralization, 0, wxALIGN_CENTER_VERTICAL);

	m_fingerView = new wxNFingerView(this, wxID_ANY);
	m_fingerView->EnableContextMenu(false);
	szBox->Add(m_fingerView, 1, wxALL | wxEXPAND, 5);

	m_generalizeProgressView = new GeneralizeProgressView(this, wxID_ANY);
	m_generalizeProgressView->SetMinSize(wxSize(20, 20));
	m_generalizeProgressView->SetView(m_fingerView);
	szBox->Add(m_generalizeProgressView, 0, wxALL | wxEXPAND, 5);

	m_statusSizer = new wxBoxSizer(wxHORIZONTAL);
	szBox->Add(m_statusSizer, 0, wxLEFT | wxRIGHT | wxEXPAND, 5);

	m_busyIndicator = new BusyIndicator( this, wxID_ANY, wxDefaultPosition, wxSize(14, 14) );
	m_busyIndicator->Hide();
	m_statusSizer->Add( m_busyIndicator, 0, wxRIGHT, 5 );

	m_statusPanel = new StatusPanel(this, wxID_ANY);
	m_statusSizer->Add( m_statusPanel, 1, wxEXPAND, 5 );

	wxBoxSizer *szControls = new wxBoxSizer(wxHORIZONTAL);
	szBox->Add(szControls, 0, wxALL | wxALIGN_CENTER, 5);

	m_btnRepeat = new wxButton(this, wxID_ANY, wxT("Repeat"));
	m_btnRepeat->SetBitmap(wxImage(wxImage(repeatIcon_xpm)));
	m_btnRepeat->Fit();
	szControls->Add(m_btnRepeat, 0, wxALL, 5);

	m_btnSkip = new wxButton(this, wxID_ANY, wxT("Next"));
	m_btnSkip->SetBitmap(wxImage(wxImage(nextIcon_xpm)));
	m_btnSkip->Fit();
	szControls->Add(m_btnSkip, 0, wxALL | wxEXPAND, 5);

	m_btnCancel = new wxButton(this, wxID_ANY, wxT("Cancel"));
	szControls->Add(m_btnCancel, 0, wxALL | wxEXPAND, 5);

	m_btnForce = new wxButton(this, wxID_ANY, wxT("Force"));
	szControls->Add(m_btnForce, 0, wxALL | wxEXPAND, 5);

	wxFlexGridSizer * szGrid = new wxFlexGridSizer(4, 0, 0);
	szGrid->AddGrowableCol(2, 1);
	szBox->Add(szGrid, 0, wxALL | wxEXPAND, 5);

	m_horizontalSlider = new wxNViewZoomSlider(this);
	m_horizontalSlider->SetView(m_fingerView);
	szGrid->Add(m_horizontalSlider, 0, wxALL | wxEXPAND, 5);

	m_chbShowBinarizedImage = new wxCheckBox(this, wxID_ANY, wxT("Show binarized image"));
	m_chbShowBinarizedImage->Enable(false);
	szGrid->Add(m_chbShowBinarizedImage, 0, wxALL | wxALIGN_CENTER_VERTICAL, 5);

	m_btnFinish = new wxButton(this, wxID_ANY, wxT("Finish"), wxDefaultPosition, wxDefaultSize);
	szGrid->Add(m_btnFinish, 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT);

	m_timer = new wxTimer(this);

	this->Layout();
}

NFPosition CaptureFingersPage::Scenario::PlainFingers[] =
{
	nfpLeftLittle,
	nfpLeftRing,
	nfpLeftMiddle,
	nfpLeftIndex,
	nfpLeftThumb,
	nfpRightThumb,
	nfpRightIndex,
	nfpRightMiddle,
	nfpRightRing,
	nfpRightLittle
};

NFPosition CaptureFingersPage::Scenario::Slaps[] =
{
	nfpPlainLeftFourFingers,
	nfpPlainRightFourFingers,
	nfpPlainThumbs
};

NFPosition CaptureFingersPage::Scenario::SlapsSeparatteThumbs[] =
{
	nfpPlainLeftFourFingers,
	nfpPlainRightFourFingers,
	nfpLeftThumb,
	nfpRightThumb
};

CaptureFingersPage::Scenario::Scenario(wxString name) : m_name(name)
{
	m_hasSlaps = false;
	m_hasRolled = false;
	m_isUnknownFingers = false;
}

wxString CaptureFingersPage::Scenario::GetName()
{
	return m_name;
}

bool CaptureFingersPage::Scenario::HasSlaps()
{
	return m_hasSlaps;
}

bool CaptureFingersPage::Scenario::HasRolled()
{
	return m_hasRolled;
}

bool CaptureFingersPage::Scenario::IsUnknownFingers()
{
	return m_isUnknownFingers;
}

void CaptureFingersPage::Scenario::SetHasSlaps(bool value)
{
	m_hasSlaps = value;
}

void CaptureFingersPage::Scenario::SetHasRolled(bool value)
{
	m_hasRolled = value;
}

void CaptureFingersPage::Scenario::SetIsUnknownFingers(bool value)
{
	m_isUnknownFingers = value;
}

std::vector<NFPosition> CaptureFingersPage::Scenario::GetPositions()
{
	std::vector<NFPosition> positions;
	for (std::vector<Tuple>::iterator it = m_items.begin(); it != m_items.end(); it++)
	{
		positions.push_back(it->position);
	}
	return positions;
}

std::vector<NFinger> CaptureFingersPage::Scenario::GetFingers(int sessionId, int generalizationCount)
{
	std::vector<NFinger> fingers;
	for (std::vector<Tuple>::iterator it = m_items.begin(); it != m_items.end(); it++)
	{
		for (int i = 0; i < generalizationCount; i++)
		{
			NFinger finger;
			finger.SetPosition(it->position);
			finger.SetImpressionType(it->impression);
			finger.SetSessionId(sessionId);
			fingers.push_back(finger);
		}
	}
	return fingers;
}

std::vector<CaptureFingersPage::Scenario> CaptureFingersPage::Scenario::GetAvailableScenarios()
{
	std::vector<Scenario> scenarios;

	Scenario scenarioUnknownPlainFinger(wxT("Unknown plain finger"));
	Scenario scenarioUnknownRolledFinger(wxT("Unknown rolled finger"));
	Scenario scenarioAllPlainFingers(wxT("All plain fingers"));
	Scenario scenarioAllRolledFingers(wxT("All rolled fingers"));
	Scenario scenarioSlaps(wxT("4-4-2"));
	Scenario scenarioSlapsSeparateThums(wxT("4-4-1-1"));
	Scenario scenarioRolledPlusSlaps(wxT("Rolled fingers + 4-4-2"));
	Scenario scenarioRolledPlusSlapsSeparateThumbs(wxT("Rolled fingers + 4-4-1-1"));

	Tuple unknownPlainFingerTuple;
	unknownPlainFingerTuple.position = nfpUnknown;
	unknownPlainFingerTuple.impression = nfitLiveScanPlain;
	scenarioUnknownPlainFinger.SetIsUnknownFingers(true);
	scenarioUnknownPlainFinger.m_items.push_back(unknownPlainFingerTuple);

	Tuple unknownRolledFingerTuple;
	unknownRolledFingerTuple.impression = nfitLiveScanRolled;
	unknownRolledFingerTuple.position = nfpUnknown;
	scenarioUnknownRolledFinger.SetHasRolled(true);
	scenarioUnknownRolledFinger.SetIsUnknownFingers(true);
	scenarioUnknownRolledFinger.m_items.push_back(unknownRolledFingerTuple);

	Tuple allPlainFingersTuple;
	Tuple allRolledFingersTuple;
	for (unsigned int i = 0; i < sizeof(PlainFingers) / sizeof(PlainFingers[0]); i++)
	{
		allRolledFingersTuple.position = PlainFingers[i];
		allRolledFingersTuple.impression = nfitLiveScanRolled;
		allPlainFingersTuple.position = PlainFingers[i];
		allPlainFingersTuple.impression = nfitLiveScanPlain;
		scenarioAllPlainFingers.m_items.push_back(allPlainFingersTuple);
		scenarioAllRolledFingers.m_items.push_back(allRolledFingersTuple);
	}
	scenarioAllRolledFingers.SetHasRolled(true);

	Tuple slapsTuple;
	for (unsigned int i = 0; i < sizeof(Slaps) / sizeof(Slaps[0]); i++)
	{
		slapsTuple.position = Slaps[i];
		slapsTuple.impression = nfitLiveScanPlain;
		scenarioSlaps.m_items.push_back(slapsTuple);
	}
	scenarioSlaps.SetHasSlaps(true);

	Tuple slapsSeparateThumbsTuple;
	for (unsigned int i = 0; i < sizeof(SlapsSeparatteThumbs) / sizeof(SlapsSeparatteThumbs[0]); i++)
	{
		slapsSeparateThumbsTuple.position = SlapsSeparatteThumbs[i];
		slapsSeparateThumbsTuple.impression = nfitLiveScanPlain;
		scenarioSlapsSeparateThums.m_items.push_back(slapsSeparateThumbsTuple);
	}
	scenarioSlapsSeparateThums.SetHasSlaps(true);

	scenarioRolledPlusSlaps.SetHasSlaps(true);
	scenarioRolledPlusSlaps.SetHasRolled(true);
	scenarioRolledPlusSlaps.m_items.insert(scenarioRolledPlusSlaps.m_items.end(), scenarioAllRolledFingers.m_items.begin(),
		scenarioAllRolledFingers.m_items.end());
	scenarioRolledPlusSlaps.m_items.insert(scenarioRolledPlusSlaps.m_items.end(), scenarioSlaps.m_items.begin(),
		scenarioSlaps.m_items.end());

	scenarioRolledPlusSlapsSeparateThumbs.SetHasRolled(true);
	scenarioRolledPlusSlapsSeparateThumbs.SetHasSlaps(true);
	scenarioRolledPlusSlapsSeparateThumbs.m_items.insert(scenarioRolledPlusSlapsSeparateThumbs.m_items.end(), scenarioAllRolledFingers.m_items.begin(),
		scenarioAllRolledFingers.m_items.end());
	scenarioRolledPlusSlapsSeparateThumbs.m_items.insert(scenarioRolledPlusSlapsSeparateThumbs.m_items.end(), scenarioSlapsSeparateThums.m_items.begin(),
		scenarioSlapsSeparateThums.m_items.end());

	scenarios.push_back(scenarioUnknownPlainFinger);
	scenarios.push_back(scenarioUnknownRolledFinger);
	scenarios.push_back(scenarioAllPlainFingers);
	scenarios.push_back(scenarioAllRolledFingers);
	scenarios.push_back(scenarioSlaps);
	scenarios.push_back(scenarioSlapsSeparateThums);
	scenarios.push_back(scenarioRolledPlusSlaps);
	scenarios.push_back(scenarioRolledPlusSlapsSeparateThumbs);

	return scenarios;
}

}}


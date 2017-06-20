#include "Precompiled.h"

#include <SubjectEditor/CapturePalmPage.h>
#include <Settings/SettingsManager.h>

#include <Resources/OpenFolderIcon.xpm>

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Images;
using namespace Neurotec::Devices;
using namespace Neurotec::Gui;

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_CAPTURE_PALM_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_CAPTURE_PALM_THREAD, wxCommandEvent);

CapturePalmPage::CapturePalmPage(NBiometricClient& biometricClient, NSubject& subject, SubjectEditorPageInterface& subjectEditorPageInterface, wxWindow *parent, wxWindowID winid) :
	ModalityPage(biometricClient, subject, subjectEditorPageInterface, parent, winid),
	m_currentBiometric(NULL),
	m_newSubject(NULL),
	m_sessionId(-1),
	m_titlePrefix(wxEmptyString)
{
	CreateGUIControls();
	RegisterGuiEvents();
}

CapturePalmPage::~CapturePalmPage()
{
	m_zoomSlider->SetView(NULL);
	UnregisterGuiEvents();
}

void CapturePalmPage::SetIsBusy(bool value)
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

void CapturePalmPage::OnNavigatedTo()
{
	m_biometricClient.AddPropertyChangedCallback(&CapturePalmPage::OnBiometricClientPropertyChangedCallback, this);
	m_sessionId = 0;
	for (int i = 0; i < m_subject.GetPalms().GetCount(); i++)
	{
		m_sessionId = std::max(m_sessionId, m_subject.GetPalms()[i].GetSessionId());
	}
	m_newSubject = NSubject();
	m_palmTreeWidget->SetSubject(m_newSubject);
	m_statusPanel->SetMessage(wxEmptyString);
	m_statusPanel->Show(false);
	m_chbShowBinarizedImage->Show(m_biometricClient.GetPalmsReturnBinarizedImage());
	m_chbShowBinarizedImage->SetValue(false);
	OnPalmScannerChanged();

	wxCommandEvent empty;
	OnSourceSelect(empty);
	m_generalizeProgressView->Clear();
	m_generalizeProgressView->Show(false);

	ModalityPage::OnNavigatedTo();
}

void CapturePalmPage::OnNavigatingFrom()
{
	NSubject nullSubject = NULL;

	Cancel();

	m_biometricClient.RemovePropertyChangedCallback(&CapturePalmPage::OnBiometricClientPropertyChangedCallback, this);
	m_palmTreeWidget->SetSubject(nullSubject);

	NArrayWrapper<NPalm> palms = m_newSubject.GetPalms().GetAll();
	m_newSubject.GetPalms().Clear();
	for (NArrayWrapper<NPalm>::iterator it = palms.begin(); it != palms.end(); it++)
	{
		m_subject.GetPalms().Add(*it);
	}
	m_view->Clear();
	m_newSubject = NULL;
	m_generalizeProgressView->Clear();

	ModalityPage::OnNavigatingFrom();
}

void CapturePalmPage::OnThread(wxCommandEvent &event)
{
	int id = event.GetId();

	switch(id)
	{
	case ID_EVENT_SCANNER_CHANGED:
		OnPalmScannerChanged();
		break;
	case ID_EVENT_STATUS_CHANGED:
		{
			NBiometricStatus status = (NBiometricStatus)event.GetInt();
			wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
			m_statusPanel->SetMessage(wxString::Format(wxT("%sStaus: %s"), m_titlePrefix.c_str(), statusString.c_str()), status == nbsOk || status == nbsNone ? StatusPanel::SUCCESS_MESSAGE : StatusPanel::ERROR_MESSAGE);
			break;
		}
	case ID_EVENT_CURRENT_BIOMETRIC_CHANGED:
		{
			NBiometric current = m_biometricClient.GetCurrentBiometric();
			if (!m_currentBiometric.IsNull())
			{
				m_currentBiometric.RemovePropertyChangedCallback(&CapturePalmPage::OnPalmPropertyChangedCallback, this);
				m_currentBiometric = NULL;
			}

			if (!current.IsNull())
			{
				bool withGeneralization = m_chbWithGeneralization->GetValue();
				Node * node = m_palmTreeWidget->GetBiometricNode(current);
				NPalm palm = NObjectDynamicCast<NPalm>(current);
				if (withGeneralization)
				{
					std::vector<NBiometric> items = node->GetItems();
					std::vector<NBiometric> generalized = node->GetAllGeneralized();
					m_generalizeProgressView->SetBiometrics(items);
					m_generalizeProgressView->SetGeneralized(generalized);
					m_generalizeProgressView->SetSelected(current);
					wxString positionString = NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), palm.GetPosition());

					std::vector<NBiometric>::iterator find = std::find(items.begin(), items.end(), current);
					int index = std::distance(items.begin(), find);
					m_titlePrefix = wxString::Format("Capturing %s (%d of %d). ", positionString, index + 1, SettingsManager::GetPalmsGeneralizationRecordCount());
				}
				m_view->SetPalm(palm);
				m_currentBiometric = palm;
				m_currentBiometric.AddPropertyChangedCallback(&CapturePalmPage::OnPalmPropertyChangedCallback, this);
			}
			else
			{
				m_generalizeProgressView->Clear();
				m_statusPanel->Hide();
				m_titlePrefix = wxEmptyString;
			}
			break;
		}
	case ID_EVENT_CREATE_TEMPLATE_COMPLETED:
		{
			NBiometricStatus status = nbsInternalError;
			try
			{
				NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
				NValue result = operation.GetResult();
				NBiometricTask biometricTask = NObjectDynamicCast<NBiometricTask>(result.ToObject(NBiometricTask::NativeTypeOf()));
				NError exception = biometricTask.GetError();
				if (!exception.IsNull()) wxExceptionDlg::Show(exception);
				status = biometricTask.GetStatus();

				if (status != nbsOk)
				{
					std::vector<NPalm> palms = SubjectUtils::FlattenPalms(m_nowCapturing);
					for (std::vector<NPalm>::iterator it = palms.begin(); it != palms.end(); it++)
					{
						NInt index = m_newSubject.GetPalms().IndexOf(*it);
						if (index != -1) m_newSubject.GetPalms().RemoveAt(index);
					}
				}

				m_palmTreeWidget->UpdateTree();
				m_generalizeProgressView->SetEnableMouseSelection(true);
				if (status != nbsOk)
				{
					wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					m_palmTreeWidget->SetSelectedItem(NULL);
					m_view->SetPalm(m_nowCapturing.empty() ? NULL : m_nowCapturing[0]);
					m_statusPanel->SetMessage(wxString::Format(wxT("Extraction failed: %s"), statusString.c_str()), StatusPanel::ERROR_MESSAGE);
				}
				else
				{
					m_statusPanel->SetMessage(wxT("Extraction completed successfully"), StatusPanel::SUCCESS_MESSAGE);
					if (m_chbWithGeneralization->GetValue())
					{
						NPalm first = m_nowCapturing[0];
						Node * node = m_palmTreeWidget->GetBiometricNode(first);
						if (node)
						{
							std::vector<NBiometric> generalized = node->GetAllGeneralized();
							m_generalizeProgressView->SetGeneralized(generalized);
							m_generalizeProgressView->SetSelected(generalized.empty() ? first : generalized[0]);
							m_generalizeProgressView->Show(node->IsGeneralizedNode());
						}
					}
				}
			}
			catch (NError & error)
			{
				wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), nbsInternalError);
				m_statusPanel->SetMessage(wxString::Format(wxT("Extraction failed: %s"), statusString.c_str()), StatusPanel::ERROR_MESSAGE);
				wxExceptionDlg::Show(error);
			}

			SetIsBusy(false);
			m_nowCapturing.clear();
			EnableControls();
			break;
		}
	case ID_EVENT_CAPTURE_FINISHED:
		{
			NBiometricStatus status = nbsInternalError;
			try
			{
				NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
				NValue result = operation.GetResult();
				NBiometricTask biometricTask = NObjectDynamicCast<NBiometricTask>(result.ToObject(NBiometricTask::NativeTypeOf()));
				NError exception = biometricTask.GetError();
				if (!exception.IsNull()) wxExceptionDlg::Show(exception);

				status = biometricTask.GetStatus();
				m_palmTreeWidget->UpdateTree();
				m_generalizeProgressView->SetEnableMouseSelection(true);
				m_statusPanel->Show(true);
				if (status == nbsOk)
				{
					m_statusPanel->SetMessage(wxT("Palms captured successfully"), StatusPanel::SUCCESS_MESSAGE);
				}
				else
				{
					if (status == nbsCanceled)
						m_statusPanel->Hide();
					else
					{
						wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
						m_statusPanel->SetMessage(wxString::Format(wxT("Extraction completed successfully: %s"), status), StatusPanel::ERROR_MESSAGE);
					}
				}

				Node * selected = m_palmTreeWidget->GetSelectedItem();
				if (selected && selected->IsGeneralizedNode())
				{
					std::vector<NBiometric> generalized = selected->GetAllGeneralized();
					std::vector<NBiometric> items = selected->GetItems();
					m_generalizeProgressView->SetBiometrics(items);
					m_generalizeProgressView->SetGeneralized(generalized);
					m_generalizeProgressView->SetSelected(generalized.empty() ? items[0] : generalized[0]);
				}
				else
				{
					m_generalizeProgressView->Clear();
					m_generalizeProgressView->Hide();
					if (selected)
					{
						std::vector<NBiometric> items = selected->GetItems();
						m_view->SetPalm(NObjectDynamicCast<NPalm>(items[0]));
					}
				}
			}
			catch(NError & error)
			{
				wxExceptionDlg::Show(error);
			}

			SetIsBusy(false);
			m_nowCapturing.clear();
			EnableControls();
			break;
		}
	default:
		break;
	};
}

void CapturePalmPage::OnBiometricClientPropertyChangedCallback(NObject::PropertyChangedEventArgs args)
{
	wxString propertyName = args.GetPropertyName();
	CapturePalmPage * page = static_cast<CapturePalmPage *>(args.GetParam());
	if (propertyName == wxT("PalmScanner"))
	{
		wxPostEvent(page, wxCommandEvent(wxEVT_CAPTURE_PALM_THREAD, ID_EVENT_SCANNER_CHANGED));
	}
	else if (propertyName == wxT("CurrentBiometric"))
	{
		wxPostEvent(page, wxCommandEvent(wxEVT_CAPTURE_PALM_THREAD, ID_EVENT_CURRENT_BIOMETRIC_CHANGED));
	}
}

void CapturePalmPage::OnPalmPropertyChangedCallback(NObject::PropertyChangedEventArgs args)
{
	if (args.GetPropertyName() == N_T("Status"))
	{
		CapturePalmPage *page = reinterpret_cast<CapturePalmPage *>(args.GetParam());
		NBiometric palm = args.GetObject<NPalm>();
		wxCommandEvent evt(wxEVT_CAPTURE_PALM_THREAD, ID_EVENT_STATUS_CHANGED);
		evt.SetInt((int)palm.GetStatus());
		wxPostEvent(page, evt);
	}
}

void CapturePalmPage::OnCreateTemplateCompletedCallback(EventArgs args)
{
	CapturePalmPage *page = static_cast<CapturePalmPage *>(args.GetParam());
	wxCommandEvent event(wxEVT_CAPTURE_PALM_THREAD, ID_EVENT_CREATE_TEMPLATE_COMPLETED);
	event.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
	wxPostEvent(page, event);
}

void CapturePalmPage::OnPalmScannerChanged()
{
	try
	{
		NFScanner device = m_biometricClient.GetPalmScanner();
		if (device.IsNull() || !device.IsAvailable())
		{
			m_radioFile->SetValue(true);
			m_radioScanner->SetLabel(wxT("Scanner (Not connected)"));
			wxCommandEvent empty;
			OnSourceSelect(empty);
		}
		else
		{
			m_radioScanner->SetLabel(wxString::Format(wxT("Scanner (%s)"), ((wxString)device.GetDisplayName()).c_str()));
		}
		EnableControls();
	}
	catch(NError & error)
	{
		wxExceptionDlg::Show(error);
	}
	Layout();
}

void CapturePalmPage::OnPalmTreeSelectionChanged(wxCommandEvent &)
{
	Node * node = m_palmTreeWidget->GetSelectedItem();
	std::vector<NBiometric> items;
	NPalm first = NULL;

	if (node) items = node->GetItems();
	if (!items.empty()) first = NObjectDynamicCast<NPalm>(items[0]);

	m_generalizeProgressView->Clear();
	if (node && node->IsGeneralizedNode())
	{
		std::vector<NBiometric> generalized = node->GetAllGeneralized();
		m_generalizeProgressView->SetBiometrics(items);
		m_generalizeProgressView->SetGeneralized(generalized);
		m_generalizeProgressView->SetSelected(generalized.empty() ? first : generalized[0]);
		m_generalizeProgressView->Show(true);
	}
	else
		m_generalizeProgressView->Show(false);

	m_view->SetPalm(first);
	m_chbShowBinarizedImage->Enable(!first.IsNull() && !first.GetBinarizedImage().IsNull());
	m_statusPanel->Show(m_statusPanel->IsShown() && IsBusy());
	if (m_chbShowBinarizedImage->GetValue() && (first.IsNull() || first.GetBinarizedImage().IsNull()))
	{
		m_chbShowBinarizedImage->SetValue(false);
		m_view->SetShownImage(wxNFrictionRidgeView::ORIGINAL_IMAGE);
	}
	Layout();
}

void CapturePalmPage::OnSourceSelect(wxCommandEvent&)
{
	std::vector<NFPosition> positions;
	m_choiceImpression->Clear();
	m_palmSelector->SetSelection(nfpUnknown);
	m_palmSelector->SetAllowedPositions(positions);
	m_choicePosition->Clear();
	if (m_radioFile->GetValue())
	{
		NArrayWrapper<NInt> values = NEnum::GetValues(NBiometricTypes::NFImpressionTypeNativeTypeOf());
		for (NArrayWrapper<NInt>::iterator it = values.begin(); it != values.end(); it++)
		{
			NFImpressionType impression = (NFImpressionType)*it;
			if (NBiometricTypes::IsImpressionTypePalm(impression))
			{
				m_choiceImpression->Append(NEnum::ToString(NBiometricTypes::NFImpressionTypeNativeTypeOf(), impression));
			}
		}
		m_choiceImpression->SetSelection(0);

		positions.push_back(nfpLeftFullPalm);
		positions.push_back(nfpRightFullPalm);
		positions.push_back(nfpLeftUpperPalm);
		positions.push_back(nfpRightUpperPalm);
		positions.push_back(nfpLeftInterdigital);
		positions.push_back(nfpRightInterdigital);
		positions.push_back(nfpLeftHypothenar);
		positions.push_back(nfpRightHypothenar);
		positions.push_back(nfpLeftLowerPalm);
		positions.push_back(nfpRightLowerPalm);
		positions.push_back(nfpLeftThenar);
		positions.push_back(nfpRightThenar);
	}
	else
	{
		try
		{
			NFScanner device = m_biometricClient.GetPalmScanner();
			NArrayWrapper<NFImpressionType> impressions = device.GetSupportedImpressionTypes();
			NArrayWrapper<NFPosition> supportedPositions = device.GetSupportedPositions();
			for (NArrayWrapper<NFImpressionType>::iterator it = impressions.begin(); it != impressions.end(); it++)
			{
				if (NBiometricTypes::IsImpressionTypePalm(*it))
				{
					m_choiceImpression->Append(NEnum::ToString(NBiometricTypes::NFImpressionTypeNativeTypeOf(), *it));
				}
			}
			m_choiceImpression->SetSelection(0);

			for (NArrayWrapper<NFPosition>::iterator it = supportedPositions.begin(); it != supportedPositions.end(); it++)
			{
				NFPosition pos = *it;
				if (NBiometricTypes::IsPositionPalm(pos))
				{
					positions.push_back(pos);
				}
			}
		}
		catch(NError & error)
		{
			wxExceptionDlg::Show(error);
		}
	}

	for (std::vector<NFPosition>::iterator it = positions.begin(); it != positions.end(); it++)
	{
		m_choicePosition->Append(NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), *it));
	}
	m_choicePosition->SetSelection(0);
	m_palmSelector->SetAllowedPositions(positions);
	m_palmSelector->SetSelection(positions[0]);
	EnableControls();
	this->Layout();
}

void CapturePalmPage::OnPositionSelect(wxCommandEvent&)
{
	NFPosition selection = (NFPosition)NEnum::Parse(NBiometricTypes::NFPositionNativeTypeOf(), m_choicePosition->GetStringSelection());
	m_palmSelector->SetSelection(selection);
}

void CapturePalmPage::OnCaptureClick(wxCommandEvent &)
{
	NFPosition position = (NFPosition)NEnum::Parse(NBiometricTypes::NFPositionNativeTypeOf(), m_choicePosition->GetStringSelection());
	NFImpressionType impressionType = (NFImpressionType)NEnum::Parse(NBiometricTypes::NFImpressionTypeNativeTypeOf(), m_choiceImpression->GetStringSelection());
	bool generalize = m_chbWithGeneralization->GetValue();
	int sessionId = generalize ? m_sessionId++ : -1;
	int count = generalize ? SettingsManager::GetPalmsGeneralizationRecordCount() : 1;
	bool manual = !m_chbCaptureAutomatically->GetValue();

	m_nowCapturing.clear();
	m_generalizeProgressView->Clear();
	m_generalizeProgressView->Show(generalize);

	for (int i = 0; i < count; i++)
	{
		NPalm palm;
		palm.SetCaptureOptions(manual ? nbcoManual : nbcoNone);
		palm.SetSessionId(sessionId);
		palm.SetPosition(position);
		palm.SetImpressionType(impressionType);
		m_nowCapturing.push_back(palm);
		m_newSubject.GetPalms().Add(palm);
	}

	m_palmTreeWidget->UpdateTree();
	m_palmTreeWidget->SetSelectedItem(m_palmTreeWidget->GetBiometricNode(m_nowCapturing[0]));
	m_statusPanel->SetMessage(wxT("Starting capturing from scanner ..."));
	m_statusPanel->Show();

	NBiometricTask biometricTask = m_biometricClient.CreateTask((NBiometricOperations)(nboCapture | nboCreateTemplate), m_newSubject);
	SetIsBusy(true);
	NAsyncOperation operation = m_biometricClient.PerformTaskAsync(biometricTask);
	operation.AddCompletedCallback(&CapturePalmPage::OnCaptureCompletedCallback, this);
	EnableControls();
}

void CapturePalmPage::OnCaptureCompletedCallback(EventArgs args)
{
	CapturePalmPage *page = static_cast<CapturePalmPage *>(args.GetParam());
	wxCommandEvent event(wxEVT_CAPTURE_PALM_THREAD, ID_EVENT_CAPTURE_FINISHED);
	event.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
	wxPostEvent(page, event);
}

void CapturePalmPage::OnCancelClick(wxCommandEvent &)
{
	m_biometricClient.Cancel();
	m_btnCancel->Disable();
}

void CapturePalmPage::OnForceClick(wxCommandEvent &)
{
	m_biometricClient.Force();
}

void CapturePalmPage::OnOpenClick(wxCommandEvent &)
{
	NFPosition position = (NFPosition)NEnum::Parse(NBiometricTypes::NFPositionNativeTypeOf(), m_choicePosition->GetStringSelection());
	NFImpressionType impressionType = (NFImpressionType)NEnum::Parse(NBiometricTypes::NFImpressionTypeNativeTypeOf(), m_choiceImpression->GetStringSelection());
	bool generalize = m_chbWithGeneralization->GetValue();
	NInt sessionId = generalize ? m_sessionId++ : -1;
	std::vector<wxString> files;
	int count = generalize ? SettingsManager::GetPalmsGeneralizationRecordCount() : 1;

	m_nowCapturing.clear();
	m_generalizeProgressView->Clear();
	m_generalizeProgressView->Show(generalize);

	while (count > (int)files.size())
	{
		wxString title = generalize ? wxString::Format(wxT("Open image (%d of %d)"), files.size() + 1, count) : wxT("Open image");
		wxFileDialog openFileDialog(this, title, wxEmptyString, wxEmptyString, (wxString)Common::GetOpenFileFilterString(true, true), wxFD_OPEN | wxFD_FILE_MUST_EXIST);
		if (openFileDialog.ShowModal() != wxID_OK) return;
		files.push_back(openFileDialog.GetPath());
	}

	for (int i = 0; i < count; i++)
	{
		NPalm palm;
		palm.SetPosition(position);
		palm.SetImpressionType(impressionType);
		palm.SetSessionId(sessionId);
		palm.SetFileName(files[i]);
		m_newSubject.GetPalms().Add(palm);
		m_nowCapturing.push_back(palm);
	}

	NPalm first = m_nowCapturing[0];
	if (generalize)
	{
		m_generalizeProgressView->SetBiometrics(m_nowCapturing);
		m_generalizeProgressView->SetSelected(first);
	}
	m_view->SetPalm(first);
	m_palmTreeWidget->UpdateTree();
	m_palmTreeWidget->SetSelectedItem(m_palmTreeWidget->GetBiometricNode(first));

	NBiometricTask biometricTask = m_biometricClient.CreateTask(nboCreateTemplate, m_newSubject);
	SetIsBusy(true);
	NAsyncOperation operation = m_biometricClient.PerformTaskAsync(biometricTask);
	operation.AddCompletedCallback(&CapturePalmPage::OnCreateTemplateCompletedCallback, this);
	m_statusPanel->SetMessage(wxT("Extracting template. Please wait ..."));
	m_statusPanel->Show();
	EnableControls();
}

void CapturePalmPage::OnFinishClick(wxCommandEvent &)
{
	SelectFirstPage();
}

void CapturePalmPage::OnPalmSelectorMouseSelect(void *param, NFPosition position)
{
	CapturePalmPage *page = static_cast<CapturePalmPage *>(param);
	if (page && position != nfpUnknownPalm)
	{
		page->m_choicePosition->SetStringSelection((wxString)NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), position));
	}
}

void CapturePalmPage::OnShowProcessedImageClick(wxCommandEvent&)
{
	m_view->SetShownImage(m_chbShowBinarizedImage->GetValue() ? wxNPalmView::PROCESSED_IMAGE : wxNPalmView::ORIGINAL_IMAGE);
}

void CapturePalmPage::EnableControls()
{
	bool isIdle = !IsBusy();
	bool fromScanner = m_radioScanner->GetValue();
	NFScanner palmScanner = m_biometricClient.GetPalmScanner();
	m_radioScanner->Enable(isIdle && !palmScanner.IsNull() && palmScanner.IsAvailable());
	m_radioFile->Enable(isIdle);
	m_choiceImpression->Enable(isIdle);
	m_chbCaptureAutomatically->Enable(isIdle);
	m_chbWithGeneralization->Enable(isIdle);
	m_palmSelector->Enable(isIdle);
	m_choicePosition->Enable(isIdle);
	m_palmTreeWidget->Enable(isIdle);
	m_btnCapture->Enable(isIdle || fromScanner);
	m_btnCapture->Show(fromScanner && isIdle);
	m_btnCancel->Enable(!isIdle);
	m_btnCancel->Show(fromScanner);
	m_btnOpen->Enable(isIdle && !fromScanner);
	m_btnOpen->Show(!fromScanner);
	m_palmTreeWidget->Enable(isIdle);
	m_chbShowBinarizedImage->Enable(isIdle);
	m_chbCaptureAutomatically->Enable(isIdle && fromScanner);
	m_btnForce->Show(fromScanner);
	m_btnForce->Enable(fromScanner && !isIdle);
	isIdle ? m_busyIndicator->Hide() : m_busyIndicator->Show();
	m_statusSizer->RecalcSizes();
	Layout();
}

void CapturePalmPage::RegisterGuiEvents()
{
	this->Bind(wxEVT_CAPTURE_PALM_THREAD, &CapturePalmPage::OnThread, this);
	m_radioScanner->Connect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CapturePalmPage::OnSourceSelect), NULL, this);
	m_radioFile->Connect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CapturePalmPage::OnSourceSelect), NULL, this);
	m_choicePosition->Connect(wxEVT_COMMAND_CHOICE_SELECTED, wxCommandEventHandler(CapturePalmPage::OnPositionSelect), NULL, this);
	m_btnFinish->Connect(wxEVT_BUTTON, wxCommandEventHandler(CapturePalmPage::OnFinishClick), NULL, this);
	m_btnCancel->Connect(wxEVT_BUTTON, wxCommandEventHandler(CapturePalmPage::OnCancelClick), NULL, this);
	m_btnForce->Connect(wxEVT_BUTTON, wxCommandEventHandler(CapturePalmPage::OnForceClick), NULL, this);
	m_btnCapture->Connect(wxEVT_BUTTON, wxCommandEventHandler(CapturePalmPage::OnCaptureClick), NULL, this);
	m_btnOpen->Connect(wxEVT_BUTTON, wxCommandEventHandler(CapturePalmPage::OnOpenClick), NULL, this);
	m_chbShowBinarizedImage->Connect(wxEVT_COMMAND_CHECKBOX_CLICKED, wxCommandEventHandler(CapturePalmPage::OnShowProcessedImageClick), NULL, this);
	m_palmTreeWidget->Connect(wxEVT_TREE_SELECTED_ITEM_CHANGED, wxCommandEventHandler(CapturePalmPage::OnPalmTreeSelectionChanged), NULL, this);
}

void CapturePalmPage::UnregisterGuiEvents()
{
	m_radioScanner->Disconnect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CapturePalmPage::OnSourceSelect), NULL, this);
	m_radioFile->Disconnect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CapturePalmPage::OnSourceSelect), NULL, this);
	m_choicePosition->Disconnect(wxEVT_COMMAND_CHOICE_SELECTED, wxCommandEventHandler(CapturePalmPage::OnPositionSelect), NULL, this);
	m_btnFinish->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CapturePalmPage::OnFinishClick), NULL, this);
	m_btnCancel->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CapturePalmPage::OnCancelClick), NULL, this);
	m_btnForce->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CapturePalmPage::OnForceClick), NULL, this);
	m_btnCapture->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CapturePalmPage::OnCaptureClick), NULL, this);
	m_btnOpen->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CapturePalmPage::OnOpenClick), NULL, this);
	m_chbShowBinarizedImage->Disconnect(wxEVT_COMMAND_CHECKBOX_CLICKED, wxCommandEventHandler(CapturePalmPage::OnShowProcessedImageClick), NULL, this);
	m_palmTreeWidget->Disconnect(wxEVT_TREE_SELECTED_ITEM_CHANGED, wxCommandEventHandler(CapturePalmPage::OnPalmTreeSelectionChanged), NULL, this);
	this->Unbind(wxEVT_CAPTURE_PALM_THREAD, &CapturePalmPage::OnThread, this);
}

void CapturePalmPage::CreateGUIControls()
{
	wxBoxSizer *sizer = new wxBoxSizer(wxHORIZONTAL);
	this->SetSizer(sizer, true);

	wxBoxSizer *szBox = new wxBoxSizer(wxVERTICAL);
	sizer->Add(szBox, 0, wxALL | wxEXPAND);

	wxStaticBoxSizer *szStaticBox = new wxStaticBoxSizer(new wxStaticBox(this, wxID_ANY, wxT("Source")), wxVERTICAL);
	szBox->Add(szStaticBox, 0, wxALL | wxEXPAND);

	m_radioScanner = new wxRadioButton(this, wxID_ANY, wxEmptyString);
	szStaticBox->Add(m_radioScanner, 0, wxALL | wxEXPAND);

	m_radioFile = new wxRadioButton(this, wxID_ANY, wxT("File"));
	m_radioFile->SetValue(true);
	szStaticBox->Add(m_radioFile, 0, wxALL | wxEXPAND);

	szStaticBox = new wxStaticBoxSizer(new wxStaticBox(this, wxID_ANY, wxT("Options")), wxVERTICAL);
	szBox->Add(szStaticBox, 0, wxALL | wxEXPAND);

	wxBoxSizer *szBoxTmp = new wxBoxSizer(wxHORIZONTAL);
	szStaticBox->Add(szBoxTmp, 0, wxALL | wxEXPAND);

	wxStaticText *text = new wxStaticText(this, wxID_ANY, wxT("Impression"));
	szBoxTmp->Add(text, 0, wxALL | wxALIGN_CENTER_VERTICAL, 5);

	m_choiceImpression = new wxChoice(this, wxID_ANY);
	szBoxTmp->Add(m_choiceImpression, 1, wxALL | wxEXPAND, 5);

	m_chbCaptureAutomatically = new wxCheckBox(this, wxID_ANY, wxT("Capture automatically"));
	m_chbCaptureAutomatically->SetValue(true);
	szStaticBox->Add(m_chbCaptureAutomatically, 0, wxALL | wxEXPAND, 5);

	m_chbWithGeneralization = new wxCheckBox(this, wxID_ANY, wxT("With generalization"));
	szStaticBox->Add(m_chbWithGeneralization, 1, wxALL | wxEXPAND, 5);

	szBoxTmp = new wxBoxSizer(wxHORIZONTAL);
	szBox->Add(szBoxTmp, 0, wxALL | wxEXPAND, 5);

	text = new wxStaticText(this, wxID_ANY, wxT("Selected position:"));
	szBoxTmp->Add(text, 0, wxALL | wxALIGN_CENTER_VERTICAL, 5);

	m_choicePosition = new wxChoice(this, wxID_ANY);
	szBoxTmp->Add(m_choicePosition, 1, wxALL | wxEXPAND);

	m_btnOpen = new wxButton(this, wxID_ANY, wxT("Open"));
	m_btnOpen->SetBitmap(wxImage(wxImage(openFolderIcon_xpm)));
	m_btnOpen->Fit();
	szBox->Add(m_btnOpen, 0, wxALL, 5);

	m_btnCapture = new wxButton(this, wxID_ANY, wxT("Capture"));
	szBox->Add(m_btnCapture, 0, wxALL, 5);

	m_btnCancel = new wxButton(this, wxID_ANY, wxT("Cancel"));
	szBox->Add(m_btnCancel, 0, wxALL, 5);

	m_btnForce = new wxButton(this, wxID_ANY, wxT("Force"));
	szBox->Add(m_btnForce, 0, wxALL, 5);

	m_palmSelector = new PalmSelector(this, wxID_ANY);
	m_palmSelector->SetMinSize(wxSize(250, 120));
	m_palmSelector->SetWindowStyle(0 | wxNO_BORDER);
	m_palmSelector->SetSelectionChangedCallback(&CapturePalmPage::OnPalmSelectorMouseSelect, this);
	szBox->Add(m_palmSelector, 0, wxALL, 5);

	m_palmTreeWidget = new SubjectTreeWidget(this, wxID_ANY);
	m_palmTreeWidget->SetShowBiometricsOnly(true);
	szBox->Add(m_palmTreeWidget, 1, wxALL | wxEXPAND);

	szBox = new wxBoxSizer(wxVERTICAL);
	sizer->Add(szBox, 1, wxALL | wxEXPAND);

	m_view = new wxNPalmView(this, wxID_ANY);
	m_view->EnableContextMenu(false);
	m_view->SetWindowStyle(wxSIMPLE_BORDER);
	szBox->Add(m_view, 1, wxALL | wxEXPAND, 5);

	m_generalizeProgressView = new GeneralizeProgressView(this, wxID_ANY);
	m_generalizeProgressView->SetView(m_view);
	m_generalizeProgressView->SetMinSize(wxSize(20, 20));
	szBox->Add(m_generalizeProgressView, 0, wxALL | wxEXPAND, 5);

	m_statusSizer = new wxBoxSizer(wxHORIZONTAL);
	szBox->Add(m_statusSizer, 0, wxLEFT | wxRIGHT | wxEXPAND, 5);

	m_busyIndicator = new BusyIndicator( this, wxID_ANY, wxDefaultPosition, wxSize(14, 14) );
	m_busyIndicator->Hide();
	m_statusSizer->Add( m_busyIndicator, 0, wxRIGHT, 5 );

	m_statusPanel = new StatusPanel(this, wxID_ANY);
	m_statusSizer->Add( m_statusPanel, 1, wxEXPAND, 5 );

	wxFlexGridSizer *szControls = new wxFlexGridSizer(1, 4, 0, 0);
	szControls->AddGrowableCol(2);
	szBox->Add(szControls, 0, wxALL | wxEXPAND);

	m_zoomSlider = new wxNViewZoomSlider(this);
	m_zoomSlider->SetView(m_view);
	szControls->Add(m_zoomSlider, 0, wxALL, 5);

	m_chbShowBinarizedImage = new wxCheckBox(this, wxID_ANY, wxT("Show binarized image"));
	szControls->Add(m_chbShowBinarizedImage, 0, wxALL | wxALIGN_CENTER_VERTICAL, 5);

	szControls->AddStretchSpacer(1);

	m_btnFinish = new wxButton(this, wxID_ANY, wxT("Finish"));
	szControls->Add(m_btnFinish, 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 5);

	this->Layout();
}

}}


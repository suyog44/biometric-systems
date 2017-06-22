#include "Precompiled.h"

#include <Common/DatabaseOperationPage.h>
#include <Common/MatchingResultPage.h>
#include <Common/MatchingResultView.h>
#include <Common/TabController.h>

#include <Settings/SettingsManager.h>

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Gui;

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_DB_OPERATION_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_DB_OPERATION_THREAD, wxCommandEvent);

BEGIN_EVENT_TABLE(DatabaseOperationPage, wxWindow)
	EVT_TIMER(ID_PROGRESS_PULSER, DatabaseOperationPage::OnProgressPulse)
END_EVENT_TABLE()

DatabaseOperationPage::DatabaseOperationPage(wxWindow *parent, wxWindowID winid, NBiometricClient & biometricClient, NSubject & subject, NBiometricOperations operationType) :
	TabPage(parent, winid),
	m_operationType(operationType),
	m_biometricClient(biometricClient),
	m_subject(subject),
	m_isOperationStarted(false)
{
	CreateGUIControls();
	RegisterGuiEvents();
}

DatabaseOperationPage::~DatabaseOperationPage()
{
	UnregisterGuiEvents();

	m_progressPulser->Stop();
	delete m_progressPulser;
}

void DatabaseOperationPage::OnThread(wxCommandEvent &event)
{
	int id = event.GetId();

	switch(id)
	{
	case ID_EVENT_OPERATION_COMPLETED:
		{
			bool closeTab = false;

			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			NError exception = operation.GetError();
			if (!exception.IsNull())
			{
				wxExceptionDlg::Show(exception);
				closeTab = true;
			}
			else
			{
				NValue result = operation.GetResult();
				NBiometricTask task = result.ToObject<NBiometricTask>();
				NBiometricStatus status = task.GetStatus();
				exception = task.GetError();
				if (!exception.IsNull())
				{
					wxExceptionDlg::Show(exception);
					closeTab = true;
				}
				else
				{
					wxString strStatus = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					wxString strOperation = wxEmptyString;

					if (task.GetOperations() != nboEnrollWithDuplicateCheck)
					{
						strOperation = NEnum::ToString(NBiometricTask::NBiometricOperationsNativeTypeOf(), task.GetOperations());
					}
					else
					{
						strOperation = wxT("Enroll");
					}

					m_statusPanel->SetMessage(wxString::Format(wxT("%s: %s"), strOperation, strStatus), (status == nbsOk)? StatusPanel::SUCCESS_MESSAGE : StatusPanel::ERROR_MESSAGE);
					if (task.GetOperations() != nboEnroll && task.GetOperations() != nboUpdate &&
						(status == nbsOk || status == nbsDuplicateFound))
					{
						if (m_subject.GetMatchingResults().GetCount() > 0)
						{
							for (int i = 0; i < m_subject.GetMatchingResults().GetCount(); i++)
							{
								bool isLinkActive = true;

								if (SettingsManager::GetDatabaseConnection() == SettingsManager::RemoteMatchingServer)
								{
									isLinkActive = false;
								}

								MatchingResultView *view = new MatchingResultView(this, wxID_ANY);
								view->SetIsLinkActive(isLinkActive);
								view->SetLinkPressedCallback(&DatabaseOperationPage::OnLinkPressedCallback, this);
								view->SetMatchingResult(m_subject.GetMatchingResults().Get(i));
								view->SetMatchingThreshold(m_biometricClient.GetMatchingThreshold());
								m_resultLayout->Add(view, 0, wxALL);
							}
						}
					}
				}
			}

			m_progressPulser->Stop();
			m_progressGauge->Hide();

			this->Layout();
			this->FitInside();

			if (closeTab)
			{
				this->Close();
			}

			break;
		}
	default:
		break;
	};
}

void DatabaseOperationPage::OperationCompletedCallback(EventArgs args)
{
	DatabaseOperationPage *page = static_cast<DatabaseOperationPage *>(args.GetParam());
	wxCommandEvent event(wxEVT_DB_OPERATION_THREAD, ID_EVENT_OPERATION_COMPLETED);
	event.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
	wxPostEvent(page, event);
}

void DatabaseOperationPage::OnLinkPressedCallback(NMatchingResult result, void *param)
{
	DatabaseOperationPage *page = static_cast<DatabaseOperationPage *>(param);
	TabController *tabController = static_cast<TabController *>(page->GetParent());
	MatchingResultPage *matchingResultPage = new MatchingResultPage(tabController, wxID_ANY, page->m_biometricClient);

	NSubject newSubject;
	newSubject.SetId(result.GetId());

	matchingResultPage->SetParameters(page->m_subject, newSubject);
	wxString pageName = wxString::Format(wxT("Matching result: %s"), (wxString)result.GetId());
	if (pageName.Length() > 30)
	{
		pageName = pageName.SubString(0, 30) << wxT("...");
	}
	tabController->AddPage(matchingResultPage, pageName, true);
}

void DatabaseOperationPage::OnSelectPage()
{
	if (m_isOperationStarted)
	{
		return;
	}

	m_isOperationStarted = true;

	NBiometricTask task = m_biometricClient.CreateTask(m_operationType, m_subject, NULL);
	NAsyncOperation operation = m_biometricClient.PerformTaskAsync(task);
	operation.AddCompletedCallback(&DatabaseOperationPage::OperationCompletedCallback, this);
}

void DatabaseOperationPage::OnProgressPulse(wxTimerEvent & /*event*/)
{
	m_progressGauge->Pulse();
}

void DatabaseOperationPage::RegisterGuiEvents()
{
	this->Bind(wxEVT_DB_OPERATION_THREAD, &DatabaseOperationPage::OnThread, this);
}

void DatabaseOperationPage::UnregisterGuiEvents()
{
	this->Unbind(wxEVT_DB_OPERATION_THREAD, &DatabaseOperationPage::OnThread, this);
}

void DatabaseOperationPage::CreateGUIControls()
{
	wxBoxSizer *sizer = new wxBoxSizer(wxVERTICAL);
	this->SetSizer(sizer);

	m_statusPanel = new StatusPanel(this, wxID_ANY);
	m_statusPanel->SetMessage(wxT("Operation is in progress. Please wait ..."), StatusPanel::INFO_MESSAGE);
	sizer->Add(m_statusPanel, 0, wxALL | wxEXPAND);

	m_resultLayout = new wxBoxSizer(wxVERTICAL);
	sizer->Add(m_resultLayout, 1, wxALL | wxEXPAND);

	m_progressGauge = new wxGauge(this, wxID_ANY, 0, wxDefaultPosition, wxDefaultSize);
	m_progressPulser = new wxTimer(this, ID_PROGRESS_PULSER);
	sizer->Add(m_progressGauge, 0, wxALL | wxEXPAND);

	m_progressGauge->SetValue(0);
	m_progressPulser->Start(50);

	this->Layout();
}

}}


#include "Precompiled.h"

#include <Common/LongActionDialog.h>

using namespace Neurotec::Gui;

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_LONG_ACTION_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_LONG_ACTION_THREAD, wxCommandEvent);

BEGIN_EVENT_TABLE(LongActionDialog, wxDialog)
	EVT_TIMER(ID_PROGRESS_PULSER, LongActionDialog::OnProgressPulse)
END_EVENT_TABLE()

LongActionDialog::LongActionDialog(wxWindow *parent, wxWindowID id, const wxString &title) :
	wxDialog(parent, id, title),
	m_actionCallback(NULL),
	m_actionCallbackParam(NULL),
	m_isOperationRunning(false)

{
	CreateGUIControls();
	RegisterGuiEvents();
}

LongActionDialog::~LongActionDialog()
{
	UnregisterGuiEvents();

	m_progressPulser->Stop();
	delete m_progressPulser;
}

void LongActionDialog::SetActionCallback(ActionCallback actionCallback, void *param)
{
	m_actionCallback = actionCallback;
	m_actionCallbackParam = param;
}

void LongActionDialog::OnThread(wxCommandEvent &event)
{
	int id = event.GetId();

	switch(id)
	{
	case ID_EVT_OPERATION_COMPLETED:
		{
			if (event.GetClientData() != NULL)
			{
				NError exception(static_cast<HNObject>(event.GetClientData()), true);

				wxExceptionDlg::Show(exception);

				EndModal(wxID_CANCEL);
			}
			else
			{
				EndModal(wxID_OK);
			}

			break;
		}
	default:
		break;
	};
}

void LongActionDialog::OnOperationCompleteCallback(void *param, NError exception)
{
	LongActionDialog *longActionDialog = reinterpret_cast<LongActionDialog *>(param);
	wxCommandEvent event(wxEVT_LONG_ACTION_THREAD, ID_EVT_OPERATION_COMPLETED);
	event.SetClientData(!exception.IsNull() ? exception.RefHandle() : NULL);
	wxPostEvent(longActionDialog, event);
}

void LongActionDialog::SetMessage(const wxString& message)
{
	m_lblMessage->SetLabel(message);
	this->Layout();
	this->Fit();
}

void LongActionDialog::OnProgressPulse(wxTimerEvent &)
{
	m_progressGauge->Pulse();
}

void LongActionDialog::OnActivate(wxActivateEvent&)
{
	if (m_isOperationRunning)
		return;

	m_isOperationRunning = true;

	ActionWorker *worker = new ActionWorker(m_actionCallback, m_actionCallbackParam);

	worker->SetCompletionCallback(&LongActionDialog::OnOperationCompleteCallback, this);

	worker->Run();
}

void LongActionDialog::OnClose(wxCloseEvent& event)
{
	if (event.CanVeto())
	{
		event.Veto(true);
		return;
	}

	Destroy();
}

void LongActionDialog::RegisterGuiEvents()
{
	this->Bind(wxEVT_LONG_ACTION_THREAD, &LongActionDialog::OnThread, this);
	this->Connect(wxEVT_ACTIVATE, wxActivateEventHandler(LongActionDialog::OnActivate));
	this->Connect(wxEVT_CLOSE_WINDOW, wxCloseEventHandler(LongActionDialog::OnClose));
}

void LongActionDialog::UnregisterGuiEvents()
{
	this->Unbind(wxEVT_LONG_ACTION_THREAD, &LongActionDialog::OnThread, this);
	this->Disconnect(wxEVT_ACTIVATE, wxActivateEventHandler(LongActionDialog::OnActivate));
	this->Disconnect(wxEVT_CLOSE_WINDOW, wxCloseEventHandler(LongActionDialog::OnClose));
}

void LongActionDialog::CreateGUIControls()
{
	wxBoxSizer *sizer = new wxBoxSizer(wxVERTICAL);

	m_progressGauge = new wxGauge(this, wxID_ANY, 200, wxDefaultPosition, wxDefaultSize);
	m_progressPulser = new wxTimer(this, ID_PROGRESS_PULSER);
	m_lblMessage = new wxStaticText(this, wxID_ANY, wxEmptyString);

	m_progressGauge->SetValue(0);
	m_progressPulser->Start(50);

	wxBoxSizer *progessSizer = new wxBoxSizer(wxHORIZONTAL);
	progessSizer->Add(m_progressGauge, 1, wxALL | wxEXPAND);

	sizer->Add(m_lblMessage, 0, wxALL, 5);
	sizer->Add(progessSizer, 1, wxALL | wxEXPAND, 5);

	this->SetMinSize(wxSize(300, -1));

	this->SetWindowStyleFlag(wxCAPTION | wxSTAY_ON_TOP);

	this->SetSizer(sizer, true);
	this->Layout();
	this->Center();
	this->Fit();
}

/**********************************************************
 * ActionWorker
 **********************************************************/

LongActionDialog::ActionWorker::ActionWorker(ActionCallback callback, void *param, wxThreadKind kind) :
	wxThread(kind),
	m_callback(callback),
	m_completeCallback(NULL),
	m_callbackParam(param),
	m_completeCallbackParam(NULL),
	m_exception(static_cast<HNObject>(NULL))
{
}

LongActionDialog::ActionWorker::~ActionWorker()
{
}

wxThread::ExitCode LongActionDialog::ActionWorker::Entry()
{
	if (m_callback != NULL)
	{
		try
		{
			(*m_callback)(m_callbackParam);
		}
		catch (NError& ex)
		{
			m_exception = ex;
		}
	}

	return (wxThread::ExitCode)0;
}

void LongActionDialog::ActionWorker::SetCompletionCallback(CompletionCallback callback, void *param)
{
	m_completeCallback = callback;
	m_completeCallbackParam = param;
}

void LongActionDialog::ActionWorker::OnExit()
{
	if (m_completeCallback != NULL)
	{
		(*m_completeCallback)(m_completeCallbackParam, m_exception);
	}
}

}}


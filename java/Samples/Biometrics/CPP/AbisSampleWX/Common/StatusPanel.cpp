#include "Precompiled.h"

#include <Common/StatusPanel.h>

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_STATUS_PANEL_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_STATUS_PANEL_THREAD, wxCommandEvent);

StatusPanel::StatusPanel(wxWindow *parent, wxWindowID winid) : wxPanel(parent, winid)
{
	CreateGUIControls();

	this->Bind(wxEVT_STATUS_PANEL_THREAD, &StatusPanel::OnThread, this);
}

StatusPanel::~StatusPanel()
{
	this->Unbind(wxEVT_STATUS_PANEL_THREAD, &StatusPanel::OnThread, this);
}

void StatusPanel::SetMessage(wxString text, MessageType type)
{
	int eventId;

	if (type == INFO_MESSAGE)
		eventId = ID_EVENT_SET_INFO_MESSAGE;
	else if (type == ERROR_MESSAGE)
		eventId = ID_EVENT_SET_ERROR_MESSAGE;
	else
		eventId = ID_EVENT_SET_SUCCESS_MESSAGE;

	wxCommandEvent event(wxEVT_STATUS_PANEL_THREAD, eventId);
	event.SetString(text);
	wxPostEvent(this, event);
}

void StatusPanel::OnThread(wxCommandEvent &event)
{
	int id = event.GetId();

	wxColor color = wxColor(255, 165, 0);
	switch(id)
	{
	case ID_EVENT_SET_ERROR_MESSAGE:
		color = wxColor(255, 0, 0);
		break;
	case ID_EVENT_SET_SUCCESS_MESSAGE:
		color = wxColor(0, 100, 0);
		break;
	default:
		break;
	};

	wxString message = event.GetString();
	m_lblMessage->SetLabel(message);
	SetBackgroundColour(color);
	Refresh(true, NULL);
	Layout();
	Update();
}

void StatusPanel::CreateGUIControls()
{
	wxBoxSizer *sizer = new wxBoxSizer(wxVERTICAL);
	this->SetSizer(sizer);

	m_lblMessage = new wxStaticText(this, wxID_ANY, wxEmptyString);
	m_lblMessage->SetForegroundColour(*wxWHITE);
	sizer->Add(m_lblMessage, 0, wxALIGN_CENTER_HORIZONTAL | wxALIGN_CENTER_VERTICAL);

	this->Layout();
}

}}

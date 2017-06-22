#include "Precompiled.h"

#include <Common/TabPage.h>
#include <Common/TabController.h>

namespace Neurotec { namespace Samples
{

TabPage::TabPage(wxWindow *parent, wxWindowID winid) : wxScrolledWindow(static_cast<wxWindow *>(parent), winid)
{
	m_state = NULL;
	m_currentState = OPEN;

	SetScrollRate(1, 1);
}

TabPage::~TabPage()
{
}

void TabPage::SetStateMonitorVariable(State *state)
{
	m_state = state;

	if (m_state != NULL)
		*m_state = m_currentState;
}

void TabPage::Close()
{
	m_currentState = CLOSED;

	if (m_state != NULL)
		*m_state = CLOSED;

	TabController *parent = dynamic_cast<TabController *>(this->GetParent());
	parent->DeletePage(parent->GetPageIndex(this));
}

void TabPage::OnCloseCallback()
{
	m_currentState = CLOSED;

	if (m_state != NULL)
		*m_state = CLOSED;

	OnClose();
}

void TabPage::OnLeavePageCallback()
{
	m_currentState = OPEN;

	if (m_state != NULL)
		*m_state = OPEN;

	OnLeavePage();
}

void TabPage::OnSelectPageCallback()
{
	m_currentState = ACTIVE;

	if (m_state != NULL)
		*m_state = ACTIVE;

	OnSelectPage();
}

void TabPage::OnClose()
{
}

void TabPage::OnLeavePage()
{
}

void TabPage::OnSelectPage()
{
}

void TabPage::SetLabel(const wxString& label)
{
	TabController *parent = dynamic_cast<TabController *>(this->GetParent());
	parent->SetPageText(parent->GetPageIndex(this), label);
}

}}


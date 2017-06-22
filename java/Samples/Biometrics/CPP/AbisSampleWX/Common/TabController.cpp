#include "Precompiled.h"

#include <Common/TabController.h>
#include <Common/TabPage.h>

namespace Neurotec { namespace Samples
{

TabController::TabController(wxWindow *parent, wxWindowID winid) : wxAuiNotebook(parent, winid)
{
	this->Bind(wxEVT_AUINOTEBOOK_PAGE_CHANGED, &TabController::OnPageChanged, this);
	this->Bind(wxEVT_AUINOTEBOOK_PAGE_CLOSE, &TabController::OnPageClose, this);
}

TabController::~TabController()
{
	this->Unbind(wxEVT_AUINOTEBOOK_PAGE_CLOSE, &TabController::OnPageClose, this);
	this->Unbind(wxEVT_AUINOTEBOOK_PAGE_CHANGED, &TabController::OnPageChanged, this);
}

void TabController::CloseAllPages()
{
	int count = GetPageCount();
	for (int i = 0; i < count; i++)
	{
		TabPage *currentPage = reinterpret_cast<TabPage *>(GetPage(i));
		currentPage->OnCloseCallback();
	}
	DeleteAllPages();
}

void TabController::OnPageClose(wxAuiNotebookEvent& event)
{
	int selection = event.GetSelection();

	if (selection > -1)
	{
		TabPage *currentPage = reinterpret_cast<TabPage *>(GetPage(selection));
		currentPage->OnCloseCallback();
	}
}

void TabController::OnPageChanged(wxAuiNotebookEvent& event)
{
	int oldSelection = event.GetOldSelection();
	int selection = event.GetSelection();

	if (oldSelection > -1)
	{
		TabPage *oldPage = reinterpret_cast<TabPage *>(GetPage(oldSelection));
		oldPage->OnLeavePageCallback();
	}

	if (selection > -1)
	{
		TabPage *currentPage = reinterpret_cast<TabPage *>(GetPage(selection));
		currentPage->OnSelectPageCallback();
	}
}

}}


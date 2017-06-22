#ifndef TAB_CONTROLLER_H_INCLUDED
#define TAB_CONTROLLER_H_INCLUDED

namespace Neurotec { namespace Samples
{

class TabController : public wxAuiNotebook
{
public:
	TabController(wxWindow *parent, wxWindowID winid = wxID_ANY);

	virtual ~TabController();

	void CloseAllPages();

private:
	void OnPageChanged(wxAuiNotebookEvent& event);
	void OnPageClose(wxAuiNotebookEvent& event);
};

}}

#endif


#ifndef TAB_PAGE_H_INCLUDED
#define TAB_PAGE_H_INCLUDED

class TabController;

namespace Neurotec { namespace Samples
{

class TabPage : public wxScrolledWindow
{
public:
	typedef enum
	{
		CLOSED,
		OPEN,
		ACTIVE
	} State;

	TabPage(wxWindow *parent, wxWindowID winid = wxID_ANY);

	virtual ~TabPage();

	void Close();

	void SetLabel(const wxString& label);

	void SetStateMonitorVariable(State *state);

protected:
	virtual void OnLeavePage();

	virtual void OnSelectPage();

	virtual void OnClose();

protected:
	State m_currentState;
	State *m_state;

private:
	void OnCloseCallback();

	void OnLeavePageCallback();

	void OnSelectPageCallback();

	friend class TabController;
};

}}

#endif


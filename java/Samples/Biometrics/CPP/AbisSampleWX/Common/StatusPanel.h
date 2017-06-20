#ifndef STATUS_PANEL_H_INCLUDED
#define STATUS_PANEL_H_INCLUDED

namespace Neurotec { namespace Samples
{

class StatusPanel : public wxPanel
{

public:
	typedef enum
	{
		INFO_MESSAGE,
		SUCCESS_MESSAGE,
		ERROR_MESSAGE
	} MessageType;

public:
	StatusPanel(wxWindow *parent, wxWindowID winid = wxID_ANY);

	virtual ~StatusPanel();

	void SetMessage(wxString text, MessageType type = INFO_MESSAGE);

private:
	void OnThread(wxCommandEvent &event);

	void CreateGUIControls();

private:
	enum
	{
		ID_EVENT_SET_INFO_MESSAGE,
		ID_EVENT_SET_SUCCESS_MESSAGE,
		ID_EVENT_SET_ERROR_MESSAGE
	};

	wxStaticText *m_lblMessage;
};

}}

#endif

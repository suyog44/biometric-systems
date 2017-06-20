#ifndef ENROLL_DLG_H_INCLUDED
#define ENROLL_DLG_H_INCLUDED

namespace Neurotec { namespace Samples
{

class EnrollDlg : public wxDialog
{
private:

protected:
	wxStaticText* m_staticText1;
	wxTextCtrl* m_idText;
	wxStdDialogButtonSizer* m_sdbSizer3;
	wxButton* m_sdbSizer3OK;
	wxButton* m_sdbSizer3Cancel;

public:
	EnrollDlg(wxWindow* parent, wxWindowID id = wxID_ANY, const wxString& title = wxT("Enroll user"), const wxPoint& pos = wxDefaultPosition, const wxSize& size = wxSize( 271,162 ), long style = wxDEFAULT_DIALOG_STYLE);
	~EnrollDlg();
	wxString GetUserId();
};

}}

#endif // ENROLL_DLG_H_INCLUDED


///////////////////////////////////////////////////////////////////////////
// C++ code generated with wxFormBuilder (version Apr 16 2008)
// http://www.wxformbuilder.org/
///////////////////////////////////////////////////////////////////////////

#ifndef ENROLL_DLG_H_INCLUDED
#define ENROLL_DLG_H_INCLUDED

///////////////////////////////////////////////////////////////////////////

namespace Neurotec { namespace Samples
{

class EnrollDlg : public wxDialog 
{
private:

protected:
	wxStaticBitmap* m_bitmap1;
	wxStaticText* m_staticText1;
	wxTextCtrl* m_idText;
	wxStdDialogButtonSizer* m_sdbSizer3;
	wxButton* m_sdbSizer3OK;
	wxButton* m_sdbSizer3Cancel;

public:
	EnrollDlg(wxWindow* parent, const wxString& userId, Neurotec::Images::NImage userFace, wxWindowID id = wxID_ANY, const wxString& title = wxT("Enroll user"), const wxPoint& pos = wxDefaultPosition, const wxSize& size = wxSize( 271,162 ), long style = wxDEFAULT_DIALOG_STYLE);
	~EnrollDlg();
	wxString GetUserId();
};

}}

#endif // ENROLL_DLG_H_INCLUDED


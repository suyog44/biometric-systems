///////////////////////////////////////////////////////////////////////////
// C++ code generated with wxFormBuilder (version Apr 16 2008)
// http://www.wxformbuilder.org/
///////////////////////////////////////////////////////////////////////////

#include "Precompiled.h"
#include "EnrollDlg.h"

///////////////////////////////////////////////////////////////////////////

namespace Neurotec { namespace Samples
{

BEGIN_EVENT_TABLE(EnrollDlg, wxDialog)
	EVT_COMMAND(wxID_ANY, wxEVT_COMMAND_TEXT_ENTER, EnrollDlg::OnPressEnter)
END_EVENT_TABLE()

EnrollDlg::EnrollDlg( wxWindow* parent, const wxBitmap& userFinger, wxWindowID id, const wxString& title, const wxPoint& pos, const wxSize& size, long style ) : wxDialog( parent, id, title, pos, size, style )
{
	this->SetSizeHints( wxDefaultSize, wxDefaultSize );

	wxBoxSizer* bSizer1;
	bSizer1 = new wxBoxSizer( wxVERTICAL );

	wxBoxSizer* bSizer2;
	bSizer2 = new wxBoxSizer( wxHORIZONTAL );

	m_bitmap1 = new wxStaticBitmap( this, wxID_ANY, wxNullBitmap, wxDefaultPosition, wxDefaultSize, 0 );
	bSizer2->Add( m_bitmap1, 0, wxALL, 5 );
	m_bitmap1->SetBitmap(userFinger);

	wxBoxSizer* bSizer3;
	bSizer3 = new wxBoxSizer( wxVERTICAL );

	m_staticText1 = new wxStaticText( this, wxID_ANY, wxT("Id:"), wxDefaultPosition, wxDefaultSize, 0 );
	m_staticText1->Wrap( -1 );
	bSizer3->Add( m_staticText1, 0, wxALL, 5 );

	m_idText = new wxTextCtrl( this, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, wxTE_PROCESS_ENTER);
	bSizer3->Add( m_idText, 0, wxALL, 5 );

	bSizer2->Add( bSizer3, 1, wxEXPAND, 5 );

	bSizer1->Add( bSizer2, 1, wxEXPAND, 5 );

	m_sdbSizer3 = new wxStdDialogButtonSizer();
	m_sdbSizer3OK = new wxButton( this, wxID_OK );
	m_sdbSizer3->AddButton( m_sdbSizer3OK );
	m_sdbSizer3Cancel = new wxButton( this, wxID_CANCEL );
	m_sdbSizer3->AddButton( m_sdbSizer3Cancel );
	m_sdbSizer3->Realize();
	bSizer1->Add( m_sdbSizer3, 0, wxEXPAND, 5 );

	this->SetSizer( bSizer1 );
	this->Layout();
}

EnrollDlg::~EnrollDlg()
{
}

wxString EnrollDlg::GetUserId()
{
	return m_idText->GetValue();
}

void EnrollDlg::OnPressEnter(wxCommandEvent&)
{
	EndModal(wxID_OK);
}

}}

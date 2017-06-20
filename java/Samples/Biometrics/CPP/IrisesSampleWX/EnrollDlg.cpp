#include "Precompiled.h"
#include "EnrollDlg.h"

namespace Neurotec { namespace Samples
{

EnrollDlg::EnrollDlg(wxWindow* parent, wxWindowID id, const wxString& title, const wxPoint& pos, const wxSize& size, long style)
	: wxDialog(parent, id, title, pos, size, style)
{
	this->SetSizeHints(wxDefaultSize, wxDefaultSize);

	wxBoxSizer* bSizer1;
	bSizer1 = new wxBoxSizer(wxVERTICAL);

	wxBoxSizer* bSizer3;
	bSizer3 = new wxBoxSizer(wxHORIZONTAL);

	m_staticText1 = new wxStaticText(this, wxID_ANY, wxT("Id:"), wxDefaultPosition, wxDefaultSize, 0);
	m_staticText1->Wrap( -1 );
	bSizer3->Add(m_staticText1, 0, wxALL, 5);

	m_idText = new wxTextCtrl(this, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, 0);
	bSizer3->Add(m_idText, 1, wxALL, 5);

	bSizer1->Add(bSizer3, 0, wxEXPAND | wxALIGN_TOP, 5);

	m_sdbSizer3 = new wxStdDialogButtonSizer();
	m_sdbSizer3OK = new wxButton(this, wxID_OK);
	m_sdbSizer3OK->SetDefault();
	m_sdbSizer3->AddButton(m_sdbSizer3OK);
	m_sdbSizer3Cancel = new wxButton(this, wxID_CANCEL);
	m_sdbSizer3->AddButton(m_sdbSizer3Cancel);
	m_sdbSizer3->Realize();
	bSizer1->Add(m_sdbSizer3, 0, wxEXPAND | wxALIGN_CENTER, 5);

	this->SetSizer(bSizer1);
	this->Layout();
	this->Fit();
}

EnrollDlg::~EnrollDlg()
{
}

wxString EnrollDlg::GetUserId()
{
	return m_idText->GetValue();
}

}}

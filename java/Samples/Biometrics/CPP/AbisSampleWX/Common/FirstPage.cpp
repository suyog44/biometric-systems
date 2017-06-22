#include "Precompiled.h"

#include <AbisSampleForm.h>

#include <Common/FirstPage.h>

namespace Neurotec { namespace Samples
{

FirstPage::FirstPage(wxWindow *parent, wxWindowID id) : TabPage(parent, id)
{
	CreateGuiElements();
	RegisterGuiEvents();
}

FirstPage::~FirstPage()
{
	UnregisterGuiEvents();
}

void FirstPage::OnNewSubjectClick(wxCommandEvent&)
{
	AbisSampleForm *sample = reinterpret_cast<AbisSampleForm *>(this->GetGrandParent());
	sample->CreateSubject();
}

void FirstPage::OnOpenSubjectClick(wxCommandEvent&)
{
	AbisSampleForm *sample = reinterpret_cast<AbisSampleForm *>(this->GetGrandParent());
	sample->OpenSubject();
}

void FirstPage::OnSettingsClick(wxCommandEvent&)
{
	AbisSampleForm *sample = reinterpret_cast<AbisSampleForm *>(this->GetGrandParent());
	sample->Settings();
}

void FirstPage::OnChangeDatabaseClick(wxCommandEvent&)
{
	AbisSampleForm *sample = reinterpret_cast<AbisSampleForm *>(this->GetGrandParent());
	sample->ChangeDatabase();
}

void FirstPage::OnAboutClick(wxCommandEvent&)
{
	AbisSampleForm *sample = reinterpret_cast<AbisSampleForm *>(this->GetGrandParent());
	sample->About();
}

void FirstPage::RegisterGuiEvents()
{
	m_btnNewSubject->Connect(wxEVT_BUTTON, wxCommandEventHandler(FirstPage::OnNewSubjectClick), NULL, this);
	m_btnOpenSubject->Connect(wxEVT_BUTTON, wxCommandEventHandler(FirstPage::OnOpenSubjectClick), NULL, this);
	m_btnSettings->Connect(wxEVT_BUTTON, wxCommandEventHandler(FirstPage::OnSettingsClick), NULL, this);
	m_btnChangeDatabase->Connect(wxEVT_BUTTON, wxCommandEventHandler(FirstPage::OnChangeDatabaseClick), NULL, this);
	m_btnAbout->Connect(wxEVT_BUTTON, wxCommandEventHandler(FirstPage::OnAboutClick), NULL, this);
}

void FirstPage::UnregisterGuiEvents()
{
	m_btnNewSubject->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(FirstPage::OnNewSubjectClick), NULL, this);
	m_btnOpenSubject->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(FirstPage::OnOpenSubjectClick), NULL, this);
	m_btnSettings->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(FirstPage::OnSettingsClick), NULL, this);
	m_btnChangeDatabase->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(FirstPage::OnChangeDatabaseClick), NULL, this);
	m_btnAbout->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(FirstPage::OnAboutClick), NULL, this);
}

void FirstPage::CreateGuiElements()
{
	wxBoxSizer *mainSizer = new wxBoxSizer(wxVERTICAL);
	this->SetSizer(mainSizer);

	wxFlexGridSizer *sizer = new wxFlexGridSizer(4, 2, 10, 10);
	mainSizer->Add(sizer, 0, wxALL | wxEXPAND, 5);

	sizer->SetCols(2);
	sizer->SetRows(4);

	wxBoxSizer *szBox = new wxBoxSizer(wxVERTICAL);
	sizer->Add(szBox, 0, wxLEFT | wxRIGHT | wxALIGN_TOP | wxEXPAND);

	m_btnNewSubject = new wxButton(this, wxID_ANY, wxT("New Subject"));
	szBox->Add(m_btnNewSubject, 0, wxALL | wxEXPAND);

	wxStaticText *text = new wxStaticText(this, wxID_ANY,
		wxString::Format(wxT("Create new subject\n    %s\n    %s"),
			wxT("Capture biometrics (fingers, faces, etc) from devices or create them from files"),
			wxT("Enroll, identify or verify subject using local database or remote matching server")));
	sizer->Add(text, 0, wxALL);

	szBox = new wxBoxSizer(wxVERTICAL);
	sizer->Add(szBox, 0, wxALL | wxALIGN_TOP | wxEXPAND);

	m_btnOpenSubject = new wxButton(this, wxID_ANY, wxT("Open Subject"));
	szBox->Add(m_btnOpenSubject, 0, wxALL | wxEXPAND);

	text = new wxStaticText(this, wxID_ANY,
		wxString::Format(wxT("Open subject template\n    %s\n    %s"),
			wxT("Open from Neurotechnology template or other supported standard templates"),
			wxT("Enroll, identify or verify subject using local database or remote matching server")));
	sizer->Add(text, 0, wxALL);

	szBox = new wxBoxSizer(wxVERTICAL);
	sizer->Add(szBox, 0, wxALL | wxALIGN_TOP | wxEXPAND);

	m_btnSettings = new wxButton(this, wxID_ANY, wxT("Settings"));
	szBox->Add(m_btnSettings, 0, wxALL | wxEXPAND);

	text = new wxStaticText(this, wxID_ANY,
		wxString::Format(wxT("Change settings\n    %s"),
			wxT("Change feature detection, extraction, matching (etc) settings")));
	sizer->Add(text, 0, wxALL);

	szBox = new wxBoxSizer(wxVERTICAL);
	sizer->Add(szBox, 0, wxALL | wxALIGN_TOP | wxEXPAND);

	m_btnChangeDatabase = new wxButton(this, wxID_ANY, wxT("Change Database"));
	szBox->Add(m_btnChangeDatabase, 0, wxALL | wxEXPAND);

	text = new wxStaticText(this, wxID_ANY,
		wxString::Format(wxT("Change database\n    %s"),
			wxT("Configure to use local database or remote matching server")));
	sizer->Add(text, 0, wxALL);

	wxGridSizer *szBottom = new wxGridSizer(1, 2, 0, 0);
	mainSizer->Add(szBottom, 1 , wxALL | wxEXPAND, 5);

	m_btnAbout = new wxButton(this, wxID_ANY, wxT("About"));
	szBottom->Add(m_btnAbout, 0, wxALIGN_BOTTOM | wxALL);

	wxStaticBitmap *bitmap = new wxStaticBitmap(this, wxID_ANY, wxBitmap(NeurotechnologyLogo_XPM));
	szBottom->Add(bitmap, 0, wxALL | wxALIGN_BOTTOM | wxALIGN_RIGHT);

	this->Layout();
}

}}


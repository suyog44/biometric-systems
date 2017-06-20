#include "Precompiled.h"

#include <SubjectEditor/EditPhrasesDialog.h>

#include <Settings/SettingsManager.h>

#include <Resources/AddIcon.xpm>
#include <Resources/RemoveIcon.xpm>

namespace Neurotec { namespace Samples
{

EditPhrasesDialog::EditPhrasesDialog(wxWindow *parent, wxWindowID id, const wxString &title) : wxDialog(parent, id, title)
{
	CreateGUIControls();
	RegisterGuiEvents();
	LoadPhrases();
}

EditPhrasesDialog::~EditPhrasesDialog()
{
	UnregisterGuiEvents();
}

void EditPhrasesDialog::SavePhrases()
{
	SettingsManager::Phrase *phrases = NULL;
	int count = 0;

	count = m_listPhrases->GetItemCount();
	if (count > 0)
	{
		phrases = new SettingsManager::Phrase[count];
	}

	for (int i = 0; i < count; i++)
	{
		long id = -1;
		m_listPhrases->GetItemText(i, 0).ToLong(&id, 10);

		phrases[i].SetId(id);
		phrases[i].SetPhrase(m_listPhrases->GetItemText(i, 1));
	}

	SettingsManager::SetPhrases(phrases, count);

	if (phrases) delete[] phrases;
}

void EditPhrasesDialog::LoadPhrases()
{
	SettingsManager::Phrase *phrases = NULL;
	int count = 0;

	phrases = SettingsManager::GetPhrases(&count);

	m_listPhrases->DeleteAllItems();

	for (int i = 0; i < count; i++)
	{
		int rowIndex = m_listPhrases->InsertItem(m_listPhrases->GetItemCount(), wxString::Format(wxT("%d"), phrases[i].GetId()));
		m_listPhrases->SetItem(rowIndex, 1, phrases[i].GetPhrase());
	}

	if (phrases) delete[] phrases;
}

void EditPhrasesDialog::OnAddClick(wxCommandEvent&)
{
	wxString id = m_txtId->GetValue();
	wxString phrase = m_txtPhrase->GetValue();

	if (id == wxEmptyString || phrase == wxEmptyString)
	{
		wxMessageBox(wxT("One or more fields is empty. Please fill all the fields."), wxT("Add phrase"), wxICON_WARNING);
		return;
	}

	long phraseId = -1;
	if (!id.ToLong(&phraseId, 10) || phraseId < 0)
	{
		wxMessageBox(wxT("Phrase Id must be integer (above 0)!"), wxT("Add phrase"), wxICON_ERROR);

		m_txtId->Clear();

		return;
	}

	for (int i = 0; i < m_listPhrases->GetItemCount(); i++)
	{
		long itemId = 0;
		if (!m_listPhrases->GetItemText(i, 0).ToLong(&itemId, 10))
		{
			continue;
		}

		if (itemId == phraseId)
		{
			wxMessageBox(wxT("Another phrase with the same phrase id already exist in the list!"), wxT("Add phrase"), wxICON_ERROR);

			return;
		}
	}

	int rowIndex = m_listPhrases->InsertItem(m_listPhrases->GetItemCount(), id);
	m_listPhrases->SetItem(rowIndex, 1, phrase);

	m_txtId->Clear();
	m_txtPhrase->Clear();

	SavePhrases();
}

void EditPhrasesDialog::OnRemoveClick(wxCommandEvent&)
{
	int selection = m_listPhrases->GetFirstSelected();

	if (selection < 0)
	{
		return;
	}

	m_listPhrases->DeleteItem(selection);

	SavePhrases();
}

void EditPhrasesDialog::OnCloseClick(wxCommandEvent&)
{
	EndModal(wxID_OK);
}

void EditPhrasesDialog::RegisterGuiEvents()
{
	m_btnAdd->Connect(wxEVT_BUTTON, wxCommandEventHandler(EditPhrasesDialog::OnAddClick), NULL, this);
	m_btnRemove->Connect(wxEVT_BUTTON, wxCommandEventHandler(EditPhrasesDialog::OnRemoveClick), NULL, this);
	m_btnClose->Connect(wxEVT_BUTTON, wxCommandEventHandler(EditPhrasesDialog::OnCloseClick), NULL, this);
}

void EditPhrasesDialog::UnregisterGuiEvents()
{
	m_btnAdd->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(EditPhrasesDialog::OnAddClick), NULL, this);
	m_btnRemove->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(EditPhrasesDialog::OnRemoveClick), NULL, this);
	m_btnClose->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(EditPhrasesDialog::OnCloseClick), NULL, this);
}

void EditPhrasesDialog::CreateGUIControls()
{
	wxBoxSizer *sizer = new wxBoxSizer(wxVERTICAL);
	this->SetSizer(sizer, true);

	wxStaticBoxSizer *szStaticBoxSizer = new wxStaticBoxSizer(new wxStaticBox(this, wxID_ANY, wxEmptyString), wxVERTICAL);
	sizer->Add(szStaticBoxSizer, 0, wxALL | wxEXPAND, 5);

	m_listPhrases = new wxListView(this, wxID_ANY);
	m_listPhrases->SetWindowStyle(m_listPhrases->GetWindowStyle() | wxLC_SINGLE_SEL);
	szStaticBoxSizer->Add(m_listPhrases, 1, wxALL | wxEXPAND);

	wxListItem listItemId;
	wxListItem listItemPhrase;

	listItemId.SetWidth(-1);
	listItemPhrase.SetWidth(200);

	listItemId.SetText(wxT("Phrase Id"));
	listItemPhrase.SetText(wxT("Phrase"));

	m_listPhrases->InsertColumn(0, listItemId);
	m_listPhrases->InsertColumn(1, listItemPhrase);

	m_listPhrases->Fit();

	m_btnRemove = new wxButton(this, wxID_ANY, wxEmptyString);
	m_btnRemove->SetBitmap(wxImage(wxImage(removeIcon_xpm)));
	m_btnRemove->Fit();
	szStaticBoxSizer->Add(m_btnRemove, 0, wxTOP | wxALIGN_RIGHT, 5);

	szStaticBoxSizer = new wxStaticBoxSizer(new wxStaticBox(this, wxID_ANY, wxT("Add new")), wxVERTICAL);
	sizer->Add(szStaticBoxSizer, 0, wxALL | wxEXPAND, 5);

	wxFlexGridSizer *szFlexGrid = new wxFlexGridSizer(2, 2, 5, 5);
	szFlexGrid->SetFlexibleDirection(wxBOTH);
	szFlexGrid->AddGrowableCol(1);
	szStaticBoxSizer->Add(szFlexGrid, 1, wxALL | wxEXPAND);

	wxStaticText *text = new wxStaticText(this, wxID_ANY, wxT("Phrase Id:"));
	szFlexGrid->Add(text, 0, wxALL | wxALIGN_CENTER_VERTICAL);

	m_txtId = new wxTextCtrl(this, wxID_ANY);
	szFlexGrid->Add(m_txtId, 1, wxALL | wxEXPAND);

	text = new wxStaticText(this, wxID_ANY, wxT("Phrase:"));
	szFlexGrid->Add(text, 0, wxALL | wxALIGN_CENTER_VERTICAL);

	m_txtPhrase = new wxTextCtrl(this, wxID_ANY);
	szFlexGrid->Add(m_txtPhrase, 1, wxALL | wxEXPAND);

	m_btnAdd = new wxButton(this, wxID_ANY, wxEmptyString);
	m_btnAdd->SetBitmap(wxImage(wxImage(addIcon_xpm)));
	m_btnAdd->Fit();
	szStaticBoxSizer->Add(m_btnAdd, 0, wxTOP | wxALIGN_RIGHT, 5);

	m_btnClose = new wxButton(this, wxID_ANY, wxT("Close"));
	sizer->Add(m_btnClose, 0, wxALL | wxALIGN_RIGHT, 5);

	this->Center();
	this->Layout();
	this->Fit();
}

}}


#ifndef EDIT_PHRASES_DIALOG_H_INCLUDED
#define EDIT_PHRASES_DIALOG_H_INCLUDED

namespace Neurotec { namespace Samples
{

class EditPhrasesDialog : public wxDialog
{

public:
	EditPhrasesDialog(wxWindow *parent, wxWindowID id, const wxString &title);

	virtual ~EditPhrasesDialog();

private:
	void LoadPhrases();

	void SavePhrases();

	void OnAddClick(wxCommandEvent& event);

	void OnRemoveClick(wxCommandEvent& event);

	void OnCloseClick(wxCommandEvent& event);

	void RegisterGuiEvents();

	void UnregisterGuiEvents();

	void CreateGUIControls();

private:
	wxListView *m_listPhrases;
	wxButton *m_btnRemove;
	wxButton *m_btnAdd;
	wxButton *m_btnClose;
	wxTextCtrl *m_txtId;
	wxTextCtrl *m_txtPhrase;
};

}}

#endif


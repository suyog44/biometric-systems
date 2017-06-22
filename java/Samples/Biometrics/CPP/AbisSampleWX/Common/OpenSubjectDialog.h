#ifndef OPEN_SUBJECT_DIALOG_H_INCLUDED
#define OPEN_SUBJECT_DIALOG_H_INCLUDED

namespace Neurotec { namespace Samples
{

class OpenSubjectDialog : public wxDialog
{
public:
	OpenSubjectDialog(wxWindow *parent, wxWindowID id, wxString title);

	virtual ~OpenSubjectDialog();

	wxString GetFilePath();

	NUShort GetFormatOwner();

	NUShort GetFormatType();

private:
	void ListOwners();

	void ListTypes();

	void OnOkClick(wxCommandEvent& event);

	void OnCancelClick(wxCommandEvent& event);

	void OnOpenFileClick(wxCommandEvent& event);

	void OnOwnerSelect(wxCommandEvent& event);

	void CreateGuiElements();

	void RegisterGuiEvents();

	void UnregisterGuiEvents();

private:
	struct ListItem
	{
		NUShort value;
		wxString name;
	};

	struct FormatItem
	{
		NUShort owner;
		NUShort value;
		wxString name;
	};

	static ListItem m_owners[];
	static FormatItem m_formats[];

	wxTextCtrl *m_txtFileName;
	wxChoice *m_choiceFormatOwner;
	wxChoice *m_choiceFormatType;
	wxButton *m_btnOpenFile;
	wxButton *m_btnOk;
	wxButton *m_btnCancel;
	wxString m_filePath;
};

}}

#endif

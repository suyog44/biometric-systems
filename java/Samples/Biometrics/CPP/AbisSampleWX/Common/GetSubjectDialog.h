#ifndef GET_SUBJECT_DIALOG_H_INCLUDED
#define GET_SUBJECT_DIALOG_H_INCLUDED

namespace Neurotec { namespace Samples
{

class GetSubjectDialog : public wxDialog
{
public:
	GetSubjectDialog(::Neurotec::Biometrics::Client::NBiometricClient client,
		wxWindow *parent, wxWindowID id, const wxString &title = wxT("Get subject"),
		const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize,
		long style = wxDEFAULT_DIALOG_STYLE, const wxString &name = wxDialogNameStr);

	~GetSubjectDialog();

	::Neurotec::Biometrics::NSubject GetSubject() const;

private:
	void RegisterGuiEvents();
	void UnregisterGuiEvents();
	void CreateGUIControls();
	void FillAutoCompletion();
	void CheckAndReturn();

private:
	void OnOkClick(wxCommandEvent & event);
	void OnCancelClick(wxCommandEvent & event);
	void OnKeyDown(wxKeyEvent & event);

private:
	wxButton * m_btnOk;
	wxButton * m_btnCancel;
	wxTextCtrl * m_txtSubjectId;

	::Neurotec::Biometrics::NSubject m_subject;
	::Neurotec::Biometrics::Client::NBiometricClient m_client;

	DECLARE_EVENT_TABLE();
};

}}

#endif

#ifndef CHANGE_DATABASE_DIALOG_H_INCLUDED
#define CHANGE_DATABASE_DIALOG_H_INCLUDED

#include <Common/LongActionDialog.h>
#include <Settings/SettingsManager.h>

namespace Neurotec { namespace Samples
{

class ChangeDatabaseDialog : public wxDialog
{
public:
	ChangeDatabaseDialog(::Neurotec::Biometrics::Client::NBiometricClient& biometricClient, wxWindow *parent, wxWindowID id,
		const wxString &title, const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize,
		long style = wxDEFAULT_DIALOG_STYLE, const wxString &name = wxDialogNameStr);

	virtual ~ChangeDatabaseDialog();

private:
	void OnDatabaseConnectionSelected(wxCommandEvent & event);
	void OnOkClick(wxCommandEvent & event);
	void OnCancelClick(wxCommandEvent & event);
	void OnEditClick(wxCommandEvent & event);
	void OnCurrentSchemaIndexChanged(wxCommandEvent & event);

	SampleDbSchema GetSelectedSchema();
	void ApplyValues(int connectionType);
	void FillForm();
	void Update();
	void DisableControls();
	void RegisterGuiEvents();
	void UnregisterGuiEvents();
	void CreateGUIControls();

	static void InitializeBiometricClient(void *object);

private:
	::Neurotec::Biometrics::Client::NBiometricClient& m_biometricClient;
	bool m_isRemoteServerChecked;
	int m_operationsIndex;
	SchemaVector m_schemas;

	wxRadioButton * m_radioSQLite;
	wxRadioButton * m_radioOdbc;
	wxRadioButton * m_radioRemoteMatchingServer;
	wxCheckBox * m_chkClearAllData;
	wxTextCtrl * m_txtConnectionString;
	wxComboBox * m_comboTables;
	wxTextCtrl * m_txtServerAddress;
	wxSpinCtrl * m_spinClientPort;
	wxSpinCtrl * m_spinAdminPort;
	wxButton * m_btnOk;
	wxButton * m_btnCancel;
	wxButton * m_btnEdit;
	wxComboBox * m_cbSchema;
	wxComboBox * m_cbLocalOperation;
};

}}

#endif


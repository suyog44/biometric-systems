#include "Precompiled.h"

#include <Common/ChangeDatabaseDialog.h>
#include <Common/LongActionDialog.h>
#include <Common/SchemaBuilderDialog.h>
#include <Common/ImagePanel.h>

#include <Settings/SettingsManager.h>

#include <Resources/HelpIcon.xpm>

using namespace ::Neurotec::Biometrics;
using namespace ::Neurotec::Biometrics::Client;
using namespace ::Neurotec::Gui;

namespace Neurotec { namespace Samples
{

ChangeDatabaseDialog::ChangeDatabaseDialog(NBiometricClient& biometricClient, wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &pos,
	const wxSize &size, long style, const wxString &name) : wxDialog(parent, id, title, pos, size, style, name), m_biometricClient(biometricClient)
{
	CreateGUIControls();
	RegisterGuiEvents();

	FillForm();
}

ChangeDatabaseDialog::~ChangeDatabaseDialog()
{
	UnregisterGuiEvents();
}

void ChangeDatabaseDialog::OnDatabaseConnectionSelected(wxCommandEvent&)
{
	Update();
}

SampleDbSchema ChangeDatabaseDialog::GetSelectedSchema()
{
	SampleDbSchema selected = SampleDbSchema::GetEmpty();
	int index = m_cbSchema->GetSelection();
	if (index < (int)m_schemas.size())
		selected = m_schemas[index];
	return selected;
}

void ChangeDatabaseDialog::ApplyValues(int connectionType)
{
	switch (connectionType)
	{
	default:
	case SettingsManager::SQLiteDatabase:
		{
			wxString dbPath = wxSampleConfig::GetUserDataDir() + wxFileName::GetPathSeparator() + wxT("BiometricsV5.db");
			m_biometricClient.SetDatabaseConnectionToSQLite(dbPath);
			break;
		}
	case SettingsManager::OdbcDatabase:
		m_biometricClient.SetDatabaseConnectionToOdbc(m_txtConnectionString->GetValue(), m_comboTables->GetValue());
		break;
	case SettingsManager::RemoteMatchingServer:
		{
			int port = m_spinClientPort->GetValue();
			int adminPort = m_spinAdminPort->GetValue();
			wxString host = m_txtServerAddress->GetValue();
			m_biometricClient.GetRemoteConnections().AddToCluster(host, port, adminPort);
			break;
		}
	};

	SampleDbSchema selected = GetSelectedSchema();
	if (!selected.IsEmpty())
	{
		m_biometricClient.SetBiographicDataSchema(selected.biographicData);
		m_biometricClient.SetCustomDataSchema(selected.customData);
	}
}

void ChangeDatabaseDialog::InitializeBiometricClient(void *object)
{
	ChangeDatabaseDialog *dialog = reinterpret_cast<ChangeDatabaseDialog *>(object);

	if (dialog == NULL) return;

	dialog->m_biometricClient.SetUseDeviceManager(true);
	dialog->m_biometricClient.Initialize();

	if (dialog->m_isRemoteServerChecked)
	{
		NBiometricOperations localOperations = nboNone;
		NBiometricOperations operations[] =
		{
			nboNone,
			nboDetect,
			nboDetectSegments,
			nboSegment,
			nboAssessQuality,
			nboCreateTemplate
		};
		for (int i = 0; i <= dialog->m_operationsIndex; i++)
		{
			localOperations = (NBiometricOperations)((int)localOperations | (int)operations[i]);
		}
		dialog->m_biometricClient.SetLocalOperations(localOperations);
	}

	SettingsManager::LoadSettings(dialog->m_biometricClient);
	dialog->m_biometricClient.SetUseDeviceManager(true);

	if (!dialog->m_radioRemoteMatchingServer->GetValue() && dialog->m_chkClearAllData->GetValue())
		dialog->m_biometricClient.Clear();
}

void ChangeDatabaseDialog::OnOkClick(wxCommandEvent&)
{
	SampleDbSchema schema = GetSelectedSchema();
	if (m_radioRemoteMatchingServer->GetValue())
	{
		if (schema.HasCustomData())
		{
			wxString columns = wxEmptyString;
			int count = schema.customData.GetElements().GetCount();
			for (int i = 0; i < count; i++)
			{
				columns += schema.customData.GetElements()[i].GetName();
				if (i + 1 != count) columns += wxT(", ");
			}
			wxString msg = wxT("Current schema contains custom data (Columns: %s). Only biographic data is supported with remote matching server. Please select different schema or edit current one.");
			wxMessageBox(wxString::Format(msg, columns));
			return;
		}
	}

	if (!m_radioSQLite->GetValue() && SettingsManager::GetWarnHasSchema() && !schema.IsEmpty())
	{
		wxString msg = wxT("Please note that for biometric client will not automaticly create columns specified in database schema for odbc connection or matching server.")
					   wxT(" User must ensure that columns specified in schema exists. Continue anyway?");
		if (wxMessageBox(msg, wxMessageBoxCaptionStr, wxCENTRE | wxYES | wxNO) == wxYES)
		{
			SettingsManager::SetWarnHasSchema(false);
		}
		else
		{
			return;
		}
	}

	int connection = 0;
	if (m_radioOdbc->GetValue())
		connection = SettingsManager::OdbcDatabase;
	else if (m_radioSQLite->GetValue())
		connection = SettingsManager::SQLiteDatabase;
	else if (m_radioRemoteMatchingServer->GetValue())
		connection = SettingsManager::RemoteMatchingServer;

	try
	{
		m_biometricClient = NBiometricClient();
		ApplyValues(connection);
		m_isRemoteServerChecked = m_radioRemoteMatchingServer->GetValue();
		m_operationsIndex = m_cbLocalOperation->GetSelection();

		LongActionDialog longActionDialog(this, wxID_ANY, wxT("Working..."));
		longActionDialog.SetMessage(wxT("Initializing biometric client ..."));
		longActionDialog.SetActionCallback(ChangeDatabaseDialog::InitializeBiometricClient, this);
		if (longActionDialog.ShowModal() != wxID_OK) return;
	}
	catch(NError & error)
	{
		wxExceptionDlg::Show(error);
		m_biometricClient = NULL;
		return;
	}

	SettingsManager::SetRemoteServerAddress(m_txtServerAddress->GetValue());
	SettingsManager::SetRemoteServerAdminPort(m_spinAdminPort->GetValue());
	SettingsManager::SetRemoteServerPort(m_spinClientPort->GetValue());
	SettingsManager::SetTableName(m_comboTables->GetValue());
	SettingsManager::SetOdbcConnectionString(m_txtConnectionString->GetValue());

	SettingsManager::SetDatabaseConnection(connection);
	SettingsManager::SetCurrentSchemaIndex(schema.IsEmpty() ? -1 : m_cbSchema->GetSelection());
	SettingsManager::SetLocalOperationsIndex(m_cbLocalOperation->GetSelection());

	EndModal(wxID_OK);
}

void ChangeDatabaseDialog::OnCancelClick(wxCommandEvent&)
{
	EndModal(wxID_CANCEL);
}

void ChangeDatabaseDialog::OnEditClick(wxCommandEvent & /*event*/)
{
	SchemaBuilderDialog dialog(this);
	SampleDbSchema current = GetSelectedSchema();
	if (!current.IsEmpty())
	{
		dialog.SetSchema(current);
		if (dialog.ShowModal() == wxID_OK)
		{
			int index = m_cbSchema->GetSelection();
			current = dialog.GetSchema();
			m_schemas[index] = current;
			SettingsManager::SetSchemas(m_schemas);
		}
	}
}

void ChangeDatabaseDialog::OnCurrentSchemaIndexChanged(wxCommandEvent & /*event*/)
{
	SampleDbSchema schema = GetSelectedSchema();
	m_btnEdit->Enable(!schema.IsEmpty());
}

void ChangeDatabaseDialog::FillForm()
{
	DisableControls();

	m_schemas = SettingsManager::GetSchemas();
	for (SchemaVector::iterator it = m_schemas.begin(); it != m_schemas.end(); it++)
	{
		m_cbSchema->Append(it->schemaName);
	}
	m_cbSchema->Append(SampleDbSchema::GetEmpty().schemaName);

	int index = SettingsManager::GetCurrentSchemaIndex();
	m_cbSchema->SetSelection(index != -1 ? index : (int)m_schemas.size());

	switch(SettingsManager::GetDatabaseConnection())
	{
	case SettingsManager::SQLiteDatabase:
		m_radioSQLite->SetValue(true);
		break;
	case SettingsManager::OdbcDatabase:
		m_radioOdbc->SetValue(true);
		break;
	case SettingsManager::RemoteMatchingServer:
		m_radioRemoteMatchingServer->SetValue(true);
		break;
	default:
		break;
	};

	m_txtConnectionString->SetValue(SettingsManager::GetOdbcConnectionString());
	m_comboTables->SetValue(SettingsManager::GetTableName());
	m_txtServerAddress->SetValue(SettingsManager::GetRemoteServerAddress());
	m_spinAdminPort->SetValue(SettingsManager::GetRemoteServerAdminPort());
	m_spinClientPort->SetValue(SettingsManager::GetRemoteServerPort());
	m_cbLocalOperation->SetSelection(SettingsManager::GetLocalOperationsIndex());

	Update();
}

void ChangeDatabaseDialog::Update()
{
	DisableControls();

	if (m_radioSQLite->GetValue())
	{
		m_chkClearAllData->Enable(true);
	}
	else if (m_radioOdbc->GetValue())
	{
		m_txtConnectionString->Enable(true);
		m_comboTables->Enable(true);
		m_chkClearAllData->Enable(true);
	}
	else
	{
		m_txtServerAddress->Enable(true);
		m_spinClientPort->Enable(true);
		m_spinAdminPort->Enable(true);
		m_cbLocalOperation->Enable(true);
	}
	m_cbSchema->Enable(true);
	m_btnEdit->Enable(true);
}

void ChangeDatabaseDialog::RegisterGuiEvents()
{
	m_radioSQLite->Connect(wxEVT_RADIOBUTTON, wxCommandEventHandler(ChangeDatabaseDialog::OnDatabaseConnectionSelected), NULL, this);
	m_radioOdbc->Connect(wxEVT_RADIOBUTTON, wxCommandEventHandler(ChangeDatabaseDialog::OnDatabaseConnectionSelected), NULL, this);
	m_radioRemoteMatchingServer->Connect(wxEVT_RADIOBUTTON, wxCommandEventHandler(ChangeDatabaseDialog::OnDatabaseConnectionSelected), NULL, this);
	m_btnOk->Connect(wxEVT_BUTTON, wxCommandEventHandler(ChangeDatabaseDialog::OnOkClick), NULL, this);
	m_btnCancel->Connect(wxEVT_BUTTON, wxCommandEventHandler(ChangeDatabaseDialog::OnCancelClick), NULL, this);
	m_btnEdit->Connect(wxEVT_BUTTON, wxCommandEventHandler(ChangeDatabaseDialog::OnEditClick), NULL, this);
	m_cbSchema->Connect(wxEVT_COMBOBOX, wxCommandEventHandler(ChangeDatabaseDialog::OnCurrentSchemaIndexChanged), NULL, this);
}

void ChangeDatabaseDialog::UnregisterGuiEvents()
{
	m_radioSQLite->Disconnect(wxEVT_RADIOBUTTON, wxCommandEventHandler(ChangeDatabaseDialog::OnDatabaseConnectionSelected), NULL, this);
	m_radioOdbc->Disconnect(wxEVT_RADIOBUTTON, wxCommandEventHandler(ChangeDatabaseDialog::OnDatabaseConnectionSelected), NULL, this);
	m_radioRemoteMatchingServer->Disconnect(wxEVT_RADIOBUTTON, wxCommandEventHandler(ChangeDatabaseDialog::OnDatabaseConnectionSelected), NULL, this);
	m_btnOk->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(ChangeDatabaseDialog::OnOkClick), NULL, this);
	m_btnCancel->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(ChangeDatabaseDialog::OnCancelClick), NULL, this);
	m_btnEdit->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(ChangeDatabaseDialog::OnEditClick), NULL, this);
	m_cbSchema->Disconnect(wxEVT_COMBOBOX, wxCommandEventHandler(ChangeDatabaseDialog::OnCurrentSchemaIndexChanged), NULL, this);
}

void ChangeDatabaseDialog::DisableControls()
{
	m_chkClearAllData->Enable(false);
	m_txtConnectionString->Enable(false);
	m_comboTables->Enable(false);
	m_txtServerAddress->Enable(false);
	m_spinClientPort->Enable(false);
	m_spinAdminPort->Enable(false);
	m_btnEdit->Enable(false);
	m_cbSchema->Enable(false);
	m_cbLocalOperation->Enable(false);
}

void ChangeDatabaseDialog::CreateGUIControls()
{
	wxBoxSizer *sizer = new wxBoxSizer(wxVERTICAL);
	wxGridBagSizer *gridSizer = new wxGridBagSizer();

	m_radioSQLite = new wxRadioButton(this, wxID_ANY, wxT("SQLite database connection"));
	m_radioOdbc = new wxRadioButton(this, wxID_ANY, wxT("Odbc database connection"));
	m_radioRemoteMatchingServer = new wxRadioButton(this, wxID_ANY, wxT("Remote matching server"));
	m_txtConnectionString = new wxTextCtrl(this, wxID_ANY, wxEmptyString);
	m_comboTables = new wxComboBox(this, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, 0, NULL, wxCB_DROPDOWN);
	m_txtServerAddress = new wxTextCtrl(this, wxID_ANY);

	m_spinAdminPort = new wxSpinCtrl(this);
	m_spinAdminPort->SetRange(0, N_UINT16_MAX);

	m_spinClientPort = new wxSpinCtrl(this);
	m_spinClientPort->SetRange(0, N_UINT16_MAX);

	m_chkClearAllData = new wxCheckBox(this, wxID_ANY, wxT("Clear all data"));
	m_btnOk = new wxButton(this, wxID_ANY, wxT("OK"));
	m_btnCancel = new wxButton(this, wxID_ANY, wxT("Cancel"));
	m_btnEdit = new wxButton(this, wxID_ANY, wxT("Edit"));
	m_cbSchema = new wxComboBox(this, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, 0, NULL, wxCB_DROPDOWN | wxCB_READONLY);

	wxString localOperations[] =
	{
		wxT("None"),
		wxT("Detect"),
		wxT("Detect - DetectSegments"),
		wxT("Detect - Segment"),
		wxT("Detect - AssessQuality"),
		wxT("All")
	};
	m_cbLocalOperation = new wxComboBox(this, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize,
		sizeof(localOperations)/sizeof(localOperations[0]), localOperations, wxCB_DROPDOWN | wxCB_READONLY);

	ImagePanel * helpImage = new ImagePanel(this, wxImage(helpIcon_xpm), wxID_ANY, wxDefaultPosition, wxSize(20, 20));
	helpImage->SetToolTip(wxT("New versions of NServer (Since 6.0 line) can perform biometric operations.")
		wxT("You can select which operations will be performed localy and which should be executed on NServer.")
		wxT("For better performance it is recomended to perform detect operation locally when capturing face from camera."));
	helpImage->GetToolTip()->SetAutoPop(24000);

	gridSizer->Add(m_radioSQLite, wxGBPosition(0, 0), wxGBSpan(1, 4), wxALL, 5);

	gridSizer->Add(m_radioOdbc, wxGBPosition(1, 0), wxGBSpan(1, 4), wxALL, 5);
	gridSizer->Add(new wxStaticText(this, wxID_ANY, wxT("Connection string:")), wxGBPosition(2, 1), wxGBSpan(1,1),
		wxALIGN_RIGHT | wxALL | wxALIGN_CENTER_VERTICAL, 5);
	gridSizer->Add(m_txtConnectionString, wxGBPosition(2, 2), wxGBSpan(1, 2), wxGROW | wxALL | wxEXPAND, 5);
	gridSizer->Add(new wxStaticText(this, wxID_ANY, wxT("Example: Dsn=mysql dsn; UID=user; PWD=password")),
		wxGBPosition(3, 1), wxGBSpan(1, 3), wxALIGN_CENTRE_HORIZONTAL | wxEXPAND, 5);
	gridSizer->Add(new wxStaticText(this, wxID_ANY, wxT("Table name:")), wxGBPosition(4, 1), wxGBSpan(1,1),
		wxALIGN_RIGHT | wxALL | wxALIGN_CENTER_VERTICAL, 5);
	gridSizer->Add(m_comboTables, wxGBPosition(4, 2), wxGBSpan(1,2), wxEXPAND | wxALL, 5);
	gridSizer->Add(m_radioRemoteMatchingServer, wxGBPosition(5, 0), wxGBSpan(1, 4), wxALL, 5);
	gridSizer->Add(new wxStaticText(this, wxID_ANY, wxT("Server address:")), wxGBPosition(6, 1), wxGBSpan(1,1),
		wxALIGN_RIGHT | wxALL | wxALIGN_CENTER_VERTICAL, 5);
	gridSizer->Add(m_txtServerAddress, wxGBPosition(6, 2), wxGBSpan(1,2), wxGROW | wxALL | wxEXPAND, 5);
	gridSizer->Add(new wxStaticText(this, wxID_ANY, wxT("Client port:")), wxGBPosition(7, 1), wxGBSpan(1,1),
		wxALIGN_RIGHT | wxALL | wxALIGN_CENTER_VERTICAL, 5);
	gridSizer->Add(m_spinClientPort, wxGBPosition(7, 2), wxGBSpan(1,1), wxALL, 5);
	gridSizer->Add(new wxStaticText(this, wxID_ANY, wxT("Admin port:")), wxGBPosition(8, 1), wxGBSpan(1,1),
		wxALIGN_RIGHT | wxALL | wxALIGN_CENTER_VERTICAL, 5);
	gridSizer->Add(m_spinAdminPort, wxGBPosition(8, 2), wxGBSpan(1,1), wxALL, 5);
	gridSizer->Add(new wxStaticText(this, wxID_ANY, wxT("Local operations:")), wxGBPosition(9, 1), wxDefaultSpan, wxALL | wxALIGN_CENTER);
	gridSizer->Add(m_cbLocalOperation, wxGBPosition(9, 2), wxDefaultSpan, wxALL | wxEXPAND | wxALIGN_CENTER);
	gridSizer->Add(helpImage, wxGBPosition(9, 3), wxGBSpan(1,1), wxLEFT | wxALIGN_CENTER_VERTICAL, 5);
	gridSizer->Add(m_chkClearAllData, wxGBPosition(10, 0), wxGBSpan(1,4));
	gridSizer->Add(new wxStaticText(this, wxID_ANY, wxT("Database schema:")), wxGBPosition(11, 1), wxDefaultSpan, wxALL | wxALIGN_CENTER);
	gridSizer->Add(m_cbSchema, wxGBPosition(11, 2), wxDefaultSpan, wxALL | wxEXPAND | wxALIGN_CENTER);
	gridSizer->Add(m_btnEdit, wxGBPosition(11, 3), wxDefaultSpan, wxALL | wxEXPAND | wxALIGN_CENTER);

	gridSizer->AddGrowableCol(2);
	gridSizer->SetMinSize(430, -1);

	wxBoxSizer *controlsSizer = new wxBoxSizer(wxHORIZONTAL);
	controlsSizer->Add(m_btnOk, 0, wxRIGHT, 5);
	controlsSizer->Add(m_btnCancel, 0);

	sizer->Add(gridSizer, 1, wxALL | wxEXPAND, 5);
	sizer->Add(controlsSizer, 0, wxALL | wxALIGN_RIGHT, 5);

	this->SetSizer(sizer);
	this->Layout();
	sizer->Fit(this);
	this->Center();
}

}}


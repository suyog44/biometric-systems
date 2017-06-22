#include "Precompiled.h"

#include <Common/GetSubjectDialog.h>

using namespace ::Neurotec::Biometrics;
using namespace ::Neurotec::Biometrics::Client;
using namespace ::Neurotec::Gui;

namespace Neurotec { namespace Samples
{

BEGIN_EVENT_TABLE(GetSubjectDialog, wxDialog)
	EVT_KEY_DOWN(GetSubjectDialog::OnKeyDown)
END_EVENT_TABLE()

GetSubjectDialog::GetSubjectDialog(NBiometricClient client, wxWindow *parent, wxWindowID id,
	const wxString &title, const wxPoint &pos, const wxSize &size, long style, const wxString &name)
	: wxDialog(parent, id, title, pos, size, style, name),
	m_client(client)
{
	CreateGUIControls();
	FillAutoCompletion();
	RegisterGuiEvents();
}

GetSubjectDialog::~GetSubjectDialog()
{
	UnregisterGuiEvents();
}

NSubject GetSubjectDialog::GetSubject() const
{
	return m_subject;
}

void GetSubjectDialog::FillAutoCompletion()
{
	try
	{
		NBiometricOperations operations = m_client.GetLocalOperations();
		if (m_client.GetRemoteConnections().GetCount() > 0)
			operations = (NBiometricOperations)((int)operations | (int)m_client.GetRemoteConnections().Get(0).GetOperations());

		if ((operations & nboListIds) == nboListIds)
		{
			wxArrayString arr;
			NArrayWrapper<NString> ids = m_client.ListIds();
			for (int i=0; i< ids.GetCount(); i++)
			{
				arr.Add(ids.Get(i));
			}
			m_txtSubjectId->AutoComplete(arr);
		}
	}
	catch (NError & error)
	{
		wxExceptionDlg::Show(error);
	}
}

void GetSubjectDialog::RegisterGuiEvents()
{
	m_btnOk->Connect(wxEVT_BUTTON, wxCommandEventHandler(GetSubjectDialog::OnOkClick), NULL, this);
	m_btnCancel->Connect(wxEVT_BUTTON, wxCommandEventHandler(GetSubjectDialog::OnCancelClick), NULL, this);
}

void GetSubjectDialog::UnregisterGuiEvents()
{
	m_btnOk->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(GetSubjectDialog::OnOkClick), NULL, this);
	m_btnCancel->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(GetSubjectDialog::OnCancelClick), NULL, this);
}

void GetSubjectDialog::CreateGUIControls()
{
	wxGridBagSizer *sizer = new wxGridBagSizer();

	m_btnOk = new wxButton(this, wxID_ANY, wxT("OK"));
	m_btnCancel = new wxButton(this, wxID_ANY, wxT("Cancel"));
	m_txtSubjectId = new wxTextCtrl(this, wxID_ANY);
	m_txtSubjectId->SetFocus();

	sizer->Add(new wxStaticText(this, wxID_ANY, wxT("Subject Id:")), wxGBPosition(0, 0), wxGBSpan(1,1),
		wxALIGN_RIGHT | wxALL | wxALIGN_CENTER_VERTICAL, 5);
	sizer->Add(m_txtSubjectId, wxGBPosition(0, 1), wxGBSpan(1, 2), wxALL | wxEXPAND, 5);
	sizer->Add(m_btnOk, wxGBPosition(1, 1), wxGBSpan(1,1), wxALL | wxALIGN_RIGHT, 5);
	sizer->Add(m_btnCancel, wxGBPosition(1, 2), wxGBSpan(1,1), wxALL, 5);

	sizer->AddGrowableCol(1);
	sizer->SetMinSize(324, -1);

	this->SetSizer(sizer);
	this->Layout();
	sizer->Fit(this);
	this->Center();
}

void GetSubjectDialog::CheckAndReturn()
{
	try
	{
		NSubject subj;
		subj.SetId(m_txtSubjectId->GetValue());

		NBiometricStatus status = m_client.Get(subj);
		if (status != nbsOk)
		{
			wxString strStatus = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
			wxMessageBox(wxT("Failed to retrieve subject. Status: ") + strStatus);
		}
		else
		{
			m_subject = subj;
			EndModal(wxID_OK);
			return;
		}
	}
	catch (NError & error)
	{
		wxExceptionDlg::Show(error);
	}

	EndModal(wxID_ABORT);
}

/* events */
void GetSubjectDialog::OnOkClick(wxCommandEvent&)
{
	CheckAndReturn();
}

void GetSubjectDialog::OnCancelClick(wxCommandEvent&)
{
	EndModal(wxID_CANCEL);
}

void GetSubjectDialog::OnKeyDown(wxKeyEvent& event)
{
	if (event.GetKeyCode() == WXK_RETURN)
		CheckAndReturn();
}

}}

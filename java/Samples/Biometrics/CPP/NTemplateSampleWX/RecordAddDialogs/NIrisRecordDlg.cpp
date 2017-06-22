#include "Precompiled.h"
#include "NIrisRecordDlg.h"

using namespace Neurotec::Biometrics;

namespace Neurotec { namespace Samples
{
	BEGIN_EVENT_TABLE(NIrisRecordDlg, wxDialog)
	END_EVENT_TABLE()

	NIrisRecordDlg::NIrisRecordDlg(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize& size) : wxDialog(parent, id, title, position, size)
	{
		CreateGUIControls();
	};

	NIrisRecordDlg::~NIrisRecordDlg()
	{
	}

	void NIrisRecordDlg::CreateGUIControls()
	{
		panel = new wxPanel(this, -1);
		vbox = new wxBoxSizer(wxVERTICAL);
		hbox1 = new wxBoxSizer(wxHORIZONTAL);
		hbox2 = new wxBoxSizer(wxHORIZONTAL);
		hbox3 = new wxBoxSizer(wxHORIZONTAL);
		flexGridSizer = new wxFlexGridSizer(2, 2, 9, 25);
		btnOk = new wxButton(panel, wxID_OK, wxT("Ok"));
		btnCancel = new wxButton(panel, wxID_CANCEL, wxT("Cancel"));
		labelWidth = new wxStaticText(panel, wxID_ANY, wxT("Width:"));
		labelHeight = new wxStaticText(panel, wxID_ANY, wxT("Height:"));
		spinctrlWidth = new wxSpinCtrl(panel, ID_WIDTH_SPIN_CTRL, wxT("500"));
		spinctrlHeight = new wxSpinCtrl(panel, ID_HEIGHT_SPIN_CTRL, wxT("500"));

		spinctrlWidth->SetRange(0, N_UINT16_MAX);
		spinctrlHeight->SetRange(0, N_UINT16_MAX);

		spinctrlWidth->SetValue(500);
		spinctrlHeight->SetValue(500);

		flexGridSizer->Add(labelWidth, 1, wxALIGN_RIGHT);
		flexGridSizer->Add(spinctrlWidth, 1, wxEXPAND);
		flexGridSizer->Add(labelHeight, 1, wxALIGN_RIGHT);
		flexGridSizer->Add(spinctrlHeight, 1, wxEXPAND);

		flexGridSizer->AddGrowableRow(0, 1);
		flexGridSizer->AddGrowableCol(1, 1);

		hbox1->Add(new wxPanel(panel, -1));
		vbox->Add(hbox1, 1, wxEXPAND);
		hbox2->Add(flexGridSizer);
		hbox3->Add(btnOk);
		hbox3->Add(btnCancel);
		vbox->Add(hbox2, 0, wxALL | wxALIGN_RIGHT | wxRIGHT, 10);
		vbox->Add(hbox3, 0, wxALL | wxALIGN_RIGHT | wxRIGHT, 10);
		panel->SetSizer(vbox);

		SetSize(282, 165);
		SetTitle("Add NERecord");
		Centre();
	}

	int NIrisRecordDlg::GetWidth()
	{
		return spinctrlWidth->GetValue();
	}

	int NIrisRecordDlg::GetHeight()
	{
		return spinctrlHeight->GetValue();
	}
}}

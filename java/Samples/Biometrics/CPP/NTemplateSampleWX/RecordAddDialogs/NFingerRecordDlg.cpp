#include "Precompiled.h"
#include "NFingerRecordDlg.h"
#include "../TemplateSampleForm.h"

using namespace Neurotec::Biometrics;

namespace Neurotec { namespace Samples
{
	BEGIN_EVENT_TABLE(NFingerRecordDlg, wxDialog)
		END_EVENT_TABLE()

		NFingerRecordDlg::NFingerRecordDlg(wxWindow *parent, bool isPalm, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize& size) : wxDialog(parent, id, title, position, size)
	{
			CreateGUIControls(isPalm);
		}

	NFingerRecordDlg::~NFingerRecordDlg()
	{
	}

	void NFingerRecordDlg::CreateGUIControls(bool isPalm)
	{
		panel = new wxPanel(this, -1);
		vbox = new wxBoxSizer(wxVERTICAL);
		hbox1 = new wxBoxSizer(wxHORIZONTAL);
		hbox2 = new wxBoxSizer(wxHORIZONTAL);
		hbox3 = new wxBoxSizer(wxHORIZONTAL);
		flexGridSizer = new wxFlexGridSizer(4, 2, 9, 25);
		btnOk = new wxButton(panel, wxID_OK, wxT("Ok"));
		btnCancel = new wxButton(panel, wxID_CANCEL, wxT("Cancel"));
		labelWidth = new wxStaticText(panel, wxID_ANY, wxT("Width:"), wxPoint());
		labelHeight = new wxStaticText(panel, wxID_ANY, wxT("Height:"));
		labelHorizontalResolution = new wxStaticText(panel, wxID_ANY, wxT("Horizontal Resolution:"));
		labelVirticalResolution = new wxStaticText(panel, wxID_ANY, wxT("Vertical Resolution:"));
		spinctrlWidth = new wxSpinCtrl(panel, ID_WIDTH_SPIN_CTRL, wxT("500"));
		spinctrlHeight = new wxSpinCtrl(panel, ID_HEIGHT_SPIN_CTRL, wxT("500"));
		spinctrlHorizontalResolution = new wxSpinCtrl(panel, ID_HORZONTAL_RESOLUTION_SPIN_CTRL, wxT("500"));
		spinctrlVirticalResolution = new wxSpinCtrl(panel, ID_VIRTICAL_RESOLUTION_SPIN_CTRL, wxT("500"));

		spinctrlWidth->SetRange(0, N_UINT16_MAX);
		spinctrlHeight->SetRange(0, N_UINT16_MAX);
		spinctrlHorizontalResolution->SetRange(0, N_UINT16_MAX);
		spinctrlVirticalResolution->SetRange(0, N_UINT16_MAX);

		spinctrlWidth->SetValue(1000);
		spinctrlHeight->SetValue(1000);
		spinctrlHorizontalResolution->SetValue(500);
		spinctrlVirticalResolution->SetValue(500);

		if (!isPalm)
		{
			spinctrlWidth->SetValue(500);
			spinctrlHeight->SetValue(500);
		}

		flexGridSizer->Add(labelWidth, 1, wxALIGN_RIGHT);
		flexGridSizer->Add(spinctrlWidth, 1, wxEXPAND);
		flexGridSizer->Add(labelHeight, 1, wxALIGN_RIGHT);
		flexGridSizer->Add(spinctrlHeight, 1, wxEXPAND);
		flexGridSizer->Add(labelHorizontalResolution, 1, wxALIGN_RIGHT);
		flexGridSizer->Add(spinctrlHorizontalResolution, 1, wxEXPAND);
		flexGridSizer->Add(labelVirticalResolution, 1, wxALIGN_RIGHT);
		flexGridSizer->Add(spinctrlVirticalResolution, 1, wxEXPAND);
		flexGridSizer->AddGrowableRow(2, 1);
		flexGridSizer->AddGrowableCol(1, 1);

		hbox1->Add(new wxPanel(panel, -1));
		vbox->Add(hbox1, 1, wxEXPAND);
		hbox2->Add(flexGridSizer);
		hbox3->Add(btnOk);
		hbox3->Add(btnCancel);
		vbox->Add(hbox2, 0, wxALL | wxALIGN_RIGHT | wxRIGHT, 10);
		vbox->Add(hbox3, 0, wxALL | wxALIGN_RIGHT | wxRIGHT, 10);
		panel->SetSizer(vbox);

		SetSize(282, 225);
		SetTitle("Add NFRecord");
		Centre();
	}

	int NFingerRecordDlg::GetWidth()
	{
		return spinctrlWidth->GetValue();
	}

	int NFingerRecordDlg::GetHeight()
	{
		return spinctrlHeight->GetValue();
	}

	int NFingerRecordDlg::GetHorizontalResolution()
	{
		return spinctrlVirticalResolution->GetValue();
	}

	int NFingerRecordDlg::GetVirticalResolution()
	{
		return spinctrlHorizontalResolution->GetValue();
	}
}}

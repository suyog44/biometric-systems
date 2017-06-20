#include "Precompiled.h"
#include "LicensePanel.h"

#define GREEN_COLOR 0x298A08

using namespace Neurotec::Licensing;

namespace Neurotec
{
	namespace Samples
	{
		LicensePanel::LicensePanel(wxWindow *parent, wxWindowID id, const wxPoint &pos, const wxSize &size, long style, const wxString &name)
			: wxPanel(parent, id, pos, size, style | wxBORDER_SIMPLE, name)
		{
			CreateGUIControls();
		}

		void LicensePanel::CreateGUIControls()
		{
			wxBoxSizer *boxSizerMain = new wxBoxSizer(wxVERTICAL);
			m_textCtrlComponents = new wxTextCtrl(this, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, wxTE_RICH | wxTE_MULTILINE | wxBORDER_NONE | wxTE_READONLY | wxTE_NO_VSCROLL);
			m_textCtrlComponents->SetBackgroundColour(this->GetBackgroundColour());
			boxSizerMain->Add(m_textCtrlComponents, 1, wxEXPAND | wxALL, 5);
			SetBackgroundColour(GetParent()->GetBackgroundColour());
			SetSizer(boxSizerMain);
		}

		void LicensePanel::RefreshComponentsStatus(const wxString &requiredComponents, const wxString &optionalComponents)
		{
			m_strRequiredcomponents = requiredComponents;
			m_strOptionalcomponents = optionalComponents;
			RefreshRequired();
		}

		void LicensePanel::RefreshRequired()
		{
			m_textCtrlComponents->Clear();
			m_textCtrlComponents->SetDefaultStyle(wxTextAttr(*wxBLACK, wxNullColour, m_textCtrlComponents->GetFont().Bold()));
			m_textCtrlComponents->SetForegroundColour(*wxBLACK);
			m_textCtrlComponents->AppendText("Required component licenses :   ");
			bool isAllRequiredActivated = true;
			wxStringTokenizer tokenizer(m_strRequiredcomponents, ",");
			while (tokenizer.HasMoreTokens())
			{
				wxString component = tokenizer.GetNextToken();
				if (NLicense::IsComponentActivated(component))
				{
					m_textCtrlComponents->SetDefaultStyle(wxTextAttr(wxColor(GREEN_COLOR)));
				}
				else
				{
					m_textCtrlComponents->SetDefaultStyle(wxTextAttr(*wxRED));
					isAllRequiredActivated = false;
				}
				m_textCtrlComponents->AppendText(component);
				if (tokenizer.HasMoreTokens())
				{
					m_textCtrlComponents->SetDefaultStyle(wxTextAttr(*wxBLACK));
					m_textCtrlComponents->AppendText(", ");
				}
			}
			RefreshOptional();
			if (isAllRequiredActivated)
			{
				m_textCtrlComponents->SetDefaultStyle(wxTextAttr(wxColor(GREEN_COLOR)));
				m_textCtrlComponents->AppendText("\r\nComponent licenses obtained");
			}
			else
			{
				m_textCtrlComponents->SetDefaultStyle(wxTextAttr(*wxRED));
				m_textCtrlComponents->AppendText("\r\nNot all required licenses obtained");
			}
		}

		void LicensePanel::RefreshOptional()
		{
			wxStringTokenizer tokenizer(m_strOptionalcomponents, ",");
			while (tokenizer.HasMoreTokens())
			{
				if (!m_strRequiredcomponents.IsEmpty())
				{
					m_textCtrlComponents->SetDefaultStyle(wxTextAttr(*wxBLACK));
				}
				wxString component = tokenizer.GetNextToken();
				if (NLicense::IsComponentActivated(component))
				{
					m_textCtrlComponents->SetDefaultStyle(wxTextAttr(wxColor(GREEN_COLOR)));
				}
				else
				{
					m_textCtrlComponents->SetDefaultStyle(wxTextAttr(*wxRED));
				}
				m_textCtrlComponents->AppendText(component);
				m_textCtrlComponents->AppendText(" (Optional)");
			}
		}
	}
}

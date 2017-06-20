#include "Precompiled.h"
#include <StructEditor.h>
#include <CustomProperty.h>

using namespace Neurotec::Gui;
using namespace Neurotec::Reflection;
using namespace Neurotec::Collections;
using namespace Neurotec::ComponentModel;

namespace Neurotec
{
	namespace Samples
	{
		namespace CommonUIHelpers
		{
			BEGIN_EVENT_TABLE(StructEditor, wxDialog)
				EVT_CHECKBOX(wxID_ANY, StructEditor::OnCheckBoxUseValueClick)
			END_EVENT_TABLE()

			StructEditor::StructEditor(wxWindow *parent, const NType &type, const NValue &value, bool isNullable) :
				wxDialog(parent, wxID_ANY, "Struct Editor"),
				m_type(type),
				m_object(value),
				m_isNullable(isNullable)
			{
				CreateGUIControls();
				LoadData();
			}

			void StructEditor::CreateGUIControls()
			{
				wxBoxSizer *boxSizerMain = new wxBoxSizer(wxVERTICAL);
				m_checkBoxUse = new wxCheckBox(this, wxID_ANY, "Use value");
				m_checkBoxUse->Show(m_isNullable);

				boxSizerMain->Add(m_checkBoxUse, 0, wxALL, 5);
				m_propertyGridManager = new wxPropertyGridManager(this, wxID_ANY, wxDefaultPosition, wxSize(250, 200),
					wxPG_BOLD_MODIFIED | wxPG_SPLITTER_AUTO_CENTER | wxPGMAN_DEFAULT_STYLE);
				boxSizerMain->Add(m_propertyGridManager, 0, wxEXPAND | wxALL, 10);

				wxBoxSizer *boxSizerButtons = new wxBoxSizer(wxHORIZONTAL);
				wxButton *buttonOK = new wxButton(this, wxID_OK, "Ok");
				boxSizerButtons->Add(buttonOK, 0, wxALL, 5);
				wxButton *buttonCancel = new wxButton(this, wxID_CANCEL, "Cancel");
				boxSizerButtons->Add(buttonCancel, 0, wxALL, 5);
				boxSizerMain->Add(boxSizerButtons, 0, wxALL | wxALIGN_RIGHT, 5);

				SetSizerAndFit(boxSizerMain);
			}

			void StructEditor::LoadData()
			{
				if (m_object.IsNull())
				{
					m_checkBoxUse->SetValue(!m_isNullable);
					m_propertyGridManager->Enable(!m_isNullable);
					m_object = m_type.CreateInstance();
				}
				else
				{
					m_checkBoxUse->SetValue(true);
				}

				NArrayWrapper<NPropertyDescriptor> objectProperties = NTypeDescriptor::GetProperties(m_type);
				for (NArrayWrapper<NPropertyDescriptor>::iterator it = objectProperties.begin(); it != objectProperties.end(); it++)
				{
					wxString propertyName = it->GetName();
					m_propertyGridManager->GetPage(0)->Append(new CustomProperty(propertyName, m_object));
				}
			}

			NValue StructEditor::GetValue()
			{
				if (m_checkBoxUse->IsChecked())
				{
					return NValue::FromObject(m_object);
				}
				return NULL;
			}

			void StructEditor::OnCheckBoxUseValueClick(wxCommandEvent &WXUNUSED(event))
			{
				if (m_checkBoxUse->IsChecked())
				{
					m_propertyGridManager->Enable();
				}
				else
				{
					m_propertyGridManager->Enable(false);
				}
			}
		}
	}
}

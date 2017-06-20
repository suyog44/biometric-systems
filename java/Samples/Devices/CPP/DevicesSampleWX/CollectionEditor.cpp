#include "Precompiled.h"
#include <CollectionEditor.h>
#include <CustomProperty.h>
#include <Resources/Up.xpm>
#include <Resources/Down.xpm>

using namespace Neurotec::Gui;
using namespace Neurotec::Samples::CommonUIHelpers;
using namespace Neurotec::Reflection;
using namespace Neurotec::Collections;
using namespace Neurotec::ComponentModel;

namespace Neurotec
{
	namespace Samples
	{
		namespace CommonUIHelpers
		{
			IMPLEMENT_ABSTRACT_CLASS(CollectionEditor, wxDialog)

			CollectionEditor::CollectionEditor(wxWindow *parent, const std::vector<NValue> &collection, const NType &type, const wxString &caption, long style, const wxPoint &pos, const wxSize &sz) : wxDialog(parent, wxID_ANY, caption, pos, sz, style),
				m_collection(collection), m_type(type)
			{
				CreateGUIControls();
				LoadData();
				if (m_collection.size() > 0)
				{
					m_listBox->Select(0);
				}
			}

			CollectionEditor::~CollectionEditor()
			{
			}

			void CollectionEditor::LoadData()
			{
				m_listBox->Clear();
				int id = 0;
				for (std::vector<NValue>::iterator iterator = m_collection.begin(); iterator != m_collection.end(); iterator++)
				{
					if (iterator->IsNull())
					{
						m_listBox->Append("NULL");
					}
					else
					{
						m_listBox->Append(iterator->ToString());
					}
					id++;
				}
			}

			std::vector<NValue> CollectionEditor::GetValue()
			{
				return m_collection;
			}

			void CollectionEditor::CreateGUIControls()
			{
				const int spacing = 5;

				wxStaticText *stcTxtMembers = new wxStaticText(this, -1, "Members: ");
				m_listBox = new wxListBox(this, wxID_ANY);
				wxButton *m_buttonAdd = new wxButton(this, -1, "Add", wxDefaultPosition, wxDefaultSize);
				wxButton *m_buttonRemove = new wxButton(this, -1, "Remove", wxDefaultPosition, wxDefaultSize);
				m_pgCollection = new wxPropertyGridManager(this, -1, wxPoint(0, 0), wxSize(225, 230), wxPG_BOLD_MODIFIED | wxPG_TOOLBAR | wxPG_SPLITTER_AUTO_CENTER | wxPGMAN_DEFAULT_STYLE | wxPG_VFB_STAY_IN_PROPERTY | wxPG_VFB_SHOW_MESSAGEBOX);
				m_pgCollection->SetExtraStyle(wxPG_EX_MODE_BUTTONS);
				m_pgCollection->InsertPage(0, wxEmptyString);

				wxBitmap upBitmap(Up_XPM, wxBITMAP_TYPE_XPM);
				wxBitmap downBitmap(Down_XPM, wxBITMAP_TYPE_XPM);
				wxBitmapButton *m_btnUp = new wxBitmapButton(this, wxID_ANY, upBitmap, wxPoint(0, 0), wxSize(25, 25), wxBU_AUTODRAW);
				wxBitmapButton *m_btnDown = new wxBitmapButton(this, wxID_ANY, downBitmap, wxPoint(0, 0), wxSize(25, 25), wxBU_AUTODRAW);

				wxBoxSizer *boxSizerMainBox = new wxBoxSizer(wxVERTICAL);
				wxBoxSizer *boxSizerStcTxtMembersContainer = new wxBoxSizer(wxHORIZONTAL);
				wxBoxSizer *boxSizerBtnAddRemoveContainer = new wxBoxSizer(wxHORIZONTAL);
				wxBoxSizer *boxSizerListCtrlContainer = new wxBoxSizer(wxHORIZONTAL);
				wxBoxSizer *boxSizerBtnUpDownContainer = new wxBoxSizer(wxVERTICAL);
				wxBoxSizer *boxSizerPGContainer = new wxBoxSizer(wxVERTICAL);

				boxSizerStcTxtMembersContainer->Add(stcTxtMembers, 1, wxTOP | wxLEFT, spacing);
				boxSizerListCtrlContainer->Add(m_listBox, 1, wxLEFT | wxEXPAND | wxALL, spacing);
				boxSizerBtnAddRemoveContainer->Add(m_buttonAdd, 0, wxBOTTOM | wxALIGN_LEFT | wxALL, spacing);
				boxSizerBtnAddRemoveContainer->Add(m_buttonRemove, 0, wxBOTTOM | wxALIGN_LEFT | wxALL, spacing);
				boxSizerBtnUpDownContainer->Add(m_btnUp, 0, wxTOP | wxLEFT, spacing);
				boxSizerBtnUpDownContainer->Add(m_btnDown, 0, wxTOP | wxLEFT, spacing);
				boxSizerPGContainer->Add(m_pgCollection, 1, wxEXPAND | wxALL, spacing);
				boxSizerListCtrlContainer->Add(boxSizerBtnUpDownContainer, 0, wxALIGN_LEFT);
				boxSizerListCtrlContainer->Add(boxSizerPGContainer, 1, wxEXPAND);
				boxSizerMainBox->Add(boxSizerStcTxtMembersContainer, 0);
				boxSizerMainBox->Add(boxSizerListCtrlContainer, 1, wxEXPAND);
				boxSizerMainBox->Add(boxSizerBtnAddRemoveContainer, 0);

				m_buttonAdd->Connect(m_buttonAdd->GetId(), wxEVT_BUTTON, wxCommandEventHandler(CollectionEditor::OnAddClick), NULL, this);
				m_buttonRemove->Connect(m_buttonRemove->GetId(), wxEVT_BUTTON, wxCommandEventHandler(CollectionEditor::OnDeleteClick), NULL, this);
				m_btnUp->Connect(m_btnUp->GetId(), wxEVT_BUTTON, wxCommandEventHandler(CollectionEditor::OnUpClick), NULL, this);
				m_btnDown->Connect(m_btnDown->GetId(), wxEVT_BUTTON, wxCommandEventHandler(CollectionEditor::OnDownClick), NULL, this);
				m_pgCollection->Connect(m_pgCollection->GetId(), wxEVT_PG_CHANGED, wxPropertyGridEventHandler(CollectionEditor::OnPropertyGridChange), NULL, this);
				m_listBox->Connect(m_listBox->GetId(), wxEVT_LISTBOX, wxListEventHandler(CollectionEditor::OnListItemSelect), NULL, this);

				wxStdDialogButtonSizer *buttonSizer = new wxStdDialogButtonSizer();
				wxButton *btnOk = new wxButton(this, wxID_OK);
				wxButton *btnCancel = new wxButton(this, wxID_CANCEL);

				buttonSizer->AddButton(btnOk);
				buttonSizer->AddButton(btnCancel);
				buttonSizer->Realize();
				boxSizerMainBox->Add(buttonSizer, 0, wxALIGN_RIGHT | wxALIGN_CENTRE_VERTICAL | wxALL, spacing);

				Maximize(false);
				SetSizer(boxSizerMainBox);
				SetSize(wxSize(500, 375));
			}

			void CollectionEditor::OnAddClick(wxCommandEvent &event)
			{
				NValue val = m_type.CreateInstance();
				m_collection.push_back(val);
				m_pgCollection->GetPage(0)->Clear();

				m_listBox->Append(val.IsNull() ? wxString("NULL") : (wxString)val.ToString());
				m_listBox->Select(m_collection.size() - 1);

				event.Skip();
			}

			void CollectionEditor::OnDeleteClick(wxCommandEvent &WXUNUSED(event))
			{
				int itemIndex = m_listBox->GetSelection();
				if (itemIndex != -1)
				{
					std::vector<NValue>::iterator it = m_collection.begin();
					m_collection.erase(it + itemIndex);
					m_listBox->Delete(itemIndex);
					if (itemIndex >= (int)m_collection.size())
					{
						itemIndex = (int)m_collection.size() - 1;
					}
					if (itemIndex >= 0)
					{
						m_listBox->Select(itemIndex);
					}
					else
					{
						m_pgCollection->GetPage(0)->Clear();
					}
				}
			}

			void CollectionEditor::OnUpClick(wxCommandEvent &WXUNUSED(event))
			{
				int selectedIndex = m_listBox->GetSelection();
				if (selectedIndex > 0)
				{
					NValue temp = m_collection[selectedIndex];
					m_collection[selectedIndex] = m_collection[selectedIndex - 1];
					m_collection[selectedIndex - 1] = temp;

					wxString tempLabel = m_listBox->GetString(selectedIndex);
					m_listBox->SetString(selectedIndex, m_listBox->GetString(selectedIndex - 1));
					m_listBox->SetString(selectedIndex - 1, tempLabel);
					m_listBox->Select(selectedIndex - 1);
				}
			}

			void CollectionEditor::OnDownClick(wxCommandEvent &WXUNUSED(event))
			{
				int selectedIndex = m_listBox->GetSelection();
				if (selectedIndex < (int)m_collection.size() - 1)
				{
					NValue temp = m_collection[selectedIndex];
					m_collection[selectedIndex] = m_collection[selectedIndex + 1];
					m_collection[selectedIndex + 1] = temp;

					wxString tempLabel = m_listBox->GetString(selectedIndex);
					m_listBox->SetString(selectedIndex, m_listBox->GetString(selectedIndex + 1));
					m_listBox->SetString(selectedIndex + 1, tempLabel);
					m_listBox->Select(selectedIndex + 1);
				}
			}

			void CollectionEditor::OnPropertyGridChange(wxPropertyGridEvent &WXUNUSED(event))
			{
				int selectedIndex = m_listBox->GetSelection();
				NValue value = m_collection[selectedIndex];
				m_listBox->SetString(selectedIndex, value.IsNull() ? wxString("NULL") : (wxString)value.ToString());
			}

			void CollectionEditor::OnListItemSelect(wxListEvent & WXUNUSED(event))
			{
				int selectedIndex = m_listBox->GetSelection();
				NValue value = m_collection[selectedIndex];
				m_pgCollection->GetPage(0)->Clear();
				if (m_type.IsObject())
				{
					NObject object = value.ToObject(m_type);
					NArrayWrapper<NPropertyDescriptor> objectProperties = NTypeDescriptor::GetProperties(m_type);

					wxPropertyGridPage *page = m_pgCollection->GetPage(0);
					for (NArrayWrapper<NPropertyDescriptor>::iterator it = objectProperties.begin(); it != objectProperties.end(); it++)
					{
						if (m_pgCollection->GetPage(0)->GetProperty(it->GetName()) == NULL)
						{
							wxPGProperty *property = m_pgCollection->GetPage(0)->Append(new CustomProperty(*it, object));
							if (!property->IsTextEditable())
							{
								page->SetPropertyTextColour(property, wxSystemSettings::GetColour(wxSYS_COLOUR_GRAYTEXT), 0);
							}
						}
					}
				}
				else
				{
					m_pgCollection->GetPage(0)->Append(new CustomProperty(&m_collection, selectedIndex, m_type, naNone));
				}
			}
		}
	}
}

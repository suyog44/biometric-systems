#include "Precompiled.h"
#include <CustomProperty.h>
#include <Dialogs/ConnectToDeviceDialog.h>
#include <DevicesSampleForm.h>

using namespace Neurotec::Gui;
using namespace Neurotec::Devices;
using namespace Neurotec::ComponentModel;
using namespace Neurotec::Plugins;

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			BEGIN_EVENT_TABLE(ConnectToDeviceDialog, wxDialog)
				EVT_COMBOBOX(ID_PLUGIN_CMB, ConnectToDeviceDialog::OnSelectedIndexChanged)
				EVT_BUTTON(wxID_OK, ConnectToDeviceDialog::OnButtonOkClick)
			END_EVENT_TABLE()

			ConnectToDeviceDialog::ConnectToDeviceDialog(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize &size, long style) :
				wxDialog(parent, id, title, position, size, style), m_parameterBag(NULL)
			{
				CreateGUIControls();

				int j = 0;
				for (int i = 0; i < NDeviceManager::GetPluginManager().GetPlugins().GetCount(); i++)
				{
					NPlugin plugin = NDeviceManager::GetPluginManager().GetPlugins().Get(i);
					if (plugin.GetState() == npsPlugged && NDeviceManager::IsConnectToDeviceSupported(plugin))
					{
						m_cmbPlugin->Append(plugin.ToString(), new ObjectClientData(plugin));
						j++;
					}
				}

				if (m_cmbPlugin->GetCount() != 0)
				{
					m_cmbPlugin->SetSelection(0);
					OnSelectedPluginChanged();
				}

				if (m_cmbPlugin->GetCount() <= 0)
					m_buttonOk->Enable(false);
			}

			ConnectToDeviceDialog::~ConnectToDeviceDialog()
			{
			}

			void ConnectToDeviceDialog::CreateGUIControls()
			{
				wxBoxSizer *boxSizerMainBox = new wxBoxSizer(wxVERTICAL);
				wxBoxSizer *boxSizerPluginContainer = new wxBoxSizer(wxHORIZONTAL);
				wxBoxSizer *boxSizerPgContainer = new wxBoxSizer(wxHORIZONTAL);

				wxStaticBox *parmStaticBox = new wxStaticBox(this, wxID_ANY, "Parameters", wxDefaultPosition, wxSize(359, 274));
				wxStaticText *stcTxtPlugin = new wxStaticText(this, wxID_ANY, "Plugin:", wxDefaultPosition, wxDefaultSize);
				m_cmbPlugin = new wxComboBox(this, ID_PLUGIN_CMB, wxEmptyString, wxDefaultPosition, wxSize(311, 21), wxArrayString(), wxCB_READONLY);
				m_propertyGridManager = new wxPropertyGridManager(parmStaticBox, wxID_ANY, wxDefaultPosition, wxSize(353, 255),
					wxPG_BOLD_MODIFIED | wxPG_SPLITTER_AUTO_CENTER | wxPG_TOOLBAR | wxPGMAN_DEFAULT_STYLE);

				m_propertyGridManager->SetExtraStyle(wxPG_EX_MODE_BUTTONS);

				wxStaticBoxSizer *sizer = new wxStaticBoxSizer(parmStaticBox, wxVERTICAL);

				boxSizerPluginContainer->Add(stcTxtPlugin, 0, wxALL | wxALIGN_CENTER, 5);
				boxSizerPluginContainer->Add(m_cmbPlugin, 1, wxALL | wxALIGN_CENTER | wxEXPAND, 5);
				boxSizerPgContainer->Add(m_propertyGridManager, 1, wxEXPAND);

				sizer->Add(boxSizerPgContainer, 1, wxEXPAND);

				boxSizerMainBox->Add(boxSizerPluginContainer, 0, wxALL | wxEXPAND, 10);
				boxSizerMainBox->Add(sizer, 1, wxEXPAND);

				wxStdDialogButtonSizer *buttonSizer = new wxStdDialogButtonSizer();
				m_buttonOk = new wxButton(this, wxID_OK);
				m_buttonCancel = new wxButton(this, wxID_CANCEL);

				buttonSizer->AddButton(m_buttonOk);
				buttonSizer->AddButton(m_buttonCancel);
				buttonSizer->Realize();

				boxSizerMainBox->Add(buttonSizer, 0, wxALIGN_RIGHT | wxALIGN_CENTRE_VERTICAL | wxALL, 5);
				SetSizerAndFit(boxSizerMainBox);

				SetTitle("Connect to Device");
				Centre();
			}

			void ConnectToDeviceDialog::OnSelectedPluginChanged()
			{
				NPlugin plugin = GetSelectedPlugin();
				m_buttonOk->Enable(!plugin.IsNull());
				m_propertyGridManager->Enable(!plugin.IsNull());
				UpdatePropertyGrid();
			}

			void ConnectToDeviceDialog::UpdatePropertyGrid()
			{
				NPlugin plugin = GetSelectedPlugin();
				if (!plugin.IsNull())
				{
					NArrayWrapper<NParameterDescriptor> parameters = NDeviceManager::GetConnectToDeviceParameters(plugin);
					m_parameterBag = NParameterBag(parameters.begin(), parameters.end());
					m_propertyGridManager->GetPage(0)->Clear();
					if (!m_parameterBag.IsNull())
					{
						NArrayWrapper<NPropertyDescriptor> objectProperties = NTypeDescriptor::GetProperties(m_parameterBag);
						for (NArrayWrapper<NPropertyDescriptor>::iterator it = objectProperties.begin(); it != objectProperties.end(); it++)
						{
							wxPGProperty *property = m_propertyGridManager->GetPage(0)->Append(new CustomProperty(*it, m_parameterBag));
							if (!property->IsTextEditable())
							{
								m_propertyGridManager->SetPropertyTextColour(property, wxSystemSettings::GetColour(wxSYS_COLOUR_GRAYTEXT));
							}
						}
					}
				}
			}

			NPlugin ConnectToDeviceDialog::GetSelectedPlugin()
			{
				if (m_cmbPlugin->GetSelection() == -1)
				{
					return NULL;
				}
				ObjectClientData *objectData = (ObjectClientData*)m_cmbPlugin->GetClientObject(m_cmbPlugin->GetSelection());
				return NObjectDynamicCast<NPlugin>(objectData->GetObject());
			}

			void ConnectToDeviceDialog::SetSelectedPlugin(const NPlugin &plugin)
			{
				for (unsigned int i = 0; i < m_cmbPlugin->GetCount(); i++)
				{
					ObjectClientData *objectData = (ObjectClientData*)m_cmbPlugin->GetClientObject(i);
					NPlugin pluginCombo = NObjectDynamicCast<NPlugin>(objectData->GetObject());
					if (plugin.Equals(pluginCombo))
					{
						m_cmbPlugin->SetSelection(i);
						break;
					}
				}
				OnSelectedPluginChanged();
			}

			NPropertyBag ConnectToDeviceDialog::GetParameters()
			{
				if (!m_parameterBag.IsNull())
				{
					return m_parameterBag.ToPropertyBag();
				}
				return NULL;
			}

			void ConnectToDeviceDialog::OnSelectedIndexChanged(wxCommandEvent &WXUNUSED(event))
			{
				try
				{
					OnSelectedPluginChanged();
				}
				catch (NError &ex)
				{
					wxExceptionDlg::Show(ex);
				}
			}

			void ConnectToDeviceDialog::OnButtonOkClick(wxCommandEvent &event)
			{
				NPlugin plugin = GetSelectedPlugin();
				if (!plugin.IsNull())
				{
					NArrayWrapper<NParameterDescriptor> parameters = NDeviceManager::GetConnectToDeviceParameters(plugin);
					for (int i = 0; i < parameters.GetCount(); i++)
					{
						if ((parameters.Get(i).GetAttributes() & naOptional) != naOptional)
						{
							if (m_parameterBag.GetValues().Get(i).IsNull())
							{
								wxMessageDialog dialog(this, parameters.Get(i).GetName() + " value not specified", "Exclamation", wxOK | wxICON_EXCLAMATION);
								dialog.ShowModal();
								return;
							}
						}
					}
				}
				event.Skip();
			}
		}
	}
}

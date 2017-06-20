#include "Precompiled.h"
#include "ConnectToDeviceForm.h"

using namespace Neurotec;
using namespace Neurotec::Devices;
using namespace Neurotec::ComponentModel;
using namespace Neurotec::Gui;

namespace Neurotec { namespace Samples
{

BEGIN_EVENT_TABLE(ConnectToDeviceForm, wxDialog)
	EVT_COMBOBOX(ID_COMBO_PLUGIN, ConnectToDeviceForm::OnSelectedPluginChanged)
	EVT_GRID_CMD_CELL_LEFT_CLICK(ID_GRID_VALUES, ConnectToDeviceForm::OnCellClick)
	EVT_SIZE(ConnectToDeviceForm::OnResize)
	EVT_BUTTON(wxID_OK, ConnectToDeviceForm::OnOKClick)
END_EVENT_TABLE()

ConnectToDeviceForm::ConnectToDeviceForm(wxWindow* parent, wxWindowID id, const wxString& title, const wxPoint& pos, const wxSize& size, long style) :
	wxDialog(parent, id, title, pos, size, style), m_params(0), m_plugin(NPlugin(NULL))

{
	CreateGUIControls();

	NPluginManager manager = NDeviceManager::GetPluginManager();
	NPluginManager::PluginCollection plugins = manager.GetPlugins();
	int count = plugins.GetCount();
	for (int i = 0; i < count; i++)
	{
		NPlugin plugin = plugins.Get(i);
		if (plugin.GetState() == npsPlugged && NDeviceManager::IsConnectToDeviceSupported(plugin))
		{
			m_cbPlugins->Append(plugin.ToString());
			m_plugins.push_back(plugin);
		}
	}

	if (count > 0)
	{
		m_cbPlugins->SetSelection(0);
		wxCommandEvent cmd(wxEVT_COMMAND_COMBOBOX_SELECTED, ID_COMBO_PLUGIN);
		::wxPostEvent(this, cmd);
	}
}

ConnectToDeviceForm::~ConnectToDeviceForm()
{
	m_plugins.clear();
}

void ConnectToDeviceForm::CreateGUIControls()
{
	SetSizeHints(wxDefaultSize, wxDefaultSize);

	wxBoxSizer * bSizerMain = new wxBoxSizer(wxVERTICAL);

	wxBoxSizer * bSizer = new wxBoxSizer(wxHORIZONTAL);
	wxStaticText * text1 = new wxStaticText(this, wxID_ANY, wxT("Plugin:"));
	text1->Wrap(-1);
	bSizer->Add(text1, 0, wxALL | wxALIGN_CENTRE_VERTICAL, 5);

	m_cbPlugins = new wxComboBox(this, ID_COMBO_PLUGIN, wxEmptyString, wxDefaultPosition, wxDefaultSize, 0, NULL, wxCB_DROPDOWN | wxCB_READONLY);
	bSizer->Add(m_cbPlugins, 1, wxALL | wxEXPAND | wxALIGN_CENTER_VERTICAL);
	bSizerMain->Add(bSizer, 0, wxALL | wxEXPAND | wxALIGN_TOP, 5);

	bSizerMain->AddSpacer(3);

	wxStaticText * text2 = new wxStaticText(this, wxID_ANY, wxT("Properties:"));
	text2->Wrap(-1);
	bSizerMain->Add(text2, 0, wxALL, 5);

	m_grid = new wxGrid(this, ID_GRID_VALUES);
	m_grid->CreateGrid(0, 2, wxGrid::wxGridSelectCells);
	m_grid->EnableScrolling(false, true);

	m_grid->SetColLabelSize(0);
	m_grid->SetRowLabelSize(0);
	wxGridCellBoolEditor::UseStringValues(wxT("True"), wxT("False"));
	bSizerMain->Add(m_grid, 1, wxALL | wxEXPAND | wxALIGN_CENTER, 5);

	bSizer = new wxBoxSizer(wxHORIZONTAL);
	wxButton * buttonOK = new wxButton(this, wxID_OK, wxT("OK"));
	bSizer->Add(buttonOK, 0, wxALL | wxEXPAND | wxALIGN_RIGHT, 5);

	wxButton * buttonCancel = new wxButton(this, wxID_CANCEL, wxT("Cancel"));
	bSizer->Add(buttonCancel, 0, wxALL | wxEXPAND | wxALIGN_RIGHT, 5);

	bSizerMain->Add(bSizer, 0, wxALL | wxALIGN_RIGHT, 5);

	SetSizer(bSizerMain);
	SetMinSize(wxSize(300, 300));
	Layout();
}

void ConnectToDeviceForm::OnCellClick(wxGridEvent &event)
{
	m_grid->SetGridCursor(event.GetRow(), event.GetCol());
	event.Skip();
}

void ConnectToDeviceForm::OnOKClick(wxCommandEvent &event)
{
	if (m_params.GetCount() > 0)
	{
		NParameterBag params(m_params.begin(), m_params.end());
		try
		{
			for (int i = 0; i < m_params.GetCount(); i++)
			{
				NParameterDescriptor descriptor = m_params[i];
				wxString key = descriptor.GetName();
				NAttributes attributes = descriptor.GetAttributes();
				NBool isStdOnly = (attributes & naStdValuesExclusive) == naStdValuesExclusive;
				wxString cellValue = m_grid->GetCellValue(i, 1);
				NValue value = NValue(0);

				if (cellValue == wxEmptyString) continue;
				if (isStdOnly)
				{
					NParameterDescriptor::StdValueCollection stdValues = descriptor.GetStdValues();
					for (int j = 0; j < stdValues.GetCount(); j++)
					{
						NNameValuePair stdValue = stdValues.Get(j);
						if (cellValue == (wxString)stdValue.GetName())
						{
							value = stdValue.GetValue();
							break;
						}
					}
				}
				else
				{
					NType type = descriptor.GetParameterType();
					switch(type.GetTypeCode())
					{
					case ntcString:
					case ntcBoolean:
					case ntcInt32:
						value = NValue::FromString(cellValue);
						break;
					default:
						break;
					};
				}
				params.Set(key, value);
			}
		}
		catch(NError & ex)
		{
			wxExceptionDlg::Show(wxString::Format(wxT("Error occured: %s"), ((wxString)ex.ToString()).c_str()));
			return;
		}

		m_propertyBag = params.ToPropertyBag();

		m_params = NArrayWrapper<NParameterDescriptor>(0);
		event.Skip();
	}
	else
	{
		wxMessageBox(wxT("No parameters selected"));
	}
}

void ConnectToDeviceForm::OnSelectedPluginChanged(wxCommandEvent &event)
{
	int selection = event.GetSelection();
	try
	{
		m_plugin = selection != -1 ? m_plugins[selection] : NPlugin(NULL);
		int rowCount = m_grid->GetNumberRows();
		if (rowCount > 0)
		{
			m_grid->ClearGrid();
			m_grid->DeleteRows(0, rowCount);
		}
		if (!m_plugin.IsNull())
		{
			m_params = NDeviceManager::GetConnectToDeviceParameters(m_plugin);
			for (int i = 0; i < m_params.GetCount(); i++)
			{
				NParameterDescriptor descriptor = m_params[i];
				NType type = descriptor.GetParameterType();
				NValue defaultValue = descriptor.GetDefaultValue();
				NAttributes attributes = descriptor.GetAttributes();
				NBool isStdOnly = (attributes & naStdValuesExclusive) == naStdValuesExclusive;
				NBool isOptional = (attributes & naOptional) == naOptional;

				m_grid->AppendRows();
				m_grid->SetReadOnly(i, 0, true);
				m_grid->SetCellValue(i, 0, descriptor.GetName());
				if (isStdOnly)
				{
					wxArrayString keys;
					int defaultValueIndex = -1;
					NParameterDescriptor::StdValueCollection stdValues = descriptor.GetStdValues();
					if (isOptional)
					{
						keys.Add(wxEmptyString);
					}
					for(int j = 0; j < stdValues.GetCount(); j++)
					{
						NNameValuePair pair = stdValues.Get(j);
						NValue stdValue = pair.GetValue();
						if (NValue::Equals(defaultValue, stdValue))
						{
							defaultValueIndex = j + (isOptional ? 1 : 0);
						}
						keys.Add(pair.GetName());
					}

					m_grid->SetCellEditor(i, 1, new wxGridCellChoiceEditor(keys));
					if (defaultValueIndex != -1)
					{
						m_grid->SetCellValue(i, 1, keys[defaultValueIndex]);
					}
				}
				else
				{
					switch(type.GetTypeCode())
					{
					case ntcString:
						if (!defaultValue.IsNull()) m_grid->SetCellValue(i, 1, defaultValue.ToString());
						break;
					case ntcBoolean:
						m_grid->SetCellValue(i, 1, !defaultValue.IsNull() ? (wxString)defaultValue.ToString() : wxT("False"));
						m_grid->SetCellEditor(i, 1, new wxGridCellBoolEditor());
						m_grid->SetCellRenderer(i, 1, new wxGridCellBoolRenderer());
						break;
					case ntcInt32:
						if (!defaultValue.IsNull()) m_grid->SetCellValue(i, 1, defaultValue.ToString());
						m_grid->SetCellEditor(i, 1, new wxGridCellNumberEditor());
						break;
					default:
						NThrowNotImplementedException();
					}
				}
			}
			ResizeColumns();
		}
		else
		{
			m_params = NArrayWrapper<NParameterDescriptor>(0);
		}
	}
	catch(NError & ex)
	{
		wxExceptionDlg::Show(wxString::Format(wxT("Error occured: %s"), ((wxString)ex.ToString()).c_str()));
	}
}

void ConnectToDeviceForm::OnResize(wxSizeEvent &event)
{
	ResizeColumns();
	event.Skip();
}

void ConnectToDeviceForm::ResizeColumns()
{
	m_grid->AutoSizeColumn(0, true);
	int minWidth = GetSize().GetWidth() - m_grid->GetColSize(0) - m_grid->GetScrollLineX() - 20;
	m_grid->SetColMinimalWidth(1, minWidth);
	m_grid->SetColSize(1, minWidth);
	m_grid->ForceRefresh();
}

NPropertyBag ConnectToDeviceForm::GetProperties()
{
	return m_propertyBag;
}

NPlugin ConnectToDeviceForm::GetSelectedPlugin()
{
	return m_plugin;
}

}}

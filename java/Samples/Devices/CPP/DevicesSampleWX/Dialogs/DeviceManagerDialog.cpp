#include "Precompiled.h"
#include <Dialogs/DeviceManagerDialog.h>

using namespace Neurotec::Devices;

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			DeviceManagerDialog::DeviceManagerDialog(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize &size, long style) :
				wxDialog(parent, id, title, position, size, style)
			{
				CreateGUIControls();
				UpdateConfigSettings();
			}

			DeviceManagerDialog::~DeviceManagerDialog()
			{
			}

			void DeviceManagerDialog::CreateGUIControls()
			{
				m_treeCtrlDeviceMgr = new wxTreeListCtrl(this, wxID_ANY, wxDefaultPosition, wxSize(280, 250), wxTL_DEFAULT_STYLE | wxTL_CHECKBOX);

				m_treeCtrlDeviceMgr->AppendColumn("Device types", wxCOL_WIDTH_AUTOSIZE, wxALIGN_LEFT);
				wxTreeListItem root = m_treeCtrlDeviceMgr->GetRootItem();
				wxTreeListItem treeIdAny = m_treeCtrlDeviceMgr->AppendItem(root, "Any", -1, -1, new NDeviceManagerTreeNodeData(ndtAny));
				wxTreeListItem treeIdCaptDev = m_treeCtrlDeviceMgr->AppendItem(treeIdAny, "Capture device", -1, -1, new NDeviceManagerTreeNodeData(ndtCaptureDevice));
				m_treeCtrlDeviceMgr->AppendItem(treeIdCaptDev, "Camera", -1, -1, new NDeviceManagerTreeNodeData(ndtCamera));
				m_treeCtrlDeviceMgr->AppendItem(treeIdCaptDev, "Microphone", -1, -1, new NDeviceManagerTreeNodeData(ndtMicrophone));
				wxTreeListItem treeIdBioMDev = m_treeCtrlDeviceMgr->AppendItem(treeIdAny, "Biometric device", -1, -1, new NDeviceManagerTreeNodeData(ndtBiometricDevice));
				wxTreeListItem treeIdFScanner = m_treeCtrlDeviceMgr->AppendItem(treeIdBioMDev, "F scanner", -1, -1, new NDeviceManagerTreeNodeData(ndtFScanner));
				m_treeCtrlDeviceMgr->AppendItem(treeIdFScanner, "Finger scanner", -1, -1, new NDeviceManagerTreeNodeData(ndtFingerScanner));
				m_treeCtrlDeviceMgr->AppendItem(treeIdFScanner, "Palm scanner", -1, -1, new NDeviceManagerTreeNodeData(ndtPalmScanner));
				m_treeCtrlDeviceMgr->AppendItem(treeIdBioMDev, "Iris scanner", -1, -1, new NDeviceManagerTreeNodeData(ndtIrisScanner));
				m_treeCtrlDeviceMgr->Expand(root);
				m_treeCtrlDeviceMgr->Expand(treeIdAny);
				m_treeCtrlDeviceMgr->Expand(treeIdCaptDev);
				m_treeCtrlDeviceMgr->Expand(treeIdBioMDev);
				m_treeCtrlDeviceMgr->Expand(treeIdFScanner);

				m_cbAutoPlug = new wxCheckBox(this, wxID_ANY, "Auto plug", wxDefaultPosition, wxSize(100, 17));

				wxButton *okButton = new wxButton(this, wxID_OK);
				wxButton *cancelButton = new wxButton(this, wxID_CANCEL);

				wxBoxSizer *boxSizerMainBox = new wxBoxSizer(wxVERTICAL);

				wxBoxSizer *boxSizerTreeList = new wxBoxSizer(wxHORIZONTAL);
				boxSizerTreeList->Add(m_treeCtrlDeviceMgr, 1, wxEXPAND | wxALL, 5);

				wxBoxSizer *boxSizerAutoPlugChkBox = new wxBoxSizer(wxHORIZONTAL);
				boxSizerAutoPlugChkBox->Add(m_cbAutoPlug, 0, wxLEFT | wxRIGHT | wxBOTTOM, 5);

				wxStdDialogButtonSizer *buttonSizer = new wxStdDialogButtonSizer();
				buttonSizer->AddButton(okButton);
				buttonSizer->AddButton(cancelButton);
				buttonSizer->Realize();

				boxSizerMainBox->Add(boxSizerTreeList, 1, wxEXPAND);
				boxSizerMainBox->Add(boxSizerAutoPlugChkBox, 0);
				boxSizerMainBox->Add(buttonSizer, 0, wxALIGN_RIGHT | wxBOTTOM, 5);

				SetSizerAndFit(boxSizerMainBox);
				SetTitle("Device Manager");
				Centre();
			}

			void DeviceManagerDialog::UpdateConfigSettings()
			{
				wxTreeListItem rootId = m_treeCtrlDeviceMgr->GetRootItem();
				wxTreeListItem item = m_treeCtrlDeviceMgr->GetFirstChild(rootId);
				m_treeCtrlDeviceMgr->CheckItem(item);
				while (item.IsOk())
				{
					wxString itemName = m_treeCtrlDeviceMgr->GetItemText(item);
					bool value = true;
					if (wxConfigBase::Get()->Read(itemName, &value))
					{
						if (value)
						{
							m_treeCtrlDeviceMgr->CheckItem(item);
						}
						else
						{
							m_treeCtrlDeviceMgr->UncheckItem(item);
						}
					}
					item = m_treeCtrlDeviceMgr->GetNextItem(item);
				}
				bool value = true;
				m_cbAutoPlug->SetValue(true);
				if (wxConfigBase::Get()->Read("AUTO_PLUGIN", &value))
				{
					m_cbAutoPlug->SetValue(value);
				}
			}

			void DeviceManagerDialog::SaveConfigSettings()
			{
				wxTreeListItem rootId = m_treeCtrlDeviceMgr->GetRootItem();
				wxTreeListItem item = m_treeCtrlDeviceMgr->GetFirstChild(rootId);

				while (item.IsOk())
				{
					wxString itemName = m_treeCtrlDeviceMgr->GetItemText(item);
					bool value = m_treeCtrlDeviceMgr->GetCheckedState(item) == wxCHK_CHECKED;
					wxConfigBase::Get()->Write(itemName, value);
					item = m_treeCtrlDeviceMgr->GetNextItem(item);
				}
				wxConfigBase::Get()->Write("AUTO_PLUGIN", m_cbAutoPlug->GetValue());
			}

			Devices::NDeviceType DeviceManagerDialog::GetDeviceTypes()
			{
				NDeviceType value = ndtNone;
				wxTreeListItem rootId = m_treeCtrlDeviceMgr->GetRootItem();
				wxTreeListItem item = m_treeCtrlDeviceMgr->GetFirstChild(rootId);

				while (item.IsOk())
				{
					NDeviceManagerTreeNodeData *nodeItem = static_cast<NDeviceManagerTreeNodeData*>(m_treeCtrlDeviceMgr->GetItemData(item));
					if (nodeItem != NULL)
					{
						if (m_treeCtrlDeviceMgr->GetCheckedState(item) == wxCHK_CHECKED)
							value = (NDeviceType)(value | nodeItem->GetDeviceType());
					}
					item = m_treeCtrlDeviceMgr->GetNextItem(item);
				}

				if (value == ndtNone)
				{
					value = ndtAny;
					m_treeCtrlDeviceMgr->CheckItem(m_treeCtrlDeviceMgr->GetFirstChild(rootId));
				}
				return value;
			}

			bool DeviceManagerDialog::IsAutoPlug()
			{
				return m_cbAutoPlug->IsChecked();
			}

			void DeviceManagerDialog::SetAutoPlug(bool value)
			{
				m_cbAutoPlug->SetValue(value);
			}

			DeviceManagerDialog::NDeviceManagerTreeNodeData::NDeviceManagerTreeNodeData(const NDeviceType &type) : m_deviceType(type)
			{
			}

			DeviceManagerDialog::NDeviceManagerTreeNodeData::~NDeviceManagerTreeNodeData()
			{
			}

			NDeviceType DeviceManagerDialog::NDeviceManagerTreeNodeData::GetDeviceType()
			{
				return m_deviceType;
			}
		}
	}
}

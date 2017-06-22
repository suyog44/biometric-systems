#ifndef DEVICE_MANAGER_DIALOG_H_INCLUDED
#define DEVICE_MANAGER_DIALOG_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{

#define DeviceManagerForm_STYLE wxCAPTION | wxCLOSE_BOX

			class DeviceManagerDialog : public wxDialog
			{
			public:
				DeviceManagerDialog(wxWindow *parent, const wxWindowID id = wxID_ANY, const wxString &title = wxEmptyString, const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = DeviceManagerForm_STYLE);
				virtual ~DeviceManagerDialog();

				void UpdateConfigSettings();
				void SaveConfigSettings();
				Neurotec::Devices::NDeviceType GetDeviceTypes();
				bool IsAutoPlug();
				void SetAutoPlug(bool value);

			private:
				void CreateGUIControls();

				wxCheckBox *m_cbAutoPlug;
				wxTreeListCtrl * m_treeCtrlDeviceMgr;

				class NDeviceManagerTreeNodeData : public wxClientData
				{
				public:
					NDeviceManagerTreeNodeData(const Neurotec::Devices::NDeviceType &type);
					virtual ~NDeviceManagerTreeNodeData();

					Neurotec::Devices::NDeviceType GetDeviceType();
					wxTreeItemId GetTreeItemId();

				private:
					Neurotec::Devices::NDeviceType m_deviceType;
				};
			};
		}
	}
}

#endif

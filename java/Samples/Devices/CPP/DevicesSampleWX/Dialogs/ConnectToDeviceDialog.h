#ifndef CONNECT_TO_DEVICE_DIALOG_H_INCLUDED
#define CONNECT_TO_DEVICE_DIALOG_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			class ConnectToDeviceDialog : public wxDialog
			{
			public:
				ConnectToDeviceDialog(wxWindow *parent, const wxWindowID id = 1, const wxString &title = wxEmptyString, const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = wxDEFAULT_DIALOG_STYLE | wxRESIZE_BORDER);
				virtual ~ConnectToDeviceDialog();

				Neurotec::Plugins::NPlugin GetSelectedPlugin();
				void SetSelectedPlugin(const Neurotec::Plugins::NPlugin &plugin);
				Neurotec::NPropertyBag GetParameters();

			private:
				void OnSelectedPluginChanged();
				void OnSelectedIndexChanged(wxCommandEvent &evt);
				void UpdatePropertyGrid();
				void OnButtonOkClick(wxCommandEvent &evt);
				void CreateGUIControls();

				enum
				{
					ID_PLUGIN_CMB
				};

				wxComboBox *m_cmbPlugin;
				Neurotec::ComponentModel::NParameterBag m_parameterBag;
				wxPropertyGridManager *m_propertyGridManager;
				wxButton *m_buttonOk;
				wxButton *m_buttonCancel;

				DECLARE_EVENT_TABLE();
			};
		}
	}
}

#endif

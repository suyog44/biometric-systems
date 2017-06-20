#ifndef CONNECT_TO_DEVICE_FORM_H_INCLUDED
#define CONNECT_TO_DEVICE_FORM_H_INCLUDED

#include <wx/combobox.h>
#include <wx/grid.h>

namespace Neurotec { namespace Samples
{

class ConnectToDeviceForm : public wxDialog
{
	DECLARE_EVENT_TABLE()
private:
	enum
	{
		ID_COMBO_PLUGIN,
		ID_GRID_VALUES
	};

	wxComboBox* m_cbPlugins;
	wxGrid * m_grid;

	::Neurotec::NArrayWrapper< ::Neurotec::ComponentModel::NParameterDescriptor> m_params;
	::Neurotec::NPropertyBag m_propertyBag;
	::Neurotec::Plugins::NPlugin m_plugin;
	std::vector< ::Neurotec::Plugins::NPlugin> m_plugins;

	void CreateGUIControls();
	void ResizeColumns();
	void OnResize(wxSizeEvent &event);
	void OnCellClick(wxGridEvent &event);
	void OnOKClick(wxCommandEvent &event);
	void OnSelectedPluginChanged(wxCommandEvent &event);
public:
	ConnectToDeviceForm(wxWindow* parent, wxWindowID id = wxID_ANY, const wxString& title = wxT("Connect to Device"), const wxPoint& pos = wxDefaultPosition, const wxSize& size = wxSize(300,300), long style = wxCAPTION|wxCLOSE_BOX|wxDEFAULT_DIALOG_STYLE|wxMAXIMIZE_BOX|wxMINIMIZE_BOX|wxRESIZE_BORDER|wxSYSTEM_MENU);
	virtual ~ConnectToDeviceForm();

	::Neurotec::NPropertyBag GetProperties();
	::Neurotec::Plugins::NPlugin GetSelectedPlugin();
};

}}

#endif

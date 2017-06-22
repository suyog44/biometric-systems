#ifndef LICENSE_PANEL_H_INCLUDED
#define LICENSE_PANEL_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		class LicensePanel : public wxPanel
		{
		public:
			LicensePanel(wxWindow *parent, wxWindowID id = wxID_ANY, const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = wxTAB_TRAVERSAL, const wxString &name = wxPanelNameStr);
			void RefreshComponentsStatus(const wxString &requiredComponents, const wxString &optionalComponents);

		private:
			void CreateGUIControls();
			void RefreshRequired();
			void RefreshOptional();

			wxTextCtrl *m_textCtrlComponents;
			wxString m_strRequiredcomponents;
			wxString m_strOptionalcomponents;
		};
	}
}
#endif

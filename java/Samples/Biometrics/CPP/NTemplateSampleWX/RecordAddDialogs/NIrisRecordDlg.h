#ifndef NIRIS_RECORD_DLG_H_INCLUDED
#define NIRIS_RECORD_DLG_H_INCLUDED

namespace Neurotec { namespace Samples
{
	class NIrisRecordDlg : public wxDialog
	{
	private:
		enum
		{
			ID_WIDTH_SPIN_CTRL,
			ID_HEIGHT_SPIN_CTRL,
			ID_BUTTON_OK,
			ID_BUTTON_CLOSE,
		};
	public:
		NIrisRecordDlg(wxWindow *parent, const wxWindowID id = 1, const wxString &title = wxT(""), const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize);
		virtual ~NIrisRecordDlg();

		int GetWidth();
		int GetHeight();

	protected:
		void CreateGUIControls();

	private:
		wxSpinCtrl *spinctrlWidth;
		wxSpinCtrl *spinctrlHeight;
		wxPanel *panel;
		wxBoxSizer *vbox;
		wxBoxSizer *hbox1;
		wxBoxSizer *hbox2;
		wxBoxSizer *hbox3;
		wxFlexGridSizer *flexGridSizer;
		wxButton *btnOk;
		wxButton *btnCancel;
		wxStaticText *labelWidth;
		wxStaticText *labelHeight;

	private:
		DECLARE_EVENT_TABLE();
	};
}}
#endif

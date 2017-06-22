#ifndef NFINGER_RECORD_DLG_H_INCLUDED
#define NFINGER_RECORD_DLG_H_INCLUDED

namespace Neurotec { namespace Samples
{
	class NFingerRecordDlg : public wxDialog
	{
	private:
		enum
		{
			ID_WIDTH_SPIN_CTRL,
			ID_HEIGHT_SPIN_CTRL,
			ID_HORZONTAL_RESOLUTION_SPIN_CTRL,
			ID_VIRTICAL_RESOLUTION_SPIN_CTRL,
			ID_BUTTON_OK,
			ID_BUTTON_CLOSE,
		};
	public:
		NFingerRecordDlg(wxWindow *parent, bool isPalm, const wxWindowID id = 1, const wxString &title = wxT(""), const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize);
		virtual ~NFingerRecordDlg();

		int GetWidth();
		int GetHeight();
		int GetHorizontalResolution();
		int GetVirticalResolution();

	protected:
		void CreateGUIControls(bool isPalm);

	private:
		wxStaticText *labelWidth;
		wxStaticText *labelHeight;
		wxStaticText *labelHorizontalResolution;
		wxStaticText *labelVirticalResolution;
		wxSpinCtrl *spinctrlWidth;
		wxSpinCtrl *spinctrlHeight;
		wxSpinCtrl *spinctrlHorizontalResolution;
		wxSpinCtrl *spinctrlVirticalResolution;
		wxPanel *panel;
		wxBoxSizer *vbox;
		wxBoxSizer *hbox1;
		wxBoxSizer *hbox2;
		wxBoxSizer *hbox3;
		wxFlexGridSizer *flexGridSizer;
		wxButton *btnOk;
		wxButton *btnCancel;

	private:
		DECLARE_EVENT_TABLE();
	};
}}
#endif

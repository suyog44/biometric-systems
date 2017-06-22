#ifndef SIMPLE_IRISES_SAMPLE_FORM_H_INCLUDED
#define SIMPLE_IRISES_SAMPLE_FORM_H_INCLUDED

#define IrisesSampleForm_STYLE wxCAPTION|wxRESIZE_BORDER|wxSYSTEM_MENU|wxMINIMIZE_BOX|wxMAXIMIZE_BOX|wxCLOSE_BOX

namespace Neurotec
{
	namespace Samples
	{
		class SimpleIrisesSampleForm : public wxFrame
		{
		public:
			SimpleIrisesSampleForm(wxWindow *parent, const int id = 1, const wxString &title = wxEmptyString, const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = IrisesSampleForm_STYLE);
			SimpleIrisesSampleForm();
			~SimpleIrisesSampleForm();

		private:
			void OnNoteBookPageChange(wxBookCtrlEvent& event);
			void CreateGUIControls();

			wxNotebook* m_notebook;
			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			DECLARE_EVENT_TABLE();
		};
	}
}

#endif // SIMPLE_IRISES_SAMPLE_FORM_H_INCLUDED

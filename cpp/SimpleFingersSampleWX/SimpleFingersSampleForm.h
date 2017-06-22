#ifndef SIMPLE_FINGERS_SAMPLE_FORM_H_INCLUDED
#define SIMPLE_FINGERS_SAMPLE_FORM_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		class SimpleFingersSampleForm : public wxFrame
		{
		public:
			SimpleFingersSampleForm(wxWindow *parent = NULL, const int id = 1, const wxString & title = wxEmptyString, const wxPoint & pos = wxDefaultPosition,
				const wxSize & size = wxDefaultSize, long style = wxDEFAULT_FRAME_STYLE, const wxString & name = "Enroll");
			~SimpleFingersSampleForm();

		private:
			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
			void CreateGUIControls();
			void OnNoteBookPageChange(wxBookCtrlEvent & event);
			DECLARE_EVENT_TABLE();
		};
	}
}
#endif

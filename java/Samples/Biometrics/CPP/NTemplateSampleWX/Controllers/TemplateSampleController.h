#ifndef TEMPLATE_SAMPLE_CONTROLLER_H_INCLUDED
#define TEMPLATE_SAMPLE_CONTROLLER_H_INCLUDED

namespace Neurotec { namespace Samples { namespace Controllers
{
	class TemplateSampleController
	{
	public:
		TemplateSampleController();
		virtual ~TemplateSampleController();

		Neurotec::Biometrics::NFrictionRidge GetDataForFingerView(Neurotec::Biometrics::NFRecord record);
		void RemoveItem(Neurotec::NObject obj, int itemId);
		void SaveItem(wxWindow* win, Neurotec::NObject obj, int itemId);
		void SaveBuffer(wxWindow* win, wxString dialogMask, Neurotec::IO::NBuffer buff);
		void OpenBuffer(wxWindow* win, wxString dialogMask, wxString type);
		wxString GetFileName();
		void SetFileName(wxString fileName);
		Neurotec::Biometrics::NTemplate GetTemplate();
		void SetTemplate(Neurotec::Biometrics::NTemplate nTempl);
		void UpdateMenu(wxMenuBar* menuBar, Neurotec::Biometrics::NTemplate);

	private:
		Neurotec::Biometrics::NTemplate m_template;
		wxString m_fileName;
	};
}}}
#endif

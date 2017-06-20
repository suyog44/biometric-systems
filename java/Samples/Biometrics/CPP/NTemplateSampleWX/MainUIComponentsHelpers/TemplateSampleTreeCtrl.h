#ifndef TEMPLATE_SAMPLE_TREECTRL_H_INCLUDED
#define TEMPLATE_SAMPLE_TREECTRL_H_INCLUDED

namespace Neurotec { namespace Samples{	namespace MainUIComponentsHelpers
{
	class TemplateSampleTreeCtrl : public wxTreeCtrl
	{
	public:
		TemplateSampleTreeCtrl(wxWindow *parent, wxWindowID id = wxID_ANY,
			const wxPoint& pos = wxDefaultPosition,
			const wxSize& size = wxDefaultSize,
			long style = wxTR_HAS_BUTTONS | wxTR_LINES_AT_ROOT);

		virtual ~TemplateSampleTreeCtrl();

		Neurotec::NObject GetCurrentSelectionData(wxTreeItemId itemId);
		Neurotec::NObject GetCurrentSelectionParentData(wxTreeItemId itemId);
		int GetCurrentSelectionId(wxTreeItemId itemId);
		void UpdateTreeView();
		void SetTemplate(Neurotec::Biometrics::NTemplate ntemplate);
		void SetFilename(wxString filename);

		void AddRootNode();
		void AddFingers();
		void AddPalms();
		void AddFaces();
		void AddIrises();
		void AddVoices();

		void AddFingerChildNodes();
		void AddPalmChildNodes();
		void AddFaceChildNodes();
		void AddIrisChildNodes();
		void AddVoiceChildNodes();
	private:

		Neurotec::Biometrics::NTemplate m_template;
		wxString m_fileName;
		wxTreeItemId m_currentTreeItemId;

		wxTreeItemId m_rootId;
		wxTreeItemId m_subRootId;
		wxTreeItemId m_childId;

		int m_prvFingerCount;
		int m_prvPalmCount;
		int m_prvFaceCount;
		int m_prvIrisCount;
		int m_prvVoiceCount;
	};
}}}
#endif

#ifndef TEMPLATE_SAMPLE_FRM_H_INCLUDED
#define TEMPLATE_SAMPLE_FRM_H_INCLUDED

#include "MainUIComponentsHelpers/TemplateSampleTreeCtrl.h"
#include "MainUIComponentsHelpers/TemplateSamplePropertyGrid.h"
#include "Controllers/TemplateSampleController.h"

namespace Neurotec { namespace Samples
{
#undef TemplateSampleForm_STYLE
#define TemplateSampleForm_STYLE wxCAPTION | wxRESIZE_BORDER | wxSYSTEM_MENU | wxMINIMIZE_BOX | wxMAXIMIZE_BOX | wxCLOSE_BOX

	class TemplateSampleForm : public wxFrame
	{
	public:
		TemplateSampleForm(wxWindow *parent, const wxWindowID id = 1, const wxString &title = wxT(""), const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = TemplateSampleForm_STYLE);
		virtual ~TemplateSampleForm();

	private:
		void CreateGUIControls();
		void InitMembers();
		void UpdateMenu();
		void OnSelectedImageChanged(wxTreeItemId itemId);
		void RefreshTemplateView();
		void ClearData();
		void UpdatePropertyGridAndFingerView(wxTreeItemId itemId);
		void SaveTemplateFile(wxFileDialog dialog, Neurotec::IO::NBuffer buff);
		void OnFingerViewClick(wxMouseEvent &event);
		void OnPropertyGridValueChange(wxPropertyGridEvent &event);
		void OnPropertyGridValueChanging(wxPropertyGridEvent &event);
		void Remove(wxCommandEvent &event);
		void SaveItem(wxCommandEvent &event);
		void TreeViewSelectionChanged(wxTreeEvent &event);
		void TreeViewSelectionChanging(wxTreeEvent &event);
		void NewTemplate(wxCommandEvent &event);
		void SaveTemplate(wxCommandEvent &event);
		void OpenTemplate(wxCommandEvent &event);
		void About(wxCommandEvent &event);
		void OnClose(wxCloseEvent &event);
		void OnExitClick(wxCommandEvent &event);
		void ChangeNTemplate(wxCommandEvent &event);

	private:
		wxSplitterWindow* splitterMain;
		wxSplitterWindow* splitterLeft;
		wxSplitterWindow* splitterRight;
		wxFlexGridSizer* fgSizer;
		wxStaticText* emptyText;
		wxMenuBar* menuBar;
		wxMenu* menu;
		Neurotec::Biometrics::Gui::wxNFingerView* m_fingerView;
		Neurotec::Biometrics::Gui::wxNPalmView* m_palmView;
		Neurotec::Samples::MainUIComponentsHelpers::TemplateSampleTreeCtrl* m_templateSampleTreeCtrl;
		Neurotec::Samples::MainUIComponentsHelpers::TemplateSamplePropertyGrid* m_templateSamplePropertyGrid;
		Neurotec::Samples::Controllers::TemplateSampleController* m_templateSampleController;
		wxTreeItemId m_currentTreeItemId;
		::Neurotec::Gui::wxNViewZoomSlider * m_zoomSlider;

		DECLARE_EVENT_TABLE();
	};
}}
#endif

#ifndef FIRST_PAGE_H_INCLUDED
#define FIRST_PAGE_H_INCLUDED

#include <Common/TabPage.h>

namespace Neurotec { namespace Samples
{

class FirstPage : public TabPage
{
public:
	FirstPage(wxWindow *parent, wxWindowID id);

	virtual ~FirstPage();

private:
	void OnAboutClick(wxCommandEvent& event);

	void OnNewSubjectClick(wxCommandEvent& event);

	void OnOpenSubjectClick(wxCommandEvent& event);

	void OnSettingsClick(wxCommandEvent& event);

	void OnChangeDatabaseClick(wxCommandEvent& event);

	void RegisterGuiEvents();

	void UnregisterGuiEvents();

	void CreateGuiElements();

private:
	wxButton *m_btnNewSubject;
	wxButton *m_btnOpenSubject;
	wxButton *m_btnSettings;
	wxButton *m_btnChangeDatabase;
	wxButton *m_btnAbout;
};

}}

#endif

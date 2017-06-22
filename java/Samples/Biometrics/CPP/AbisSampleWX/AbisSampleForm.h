#ifndef ABIS_SAMPLE_FORM_H_INCLUDED
#define ABIS_SAMPLE_FORM_H_INCLUDED

#include <Common/TabPage.h>
#include <Common/TabController.h>

#include <Settings/SettingsPanel.h>

namespace Neurotec { namespace Samples
{

#define AbisSampleForm_STYLE \
	wxCAPTION | wxRESIZE_BORDER | wxSYSTEM_MENU | wxMINIMIZE_BOX | wxMAXIMIZE_BOX | wxCLOSE_BOX

class AbisSampleForm : public wxFrame
{
public:
	AbisSampleForm(wxWindow *parent, const wxWindowID id = 1, const wxString &title = wxEmptyString);

	virtual ~AbisSampleForm();

	void About();
	void CreateSubject(const ::Neurotec::Biometrics::NSubject& subject = NULL);
	void OpenSubject();
	void Settings();
	void ChangeDatabase();
	void GetSubject();

private:
	void MenuExitClick(wxCommandEvent& event);
	void MenuAboutClick(wxCommandEvent& event);
	void MenuCreateSubjectClick(wxCommandEvent& event);
	void MenuOpenSubjectClick(wxCommandEvent& event);
	void MenuSettingsClick(wxCommandEvent& event);
	void MenuChangeDatabaseClick(wxCommandEvent& event);
	void MenuGetSubjectClick(wxCommandEvent& event);
	void OnClosing(wxCloseEvent & event);

	void RegisterGuiEvents();
	void UnregisterGuiEvents();
	void CreateGUIControls();
	::Neurotec::Biometrics::NSubject RecreateSubject(const ::Neurotec::Biometrics::NSubject& subject);

private:
	enum
	{
		ID_MENU_CHANGE_DATABASE,
		ID_MENU_NEW_SUBJECT,
		ID_MENU_OPEN_SUBJECT,
		ID_MENU_GET_SUBJECT,
		ID_MENU_SETTINGS
	};

	Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;

	wxButton *m_btnNewSubject;
	wxButton *m_btnOpenSubject;
	wxButton *m_btnSettings;
	wxButton *m_btnChangeDatabase;
	wxButton *m_btnGetSubject;
	wxMenuBar *m_menuBar;
	wxToolBar *m_toolBar;
	TabController *m_tabbedPanel;
	SettingsPanel *m_settingsPanel;
	TabPage::State m_settingsPanelState;

private:
	DECLARE_EVENT_TABLE();
};

}}

#endif


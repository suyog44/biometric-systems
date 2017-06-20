#ifndef SETTINGS_PANEL_H_INCLUDED
#define SETTINGS_PANEL_H_INCLUDED

#include <Common/TabPage.h>

#include <Settings/BaseSettingsPage.h>
#include <Settings/GeneralSettingsPage.h>
#include <Settings/FingersSettingsPage.h>
#include <Settings/FacesSettingsPage.h>
#include <Settings/IrisesSettingsPage.h>
#include <Settings/PalmsSettingsPage.h>
#include <Settings/VoicesSettingsPage.h>

namespace Neurotec { namespace Samples
{

class SettingsPanel : public TabPage
{
private:
	::Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
	::Neurotec::NPropertyBag m_properties;

private:
	BaseSettingsPage *m_activePage;

private:
	wxListBox *m_list;
	wxButton *m_btnDefault;
	wxButton *m_btnOK;
	wxButton *m_btnCancel;
	wxBoxSizer *m_pageLayout;
	wxPanel *m_pagePanel;

private:
	int m_generalSelIndex;
	int m_facesSelIndex;
	int m_fingersSelIndex;
	int m_irisesSelIndex;
	int m_palmsSelIndex;
	int m_voicesSelIndex;

private:
	GeneralSettingsPage *m_generalPage;
	FingersSettingsPage *m_fingersPage;
	FacesSettingsPage *m_facesPage;
	IrisesSettingsPage *m_irisesPage;
	PalmsSettingsPage *m_palmsPage;
	VoicesSettingsPage *m_voicesPage;

public:
	SettingsPanel(wxWindow *parent, wxWindowID winid = wxID_ANY);
	virtual ~SettingsPanel();

public:
	void Initialize(::Neurotec::Biometrics::Client::NBiometricClient biometricClient);

private:
	void SetActivePage(int page);
	void LoadParameters();

private:
	void OnListSelectionChanged(wxCommandEvent& event);
	void OnDefaultClick(wxCommandEvent &event);
	void OnOKClick(wxCommandEvent &event);
	void OnCancelClick(wxCommandEvent &event);

private:
	void RegisterGuiEvents();
	void UnregisterGuiEvents();
	void CreateGUIControls();
};

}}

#endif


#ifndef GENERAL_SETTINGS_PAGE_H_INCLUDED
#define GENERAL_SETTINGS_PAGE_H_INCLUDED

#include <Settings/BaseSettingsPage.h>

namespace Neurotec { namespace Samples
{

class GeneralSettingsPage : public BaseSettingsPage
{
public:
	GeneralSettingsPage(wxWindow *parent, wxWindowID winid = wxID_ANY);

	virtual ~GeneralSettingsPage();

	void Load();

	void Reset();

private:
	void OnMatchingThresholdChanged(wxCommandEvent& event);

	void OnMaximalResultCountChanged(wxSpinEvent& event);

	void OnReturnMatchingDetailsChanged(wxCommandEvent& event);

	void OnFirstResultOnlyChanged(wxCommandEvent& event);

	double MatchingThresholdToFAR(int threshold);

	int FARToMatchingThreshold(double far);

	wxString MatchingThresholdToFARString(int matchingThreshold);

	int FARStringToMatchingThreshold(const wxString& farString);

	NValue GetDefaultPropertyValue(const wxString& name);

	void RegisterGuiEvents();

	void UnregisterGuiEvents();

	void CreateGUIControls();

private:
	wxChoice *m_choiceMatchingTreshold;
	wxSpinCtrl *m_spinMaximalResultsCount;
	wxCheckBox *m_chkRetunMatchingDetails;
	wxCheckBox *m_chkFirstResultOnly;
};

}}

#endif


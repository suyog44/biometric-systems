#ifndef OPTIONS_DLG_H_INCLUDED
#define OPTIONS_DLG_H_INCLUDED

#include <wx/spinctrl.h>

namespace Neurotec { namespace Samples
{

#undef OptionsDlg_STYLE
#ifdef __WXMAC__
#define OptionsDlg_STYLE wxCAPTION | wxRESIZE_BORDER | wxSYSTEM_MENU
#else
#define OptionsDlg_STYLE wxCAPTION | wxRESIZE_BORDER | wxSYSTEM_MENU | wxCLOSE_BOX
#endif

struct EnumOptionEntry;

class OptionsDlg : public wxDialog
{
public:
	OptionsDlg(wxWindow *parent, ::Neurotec::Biometrics::Client::NBiometricClient biometricClient, wxWindowID id = wxID_ANY, const wxString &title = wxT("Options"), const wxPoint& pos = wxDefaultPosition, const wxSize& size = wxSize(450, 600), long style = OptionsDlg_STYLE);
	virtual ~OptionsDlg();
	static void SaveOptions(::Neurotec::Biometrics::Client::NBiometricClient biometricClient);
	static void LoadOptions(::Neurotec::Biometrics::Client::NBiometricClient biometricClient);

private:
	Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
	::Neurotec::NPropertyBag m_properties;

private:
	//Enroll
	wxSlider *m_sliderQualityThreshold;
	wxSpinCtrl *m_spinInnerBoundaryFrom;
	wxSpinCtrl *m_spinInnerBoundaryTo;
	wxSpinCtrl *m_spinOuterBoundaryFrom;
	wxSpinCtrl *m_spinOuterBoundaryTo;

	//Identify
	wxComboBox *m_comboFAR;
	wxSlider *m_sliderMaximalRotation;
	wxComboBox *m_comboMatchingSpeed;
	wxSpinCtrl *m_spinMaximalResultCount;
	wxCheckBox *m_cbFirstResultOnly;

	wxStdDialogButtonSizer* m_sdbSizer1;
	wxButton* m_sdbSizer1OK;
	wxButton* m_sdbSizer1Cancel;

private:
	wxWindow* CreateEnrollPage(wxWindow *parent);
	wxWindow* CreateMatchingPage(wxWindow *parent);
	int EnumValueToIndex(const EnumOptionEntry *entries, int value);
	int EnumIndexToValue(const EnumOptionEntry *entries, int index);
	wxArrayString GetEnumStrings(const EnumOptionEntry *entries);
	void OnClose(wxCloseEvent &event);
	void CreateGUIControls();
	void OnDefault(wxCommandEvent &event);
	void OnOK(wxCommandEvent &event);
	void UpdateGui();

	NValue GetDefaultPropertyValue(const NStringWrapper & name);
	double MatchingThresholdToFAR(int th);
	int FARToMatchingThreshold(double f);
	wxString MatchingThresholdToFARString(int matchingThreshold);
	int FARStringToMatchingThreshold(const wxString& farString);

private:
	DECLARE_EVENT_TABLE();
};

}}

#endif

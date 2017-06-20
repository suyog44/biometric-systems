#ifndef OPTIONS_DLG_H_INCLUDED
#define OPTIONS_DLG_H_INCLUDED

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
	private:
		DECLARE_EVENT_TABLE();

	public:
		OptionsDlg(wxWindow *parent, ::Neurotec::Biometrics::Client::NBiometricClient biometricClient, wxWindowID id = wxID_ANY, const wxString &title = wxT("Options"), const wxPoint& pos = wxDefaultPosition, const wxSize& size = wxSize(450, 480), long style = OptionsDlg_STYLE);
		virtual ~OptionsDlg();

		static void SaveOptions(::Neurotec::Biometrics::Client::NBiometricClient biometricClient);
		static void LoadOptions(::Neurotec::Biometrics::Client::NBiometricClient biometricClient);

	private:
		// enroll
		wxSpinCtrl * m_spinMinMinutiaCount;
		wxSlider *m_sliderQualityThreshold;
		wxComboBox *m_comboTemplateSize;
		wxComboBox *m_comboExtractedRidgeCounts;
		wxCheckBox *m_cbReturnBinarizedImage;
		wxCheckBox *m_cbFastExtraction;

		// matching
		wxComboBox *m_comboFAR;
		wxSlider *m_sliderMaximalRotation;
		wxComboBox *m_comboMatchingSpeed;
		wxSpinCtrl *m_spinMaximalResultCount;
		wxCheckBox *m_cbFirstResultOnly;

		wxStdDialogButtonSizer* m_sdbSizer1;
		wxButton* m_sdbSizer1OK;
		wxButton* m_sdbSizer1Cancel;

		::Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
		::Neurotec::NPropertyBag m_properties;

	private:
		void SaveOptions();

		wxWindow* CreateEnrollPage(wxWindow *parent);
		wxWindow* CreateMatchingPage(wxWindow *parent);
		int EnumValueToIndex(const EnumOptionEntry *entries, int value);
		int EnumIndexToValue(const EnumOptionEntry *entries, int index);
		wxArrayString GetEnumStrings(const EnumOptionEntry *entries);
		void OnClose(wxCloseEvent &event);
		void CreateGUIControls();
		void OnReset(wxCommandEvent &event);
		void OnOK(wxCommandEvent &event);

		void UpdateGui();
		NValue GetDefaultPropertyValue(const wxString & name);

		double MatchingThresholdToFAR(int th);
		int FARToMatchingThreshold(double f);
		wxString MatchingThresholdToFARString(int matchingThreshold);
		int FARStringToMatchingThreshold(const wxString& farString);
};

}}

#endif

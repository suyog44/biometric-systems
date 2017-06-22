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
		OptionsDlg(wxWindow *parent, wxWindowID id = 2, const wxString &title = wxT("Options"), const wxPoint& pos = wxDefaultPosition, const wxSize& size = wxDefaultSize, long style = OptionsDlg_STYLE);
		virtual ~OptionsDlg();

		void SetBiometricClient( ::Neurotec::Biometrics::Client::NBiometricClient biometricClient);
		::Neurotec::Biometrics::Client::NBiometricClient GetBiometricClient();

	private:
		::Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;

		void LoadOptions();
		void SaveOptions();
		void DefaultOptions();

		wxComboBox * m_comboMatchingThreshold;
		wxSpinCtrl * m_spinMaxResults;
		wxCheckBox * m_chbFirstResultOnly;
		wxComboBox * m_comboTemplateSize;
		wxComboBox * m_comboMatchingSpeed;
		wxSpinCtrl * m_spinMinIOD;
		wxSpinCtrl * m_spinConfidenceThreshold;
		wxSpinCtrl * m_spinMaxRoll;
		wxSpinCtrl * m_spinMaxYaw;
		wxSpinCtrl * m_spinQualityThreshold;
		wxChoice * m_choiceLivenessMode;
		wxSpinCtrl * m_spinLivenessThreshold;
		wxCheckBox * m_chbDetectAllFeatures;
		wxCheckBox * m_chbDetectBaseFeatures;
		wxCheckBox * m_chbDetermineGender;
		wxCheckBox * m_chbDetermineAge;
		wxCheckBox * m_chbDetectProperties;
		wxCheckBox * m_chbRecognizeEmotion;
		wxCheckBox * m_chbRecognizeExpression;
		wxCheckBox * m_chbCreateThumbnail;
		wxSpinCtrl * m_spinThumbnailWidth;

		wxStdDialogButtonSizer* m_sdbSizer1;
		wxButton* m_sdbSizer1OK;
		wxButton* m_sdbSizer1Cancel;

	private:
		wxWindow* CreateExtractionPage(wxWindow *parent);
		wxWindow* CreateMatchingPage(wxWindow *parent);
		virtual void OnCheckBoxClicked(wxCommandEvent& event);
		void OnClose(wxCloseEvent &event);
		void CreateGUIControls();
		void OnDefault(wxCommandEvent &event);
		void OnOK(wxCommandEvent &event);
		void OnEnableLivenessCheck(wxCommandEvent &/*event*/);
		void EnableControls();
		static wxString MatchingThresholdToFARString(int matchingThreshold);
		static double MatchingThresholdToFAR(int th);
		static int FARStringToMatchingThreshold(const wxString& farString);
		static int FARToMatchingThreshold(double f);
};

}}

#endif

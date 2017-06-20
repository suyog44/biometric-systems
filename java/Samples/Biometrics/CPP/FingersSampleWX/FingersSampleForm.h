#ifndef FINGERS_SAMPLE_FRM_H_INCLUDED
#define FINGERS_SAMPLE_FRM_H_INCLUDED

#include "OptionsDlg.h"

namespace Neurotec { namespace Samples
{

#undef FingersSampleForm_STYLE
#define FingersSampleForm_STYLE wxCAPTION | wxRESIZE_BORDER | wxSYSTEM_MENU | wxMINIMIZE_BOX | wxMAXIMIZE_BOX | wxCLOSE_BOX

class FingersSampleForm : public wxFrame
{
	private:
		enum
		{
			ID_STATUSBAR = 1000,

			ID_MNU_CANCEL,
			ID_MNU_EXIT,
			ID_MNU_CLEAR_LOG,
			ID_MNU_CLEAR_DB,
			ID_MNU_OPTIONS,
			ID_MNU_ABOUT,
			ID_MNU_SAVE_IMAGE,

			ID_TOOLBAR,

			ID_BUTTON_ENROLL,
			ID_BUTTON_IDENTIFY,
			ID_BUTTON_VERIFY,

			ID_COMBO_SOURCE,
			ID_COMBO_SCANNER,

			ID_BUTTON_SAVE_IMAGE,

			ID_FINGER_VIEW_LEFT,
			ID_FINGER_VIEW_RIGHT,
			ID_RICHTEXTCTRLLOG,
			ID_LISTCTRLRESULTS,

			ID_PROGRESS_PULSER,
		};

		enum
		{
			ID_SCANNER_ADDED = wxID_HIGHEST +1,
			ID_SCANNER_REMOVED,
		};

		enum
		{
			SOURCE_FILE = 0,
			SOURCE_DIRECTORY,
			SOURCE_SCANNER,
		};

		enum WorkMode
		{
			MODE_ENROLL,
			MODE_IDENTIFY,
			MODE_VERIFY,
			MAX_WORK_MODE,
		};

	public:
		FingersSampleForm(wxWindow *parent, const wxWindowID id = 1, const wxString &title = wxT(""), const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = FingersSampleForm_STYLE);
		virtual ~FingersSampleForm();

	private:

		virtual bool ProcessEvent(wxEvent& event);

		void CreateGUIControls();
		void Clear();
		void BeforeProcessImage();

		void OnEnroll(wxCommandEvent &event);
		void OnIdentify(wxCommandEvent &event);
		void OnVerify(wxCommandEvent &event);

		void OnSaveImage(wxCommandEvent &event);
		void OnOpenFile(wxCommandEvent &event);
		void OnOpenDirectory(wxCommandEvent &event);
		void OnCancelAction(wxCommandEvent &event);
		void MnuExitClick(wxCommandEvent &event);
		void MnuClearLogClick(wxCommandEvent &event);
		void MnuClearDatabaseClick(wxCommandEvent &event);
		void MnuOptionsClick(wxCommandEvent &event);
		void MnuAboutClick(wxCommandEvent &event);
		void OnClose(wxCloseEvent &event);
		void OnResultColumnClick(wxListEvent &event);
		void OnResultSelected(wxListEvent &event);
		static int wxCALLBACK ResultsCompareFunction(wxIntPtr item1, wxIntPtr item2, wxIntPtr sortData);
		void OnMinutiaSelectionChanged(wxCommandEvent &event);

		void MatchingResultsAdd(::Neurotec::Biometrics::NMatchingResult result);
		void MatchingResultsClear();

		void AppendText(wxString text, const wxColour &color = wxColour(0, 0, 0));
		void AppendTextError(wxString text);

		void GetFileList(wxArrayString & fileList);

		void ProcessMatchingResults(::Neurotec::Biometrics::NSubject subject);

		void AddPendingOperation(NAsyncOperation operation);
		void RemovePendingOperation(NAsyncOperation operation);
		void CancelPendingOperations();

		void OnProgressPulse(wxTimerEvent &event);
		void OnOperationsDone();

		void OnOperationCompleted(wxCommandEvent &event);
		static void OnOperationCompletedCallback(::Neurotec::EventArgs args);

		void OnScannersCollectionChanged(wxCommandEvent &event);
		static void OnScannersCollectionChangedCallback(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Devices::NDevice> args);

	private:
		::Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
		::std::vector<Neurotec::NAsyncOperation> m_pendingOperations;

#ifndef N_PRODUCT_NO_FINGER_SCANNERS
		Neurotec::Devices::NDeviceManager m_deviceManager;
#endif

		wxRichTextCtrl *m_richTextCtrlLog;
		wxListCtrl *m_resultsList;
		::Neurotec::Biometrics::Gui::wxNFingerView *m_fingerViewLeft;
		::Neurotec::Biometrics::Gui::wxNFingerView *m_fingerViewRight;
		std::list< ::Neurotec::NIndexPair> m_matedMinutiae;

		wxButton * m_buttonEnroll;
		wxCheckBox * m_chbCheckForDuplicates;
		wxButton * m_buttonIdentify;
		wxButton * m_buttonVerify;

		wxComboBox * m_comboSource;

		wxButton *m_buttonSaveImage;

		wxGauge * m_progressGauge;
		wxTimer * m_progressPulser;

		int m_sourceIdx;

		::Neurotec::Biometrics::NFRecord m_leftRecord;
		::Neurotec::Biometrics::NFRecord m_rightRecord;
		bool m_sortResultsDescending;

		bool m_isClosing;
		bool m_isCancelling;
	private:
		DECLARE_EVENT_TABLE();
};

}}

#endif

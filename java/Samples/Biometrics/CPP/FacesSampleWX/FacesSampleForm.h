#ifndef FACES_SAMPLE_FRM_H_INCLUDED
#define FACES_SAMPLE_FRM_H_INCLUDED

#include "OptionsDlg.h"
#include "MatchingResultsView.h"
#include "IcaoWarningsView.h"

namespace Neurotec { namespace Samples
{

#undef FacesSampleForm_STYLE
#define FacesSampleForm_STYLE wxCAPTION | wxRESIZE_BORDER | wxSYSTEM_MENU | wxMINIMIZE_BOX | wxMAXIMIZE_BOX | wxCLOSE_BOX

#define MAX_TASK_COUNT 8

class FacesSampleForm : public wxFrame
{
	private:
		DECLARE_EVENT_TABLE();

	public:
		FacesSampleForm(wxWindow *parent, const wxWindowID id = 1, const wxString &title = wxT(""), const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = FacesSampleForm_STYLE);
		virtual ~FacesSampleForm();
	protected:
		virtual bool ProcessEvent(wxEvent& event);
		void MnuEnrollClick(wxCommandEvent &event);
		void MnuIdentifyClick(wxCommandEvent &event);
		void OnCancelAction(wxCommandEvent &event);
		void MnuExitClick(wxCommandEvent &event);
		void MnuClearLogClick(wxCommandEvent &event);
		void MnuClearDatabaseClick(wxCommandEvent &event);
		void MnuOptionsClick(wxCommandEvent &event);
		void MnuAboutClick(wxCommandEvent &event);
		void OnSourcesChanged(wxCommandEvent &event);
		void OnClientActionCompleted(wxCommandEvent &event);
		void OnExtractionSourceSelected(wxCommandEvent &event);
		void OnMediaFormatSelected(wxCommandEvent &event);
		void OnClose(wxCloseEvent &event);
		void OnRadioButtonCheckedChanged(wxCommandEvent &event);

		static void OnDevicesCollectionChanged(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Devices::NDevice> args);

		wxString GetIcaoWarningsString(const ::Neurotec::Biometrics::NLAttributes & attributes) const;
		void OnEnrollCompleted( ::Neurotec::Biometrics::NBiometricTask enrollTask);
		void OnCreateTemplateCompleted( ::Neurotec::Biometrics::NBiometricTask createTempalteTask);
		void OnIdentifyCompleted( ::Neurotec::Biometrics::NBiometricTask identifyCompleted);

		void RestartCapture();
		void CancelAction(bool force);
		void ClearMediaFormats();
		void AppendText(wxString text);
		void AppendTextLine(wxString text);

		void EnableControls();
		void EnableControls(bool enable);
		void CreateGUIControls();
		void PrepareViews(bool isBusy, bool checkIcao, bool fromFile);

	private:
		::Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
		std::vector< ::Neurotec::NAsyncOperation> m_asyncOperations;
		std::vector< ::Neurotec::Media::NMediaFormat> m_mediaFormats;
		::Neurotec::Biometrics::NBiometricOperations m_currentOperations;
		::Neurotec::Biometrics::NSubject m_shownSubject;
		bool m_checkIcao;
		wxArrayString m_files;
		int m_fileIndex;
		bool m_cancel;
		bool m_close;
		bool m_restartCapture;

		void OnAsyncOperationStarted( ::Neurotec::NAsyncOperation operation);
		void OnAsyncOperationCompleted( ::Neurotec::NAsyncOperation operation);
		static void AsyncOperationCompletedCallback(::Neurotec::EventArgs args);

		void StartCreateTemplateFromFile(wxString fileName);
		void StartCreateTemplateFromCamera(bool manual = true);
		bool GetFileQueue(bool isDirectory);
		wxString GetNextFile();
		bool IsFromCamera();

	private:
		MatchingResultsView *m_resultsView;
		wxRichTextCtrl *richTextCtrlLog;
		wxSplitterWindow *splitterWindowHor;
		::Neurotec::Biometrics::Gui::wxNFaceView *m_faceView;
		IcaoWarningsView * m_icaoView;
		wxMenuBar *menuBar;
		wxButton *buttonIdentify;
		wxButton *buttonEnroll;
		wxCheckBox * chbCheckForDuplicates;
		wxCheckBox * chbCheckIcao;
		wxComboBox *comboSource;
		wxComboBox *comboFormat;
		wxMenuItem * mnuIdentify;
		wxMenuItem * mnuEnroll;
		wxMenuItem * mnuCancel;
		wxRadioButton * m_radioOriginal;
		wxRadioButton * m_radioSegmented;

	private:
		enum
		{
			ID_STATUSBAR = 1000,

			ID_MNU_JOBS,
			ID_MNU_ENROLL,
			ID_MNU_IDENTIFY,
			ID_MNU_CANCEL,
			ID_MNU_EXIT,
			ID_MNU_TOOLS,
			ID_MNU_CLEAR_LOG,
			ID_MNU_CLEAR_DB,
			ID_MNU_OPTIONS,
			ID_MNU_HELP,
			ID_MNU_ABOUT,

			ID_TOOLBAR,
			ID_BUTTON_ENROLL,
			ID_BUTTON_IDENTIFY,

			ID_COMBO_SOURCE,
			ID_COMBO_MODE,

			ID_SPLITTERWINDOWVER,
			ID_SCROLLEDWINDOWIMAGE,
			ID_SPLITTERWINDOWHOR,
			ID_RICHTEXTCTRLLOG,
			ID_LISTCTRLRESULTS,
		};

		enum
		{
			ID_EXTRACT_EXIT = wxID_EXIT,
			ID_EXTRACT_NULL = wxID_HIGHEST + 1,
			ID_EXTRACT_ADDED_RESULTS,
			ID_EXTRACT_WRITE,
			ID_WRITE,
			ID_WRITE_ERROR,
			ID_IDENTIFY_COMPLETED,
			ID_ENROLL_COMPLETED,
			ID_CREATE_TEMPLATE_COMPLETED,
		};

		enum
		{
			ID_DEVICES_RESET = 0,
			ID_DEVICES_REMOVED = 1,
			ID_DEVICES_ADDED = 2,
		};
};

}}

#endif

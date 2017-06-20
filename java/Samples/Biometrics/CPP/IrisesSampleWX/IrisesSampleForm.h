#ifndef IRISES_SAMPLE_FRM_H_INCLUDED
#define IRISES_SAMPLE_FRM_H_INCLUDED

#include "OptionsDlg.h"
#include "ExtractionSource.h"

namespace Neurotec { namespace Samples
{

#undef IrisesSampleForm_STYLE
#define IrisesSampleForm_STYLE wxCAPTION | wxRESIZE_BORDER | wxSYSTEM_MENU | wxMINIMIZE_BOX | wxMAXIMIZE_BOX | wxCLOSE_BOX

class IrisesSampleForm : public wxFrame
{
private:
	Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
	std::vector<Neurotec::NAsyncOperation> m_pendingOperations;
	ExtractionSource * m_currentSource;
	bool m_isCanceling;
	bool m_isOnDestroy;

private:
	wxListCtrl *listCtrlResults;
	wxRichTextCtrl *richTextCtrlLog;
	wxSplitterWindow *splitterWindowHor;
	Neurotec::Biometrics::Gui::wxNIrisView *irisView;
	wxSplitterWindow *splitterWindowVer;
	wxMenuBar *menuBar;
	wxButton *buttonIdentify;
	wxButton *buttonEnroll;
	wxCheckBox *chbCheckForDuplicates;
	wxComboBox *comboSource;
	wxComboBox *comboPosition;

public:
	IrisesSampleForm(wxWindow *parent, const wxWindowID id = 1, const wxString &title = wxEmptyString, const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = IrisesSampleForm_STYLE);
	virtual ~IrisesSampleForm();

private:
	void MnuCancelClick(wxCommandEvent &event);
	void MnuEnrollClick(wxCommandEvent &event);
	void MnuIdentifyClick(wxCommandEvent &event);
	void MnuExitClick(wxCommandEvent &event);
	void MnuClearLogClick(wxCommandEvent &event);
	void MnuClearDatabaseClick(wxCommandEvent &event);
	void MnuOptionsClick(wxCommandEvent &event);
	void MnuAboutClick(wxCommandEvent &event);
	void OnThread(wxCommandEvent &event);
	void OnSelectedSourceChanged(wxCommandEvent &event);
	void OnSelectedModeChanged(wxCommandEvent &event);
	void OnSourcesChanged(wxCommandEvent &event);
	void AppendText(const wxString & text, const wxColour &color = wxColour(0, 0, 0));
	void AppendTextError(const wxString & text);

private:
	enum
	{
		ID_STATUSBAR = 1000,

		ID_MNU_JOBS,
		ID_MNU_ENROLL,
		ID_MNU_IDENTIFY,
		ID_MNU_EXIT,
		ID_MNU_TOOLS,
		ID_MNU_CLEAR_LOG,
		ID_MNU_CLEAR_DB,
		ID_MNU_OPTIONS,
		ID_MNU_CANCEL_TASK,
		ID_MNU_HELP,
		ID_MNU_ABOUT,

		ID_TOOLBAR,
		ID_BUTTON_ENROLL,
		ID_BUTTON_IDENTIFY,

		ID_SPLITTERWINDOWVER,
		ID_SCROLLEDWINDOWIMAGE,
		ID_SPLITTERWINDOWHOR,
		ID_RICHTEXTCTRLLOG,
		ID_LISTCTRLRESULTS,

		ID_COMBO_SOURCE,
		ID_COMBO_POSITION,
	};
	enum
	{
		ID_WRITE,
		ID_WRITE_ERROR,
		ID_ADD_RESULTS,
		ID_SET_IRIS_TO_VIEW,
		ID_REMOVE_OPERATION,
		ID_CANCEL_OPERATIONS
	};
	enum
	{
		ID_DEVICE_PLUGGED,
		ID_DEVICE_UNPLUGGED,
	};

private:
	void OnClose(wxCloseEvent &event);
	void CreateGUIControls();
	void PerformTask(Neurotec::Biometrics::NBiometricOperations operation);
	void DisableControls();
	void EnableControls();
	void OnCancelCompleted();

	wxArrayString SelectFileList(int mode);
	wxArrayString SelectAllFilesFromDirectory();
	std::vector<Neurotec::Biometrics::NSubject> CreateSubjectsFromSource(bool requiresId = false);

	static void Write(IrisesSampleForm * sample, wxString& text);
	static void WriteError(IrisesSampleForm * sample, wxString& text);
	static void OnOperationCompleted(Neurotec::EventArgs args);
	static void OnDeviceAdded(Neurotec::Devices::NDevice device, void * param);
	static void OnDeviceRemoved(Neurotec::Devices::NDevice device, void * param);
	static void OnScannerCollectionChanged(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Devices::NDevice> args);

private:
	DECLARE_EVENT_TABLE();
};

}}

#endif

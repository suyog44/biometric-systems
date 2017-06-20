#ifndef LONG_ACTION_DIALOG_H_INCLUDED
#define LONG_ACTION_DIALOG_H_INCLUDED

namespace Neurotec { namespace Samples
{

class LongActionDialog : public wxDialog
{
public:
	typedef void (*ActionCallback)(void *param);

	typedef void (*CompletionCallback)(void *param, NError exception);

	enum
	{
		ID_PROGRESS_PULSER,
	};

	enum
	{
		ID_EVT_OPERATION_COMPLETED
	};

public:
	LongActionDialog(wxWindow *parent, wxWindowID id, const wxString &title);

	virtual ~LongActionDialog();

	void SetMessage(const wxString& message);

	void SetActionCallback(ActionCallback actionCallback, void *param);

	void OnThread(wxCommandEvent &event);

	void OnClose(wxCloseEvent& event);

private:
	void OnActivate(wxActivateEvent& event);

	void OnProgressPulse(wxTimerEvent &);

	void RegisterGuiEvents();

	void UnregisterGuiEvents();

	void CreateGUIControls();

	static void OnOperationCompleteCallback(void *param, NError exception);

private:
	class ActionWorker : public wxThread
	{
	public:
		ActionWorker(ActionCallback callback, void *param, wxThreadKind kind = wxTHREAD_DETACHED);

		virtual ~ActionWorker();

		void SetCompletionCallback(CompletionCallback callback, void *param);

	protected:
		virtual ExitCode Entry();

	private:
		virtual void OnExit();

	private:
		ActionCallback m_callback;
		CompletionCallback m_completeCallback;
		void *m_callbackParam;
		void *m_completeCallbackParam;
		NError m_exception;
	};

private:
	ActionCallback m_actionCallback;
	void *m_actionCallbackParam;
	bool m_isOperationRunning;
	wxGauge *m_progressGauge;
	wxTimer *m_progressPulser;
	wxStaticText *m_lblMessage;

private:
	DECLARE_EVENT_TABLE();
};

}}

#endif


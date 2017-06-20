#ifndef DATABASE_OPERATION_PAGE_H_INCLUDED
#define DATABASE_OPERATION_PAGE_H_INCLUDED

#include <Common/TabPage.h>
#include <Common/StatusPanel.h>

namespace Neurotec { namespace Samples
{

class DatabaseOperationPage : public TabPage
{
public:
	DatabaseOperationPage(
		wxWindow *parent,
		wxWindowID winid,
		::Neurotec::Biometrics::Client::NBiometricClient & biometricClient,
		::Neurotec::Biometrics::NSubject & subject,
		::Neurotec::Biometrics::NBiometricOperations operationType);

	virtual ~DatabaseOperationPage();

private:
	void OnSelectPage();

	void OnThread(wxCommandEvent &event);

	void OnProgressPulse(wxTimerEvent &event);

	void RegisterGuiEvents();

	void UnregisterGuiEvents();

	void CreateGUIControls();

	static void OperationCompletedCallback(::Neurotec::EventArgs args);

	static void OnLinkPressedCallback(::Neurotec::Biometrics::NMatchingResult result, void *param);

private:
	enum {
		ID_EVENT_OPERATION_COMPLETED
	};

	enum
	{
		ID_PROGRESS_PULSER
	};

	::Neurotec::Biometrics::NBiometricOperations m_operationType;
	::Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
	::Neurotec::Biometrics::NSubject m_subject;
	bool m_isOperationStarted;
	StatusPanel *m_statusPanel;
	wxGauge *m_progressGauge;
	wxTimer *m_progressPulser;
	wxBoxSizer *m_resultLayout;

private:
	DECLARE_EVENT_TABLE();
};

}}

#endif


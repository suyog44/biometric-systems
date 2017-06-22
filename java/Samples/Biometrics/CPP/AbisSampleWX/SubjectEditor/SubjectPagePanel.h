#ifndef SUBJECT_PAGE_PANEL_H_INCLUDED
#define SUBJECT_PAGE_PANEL_H_INCLUDED

#include <Common/SubjectTreeWidget.h>
#include <Common/TabPage.h>
#include <Common/TabController.h>

#include <SubjectEditor/SubjectPage.h>
#include <SubjectEditor/CaptureFingersPage.h>
#include <SubjectEditor/CaptureFacePage.h>
#include <SubjectEditor/CaptureIrisesPage.h>
#include <SubjectEditor/CapturePalmPage.h>
#include <SubjectEditor/CaptureVoicePage.h>
#include <SubjectEditor/PreviewBiometricPage.h>
#include <SubjectEditor/SubjectEditorPageInterface.h>

namespace Neurotec { namespace Samples
{

class SubjectPagePanel : public TabPage, public SubjectEditorPageInterface
{
public:
	SubjectPagePanel(Neurotec::Biometrics::Client::NBiometricClient& biometricClient, Neurotec::Biometrics::NSubject subject, TabController *parent,
		wxWindowID winid = wxID_ANY);

	virtual ~SubjectPagePanel();

	virtual void SelectFirstPage();

	void PerformOperation(::Neurotec::Biometrics::NBiometricOperations operation);

private:
	void OnThread(wxCommandEvent &event);

	void OnClose();

	void OnLeavePage();

	void OnSelectPage();

	void RegisterGuiEvents();

	void UnregisterGuiEvents();

	void CreateGUIControls();

	void OnSubjectTreeSelectedItemChanged(wxCommandEvent & event);

	void SelectPage(Node * selected);

private:
	enum
	{
		ID_EVENT_SELECT_FIRST_PAGE
	};

	wxPanel *m_leftPanel;
	wxPanel *m_rightPanel;
	wxSizer *m_rightPanelLayout;
	SubjectTreeWidget *m_treeCtrl;
	ModalityPage *m_currentModalityPage;
	SubjectPage *m_subjectPage;
	CaptureFingersPage *m_captureFingersPage;
	CaptureFacePage *m_captureFacePage;
	CaptureIrisesPage *m_captureIrisesPage;
	CapturePalmPage *m_capturePalmPage;
	CaptureVoicePage *m_captureVoicePage;
	PreviewBiometricPage *m_previewPage;

	Neurotec::Biometrics::Client::NBiometricClient& m_biometricClient;
	Neurotec::Biometrics::NSubject m_subject;
};

}}

#endif

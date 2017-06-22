#ifndef PREVIEW_BIOMETRIC_PAGE_H_INCLUDED
#define PREVIEW_BIOMETRIC_PAGE_H_INCLUDED

#include <SubjectEditor/ModalityPage.h>
#include <Common/SubjectTreeWidget.h>
#include <Common/GeneralizeProgressView.h>
#include <Common/IcaoWarningsView.h>

namespace Neurotec { namespace Samples
{

class PreviewBiometricPage : public ModalityPage
{
public:
	PreviewBiometricPage(::Neurotec::Biometrics::Client::NBiometricClient& biometricClient, ::Neurotec::Biometrics::NSubject& subject,
		SubjectEditorPageInterface& subjectEditorPageInterface, wxWindow *parent, wxWindowID winid = wxID_ANY);
	virtual ~PreviewBiometricPage();

	void SetSelection(Node * selection);

private:
	void OnFinishClick(wxCommandEvent &event);
	void OnSaveClick(wxCommandEvent &event);
	void OnShowProcessedImageClick(wxCommandEvent& event);
	void OnSelectedItemChanged(wxCommandEvent & event);

	void OnBiometricChanged(::Neurotec::Biometrics::NBiometric item);

	void RegisterGuiEvents();
	void UnregisterGuiEvents();
	void CreateGUIControls();

	virtual void OnNavigatedTo();
	virtual void OnNavigatingFrom();

private:
	bool m_canSave;
	bool m_showBinarizedCheckbox;

	::Neurotec::Biometrics::NBiometric m_biometric;
	Node * m_selection;
	wxWindow *m_view;
	IcaoWarningsView * m_icaoWarningsView;
	wxButton *m_btnFinish;
	wxButton *m_btnSave;
	wxBoxSizer *m_viewSizer;
	wxFlexGridSizer *m_centerSizer;
	wxBoxSizer *m_controlsSizer;
	wxCheckBox *m_chbShowBinarized;
	GeneralizeProgressView *m_generalizeView;
	::Neurotec::Gui::wxNViewZoomSlider * m_zoomSlider;
};

}}

#endif


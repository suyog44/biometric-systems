#ifndef MODALITY_PAGE_H_INCLUDED
#define MODALITY_PAGE_H_INCLUDED

#include <SubjectEditor/SubjectEditorPageInterface.h>

namespace Neurotec { namespace Samples
{

class ModalityPage : public wxPanel
{
public:
	ModalityPage(::Neurotec::Biometrics::Client::NBiometricClient& biometricClient, ::Neurotec::Biometrics::NSubject& subject,
			SubjectEditorPageInterface& subjectEditorPageInterface, wxWindow *parent, wxWindowID winid = wxID_ANY);

	virtual ~ModalityPage();

	void SelectFirstPage();

	virtual void OnNavigatedTo();

	virtual void OnNavigatingFrom();

	virtual bool IsPageShown();

	virtual void Reset();

	virtual void Cancel();

	virtual bool IsBusy();

	virtual void SetIsBusy(bool value);

protected:
	bool m_isPageShown;
	SubjectEditorPageInterface& m_subjectEditorPage;
	::Neurotec::Biometrics::Client::NBiometricClient& m_biometricClient;
	::Neurotec::Biometrics::NSubject& m_subject;
	Neurotec::Threading::NSyncEvent m_isIdle;

private:
	static void CancelCapturing(void *object);
};

}}

#endif


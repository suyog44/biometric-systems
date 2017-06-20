#include "Precompiled.h"

#include <Common/LongActionDialog.h>

#include <SubjectEditor/ModalityPage.h>

using namespace Neurotec::Threading;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;

namespace Neurotec { namespace Samples
{

ModalityPage::ModalityPage(NBiometricClient& biometricClient, NSubject& subject, SubjectEditorPageInterface& subjectEditorPageInterface,
	wxWindow *parent, wxWindowID winid) : wxPanel(parent, winid),
	m_isPageShown(false), m_subjectEditorPage(subjectEditorPageInterface), m_biometricClient(biometricClient), m_subject(subject), m_isIdle(NSyncEvent(true, true))
{
}

ModalityPage::~ModalityPage()
{
}

void ModalityPage::SelectFirstPage()
{
	m_subjectEditorPage.SelectFirstPage();
}

bool ModalityPage::IsBusy()
{
	return !m_isIdle.WaitFor(0);
}

void ModalityPage::SetIsBusy(bool value)
{
	if (value)
		m_isIdle.Reset();
	else
		m_isIdle.Set();
}

bool ModalityPage::IsPageShown()
{
	return m_isPageShown;
}

void ModalityPage::OnNavigatedTo()
{
	m_isPageShown = true;
}

void ModalityPage::OnNavigatingFrom()
{
	m_isPageShown = false;
}

void ModalityPage::Cancel()
{
	if (IsBusy())
	{
		LongActionDialog longActionDialog(this, wxID_ANY, wxT("Working..."));
		longActionDialog.SetMessage(wxT("Finishing current action"));
		longActionDialog.SetActionCallback(&ModalityPage::CancelCapturing, this);
		longActionDialog.ShowModal();
	}
}

void ModalityPage::Reset()
{
}

void ModalityPage::CancelCapturing(void *object)
{
	ModalityPage *page = reinterpret_cast<ModalityPage *>(object);
	if (page)
	{
		page->m_biometricClient.Cancel();
		page->m_isIdle.WaitFor();
	}
}

}}

#include "Precompiled.h"

#include <Settings/BaseSettingsPage.h>

using namespace ::Neurotec::Biometrics;
using namespace ::Neurotec::Biometrics::Client;

namespace Neurotec { namespace Samples
{

BaseSettingsPage::BaseSettingsPage(wxWindow *parent, wxWindowID winid)
	: wxPanel(parent, winid), m_biometricClient(NULL)
{
}

BaseSettingsPage::~BaseSettingsPage()
{
}

void BaseSettingsPage::Initialize(::Neurotec::Biometrics::Client::NBiometricClient biometricClient)
{
	m_biometricClient = biometricClient;
}

}}


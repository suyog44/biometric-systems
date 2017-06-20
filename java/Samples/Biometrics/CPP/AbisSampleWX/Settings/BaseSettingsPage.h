#ifndef BASE_SETTINGS_PAGE_H_INCLUDED
#define BASE_SETTINGS_PAGE_H_INCLUDED

namespace Neurotec { namespace Samples
{

class BaseSettingsPage : public wxPanel
{
public:
	BaseSettingsPage(wxWindow *parent, wxWindowID winid = wxID_ANY);

	virtual ~BaseSettingsPage();

	virtual void Initialize(::Neurotec::Biometrics::Client::NBiometricClient biometricClient);

	virtual void Load() = 0;

	virtual void Reset() = 0;

protected:
	::Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;
};

}}

#endif


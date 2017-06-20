#include "Precompiled.h"
#include "FingersSample.h"
#include "FingersSampleForm.h"

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NGui/NGuiLibrary.cpp>
	#include <NBiometricGui/NBiometricGuiLibrary.cpp>
	#include <NBiometricClient/NBiometricClientLibrary.cpp>
	#include <NLicensing/NLicensingLibrary.cpp>
#else
	#include <NGuiLibrary.cpp>
	#include <NBiometricGuiLibrary.cpp>
	#include <NBiometricClientLibrary.cpp>
	#include <NLicensingLibrary.cpp>
#endif

using namespace Neurotec::Gui;

IMPLEMENT_APP(Neurotec::Samples::FingersSampleApp)

namespace Neurotec { namespace Samples
{

bool FingersSampleApp::OnInit()
{
	bool successful = false;
	try
	{
		NCore::OnStart();
	#if defined(N_PRODUCT_LIB)
		using namespace ::Neurotec::Devices;
		NResult res = N_OK;
		SET_DEVICE_PLUGINS_PATH(res);
		ADD_F_SCANNER_PLUGINS(res);
		ADD_MULTI_MODAL_DEVICE_PLUGINS(res);
		//ADD_SAMPLE_PLUGIN(res);
		NCheck(res);
	#endif

		wxSampleConfig::Init();
		FingersSampleForm *frame = new FingersSampleForm(NULL);
		SetTopWindow(frame);
		frame->Show();
		frame->Refresh();
		frame->Update();
		successful = true;
	}
	catch (NError& ex)
	{
		wxExceptionDlg::Show(ex);
	}
	catch (std::exception& ex)
	{
		wxExceptionDlg::Show(wxString(ex.what(), wxConvLibc));
	}

	if (!successful) NCore::OnExit(false);
	return successful;
}

int FingersSampleApp::OnExit()
{
	try
	{
		wxSampleConfig::Save();
	}
	catch (NError& ex)
	{
		wxExceptionDlg::Show(ex);
	}
	catch (std::exception& ex)
	{
		wxExceptionDlg::Show(wxString(ex.what(), wxConvLibc));
	}

	NCore::OnExit(false);
	return wxApp::OnExit();
}

}}
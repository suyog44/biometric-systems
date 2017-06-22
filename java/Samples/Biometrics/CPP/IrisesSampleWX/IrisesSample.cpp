#include "Precompiled.h"
#include "IrisesSample.h"
#include "IrisesSampleForm.h"

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

IMPLEMENT_APP(Neurotec::Samples::IrisesSampleApp)

namespace Neurotec { namespace Samples
{

bool IrisesSampleApp::OnInit()
{
	bool successful = false;
	try
	{
		NCore::OnStart();
	#if defined(N_PRODUCT_LIB)
		using namespace ::Neurotec::Devices;
		NResult res = N_OK;
		SET_DEVICE_PLUGINS_PATH(res);
		ADD_IRIS_SCANNER_PLUGINS(res);
		ADD_MULTI_MODAL_DEVICE_PLUGINS(res);
		//ADD_SAMPLE_PLUGIN(res);
		NCheck(res);
	#endif

		wxSampleConfig::Init();
		IrisesSampleForm *frame = new IrisesSampleForm(NULL);
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

int IrisesSampleApp::OnExit()
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

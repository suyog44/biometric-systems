#include "Precompiled.h"
#include "FacesSample.h"
#include "FacesSampleForm.h"

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NCore/NCoreLibrary.cpp>
	#include <NGui/NGuiLibrary.cpp>
	#include <NBiometricGui/NBiometricGuiLibrary.cpp>
	#include <NDevices/NDevicesLibrary.cpp>
	#include <NBiometrics/NBiometricsLibrary.cpp>
	#include <NBiometricClient/NBiometricClientLibrary.cpp>
	#include <NMedia/NMediaLibrary.cpp>
	#include <NLicensing/NLicensingLibrary.cpp>
#else
	#include <NCoreLibrary.cpp>
	#include <NGuiLibrary.cpp>
	#include <NBiometricGuiLibrary.cpp>
	#include <NDevicesLibrary.cpp>
	#include <NBiometricsLibrary.cpp>
	#include <NBiometricClientLibrary.cpp>
	#include <NMediaLibrary.cpp>
	#include <NLicensingLibrary.cpp>
#endif

using namespace Neurotec::Gui;

IMPLEMENT_APP(Neurotec::Samples::FacesSampleApp)

namespace Neurotec { namespace Samples
{

bool FacesSampleApp::OnInit()
{
	bool successful = false;
	try
	{
		NCore::OnStart();
	#if defined(N_PRODUCT_LIB)
		using namespace ::Neurotec::Devices;
		NResult res = N_OK;
		SET_DEVICE_PLUGINS_PATH(res);
			ADD_MEDIA_PLUGIN(res);
			ADD_CAMERA_PLUGINS(res);
			ADD_MULTI_MODAL_DEVICE_PLUGINS(res);
		//ADD_SAMPLE_PLUGIN(res);
		NCheck(res);
	#endif

		wxSampleConfig::Init();
		FacesSampleForm *frame = new FacesSampleForm(NULL);
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

int FacesSampleApp::OnExit()
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

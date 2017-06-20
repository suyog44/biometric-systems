#include "Precompiled.h"
#include "TemplateSample.h"
#include "TemplateSampleForm.h"

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

using namespace Neurotec;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;

IMPLEMENT_APP(Neurotec::Samples::TemplateSampleApp);

namespace Neurotec { namespace Samples
{

	bool TemplateSampleApp::OnInit()
	{
		bool successful = false;
		try
		{
			NCore::OnStart();
#if defined(N_PRODUCT_LIB)
			NResult res = N_OK;
			NCheck(res);
#endif
			TemplateSampleForm * frame = new TemplateSampleForm(NULL);
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

	int TemplateSampleApp::OnExit()
	{
		NCore::OnExit(false);
		return wxApp::OnExit();
	}
}}

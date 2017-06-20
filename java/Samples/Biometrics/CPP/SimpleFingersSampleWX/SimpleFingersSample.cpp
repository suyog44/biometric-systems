#include "Precompiled.h"
#include "SimpleFingersSample.h"
#include"SimpleFingersSampleForm.h"

#ifdef N_MAC_OSX_FRAMEWORKS
#include <NCore/NCoreLibrary.cpp>
#include <NGui/NGuiLibrary.cpp>
#include <NBiometricGui/NBiometricGuiLibrary.cpp>
#include <NBiometricClient/NBiometricClientLibrary.cpp>
#include <NLicensing/NLicensingLibrary.cpp>
#else
#include <NCoreLibrary.cpp>
#include <NGuiLibrary.cpp>
#include <NBiometricGuiLibrary.cpp>
#include <NBiometricClientLibrary.cpp>
#include <NLicensingLibrary.cpp>
#endif

using namespace Neurotec::Licensing;
using namespace Neurotec::Gui;

IMPLEMENT_APP(Neurotec::Samples::SimpleFingerSampleApp)

namespace Neurotec
{
	namespace Samples
	{

#define LICENSE_COMPONENTS {\
	"Biometrics.FingerExtraction", \
	"Biometrics.FingerMatching", \
	"Devices.FingerScanners", \
	"Images.WSQ", \
	"Biometrics.FingerSegmentation", \
	"Biometrics.FingerQualityAssessmentBase" \
	}
#define LICENSE_SERVER "/local"
#define LICENSE_PORT "5000"

		bool SimpleFingerSampleApp::OnInit()
		{
			bool successfull = false;
			const wxString Components[] = LICENSE_COMPONENTS;
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
				for (unsigned int i = 0; i < WXSIZEOF(Components); i++)
				{
					NLicense::ObtainComponents(LICENSE_SERVER, LICENSE_PORT, Components[i]);
				}
				SimpleFingersSampleForm *notebook = new SimpleFingersSampleForm(NULL);
				SetTopWindow(notebook);
				notebook->Show();
				notebook->Refresh();
				notebook->Update();
				successfull = true;
			}
			catch (NError& ex)
			{
				wxExceptionDlg::Show(ex);
				for (unsigned int i = 0; i < WXSIZEOF(Components); i++)
				{
					NLicense::ReleaseComponents(Components[i]);
				}
			}
			catch (std::exception& ex)
			{
				wxExceptionDlg::Show(wxString(ex.what(), wxConvLibc));
			}
			if (!successfull)
			{
				NCore::OnExit(false);
			}
			return successfull;
		}

		int SimpleFingerSampleApp::OnExit()
		{
			try
			{
				const wxString Components[] = LICENSE_COMPONENTS;
				for (unsigned int i = 0; i < WXSIZEOF(Components); i++)
				{
					NLicense::ReleaseComponents(Components[i]);
				}
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
	}
}

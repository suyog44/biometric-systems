#include "Precompiled.h"
#include "SimpleIrisesSample.h"
#include "SimpleIrisesSampleForm.h"

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

IMPLEMENT_APP(Neurotec::Samples::SimpleIrisesSampleApp)

namespace Neurotec
{
	namespace Samples
	{

#define LICENSE_COMPONENTS {\
	"Biometrics.IrisExtraction",\
	"Biometrics.IrisSegmentation",\
	"Biometrics.IrisMatching",\
	"Devices.IrisScanners"\
}
#define LICENSE_SERVER "/local"
#define LICENSE_PORT "5000"

		bool SimpleIrisesSampleApp::OnInit()
		{
			bool successfull = false;
			const wxString Components[] = LICENSE_COMPONENTS;
			try
			{
				NCore::OnStart();
				for (unsigned int i = 0; i < WXSIZEOF(Components); i++)
				{
					NLicense::ObtainComponents(LICENSE_SERVER, LICENSE_PORT, Components[i]);
				}
				SimpleIrisesSampleForm *frame = new SimpleIrisesSampleForm(NULL);
				SetTopWindow(frame);
				frame->Show();
				successfull = true;
			}
			catch (NError & ex)
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

		int SimpleIrisesSampleApp::OnExit()
		{
			NCore::OnExit(false);
			return wxApp::OnExit();
		}
	}
}

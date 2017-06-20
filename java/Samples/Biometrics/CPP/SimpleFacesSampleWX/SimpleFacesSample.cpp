#include "Precompiled.h"
#include "SimpleFacesSample.h"
#include "SimpleFacesSampleForm.h"

#ifdef N_MAC_OSX_FRAMEWORKS
#include <NCore/NCoreLibrary.cpp>
#include <NGui/NGuiLibrary.cpp>
#include <NBiometricGui/NBiometricGuiLibrary.cpp>
#include <NDevices/NDevicesLibrary.cpp>
#include <NBiometrics/NBiometricsLibrary.cpp>
#include <NBiometricClient/NBiometricClientLibrary.cpp>
#include <NLicensing/NLicensingLibrary.cpp>
#else
#include <NCoreLibrary.cpp>
#include <NGuiLibrary.cpp>
#include <NBiometricGuiLibrary.cpp>
#include <NDevicesLibrary.cpp>
#include <NBiometricsLibrary.cpp>
#include <NBiometricClientLibrary.cpp>
#include <NLicensingLibrary.cpp>
#endif

using namespace Neurotec::Gui;
using namespace Neurotec::Licensing;

IMPLEMENT_APP(Neurotec::Samples::SimpleFacesSampleApp)

namespace Neurotec { namespace Samples {
#define LICENSE_COMPONENTS {\
	"Biometrics.FaceDetection", \
	"Biometrics.FaceSegmentsDetection", \
	"Devices.Cameras", \
	"Biometrics.FaceExtraction", \
	"Biometrics.FaceSegmentation", \
	"Biometrics.FaceMatching", \
	"Biometrics.FaceQualityAssessment" \
}
#define LICENSE_SERVER	"/local"
#define LICENSE_PORT	"5000"

		bool SimpleFacesSampleApp::OnInit()
		{
			const wxString Components[] = LICENSE_COMPONENTS;
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
				NCheck(res);
#endif
				for (unsigned int i = 0; i < WXSIZEOF(Components); i++)
				{
					NLicense::ObtainComponents(LICENSE_SERVER, LICENSE_PORT, Components[i]);
				}
				SimpleFacesSampleForm *frame = new SimpleFacesSampleForm(NULL);
				SetTopWindow(frame);
				frame->Show();
				frame->Refresh();
				frame->Update();
				successful = true;
			}
			catch (NError& e)
			{
				wxExceptionDlg::Show(e);
				for (unsigned int i = 0; i < WXSIZEOF(Components); i++)
				{
					NLicense::ReleaseComponents(Components[i]);
				}
			}
			catch (std::exception& ex)
			{
				wxExceptionDlg::Show(wxString(ex.what(), wxConvLibc));
			}

			if (!successful) NCore::OnExit(false);
			return successful;
		}

		int SimpleFacesSampleApp::OnExit()
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

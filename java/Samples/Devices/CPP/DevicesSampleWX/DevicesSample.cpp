#include "Precompiled.h"
#include <DevicesSample.h>
#include <DevicesSampleForm.h>

using namespace Neurotec::Gui;
using namespace Neurotec::Licensing;

IMPLEMENT_APP(Neurotec::Samples::DevicesSample);

namespace Neurotec
{
	namespace Samples
	{
		const wxString Licenses[] =
		{
			"Biometrics.FingerDetection",
			"Biometrics.IrisDetection",
			"Devices.FingerScanners",
			"Devices.Cameras",
			"Devices.IrisScanners",
			"Devices.Microphones"
		};
		bool DevicesSample::OnInit()
		{
			bool successful = false;
			try
			{
				NCore::OnStart();
#if defined(N_PRODUCT_LIB)
				NResult res = N_OK;
				SET_DEVICE_PLUGINS_PATH(res);
				ADD_MEDIA_PLUGIN(res);
				ADD_CAMERA_PLUGINS(res);
				ADD_MULTI_MODAL_DEVICE_PLUGINS(res);
				NCheck(res);
#endif

				bool anyObtained = false;
				for (unsigned int i = 0; i < WXSIZEOF(Licenses); i++)
				{
					if (NLicense::ObtainComponents("/local", "5000", Licenses[i])) anyObtained = true;
				}
				if (!anyObtained)
				{
					wxString msg = wxString::Format("Could not obtain licenses for any of components : %s", Licenses[0]);
					wxLogMessage(msg);
				}
				else
				{
					DevicesSampleForm *frame = new DevicesSampleForm(NULL);
					SetTopWindow(frame);
					frame->Show();
					frame->Refresh();
					frame->Update();
					successful = true;
				}
			}
			catch (NError &ex)
			{
				wxExceptionDlg::Show(ex);
			}
			catch (std::exception &ex)
			{
				wxExceptionDlg::Show(wxString(ex.what(), wxConvLibc));
			}
			if (!successful)
			{
				NCore::OnExit(false);
			}
			return successful;
		}

		int DevicesSample::OnExit()
		{
			for (unsigned int i = 0; i < WXSIZEOF(Licenses); i++)
			{
				NLicense::ReleaseComponents(Licenses[i]);
			}
			NCore::OnExit(false);
			return wxApp::OnExit();
		}
	}
}

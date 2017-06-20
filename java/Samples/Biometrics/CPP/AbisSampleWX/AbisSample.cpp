#include "Precompiled.h"
#include <AbisSample.h>
#include <AbisSampleForm.h>

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NGui/NGuiLibrary.cpp>
	#include <NBiometricClient/NBiometricClientLibrary.cpp>
	#include <NBiometrics/NBiometricsLibrary.cpp>
	#include <NBiometricGui/NBiometricGuiLibrary.cpp>
#else
	#include <NGuiLibrary.cpp>
	#include <NBiometricGuiLibrary.cpp>
	#include <NBiometricsLibrary.cpp>
	#include <NBiometricClientLibrary.cpp>
#endif

using namespace Neurotec::Gui;

IMPLEMENT_APP(Neurotec::Samples::AbisSampleApp)

namespace Neurotec { namespace Samples
{
	bool AbisSampleApp::OnInit()
	{
		bool successful = false;
		try
		{
			NCore::OnStart();

			wxSampleConfig::Init();

			AbisSampleForm *abisSampleForm = new AbisSampleForm(NULL);
			SetTopWindow(abisSampleForm);
			abisSampleForm->Show();
			abisSampleForm->Refresh();
			abisSampleForm->Update();
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

	int AbisSampleApp::OnExit()
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

#include <wx/config.h>
#include <wx/dialog.h>
#include <wx/dir.h>
#include <wx/ffile.h>
#include <wx/file.h>
#include <wx/fileconf.h>
#include <wx/filename.h>
#include <wx/frame.h>
#include <wx/listctrl.h>
#include <wx/notebook.h>
#include <wx/richtext/richtextctrl.h>
#include <wx/splitter.h>
#include <wx/thread.h>
#include <wx/tokenzr.h>
#include <wx/wx.h>

#include <exception>

#include <Utils.hpp>

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NGui/NGui.hpp>
	#include <NBiometricGui/NBiometricGui.hpp>
	#include <NLicensing/NLicensing.hpp>
	#include <NBiometricClient/NBiometricClient.hpp>
#else
	#include <NLicensing.hpp>
	#include <NBiometricClient.hpp>
	#include <NGui.hpp>
	#include <NBiometricGui.hpp>
#endif

#include <wxSampleConfig.h>
#include <wxSampleException.h>
#include <SampleCommon.h>

#include <wx/bitmap.h>
#include <wx/button.h>
#include <wx/colour.h>
#include <wx/confbase.h>
#include <wx/dcbuffer.h>
#include <wx/dialog.h>
#include <wx/dir.h>
#include <wx/ffile.h>
#include <wx/file.h>
#include <wx/fileconf.h>
#include <wx/filename.h>
#include <wx/font.h>
#include <wx/frame.h>
#include <wx/gdicmn.h>
#include <wx/icon.h>
#include <wx/image.h>
#include <wx/listctrl.h>
#include <wx/mstream.h>
#include <wx/notebook.h>
#include <wx/richtext/richtextctrl.h>
#include <wx/settings.h>
#include <wx/sizer.h>
#include <wx/spinctrl.h>
#include <wx/splitter.h>
#include <wx/statbmp.h>
#include <wx/stattext.h>
#include <wx/string.h>
#include <wx/textctrl.h>
#include <wx/tokenzr.h>
#include <wx/valgen.h>
#include <wx/wx.h>

#include <algorithm>
#include <exception>
#include <memory>
#include <vector>
#include <queue>

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NCore/NCore.hpp>
	#include <NMedia/NMedia.hpp>
	#include <NDevices/NDevices.hpp>
	#include <NBiometrics/NBiometrics.hpp>
	#include <NBiometricClient/NBiometricClient.hpp>
	#include <NMediaProc/NMediaProc.hpp>
	#include <NLicensing/NLicensing.hpp>
#else
	#include <NCore.hpp>
	#include <NMedia.hpp>
	#include <NDevices.hpp>
	#include <NBiometrics.hpp>
	#include <NBiometricClient.hpp>
	#include <NMediaProc.hpp>
	#include <NLicensing.hpp>
#endif

#include <Utils.hpp>

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NGui/NGui.hpp>
	#include <NBiometricGui/NBiometricGui.hpp>
#else
	#include <NGui.hpp>
	#include <NBiometricGui.hpp>
#endif

#include <wxSampleConfig.h>
#include <wxSampleException.h>
#include <SampleCommon.h>

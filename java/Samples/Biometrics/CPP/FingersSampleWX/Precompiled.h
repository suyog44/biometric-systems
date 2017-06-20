#include <wx/bitmap.h>
#include <wx/busyinfo.h>
#include <wx/button.h>
#include <wx/radiobox.h>
#include <wx/colour.h>
#include <wx/confbase.h>
#include <wx/config.h>
#include <wx/dcbuffer.h>
#include <wx/dialog.h>
#include <wx/dir.h>
#include <wx/ffile.h>
#include <wx/fileconf.h>
#include <wx/font.h>
#include <wx/frame.h>
#include <wx/gdicmn.h>
#include <wx/icon.h>
#include <wx/image.h>
#include <wx/listctrl.h>
#include <wx/mstream.h>
#include <wx/notebook.h>
#include <wx/progdlg.h>
#include <wx/richtext/richtextctrl.h>
#include <wx/settings.h>
#include <wx/sizer.h>
#include <wx/spinctrl.h>
#include <wx/splitter.h>
#include <wx/statbmp.h>
#include <wx/stattext.h>
#include <wx/string.h>
#include <wx/textctrl.h>
#include <wx/thread.h>
#include <wx/tokenzr.h>
#include <wx/valgen.h>
#include <wx/wx.h>

#include <exception>
#include <vector>
#include <queue>

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

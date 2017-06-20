#include <wx/wx.h>
#include <wx/config.h>
#include <wx/frame.h>
#include <wx/gbsizer.h>
#include <wx/menu.h>
#include <wx/aui/auibook.h>
#include <wx/splitter.h>
#include <wx/sizer.h>
#include <wx/statline.h>
#include <wx/treectrl.h>
#include <wx/spinctrl.h>
#include <wx/statbmp.h>
#include <wx/xml/xml.h>
#include <wx/sstream.h>
#include <wx/tooltip.h>
#include <wx/hyperlink.h>
#include <wx/filename.h>
#include <wx/bmpbuttn.h>
#include <wx/dataview.h>
#include <wx/propgrid/propgrid.h>
#include <wx/propgrid/advprops.h>

#include <vector>
#include <algorithm>

#include <Utils.hpp>

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NBiometricClient/NBiometricClient.hpp>
	#include <NBiometricGui/NBiometricGui.hpp>
	#include <NBiometrics/NBiometrics.hpp>
	#include <NGui/NGui.hpp>
	#include <NLicensing/NLicensing.hpp>
#else
	#include <NCore.hpp>
	#include <NMedia.hpp>
	#include <NDevices.hpp>
	#include <NBiometrics.hpp>
	#include <NBiometricClient.hpp>
	#include <NMediaProc.hpp>
	#include <NLicensing.hpp>
	#include <NGui.hpp>
	#include <NBiometricGui.hpp>
#endif

#include <wxSampleConfig.h>
#include <wxSampleException.h>
#include <SampleCommon.h>

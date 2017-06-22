#include <wx/button.h>
#include <wx/colour.h>
#include <wx/filename.h>
#include <wx/frame.h>
#include <wx/listctrl.h>
#include <wx/notebook.h>
#include <wx/sizer.h>
#include <wx/spinctrl.h>
#include <wx/stattext.h>
#include <wx/string.h>
#include <wx/wx.h>
#include <wx/valnum.h>

#include <algorithm>
#include <cmath>
#include <Utils.hpp>

#ifdef N_MAC_OSX_FRAMEWORKS
#include <NCore/NCore.hpp>
#include <NDevices/NDevices.hpp>
#include <NBiometrics/NBiometrics.hpp>
#include <NBiometricClient/NBiometricClient.hpp>
#include <NLicensing/NLicensing.hpp>

#include <NGui/NGui.hpp>
#include <NBiometricGui/NBiometricGui.hpp>
#else
#include <NCore.hpp>
#include <NDevices.hpp>
#include <NBiometrics.hpp>
#include <NBiometricClient.hpp>
#include <NLicensing.hpp>
#include <NGui.hpp>
#include <NBiometricGui.hpp>
#endif

#include <SampleCommon.h>

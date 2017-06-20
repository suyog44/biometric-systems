#include <wx/button.h>
#include <wx/filename.h>
#include <wx/font.h>
#include <wx/frame.h>
#include <wx/icon.h>
#include <wx/listctrl.h>
#include <wx/richtext/richtextctrl.h>
#include <wx/sizer.h>
#include <wx/stattext.h>
#include <wx/string.h>
#include <wx/textctrl.h>
#include <wx/list.h>
#include <wx/wx.h>
#include <wx/notebook.h>
#include <wx/valnum.h>
#include <wx/numformatter.h>

#include <Utils.hpp>

#ifdef N_MAC_OSX_FRAMEWORKS
#include <NCore/NCore.hpp>
#include <NLicensing/NLicensing.hpp>
#include <NBiometricClient/NBiometricClient.hpp>
#include <NBiometrics/NBiometrics.hpp>
#include <NGui/NGui.hpp>
#include <NBiometricGui/NBiometricGui.hpp>
#else
#include <NCore.hpp>
#include <NLicensing.hpp>
#include <NBiometricClient.hpp>
#include <NBiometrics.hpp>
#include <NGui.hpp>
#include <NBiometricGui.hpp>
#endif

#include <SampleCommon.h>

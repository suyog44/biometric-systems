#include <wx/wx.h>
#include <wx/propgrid/manager.h>
#include <wx/generic/statbmpg.h>
#include <wx/treectrl.h>
#include <wx/treelist.h>
#include <wx/splitter.h>
#include <wx/xml/xml.h>
#include <wx/config.h>

#include <algorithm>
#include <exception>
#include <memory>
#include <vector>
#include <queue>
#include <list>

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
#else
#include <NGui.hpp>
#endif

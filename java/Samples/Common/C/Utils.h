#ifndef SAMPLE_UTILS_H_INCLUDED
#define SAMPLE_UTILS_H_INCLUDED

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NCore/NCore.h>
#else
	#include <Core/NDefs.h>
#endif

// system headers
#ifndef N_WINDOWS
	#ifndef _GNU_SOURCE
		#define _GNU_SOURCE
	#endif
#endif

#ifndef N_MAC_OSX_FRAMEWORKS
	#include <Core/NTypes.h>
#endif

#if defined(N_PRODUCT_LIB)
	#ifdef N_MAC_OSX_FRAMEWORKS
		#include <NDevices/NDevices.h>
	#else
		#include <NDevices.h>
	#endif
#endif

#ifdef N_CPP
extern "C"
{
#endif

#if defined(N_PRODUCT_LIB)

#if defined(N_PRODUCT_FINGERS_ONLY)
	#define N_PRODUCT_HAS_FINGERS
	#define N_PRODUCT_HAS_FINGERS_OR_PALMS
	#define N_PRODUCT_HAS_F_SCANNERS
#elif defined(N_PRODUCT_FACES_ONLY)
	#define N_PRODUCT_HAS_FACES
	#define N_PRODUCT_HAS_CAMERAS
#elif defined(N_PRODUCT_IRISES_ONLY)
	#define N_PRODUCT_HAS_IRISES
	#define N_PRODUCT_HAS_IRIS_SCANNERS
#elif defined(N_PRODUCT_PALMS_ONLY)
	#define N_PRODUCT_HAS_PALMS
	#define N_PRODUCT_HAS_FINGERS_OR_PALMS
	#define N_PRODUCT_HAS_F_SCANNERS
#elif defined(N_PRODUCT_VOICES_ONLY)
	#define N_PRODUCT_HAS_VOICES
	#define N_PRODUCT_HAS_MICROPHONES
#else
	#define N_PRODUCT_HAS_FINGERS
	#define N_PRODUCT_HAS_FACES
	#define N_PRODUCT_HAS_IRISES
	#define N_PRODUCT_HAS_PALMS
	#define N_PRODUCT_HAS_VOICES
	#define N_PRODUCT_HAS_FINGERS_OR_PALMS
	#define N_PRODUCT_HAS_MULTI_MODAL
	#define N_PRODUCT_HAS_F_SCANNERS
	#define N_PRODUCT_HAS_IRIS_SCANNERS
	#define N_PRODUCT_HAS_CAMERAS
	#define N_PRODUCT_HAS_MICROPHONES
	#define N_PRODUCT_HAS_MULTI_MODAL_DEVICES
#endif

#if defined(N_MAC)

#include <CoreFoundation/CoreFoundation.h>
#include <ApplicationServices/ApplicationServices.h>
#include <libgen.h>

#define SET_DEVICE_PLUGINS_PATH(res)\
	{\
		OSStatus err;\
		ProcessSerialNumber PSN;\
		if (NSucceeded(res))\
		{\
			err = GetCurrentProcess(&PSN);\
			if (err != 0) res = N_E_FAILED;\
			if (NSucceeded(res))\
			{\
				FSRef lref;\
				err = GetProcessBundleLocation(&PSN, &lref);\
				if (err != 0) res = N_E_FAILED;\
				if (NSucceeded(res))\
				{\
					NAChar exePath[4096];\
					err = FSRefMakePath(&lref, (UInt8 *)exePath, 4096);\
					if (err != 0) res = N_E_FAILED;\
					if (NSucceeded(res))\
					{\
						HNPluginManager hPluginManager = NULL;\
						res = NDeviceManagerGetPluginManager(&hPluginManager);\
						if (NSucceeded(res))\
						{\
							res = NPluginManagerSetPluginSearchPathA(hPluginManager, dirname(exePath));\
							res = NObjectUnref(hPluginManager);\
						}\
					}\
				}\
			}\
		}\
	}

#else

#define SET_DEVICE_PLUGINS_PATH(res)

#endif

N_DECLARE_PLUGIN_MODULE(NdmMedia)

N_DECLARE_PLUGIN_MODULE(NdmCisco)
N_DECLARE_PLUGIN_MODULE(NdmMobotix)
N_DECLARE_PLUGIN_MODULE(NdmProsillica)

N_DECLARE_PLUGIN_MODULE(NdmAbilma)
N_DECLARE_PLUGIN_MODULE(NdmArhFps)
N_DECLARE_PLUGIN_MODULE(NdmBioca)
N_DECLARE_PLUGIN_MODULE(NdmBiometrika)
N_DECLARE_PLUGIN_MODULE(NdmBioTech)
N_DECLARE_PLUGIN_MODULE(NdmBlueFin)
N_DECLARE_PLUGIN_MODULE(NdmCogent)
N_DECLARE_PLUGIN_MODULE(NdmCrossMatch)
N_DECLARE_PLUGIN_MODULE(NdmCrossMatchLScan)
N_DECLARE_PLUGIN_MODULE(NdmDermalog)
N_DECLARE_PLUGIN_MODULE(NdmDigitalPersonaUareU)
N_DECLARE_PLUGIN_MODULE(NdmDigitalPersonaUareUOneTouch)
N_DECLARE_PLUGIN_MODULE(NdmFutronic)
N_DECLARE_PLUGIN_MODULE(NdmFutronicEthernetFam)
N_DECLARE_PLUGIN_MODULE(NdmGreenBit)
N_DECLARE_PLUGIN_MODULE(NdmHongda)
N_DECLARE_PLUGIN_MODULE(NdmId3Certis)
N_DECLARE_PLUGIN_MODULE(NdmIdentix)
N_DECLARE_PLUGIN_MODULE(NdmIntec)
N_DECLARE_PLUGIN_MODULE(NdmIntegratedBiometricsIBScanUltimate)
N_DECLARE_PLUGIN_MODULE(NdmKoehlke)
N_DECLARE_PLUGIN_MODULE(NdmLumidigm)
N_DECLARE_PLUGIN_MODULE(NdmMiaxis)
N_DECLARE_PLUGIN_MODULE(NdmNextBiometrics)
N_DECLARE_PLUGIN_MODULE(NdmNitgen)
N_DECLARE_PLUGIN_MODULE(NdmNitgenNBioScan)
N_DECLARE_PLUGIN_MODULE(NdmSecuGen)
N_DECLARE_PLUGIN_MODULE(NdmStartek)
N_DECLARE_PLUGIN_MODULE(NdmSupremaBioMini)
N_DECLARE_PLUGIN_MODULE(NdmSupremaRealScan)
N_DECLARE_PLUGIN_MODULE(NdmTenBio)
N_DECLARE_PLUGIN_MODULE(NdmTSTBiometrics)
N_DECLARE_PLUGIN_MODULE(NdmUnionCommunity)
N_DECLARE_PLUGIN_MODULE(NdmUpek)
N_DECLARE_PLUGIN_MODULE(NdmZKSGroup)
N_DECLARE_PLUGIN_MODULE(NdmZKSoftware)

N_DECLARE_PLUGIN_MODULE(NdmCmiTechIris)
N_DECLARE_PLUGIN_MODULE(NdmCrossMatchIScan)
N_DECLARE_PLUGIN_MODULE(NdmIriTechIriShield)

N_DECLARE_PLUGIN_MODULE(NdmIrisGuard)
N_DECLARE_PLUGIN_MODULE(NdmVistaImaging)

N_DECLARE_PLUGIN_MODULE(NdmSample)

#define ADD_PLUGIN(res, moduleName, szPath) \
	{\
		HNPluginManager hPluginManager = NULL;\
		HNPluginModule hModule = NULL;\
		HNPlugin hPlugin = NULL;\
		if (NSucceeded(res)) res = NDeviceManagerGetPluginManager(&hPluginManager);\
		if (NSucceeded(res)) res = N_MODULE_OF(moduleName)(&hModule);\
		if (NSucceeded(res))\
		{\
			res = NPluginManagerAddPlugin(hPluginManager, hModule, szPath, &hPlugin);\
			if (NSucceeded(res)) res = NObjectUnref(hPlugin);\
			res = NObjectUnref(hModule);\
		}\
		if (hPluginManager) res = NObjectUnref(hPluginManager);\
	}\

#define ADD_MEDIA_PLUGIN(res) \
	{\
		ADD_PLUGIN(res, NdmMedia, NULL);\
	}

#define ADD_SAMPLE_PLUGIN(res) \
	{\
		ADD_PLUGIN(res, NdmSample, NULL);\
	}

#if defined(N_WINDOWS) && !defined(N_64)

#define ADD_CAMERA_PLUGINS(res) \
	{\
		ADD_PLUGIN(res, NdmCisco, N_T("Cameras/NdmCisco"));\
		ADD_PLUGIN(res, NdmMobotix, N_T("Cameras/NdmMobotix"));\
		ADD_PLUGIN(res, NdmProsillica, N_T("Cameras/NdmProsillica"));\
	}

#define ADD_F_SCANNER_PLUGINS(res) \
	{\
		ADD_PLUGIN(res, NdmAbilma, N_T("FScanners/NdmAbilma"));\
		ADD_PLUGIN(res, NdmArhFps, N_T("FScanners/NdmArhFps"));\
		ADD_PLUGIN(res, NdmBioca, N_T("FScanners/NdmBioca"));\
		ADD_PLUGIN(res, NdmBiometrika, N_T("FScanners/NdmBiometrika"));\
		ADD_PLUGIN(res, NdmBioTech, N_T("FScanners/NdmBioTech"));\
		ADD_PLUGIN(res, NdmBlueFin, N_T("FScanners/NdmBlueFin"));\
		ADD_PLUGIN(res, NdmCogent, N_T("FScanners/NdmCogent"));\
		ADD_PLUGIN(res, NdmCrossMatch, N_T("FScanners/NdmCrossMatch"));\
		ADD_PLUGIN(res, NdmCrossMatchLScan, N_T("FScanners/NdmCrossMatchLScan"));\
		ADD_PLUGIN(res, NdmDermalog, N_T("FScanners/NdmDermalog"));\
		ADD_PLUGIN(res, NdmDigitalPersonaUareU, N_T("FScanners/NdmDigitalPersonaUareU"));\
		ADD_PLUGIN(res, NdmDigitalPersonaUareUOneTouch, N_T("FScanners/NdmDigitalPersonaUareUOneTouch"));\
		ADD_PLUGIN(res, NdmFutronic, N_T("FScanners/NdmFutronic"));\
		ADD_PLUGIN(res, NdmFutronicEthernetFam, N_T("FScanners/NdmFutronicEthernetFam"));\
		ADD_PLUGIN(res, NdmGreenBit, N_T("FScanners/NdmGreenBit"));\
		ADD_PLUGIN(res, NdmHongda, N_T("FScanners/NdmHongda"));\
		ADD_PLUGIN(res, NdmId3Certis, N_T("FScanners/NdmId3Certis"));\
		ADD_PLUGIN(res, NdmIdentix, N_T("FScanners/NdmIdentix"));\
		ADD_PLUGIN(res, NdmIntec, N_T("FScanners/NdmIntec"));\
		ADD_PLUGIN(res, NdmIntegratedBiometricsIBScanUltimate, N_T("FScanners/NdmIntegratedBiometricsIBScanUltimate"));\
		ADD_PLUGIN(res, NdmKoehlke, N_T("FScanners/NdmKoehlke"));\
		ADD_PLUGIN(res, NdmLumidigm, N_T("FScanners/NdmLumidigm"));\
		ADD_PLUGIN(res, NdmMiaxis, N_T("FScanners/NdmMiaxis"));\
		ADD_PLUGIN(res, NdmNextBiometrics, N_T("FScanners/NdmNextBiometrics"));\
		ADD_PLUGIN(res, NdmNitgen, N_T("FScanners/NdmNitgen"));\
		ADD_PLUGIN(res, NdmNitgenNBioScan, N_T("FScanners/NdmNitgenNBioScan"));\
		ADD_PLUGIN(res, NdmSecuGen, N_T("FScanners/NdmSecuGen"));\
		ADD_PLUGIN(res, NdmStartek, N_T("FScanners/NdmStartek"));\
		ADD_PLUGIN(res, NdmSupremaBioMini, N_T("FScanners/NdmSupremaBioMini"));\
		ADD_PLUGIN(res, NdmSupremaRealScan, N_T("FScanners/NdmSupremaRealScan"));\
		ADD_PLUGIN(res, NdmTenBio, N_T("FScanners/NdmTenBio"));\
		ADD_PLUGIN(res, NdmTSTBiometrics, N_T("FScanners/NdmTSTBiometrics"));\
		ADD_PLUGIN(res, NdmUnionCommunity, N_T("FScanners/NdmUnionCommunity"));\
		ADD_PLUGIN(res, NdmUpek, N_T("FScanners/NdmUpek"));\
		ADD_PLUGIN(res, NdmZKSGroup, N_T("FScanners/NdmZKSGroup"));\
		ADD_PLUGIN(res, NdmZKSoftware, N_T("FScanners/NdmZKSoftware"));\
	}

#define ADD_IRIS_SCANNER_PLUGINS(res) \
	{\
		ADD_PLUGIN(res, NdmCmiTechIris, N_T("IrisScanners/NdmCmiTechIris"));\
		ADD_PLUGIN(res, NdmCrossMatchIScan, N_T("IrisScanners/NdmCrossMatchIScan"));\
		ADD_PLUGIN(res, NdmIriTechIriShield, N_T("IrisScanners/NdmIriTechIriShield"));\
	}

#define ADD_MULTI_MODAL_DEVICE_PLUGINS(res) \
	{\
		ADD_PLUGIN(res, NdmVistaImaging, N_T("MultiModalDevices/NdmVistaImaging"));\
		ADD_PLUGIN(res, NdmIrisGuard, N_T("MultiModalDevices/NdmIrisGuard"));\
	}

#elif defined(N_WINDOWS) && defined(N_64)

#define ADD_CAMERA_PLUGINS(res) \
	{\
		ADD_PLUGIN(res, NdmCisco, N_T("Cameras/NdmCisco"));\
		ADD_PLUGIN(res, NdmMobotix, N_T("Cameras/NdmMobotix"));\
		ADD_PLUGIN(res, NdmProsillica, N_T("Cameras/NdmProsillica"));\
	}

#define ADD_F_SCANNER_PLUGINS(res) \
	{\
		ADD_PLUGIN(res, NdmAbilma, N_T("FScanners/NdmAbilma"));\
		ADD_PLUGIN(res, NdmArhFps, N_T("FScanners/NdmArhFps"));\
		ADD_PLUGIN(res, NdmBiometrika, N_T("FScanners/NdmBiometrika"));\
		ADD_PLUGIN(res, NdmBioTech, N_T("FScanners/NdmBioTech"));\
		ADD_PLUGIN(res, NdmBlueFin, N_T("FScanners/NdmBlueFin"));\
		ADD_PLUGIN(res, NdmCrossMatchLScan, N_T("FScanners/NdmCrossMatchLScan"));\
		ADD_PLUGIN(res, NdmDermalog, N_T("FScanners/NdmDermalog"));\
		ADD_PLUGIN(res, NdmDigitalPersonaUareU, N_T("FScanners/NdmDigitalPersonaUareU"));\
		ADD_PLUGIN(res, NdmDigitalPersonaUareUOneTouch, N_T("FScanners/NdmDigitalPersonaUareUOneTouch"));\
		ADD_PLUGIN(res, NdmFutronic, N_T("FScanners/NdmFutronic"));\
		ADD_PLUGIN(res, NdmFutronicEthernetFam, N_T("FScanners/NdmFutronicEthernetFam"));\
		ADD_PLUGIN(res, NdmGreenBit, N_T("FScanners/NdmGreenBit"));\
		ADD_PLUGIN(res, NdmId3Certis, N_T("FScanners/NdmId3Certis"));\
		ADD_PLUGIN(res, NdmIntegratedBiometricsIBScanUltimate, N_T("FScanners/NdmIntegratedBiometricsIBScanUltimate"));\
		ADD_PLUGIN(res, NdmLumidigm, N_T("FScanners/NdmLumidigm"));\
		ADD_PLUGIN(res, NdmNextBiometrics, N_T("FScanners/NdmNextBiometrics"));\
		ADD_PLUGIN(res, NdmNitgen, N_T("FScanners/NdmNitgen"));\
		ADD_PLUGIN(res, NdmNitgenNBioScan, N_T("FScanners/NdmNitgenNBioScan"));\
		ADD_PLUGIN(res, NdmSecuGen, N_T("FScanners/NdmSecuGen"));\
		ADD_PLUGIN(res, NdmStartek, N_T("FScanners/NdmStartek"));\
		ADD_PLUGIN(res, NdmSupremaBioMini, N_T("FScanners/NdmSupremaBioMini"));\
		ADD_PLUGIN(res, NdmSupremaRealScan, N_T("FScanners/NdmSupremaRealScan"));\
		ADD_PLUGIN(res, NdmUpek, N_T("FScanners/NdmUpek"));\
	}

#define ADD_IRIS_SCANNER_PLUGINS(res) \
	{\
		ADD_PLUGIN(res, NdmCmiTechIris, N_T("IrisScanners/NdmCmiTechIris"));\
		ADD_PLUGIN(res, NdmIriTechIriShield, N_T("IrisScanners/NdmIriTechIriShield"));\
	}

#define ADD_MULTI_MODAL_DEVICE_PLUGINS(res) \
	{\
		ADD_PLUGIN(res, NdmVistaImaging, N_T("MultiModalDevices/NdmVistaImaging"));\
	}

#elif defined(N_LINUX) && !defined(N_64)

#define ADD_CAMERA_PLUGINS(res) \
	{\
		ADD_PLUGIN(res, NdmCisco, N_T("Cameras/NdmCisco"));\
		ADD_PLUGIN(res, NdmMobotix, N_T("Cameras/NdmMobotix"));\
		ADD_PLUGIN(res, NdmProsillica, N_T("Cameras/NdmProsillica"));\
	}

#define ADD_F_SCANNER_PLUGINS(res) \
	{\
		ADD_PLUGIN(res, NdmAbilma, N_T("FScanners/NdmAbilma"));\
		ADD_PLUGIN(res, NdmArhFps, N_T("FScanners/NdmArhFps"));\
		ADD_PLUGIN(res, NdmBlueFin, N_T("FScanners/NdmBlueFin"));\
		ADD_PLUGIN(res, NdmFutronic, N_T("FScanners/NdmFutronic"));\
		ADD_PLUGIN(res, NdmFutronicEthernetFam, N_T("FScanners/NdmFutronicEthernetFam"));\
		ADD_PLUGIN(res, NdmGreenBit, N_T("FScanners/NdmGreenBit"));\
		ADD_PLUGIN(res, NdmIntegratedBiometricsIBScanUltimate, N_T("FScanners/NdmIntegratedBiometricsIBScanUltimate"));\
		ADD_PLUGIN(res, NdmLumidigm, N_T("FScanners/NdmLumidigm"));\
		ADD_PLUGIN(res, NdmNextBiometrics, N_T("FScanners/NdmNextBiometrics"));\
		ADD_PLUGIN(res, NdmNitgen, N_T("FScanners/NdmNitgen"));\
		ADD_PLUGIN(res, NdmSupremaBioMini, N_T("FScanners/NdmSupremaBioMini"));\
		ADD_PLUGIN(res, NdmSupremaRealScan, N_T("FScanners/NdmSupremaRealScan"));\
		ADD_PLUGIN(res, NdmUpek, N_T("FScanners/NdmUpek"));\
	}

#define ADD_IRIS_SCANNER_PLUGINS(res) \
	{\
		ADD_PLUGIN(res, NdmIriTechIriShield, N_T("IrisScanners/NdmIriTechIriShield"));\
	}

#define ADD_MULTI_MODAL_DEVICE_PLUGINS(res) \
	{\
	}

#elif defined(N_LINUX) && defined(N_64)

#define ADD_CAMERA_PLUGINS(res) \
	{\
		ADD_PLUGIN(res, NdmCisco, N_T("Cameras/NdmCisco"));\
		ADD_PLUGIN(res, NdmMobotix, N_T("Cameras/NdmMobotix"));\
		ADD_PLUGIN(res, NdmProsillica, N_T("Cameras/NdmProsillica"));\
	}

#define ADD_F_SCANNER_PLUGINS(res) \
	{\
		ADD_PLUGIN(res, NdmAbilma, N_T("FScanners/NdmAbilma"));\
		ADD_PLUGIN(res, NdmArhFps, N_T("FScanners/NdmArhFps"));\
		ADD_PLUGIN(res, NdmBlueFin, N_T("FScanners/NdmBlueFin"));\
		ADD_PLUGIN(res, NdmFutronic, N_T("FScanners/NdmFutronic"));\
		ADD_PLUGIN(res, NdmFutronicEthernetFam, N_T("FScanners/NdmFutronicEthernetFam"));\
		ADD_PLUGIN(res, NdmGreenBit, N_T("FScanners/NdmGreenBit"));\
		ADD_PLUGIN(res, NdmIntegratedBiometricsIBScanUltimate, N_T("FScanners/NdmIntegratedBiometricsIBScanUltimate"));\
		ADD_PLUGIN(res, NdmLumidigm, N_T("FScanners/NdmLumidigm"));\
		ADD_PLUGIN(res, NdmNextBiometrics, N_T("FScanners/NdmNextBiometrics"));\
		ADD_PLUGIN(res, NdmNitgen, N_T("FScanners/NdmNitgen"));\
		ADD_PLUGIN(res, NdmSupremaBioMini, N_T("FScanners/NdmSupremaBioMini"));\
		ADD_PLUGIN(res, NdmSupremaRealScan, N_T("FScanners/NdmSupremaRealScan"));\
		ADD_PLUGIN(res, NdmUpek, N_T("FScanners/NdmUpek"));\
	}

#define ADD_IRIS_SCANNER_PLUGINS(res) \
	{\
		ADD_PLUGIN(res, NdmIriTechIriShield, N_T("IrisScanners/NdmIriTechIriShield"));\
	}

#define ADD_MULTI_MODAL_DEVICE_PLUGINS(res) \
	{\
	}

#elif defined(N_MAC) && !defined(N_64)

#define ADD_CAMERA_PLUGINS(res) \
	{\
	}

#define ADD_F_SCANNER_PLUGINS(res) \
	{\
		ADD_PLUGIN(res, NdmFutronic, N_T("FScanners/NdmFutronic"));\
		ADD_PLUGIN(res, NdmFutronicEthernetFam, N_T("FScanners/NdmFutronicEthernetFam"));\
		ADD_PLUGIN(res, NdmNextBiometrics, N_T("FScanners/NdmNextBiometrics"));\
		ADD_PLUGIN(res, NdmUpek, N_T("FScanners/NdmUpek"));\
	}

#define ADD_IRIS_SCANNER_PLUGINS(res) \
	{\
	}

#define ADD_MULTI_MODAL_DEVICE_PLUGINS(res) \
	{\
	}

#elif defined(N_MAC) && defined(N_64)

#define ADD_CAMERA_PLUGINS(res) \
	{\
	}

#define ADD_F_SCANNER_PLUGINS(res) \
	{\
		ADD_PLUGIN(res, NdmAbilma, N_T("FScanners/NdmAbilma"));\
		ADD_PLUGIN(res, NdmFutronic, N_T("FScanners/NdmFutronic"));\
		ADD_PLUGIN(res, NdmFutronicEthernetFam, N_T("FScanners/NdmFutronicEthernetFam"));\
		ADD_PLUGIN(res, NdmNextBiometrics, N_T("FScanners/NdmNextBiometrics"));\
		ADD_PLUGIN(res, NdmUpek, N_T("FScanners/NdmUpek"));\
	}

#define ADD_IRIS_SCANNER_PLUGINS(res) \
	{\
	}

#define ADD_MULTI_MODAL_DEVICE_PLUGINS(res) \
	{\
	}

#endif

#endif

#ifdef N_CPP
}
#endif

#endif // SAMPLE_UTILS_H_INCLUDED

include ../../../Common/Build/config.mk

### wx-config ###

# WX_CONFIG = wx-config

### name of target ###

TARGET_NAME = DevicesSampleWX

### application bundle for MacOSX ###
ifeq ($(OS),MacOSX)
  TARGET_BUNDLE = yes
endif

### Compiler flags ###
CXXFLAGS += `$(WX_CONFIG) --cxxflags`
CXXFLAGS += -I.

### Source files ###

# CPP source files

CXXSRCS += \
		Precompiled.cpp \
		DevicesSample.cpp \
		DevicesSampleForm.cpp \
		CustomProperty.cpp \
		Common/CPP/wxSampleConfig.cpp \
		Dialogs/BiometricDeviceDialog.cpp \
		Dialogs/CameraDialog.cpp \
		Dialogs/CaptureDeviceDialog.cpp \
		Dialogs/CaptureDialog.cpp \
		Dialogs/ConnectToDeviceDialog.cpp \
		Dialogs/CustomizeFormatDialog.cpp \
		Dialogs/DeviceManagerDialog.cpp \
		Dialogs/FScannerDialog.cpp \
		Dialogs/IrisScannerDialog.cpp \
		Dialogs/MicrophoneDialog.cpp \
		CollectionEditor.cpp \
		StructEditor.cpp \
			
### Default libraries ###

LDLIBS += `$(WX_CONFIG) --libs core,propgrid,xml,adv,richtext`
ifeq ($(OS),MacOSX)
  LDLIBS += -framework NBiometricClient -framework NBiometrics -framework NDevices -framework NMedia -framework NMediaProc -framework NCore -framework NLicensing
else
  LDLIBS += -lNBiometricClient -lNBiometrics -lNDevices -lNMedia -lNLicensing -lNCore -lNMediaProc
  LDLIBS += -luuid
endif

include ../../../Common/Build/rules.mk

include ../../../Common/Build/config.mk

### wx-config ###
# Define this variable with correct path to wx-config if you want to use other then default wxWidgets version.
#WX_CONFIG := wx-config

### name of target ###

TARGET_NAME = IrisesSampleWX

### application bundle for MacOSX ###
ifeq ($(OS),MacOSX)
  TARGET_BUNDLE = yes
endif

### Compiler flags ###

CXXFLAGS += `$(WX_CONFIG) --cxxflags`

### Source files ###

# CPP source files

CXXSRCS += \
		Precompiled.cpp \
		IrisesSample.cpp \
		IrisesSampleForm.cpp \
		ExtractionSource.cpp \
		EnrollDlg.cpp \
		OptionsDlg.cpp \
		Common/CPP/wxSampleConfig.cpp \
		Common/CPP/SampleCommon.cpp

### Default libraries ###

LDLIBS += `$(WX_CONFIG) --libs core,richtext,html,xml,adv`

LDLIBS_Linux += -lNBiometricClient -lNBiometrics -lNDevices -lNMedia -lNLicensing -lNCore

LDLIBS_MacOSX += -framework NBiometricClient -framework NBiometrics -framework NDevices -framework NMedia -framework NMediaProc -framework NCore -framework NLicensing

include ../../../Common/Build/rules.mk


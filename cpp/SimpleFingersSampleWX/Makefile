include ../../../Common/Build/config.mk

### wx-config ###
# Define this variable with correct path to wx-config if you want to use other then default wxWidgets version.
#WX_CONFIG := wx-config

### name of target ###

TARGET_NAME = SimpleFingersSampleWX

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
		SimpleFingersSample.cpp \
		SimpleFingersSampleForm.cpp \
		EnrollFromImage.cpp \
		EnrollFromScanner.cpp \
		IdentifyFinger.cpp \
		VerifyFinger.cpp \
		SegmentFingerprints.cpp \
		GeneralizeFinger.cpp \
		LicensePanel.cpp \
		../../../Common/CPP/SampleCommon.cpp

### Default libraries ###

LDLIBS += `$(WX_CONFIG) --libs core`

LDLIBS_MacOSX += -framework NBiometricClient -framework NBiometrics -framework NDevices -framework NMedia -framework NCore -framework NLicensing

LDLIBS_Linux += -lNBiometricClient -lNBiometrics -lNDevices -lNMedia -lNLicensing -lNCore

include ../../../Common/Build/rules.mk


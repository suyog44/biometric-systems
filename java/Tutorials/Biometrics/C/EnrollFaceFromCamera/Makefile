include ../../../Common/Build/config.mk

### target ###

TARGET_NAME   = EnrollFaceFromCamera

### Objects, sources ###

# C source files

CSRCS = \
	EnrollFaceFromCamera.c

ifeq ($(N_MAC_OSX_FRAMEWORKS),yes)
  LDLIBS ?= -framework NBiometricClient -framework NBiometrics -framework NDevices -framework NMedia -framework NCore -framework NLicensing
else
  LDLIBS ?= -lNBiometricClient -lNBiometrics -lNDevices -lNMedia -lNCore -lNLicensing
endif

include ../../../Common/Build/rules.mk


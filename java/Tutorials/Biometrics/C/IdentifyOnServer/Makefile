include ../../../Common/Build/config.mk

### target ###

TARGET_NAME   = IdentifyOnServer

### Objects, sources ###

# C source files

CSRCS = \
	IdentifyOnServer.c

ifeq ($(N_MAC_OSX_FRAMEWORKS),yes)
  LDLIBS ?= -framework NBiometricClient -framework NBiometrics -framework NDevices -framework NMedia -framework NCore -framework NLicensing
else
  LDLIBS ?= -lNBiometricClient -lNBiometrics -lNDevices -lNMedia -lNCore -lNLicensing
endif

include ../../../Common/Build/rules.mk

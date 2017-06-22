include ../../../Common/Build/config.mk

### target ###

TARGET_NAME   = LicenseActivationFromDongle

### Objects, sources ###

# C source files

CSRCS = \
	LicenseActivationFromDongle.c

ifeq ($(N_MAC_OSX_FRAMEWORKS),yes)
  LDLIBS ?= -framework NLicensing -framework NCore
else
  LDLIBS ?= -lNLicensing -lNCore
endif

include ../../../Common/Build/rules.mk


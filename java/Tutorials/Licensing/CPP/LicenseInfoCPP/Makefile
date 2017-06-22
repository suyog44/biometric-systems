include ../../../Common/Build/config.mk

### target ###

TARGET_NAME   = LicenseInfoCPP

### Objects, sources ###

# CPP source files

CXXSRCS = \
	LicenseInfoCPP.cpp

ifeq ($(N_MAC_OSX_FRAMEWORKS),yes)
  LDLIBS ?= -framework NLicensing -framework NCore
else
  LDLIBS ?= -lNLicensing -lNCore
endif

include ../../../Common/Build/rules.mk


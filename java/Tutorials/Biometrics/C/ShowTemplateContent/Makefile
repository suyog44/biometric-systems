include ../../../Common/Build/config.mk

CPPFLAGS += -DN_PRODUCT_HAS_FINGERS
CPPFLAGS += -DN_PRODUCT_HAS_FACES
CPPFLAGS += -DN_PRODUCT_HAS_IRISES

### target ###

TARGET_NAME = ShowTemplateContent

### Objects, sources ###

# C source files

CSRCS = \
	ShowTemplateContent.c

### libraries ###

ifeq ($(N_MAC_OSX_FRAMEWORKS),yes)
  LDLIBS ?= -framework NBiometricClient -framework NBiometrics -framework NDevices -framework NMedia -framework NCore -framework NLicensing
else
  LDLIBS ?= -lNBiometricClient -lNBiometrics -lNDevices -lNMedia -lNCore -lNLicensing
endif

include ../../../Common/Build/rules.mk


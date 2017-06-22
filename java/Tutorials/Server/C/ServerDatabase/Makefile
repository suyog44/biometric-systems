include ../../../Common/Build/config.mk

### target ###

TARGET_NAME   = ServerDatabase

### Objects, sources ###

# C source files

CSRCS = \
	ServerDatabase.c

ifeq ($(N_MAC_OSX_FRAMEWORKS),yes)
  LDLIBS ?= -framework NCluster -framework NCore
else
  LDLIBS ?= -lNCluster -lNCore
endif

include ../../../Common/Build/rules.mk


ROOTDIR = ../../../../

BUILDTOOLS_DIR := $(dir $(lastword $(MAKEFILE_LIST)))

include $(or $(wildcard $(BUILDTOOLS_DIR)arch.mk),$(ROOTDIR)Build/MakeTools/arch.mk)

ifeq ($(OS),MacOSX)
  ifneq ($(USE_STATIC_LIBS),yes)
    N_MAC_OSX_FRAMEWORKS := yes
  endif
endif

# vim:filetype=make

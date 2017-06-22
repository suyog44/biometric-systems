ROOTDIR := ../../../../

BUILDTOOLS_DIR := $(dir $(lastword $(MAKEFILE_LIST)))

include $(or $(wildcard $(BUILDTOOLS_DIR)arch.mk),$(ROOTDIR)Build/MakeTools/arch.mk)

ifndef WX_CONFIG
  WX_CONFIG = $(or $(if $(strip $(shell which wx-config-3.0 2> /dev/null)),wx-config-3.0),$(if $(filter $(strip $(shell wx-config --release 3.0 2> /dev/null)),3.0),wx-config,$(error ERROR: wxWidgets-3.0 not found! Please install wxWidgets-3.0.x or define WX_CONFIG variable with correct path to wx-config of wzxWidgets-3.0 installation)))
endif

# vim:filetype=make

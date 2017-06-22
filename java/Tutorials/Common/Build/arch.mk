# Try to guess OS and ARCH for Makefiles

NATIVE_ARCH	?= $(shell uname -m | sed 's/i.86/x86/')
NATIVE_OS       ?= $(shell uname | sed 's/Darwin/MacOSX/g')
NATIVE_PLATFORM ?= $(NATIVE_OS)_$(NATIVE_ARCH)

NATIVE_OSTYPE   ?= $(shell uname -o 2>/dev/null || uname)

ifneq ($(CROSS),)
  ifneq (,$(shell $(CROSS)gcc -dM -E - < /dev/null | grep ANDROID | sed -n '/ANDROID/p'))
    OS ?= Android
  endif
  ifneq (,$(shell $(CROSS)gcc -dM -E - < /dev/null | grep linux | sed -n '/linux/p'))
    OS ?= Linux
  endif
endif

ifeq ($(OS),Android)
  ANDROID_ARCHS ?= armeabi armeabi-v7a arm64-v8a x86
  ifeq ($(ARCH),arm64-v8a)
    CROSS ?= aarch64-linux-android-
  endif
  ifeq ($(ARCH),x86)
    CROSS ?= i686-linux-android-
  endif
  ifeq ($(ARCH),x86_64)
    CROSS ?= x86_64-linux-android-
  endif
  CROSS ?= arm-linux-androideabi-
  $(if $(wildcard $(shell which $(CROSS)gcc 2> /dev/null)),,$(error Standalone toolchain of android ndk not found. Please, install it with <ANDROID_NDK_HOME>/build/tools/make-standalone-toolchain.sh and add path of it to PATH variable))
endif

ifeq ($(NATIVE_OS),AIX)
  OS ?= aix
endif

OS ?= $(NATIVE_OS)

ifeq ($(OS),QNX)
  ARCH = armv7
endif

ifeq ($(OS),Linux)
  ifneq ($(CROSS),)
    ifneq (,$(shell $(CROSS)gcc -dM -E - < /dev/null | grep __arm__))
      ifneq (,$(shell $(CROSS)gcc -dM -E - < /dev/null | grep __ARMEL__))
        ifneq (,$(shell $(CROSS)gcc -dM -E - < /dev/null | grep __SOFTFP__))
          ARCH ?= armel
        endif
        ARCH ?= armhf
      endif
      ARCH ?= arm
    else
      CFW = $(firstword $(subst -, ,$(notdir $(CROSS))))
      ifeq ($(NATIVE_ARCH),x86)
        ARCH ?= x86
      else
        ARCH ?= $(CFW)
      endif
    endif
  endif
endif

ifneq (,$(findstring $(OS),MacOSX iOS))
  ifeq ($(OS),iOS)
    ARCH = universal
  else ifeq ($(UNIVERSAL_BIN),yes)
    ARCH = universal
  else
    ifeq ($(shell uname -p),powerpc)
      ARCH ?= ppc
      APPLE_ARCHS := ppc
    endif
    ifneq ($(shell echo "\#include <stdlib.h>" | $(CROSS)gcc $(CFLAGS) -dM -E - | grep __i386__ | sed -n '/i386/p'),)
      ARCH ?= x86
    endif
    ifneq ($(shell echo "\#include <stdlib.h>" | $(CROSS)gcc $(CFLAGS) -dM -E - | grep __x86_64__ | sed -n '/x86_64/p'),)
      ARCH ?= x86_64
    endif
    ARCH ?= x86
  endif

  APPLE_BUILD = yes
  ifeq ($(OS),iOS)
    APPLE_ARCHS ?= armv7 arm64 i386 x86_64
  endif
  ifneq ($(UNIVERSAL_BIN),yes)
    APPLE_ARCHS ?= $(or $(if $(filter x86,$(ARCH)),i386),$(if $(filter ppc,$(ARCH)),powerpc),$(ARCH))
  endif
  APPLE_ARCHS ?= i386 x86_64
else
  ifneq ($(shell uname -m | sed -n -e '/arm/p;'),)
    ifneq (,$(shell $(CROSS)gcc -dM -E - < /dev/null | grep __ARMEL__))
      ifneq (,$(shell $(CROSS)gcc -dM -E - < /dev/null | grep __SOFTFP__))
        ARCH ?= armel
      endif
      ARCH ?= armhf
    endif
    ARCH ?= arm
  endif

  ifndef CROSS
    ARCH ?= $(NATIVE_ARCH)
  endif

  ifneq ($(shell echo "\#include <stdlib.h>" | $(CROSS)gcc -dM -E - | grep __UCLIBC__ | sed -n '/UCLIBC/p'),)
    LIBC ?= uclibc
  endif
endif

ifneq ($(OS),Android)
  ifeq ($(ARCH),arm)
    ifneq (,$(shell echo "\#include <stdlib.h>" | $(CROSS)gcc -dM -E - | grep __GLIBC__ | sed -n '/GLIBC/p'))
      LIBC ?= glibc
    endif
    ifeq (,$(LIBC))
      $(error LIBC variable should ve set to "glibc" or "uClibc")
    endif
  endif
endif

ifeq ($(OS),iOS)
  LIBC ?=
  ARCH := universal
endif

PLATFORM ?= $(OS)$(if $(LIBC),_$(LIBC))$(if $(ARCH),_$(ARCH))

# vim:filetype=make:

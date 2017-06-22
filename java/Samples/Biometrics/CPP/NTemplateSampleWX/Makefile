include ../../../Common/Build/config.mk

### wx-config ###
# Define this variable with correct path to wx-config if you want to use other then default wxWidgets version.
#WX_CONFIG := wx-config

### name of target ###

TARGET_NAME = NTemplateSampleWX

### application bundle for MacOSX ###
ifeq ($(OS),MacOSX)
  TARGET_BUNDLE = yes
endif

### Compiler flags ###

CXXFLAGS += `$(WX_CONFIG) --cxxflags`
CXXFLAGS += -I.

### Source files ###

# CPP source files

CXXSRCS += \
		Precompiled.cpp \
		TemplateSample.cpp \
		TemplateSampleForm.cpp \
		MainUIComponentsHelpers/TemplateSamplePropertyGrid.cpp \
		MainUIComponentsHelpers/TemplateSampleTreeCtrl.cpp \
		MainUIComponentsHelpers/TreeNodeData.cpp \
		Controllers/TemplateSampleController.cpp \
		Common/CPP/wxSampleConfig.cpp \
		CollectionEditor/CollectionBaseAdapter.cpp \
		CollectionEditor/CollectionEditorDialog.cpp \
		CollectionEditor/CoreCollectionAdapter.cpp \
		CollectionEditor/DeltaCollectionAdapter.cpp \
		CollectionEditor/DoubleCoreCollectionAdapter.cpp \
		CollectionEditor/FaceCollectionAdapter.cpp \
		CollectionEditor/FingerCollectionAdapter.cpp \
		CollectionEditor/PalmCollectionAdapter.cpp \
		CollectionEditor/IrisCollectionAdapter.cpp \
		CollectionEditor/VoiceCollectionAdapter.cpp \
		CollectionEditor/MinutiaeCollectionAdapter.cpp \
		CollectionEditor/MinutiaeNeighborsCollectionAdapter.cpp \
		CollectionEditor/NCollectionProperty.cpp \
		RecordAddDialogs/NFingerRecordDlg.cpp \
		RecordAddDialogs/NIrisRecordDlg.cpp \
		CollectionEditor/PossiblePositionCollectionAdapter.cpp \
		CollectionEditor/MultiChoiceProperty.cpp \
			
### Default libraries ###

LDLIBS += `$(WX_CONFIG) --libs core,propgrid,xml,adv`

LDLIBS_MacOSX += -framework NBiometricClient -framework NBiometrics -framework NDevices -framework NMedia -framework NMediaProc -framework NCore -framework NLicensing

LDLIBS_Linux += -lNBiometricClient -lNBiometrics -lNDevices -lNMedia -lNLicensing -lNCore

include ../../../Common/Build/rules.mk


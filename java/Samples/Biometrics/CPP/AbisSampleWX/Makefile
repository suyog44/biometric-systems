include ../../../Common/Build/config.mk

### wx-config ###
# Define this variable with correct path to wx-config if you want to use other then default wxWidgets version.
#WX_CONFIG := wx-config

### name of target ###

TARGET_NAME = AbisSampleWX

### application bundle for MacOSX ###
ifeq ($(OS),MacOSX)
  TARGET_BUNDLE = yes
endif

### Compiler flags ###

CXXFLAGS += -Wall `$(WX_CONFIG) --cxxflags`
CXXFLAGS += -I.

### Source files ###

# CPP source files

CXXSRCS += \
	AbisSample.cpp \
	AbisSampleForm.cpp \
	Common/BusyIndicator.cpp \
	Common/CPP/SampleCommon.cpp \
	Common/CPP/wxSampleConfig.cpp \
	Common/ChangeDatabaseDialog.cpp \
	Common/DatabaseOperationPage.cpp \
	Common/EnrollDataSerializer.cpp \
	Common/FirstPage.cpp \
	Common/GeneralizeProgressView.cpp \
	Common/GetSubjectDialog.cpp \
	Common/ImagePanel.cpp \
	Common/ImageView.cpp \
	Common/LicensingTools.cpp \
	Common/LongActionDialog.cpp \
	Common/MatchingResultPage.cpp \
	Common/MatchingResultView.cpp \
	Common/OpenSubjectDialog.cpp \
	Common/SchemaBuilderDialog.cpp \
	Common/SchemaPropertyGridCtrl.cpp \
	Common/StatusPanel.cpp \
	Common/SubjectTreeWidget.cpp \
	Common/SubjectUtils.cpp \
	Common/SvgShape.cpp \
	Common/TabController.cpp \
	Common/TabPage.cpp \
	Common/IcaoWarningsView.cpp \
	Precompiled.cpp \
	Settings/BaseSettingsPage.cpp \
	Settings/ConnectToDeviceForm.cpp \
	Settings/FacesSettingsPage.cpp \
	Settings/FingersSettingsPage.cpp \
	Settings/GeneralSettingsPage.cpp \
	Settings/IrisesSettingsPage.cpp \
	Settings/PalmsSettingsPage.cpp \
	Settings/SettingsManager.cpp \
	Settings/SettingsPanel.cpp \
	Settings/VoicesSettingsPage.cpp \
	SubjectEditor/CaptureFacePage.cpp \
	SubjectEditor/CaptureFingersPage.cpp \
	SubjectEditor/CaptureIrisesPage.cpp \
	SubjectEditor/CapturePalmPage.cpp \
	SubjectEditor/CaptureVoicePage.cpp \
	SubjectEditor/EditPhrasesDialog.cpp \
	SubjectEditor/FingerSelector.cpp \
	SubjectEditor/FingersSelectorBase.cpp \
	SubjectEditor/ModalityPage.cpp \
	SubjectEditor/NFObjectUi.cpp \
	SubjectEditor/PalmSelector.cpp \
	SubjectEditor/PreviewBiometricPage.cpp \
	SubjectEditor/SubjectPage.cpp \
	SubjectEditor/SubjectPagePanel.cpp \
	SubjectEditor/VoiceView.cpp \

### Default libraries ###

LDLIBS += `$(WX_CONFIG) --libs core,richtext,html,xml,adv,aui,propgrid`
LDLIBS_Linux += `pkg-config --libs gobject-2.0`

LDLIBS_Linux += -lNBiometricClient -lNBiometrics -lNDevices -lNMedia -lNLicensing -lNCore

LDLIBS_Linux += `pkg-config --libs x11`
LDLIBS_Linux += `pkg-config --libs cairo`
LDLIBS_Linux += -lrt

LDLIBS_MacOSX += -framework NBiometricClient -framework NBiometrics -framework NDevices -framework NMedia -framework NMediaProc -framework NCore -framework NLicensing

include ../../../Common/Build/rules.mk


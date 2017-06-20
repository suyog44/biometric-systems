#include "Precompiled.h"
#include <DevicesSampleForm.h>
#include <DevicesSampleWXVersionInfo.h>
#include <Dialogs/DeviceManagerDialog.h>
#include <Dialogs/ConnectToDeviceDialog.h>
#include <Dialogs/CustomizeFormatDialog.h>
#include <Dialogs/IrisScannerDialog.h>
#include <Dialogs/CameraDialog.h>
#include <Dialogs/MicrophoneDialog.h>
#include <Dialogs/FScannerDialog.h>
#include <Dialogs/IrisScannerDialog.h>
#include <CustomProperty.h>

#ifdef N_MAC_OSX_FRAMEWORKS
#include <NGui/Gui/Neurotechnology.xpm>
#else
#include <Gui/Neurotechnology.xpm>
#endif

using namespace Neurotec::Gui;
using namespace Neurotec::Devices;
using namespace Neurotec::Media;
using namespace Neurotec::Collections;
using namespace Neurotec::Biometrics;
using namespace Neurotec::ComponentModel;
using namespace Neurotec::Samples::Dialogs;

wxDEFINE_EVENT(DEVICE_COLLECTION_CHANGED_NOTIF, wxCommandEvent);
wxDEFINE_EVENT(NCORE_SUPPRESSED_ERROR_NOTIF, wxCommandEvent);

namespace Neurotec
{
	namespace Samples
	{
		BEGIN_EVENT_TABLE(DevicesSampleForm, wxFrame)
			EVT_MENU(ID_MNU_ABOUT, DevicesSampleForm::OnMenuAboutClick)
			EVT_MENU(ID_MNU_CLOSE, DevicesSampleForm::OnMenuCloseDeviceManagerClick)
			EVT_MENU(ID_MNU_EXIT, DevicesSampleForm::OnMenuExitClick)
			EVT_MENU(ID_MNU_NEW, DevicesSampleForm::OnMenuNewDeviceManagerClick)
			EVT_MENU(ID_MNU_CONNECT, DevicesSampleForm::OnMenuConnectClick)
			EVT_MENU(ID_MNU_DISCONNECT, DevicesSampleForm::OnMenuDisconnectClick)
			EVT_MENU(ID_MNU_SHOW_PLUGIN, DevicesSampleForm::OnMenuShowPluginClick)
			EVT_BUTTON(ID_CUSTOMIZE_FORMAT_BTN, DevicesSampleForm::OnButtonCustomizeClick)
			EVT_BUTTON(ID_DEVICE_CAPTURE_BTN, DevicesSampleForm::OnButtonCaptureClick)
			EVT_BUTTON(ID_START_SEQUENCE_BTN, DevicesSampleForm::OnButtonStartSequenceClick)
			EVT_BUTTON(ID_END_SEQUENCE_BTN, DevicesSampleForm::OnButtonEndSequenceClick)
			EVT_CHECKBOX(ID_USE_TIMEOUT_CHK, DevicesSampleForm::OnTimeoutCheckChanged)
			EVT_COMMAND(wxID_ANY, DEVICE_COLLECTION_CHANGED_NOTIF, DevicesSampleForm::OnDeviceCollectionChanged)
			EVT_COMMAND(wxID_ANY, NCORE_SUPPRESSED_ERROR_NOTIF, DevicesSampleForm::OnNCoreSuppressedError)
			EVT_TREE_SEL_CHANGED(ID_TREE_VIEW, DevicesSampleForm::OnTreeViewSelectionChange)
			EVT_COMBOBOX(ID_DEVICE_IMPRESSION_CMB, DevicesSampleForm::OnComboImpressionChange)
		END_EVENT_TABLE()

		DevicesSampleForm::DevicesSampleForm(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize &size, long style) :
			wxFrame(parent, id, title, position, size, style), m_boolClearingTree(false)
		{
			CreateGUIControls();

			wxCommandEvent ev(wxEVT_MENU);
			ev.SetId(ID_MNU_NEW);
			wxPostEvent(this, ev);

			UpdateMenu();
			NCore::AddErrorSuppressedCallback(&DevicesSampleForm::OnNCoreErrorSuppressedCallback, this);
		}

		DevicesSampleForm::~DevicesSampleForm()
		{
		}

		void DevicesSampleForm::CreateGUIControls()
		{
			m_menuBar = new wxMenuBar();
			wxMenu *menu = new wxMenu();
			menu->Append(ID_MNU_NEW, "New");
			menu->Append(ID_MNU_CLOSE, "Close");
			menu->AppendSeparator();
			menu->Append(ID_MNU_EXIT, "E&xit");
			m_menuBar->Append(menu, "Device &manager");
			menu = new wxMenu();
			menu->Append(ID_MNU_CONNECT, "&Connect...");
			menu->Append(ID_MNU_DISCONNECT, "&Disconnect");
			menu->AppendSeparator();
			menu->Append(ID_MNU_SHOW_PLUGIN, "Show &plugin");
			m_menuBar->Append(menu, "Device");
			menu = new wxMenu();
			menu->Append(ID_MNU_ABOUT, "About DevicesSample");
			m_menuBar->Append(menu, "Help");
			SetMenuBar(m_menuBar);

			wxBoxSizer *boxSizerMain = new wxBoxSizer(wxVERTICAL);
			wxSplitterWindow *splitterVertical = new wxSplitterWindow(this, wxID_ANY, wxPoint(0, 0), wxDefaultSize, wxSP_3D);
			m_splitterTopHorizontal = new wxSplitterWindow(splitterVertical, wxID_ANY, wxPoint(0, 0), wxDefaultSize, wxSP_3D);
			m_treeCtrlDeviceList = new wxTreeCtrl(m_splitterTopHorizontal, ID_TREE_VIEW, wxPoint(0, 0), wxDefaultSize, wxTR_HAS_BUTTONS | wxTR_LINES_AT_ROOT | wxTR_HIDE_ROOT);
			m_panelMain = new wxPanel(m_splitterTopHorizontal, wxID_ANY, wxPoint(0, 0), wxDefaultSize);

			wxBoxSizer *BoxSizerMainPanel = new wxBoxSizer(wxVERTICAL);

			wxBoxSizer *boxSizerFingers = new wxBoxSizer(wxHORIZONTAL);
			wxBoxSizer *boxSizerSettings = new wxBoxSizer(wxVERTICAL);
			m_cmbDeviceImpression = new wxComboBox(m_panelMain, ID_DEVICE_IMPRESSION_CMB, wxEmptyString, wxDefaultPosition, wxSize(220, 20), wxArrayString(), wxCB_READONLY);
			m_cmbDevicePosition = new wxComboBox(m_panelMain, wxID_ANY, wxEmptyString, wxDefaultPosition, wxSize(220, 20), wxArrayString(), wxCB_READONLY);

			m_cmbDeviceImpression->Show(false);
			m_cmbDevicePosition->Show(false);
			boxSizerSettings->Add(m_cmbDeviceImpression, 0, wxTOP, 5);
			boxSizerSettings->Add(m_cmbDevicePosition, 0, wxTOP, 5);

			m_cbAutomatic = new wxCheckBox(m_panelMain, wxID_ANY, "Automatic", wxDefaultPosition, wxSize(75, 20));
			m_cbAutomatic->SetValue(true);
			m_cbAutomatic->Show(false);
			boxSizerSettings->Add(m_cbAutomatic, 0, wxEXPAND | wxLEFT | wxRIGHT | wxTOP, 10);

			wxBoxSizer *boxSizerTimeout = new wxBoxSizer(wxHORIZONTAL);
			m_cbUseTimeout = new wxCheckBox(m_panelMain, ID_USE_TIMEOUT_CHK, "Use timeout", wxDefaultPosition, wxSize(80, 20));
			m_cbUseTimeout->Show(false);
			boxSizerTimeout->Add(m_cbUseTimeout, 0, wxRIGHT, 5);

			m_txtCtrlMilliseconds = new wxTextCtrl(m_panelMain, wxID_ANY, "0", wxDefaultPosition, wxSize(80, 20), wxALIGN_RIGHT);
			m_txtCtrlMilliseconds->Show(false);
			boxSizerTimeout->Add(m_txtCtrlMilliseconds, 0, wxRIGHT, 5);

			m_staticTxtMilliseconds = new wxStaticText(m_panelMain, wxID_ANY, "ms", wxDefaultPosition, wxDefaultSize);
			m_staticTxtMilliseconds->Show(false);
			boxSizerTimeout->Add(m_staticTxtMilliseconds, 0);
			boxSizerSettings->Add(boxSizerTimeout, 0, wxEXPAND | wxLEFT | wxRIGHT | wxTOP, 10);

			m_cbGatherImages = new wxCheckBox(m_panelMain, wxID_ANY, "Gather images", wxDefaultPosition, wxDefaultSize);
			m_cbGatherImages->Show(false);
			boxSizerSettings->Add(m_cbGatherImages, 0, wxEXPAND | wxLEFT | wxRIGHT | wxTOP, 10);

			boxSizerFingers->Add(boxSizerSettings, 0, wxEXPAND);

			m_panelMissingFingers = new wxPanel(m_panelMain, wxID_ANY, wxDefaultPosition, wxSize(300, 80));
			m_cbLeftLittle = new wxCheckBox(m_panelMissingFingers, wxID_ANY, "LL", wxPoint(0, 40), wxSize(50, 20));
			m_cbLeftRing = new wxCheckBox(m_panelMissingFingers, wxID_ANY, "LR", wxPoint(25, 20), wxSize(50, 20));
			m_cbLeftMiddle = new wxCheckBox(m_panelMissingFingers, wxID_ANY, "LM", wxPoint(50, 0), wxSize(50, 20));
			m_cbLeftIndex = new wxCheckBox(m_panelMissingFingers, wxID_ANY, "LI", wxPoint(75, 25), wxSize(50, 20));
			m_cbLeftThumb = new wxCheckBox(m_panelMissingFingers, wxID_ANY, "LT", wxPoint(90, 60), wxSize(50, 20));
			m_cbRightThumb = new wxCheckBox(m_panelMissingFingers, wxID_ANY, "RT", wxPoint(160, 60), wxSize(50, 20));
			m_cbRightIndex = new wxCheckBox(m_panelMissingFingers, wxID_ANY, "RI", wxPoint(175, 25), wxSize(50, 20));
			m_cbRightMiddle = new wxCheckBox(m_panelMissingFingers, wxID_ANY, "RM", wxPoint(200, 0), wxSize(50, 20));
			m_cbRightRing = new wxCheckBox(m_panelMissingFingers, wxID_ANY, "RR", wxPoint(225, 20), wxSize(50, 20));
			m_cbRightLittle = new wxCheckBox(m_panelMissingFingers, wxID_ANY, "RL", wxPoint(250, 40), wxSize(50, 20));
			m_panelMissingFingers->Show(false);

			boxSizerFingers->Add(m_panelMissingFingers, 0, wxEXPAND | wxALL, 15);
			BoxSizerMainPanel->Add(boxSizerFingers, 0, wxEXPAND | wxLEFT | wxRIGHT | wxTOP, 10);

			wxBoxSizer *boxSizerFormats = new wxBoxSizer(wxHORIZONTAL);
			m_cmbFormats = new wxComboBox(m_panelMain, wxID_ANY, wxEmptyString, wxDefaultPosition, wxSize(320, 20), wxArrayString(), wxCB_READONLY);
			m_cmbFormats->Show(false);
			boxSizerFormats->Add(m_cmbFormats, 1, wxRIGHT | wxEXPAND, 5);

			m_btnCostomizeFormat = new wxButton(m_panelMain, ID_CUSTOMIZE_FORMAT_BTN, "Customize", wxDefaultPosition, wxDefaultSize);
			m_btnCostomizeFormat->Show(false);
			boxSizerFormats->Add(m_btnCostomizeFormat, 0, wxEXPAND);
			BoxSizerMainPanel->Add(boxSizerFormats, 0, wxEXPAND | wxLEFT | wxRIGHT | wxTOP, 10);

			wxBoxSizer *boxSizerCaptureButtons = new wxBoxSizer(wxHORIZONTAL);
			m_btnDeviceCapture = new wxButton(m_panelMain, ID_DEVICE_CAPTURE_BTN, "Capture", wxDefaultPosition, wxDefaultSize);
			m_btnDeviceCapture->Show(false);
			boxSizerCaptureButtons->Add(m_btnDeviceCapture, 0, wxRIGHT | wxEXPAND, 30);

			m_btnStartSequence = new wxButton(m_panelMain, ID_START_SEQUENCE_BTN, "Start sequence", wxDefaultPosition, wxDefaultSize);
			m_btnStartSequence->Show(false);
			boxSizerCaptureButtons->Add(m_btnStartSequence, 0, wxRIGHT | wxEXPAND, 5);

			m_btnEndSequence = new wxButton(m_panelMain, ID_END_SEQUENCE_BTN, "End sequence", wxDefaultPosition, wxDefaultSize);
			m_btnEndSequence->Show(false);
			boxSizerCaptureButtons->Add(m_btnEndSequence, 0, wxEXPAND);
			BoxSizerMainPanel->Add(boxSizerCaptureButtons, 0, wxEXPAND | wxLEFT | wxRIGHT | wxTOP, 10);

			m_panelMain->SetSizer(BoxSizerMainPanel);

			m_splitterTopHorizontal->SplitVertically(m_treeCtrlDeviceList, m_panelMain, 300);
			wxSplitterWindow *splitterBottomHorizontal = new wxSplitterWindow(splitterVertical, wxID_ANY, wxPoint(0, 0), wxDefaultSize, wxSP_3D);
			wxSplitterWindow *splitterBottomSubVertical = new wxSplitterWindow(splitterBottomHorizontal, wxID_ANY, wxPoint(0, 0), wxDefaultSize, wxSP_3D);

			m_staticTxtType = new wxStaticText(splitterBottomSubVertical, wxID_ANY, wxEmptyString, wxPoint(0, 0), wxSize(300, 10));

			m_pgDeviceProperties = new wxPropertyGridManager(splitterBottomSubVertical, wxID_ANY, wxDefaultPosition, wxDefaultSize,
				wxPG_BOLD_MODIFIED | wxPG_SPLITTER_AUTO_CENTER | wxPG_TOOLBAR | wxPGMAN_DEFAULT_STYLE);
			m_pgDeviceProperties->SetExtraStyle(wxPG_EX_MODE_BUTTONS);
			m_pgDeviceProperties->AddPage();

			splitterBottomSubVertical->SplitHorizontally(m_staticTxtType, m_pgDeviceProperties, 15);
			m_textCtrlLog = new wxTextCtrl(splitterBottomHorizontal, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, wxTE_MULTILINE | wxTE_READONLY);
			m_textCtrlLog->SetBackgroundColour(splitterBottomHorizontal->GetBackgroundColour());

			splitterBottomHorizontal->SplitVertically(splitterBottomSubVertical, m_textCtrlLog, 300);
			splitterVertical->SplitHorizontally(m_splitterTopHorizontal, splitterBottomHorizontal, 300);

			splitterVertical->SetSizeHints(900, 700);
			boxSizerMain->Add(splitterVertical, 1, wxEXPAND);
			SetSizerAndFit(boxSizerMain);
			SetTitle(DEVICES_SAMPLE_WX_TITLE);
			SetIcon(Neurotechnology_XPM);
			Center();
		}

		void DevicesSampleForm::UpdateDeviceList()
		{
			ClearTreeView();
			if (!m_deviceManager.IsNull())
			{
				NDeviceManager::NDeviceCollection deviceCollection = m_deviceManager.GetDevices();
				for (int n = 0; n < deviceCollection.GetCount(); n++)
				{
					NDevice device = deviceCollection.Get(n);
					if (device.GetParent().IsNull())
					{
						FoundDevice(device, m_treeCtrlDeviceList->GetRootItem());
					}
				}
				if (deviceCollection.GetCount() > 0)
				{
					wxTreeItemIdValue cookie = wxTreeItemIdValue();
					m_treeCtrlDeviceList->SelectItem(m_treeCtrlDeviceList->GetFirstChild(m_treeCtrlDeviceList->GetRootItem(), cookie));
				}
				OnSelectedDeviceChanged();
			}
		}

		void DevicesSampleForm::FoundDevice(const NDevice &device, wxTreeItemId parentId)
		{
			UpdateLog("Found device: ", device.GetId(), false);
			wxTreeItemId childId = m_treeCtrlDeviceList->AppendItem(parentId, device.GetDisplayName());
			m_treeCtrlDeviceList->SetItemData(childId, new DeviceTreeNodeData(device));
			NDevice::ChildCollection children = device.GetChildren();
			for (NDevice::ChildCollection::iterator it = children.begin(); it != children.end(); it++)
			{
				FoundDevice(*it, childId);
			}
		}

		void DevicesSampleForm::OnSelectedDeviceChanged()
		{
			NDevice device = NULL;
			wxTreeItemId treeItem = m_treeCtrlDeviceList->GetSelection();
			if (treeItem.IsOk())
			{
				DeviceTreeNodeData * data = static_cast<DeviceTreeNodeData*>(m_treeCtrlDeviceList->GetItemData(treeItem));
				device = data->GetDevice();
				if (!device.IsAvailable())
				{
					device = NULL;
				}
			}
			try
			{
				UpdateMenu();
				UpdatePropertyGrid(device);
				UpdateCapturePanel(device);
			}
			catch (NError &ex)
			{
				wxExceptionDlg::Show(ex);
			}
		}

		void DevicesSampleForm::UpdateMenu()
		{
			m_menuBar->Enable(ID_MNU_DISCONNECT, false);
			m_menuBar->Enable(ID_MNU_CONNECT, false);
			m_menuBar->Enable(ID_MNU_CLOSE, false);
			m_menuBar->Enable(ID_MNU_SHOW_PLUGIN, false);
			if (!m_deviceManager.IsNull())
			{
				NDevice device = NULL;
				wxTreeItemId treeItem = m_treeCtrlDeviceList->GetSelection();
				if (treeItem.IsOk())
				{
					DeviceTreeNodeData* nodeData = static_cast<DeviceTreeNodeData*>(m_treeCtrlDeviceList->GetItemData(treeItem));
					if (nodeData != NULL)
					{
						device = nodeData->GetDevice();
					}
				}

				m_menuBar->Enable(ID_MNU_CLOSE, true);
				m_menuBar->Enable(ID_MNU_CONNECT, true);
				if (!device.IsNull())
				{
					m_menuBar->Enable(ID_MNU_SHOW_PLUGIN, true);
					if (device.IsDisconnectable())
					{
						m_menuBar->Enable(ID_MNU_DISCONNECT, true);
					}
				}
			}
		}

		void DevicesSampleForm::UpdatePropertyGrid(const NDevice &device)
		{
			wxPropertyGridPage *propertyGridPage = m_pgDeviceProperties->GetPage(0);
			propertyGridPage->Clear();
			wxString labelText = wxEmptyString;
			if (!device.IsNull())
			{
				labelText = "Type: " + device.GetNativeType().ToString();
				NArrayWrapper<NPropertyDescriptor> objectProperties = NTypeDescriptor::GetProperties(device);
				for (NArrayWrapper<NPropertyDescriptor>::iterator it = objectProperties.begin(); it != objectProperties.end(); it++)
				{
					if (propertyGridPage->GetProperty(it->GetName()) == NULL)
					{
						propertyGridPage->Append(new CustomProperty(*it, device));
					}
				}
				wxPropertyGridIterator it;
				for (it = propertyGridPage->GetIterator(wxPG_ITERATE_ALL, wxBOTTOM); !it.AtEnd(); it--)
				{
					wxPGProperty* p = *it;
					if (!p->IsTextEditable())
					{
						propertyGridPage->SetPropertyTextColour(p, wxSystemSettings::GetColour(wxSYS_COLOUR_GRAYTEXT));
					}
				}
			}
			m_staticTxtType->SetLabelText(labelText);
		}

		void DevicesSampleForm::UpdateCapturePanel(const NDevice &device)
		{
			NCaptureDevice captureDevice = NULL;
			NCamera camera = NULL;
			NMicrophone microphone = NULL;
			NBiometricDevice biometricDevice = NULL;
			NFScanner fScanner = NULL;
			NIrisScanner irisScanner = NULL;

			if (!device.IsNull())
			{
				NDeviceType deviceType = device.GetDeviceType();

				if ((deviceType & ndtCamera) == ndtCamera)
				{
					camera = NObjectDynamicCast<NCamera>(device);
				}
				if ((deviceType & ndtMicrophone) == ndtMicrophone)
				{
					microphone = NObjectDynamicCast<NMicrophone>(device);
				}
				if ((deviceType & ndtCaptureDevice) == ndtCaptureDevice)
				{
					captureDevice = NObjectDynamicCast<NCaptureDevice>(device);
				}
				if ((deviceType & ndtFScanner) == ndtFScanner)
				{
					fScanner = NObjectDynamicCast<NFScanner>(device);
				}
				if ((deviceType & ndtIrisScanner) == ndtIrisScanner)
				{
					irisScanner = NObjectDynamicCast<NIrisScanner>(device);
				}
				if ((deviceType & ndtBiometricDevice) == ndtBiometricDevice)
				{
					biometricDevice = NObjectDynamicCast<NBiometricDevice>(device);
				}
			}

			bool isCaptureDevice = !camera.IsNull() || !microphone.IsNull() || !fScanner.IsNull() || !irisScanner.IsNull();
			bool isGettingImages = !camera.IsNull() || !fScanner.IsNull() || !irisScanner.IsNull();

			m_cmbDeviceImpression->Show(!fScanner.IsNull());
			m_cmbDevicePosition->Show(!fScanner.IsNull() || !irisScanner.IsNull());

			m_panelMissingFingers->Show(!fScanner.IsNull());

			m_txtCtrlMilliseconds->Show(isCaptureDevice && !biometricDevice.IsNull());
			m_cbUseTimeout->Show(isCaptureDevice && !biometricDevice.IsNull());
			m_cbAutomatic->Show(isCaptureDevice && !biometricDevice.IsNull());
			m_staticTxtMilliseconds->Show(isCaptureDevice && !biometricDevice.IsNull());
			m_txtCtrlMilliseconds->Enable(m_cbUseTimeout->IsChecked());

			m_cbGatherImages->Show(isGettingImages);
			m_btnDeviceCapture->Show(isCaptureDevice);

			m_cmbFormats->Show(!captureDevice.IsNull());
			m_btnCostomizeFormat->Show(!captureDevice.IsNull());

			m_btnStartSequence->Show(!biometricDevice.IsNull());
			m_btnEndSequence->Show(!biometricDevice.IsNull());

			m_panelMain->Layout();

			m_cmbFormats->Clear();

			if (!fScanner.IsNull())
			{
				m_cmbDeviceImpression->Clear();
				m_cmbDevicePosition->Clear();

				if (fScanner.IsAvailable())
				{
					NArrayWrapper<NFImpressionType> supportedTypes = fScanner.GetSupportedImpressionTypes();
					for (int n = 0; n < supportedTypes.GetCount(); n++)
					{
						m_cmbDeviceImpression->Append(NEnum::ToString(NBiometricTypes::NFImpressionTypeNativeTypeOf(), supportedTypes[n]), new IntClientData(supportedTypes[n]));
					}

					if (m_cmbDeviceImpression->GetCount() != 0) m_cmbDeviceImpression->Select(0);

					NArrayWrapper<NFPosition> supportedPossitions = fScanner.GetSupportedPositions();
					m_biometricDevicePositionList.clear();
					for (NArrayWrapper<NFPosition>::iterator it = supportedPossitions.begin(); it != supportedPossitions.end(); it++)
					{
						m_biometricDevicePositionList.push_back(*it);
					}
					ReformDevicePositionComboBox();
				}
			}

			else if (!irisScanner.IsNull())
			{
				m_cmbDevicePosition->Clear();
				if (irisScanner.IsAvailable())
				{
					for (int n = 0; n < irisScanner.GetSupportedPositions().GetCount(); n++)
					{
						m_cmbDevicePosition->Append(NEnum::ToString(NBiometricTypes::NEPositionNativeTypeOf(), irisScanner.GetSupportedPositions()[n]), new IntClientData(irisScanner.GetSupportedPositions().Get(n)));
					}
					if (m_cmbDevicePosition->GetCount() != 0) m_cmbDevicePosition->Select(0);
				}
			}

			if (!captureDevice.IsNull())
			{
				NArrayWrapper<NMediaFormat> mediaFormats = captureDevice.GetFormats();
				for (int n = 0; n < mediaFormats.GetCount(); n++)
				{
					NMediaFormat format = mediaFormats.Get(n);
					m_cmbFormats->SetClientObject(m_cmbFormats->Append(format.ToString()), new ObjectClientData(format));
				}
				NMediaFormat currentFormat = captureDevice.GetCurrentFormat();
				if (!currentFormat.IsNull())
				{
					int formatIndex = m_cmbFormats->FindString(currentFormat.ToString());
					if (formatIndex == -1)
					{
						formatIndex = m_cmbFormats->Append(currentFormat.ToString(), new ObjectClientData(currentFormat));
					}
					m_cmbFormats->Select(formatIndex);
				}
				m_panelMain->Layout();
			}
		}

		void DevicesSampleForm::UpdateLog(const wxString &text, const wxString &data, bool clear)
		{
			if (clear)
			{
				m_textCtrlLog->Clear();
			}
			else
			{
				m_textCtrlLog->WriteText(text + data + "\n");
			}
		}

		void DevicesSampleForm::SetDeviceManager(const NDeviceManager &manager)
		{
			if (manager.Equals(m_deviceManager))
				return;
			if (!m_deviceManager.IsNull())
			{
				m_deviceManager.GetDevices().RemoveCollectionChangedCallback(&DevicesSampleForm::OnDevicesCollectionChangedCallback, this);
				ClearTreeView();
				m_deviceManager = NULL;
			}
			m_deviceManager = manager;

			wxString titleTxt = DEVICES_SAMPLE_WX_TITLE;
			if (!m_deviceManager.IsNull())
			{
				m_deviceManager.GetDevices().AddCollectionChangedCallback(&DevicesSampleForm::OnDevicesCollectionChangedCallback, this);
				wxString deviceTypes = NEnum::ToString(NDevice::NDeviceTypeNativeTypeOf(), m_deviceManager.GetDeviceTypes());
				titleTxt = wxString::Format("%s (Device types: %s)", DEVICES_SAMPLE_WX_TITLE, deviceTypes);
			}
			SetTitle(titleTxt);
			UpdateDeviceList();
		}

		void DevicesSampleForm::OnDevicesCollectionChangedCallback(const CollectionChangedEventArgs<NDevice> &args)
		{
			wxCommandEvent evt(DEVICE_COLLECTION_CHANGED_NOTIF);
			if (args.GetAction() == nccaAdd)
			{
				evt.SetId(DEVICES_ADDED);
				evt.SetClientData(args.GetNewItems()[0].RefHandle());
			}
			else if (args.GetAction() == nccaRemove)
			{
				evt.SetId(DEVICES_REMOVED);
				evt.SetClientData(args.GetOldItems()[0].RefHandle());
			}
			wxPostEvent(static_cast<DevicesSampleForm*>(args.GetParam()), evt);
		}

		void DevicesSampleForm::OnDeviceCollectionChanged(wxCommandEvent &event)
		{
			NDevice device(reinterpret_cast<HNObject>(event.GetClientData()), true);
			switch (event.GetId())
			{
			case DEVICES_ADDED:
				AddDevice(device);
				break;
			case DEVICES_REMOVED:
				RemoveDevice(device);
				break;
			}
		}

		void DevicesSampleForm::AddDevice(const NDevice &device)
		{
			UpdateLog("Added device: ", device.GetId(), false);
			wxTreeItemId deviceId;
			if (GetTreeNodeID(device).IsOk())
			{
				return;
			}
			if (device.GetParent().IsNull())
			{
				deviceId = m_treeCtrlDeviceList->AppendItem(m_treeCtrlDeviceList->GetRootItem(), device.GetDisplayName(), -1, -1, new DeviceTreeNodeData(device));
			}
			else
			{
				deviceId = GetTreeNodeID(device.GetParent());
				if (deviceId.IsOk())
				{
					m_treeCtrlDeviceList->AppendItem(deviceId, device.GetDisplayName(), -1, -1, new DeviceTreeNodeData(device));
				}
			}

			for (int n = 0; n < device.GetChildren().GetCount(); n++)
			{
				NDevice childDevice = device.GetChildren().Get(n);
				if (!childDevice.IsNull() && deviceId.IsOk())
				{
					wxTreeItemId treeItem = GetTreeNodeID(childDevice);
					if (treeItem.IsOk())
					{
						m_treeCtrlDeviceList->Delete(treeItem);
						m_treeCtrlDeviceList->AppendItem(deviceId, childDevice.GetDisplayName(), -1, -1, new DeviceTreeNodeData(device));
					}
				}
			}
		}

		void DevicesSampleForm::RemoveDevice(const NDevice &device)
		{
			UpdateLog("Removed device: ", device.GetId(), false);
			wxTreeItemId node = GetTreeNodeID(device);
			if (node.IsOk())
			{
				m_treeCtrlDeviceList->Delete(node);
			}
			OnSelectedDeviceChanged();
		}

		void DevicesSampleForm::OnTreeViewSelectionChange(wxTreeEvent &WXUNUSED(event))
		{
			try
			{
				if (!m_boolClearingTree)
				{
					OnSelectedDeviceChanged();
				}
			}
			catch (NError &ex)
			{
				wxExceptionDlg::Show(ex);
			}
		}

		wxTreeItemId DevicesSampleForm::GetTreeNodeID(const NDevice &device)
		{
			return FindTreeItem(m_treeCtrlDeviceList, m_treeCtrlDeviceList->GetRootItem(), device);
		}

		wxTreeItemId DevicesSampleForm::FindTreeItem(wxTreeCtrl *pTreeCtrl, const wxTreeItemId &root, const NDevice &device)
		{
			wxTreeItemIdValue cookie;
			wxTreeItemId search;
			wxTreeItemId item = pTreeCtrl->GetFirstChild(root, cookie);
			wxTreeItemId child;

			while (item.IsOk())
			{
				wxString sData = pTreeCtrl->GetItemText(item);
				if (((wxString)device.GetDisplayName()).CompareTo(sData) == 0)
				{
					return item;
				}
				if (pTreeCtrl->ItemHasChildren(item))
				{
					wxTreeItemId search = FindTreeItem(pTreeCtrl, item, device);
					if (search.IsOk())
					{
						return search;
					}
				}
				item = pTreeCtrl->GetNextChild(root, cookie);
			}
			wxTreeItemId dummy;
			return dummy;
		}

		void DevicesSampleForm::ReformDevicePositionComboBox()
		{
			NFImpressionType selectedImpression = (NFImpressionType)((IntClientData*)m_cmbDeviceImpression->GetClientObject(m_cmbDeviceImpression->GetSelection()))->GetData();
			m_cmbDevicePosition->Clear();
			for (std::list<NFPosition>::iterator it = m_biometricDevicePositionList.begin(); it != m_biometricDevicePositionList.end(); it++)
			{
				NFPosition position = *it;
				if (NBiometricTypes::IsPositionCompatibleWith(position, selectedImpression))
				{
					m_cmbDevicePosition->Append(NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), position), new IntClientData(position));
				}
			}
			if (m_cmbDevicePosition->GetCount() != 0) m_cmbDevicePosition->Select(0);
		}

		void DevicesSampleForm::OnMenuAboutClick(wxCommandEvent &WXUNUSED(event))
		{
			wxAboutBox aboutBox(this, -1, DEVICES_SAMPLE_WX_PRODUCT_NAME, DEVICES_SAMPLE_WX_VERSION_STRING, DEVICES_SAMPLE_WX_COPYRIGHT);
			aboutBox.ShowModal();
		}

		void DevicesSampleForm::OnMenuCloseDeviceManagerClick(wxCommandEvent &WXUNUSED(event))
		{
			SetDeviceManager(NULL);
			OnSelectedDeviceChanged();
		}

		void DevicesSampleForm::OnMenuExitClick(wxCommandEvent &WXUNUSED(event))
		{
			Close();
		}

		void DevicesSampleForm::OnMenuNewDeviceManagerClick(wxCommandEvent &WXUNUSED(event))
		{
			try
			{
				DeviceManagerDialog dialog(this);
				if (dialog.ShowModal() == wxID_OK)
				{
					NDeviceManager deviceManager;
					deviceManager.SetDeviceTypes(dialog.GetDeviceTypes());
					deviceManager.SetAutoPlug(dialog.IsAutoPlug());
					deviceManager.Initialize();
					SetDeviceManager(deviceManager);
					dialog.SaveConfigSettings();
				}
			}
			catch (NError &ex)
			{
				wxExceptionDlg::Show(ex);
			}
		}

		void DevicesSampleForm::OnMenuConnectClick(wxCommandEvent &WXUNUSED(event))
		{
			NDevice device = NULL;
			wxTreeItemId treeItem = m_treeCtrlDeviceList->GetSelection();
			if (treeItem.IsOk())
			{
				DeviceTreeNodeData* nodeData = static_cast<DeviceTreeNodeData*>(m_treeCtrlDeviceList->GetItemData(treeItem));
				if (nodeData != NULL)
				{
					device = nodeData->GetDevice();
				}
			}

			ConnectToDeviceDialog connectToDeviceDlg(this);
			if (!device.IsNull()) connectToDeviceDlg.SetSelectedPlugin(device.GetPlugin());
			if (connectToDeviceDlg.ShowModal() == wxID_OK)
			{
				try
				{
					NDevice newDevice = m_deviceManager.ConnectToDevice(connectToDeviceDlg.GetSelectedPlugin(), connectToDeviceDlg.GetParameters());
				}
				catch (NError &ex)
				{
					wxExceptionDlg::Show(ex);
				}
			}
		}

		void DevicesSampleForm::OnMenuDisconnectClick(wxCommandEvent &WXUNUSED(event))
		{
			NDevice device = NULL;
			wxTreeItemId treeItem = m_treeCtrlDeviceList->GetSelection();
			if (treeItem.IsOk())
			{
				DeviceTreeNodeData* nodeData = static_cast<DeviceTreeNodeData*>(m_treeCtrlDeviceList->GetItemData(treeItem));
				if (nodeData != NULL)
				{
					device = nodeData->GetDevice();
				}
			}

			if (!device.IsNull())
			{
				try
				{
					m_deviceManager.DisconnectFromDevice(device);
				}
				catch (NError &ex)
				{
					wxExceptionDlg::Show(ex);
				}
			}
		}

		void DevicesSampleForm::OnMenuShowPluginClick(wxCommandEvent &WXUNUSED(event))
		{
			wxTreeItemId treeItem = m_treeCtrlDeviceList->GetSelection();
			if (treeItem.IsOk())
			{
				DeviceTreeNodeData* nodeData = static_cast<DeviceTreeNodeData*>(m_treeCtrlDeviceList->GetItemData(treeItem));
				if (nodeData != NULL)
				{
					NDevice device = nodeData->GetDevice();
					wxPluginManagerDlg pluginDlg(this, wxID_ANY, NDeviceManager::GetPluginManager(), device.GetPlugin());
					pluginDlg.ShowModal();
				}
			}
		}

		void DevicesSampleForm::OnButtonCaptureClick(wxCommandEvent &WXUNUSED(event))
		{
			wxTreeItemId itemId = m_treeCtrlDeviceList->GetSelection();
			if (itemId != NULL)
			{
				NDevice device = static_cast<DeviceTreeNodeData*>(m_treeCtrlDeviceList->GetItemData(itemId))->GetDevice();

				NCaptureDevice captureDevice = NULL;
				NCamera camera = NULL;
				NMicrophone microphone = NULL;
				NBiometricDevice biometricDevice = NULL;

				NDeviceType deviceType = device.GetDeviceType();

				if ((deviceType & ndtCamera) == ndtCamera)
				{
					camera = NObjectDynamicCast<NCamera>(device);
				}
				if ((deviceType & ndtMicrophone) == ndtMicrophone)
				{
					microphone = NObjectDynamicCast<NMicrophone>(device);
				}
				if ((deviceType & ndtCaptureDevice) == ndtCaptureDevice)
				{
					captureDevice = NObjectDynamicCast<NCaptureDevice>(device);
				}
				if ((deviceType & ndtBiometricDevice) == ndtBiometricDevice)
				{
					biometricDevice = NObjectDynamicCast<NBiometricDevice>(device);
				}

				CaptureDialog *captureDlg = NULL;

				int formatId = m_cmbFormats->GetSelection();
				if (m_cmbFormats->GetCount() > 0 && formatId != -1)
				{
					ObjectClientData *formatData = static_cast<ObjectClientData*>(m_cmbFormats->GetClientObject(formatId));
					NMediaFormat mediaFormat = NObjectDynamicCast<NMediaFormat>(formatData->GetObject());
					if (!mediaFormat.IsNull())
					{
						if (!captureDevice.IsNull())
						{
							try
							{
								captureDevice.SetCurrentFormat(mediaFormat);
							}
							catch (NError &ex)
							{
								wxMessageBox("Error: " + ex.ToString(), "DevicesSampleWX");
								UpdateLog("Error : ", ex.ToString(), false);
								return;
							}
						}
					}
				}

				if (!camera.IsNull())
				{
					CameraDialog *cameraDlg = new CameraDialog(this);
					captureDlg = cameraDlg;
				}
				if (!microphone.IsNull())
				{
					MicrophoneDialog *microphoneDlg = new MicrophoneDialog(this);
					captureDlg = microphoneDlg;
				}

				if (!biometricDevice.IsNull())
				{
					NFScanner fScanner = NULL;
					NIrisScanner irisScanner = NULL;

					if ((deviceType & ndtFScanner) == ndtFScanner || (deviceType & ndtFingerScanner) == ndtFingerScanner)
					{
						fScanner = NObjectDynamicCast<NFScanner>(device);
					}
					else if ((deviceType & ndtIrisScanner) == ndtIrisScanner)
					{
						irisScanner = NObjectDynamicCast<NIrisScanner>(device);
					}

					BiometricDeviceDialog *biometricDeviceDlg = NULL;

					if (!fScanner.IsNull())
					{
						NFImpressionType impressionType = (NFImpressionType)((IntClientData*)m_cmbDeviceImpression->GetClientObject(m_cmbDeviceImpression->GetSelection()))->GetData();
						NFPosition position = (NFPosition)((IntClientData*)m_cmbDevicePosition->GetClientObject(m_cmbDevicePosition->GetSelection()))->GetData();

						FScannerDialog *fScannerDlg = new FScannerDialog(this);
						std::vector<NFPosition> missingPositions;

						if (m_cbLeftLittle->IsChecked()) missingPositions.push_back(nfpLeftLittleFinger);
						if (m_cbLeftRing->IsChecked()) missingPositions.push_back(nfpLeftRingFinger);
						if (m_cbLeftMiddle->IsChecked()) missingPositions.push_back(nfpLeftMiddleFinger);
						if (m_cbLeftIndex->IsChecked()) missingPositions.push_back(nfpLeftIndexFinger);
						if (m_cbLeftThumb->IsChecked()) missingPositions.push_back(nfpLeftThumb);
						if (m_cbRightThumb->IsChecked()) missingPositions.push_back(nfpRightThumb);
						if (m_cbRightIndex->IsChecked()) missingPositions.push_back(nfpRightIndexFinger);
						if (m_cbRightMiddle->IsChecked()) missingPositions.push_back(nfpRightMiddleFinger);
						if (m_cbRightRing->IsChecked()) missingPositions.push_back(nfpRightRingFinger);
						if (m_cbRightLittle->IsChecked()) missingPositions.push_back(nfpRightLittleFinger);

						fScannerDlg->SetMissingPositions(missingPositions);
						fScannerDlg->SetImpressionType(impressionType);
						fScannerDlg->SetPosition(position);

						biometricDeviceDlg = fScannerDlg;
					}

					if (!irisScanner.IsNull())
					{
						IrisScannerDialog *irisScannerDlg = new IrisScannerDialog(this);
						irisScannerDlg->SetPosition((NEPosition)((IntClientData*)m_cmbDevicePosition->GetClientObject(m_cmbDevicePosition->GetSelection()))->GetData());
						biometricDeviceDlg = irisScannerDlg;
					}

					long value;
					m_txtCtrlMilliseconds->GetValue().ToLong(&value);
					biometricDeviceDlg->SetAutomatic(m_cbAutomatic->IsChecked());
					biometricDeviceDlg->SetTimeout((m_cbUseTimeout->IsChecked()) ? (int)value : -1);
					captureDlg = biometricDeviceDlg;
				}

				if (captureDlg != NULL)
				{
					captureDlg->SetGatherImages(m_cbGatherImages->IsChecked());
					captureDlg->SetDevice(device);
					captureDlg->Show();
					captureDlg->OnCaptureDialogShown();
				}
			}
		}

		void DevicesSampleForm::OnButtonCustomizeClick(wxCommandEvent &WXUNUSED(event))
		{
			NMediaFormat selectedFormat = NULL;
			if (m_cmbFormats->GetCount() > 0)
			{
				selectedFormat = NObjectDynamicCast<NMediaFormat>(((ObjectClientData*)m_cmbFormats->GetClientObject(m_cmbFormats->GetSelection()))->GetObject());
			}

			if (selectedFormat.IsNull())
			{
				wxTreeItemId selection = m_treeCtrlDeviceList->GetSelection();
				DeviceTreeNodeData * data = static_cast<DeviceTreeNodeData*>(m_treeCtrlDeviceList->GetItemData(selection));
				NDevice device = data->GetDevice();
				if ((device.GetDeviceType() & ndtCamera) == ndtCamera)
					selectedFormat = NVideoFormat();
				else if ((device.GetDeviceType() & ndtMicrophone) == ndtMicrophone)
					selectedFormat = NAudioFormat();
				else
					NThrowNotImplementedException();
			}

			CustomizeFormatDialog customizeFormatDlg(this);
			NMediaFormat customFormat = customizeFormatDlg.CustomizeFormat(selectedFormat);

			if (customFormat.IsNull())
				return;

			for (unsigned int i = 0; i < m_cmbFormats->GetCount(); i++)
			{
				ObjectClientData * data = static_cast<ObjectClientData*>(m_cmbFormats->GetClientObject(i));
				NMediaFormat format = NObjectDynamicCast<NMediaFormat>(data->GetObject());
				if (format.Equals(customFormat))
				{
					m_cmbFormats->Select(i);
					return;
				}
			}
			int item = m_cmbFormats->Append(customFormat.ToString(), new ObjectClientData(customFormat));
			m_cmbFormats->Select(item);
		}

		void DevicesSampleForm::OnButtonStartSequenceClick(wxCommandEvent &WXUNUSED(event))
		{
			NDevice device = ((DeviceTreeNodeData*)(m_treeCtrlDeviceList->GetItemData(m_treeCtrlDeviceList->GetSelection())))->GetDevice();
			NBiometricDevice biometricDevice = NObjectDynamicCast<NBiometricDevice>(device);

			if (biometricDevice.IsNull()) return;

			try
			{
				biometricDevice.StartSequence();
			}
			catch (NError &ex)
			{
				wxExceptionDlg::Show(ex);
			}
		}

		void DevicesSampleForm::OnButtonEndSequenceClick(wxCommandEvent &WXUNUSED(event))
		{
			NDevice device = ((DeviceTreeNodeData*)(m_treeCtrlDeviceList->GetItemData(m_treeCtrlDeviceList->GetSelection())))->GetDevice();
			NBiometricDevice biometricDevice = NObjectDynamicCast<NBiometricDevice>(device);

			if (biometricDevice.IsNull()) return;

			try
			{
				biometricDevice.EndSequence();
			}
			catch (NError &ex)
			{
				wxExceptionDlg::Show(ex);
			}
		}

		void DevicesSampleForm::OnTimeoutCheckChanged(wxCommandEvent &WXUNUSED(event))
		{
			m_txtCtrlMilliseconds->Enable(m_cbUseTimeout->IsChecked());
		}

		void DevicesSampleForm::OnComboImpressionChange(wxCommandEvent &WXUNUSED(event))
		{
			ReformDevicePositionComboBox();
		}

		void DevicesSampleForm::ClearTreeView()
		{
			m_boolClearingTree = true;
			m_treeCtrlDeviceList->DeleteAllItems();
			m_treeCtrlDeviceList->AddRoot("Devices");
			m_boolClearingTree = false;
		}

		void DevicesSampleForm::OnNCoreErrorSuppressedCallback(const NCore::ErrorSuppressedEventArgs &args)
		{
			wxCommandEvent evt(NCORE_SUPPRESSED_ERROR_NOTIF);
			evt.SetString(args.GetError().ToString());
			wxPostEvent(static_cast<DevicesSampleForm*>(args.GetParam()), evt);
		}

		void DevicesSampleForm::OnNCoreSuppressedError(wxCommandEvent &event)
		{
			UpdateLog("Error suppressed: ", event.GetString(), false);
		}

		DevicesSampleForm::DeviceTreeNodeData::DeviceTreeNodeData(const NDevice &device) : m_device(device)
		{
		}

		NDevice DevicesSampleForm::DeviceTreeNodeData::GetDevice()
		{
			return m_device;
		}

		ObjectClientData::ObjectClientData(const NObject & object) :wxClientData(), m_object(object)
		{
		}

		NObject ObjectClientData::GetObject()
		{
			return m_object;
		}

		void ObjectClientData::SetObject(const NObject & object)
		{
			m_object = object;
		}

		IntClientData::IntClientData(int value) : wxClientData(), m_value(value)
		{
		}

		void IntClientData::SetData(int value)
		{
			m_value = value;
		}

		int IntClientData::GetData()
		{
			return m_value;
		}
	}
}

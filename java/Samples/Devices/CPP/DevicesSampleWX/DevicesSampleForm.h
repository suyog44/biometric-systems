#ifndef DEVICES_SAMPLE_FORM_H_INCLUDED
#define DEVICES_SAMPLE_FORM_H_INCLUDED

wxDECLARE_EVENT(DEVICE_COLLECTION_CHANGED_NOTIF, wxCommandEvent);

#define DevicesSampleForm_STYLE wxCAPTION | wxRESIZE_BORDER | wxSYSTEM_MENU | wxMINIMIZE_BOX | wxMAXIMIZE_BOX | wxCLOSE_BOX

namespace Neurotec
{
	namespace Samples
	{
		class DevicesSampleForm : public wxFrame
		{
		public:
			DevicesSampleForm(wxWindow *parent, const wxWindowID id = wxID_ANY, const wxString &title = wxEmptyString, const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = DevicesSampleForm_STYLE);
			~DevicesSampleForm();

		private:
			void AddDevice(const Neurotec::Devices::NDevice &device);
			void RemoveDevice(const Neurotec::Devices::NDevice &device);
			void CreateGUIControls();
			void UpdateCapturePanel(const Neurotec::Devices::NDevice &device);
			void UpdateDeviceList();
			void FoundDevice(const Neurotec::Devices::NDevice &device, wxTreeItemId parentId);
			void OnSelectedDeviceChanged();
			void UpdatePropertyGrid(const Neurotec::Devices::NDevice &device);
			void UpdateMenu();
			void UpdateLog(const wxString &text, const wxString &data, bool clear);
			void ReformDevicePositionComboBox();
			void OnMenuAboutClick(wxCommandEvent &event);
			void OnMenuCloseDeviceManagerClick(wxCommandEvent &event);
			void OnMenuExitClick(wxCommandEvent &event);
			void OnMenuNewDeviceManagerClick(wxCommandEvent &event);
			void OnButtonCaptureClick(wxCommandEvent &event);
			void OnMenuConnectClick(wxCommandEvent &event);
			void OnMenuDisconnectClick(wxCommandEvent &event);
			void OnMenuShowPluginClick(wxCommandEvent &event);
			void OnButtonCustomizeClick(wxCommandEvent &event);
			void OnDeviceCollectionChanged(wxCommandEvent &event);
			void OnButtonStartSequenceClick(wxCommandEvent &event);
			void OnButtonEndSequenceClick(wxCommandEvent &event);
			void OnTimeoutCheckChanged(wxCommandEvent &event);
			void OnTreeViewSelectionChange(wxTreeEvent &event);
			void OnComboImpressionChange(wxCommandEvent &event);
			void SetDeviceManager(const Neurotec::Devices::NDeviceManager &manager);
			wxTreeItemId GetTreeNodeID(const Neurotec::Devices::NDevice &device);
			wxTreeItemId FindTreeItem(wxTreeCtrl *pTreeCtrl, const wxTreeItemId &root, const Neurotec::Devices::NDevice &device);
			static void OnDevicesCollectionChangedCallback(const Neurotec::Collections::CollectionChangedEventArgs<Neurotec::Devices::NDevice> &args);
			void ClearTreeView();
			static void OnNCoreErrorSuppressedCallback(const Neurotec::NCore::ErrorSuppressedEventArgs &args);
			void OnNCoreSuppressedError(wxCommandEvent &event);

			enum
			{
				ID_MNU_ABOUT,
				ID_MNU_CLOSE,
				ID_MNU_EXIT,
				ID_MNU_NEW,
				ID_MNU_CONNECT,
				ID_MNU_DISCONNECT,
				ID_MNU_SHOW_PLUGIN,
				ID_CUSTOMIZE_FORMAT_BTN,
				ID_DEVICE_CAPTURE_BTN,
				ID_START_SEQUENCE_BTN,
				ID_END_SEQUENCE_BTN,
				ID_USE_TIMEOUT_CHK,
				ID_TREE_VIEW,
				ID_DEVICE_IMPRESSION_CMB
			};

			enum
			{
				DEVICES_RESET = 1002,
				DEVICES_REMOVED,
				DEVICES_ADDED
			};

			wxMenuBar *m_menuBar;
			wxPropertyGridManager *m_pgDeviceProperties;
			wxTreeCtrl *m_treeCtrlDeviceList;
			wxSplitterWindow *m_splitterTopHorizontal;
			wxStaticText *m_staticTxtType;
			wxStaticText *m_staticTxtMilliseconds;
			wxTextCtrl *m_textCtrlLog;
			wxComboBox *m_cmbDeviceImpression;
			wxComboBox *m_cmbDevicePosition;
			wxComboBox *m_cmbFormats;
			wxCheckBox *m_cbAutomatic;
			wxCheckBox *m_cbUseTimeout;
			wxCheckBox *m_cbGatherImages;
			wxButton *m_btnCostomizeFormat;
			wxButton *m_btnDeviceCapture;
			wxButton *m_btnStartSequence;
			wxButton *m_btnEndSequence;
			wxTextCtrl *m_txtCtrlMilliseconds;
			wxCheckBox *m_cbLeftLittle;
			wxCheckBox *m_cbLeftRing;
			wxCheckBox *m_cbLeftMiddle;
			wxCheckBox *m_cbLeftIndex;
			wxCheckBox *m_cbLeftThumb;
			wxCheckBox *m_cbRightThumb;
			wxCheckBox *m_cbRightIndex;
			wxCheckBox *m_cbRightMiddle;
			wxCheckBox *m_cbRightRing;
			wxCheckBox *m_cbRightLittle;
			wxPanel *m_panelMain;
			wxPanel *m_panelMissingFingers;
			std::list<Neurotec::Biometrics::NFPosition> m_biometricDevicePositionList;
			bool m_boolClearingTree;

			Neurotec::Devices::NDeviceManager m_deviceManager;

			DECLARE_EVENT_TABLE();

			class DeviceTreeNodeData : public wxTreeItemData
			{
			public:
				DeviceTreeNodeData(const Neurotec::Devices::NDevice &device);

				Neurotec::Devices::NDevice GetDevice();
				wxTreeItemId GetTreeItemId();

			private:
				Neurotec::Devices::NDevice m_device;
				wxTreeItemId m_itemId;
			};
		};

		class ObjectClientData : public wxClientData
		{
		public:
			ObjectClientData(const Neurotec::NObject &object);
			Neurotec::NObject GetObject();
			void SetObject(const Neurotec::NObject &object);
		private:
			Neurotec::NObject m_object;
		};

		class IntClientData : public wxClientData
		{
		public:
			IntClientData(int value);
			int GetData();
			void SetData(int value);
		private:
			int m_value;
		};
	}
}

#endif

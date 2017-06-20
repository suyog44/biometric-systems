#include "Precompiled.h"
#include "IrisesSampleForm.h"
#include "EnrollDlg.h"

#include "IrisesSampleWXVersionInfo.h"

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NGui/Gui/Neurotechnology.xpm>
#else
	#include <Gui/Neurotechnology.xpm>
#endif

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Images;
using namespace Neurotec::Collections;
using namespace Neurotec::Devices;
using namespace Neurotec::Gui;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Licensing;

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_THREAD, wxCommandEvent);

wxDECLARE_EVENT(wxEVT_DEVICEPLUGGED, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_DEVICEPLUGGED, wxCommandEvent);

BEGIN_EVENT_TABLE(IrisesSampleForm, wxFrame)
	EVT_CLOSE(IrisesSampleForm::OnClose)
	EVT_MENU(ID_MNU_ENROLL, IrisesSampleForm::MnuEnrollClick)
	EVT_MENU(ID_MNU_IDENTIFY, IrisesSampleForm::MnuIdentifyClick)
	EVT_MENU(ID_MNU_CLEAR_LOG, IrisesSampleForm::MnuClearLogClick)
	EVT_MENU(ID_MNU_CLEAR_DB, IrisesSampleForm::MnuClearDatabaseClick)
	EVT_MENU(ID_MNU_CANCEL_TASK, IrisesSampleForm::MnuCancelClick)
#ifdef __WXMAC__
	EVT_MENU(wxID_EXIT, IrisesSampleForm::MnuExitClick)
	EVT_MENU(wxID_PREFERENCES, IrisesSampleForm::MnuOptionsClick)
	EVT_MENU(wxID_ABOUT, IrisesSampleForm::MnuAboutClick)
#else
	EVT_MENU(ID_MNU_EXIT, IrisesSampleForm::MnuExitClick)
	EVT_MENU(ID_MNU_OPTIONS, IrisesSampleForm::MnuOptionsClick)
	EVT_MENU(ID_MNU_ABOUT, IrisesSampleForm::MnuAboutClick)
#endif
	EVT_BUTTON(ID_BUTTON_ENROLL, IrisesSampleForm::MnuEnrollClick)
	EVT_BUTTON(ID_BUTTON_IDENTIFY, IrisesSampleForm::MnuIdentifyClick)
	EVT_COMBOBOX(ID_COMBO_SOURCE, IrisesSampleForm::OnSelectedSourceChanged)
	EVT_COMBOBOX(ID_COMBO_POSITION, IrisesSampleForm::OnSelectedModeChanged)
END_EVENT_TABLE()

IrisesSampleForm::IrisesSampleForm(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize& size, long style)
: wxFrame(parent, id, title, position, size, style)
{
	CreateGUIControls();

	this->Bind(wxEVT_THREAD, &IrisesSampleForm::OnThread, this);
	this->Bind(wxEVT_DEVICEPLUGGED, &IrisesSampleForm::OnSourcesChanged, this);

	const wxString Address = wxT("/local");
	const wxString Port = wxT("5000");

	try
	{
		bool obtained = NLicense::ObtainComponents(Address, Port, wxT("Biometrics.IrisExtraction"));
		if(obtained) AppendText(wxT("License for extractor obtained successfully.\n"));
		else AppendTextError(wxString::Format(wxT("Failed to obtain license for extractor.\n")));

		obtained = NLicense::ObtainComponents(Address, Port, wxT("Biometrics.IrisMatchingFast"));
		if(obtained) AppendText(wxT("License for fast matcher obtained successfully.\n"));
		else AppendTextError(wxString::Format(wxT("Failed to obtain license for fast matcher.\n")));

		obtained = NLicense::ObtainComponents(Address, Port, wxT("Biometrics.IrisMatching"));
		if(obtained) AppendText(wxT("License for matcher obtained successfully.\n"));
		else AppendTextError(wxString::Format(wxT("Failed to obtain license for matcher.\n")));

		obtained = NLicense::ObtainComponents(Address, Port, wxT("Biometrics.Standards.Irises"));
		if(obtained) AppendText(wxT("Licenses for biometric standards obtained successfully.\n"));
	}
	catch(Neurotec::NError& ex)
	{
		AppendTextError(wxString::Format(wxT("Failed to obtain licenses for components: %s\n"), ((wxString)ex.ToString()).c_str()));
	}
	try
	{
		m_biometricClient.SetBiometricTypes(nbtIris);
		m_biometricClient.SetUseDeviceManager(true);
		wxString dbPath = wxSampleConfig::GetUserDataDir() + wxFileName::GetPathSeparator() + wxT("IrisesV5.db");
		m_biometricClient.SetDatabaseConnectionToSQLite(dbPath);
		m_biometricClient.Initialize();

		OptionsDlg::LoadOptions(m_biometricClient);

		NDeviceManager deviceManager = m_biometricClient.GetDeviceManager();
		deviceManager.GetDevices().AddCollectionChangedCallback(&IrisesSampleForm::OnScannerCollectionChanged, this);

		ExtractionSource * source = NULL;
		NArrayWrapper<NDevice> devices = deviceManager.GetDevices().GetAll();
		for (int i = 0; i < devices.GetCount(); i++)
		{
			source = new ExtractionSource(ExtractionSource::sourceTypeIrisScanner);
			comboSource->Append(devices[i].GetId(), source);
		}

		source = new ExtractionSource(ExtractionSource::sourceTypeFile);
		comboSource->Append(wxT("File"), source);
		source = new ExtractionSource(ExtractionSource::sourceTypeDirectory);
		comboSource->Append(wxT("Directory"), source);

		m_isCanceling = false;
		m_isOnDestroy = false;
		m_currentSource = NULL;

		this->SetSize(700, 700);
		comboSource->SetSelection(0);
		wxCommandEvent cmd(wxEVT_COMMAND_COMBOBOX_SELECTED, ID_COMBO_SOURCE);
		wxPostEvent(this, cmd);
	}
	catch (NError & error)
	{
		wxExceptionDlg::Show(error);
	}
}

IrisesSampleForm::~IrisesSampleForm()
{
	if (m_biometricClient.GetHandle() != NULL)
	{
		OptionsDlg::SaveOptions(m_biometricClient);
		m_biometricClient.GetDeviceManager().GetDevices().RemoveCollectionChangedCallback(&IrisesSampleForm::OnScannerCollectionChanged, this);
	}

	if (irisView != NULL)
		delete irisView;

	NLicense::ReleaseComponents(wxT("Biometrics.IrisExtraction"));
	NLicense::ReleaseComponents(wxT("Biometrics.IrisMatchingFast"));
	NLicense::ReleaseComponents(wxT("Biometrics.IrisMatching"));
	NLicense::ReleaseComponents(wxT("Biometrics.Standards.Irises"));
}

void IrisesSampleForm::OnClose(wxCloseEvent &/*event*/)
{
	m_isOnDestroy = true;
	wxCommandEvent evt(wxEVT_THREAD, ID_CANCEL_OPERATIONS);
	wxPostEvent(this, evt);
}

void IrisesSampleForm::MnuExitClick(wxCommandEvent &/*event*/)
{
	Close(true);
}

void IrisesSampleForm::MnuCancelClick(wxCommandEvent &/*event*/)
{
	wxCommandEvent cancelEvent(wxEVT_THREAD, ID_CANCEL_OPERATIONS);
	wxPostEvent(this, cancelEvent);
}

void IrisesSampleForm::MnuEnrollClick(wxCommandEvent &/*event*/)
{
	PerformTask(chbCheckForDuplicates->GetValue() ? nboEnrollWithDuplicateCheck : nboEnroll);
}

void IrisesSampleForm::MnuIdentifyClick(wxCommandEvent &/*event*/)
{
	if (m_currentSource->GetSourceType() == ExtractionSource::sourceTypeDirectory)
	{
		comboSource->SetStringSelection(wxT("File"));
		m_currentSource = new ExtractionSource(ExtractionSource::sourceTypeFile);
	}
	listCtrlResults->DeleteAllItems();

	PerformTask(nboIdentify);
}

void IrisesSampleForm::OnCancelCompleted()
{
	AppendText("Cancel finished\n");

	m_isCanceling = false;
	if (m_isOnDestroy) Destroy();
}

void IrisesSampleForm::OnOperationCompleted(EventArgs args)
{
	NAsyncOperation operation = args.GetObject<NAsyncOperation>();
	IrisesSampleForm *sampleForm = static_cast<IrisesSampleForm *>(args.GetParam());

	try
	{
		NValue result = operation.GetResult();

		NBiometricTask task = result.ToObject(NBiometricTask::NativeTypeOf()).GetHandle();
		NBiometricTask::SubjectCollection subjects = task.GetSubjects();

		NBiometricOperations nbOperation = task.GetOperations();
		if (subjects.GetCount() > 0)
		{
			NSubject subject = subjects.Get(0);

			NSubject::IrisCollection irises = subject.GetIrises();
			if (irises.GetCount() > 0)
			{
				NIris iris = irises.Get(0);
				wxCommandEvent event(wxEVT_THREAD, ID_SET_IRIS_TO_VIEW);
				event.SetClientData(iris.RefHandle());
				wxPostEvent(sampleForm, event);
			}

			if (subject.GetStatus() == nbsOk)
			{
				if (nbOperation == nboEnroll || nbOperation == nboEnrollWithDuplicateCheck)
				{
					wxString resultTxt = wxString::Format(wxT("Template: '%s'\nenrolled to database\n"), (wxString)subject.GetId());
					Write(sampleForm, resultTxt);
				}
				else if (nbOperation == nboIdentify)
				{
					NSubject::MatchingResultCollection matchingResults = subject.GetMatchingResults();
					std::vector<NMatchingResult> * resultVector = new std::vector<NMatchingResult>();

					for (int i = 0; i < matchingResults.GetCount(); i++)
					{
						resultVector->push_back(matchingResults.Get(i));
						wxString resultTxt = wxString::Format(wxT("matched with ID '%s' with score: %i\n"), (wxString)(matchingResults.Get(i).GetId()), matchingResults.Get(i).GetScore());
						Write(sampleForm, resultTxt);
					}

					wxCommandEvent event(wxEVT_THREAD, ID_ADD_RESULTS);
					event.SetClientData(resultVector);
					wxPostEvent(sampleForm, event);
				}
			}
			else
			{
				NError exception = subject.GetError();
				if (exception.GetHandle() != NULL)
				{
					wxString resultTxt = exception.ToString();
					WriteError(sampleForm, resultTxt);
				}
				else
				{
					if (nbOperation == nboEnroll || nbOperation == nboEnrollWithDuplicateCheck)
					{
						wxString resultTxt = wxString::Format("Template: '%s'\nFailed: %s\n", (wxString)subject.GetId(),
							((wxString)NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), subject.GetStatus())).mb_str());
						Write(sampleForm, resultTxt);
					}
					else
					{
						wxString resultTxt = wxString::Format("Failed: %s\n", ((wxString)NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(),
							subject.GetStatus())).mb_str());
						Write(sampleForm, resultTxt);
					}
				}
			}
		}

		wxCommandEvent removeOperation(wxEVT_THREAD, ID_REMOVE_OPERATION);
		removeOperation.SetClientData(operation.RefHandle());
		wxPostEvent(sampleForm, removeOperation);
	}
	catch (NError& ex)
	{
		if (operation.IsFaulted())
		{
			wxString exceptionMessage = ex.ToString();
			WriteError(sampleForm, exceptionMessage);
		}

		wxCommandEvent removeOperation(wxEVT_THREAD, ID_REMOVE_OPERATION);
		removeOperation.SetClientData(operation.RefHandle());
		wxPostEvent(sampleForm, removeOperation);

		return;
	}
}

std::vector<NSubject> IrisesSampleForm::CreateSubjectsFromSource(bool requiresId)
{
	std::vector<NSubject> subjects;

	if (m_currentSource)
	{
		switch(m_currentSource->GetSourceType())
		{
		case ExtractionSource::sourceTypeIrisScanner:
		{
			wxString id = wxEmptyString;

			if (requiresId)
			{
				EnrollDlg sourceIdDialog(this);
				if(sourceIdDialog.ShowModal() == wxID_OK)
				{
					id = sourceIdDialog.GetUserId();
				}

				if (id == wxEmptyString) break;
			}

			NSubject subject;
			if (requiresId) subject.SetId(id);

			NIris iris;
			iris.SetPosition(static_cast<NEPosition>(m_currentSource->GetSelectedMode()));
			subject.GetIrises().Add(iris);

			subjects.push_back(subject);
			break;
		}
		case ExtractionSource::sourceTypeFile:
		{
			wxString id = wxEmptyString;
			wxArrayString selectedFiles = SelectFileList(m_currentSource->GetSelectedMode());

			if (selectedFiles.GetCount() < 1) break;

			if (requiresId)
			{
				if (selectedFiles.GetCount() == 1)
				{
					id = selectedFiles[0];
				}
				else
				{
					EnrollDlg sourceIdDialog(this);
					if(sourceIdDialog.ShowModal() == wxID_OK)
					{
						id = sourceIdDialog.GetUserId();
					}
				}

				if (id == wxEmptyString) break;
			}

			NSubject subject;

			if (requiresId) subject.SetId(id);

			for (int i = 0; i < ((m_currentSource->GetSelectedMode() == nepBoth)? 2 : 1); i++)
			{
				NIris iris;

				if (m_currentSource->GetSelectedMode() == nepBoth)
				{
					iris.SetPosition((i == 0)? nepLeft : nepRight);
				}
				else
				{
					iris.SetPosition(static_cast<NEPosition>(m_currentSource->GetSelectedMode()));
				}

				iris.SetFileName(selectedFiles[i]);
				subject.GetIrises().Add(iris);
			}

			subjects.push_back(subject);
			break;
		}
		case ExtractionSource::sourceTypeDirectory:
		{
			wxArrayString selectedFiles = SelectAllFilesFromDirectory();
			if (selectedFiles.GetCount() < 1) break;

			for (unsigned int i = 0; i < selectedFiles.GetCount(); i++)
			{
				NSubject subject;
				NIris iris;
				iris.SetFileName(selectedFiles[i]);
				iris.SetPosition(static_cast<NEPosition>(m_currentSource->GetSelectedMode()));
				subject.GetIrises().Add(iris);
				subject.SetId(selectedFiles[i]);
				subjects.push_back(subject);
			}

			break;
		}
		default: break;
		};
	}

	return subjects;
}

void IrisesSampleForm::PerformTask(Neurotec::Biometrics::NBiometricOperations operation)
{
	if(m_currentSource)
	{
		bool askForId = false;
		if (operation == nboEnroll || operation == nboEnrollWithDuplicateCheck) askForId = true;

		std::vector<NSubject> subjects = CreateSubjectsFromSource(askForId);
		if (subjects.size() > 0)
		{
			if (operation == nboEnroll || operation == nboEnrollWithDuplicateCheck)
			{
				AppendText(wxT("\nEnroll started\n"));
			}
			else if (operation == nboIdentify)
			{
				AppendText(wxT("\nIdentify started\n"));
			}
		}

		if (subjects.size() > 0) DisableControls();

		for (unsigned int i = 0; i < subjects.size(); i++)
		{
			NSubject subject = subjects.at(i);

			if (m_currentSource->GetSourceType() == ExtractionSource::sourceTypeIrisScanner)
			{
				NSubject::IrisCollection irises = subject.GetIrises();
				if (irises.GetCount() > 0)
				{
					NIris iris = irises.Get(0);
					irisView->SetIris(iris);
				}
			}

			NBiometricTask task = m_biometricClient.CreateTask(operation, subject);
			NAsyncOperation asyncOperation = m_biometricClient.PerformTaskAsync(task);
			m_pendingOperations.push_back(asyncOperation);
			asyncOperation.AddCompletedCallback(&IrisesSampleForm::OnOperationCompleted, this);
		}
	}
}

void IrisesSampleForm::MnuOptionsClick(wxCommandEvent &/*event*/)
{
	OptionsDlg optionsDlg(this, m_biometricClient);
	optionsDlg.ShowModal();
}

void IrisesSampleForm::MnuAboutClick(wxCommandEvent &/*event*/)
{
	wxAboutBox aboutBox(this, -1, IRISES_SAMPLE_WX_PRODUCT_NAME, IRISES_SAMPLE_WX_VERSION_STRING, IRISES_SAMPLE_WX_COPYRIGHT);
	aboutBox.ShowModal();
}

void IrisesSampleForm::MnuClearLogClick(wxCommandEvent &/*event*/)
{
	richTextCtrlLog->Clear();
}

void IrisesSampleForm::MnuClearDatabaseClick(wxCommandEvent &/*event*/)
{
	m_biometricClient.Clear();
	AppendText(wxT("Database cleared\n"));
}

void IrisesSampleForm::OnThread(wxCommandEvent &event)
{
	int id = event.GetId();
	switch(id)
	{
	case ID_CANCEL_OPERATIONS:
		{
			m_isCanceling = true;
			AppendText(wxT("Cancel started\n"));

			if (m_pendingOperations.size() > 0)
			{
				m_biometricClient.Cancel();
				for (unsigned int i = 0; i < m_pendingOperations.size(); i++)
				{
					try
					{
						m_pendingOperations.at(i).Cancel();
					}
					catch(NError& ex)
					{
						wxString exceptionText = ex.ToString();
						AppendTextError(exceptionText);
					}
				}
			}
			else
			{
				m_isCanceling = false;
				OnCancelCompleted();
			}

			break;
		}
	case ID_REMOVE_OPERATION:
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetHandle() == NULL) return;

			for (unsigned int i = 0; i < m_pendingOperations.size(); i++)
			{
				if (m_pendingOperations.at(i).Equals(operation))
				{
					m_pendingOperations.erase(m_pendingOperations.begin() + i);
					break;
				}
			}

			if (m_pendingOperations.size() == 0)
			{
				if (m_isCanceling)
				{
					m_isCanceling = false;
					OnCancelCompleted();
				}

				EnableControls();
			}

			break;
		}
	case ID_WRITE:
		{
			wxString resultText = event.GetString();
			AppendText(resultText);
			break;
		}
	case ID_WRITE_ERROR:
		{
			wxString resultText = event.GetString();
			AppendTextError(resultText);
			break;
		}
	case ID_ADD_RESULTS:
		{
			std::vector<NMatchingResult> *results = (std::vector<NMatchingResult> *)event.GetClientData();
			if (results == NULL) break;

			for (unsigned int i = 0; i < results->size(); i++)
			{
				NMatchingResult result = results->at(i);
				listCtrlResults->InsertItem(i, wxString::Format(wxT("%d"), result.GetScore()));
				listCtrlResults->SetItem(i, 1, result.GetId());
			}

			delete results;

			break;
		}
	case ID_SET_IRIS_TO_VIEW:
		{
			NIris iris(static_cast<HNObject>(event.GetClientData()), true);
			irisView->SetIris(iris);
			break;
		}
	default: break;
	}
}

wxArrayString IrisesSampleForm::SelectFileList(int mode)
{
	wxArrayString files = wxArrayString();

	wxArrayString paths;
	if(mode != nepBoth)
	{
		wxFileDialog openFileDialog(this, wxT("Choose iris image(s) to extract"), wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, true), wxFD_OPEN | wxFD_FILE_MUST_EXIST | wxFD_MULTIPLE);
		if (openFileDialog.ShowModal() == wxID_OK)
		{
			openFileDialog.GetPaths(paths);
		}
	}
	else
	{
		wxFileDialog openFileDialogLeft(this, wxT("Choose left iris image to extract"), wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, true), wxFD_OPEN | wxFD_FILE_MUST_EXIST);
		if(openFileDialogLeft.ShowModal() == wxID_OK)
		{
			paths.push_back(openFileDialogLeft.GetPath());
		}
		else return files;

		wxFileDialog openFileDialogRight(this, wxT("Choose right iris image to extract"), wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, true), wxFD_OPEN | wxFD_FILE_MUST_EXIST);
		if(openFileDialogRight.ShowModal() == wxID_OK)
		{
			paths.push_back(openFileDialogRight.GetPath());
		}
		else return files;
	}
	files = paths;
	return files;
}

wxArrayString IrisesSampleForm::SelectAllFilesFromDirectory()
{
	wxArrayString files = wxArrayString();

	wxDirDialog dirDialog(this, wxT("Choose a directory with irises image(s) to extract"));
	if (dirDialog.ShowModal() == wxID_OK)
	{
		wxDir dir(dirDialog.GetPath());
		if (dir.IsOpened())
		{
			wxWindowDisabler disableAll;

			ExtractionSource::DirTraverser sink(&files);
			wxStringTokenizer tkz(Common::GetOpenFileFilter(), wxT(";"));
			while (tkz.HasMoreTokens())
			{
				dir.Traverse(sink, tkz.GetNextToken());
			}
			// remove duplicate entries
			files.Sort();
			size_t j = 0;
			size_t count = files.GetCount();
			for (size_t i = 1; i < count; ++i)
			{
				if (files[j].Cmp(files[i]))
				{
					++j;
					files[j] = files[i];
				}
			}
			++j;
			if (j < files.GetCount())
			{
				files.RemoveAt(j, files.GetCount() - j);
			}
		}
	}

	return files;
}

void IrisesSampleForm::OnSelectedModeChanged(wxCommandEvent &event)
{
	if(!m_currentSource) return;

	int selection = nepUnknown;
	wxString selectionName = comboPosition->GetString(event.GetSelection());

	for (int i = 0; i < ExtractionSource::GetModesCount(); i++)
	{
		if (ExtractionSource::GetModeAsString(i).Contains(selectionName))
		{
			selection = i;
			break;
		}
	}

	if(selection != m_currentSource->GetSelectedMode())
	{
		m_currentSource->SetSelectedMode(selection);
	}
}

void IrisesSampleForm::OnSelectedSourceChanged(wxCommandEvent &/*event*/)
{
	ExtractionSource *source = NULL;
	int selection = comboSource->GetSelection();

	if(selection != -1)
	{
		source = dynamic_cast<ExtractionSource*>(comboSource->GetClientObject(selection));
	}
	comboPosition->Clear();

	if(source)
	{
		switch(source->GetSourceType())
		{
		case ExtractionSource::sourceTypeFile:
			for (int i = 0; i < ExtractionSource::GetModesCount(); i++)
			{
				comboPosition->Append(ExtractionSource::GetModeAsString(i));
			}
			source->SetSelectedMode(nepUnknown);
			break;
		case ExtractionSource::sourceTypeDirectory:
			comboPosition->Append(ExtractionSource::GetModeAsString(nepUnknown));
			source->SetSelectedMode(nepUnknown);
			break;
		case ExtractionSource::sourceTypeIrisScanner:
			NDeviceManager deviceManager = m_biometricClient.GetDeviceManager();
			NIrisScanner selectedDevice = deviceManager.GetDevices().Get(comboSource->GetStringSelection()).GetHandle();

			if (selectedDevice.GetHandle() != NULL)
			{
				m_biometricClient.SetIrisScanner(selectedDevice);

				NArrayWrapper<NEPosition> positions = selectedDevice.GetSupportedPositions();
				for (int i = 0; i < positions.GetCount(); i++)
				{
					comboPosition->Append(ExtractionSource::GetModeAsString(positions[i]));
				}

				source->SetSelectedMode(positions[0]);
			}
			break;
		}

		comboPosition->SetSelection(0);
	}

	m_currentSource = source;
	comboPosition->Enable(comboPosition->GetCount() > 1);
}

void IrisesSampleForm::OnDeviceAdded(NDevice device, void * param)
{
	IrisesSampleForm * irisesSampleForm = (IrisesSampleForm*)param;
	wxCommandEvent resultEvent;
	resultEvent.SetEventType(wxEVT_DEVICEPLUGGED);
	resultEvent.SetInt(ID_DEVICE_PLUGGED);
	resultEvent.SetString(device.GetId());
	wxPostEvent(irisesSampleForm, resultEvent);
}

void IrisesSampleForm::OnDeviceRemoved(NDevice device, void * param)
{
	IrisesSampleForm * irisesSampleForm = (IrisesSampleForm*)param;
	wxCommandEvent resultEvent;
	resultEvent.SetEventType(wxEVT_DEVICEPLUGGED);
	resultEvent.SetInt(ID_DEVICE_UNPLUGGED);
	resultEvent.SetString(device.GetId());
	wxPostEvent(irisesSampleForm, resultEvent);
}

void IrisesSampleForm::OnSourcesChanged(wxCommandEvent &event)
{
	if(event.GetInt() == ID_DEVICE_PLUGGED)
	{
		ExtractionSource *source = new ExtractionSource(ExtractionSource::sourceTypeIrisScanner);
		comboSource->Insert(event.GetString(), 0, source);
		AppendText(wxString::Format(wxT("%s plugged\n"), event.GetString().c_str()));
	}
	else
	{
		AppendText(wxString::Format(wxT("%s unplugged\n"), event.GetString().c_str()));
		int selection = comboSource->GetSelection();
		int removed = comboSource->FindString(event.GetString());
		if(removed != -1)
		{
			comboSource->Delete(removed);
			if(selection == removed)
			{
				m_currentSource = NULL;
				comboSource->SetSelection(0);
				wxCommandEvent cmd(wxEVT_COMMAND_COMBOBOX_SELECTED, ID_COMBO_SOURCE);
				wxPostEvent(this, cmd);
			}
		}
	}
}

void IrisesSampleForm::OnScannerCollectionChanged(Collections::CollectionChangedEventArgs<NDevice> args)
{
	switch (args.GetAction())
	{
	case nccaAdd:
		{
			for (int i = 0; i < args.GetNewItems().GetCount(); i++)
			{
				OnDeviceAdded(args.GetNewItems().Get(i), args.GetParam());
			}
			break;
		}
	case nccaRemove:
		{
			for (int i = 0; i < args.GetOldItems().GetCount(); i++)
			{
				OnDeviceRemoved(args.GetOldItems().Get(i), args.GetParam());
			}
			break;
		}
	default: break;
	};
}

void IrisesSampleForm::AppendText(const wxString & text, const wxColour &color)
{
	richTextCtrlLog->BeginTextColour(color);
	richTextCtrlLog->AppendText(text);
	richTextCtrlLog->EndTextColour();
	long pos = richTextCtrlLog->GetLastPosition();
	richTextCtrlLog->SetCaretPosition(pos);
	richTextCtrlLog->ShowPosition(pos);
	richTextCtrlLog->Update();
}

void IrisesSampleForm::AppendTextError(const wxString & text)
{
	AppendText(text, wxColor(255, 0, 0));
}

void IrisesSampleForm::DisableControls()
{
	buttonEnroll->Enable(false);
	buttonIdentify->Enable(false);
	comboSource->Enable(false);
	comboPosition->Enable(false);
	menuBar->Enable(ID_MNU_ENROLL, false);
	menuBar->Enable(ID_MNU_IDENTIFY, false);
	menuBar->Enable(ID_MNU_OPTIONS, false);
	menuBar->Enable(ID_MNU_CLEAR_DB, false);
}

void IrisesSampleForm::EnableControls()
{
	buttonEnroll->Enable(true);
	buttonIdentify->Enable(true);
	comboPosition->Enable(true);
	comboSource->Enable(true);
	menuBar->Enable(ID_MNU_ENROLL, true);
	menuBar->Enable(ID_MNU_IDENTIFY, true);
	menuBar->Enable(ID_MNU_OPTIONS, true);
	menuBar->Enable(ID_MNU_CLEAR_DB, true);
}

void IrisesSampleForm::Write(IrisesSampleForm * form, wxString& text)
{
	wxCommandEvent event(wxEVT_THREAD, ID_WRITE);
	event.SetString(text);
	wxPostEvent(form, event);
}

void IrisesSampleForm::WriteError(IrisesSampleForm * form, wxString& text)
{
	wxCommandEvent event(wxEVT_THREAD, ID_WRITE_ERROR);
	event.SetString(text);
	wxPostEvent(form, event);
}

void IrisesSampleForm::CreateGUIControls()
{
	CreateToolBar(wxNO_BORDER | wxHORIZONTAL | wxTB_FLAT, ID_TOOLBAR);
	wxToolBar *toolBar = GetToolBar();

	buttonEnroll = new wxButton(toolBar, ID_BUTTON_ENROLL, wxT("&Enroll"));
	buttonEnroll->SetHelpText(wxT("Enroll iris image to the database"));
	toolBar->AddControl(buttonEnroll);

	chbCheckForDuplicates = new wxCheckBox(toolBar, wxID_ANY, wxT("Check for duplicates"));
	chbCheckForDuplicates->SetHelpText(wxT("Check for duplicates when enrolling subject(s) to database"));
	toolBar->AddControl(chbCheckForDuplicates);

	toolBar->AddSeparator();

	buttonIdentify = new wxButton(toolBar, ID_BUTTON_IDENTIFY, wxT("&Identify"));
	buttonIdentify->SetHelpText(wxT("Match iris to all database records"));
	toolBar->AddControl(buttonIdentify);

	toolBar->AddSeparator();

	comboSource = new wxComboBox(toolBar, ID_COMBO_SOURCE, wxEmptyString, wxDefaultPosition, wxDefaultSize, 0, 0, wxCB_READONLY);
	toolBar->AddControl(comboSource);

	comboPosition = new wxComboBox(toolBar, ID_COMBO_POSITION, wxEmptyString, wxDefaultPosition, wxDefaultSize, 0, 0, wxCB_READONLY);
	toolBar->AddControl(comboPosition);

	CreateStatusBar();
	SetStatusText(wxT("Ready"), 0);

	menuBar = new wxMenuBar();
	wxMenu *ID_MNU_JOBS_Mnu_Obj = new wxMenu();
	ID_MNU_JOBS_Mnu_Obj->Append(ID_MNU_ENROLL, wxT("&Enroll\tCtrl-E"), wxT("Enroll iris image to the database"), wxITEM_NORMAL);
	ID_MNU_JOBS_Mnu_Obj->Append(ID_MNU_IDENTIFY, wxT("&Identify\tCtrl-I"), wxT("Match iris image to all database records"), wxITEM_NORMAL);
	ID_MNU_JOBS_Mnu_Obj->Append(ID_MNU_CANCEL_TASK, wxT("&Cancel\tDel"), wxT("Cancel currently running job"), wxITEM_NORMAL);

	ID_MNU_JOBS_Mnu_Obj->AppendSeparator();
#ifdef __WXMAC__
	wxApp::s_macExitMenuItemId = wxID_EXIT;
	ID_MNU_JOBS_Mnu_Obj->Append(wxID_EXIT);
#else
	ID_MNU_JOBS_Mnu_Obj->Append(ID_MNU_EXIT, wxT("E&xit"), wxT("Exit from application"), wxITEM_NORMAL);
#endif
	menuBar->Append(ID_MNU_JOBS_Mnu_Obj, wxT("&Jobs"));

	wxMenu *ID_MNU_TOOLS_Mnu_Obj = new wxMenu();
	ID_MNU_TOOLS_Mnu_Obj->Append(ID_MNU_CLEAR_LOG, wxT("Clear &Log\tCtrl-L"), wxT("Clear log in the lower left window"), wxITEM_NORMAL);
	ID_MNU_TOOLS_Mnu_Obj->Append(ID_MNU_CLEAR_DB, wxT("Clear &Database\tCtrl-D"), wxT("Remove all iris records from the database"), wxITEM_NORMAL);

#ifdef __WXMAC__
	wxApp::s_macPreferencesMenuItemId = wxID_PREFERENCES;
	ID_MNU_TOOLS_Mnu_Obj->Append(wxID_PREFERENCES);
#else
	ID_MNU_TOOLS_Mnu_Obj->AppendSeparator();
	ID_MNU_TOOLS_Mnu_Obj->Append(ID_MNU_OPTIONS, wxT("&Options...\tCtrl-O"), wxT("Open options dialog"), wxITEM_NORMAL);
#endif
	menuBar->Append(ID_MNU_TOOLS_Mnu_Obj, wxT("&Tools"));

	wxMenu *ID_MNU_HELP_Mnu_Obj = new wxMenu();
#ifdef __WXMAC__
	wxApp::s_macAboutMenuItemId = wxID_ABOUT;
	ID_MNU_HELP_Mnu_Obj->Append(wxID_ABOUT);
#else
	ID_MNU_HELP_Mnu_Obj->Append(ID_MNU_ABOUT, wxT("&About"), wxT("Open about dialog"), wxITEM_NORMAL);
#endif
	menuBar->Append(ID_MNU_HELP_Mnu_Obj, wxT("&Help"));
	SetMenuBar(menuBar);

	splitterWindowVer = new wxSplitterWindow(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxSP_3D | wxSP_LIVE_UPDATE);

	irisView = new wxNIrisView(splitterWindowVer);
	splitterWindowHor = new wxSplitterWindow(splitterWindowVer, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxSP_3D | wxSP_LIVE_UPDATE);

	richTextCtrlLog = new wxRichTextCtrl(splitterWindowHor, wxID_ANY, wxT(""), wxDefaultPosition, wxDefaultSize, wxVSCROLL | wxHSCROLL | wxWANTS_CHARS);
	richTextCtrlLog->SetEditable(false);

	listCtrlResults = new wxListCtrl(splitterWindowHor, ID_LISTCTRLRESULTS, wxDefaultPosition, wxDefaultSize, wxLC_REPORT | wxLC_HRULES);
	listCtrlResults->InsertColumn(0, wxT("Score"), wxLIST_FORMAT_LEFT, 60);
	listCtrlResults->InsertColumn(1, wxT("Matched with ID"));
	listCtrlResults->SetColumnWidth(1, 240);

	splitterWindowHor->SplitVertically(richTextCtrlLog, listCtrlResults, 240);
	splitterWindowHor->SetSashGravity(0.5);
	splitterWindowHor->SetMinimumPaneSize(100);

	splitterWindowVer->SplitHorizontally(irisView, splitterWindowHor, 490);
	splitterWindowVer->GetWindow2()->SetMinSize(wxSize(100, 200));
	splitterWindowVer->SetSashGravity(1.0);
	splitterWindowVer->SetMinimumPaneSize(100);

	toolBar->Realize();
	SetTitle(IRISES_SAMPLE_WX_TITLE);
	SetIcon(Neurotechnology_XPM);
	SetSize(900, 750);
	Center();
}

}}

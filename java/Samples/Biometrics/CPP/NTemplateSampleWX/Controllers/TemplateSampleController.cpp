#include "Precompiled.h"
#include "TemplateSampleController.h"

using namespace Neurotec::Biometrics;
using namespace Neurotec::IO;
using namespace Neurotec;
using namespace Neurotec::Gui;

namespace Neurotec { namespace Samples { namespace Controllers
{
	TemplateSampleController::TemplateSampleController() 
	{
		m_template = NTemplate();
		m_fileName = N_TMPL_NTMPL;
	}

	TemplateSampleController::~TemplateSampleController()
	{
	}

	NFrictionRidge TemplateSampleController::GetDataForFingerView(NFRecord record)
	{
		NSubject subject = NSubject();
		NTemplate ntemplate = NTemplate();

		NFrictionRidge frictionRidge = NULL;

		if (NBiometricTypes::IsPositionFinger(record.GetPosition()))
		{
			NFTemplate nfTemplate;
			ntemplate.SetFingers(nfTemplate);
			ntemplate.GetFingers().GetRecords().Add(record);
			subject.SetTemplate(ntemplate);
			frictionRidge = subject.GetFingers().Get(0);
		}
		else
		{
			NFTemplate nfTemplate = NFTemplate(true);
			ntemplate.SetPalms(nfTemplate);
			ntemplate.GetPalms().GetRecords().Add(record);
			subject.SetTemplate(ntemplate);
			frictionRidge = subject.GetPalms().Get(0);
		}

		return frictionRidge;
	}

	void TemplateSampleController::RemoveItem(NObject obj, int currentItemId)
	{
		NTemplate nTemplate = NULL;
		NFTemplate nfTemplate = NULL;
		NLTemplate nlTemplate = NULL;
		NETemplate neTemplate = NULL;
		NSTemplate nsTemplate = NULL;
		NFRecord nfRecord = NULL;
		NLRecord nlRecord = NULL;
		NERecord neRecord = NULL;
		NSRecord nsRecord = NULL;
		wxString type = obj.GetNativeType().GetName();

		if (type == N_TMPL_NTMPL) nTemplate = (NTemplate&)obj;
		else if (type == N_TMPL_NFTMPL)	nfTemplate = (NFTemplate&)obj;
		else if (type == N_TMPL_NFTMPL)	nlTemplate = (NLTemplate&)obj;
		else if (type == N_TMPL_NETMPL)	neTemplate = (NETemplate&)obj;
		else if (type == N_TMPL_NSTMPL)	nsTemplate = (NSTemplate&)obj;
		else if (type == N_TMPL_NFREC) nfRecord = (NFRecord&)obj;
		else if (type == N_TMPL_NLREC) nlRecord = (NLRecord&)obj;
		else if (type == N_TMPL_NEREC) neRecord = (NERecord&)obj;
		else if (type == N_TMPL_NSREC) nsRecord = (NSRecord&)obj;

		if (!nfTemplate.IsNull())
		{
			if (!nfTemplate.IsPalm())
			{
				m_template.SetFingers(NULL);
			}
			else
			{
				m_template.SetPalms(NULL);
			}
		}
		if (!nlTemplate.IsNull())
		{
			m_template.SetFaces(NULL);
		}
		if (!neTemplate.IsNull())
		{
			m_template.SetIrises(NULL);
		}
		if (!nsTemplate.IsNull())
		{
			m_template.SetVoices(NULL);
		}
		if (!nfRecord.IsNull())
		{
			if (nfRecord.GetImpressionType() >= nfitLiveScanPalm)
			{
				m_template.GetPalms().GetRecords().RemoveAt(currentItemId);
			}
			else
			{
				m_template.GetFingers().GetRecords().RemoveAt(currentItemId);
			}
		}
		if (!nlRecord.IsNull())
		{
			m_template.GetFaces().GetRecords().RemoveAt(currentItemId);
		}
		if (!neRecord.IsNull())
		{
			m_template.GetIrises().GetRecords().RemoveAt(currentItemId);
		}
		if (!nsRecord.IsNull())
		{
			m_template.GetVoices().GetRecords().RemoveAt(currentItemId);
		}
	}

	void TemplateSampleController::SaveItem(wxWindow* win, NObject obj, int currentItemId)
	{
		wxString type = obj.GetNativeType().GetName();
		NTemplate nTemplate = NULL;
		NFTemplate nfTemplate = NULL;
		NLTemplate nlTemplate = NULL;
		NETemplate neTemplate = NULL;
		NSTemplate nsTemplate = NULL;
		NFRecord nfRecord = NULL;
		NLRecord nlRecord = NULL;
		NERecord neRecord = NULL;
		NSRecord nsRecord = NULL;

		if (type == N_TMPL_NTMPL) nTemplate = (NTemplate&)obj;
		else if (type == N_TMPL_NFTMPL)	nfTemplate = (NFTemplate&)obj;
		else if (type == N_TMPL_NFTMPL)	nlTemplate = (NLTemplate&)obj;
		else if (type == N_TMPL_NETMPL)	neTemplate = (NETemplate&)obj;
		else if (type == N_TMPL_NSTMPL)	nsTemplate = (NSTemplate&)obj;
		else if (type == N_TMPL_NFREC) nfRecord = (NFRecord&)obj;
		else if (type == N_TMPL_NLREC) nlRecord = (NLRecord&)obj;
		else if (type == N_TMPL_NEREC) neRecord = (NERecord&)obj;
		else if (type == N_TMPL_NSREC) nsRecord = (NSRecord&)obj;

		if (!nfTemplate.IsNull())
		{
			if (!nfTemplate.IsPalm())
			{
				SaveBuffer(win, N_TMPL_FTEMPLATE_DLG_MASK, m_template.GetFingers().Save());
			}
			else
			{
				SaveBuffer(win, N_TMPL_FTEMPLATE_DLG_MASK, m_template.GetPalms().Save());
			}
		}

		if (!nlTemplate.IsNull())
		{
			SaveBuffer(win, N_TMPL_FTEMPLATE_DLG_MASK, m_template.GetFaces().Save());
		}
		if (!neTemplate.IsNull())
		{
			SaveBuffer(win, N_TMPL_FTEMPLATE_DLG_MASK, m_template.GetIrises().Save());
		}

		if (!nsTemplate.IsNull())
		{
			SaveBuffer(win, N_TMPL_FTEMPLATE_DLG_MASK, m_template.GetVoices().Save());
		}

		if (!nfRecord.IsNull())
		{
			if (nfRecord.GetImpressionType() >= nfitLiveScanPalm)
			{
				SaveBuffer(win, N_TMPL_FRECORD_DLG_MASK, m_template.GetPalms().GetRecords().Get(currentItemId).Save());
			}
			else
			{
				SaveBuffer(win, N_TMPL_FRECORD_DLG_MASK, m_template.GetFingers().GetRecords().Get(currentItemId).Save());
			}
		}

		if (!nlRecord.IsNull())
		{
			SaveBuffer(win, N_TMPL_FRECORD_DLG_MASK, m_template.GetFaces().GetRecords().Get(currentItemId).Save());
		}

		if (!neRecord.IsNull())
		{
			SaveBuffer(win, N_TMPL_FRECORD_DLG_MASK, m_template.GetIrises().GetRecords().Get(currentItemId).Save());
		}

		if (!nsRecord.IsNull())
		{
			SaveBuffer(win, N_TMPL_FRECORD_DLG_MASK, m_template.GetVoices().GetRecords().Get(currentItemId).Save());
		}

		if (!nTemplate.IsNull())
		{
			SaveBuffer(win, N_TMPL_TEMPLATE_DLG_MASK, m_template.Save());
		}
	}

	void TemplateSampleController::SaveBuffer(wxWindow* win, wxString dialogMask, NBuffer buff)
	{
		wxFileDialog dialog(win, wxT("Save As"), wxEmptyString, wxEmptyString, dialogMask, wxFD_SAVE | wxFD_OVERWRITE_PROMPT, wxDefaultPosition);
		if (dialog.ShowModal() == wxID_OK)
		{
			wxFileOutputStream output_stream(dialog.GetPath());

			if (output_stream.IsOk()) output_stream.Write(buff.GetPtr(), buff.GetSize());
		}
	}

	void TemplateSampleController::OpenBuffer(wxWindow* win, wxString dialogMask, wxString type)
	{
		wxFileDialog dialog(win, wxT("Open"), wxEmptyString, wxEmptyString, dialogMask, wxFD_OPEN, wxDefaultPosition);

		if (dialog.ShowModal() == wxID_OK)
		{
			wxFileInputStream input_stream(dialog.GetPath());

			if (input_stream.IsOk())
			{
				try
				{
					NBuffer buff = NBuffer(input_stream.GetLength());
					input_stream.Read(buff.GetPtr(), buff.GetSize());

					if (type == N_TMPL_NTMPL)
					{
						m_template = NTemplate(buff);
						wxFileName filename(dialog.GetFilename());
						m_fileName = filename.GetName();
					}
					else if (type == N_TMPL_NFTMPL)	m_template.SetFingers(NFTemplate(buff));
					else if (type == wxT("NFTemplate_Palm")) m_template.SetPalms(NFTemplate(buff));
					else if (type == N_TMPL_NLTMPL) m_template.SetFaces(NLTemplate(buff));
					else if (type == N_TMPL_NETMPL)	m_template.SetIrises(NETemplate(buff));
					else if (type == N_TMPL_NSTMPL)	m_template.SetVoices(NSTemplate(buff));
					else if (type == N_TMPL_NFREC) m_template.GetFingers().GetRecords().Add(NFRecord(buff));
					else if (type == wxT("NFRecord_Palm")) m_template.GetPalms().GetRecords().Add(NFRecord(buff));
					else if (type == N_TMPL_NLREC) m_template.GetFaces().GetRecords().Add(NLRecord(buff));
					else if (type == N_TMPL_NEREC) m_template.GetIrises().GetRecords().Add(NERecord(buff));
					else if (type == N_TMPL_NSREC) m_template.GetVoices().GetRecords().Add(NSRecord(buff));
				}
				catch (NError& ex)
				{
					wxExceptionDlg::Show(ex);
				}
			}
		}

		dialog.Destroy();
	}

	void TemplateSampleController::UpdateMenu(wxMenuBar* menuBar, Neurotec::Biometrics::NTemplate nTemplate)
	{
		menuBar->Enable(ID_MNU_ADD_FINGER, false);
		menuBar->Enable(ID_MNU_ADD_FACE, false);
		menuBar->Enable(ID_MNU_ADD_IRISE, false);
		menuBar->Enable(ID_MNU_ADD_PALM, false);
		menuBar->Enable(ID_MNU_ADD_VOICE, false);

		menuBar->Enable(ID_MNU_ADD_FINGER_FROM_FILE, false);
		menuBar->Enable(ID_MNU_ADD_FACE_FROM_FILE, false);
		menuBar->Enable(ID_MNU_ADD_IRISE_FROM_FILE, false);
		menuBar->Enable(ID_MNU_ADD_PALM_FROM_FILE, false);
		menuBar->Enable(ID_MNU_ADD_VOICE_FROM_FILE, false);

		menuBar->Enable(ID_MNU_ADD_FINGERS, true);
		menuBar->Enable(ID_MNU_ADD_FACES, true);
		menuBar->Enable(ID_MNU_ADD_IRISES, true);
		menuBar->Enable(ID_MNU_ADD_PALMS, true);
		menuBar->Enable(ID_MNU_ADD_VOICES, true);

		menuBar->Enable(ID_MNU_ADD_FINGERS_FROM_FILE, true);
		menuBar->Enable(ID_MNU_ADD_FACES_FROM_FILE, true);
		menuBar->Enable(ID_MNU_ADD_IRISES_FROM_FILE, true);
		menuBar->Enable(ID_MNU_ADD_PALMS_FROM_FILE, true);
		menuBar->Enable(ID_MNU_ADD_VOICES_FROM_FILE, true);

		menuBar->Enable(ID_MNU_REMOVE, false);
		menuBar->Enable(ID_MNU_SAVE_ITEM, false);

		if (!nTemplate.GetFingers().IsNull())
		{
			menuBar->Enable(ID_MNU_ADD_FINGERS, false);
			menuBar->Enable(ID_MNU_ADD_FINGERS_FROM_FILE, false);
			menuBar->Enable(ID_MNU_ADD_FINGER, true);
			menuBar->Enable(ID_MNU_ADD_FINGER_FROM_FILE, true);
			menuBar->Enable(ID_MNU_REMOVE, true);
			menuBar->Enable(ID_MNU_SAVE_ITEM, true);
		}
		else
		{
			menuBar->Enable(ID_MNU_ADD_FINGERS, true);
			menuBar->Enable(ID_MNU_ADD_FINGERS_FROM_FILE, true);
		}
		if (!nTemplate.GetPalms().IsNull())
		{
			menuBar->Enable(ID_MNU_ADD_PALMS, false);
			menuBar->Enable(ID_MNU_ADD_PALMS_FROM_FILE, false);
			menuBar->Enable(ID_MNU_ADD_PALM, true);
			menuBar->Enable(ID_MNU_ADD_PALM_FROM_FILE, true);
			menuBar->Enable(ID_MNU_REMOVE, true);
			menuBar->Enable(ID_MNU_SAVE_ITEM, true);
		}
		else
		{
			menuBar->Enable(ID_MNU_ADD_PALMS, true);
			menuBar->Enable(ID_MNU_ADD_PALMS_FROM_FILE, true);
		}
		if (!nTemplate.GetFaces().IsNull())
		{
			menuBar->Enable(ID_MNU_ADD_FACES, false);
			menuBar->Enable(ID_MNU_ADD_FACES_FROM_FILE, false);
			menuBar->Enable(ID_MNU_ADD_FACE, true);
			menuBar->Enable(ID_MNU_ADD_FACE_FROM_FILE, true);
			menuBar->Enable(ID_MNU_REMOVE, true);
			menuBar->Enable(ID_MNU_SAVE_ITEM, true);
		}
		else
		{
			menuBar->Enable(ID_MNU_ADD_FACES, true);
			menuBar->Enable(ID_MNU_ADD_FACES_FROM_FILE, true);
		}
		if (!nTemplate.GetIrises().IsNull())
		{
			menuBar->Enable(ID_MNU_ADD_IRISES, false);
			menuBar->Enable(ID_MNU_ADD_IRISES_FROM_FILE, false);
			menuBar->Enable(ID_MNU_ADD_IRISE, true);
			menuBar->Enable(ID_MNU_ADD_IRISE_FROM_FILE, true);
			menuBar->Enable(ID_MNU_REMOVE, true);
			menuBar->Enable(ID_MNU_SAVE_ITEM, true);
		}
		else
		{
			menuBar->Enable(ID_MNU_ADD_IRISES, true);
			menuBar->Enable(ID_MNU_ADD_IRISES_FROM_FILE, true);
		}
		if (!nTemplate.GetVoices().IsNull())
		{
			menuBar->Enable(ID_MNU_ADD_VOICES, false);
			menuBar->Enable(ID_MNU_ADD_VOICES_FROM_FILE, false);
			menuBar->Enable(ID_MNU_ADD_VOICE, true);
			menuBar->Enable(ID_MNU_ADD_VOICE_FROM_FILE, true);
			menuBar->Enable(ID_MNU_REMOVE, true);
			menuBar->Enable(ID_MNU_SAVE_ITEM, true);
		}
		else
		{
			menuBar->Enable(ID_MNU_ADD_VOICES, true);
			menuBar->Enable(ID_MNU_ADD_VOICES_FROM_FILE, true);
		}
	}

	wxString TemplateSampleController::GetFileName()
	{
		return m_fileName;
	}

	void TemplateSampleController::SetFileName(wxString fileName)
	{
		m_fileName = fileName;
	}

	NTemplate TemplateSampleController::GetTemplate()
	{
		return m_template;
	}

	void TemplateSampleController::SetTemplate(NTemplate nTempltate)
	{
		m_template = nTempltate;
	}
}}}

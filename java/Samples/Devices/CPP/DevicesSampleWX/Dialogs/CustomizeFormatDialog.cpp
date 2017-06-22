#include "Precompiled.h"
#include <Dialogs/CustomizeFormatDialog.h>
#include <CustomProperty.h>

using namespace Neurotec::Media;
using namespace Neurotec::Gui;
using namespace Neurotec::ComponentModel;

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			CustomizeFormatDialog::CustomizeFormatDialog(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize &size, long style) :
				wxDialog(parent, id, title, position, size, style), m_mediaFormat(NULL)
			{
				CreateGUIControls();
			}

			CustomizeFormatDialog::~CustomizeFormatDialog()
			{
			}

			void CustomizeFormatDialog::CreateGUIControls()
			{
				wxBoxSizer *boxSizerMainBox = new wxBoxSizer(wxVERTICAL);

				m_pgman = new wxPropertyGridManager(this, ID_FORMAT_GRID, wxDefaultPosition, wxSize(350, 250),
					wxPG_BOLD_MODIFIED | wxPG_SPLITTER_AUTO_CENTER | wxPGMAN_DEFAULT_STYLE);
				m_pgman->AddPage();
				boxSizerMainBox->Add(m_pgman, 1, wxEXPAND);

				wxStdDialogButtonSizer *buttonSizer = new wxStdDialogButtonSizer();
				wxButton *okButton = new wxButton(this, wxID_OK);
				wxButton *cancelButton = new wxButton(this, wxID_CANCEL);

				buttonSizer->AddButton(okButton);
				buttonSizer->AddButton(cancelButton);
				buttonSizer->Realize();

				boxSizerMainBox->Add(buttonSizer, 0, wxALIGN_RIGHT | wxALIGN_CENTRE_VERTICAL | wxALL, 5);
				SetSizer(boxSizerMainBox);

				SetSize(wxSize(300, 300));
				SetTitle("Customize Format");
				Centre();
			}

			NMediaFormat CustomizeFormatDialog::CustomizeFormat(const NMediaFormat &mediaFormat)
			{
				if (mediaFormat.IsNull())
					NThrowArgumentNullException("mediaFormat");

				m_mediaFormat = mediaFormat.Clone<NMediaFormat>();
				UpdatePropertyGrid(m_mediaFormat);

				if (ShowModal() == wxID_OK)
					return m_mediaFormat;
				else
					return NULL;
			}

			void CustomizeFormatDialog::UpdatePropertyGrid(const NMediaFormat &mediaFormat)
			{
				NArrayWrapper<NPropertyDescriptor> objectProperties = NTypeDescriptor::GetProperties(mediaFormat);
				for (NArrayWrapper<NPropertyDescriptor>::iterator it = objectProperties.begin(); it != objectProperties.end(); it++)
				{
					wxPGProperty *property = m_pgman->GetPage(0)->Append(new CustomProperty(*it, mediaFormat));
					if (!property->IsTextEditable())
					{
						m_pgman->SetPropertyTextColour(property, wxSystemSettings::GetColour(wxSYS_COLOUR_GRAYTEXT));
					}
				}
			}
		}
	}
}

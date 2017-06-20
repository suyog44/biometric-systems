#ifndef CUSTOMIZE_FORMAT_DIALOG_H_INCLUDED
#define CUSTOMIZE_FORMAT_DIALOG_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		namespace Dialogs
		{
			class CustomizeFormatDialog : public wxDialog
			{
			public:
				CustomizeFormatDialog(wxWindow *parent, const wxWindowID id = 1, const wxString &title = wxEmptyString, const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = wxDEFAULT_DIALOG_STYLE | wxRESIZE_BORDER);
				virtual ~CustomizeFormatDialog();
				Neurotec::Media::NMediaFormat CustomizeFormat(const Neurotec::Media::NMediaFormat &format);

			private:
				void CreateGUIControls();
				void UpdatePropertyGrid(const Neurotec::Media::NMediaFormat &mediaFormat);

				enum
				{
					ID_FORMAT_GRID,
				};

				wxPropertyGridManager *m_pgman;
				Neurotec::Media::NMediaFormat m_mediaFormat;
			};
		}
	}
}

#endif

#ifndef EXTRACTION_SOURCE_H_INCLUDED
#define EXTRACTION_SOURCE_H_INCLUDED

namespace Neurotec { namespace Samples
{

class ExtractionSource : public wxClientData
{
protected:
	int m_sourceType;
	int m_selectedMode;

public:
	class DirTraverser : public wxDirTraverser
	{
	public:
		DirTraverser(wxArrayString *files) { m_files = files; }
		~DirTraverser() { }

		wxDirTraverseResult OnFile(const wxString &filename)
		{
			m_files->Add(filename);
			return wxDIR_CONTINUE;
		}
		wxDirTraverseResult OnDir(const wxString &/*dirname*/)
		{
			return wxDIR_CONTINUE;
		}
	private:
		wxArrayString *m_files;
	};

	enum {
		sourceTypeFile,
		sourceTypeDirectory,
		sourceTypeIrisScanner
	};

	static wxString GetModeAsString(int mode);
	static int GetModesCount();

	ExtractionSource(int sourceType);

	void SetSelectedMode(int mode);
	int GetSelectedMode();
	int GetSourceType();
};

}}

#endif // EXTRACTION_SOURCE_H_INCLUDED

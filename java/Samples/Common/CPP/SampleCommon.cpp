#include "SampleCommon.h"

namespace Neurotec { namespace Samples { namespace Common
{

NString GetOpenFileFilter()
{
	::Neurotec::Text::NStringBuilder fileFilter;

	::Neurotec::Images::NImageFormat::ImageFormatCollection formats = ::Neurotec::Images::NImageFormat::GetFormats();
	for (int i = 0; i < formats.GetCount(); i++)
	{
		if (formats[i].CanRead())
		{
			if (fileFilter.GetLength() != 0)
			{
				fileFilter.AppendChar(N_T(';'));
			}
			fileFilter.Append(formats[i].GetFileFilter());
		}
	}

	return fileFilter.DetachStringN();
}

NString GetSaveFileFilter()
{
	::Neurotec::Text::NStringBuilder fileFilter;

	::Neurotec::Images::NImageFormat::ImageFormatCollection formats = ::Neurotec::Images::NImageFormat::GetFormats();
	for (int i = 0; i < formats.GetCount(); i++)
	{
		if (formats[i].CanWrite())
		{
			if (fileFilter.GetLength() != 0)
			{
				fileFilter.AppendChar(N_T(';'));
			}
			fileFilter.Append(formats[i].GetFileFilter());
		}
	}

	return fileFilter.DetachStringN();
}

NString GetOpenFileFilterString(bool addAllImage, bool addAllFiles)
{
	::Neurotec::Text::NStringBuilder fileFilter;

	NString fileFormat = GetOpenFileFilter();

	if (addAllImage)
	{
		fileFilter.Append(N_T("All Image Files ("));
		fileFilter.Append(fileFormat);
		fileFilter.Append(N_T(")|"));
		fileFilter.Append(fileFormat);
	}
	::Neurotec::Images::NImageFormat::ImageFormatCollection formats = ::Neurotec::Images::NImageFormat::GetFormats();
	for (int i = 0; i < formats.GetCount(); i++)
	{
		if (formats[i].CanRead())
		{
			NString imageFormatFilter = formats[i].GetFileFilter();
			if (fileFilter.GetLength() != 0)
			{
				fileFilter.AppendChar(N_T('|'));
			}

			fileFilter.Append(formats[i].GetName());
			fileFilter.Append(N_T(" Files ("));
			fileFilter.Append(imageFormatFilter);
			fileFilter.Append(N_T(")|"));
			fileFilter.Append(imageFormatFilter);
		}
	}
	if (addAllFiles)
	{
		fileFilter.Append(N_T("|All Files (*.*)|*.*"));
	}
	return fileFilter.DetachStringN();
}

NString GetSaveFileFilterString()
{
	::Neurotec::Text::NStringBuilder fileFilter;

	::Neurotec::Images::NImageFormat::ImageFormatCollection formats = ::Neurotec::Images::NImageFormat::GetFormats();
	for (int i = 0; i < formats.GetCount(); i++)
	{
		if (formats[i].CanWrite())
		{
			NString imageFormatFilter = formats[i].GetFileFilter();
			if (fileFilter.GetLength() != 0)
			{
				fileFilter.AppendChar(N_T('|'));
			}

			fileFilter.Append(formats[i].GetName());
			fileFilter.Append(N_T(" Files ("));
			fileFilter.Append(imageFormatFilter);
			fileFilter.Append(N_T(")|"));
			fileFilter.Append(imageFormatFilter);
		}
	}
	return fileFilter.DetachStringN();
}

}}}

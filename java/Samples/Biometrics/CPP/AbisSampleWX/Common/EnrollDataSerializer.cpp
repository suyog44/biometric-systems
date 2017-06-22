#include "Precompiled.h"
#include <Common/EnrollDataSerializer.h>
#include <Common/SubjectUtils.h>

using namespace Neurotec;
using namespace Neurotec::IO;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Images;
using namespace Neurotec::Sound;
using namespace Neurotec::Text;

namespace Neurotec { namespace Samples
{

NVersion EnrollDataSerializer::Version = NVersion(1, 0);
wxString EnrollDataSerializer::EnrollDataHeader = wxT("NeurotechnologySampleData");

void EnrollDataSerializer::WriteImage(NBinaryWriter & writer, const NImage & image, const NImageFormat & format)
{
	NBuffer buffer = NULL;
	if (!image.IsNull())
		buffer = image.Save(format);
	WriteBuffer(writer, buffer);
}

void EnrollDataSerializer::WriteBuffer(NBinaryWriter & writer, const NBuffer & buffer)
{
	NSizeType size = 0;
	if (!buffer.IsNull())
		size = buffer.GetSize();
	writer.Write((NInt)size);
	if (size != 0)
		writer.Write(buffer);
}

void EnrollDataSerializer::WriteHeaderAndVersion(NBinaryWriter & writer)
{
	NByte bytes[25];
	const NSizeType bytesLength = 25;
	NSizeType actualSize = NEncodingEx::GetBytes(neAscii, EnrollDataSerializer::EnrollDataHeader.c_str(), bytes, bytesLength);
	writer.Write(bytes, actualSize);

	NInt major = EnrollDataSerializer::Version.GetMajor();
	NInt minor = EnrollDataSerializer::Version.GetMinor();
	NUShort version = ((NByte)major) << 8 | (NByte)minor;
	writer.Write(version);
}

bool EnrollDataSerializer::CheckHeaderAndVersion(NBinaryReader & reader)
{
	const NSizeType bytesLength = 25;
	NBuffer buffer = reader.ReadBytes(bytesLength);
	wxString header = NEncodingEx::GetString(neAscii, buffer.GetPtr(), bytesLength);
	if (header != EnrollDataSerializer::EnrollDataHeader) return false;

	NUShort val = reader.ReadUInt16();
	NVersion ver = NVersion((NInt)(val >> 8), (NInt)(val & 0xFF));
	if (ver > EnrollDataSerializer::Version) return false;

	return true;
}

NBuffer EnrollDataSerializer::Serialize(const NSubject & subject, bool useWsqForFingers)
{
	NMemoryStream stream;
	NBinaryWriter writer(stream, nboLittleEndian);

	WriteHeaderAndVersion(writer);

	std::vector<NFinger> fingers = SubjectUtils::GetTemplateCompositeFingers(subject);
	writer.Write((NInt)fingers.size());
	for (std::vector<NFinger>::iterator it = fingers.begin(); it != fingers.end(); it++)
	{
		WriteImage(writer, it->GetImage(), useWsqForFingers ? NImageFormat::GetWsq() : NImageFormat::GetPng());
	}

	std::vector<NFace> faces = SubjectUtils::GetTemplateCompositeFaces(subject);
	writer.Write((NInt)faces.size());
	for (std::vector<NFace>::iterator it = faces.begin(); it != faces.end(); it++)
	{
		NLAttributes attributes = it->GetObjects()[0];
		NLTemplate tmpl = attributes.GetTemplate();
		writer.Write((NInt)tmpl.GetRecords().GetCount());
		WriteImage(writer, it->GetImage(), NImageFormat::GetPng());
	}

	std::vector<NIris> irises = SubjectUtils::GetTemplateCompositeIrises(subject);
	writer.Write((NInt)irises.size());
	for (std::vector<NIris>::iterator it = irises.begin(); it != irises.end(); it++)
	{
		WriteImage(writer, it->GetImage(), NImageFormat::GetPng());
	}

	std::vector<NPalm> palms = SubjectUtils::GetTemplateCompositePalms(subject);
	writer.Write((NInt)palms.size());
	for (std::vector<NPalm>::iterator it = palms.begin(); it != palms.end(); it++)
	{
		WriteImage(writer, it->GetImage(), NImageFormat::GetPng());
	}

	std::vector<NVoice> voices = SubjectUtils::GetTemplateCompositeVoices(subject);
	writer.Write((NInt)voices.size());
	for (std::vector<NVoice>::iterator it = voices.begin(); it != voices.end(); it++)
	{
		NSoundBuffer soundBuffer = it->GetSoundBuffer();
		WriteBuffer(writer, soundBuffer.IsNull() ? NBuffer::GetEmpty() : soundBuffer.Save());
	}

	writer.Write(0);

	return stream.GetBuffer();
}

NSubject EnrollDataSerializer::Deserialize(const NBuffer & templateData, const NBuffer & serializeData, wxArrayInt & faceRecordCounts)
{
	faceRecordCounts.Clear();

	NTemplate tmpl(templateData);
	NMemoryStream stream(serializeData);
	NBinaryReader reader(stream, nboLittleEndian);

	NSubject subject;
	NInt length;
	NInt bufferSize;
	NBuffer bytes = NULL;
	NInt recordIndex = 0;

	if (!CheckHeaderAndVersion(reader)) NThrowFormatException();

	// fingers
	length = reader.ReadInt32();
	for (int i = 0; i < length; i++)
	{
		bufferSize = reader.ReadInt32();
		bytes = reader.ReadBytes(bufferSize);
		NImage image = bufferSize > 0 ? NImage::FromMemory(bytes) : NULL;
		NFrictionRidge finger = NFinger::FromImageAndTemplate(image, tmpl.GetFingers().GetRecords()[i]);
		subject.GetFingers().Add(NObjectDynamicCast<NFinger>(finger));
	}

	// faces
	length = reader.ReadInt32();
	for (int i = 0; i < length; i++)
	{
		NInt recordCount = reader.ReadInt32();
		faceRecordCounts.Add(recordCount);

		bufferSize = reader.ReadInt32();
		bytes = reader.ReadBytes(bufferSize);
		NImage image = bufferSize > 0 ? NImage::FromMemory(bytes) : NULL;
		NLTemplate actualTemplate;
		NLAttributes attributes;
		for (int j = 0; j < recordCount; j++)
		{
			actualTemplate.GetRecords().Add(tmpl.GetFaces().GetRecords()[recordIndex++]);
		}
		attributes.SetTemplate(actualTemplate);
		NFace face = NFace::FromImageAndAttributes(image, attributes);
		subject.GetFaces().Add(face);
	}

	// irises
	length = reader.ReadInt32();
	for (int i = 0; i < length; i++)
	{
		bufferSize = reader.ReadInt32();
		bytes = reader.ReadBytes(bufferSize);
		NImage image = bufferSize > 0 ? NImage::FromMemory(bytes) : NULL;
		NIris iris = NIris::FromImageAndTemplate(image, tmpl.GetIrises().GetRecords()[i]);
		subject.GetIrises().Add(iris);
	}

	// palms
	length = reader.ReadInt32();
	for (int i = 0; i < length; i++)
	{
		bufferSize = reader.ReadInt32();
		bytes = reader.ReadBytes(bufferSize);
		NImage image = bufferSize > 0 ? NImage::FromMemory(bytes) : NULL;
		NFrictionRidge palm = NPalm::FromImageAndTemplate(image, tmpl.GetPalms().GetRecords()[i]);
		subject.GetPalms().Add(NObjectDynamicCast<NPalm>(palm));
	}

	// voices
	length = reader.ReadInt32();
	for (int i = 0; i < length; i++)
	{
		bufferSize = reader.ReadInt32();
		bytes = reader.ReadBytes(bufferSize);
		NSoundBuffer soundBuffer = bufferSize > 0 ? NSoundBuffer::FromMemory(bytes) : NULL;
		NVoice voice = NVoice::FromSoundBufferAndTemplate(soundBuffer, tmpl.GetVoices().GetRecords()[i]);
		subject.GetVoices().Add(voice);
	}

	return subject;
}

NSubject EnrollDataSerializer::Deserialize(const NBuffer & templateData, const NBuffer & serializeData)
{
	wxArrayInt counts;
	return Deserialize(templateData, serializeData, counts);
}

}}


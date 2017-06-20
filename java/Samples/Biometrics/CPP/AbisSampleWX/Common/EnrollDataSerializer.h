#ifndef ENROLL_DATA_SERIALIZER_H_INCLUDED
#define ENROLL_DATA_SERIALIZER_H_INCLUDED

namespace Neurotec { namespace Samples
{

class EnrollDataSerializer
{
private:
	EnrollDataSerializer()
	{
	}

	static ::Neurotec::NVersion Version;
	static wxString EnrollDataHeader;

	static void WriteImage(::Neurotec::IO::NBinaryWriter & writer, const ::Neurotec::Images::NImage & image, const ::Neurotec::Images::NImageFormat & format);
	static void WriteBuffer(::Neurotec::IO::NBinaryWriter & writer, const ::Neurotec::IO::NBuffer & buffer);
	static void WriteHeaderAndVersion(::Neurotec::IO::NBinaryWriter & writer);
	static bool CheckHeaderAndVersion(::Neurotec::IO::NBinaryReader & reader);

public:
	static ::Neurotec::IO::NBuffer Serialize(const ::Neurotec::Biometrics::NSubject & subject, bool useWsqForFingers = false);
	static ::Neurotec::Biometrics::NSubject Deserialize(const ::Neurotec::IO::NBuffer & templateData, const ::Neurotec::IO::NBuffer & serializeData, wxArrayInt & faceRecordCounts);
	static ::Neurotec::Biometrics::NSubject Deserialize(const ::Neurotec::IO::NBuffer & templateData, const ::Neurotec::IO::NBuffer & serializeData);
};

}}

#endif

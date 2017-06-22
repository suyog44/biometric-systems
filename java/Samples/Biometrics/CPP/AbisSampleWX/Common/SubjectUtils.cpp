#include <Precompiled.h>
#include <Common/SubjectUtils.h>

using namespace ::Neurotec;
using namespace ::Neurotec::Biometrics;

namespace Neurotec { namespace Samples
{

bool SubjectUtils::IsBiometricGeneralizationResult(NBiometric & biometric)
{
	if (biometric.GetSessionId() == -1)
	{
		NBiometricAttributes parentObject = biometric.GetParentObject();
		if (!parentObject.IsNull())
		{
			NBiometric owner = parentObject.GetOwner<NBiometric>();
			return !owner.IsNull() && owner.GetSessionId() != -1;
		}
	}
	return false;
}

bool SubjectUtils::IsBiometricGeneralizationSource(NBiometric & biometric)
{
	return biometric.GetSessionId() != -1;
}

std::vector<FacesGeneralizationGroup> SubjectUtils::GetFaceGeneralizationGroups(NArrayWrapper<NFace> & allFaces)
{
	std::vector<FacesGeneralizationGroup> result;
	std::vector<NInt> ids;
	for (NArrayWrapper<NFace>::iterator it = allFaces.begin(); it != allFaces.end(); it++)
	{
		NInt sessionId = it->GetSessionId();
		if (sessionId == -1 && it->GetParentObject().IsNull())
		{
			FacesGeneralizationGroup r;
			r.push_back(*it);
			result.push_back(r);
		}

		if (sessionId != -1)
		{
			std::vector<int>::iterator f = std::find(ids.begin(), ids.end(), sessionId);
			if (f == ids.end())
				ids.push_back(sessionId);
		}
	}

	for (std::vector<NInt>::iterator it = ids.begin(); it != ids.end(); it++)
	{
		FacesGeneralizationGroup withParent;
		FacesGeneralizationGroup withoutParent;
		for (NArrayWrapper<NFace>::iterator fit = allFaces.begin(); fit != allFaces.end(); fit++)
		{
			if (fit->GetSessionId() == *it)
			{
				if (fit->GetParentObject().IsNull())
					withoutParent.push_back(*fit);
				else
					withParent.push_back(*fit);
			}
		}
		if (!withParent.empty())
			result.push_back(withParent);
		else
			result.push_back(withoutParent);
	}
	return result;
}

FacesGeneralizationGroup SubjectUtils::GetFacesInSameGroup(NArrayWrapper<NFace> & faces, NFace & face)
{
	FacesGeneralizationGroup result;
	if (IsBiometricGeneralizationSource(face))
	{
		NInt sessionId = face.GetSessionId();
		bool hasParent = !face.GetParentObject().IsNull();
		for (NArrayWrapper<NFace>::iterator it = faces.begin(); it != faces.end(); it++)
		{
			if (it->GetSessionId() == sessionId && (it->GetParentObject().IsNull() == false) == hasParent) result.push_back(*it);
		}

		if (!result.empty())
		{
			NLAttributes attributes = NULL;
			if (face.GetObjects().GetCount() > 0)
			{
				attributes = face.GetObjects()[0];
				NObject childObject = attributes.GetChild();
				if (!childObject.IsNull())
				{
					NFace child = NObjectDynamicCast<NFace>(childObject);
					if (child.GetSessionId() == -1)
					{
						result.push_back(child);
					}
				}
			}
			return result;
		}
	}
	else if (IsBiometricGeneralizationResult(face))
	{
		NBiometricAttributes parentObject = face.GetParentObject();
		if (!parentObject.IsNull())
		{
			NFace owner = parentObject.GetOwner<NFace>();
			return GetFacesInSameGroup(faces, owner);
		}
	}

	result.push_back(face);
	return result;
}

std::vector<NFinger> SubjectUtils::GetTemplateCompositeFingers(const NSubject & subject)
{
	std::vector<NFinger> result;
	NArrayWrapper<NFinger> allFingers = subject.GetFingers().GetAll();
	for (NArrayWrapper<NFinger>::iterator it = allFingers.begin(); it != allFingers.end(); it++)
	{
		if (it->GetSessionId() != -1) continue;
		NArrayWrapper<NFAttributes> attributes = it->GetObjects().GetAll();
		if (attributes.GetCount() == 1 && !attributes[0].GetTemplate().IsNull())
		{
			result.push_back(*it);
		}
	}
	return result;
}

std::vector<NFace> SubjectUtils::GetTemplateCompositeFaces(const NSubject & subject)
{
	std::vector<NFace> result;
	NArrayWrapper<NFace> allFaces = subject.GetFaces().GetAll();
	for (NArrayWrapper<NFace>::iterator it = allFaces.begin(); it != allFaces.end(); it++)
	{
		if (it->GetSessionId() != -1) continue;
		NArrayWrapper<NLAttributes> attributes = it->GetObjects().GetAll();
		if (attributes.GetCount() > 0 && !attributes[0].GetTemplate().IsNull())
		{
			result.push_back(*it);
		}
	}
	return result;
}

std::vector<NIris> SubjectUtils::GetTemplateCompositeIrises(const NSubject & subject)
{
	std::vector<NIris> result;
	NArrayWrapper<NIris> allIrises = subject.GetIrises().GetAll();
	for (NArrayWrapper<NIris>::iterator it = allIrises.begin(); it != allIrises.end(); it++)
	{
		if (it->GetSessionId() != -1) continue;
		NArrayWrapper<NEAttributes> attributes = it->GetObjects().GetAll();
		if (attributes.GetCount() == 1 && !attributes[0].GetTemplate().IsNull())
		{
			result.push_back(*it);
		}
	}
	return result;
}

std::vector<NPalm> SubjectUtils::GetTemplateCompositePalms(const NSubject & subject)
{
	std::vector<NPalm> result;
	NArrayWrapper<NPalm> allPalms = subject.GetPalms().GetAll();
	for (NArrayWrapper<NPalm>::iterator it = allPalms.begin(); it != allPalms.end(); it++)
	{
		if (it->GetSessionId() != -1) continue;
		NArrayWrapper<NFAttributes> attributes = it->GetObjects().GetAll();
		if (attributes.GetCount() == 1 && !attributes[0].GetTemplate().IsNull())
		{
			result.push_back(*it);
		}
	}
	return result;
}

std::vector<NVoice> SubjectUtils::GetTemplateCompositeVoices(const NSubject & subject)
{
	std::vector<NVoice> result;
	NArrayWrapper<NVoice> allVoices = subject.GetVoices().GetAll();
	for (NArrayWrapper<NVoice>::iterator it = allVoices.begin(); it != allVoices.end(); it++)
	{
		if (it->GetSessionId() != -1) continue;
		NArrayWrapper<NSAttributes> attributes = it->GetObjects().GetAll();
		if (attributes.GetCount() == 1 && !attributes[0].GetTemplate().IsNull())
		{
			result.push_back(*it);
		}
	}
	return result;
}

}};

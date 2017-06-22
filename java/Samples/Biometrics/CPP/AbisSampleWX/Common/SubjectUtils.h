#ifndef SUBJECT_UTILS_H_INCLUDED
#define SUBJECT_UTILS_H_INCLUDED

namespace Neurotec { namespace Samples
{

typedef std::vector< ::Neurotec::Biometrics::NFinger> FingersGeneralizationGroup;
typedef std::vector< ::Neurotec::Biometrics::NPalm> PalmsGeneralizationGroup;
typedef std::vector< ::Neurotec::Biometrics::NFace> FacesGeneralizationGroup;

class SubjectUtils
{
private:
	SubjectUtils()
	{
	}
public:
	static bool IsBiometricGeneralizationResult( ::Neurotec::Biometrics::NBiometric & biometric);
	static bool IsBiometricGeneralizationSource( ::Neurotec::Biometrics::NBiometric & biometric);

	static std::vector<FacesGeneralizationGroup> GetFaceGeneralizationGroups(::Neurotec::NArrayWrapper< ::Neurotec::Biometrics::NFace> & allFaces);
	static std::vector<FingersGeneralizationGroup> GetFingersGeneralizationGroups(std::vector< ::Neurotec::Biometrics::NFinger> & allFingers)
	{
		return GetFrictionRidgeGeneralizationGroups< ::Neurotec::Biometrics::NFinger>(allFingers);
	}
	static std::vector<PalmsGeneralizationGroup> GetPalmsGeneralizationGroups(std::vector< ::Neurotec::Biometrics::NPalm> & allPalms)
	{
		return GetFrictionRidgeGeneralizationGroups< ::Neurotec::Biometrics::NPalm>(allPalms);
	}
	static FingersGeneralizationGroup GetFingersInSameGroup(::Neurotec::NArrayWrapper< ::Neurotec::Biometrics::NFinger> & fingers, ::Neurotec::Biometrics::NFinger & finger)
	{
		return GetFrictionRidgesInSameGroup< ::Neurotec::Biometrics::NFinger>(fingers, finger);
	}
	static PalmsGeneralizationGroup GetPalmsInSameGroup(::Neurotec::NArrayWrapper< ::Neurotec::Biometrics::NPalm> & palms, ::Neurotec::Biometrics::NPalm & palm)
	{
		return GetFrictionRidgesInSameGroup< ::Neurotec::Biometrics::NPalm>(palms, palm);
	}
	static FacesGeneralizationGroup GetFacesInSameGroup(::Neurotec::NArrayWrapper< ::Neurotec::Biometrics::NFace> & faces, ::Neurotec::Biometrics::NFace & face);

	static std::vector< ::Neurotec::Biometrics::NFinger> FlattenFingers( std::vector< ::Neurotec::Biometrics::NFinger> & fingers)
	{
		return FlattenFrictionRidges< ::Neurotec::Biometrics::NFinger>(fingers);
	}
	static std::vector< ::Neurotec::Biometrics::NPalm> FlattenPalms( std::vector< ::Neurotec::Biometrics::NPalm> & palms)
	{
		return FlattenFrictionRidges< ::Neurotec::Biometrics::NPalm>(palms);
	}

	static std::vector< ::Neurotec::Biometrics::NFinger> GetTemplateCompositeFingers(const ::Neurotec::Biometrics::NSubject & subject);
	static std::vector< ::Neurotec::Biometrics::NFace> GetTemplateCompositeFaces(const ::Neurotec::Biometrics::NSubject & subject);
	static std::vector< ::Neurotec::Biometrics::NIris> GetTemplateCompositeIrises(const ::Neurotec::Biometrics::NSubject & subject);
	static std::vector< ::Neurotec::Biometrics::NPalm> GetTemplateCompositePalms(const ::Neurotec::Biometrics::NSubject & subject);
	static std::vector< ::Neurotec::Biometrics::NVoice> GetTemplateCompositeVoices(const ::Neurotec::Biometrics::NSubject & subject);
private:
	class Group
	{
	public:
		::Neurotec::Biometrics::NFPosition pos;
		::Neurotec::Biometrics::NFImpressionType impr;
		::Neurotec::NInt id;
	};
private:
	template <typename T>
	static std::vector< std::vector< T> > GetFrictionRidgeGeneralizationGroups(std::vector< T> & allFingers)
	{
		std::vector< std::vector< T> > results;
		std::vector<Group> groups;
		for (typename std::vector< T>::iterator it = allFingers.begin(); it != allFingers.end(); it++)
		{
			NInt sessionId = it->GetSessionId();
			if (sessionId == -1 && it->GetParentObject().IsNull())
			{
				std::vector< T> r;
				r.push_back(*it);
				results.push_back(r);
			}

			Group g;
			g.id = sessionId;
			if (g.id != -1)
			{
				bool found = false;
				g.pos = it->GetPosition();
				g.impr = it->GetImpressionType();
				for (std::vector<Group>::iterator git = groups.begin(); git != groups.end(); git++)
				{
					if (g.id == git->id && g.pos == git->pos && g.impr == git->impr)
					{
						found = true;
						break;
					}
				}
				if (!found) groups.push_back(g);
			}
		}

		for(std::vector<Group>::iterator git = groups.begin(); git != groups.end(); git++)
		{
			std::vector< T> r;
			for (typename std::vector< T>::iterator it = allFingers.begin(); it != allFingers.end(); it++)
			{
				if (it->GetSessionId() == git->id && it->GetPosition() == git->pos && it->GetImpressionType() == git->impr)
				{
					r.push_back(*it);
				}
			}

			if (!r.empty())
			{
				T first = r[0];
				if (first.GetObjects().GetCount() > 0)
				{
					::Neurotec::Biometrics::NFAttributes attributes = first.GetObjects()[0];
					::Neurotec::NObject childObject = attributes.GetChild();
					if (!childObject.IsNull())
					{
						T child = NObjectDynamicCast< T>(childObject);
						if (child.GetSessionId() == -1 && child.GetPosition() == first.GetPosition())
						{
							r.push_back(child);
						}
					}
				}
				results.push_back(r);
			}
		}
		return results;
	}

	template <typename T>
	static std::vector< T> GetFrictionRidgesInSameGroup(::Neurotec::NArrayWrapper< T> & allFingers, T & finger)
	{
		if (IsBiometricGeneralizationSource(finger))
		{
			std::vector< T> result;
			for (typename ::Neurotec::NArrayWrapper< T>::iterator it = allFingers.begin(); it != allFingers.end(); it++)
			{
				if (it->GetPosition() == finger.GetPosition() && it->GetImpressionType() == finger.GetImpressionType() && it->GetSessionId() == finger.GetSessionId())
				{
					result.push_back(*it);
				}
			}

			::Neurotec::Biometrics::NFAttributes attributes = NULL;
			if (finger.GetObjects().GetCount() > 0) attributes = finger.GetObjects()[0];
			if (!attributes.IsNull())
			{
				::Neurotec::NObject childObject = attributes.GetChild();
				if (!childObject.IsNull())
				{
					T child = NObjectDynamicCast< T>(childObject);
					if (child.GetSessionId() == -1)
						result.push_back(child);
				}
			}
			return result;
		}
		else if (IsBiometricGeneralizationResult(finger))
		{
			::Neurotec::Biometrics::NBiometricAttributes parentObject = finger.GetParentObject();
			if (!parentObject.IsNull())
			{
				T owner = parentObject.GetOwner< T>();
				if (!owner.IsNull() && owner.GetSessionId() != -1)
				{
					return GetFrictionRidgesInSameGroup< T>(allFingers, owner);
				}
			}
		}

		std::vector< T> g;
		g.push_back(finger);
		return g;
	}

	template <typename T>
	static std::vector< T> FlattenFrictionRidge(T & finger)
	{
		std::vector< T> result;
		if (!finger.IsNull())
		{
			result.push_back(finger);

			NArrayWrapper< ::Neurotec::Biometrics::NFAttributes> objects = finger.GetObjects().GetAll();
			for (NArrayWrapper< ::Neurotec::Biometrics::NFAttributes>::iterator oit = objects.begin(); oit != objects.end(); oit++)
			{
				::Neurotec::NObject childBiometric = oit->GetChild();
				if (!childBiometric.IsNull())
				{
					T fr = ::Neurotec::NObjectDynamicCast< T>(childBiometric);
					std::vector< T> flattened = FlattenFrictionRidge(fr);
					typename std::vector< T>::iterator f;
					for (typename std::vector< T>::iterator it = flattened.begin(); it != flattened.end(); it++)
					{
						f = std::find(result.begin(), result.end(), *it);
						if (f == result.end())
							result.push_back(*it);
					}
				}
			}
		}
		return result;
	}

	template <typename T>
	static std::vector< T> FlattenFrictionRidges(std::vector< T> & fingers)
	{
		std::vector< T> result;
		for (typename std::vector< T>::iterator it = fingers.begin(); it != fingers.end(); it++)
		{
			std::vector< T> flattened = FlattenFrictionRidge(*it);
			typename std::vector< T>::iterator f;
			for (typename std::vector< T>::iterator it = flattened.begin(); it != flattened.end(); it++)
			{
				f = std::find(result.begin(), result.end(), *it);
				if (f == result.end())
					result.push_back(*it);
			}
		}
		return result;
	}
};

}};

#endif

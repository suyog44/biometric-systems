#ifndef SUBJECT_EDITOR_PAGE_INTERFACE_H_INCLUDED
#define SUBJECT_EDITOR_PAGE_INTERFACE_H_INCLUDED

namespace Neurotec { namespace Samples
{

class SubjectEditorPageInterface
{
public:
	virtual void SelectFirstPage() = 0;

	virtual ~SubjectEditorPageInterface()
	{
	}
};

}}

#endif


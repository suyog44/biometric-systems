#include "Precompiled.h"

#include <Common/SvgShape.h>

namespace Neurotec { namespace Samples
{

SvgShape::SvgShape(wxString strPath)
{
	m_zIndex = 0;

	CreateShape(strPath);
}

SvgShape::~SvgShape()
{
}

void SvgShape::Draw(wxGraphicsContext *gc)
{
	if (gc != NULL)
	{
		gc->DrawPath(m_path);
		gc->Flush();
	}
}

bool SvgShape::Contains(wxDouble x, wxDouble y)
{
	return m_path.Contains(x, y);
}

void SvgShape::SetZIndex(unsigned int value)
{
	m_zIndex = value;
}

unsigned int SvgShape::GetZIndex()
{
	return m_zIndex;
}

bool SvgShape::IsLessThen(SvgShape *shape, SvgShape *shapeCompareTo)
{
	return (shape->m_zIndex < shapeCompareTo->m_zIndex);
}

void SvgShape::CreateShape(wxString strPath)
{
	m_path = wxGraphicsRenderer::GetDefaultRenderer()->CreatePath();

	wxArrayString instructions;

	wxString path = strPath.Trim(false);
	path = strPath.Trim(true);

	wxStringTokenizer tokenizer(path, wxT(", "));
	int tokenCount = tokenizer.CountTokens();

	if (tokenCount < 1)
		return;

	instructions.Alloc(tokenCount);

	for (int i = 0; i < tokenCount; i++)
	{
		instructions.Insert(tokenizer.GetNextToken(), i);
	}

	if (instructions.GetCount() < 1)
		return;

	unsigned int i = 0;
	while (i < instructions.GetCount())
	{
		if (instructions[i] == wxT("c") || instructions[i] == wxT("C"))
		{
			double x1, y1;
			double x2, y2;
			double x3, y3;

			instructions[i + 1].ToDouble(&x1); i++;
			instructions[i + 1].ToDouble(&y1); i++;
			instructions[i + 1].ToDouble(&x2); i++;
			instructions[i + 1].ToDouble(&y2); i++;
			instructions[i + 1].ToDouble(&x3); i++;
			instructions[i + 1].ToDouble(&y3); i++;

			m_path.AddCurveToPoint(x1, y1, x2, y2, x3, y3);
		}
		else if (instructions[i] == wxT("l") || instructions[i] == wxT("L"))
		{
			double x, y;

			instructions[i + 1].ToDouble(&x); i++;
			instructions[i + 1].ToDouble(&y); i++;

			m_path.AddLineToPoint(x, y);
		}
		else if (instructions[i] == wxT("m") || instructions[i] == wxT("M"))
		{
			double x, y;

			instructions[i + 1].ToDouble(&x); i++;
			instructions[i + 1].ToDouble(&y); i++;

			m_path.MoveToPoint(x, y);
		}
		else if (instructions[i] == wxT("z") || instructions[i] == wxT("Z"))
		{
			m_path.CloseSubpath();
		}

		i++;
	}

	instructions.Clear();
}

}}

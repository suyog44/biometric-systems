#ifndef SVG_SHAPE_H_INCLUDED
#define SVG_SHAPE_H_INCLUDED

namespace Neurotec { namespace Samples
{

class SvgShape
{
public:
	SvgShape(wxString strPath);

	virtual ~SvgShape();

	virtual void Draw(wxGraphicsContext *gc);

	bool Contains(wxDouble x, wxDouble y);

	void SetZIndex(unsigned int value);

	unsigned int GetZIndex();

	static bool IsLessThen(SvgShape *shape, SvgShape *shapeCompareTo);

private:
	void CreateShape(wxString strPath);

private:
	wxGraphicsPath m_path;
	unsigned int m_zIndex;
};

}}

#endif

#ifndef IRISES_SAMPLE_WX_VERSION_INFO_H_INCLUDED
#define IRISES_SAMPLE_WX_VERSION_INFO_H_INCLUDED

namespace Neurotec { namespace Samples
{

#ifndef wxT
#define wxT(x) x
#endif

#define IRISES_SAMPLE_WX_PRODUCT_NAME wxT("Irises Identification Technology Sample")
#define IRISES_SAMPLE_WX_INTERNAL_NAME wxT("IrisesSampleWX")
#define IRISES_SAMPLE_WX_TITLE IRISES_SAMPLE_WX_PRODUCT_NAME

#define IRISES_SAMPLE_WX_COMPANY_NAME wxT("Neurotechnology")
#ifdef N_PRODUCT_LIB
#define IRISES_SAMPLE_WX_FILE_NAME IRISES_SAMPLE_WX_INTERNAL_NAME wxT("Lib.exe")
#else
#define IRISES_SAMPLE_WX_FILE_NAME IRISES_SAMPLE_WX_INTERNAL_NAME wxT(".exe")
#endif
#define IRISES_SAMPLE_WX_COPYRIGHT wxT("Copyright (C) 2008-2017 Neurotechnology")
#define IRISES_SAMPLE_WX_VERSION_MAJOR 9
#define IRISES_SAMPLE_WX_VERSION_MINOR 0
#define IRISES_SAMPLE_WX_VERSION_BUILD 0
#define IRISES_SAMPLE_WX_VERSION_REVISION 0
#define IRISES_SAMPLE_WX_VERSION_STRING wxT("9.0.0.0")

}}

#endif // !IRISES_SAMPLE_WX_VERSION_INFO_H_INCLUDED

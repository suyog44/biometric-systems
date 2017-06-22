#ifndef ABIS_SAMPLE_WX_VERSION_INFO_H_INCLUDED
#define ABIS_SAMPLE_WX_VERSION_INFO_H_INCLUDED

namespace Neurotec { namespace Samples
{

#ifndef wxT
#define wxT(x) x
#endif

#define ABIS_SAMPLE_WX_PRODUCT_NAME wxT("Neurotechnology Abis Sample")
#define ABIS_SAMPLE_WX_INTERNAL_NAME wxT("AbisSampleWX")
#define ABIS_SAMPLE_WX_TITLE ABIS_SAMPLE_WX_PRODUCT_NAME

#define ABIS_SAMPLE_WX_COMPANY_NAME wxT("Neurotechnology")
#ifdef N_PRODUCT_LIB
#define ABIS_SAMPLE_WX_FILE_NAME ABIS_SAMPLE_WX_INTERNAL_NAME wxT("Lib.exe")
#else
#define ABIS_SAMPLE_WX_FILE_NAME ABIS_SAMPLE_WX_INTERNAL_NAME wxT(".exe")
#endif
#define ABIS_SAMPLE_WX_COPYRIGHT wxT("Copyright (C) 2014-2017 Neurotechnology")
#define ABIS_SAMPLE_WX_VERSION_MAJOR 6
#define ABIS_SAMPLE_WX_VERSION_MINOR 0
#define ABIS_SAMPLE_WX_VERSION_BUILD 0
#define ABIS_SAMPLE_WX_VERSION_REVISION 0
#define ABIS_SAMPLE_WX_VERSION_STRING wxT("9.0.0.0")

}}

#endif // !ABIS_SAMPLE_WX_VERSION_INFO_H_INCLUDED

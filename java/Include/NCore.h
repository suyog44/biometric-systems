#ifndef N_CORE_LIBRARY_H_INCLUDED
#define N_CORE_LIBRARY_H_INCLUDED

#include <Collections/NCollection.h>
#include <Collections/NDictionary.h>
#include <Collections/NArrayCollection.h>
#include <Collections/NList.h>
#include <Collections/NListList.h>
#include <Collections/NQueue.h>
#include <Collections/NStack.h>
#include <ComponentModel/NDescriptor.h>
#include <ComponentModel/NParameterDescriptor.h>
#include <ComponentModel/NMemberDescriptor.h>
#include <ComponentModel/NMethodDescriptor.h>
#include <ComponentModel/NPropertyDescriptor.h>
#include <ComponentModel/NEventDescriptor.h>
#include <ComponentModel/NCustomParameterDescriptor.h>
#include <ComponentModel/NCustomMethodDescriptor.h>
#include <ComponentModel/NCustomPropertyDescriptor.h>
#include <ComponentModel/NCustomEventDescriptor.h>
#include <ComponentModel/NTypeDescriptor.h>
#include <ComponentModel/NParameterBag.h>
#include <Core/NDefs.h>
#include <Core/NTypes.h>
#include <Core/NError.h>
#include <Core/NMemory.h>
#include <Core/NObject.h>
#include <Core/NObjectPart.h>
#include <Core/NCore.h>
#include <Core/NWeakReference.h>
#include <Core/NEvent.h>
#include <Core/NType.h>
#include <Core/NValue.h>
#include <Core/NArray.h>
#include <Core/NPropertyBag.h>
#include <Core/NExpandableObject.h>
#include <Core/NEnum.h>
#include <Core/NModule.h>
#include <Core/NAsyncOperation.h>
#include <Core/NProcessorInfo.h>
#include <Core/NString.h>
#include <Core/NCallback.h>
#include <Core/NTimeSpan.h>
#include <Core/NDateTime.h>
#include <Interop/NWindows.h>
#include <Core/NEnvironment.h>
#include <Core/NConsole.h>
#include <Core/NNoDeprecate.h>
#include <Core/NReDeprecate.h>
#include <Diagnostics/NStopwatch.h>
#include <IO/NIOTypes.h>
#include <IO/NStream.h>
#include <IO/NFileStream.h>
#include <IO/NMemoryStream.h>
#include <IO/NBufferedStream.h>
#include <IO/NCustomStream.h>
#include <IO/NBinaryReader.h>
#include <IO/NBinaryWriter.h>
#include <IO/NBuffer.h>
#include <IO/NFile.h>
#include <IO/NFileEnumerator.h>
#include <IO/NDirectory.h>
#include <IO/NPath.h>
#include <IO/NTextReader.h>
#include <IO/NTextWriter.h>
#include <IO/NStreamReader.h>
#include <IO/NStreamWriter.h>
#include <IO/NStringReader.h>
#include <IO/NStringWriter.h>
#include <Plugins/ComponentModel/NPluginMethodDescriptor.h>
#include <Plugins/ComponentModel/NPluginPropertyDescriptor.h>
#include <Plugins/ComponentModel/NPluginEventDescriptor.h>
#include <Plugins/NPluginModule.h>
#include <Plugins/NPlugin.h>
#include <Plugins/NPluginManager.h>
#include <Reflection/NParameterInfo.h>
#include <Reflection/NMemberInfo.h>
#include <Reflection/NConstantInfo.h>
#include <Reflection/NEnumConstantInfo.h>
#include <Reflection/NMethodInfo.h>
#include <Reflection/NPropertyInfo.h>
#include <Reflection/NEventInfo.h>
#include <Reflection/NObjectPartInfo.h>
#include <Reflection/NCollectionInfo.h>
#include <Reflection/NDictionaryInfo.h>
#include <Reflection/NArrayCollectionInfo.h>
#include <Text/NEncoding.h>
#include <Text/NStringBuilder.h>
#include <Threading/NMonitor.h>
#include <Threading/NInterlocked.h>
#include <Threading/NRWLock.h>
#include <Threading/NWaitObject.h>
#include <Threading/NMutex.h>
#include <Threading/NSemaphore.h>
#include <Threading/NSyncEvent.h>
#include <Threading/NThread.h>
#include <Threading/NTls.h>

#endif // !N_CORE_LIBRARY_H_INCLUDED

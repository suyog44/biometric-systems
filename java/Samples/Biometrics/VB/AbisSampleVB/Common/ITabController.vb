Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client

Public Interface ITabController
	Sub CreateNewSubjectTab(ByVal subject As NSubject)
	Sub OpenSubject()
	Sub ShowSettings(ByVal ParamArray navigationParams() As Object)
	Sub ShowAbout()
	Function ShowChangeDatabase() As Boolean
	Sub ShowTab(ByVal tabType As Type, ByVal alwaysCreateNew As Boolean, ByVal canClose As Boolean, ByVal ParamArray args() As Object)
	Sub CloseTab(ByVal tab As TabPageContentBase)

	Property Client() As NBiometricClient
End Interface

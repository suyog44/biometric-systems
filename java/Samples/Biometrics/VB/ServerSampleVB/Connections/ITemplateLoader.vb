Imports System
Imports Neurotec.Biometrics

Public Interface ITemplateLoader
	Inherits IDisposable
	Sub BeginLoad()
	Sub EndLoad()
	Function LoadNext(ByRef subjects() As NSubject, ByVal n As Integer) As Boolean
	ReadOnly Property TemplateCount() As Integer
End Interface

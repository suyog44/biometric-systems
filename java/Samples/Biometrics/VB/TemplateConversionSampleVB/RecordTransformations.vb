Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Standards

Public Class RecordTransformations
	Public Shared Function NFRecordToANTemplate(ByVal nfRecord As NFRecord) As ANTemplate
		Dim tot As String = "a"	' type of transaction
		Dim dai As String = "b"	' destination agency identifier
		Dim ori As String = "c"	' originating agency identifier
		Dim tcn As String = "d"	' transaction control number

		Try
			Dim anTemplate As New ANTemplate(tot, dai, ori, tcn, True, New NTemplate(nfRecord.Save()), 0)
			Return anTemplate
		Catch ex As Exception
			Throw New Exception("Error converting NFRecord to ANTemplate.", ex)
		End Try
	End Function

	Public Shared Function NTemplateToANTemplate(ByVal ntemplate As NTemplate) As ANTemplate
		Dim tot As String = "a"	' type of transaction
		Dim dai As String = "b"	' destination agency identifier
		Dim ori As String = "c"	' originating agency identifier
		Dim tcn As String = "d"	' transaction control number

		' Creating ANTemplate object from NTemplate object
		Try
			Dim anTemplate As New ANTemplate(tot, dai, ori, tcn, True, ntemplate, 0)
			Return anTemplate
		Catch ex As Exception
			Throw New Exception("Error converting NTemplate to ANTemplate.", ex)
		End Try
	End Function

	Public Shared Function ANTemplateToNTemplate(ByVal antemplate As ANTemplate) As NTemplate
		Try
			Dim ntemplate As NTemplate = antemplate.ToNTemplate()
			Return ntemplate
		Catch ex As Exception
			Throw New Exception("Error converting ANTemplate to NTemplate.", ex)
		End Try
	End Function

	Public Shared Function ANTemplateToNFTemplate(ByVal antemplate As ANTemplate) As NFTemplate
		Try
			Dim ntemplate As NTemplate = antemplate.ToNTemplate()
			Return ntemplate.Fingers
		Catch ex As Exception
			Throw New Exception("Error converting ANTemplate to NFTemplate.", ex)
		End Try
	End Function

	Public Shared Function NTemplateToNFTemplate(ByVal ntemplate As NTemplate) As NFTemplate
		Try
			Dim nfTemplate As NFTemplate = ntemplate.Fingers
			Return nfTemplate
		Catch ex As Exception
			Throw New Exception("Error converting NTemplate to NFTemplate.", ex)
		End Try
	End Function

	Public Shared Function NTemplateToNFRecords(ByVal ntemplate As NTemplate) As NFRecord()
		Try
			Dim nfTemplate As NFTemplate = ntemplate.Fingers
			Dim test As New List(Of NFRecord)()
			For i As Integer = 0 To nfTemplate.Records.Count - 1
				test.Add(nfTemplate.Records(i))
			Next i
			Return test.ToArray()
		Catch ex As Exception
			Throw New Exception("Error converting NTemplate to NFRecord.", ex)
		End Try
	End Function

	Public Shared Function NFRecordToFMRecord(ByVal nfrecord As NFRecord, ByVal standard As BdifStandard) As FMRecord
		Try
			Dim fmRecord As New FMRecord(nfrecord, standard, New NVersion(2, 0))
			Return fmRecord
		Catch ex As Exception
			Throw New Exception("Error converting NFRecord to FMRecord.", ex)
		End Try
	End Function

	Public Shared Function NFTemplateToFMRecord(ByVal nftemplate As NFTemplate, ByVal standard As BdifStandard) As FMRecord
		Try
			Dim fmRecord As New FMRecord(nftemplate, standard, New NVersion(2, 0))
			Return fmRecord
		Catch ex As Exception
			Throw New Exception("Error converting NFTemplate to FMRecord.", ex)
		End Try
	End Function

	Public Shared Function FMRecordToNFTemplate(ByVal fmrecord As FMRecord) As NFTemplate
		Try
			Dim nfTemplate As NFTemplate = fmrecord.ToNFTemplate()
			Return nfTemplate
		Catch ex As Exception
			Throw New Exception("Error converting FMRecord to NFTemplate.", ex)
		End Try
	End Function

	Public Shared Function FMRecordToNTemplate(ByVal fmrecord As FMRecord) As NTemplate
		Try
			Dim nTemplate As NTemplate = fmrecord.ToNTemplate()
			Return nTemplate
		Catch ex As Exception
			Throw New Exception("Error converting FMRecord to NTemplate.", ex)
		End Try
	End Function

End Class


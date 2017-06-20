Imports Microsoft.VisualBasic
Imports System
Imports System.IO

Imports Neurotec.Biometrics

Public Class Program
	Private Shared Function RotationToDegrees(ByVal rotation As Integer) As Integer
		Return (2 * rotation * 360 + 256) / (2 * 256)
	End Function

	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(vbTab & "{0} [NTemplate]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(vbTab & "[NTemplate] - file containing NTemplate.")
		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 1 Then
			Return Usage()
		End If

		Try
			Dim templateBuffer() As Byte = File.ReadAllBytes(args(0))

			Console.WriteLine()
			Console.WriteLine("template {0} contains:", args(0))
			Using template As New NTemplate(templateBuffer)
				If template.Fingers IsNot Nothing Then
					Console.WriteLine("{0} fingers", template.Fingers.Records.Count)
					For Each nfRec As NFRecord In template.Fingers.Records
						PrintNFRecord(nfRec)
					Next nfRec
				Else
					Console.WriteLine("0 fingers")
				End If
				If template.Faces IsNot Nothing Then
					Console.WriteLine("{0} faces", template.Faces.Records.Count)
					For Each nlRec As NLRecord In template.Faces.Records
						PrintNLRecord(nlRec)
					Next nlRec
				Else
					Console.WriteLine("0 faces")
				End If
				If template.Irises IsNot Nothing Then
					Console.WriteLine("{0} irises", template.Irises.Records.Count)
					For Each neRec As NERecord In template.Irises.Records
						PrintNERecord(neRec)
					Next neRec
				Else
					Console.WriteLine("0 irises")
				End If
				If template.Voices IsNot Nothing Then
					Console.WriteLine("{0} voices", template.Voices.Records.Count)
					For Each nsRec As NSRecord In template.Voices.Records
						PrintNSRecord(nsRec)
					Next nsRec
				Else
					Console.WriteLine("0 voices")
				End If
			End Using
			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		End Try
	End Function

	Private Shared Sub PrintNFRecord(ByVal nfRec As NFRecord)
		Console.WriteLine(vbTab & "g: {0}", nfRec.G)
		Console.WriteLine(vbTab & "impression type: {0}", nfRec.ImpressionType)
		Console.WriteLine(vbTab & "pattern class: {0}", nfRec.PatternClass)
		Console.WriteLine(vbTab & "cbeff product type: {0}", nfRec.CbeffProductType)
		Console.WriteLine(vbTab & "position: {0}", nfRec.Position)
		Console.WriteLine(vbTab & "ridge counts type: {0}", nfRec.RidgeCountsType)
		Console.WriteLine(vbTab & "width: {0}", nfRec.Width)
		Console.WriteLine(vbTab & "height: {0}", nfRec.Height)
		Console.WriteLine(vbTab & "horizontal resolution: {0}", nfRec.HorzResolution)
		Console.WriteLine(vbTab & "vertical resolution: {0}", nfRec.VertResolution)
		Console.WriteLine(vbTab & "quality: {0}", nfRec.Quality)
		Console.WriteLine(vbTab & "size: {0}", nfRec.GetSize())

		Console.WriteLine(vbTab & "minutia count: {0}", nfRec.Minutiae.Count)

		Dim minutiaFormat As NFMinutiaFormat = nfRec.MinutiaFormat

		Dim index As Integer = 1
		For Each minutia As NFMinutia In nfRec.Minutiae
			Console.WriteLine(vbTab + vbTab & "minutia {0} of {1}", index, nfRec.Minutiae.Count)
			Console.WriteLine(vbTab + vbTab & "x: {0}", minutia.X)
			Console.WriteLine(vbTab + vbTab & "y: {0}", minutia.Y)
			Console.WriteLine(vbTab + vbTab & "angle: {0}", RotationToDegrees(minutia.RawAngle))
			If (minutiaFormat And NFMinutiaFormat.HasQuality) = NFMinutiaFormat.HasQuality Then
				Console.WriteLine(vbTab + vbTab & "quality: {0}", minutia.Quality)
			End If
			If (minutiaFormat And NFMinutiaFormat.HasG) = NFMinutiaFormat.HasG Then
				Console.WriteLine(vbTab + vbTab & "g: {0}", minutia.G)
			End If
			If (minutiaFormat And NFMinutiaFormat.HasCurvature) = NFMinutiaFormat.HasCurvature Then
				Console.WriteLine(vbTab + vbTab & "curvature: {0}", minutia.Curvature)
			End If

			Console.WriteLine()
			index += 1
		Next minutia

		index = 1
		For Each delta As NFDelta In nfRec.Deltas
			Console.WriteLine(vbTab + vbTab & "delta {0} of {1}", index, nfRec.Deltas.Count)
			Console.WriteLine(vbTab + vbTab & "x: {0}", delta.X)
			Console.WriteLine(vbTab + vbTab & "y: {0}", delta.Y)
			Console.WriteLine(vbTab + vbTab & "angle1: {0}", RotationToDegrees(delta.RawAngle1))
			Console.WriteLine(vbTab + vbTab & "angle2: {0}", RotationToDegrees(delta.RawAngle2))
			Console.WriteLine(vbTab + vbTab & "angle3: {0}", RotationToDegrees(delta.RawAngle3))

			Console.WriteLine()
			index += 1
		Next delta

		index = 1
		For Each core As NFCore In nfRec.Cores

			Console.WriteLine(vbTab + vbTab & "core {0} of {1}", index, nfRec.Cores.Count)
			Console.WriteLine(vbTab + vbTab & "x: {0}", core.X)
			Console.WriteLine(vbTab + vbTab & "y: {0}", core.Y)
			Console.WriteLine(vbTab + vbTab & "angle: {0}", RotationToDegrees(core.RawAngle))

			Console.WriteLine()
			index += 1
		Next core

		index = 1
		For Each doubleCore As NFDoubleCore In nfRec.DoubleCores

			Console.WriteLine(vbTab + vbTab & "double core {0} of {1}", index, nfRec.DoubleCores.Count)
			Console.WriteLine(vbTab + vbTab & "x: {0}", doubleCore.X)
			Console.WriteLine(vbTab + vbTab & "y: {0}", doubleCore.Y)

			Console.WriteLine()
			index += 1
		Next doubleCore
	End Sub

	Private Shared Sub PrintNLRecord(ByVal nlRec As NLRecord)
		Console.WriteLine(vbTab & "quality: {0}", nlRec.Quality)
		Console.WriteLine(vbTab & "size: {0}", nlRec.GetSize())
	End Sub

	Private Shared Sub PrintNERecord(ByVal neRec As NERecord)
		Console.WriteLine(vbTab & "position: {0}", neRec.Position)
		Console.WriteLine(vbTab & "size: {0}", neRec.GetSize())
	End Sub

	Private Shared Sub PrintNSRecord(ByVal nsRec As NSRecord)
		Console.WriteLine(vbTab & "phrase id: {0}", nsRec.PhraseId)
		Console.WriteLine(vbTab & "size: {0}", nsRec.GetSize())
	End Sub
End Class

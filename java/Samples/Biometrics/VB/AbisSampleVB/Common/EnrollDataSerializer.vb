Imports System.IO
Imports System.Linq
Imports Neurotec.Biometrics
Imports Neurotec.Images
Imports Neurotec.IO
Imports Neurotec.Sound
Imports System.Text
Imports System
Imports System.Collections.Generic

Public Class EnrollDataSerializer
#Region "Private constructor"

	Private Sub New()
	End Sub

#End Region

#Region "Private fields"
	Private Shared ReadOnly Version As New NVersion(1, 0)
	Private Shared ReadOnly EnrollDataHeader As String = "NeurotechnologySampleData"
#End Region

#Region "Private static methods"

	Private Shared Sub WriteImage(ByVal writer As BinaryWriter, ByVal image As NImage, ByVal format As NImageFormat)
		If image IsNot Nothing Then
			Using buffer As NBuffer = image.Save(format)
				WriteBuffer(writer, buffer)
			End Using
		Else
			WriteBuffer(writer, NBuffer.Empty)
		End If
	End Sub

	Private Shared Sub WriteBuffer(ByVal writer As BinaryWriter, ByVal buffer As NBuffer)
		writer.Write(buffer.Size)
		writer.Write(buffer.ToArray())
	End Sub

	Private Shared Function CheckHeaderAndVersion(ByVal reader As BinaryReader) As Boolean
		Dim headerBytes() As Byte = reader.ReadBytes(Encoding.ASCII.GetByteCount(EnrollDataHeader))
		Dim header As String = Encoding.ASCII.GetString(headerBytes)
		If header <> EnrollDataHeader Then
			Return False
		End If
		Dim ver As NVersion = CType(reader.ReadUInt16(), NVersion)
		If ver > Version Then
			Return False
		End If

		Return True
	End Function

	Private Shared Sub WriteHeaderAndVersion(ByVal writer As BinaryWriter)
		writer.Write(Encoding.ASCII.GetBytes(EnrollDataHeader))
		writer.Write(CUShort(Version))
	End Sub

#End Region

#Region "Public static methods"

	Public Shared Function Serialize(ByVal subject As NSubject, ByVal useWsqForFingers As Boolean) As NBuffer
		Using stream As New MemoryStream()
			Using writer As New BinaryWriter(stream)
				WriteHeaderAndVersion(writer)

				Dim fingers = SubjectUtils.GetTemplateCompositeFingers(subject).ToArray()
				writer.Write(fingers.Length)
				For Each finger In fingers
					WriteImage(writer, finger.Image, If(useWsqForFingers, NImageFormat.Wsq, NImageFormat.Png))
				Next finger

				Dim faces = SubjectUtils.GetTemplateCompositeFaces(subject).ToArray()
				writer.Write(faces.Length)
				For Each face In faces
					Dim attributes = face.Objects.First()
					Dim template = attributes.Template
					writer.Write(template.Records.Count)
					WriteImage(writer, face.Image, NImageFormat.Png)
				Next face

				Dim irises = SubjectUtils.GetTemplateCompositeIrises(subject).ToArray()
				writer.Write(irises.Length)
				For Each iris In irises
					WriteImage(writer, iris.Image, NImageFormat.Png)
				Next iris

				Dim palms = SubjectUtils.GetTemplateCompositePalms(subject).ToArray()
				writer.Write(palms.Length)
				For Each palm In palms
					WriteImage(writer, palm.Image, NImageFormat.Png)
				Next palm

				Dim voices = SubjectUtils.GetTemplateCompositeVoices(subject).ToArray()
				writer.Write(voices.Length)
				For Each voice In voices
					WriteBuffer(writer, If(voice.SoundBuffer IsNot Nothing, voice.SoundBuffer.Save(), NBuffer.Empty))
				Next voice

				writer.Write(0)	' fin

				Return New NBuffer(stream.ToArray())
			End Using
		End Using
	End Function

	Public Shared Function Serialize(ByVal subject As NSubject) As NBuffer
		Return Serialize(subject, False)
	End Function

	Public Shared Function Deserialize(ByVal templateData As NBuffer, ByVal serializeData As NBuffer) As NSubject
		Dim recordCounts As Integer() = Nothing
		Return Deserialize(templateData, serializeData, recordCounts)
	End Function

	Public Shared Function Deserialize(ByVal templateData As NBuffer, ByVal serializeData As NBuffer, <System.Runtime.InteropServices.Out()> ByRef faceRecordCounts() As Integer) As NSubject
		Dim recrodCounts As List(Of Integer) = New List(Of Integer)()
		Using template As New NTemplate(templateData)
			Using stream As New MemoryStream(serializeData.ToArray())
				Using reader As New BinaryReader(stream)
					Dim subject As New NSubject()
					Dim length As Integer = 0
					Dim bufferSize As Integer
					Dim bytes() As Byte = Nothing
					Dim recordIndex As Integer = 0

					If Not CheckHeaderAndVersion(reader) Then
						Throw New FormatException()
					End If

					' fingers
					length = reader.ReadInt32()
					For i As Integer = 0 To length - 1
						bufferSize = reader.ReadInt32()
						bytes = reader.ReadBytes(bufferSize)
						Using image As NImage = If(bufferSize > 0, NImage.FromMemory(bytes), Nothing)
							Dim finger As NFinger = CType(NFinger.FromImageAndTemplate(image, template.Fingers.Records(i)), NFinger)
							subject.Fingers.Add(finger)
						End Using
					Next i

					' faces
					length = reader.ReadInt32()
					For i As Integer = 0 To length - 1
						Dim recordCount As Integer = reader.ReadInt32()
						recrodCounts.Add(recordCount)

						bufferSize = reader.ReadInt32()
						bytes = reader.ReadBytes(bufferSize)
						Using image As NImage = If(bufferSize > 0, NImage.FromMemory(bytes), Nothing)
							Dim actualTemplate As New NLTemplate()
							Dim attributes As New NLAttributes()
							For j As Integer = 0 To recordCount - 1
								actualTemplate.Records.Add(template.Faces.Records(recordIndex))
							Next
							attributes.Template = actualTemplate
							Dim face As NFace = NFace.FromImageAndAttributes(image, attributes)
							subject.Faces.Add(face)
						End Using
					Next i

					' irises
					length = reader.ReadInt32()
					For i As Integer = 0 To length - 1
						bufferSize = reader.ReadInt32()
						bytes = reader.ReadBytes(bufferSize)
						Using image As NImage = If(bufferSize > 0, NImage.FromMemory(bytes), Nothing)
							Dim iris As NIris = NIris.FromImageAndTemplate(image, template.Irises.Records(i))
							subject.Irises.Add(iris)
						End Using
					Next i

					' palms
					length = reader.ReadInt32()
					For i As Integer = 0 To length - 1
						bufferSize = reader.ReadInt32()
						bytes = reader.ReadBytes(bufferSize)
						Using image As NImage = If(bufferSize > 0, NImage.FromMemory(bytes), Nothing)
							Dim palm As NPalm = CType(NPalm.FromImageAndTemplate(image, template.Palms.Records(i)), NPalm)
							subject.Palms.Add(palm)
						End Using
					Next i

					' voices
					length = reader.ReadInt32()
					For i As Integer = 0 To length - 1
						bufferSize = reader.ReadInt32()
						bytes = reader.ReadBytes(bufferSize)
						Using buffer As NSoundBuffer = If(bufferSize > 0, NSoundBuffer.FromMemory(bytes), Nothing)
							Dim voice As NVoice = NVoice.FromSoundBufferAndTemplate(buffer, template.Voices.Records(i))
							subject.Voices.Add(voice)
						End Using
					Next i

					faceRecordCounts = recrodCounts.ToArray()
					Return subject
				End Using
			End Using
		End Using
	End Function

#End Region
End Class

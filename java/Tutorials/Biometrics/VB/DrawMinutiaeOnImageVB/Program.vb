Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Licensing
Imports Neurotec.Biometrics.Gui
Imports Neurotec.Images

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [image] [bitmap]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "[image] - image file containing a finger")
		Console.WriteLine(Constants.vbTab & "[bitmap] - filename to store finger.")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.FingerExtraction"

		TutorialUtils.PrintTutorialHeader(args)
		If args.Length < 2 Then
			Return Usage()
		End If

		Try
			' obtain license
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New ApplicationException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Using client = New NBiometricClient()
				Using fingerView = New NFingerView()
					Using subject = New NSubject()
						Using finger = New NFinger()
							Using image = NImage.FromFile(args(0))
								' setting fingers image
								finger.Image = image

								' adding finger to subject
								subject.Fingers.Add(finger)

								' creating template from subject
								Dim status As NBiometricStatus = client.CreateTemplate(subject)

								If status = NBiometricStatus.Ok Then
									Console.WriteLine("Template creation succeeded")

									fingerView.Width = CInt(Fix(image.Width))
									fingerView.Height = CInt(Fix(image.Height))

									' settings finger with template to finger view
									fingerView.Finger = subject.Fingers(0)

									' creating new bitmap with not indexed pixel format
									Using tempBitmap As New Bitmap(fingerView.Width, fingerView.Height, PixelFormat.Format32bppArgb)
										Dim rect As New Rectangle(0, 0, fingerView.Width, fingerView.Height)

										' draw minutiae on bitmap
										fingerView.DrawToBitmap(tempBitmap, rect)

										' save bitmap
										tempBitmap.Save(args(1))
									End Using
								Else
									Console.WriteLine("Template creation failed. Status {0}", status)

									Return -1
								End If
							End Using
						End Using
					End Using
				End Using
			End Using

			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			NLicense.ReleaseComponents(Components)
		End Try
	End Function
End Class

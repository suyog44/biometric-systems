Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Licensing

Friend Class Program
	Shared Function Main(ByVal args() As String) As Integer
		TutorialUtils.PrintTutorialHeader(args)

		Try
			Dim dongle As NLicManDongle = NLicenseManager.FindFirstDongle()
			If dongle Is Nothing Then
				Console.WriteLine("no dongles found")
				Return -1
			End If

			Do
				Console.WriteLine("=== Dongle Id: {0} ===" & vbLf, dongle.DistributorId)
				Dim licenses() As NLicenseProductInfo = dongle.GetLicenses()
				For Each license As NLicenseProductInfo In licenses
					Console.WriteLine("{0} OS: {1}, Count: {2}", NLicenseManager.GetShortProductName(license.Id, license.LicenseType), license.OSFamily, license.LicenseCount)
				Next license

				dongle = NLicenseManager.FindNextDongle()
				If dongle Is Nothing Then
					Console.WriteLine("no more dongles found")
				End If
			Loop While dongle IsNot Nothing

			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		End Try
	End Function
End Class

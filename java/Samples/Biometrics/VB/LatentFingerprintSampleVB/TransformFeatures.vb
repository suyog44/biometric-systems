Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Biometrics
Imports Neurotec.Images

Friend NotInheritable Class TransformFeatures
	#Region "Private static methods"

	Private Sub New()
	End Sub
	Private Shared Function FlipFeatureAngleHorizontally(ByVal angle As Double) As Double
		angle = Math.PI - angle
		Do While angle > Math.PI
			angle -= Math.PI * 2
		Loop
		Return angle
	End Function

	Private Shared Function FlipFeatureAngleVertically(ByVal angle As Double) As Double
		Return -angle
	End Function

	Private Shared Function RotateFeatureAngle(ByVal angle As Double, ByVal rotateAngle As Double) As Double
		angle -= rotateAngle

		If angle < -Math.PI Then
			angle += Math.PI * 2
		ElseIf angle > Math.PI Then
			angle -= Math.PI * 2
		End If

		Return angle
	End Function

	#End Region

	#Region "Public static methods"

	Public Shared Function FlipHorizontally(ByVal template As NFRecord) As NFRecord
		Dim record = New NFRecord(template.Width, template.Height, template.HorzResolution, template.VertResolution) With {.MinutiaFormat = template.MinutiaFormat, .CbeffProductType = template.CbeffProductType, .RidgeCountsType = template.RidgeCountsType}

		Dim templateWidth = CInt(Fix(CDbl(template.Width) * NFRecord.Resolution / template.HorzResolution))
		For Each minutia As NFMinutia In template.Minutiae
			Dim newMinutia As NFMinutia = minutia
			newMinutia.X = CUShort(templateWidth - minutia.X - 1)
			newMinutia.Angle = FlipFeatureAngleHorizontally(newMinutia.Angle)
			record.Minutiae.Add(newMinutia)
		Next minutia
		For Each delta As NFDelta In template.Deltas
			Dim newDelta As NFDelta = delta
			newDelta.X = CUShort(templateWidth - delta.X - 1)
			newDelta.Angle1 = FlipFeatureAngleHorizontally(newDelta.Angle1)
			newDelta.Angle2 = FlipFeatureAngleHorizontally(newDelta.Angle2)
			newDelta.Angle3 = FlipFeatureAngleHorizontally(newDelta.Angle3)
			record.Deltas.Add(newDelta)
		Next delta
		For Each core As NFCore In template.Cores
			Dim newCore As NFCore = core
			newCore.X = CUShort(templateWidth - core.X - 1)
			newCore.Angle = FlipFeatureAngleHorizontally(newCore.Angle)
			record.Cores.Add(newCore)
		Next core
		For Each doubleCore As NFDoubleCore In template.DoubleCores
			Dim newDoubleCore As NFDoubleCore = doubleCore
			newDoubleCore.X = CUShort(templateWidth - doubleCore.X - 1)
			record.DoubleCores.Add(newDoubleCore)
		Next doubleCore

		Return record
	End Function

	Public Shared Function FlipVertically(ByVal template As NFRecord) As NFRecord
		Dim record = New NFRecord(template.Width, template.Height, template.HorzResolution, template.VertResolution) With {.MinutiaFormat = template.MinutiaFormat, .CbeffProductType = template.CbeffProductType, .RidgeCountsType = template.RidgeCountsType}

		Dim templateHeight = CInt(Fix(CDbl(template.Height) * NFRecord.Resolution / template.VertResolution))
		For Each minutia As NFMinutia In template.Minutiae
			Dim newMinutia As NFMinutia = minutia
			newMinutia.Y = CUShort(templateHeight - minutia.Y - 1)
			newMinutia.Angle = FlipFeatureAngleVertically(newMinutia.Angle)
			record.Minutiae.Add(newMinutia)
		Next minutia
		For Each delta As NFDelta In template.Deltas
			Dim newDelta As NFDelta = delta
			newDelta.Y = CUShort(templateHeight - delta.Y - 1)
			newDelta.Angle1 = FlipFeatureAngleVertically(newDelta.Angle1)
			newDelta.Angle2 = FlipFeatureAngleVertically(newDelta.Angle2)
			newDelta.Angle3 = FlipFeatureAngleVertically(newDelta.Angle3)
			record.Deltas.Add(newDelta)
		Next delta
		For Each core As NFCore In template.Cores
			Dim newCore As NFCore = core
			newCore.Y = CUShort(templateHeight - core.Y - 1)
			newCore.Angle = FlipFeatureAngleVertically(newCore.Angle)
			record.Cores.Add(newCore)
		Next core
		For Each doubleCore As NFDoubleCore In template.DoubleCores
			Dim newDoubleCore As NFDoubleCore = doubleCore
			newDoubleCore.Y = CUShort(templateHeight - doubleCore.Y - 1)
			record.DoubleCores.Add(newDoubleCore)
		Next doubleCore

		Return record
	End Function

	Public Shared Function Rotate90(ByVal template As NFRecord) As NFRecord
		Dim record = New NFRecord(template.Height, template.Width, template.VertResolution, template.HorzResolution) With {.MinutiaFormat = template.MinutiaFormat, .CbeffProductType = template.CbeffProductType, .RidgeCountsType = template.RidgeCountsType}

		Dim templateHeight = CInt(Fix(CDbl(template.Height) * NFRecord.Resolution / template.VertResolution))
		For Each minutia As NFMinutia In template.Minutiae
			Dim newMinutia As NFMinutia = minutia
			newMinutia.X = CUShort(templateHeight - minutia.Y - 1)
			newMinutia.Y = (minutia.X)
			newMinutia.Angle = RotateFeatureAngle(newMinutia.Angle, -Math.PI / 2)
			record.Minutiae.Add(newMinutia)
		Next minutia
		For Each delta As NFDelta In template.Deltas
			Dim newDelta As NFDelta = delta
			newDelta.X = CUShort(templateHeight - delta.Y - 1)
			newDelta.Y = (delta.X)
			newDelta.Angle1 = RotateFeatureAngle(newDelta.Angle1, -Math.PI / 2)
			newDelta.Angle2 = RotateFeatureAngle(newDelta.Angle2, -Math.PI / 2)
			newDelta.Angle3 = RotateFeatureAngle(newDelta.Angle3, -Math.PI / 2)
			record.Deltas.Add(newDelta)
		Next delta
		For Each core As NFCore In template.Cores
			Dim newCore As NFCore = core
			newCore.X = CUShort(templateHeight - core.Y - 1)
			newCore.Y = (core.X)
			newCore.Angle = RotateFeatureAngle(newCore.Angle, -Math.PI / 2)
			record.Cores.Add(newCore)
		Next core
		For Each doubleCore As NFDoubleCore In template.DoubleCores
			Dim newDoubleCore As NFDoubleCore = doubleCore
			newDoubleCore.X = CUShort(templateHeight - doubleCore.Y - 1)
			newDoubleCore.Y = (doubleCore.X)
			record.DoubleCores.Add(newDoubleCore)
		Next doubleCore

		Return record
	End Function

	Public Shared Function Rotate180(ByVal template As NFRecord) As NFRecord
		Dim record = New NFRecord(template.Width, template.Height, template.HorzResolution, template.VertResolution) With {.MinutiaFormat = template.MinutiaFormat, .CbeffProductType = template.CbeffProductType, .RidgeCountsType = template.RidgeCountsType}

		Dim templateWidth = CInt(Fix(CDbl(template.Width) * NFRecord.Resolution / template.HorzResolution))
		Dim templateHeight = CInt(Fix(CDbl(template.Height) * NFRecord.Resolution / template.VertResolution))
		For Each minutia As NFMinutia In template.Minutiae
			Dim newMinutia As NFMinutia = minutia
			newMinutia.X = CUShort(templateWidth - minutia.X - 1)
			newMinutia.Y = CUShort(templateHeight - minutia.Y - 1)
			newMinutia.Angle = RotateFeatureAngle(newMinutia.Angle, Math.PI)
			record.Minutiae.Add(newMinutia)
		Next minutia
		For Each delta As NFDelta In template.Deltas
			Dim newDelta As NFDelta = delta
			newDelta.X = CUShort(templateWidth - delta.X - 1)
			newDelta.Y = CUShort(templateHeight - delta.Y - 1)
			newDelta.Angle1 = RotateFeatureAngle(newDelta.Angle1, Math.PI)
			newDelta.Angle2 = RotateFeatureAngle(newDelta.Angle2, Math.PI)
			newDelta.Angle3 = RotateFeatureAngle(newDelta.Angle3, Math.PI)
			record.Deltas.Add(newDelta)
		Next delta
		For Each core As NFCore In template.Cores
			Dim newCore As NFCore = core
			newCore.X = CUShort(templateWidth - core.X - 1)
			newCore.Y = CUShort(templateHeight - core.Y - 1)
			newCore.Angle = RotateFeatureAngle(newCore.Angle, Math.PI)
			record.Cores.Add(newCore)
		Next core
		For Each doubleCore As NFDoubleCore In template.DoubleCores
			Dim newDoubleCore As NFDoubleCore = doubleCore
			newDoubleCore.X = CUShort(templateWidth - doubleCore.X - 1)
			newDoubleCore.Y = CUShort(templateHeight - doubleCore.Y - 1)
			record.DoubleCores.Add(newDoubleCore)
		Next doubleCore

		Return record
	End Function

	Public Shared Function Rotate270(ByVal template As NFRecord) As NFRecord
		Dim record = New NFRecord(template.Height, template.Width, template.VertResolution, template.HorzResolution) With {.MinutiaFormat = template.MinutiaFormat, .CbeffProductType = template.CbeffProductType, .RidgeCountsType = template.RidgeCountsType}

		Dim templateWidth = CInt(Fix(CDbl(template.Width) * NFRecord.Resolution / template.HorzResolution))
		For Each minutia As NFMinutia In template.Minutiae
			Dim newMinutia As NFMinutia = minutia
			newMinutia.X = (minutia.Y)
			newMinutia.Y = CUShort(templateWidth - minutia.X - 1)
			newMinutia.Angle = RotateFeatureAngle(newMinutia.Angle, Math.PI / 2)
			record.Minutiae.Add(newMinutia)
		Next minutia
		For Each delta As NFDelta In template.Deltas
			Dim newDelta As NFDelta = delta
			newDelta.X = (delta.Y)
			newDelta.Y = CUShort(templateWidth - delta.X - 1)
			newDelta.Angle1 = RotateFeatureAngle(newDelta.Angle1, Math.PI / 2)
			newDelta.Angle2 = RotateFeatureAngle(newDelta.Angle2, Math.PI / 2)
			newDelta.Angle3 = RotateFeatureAngle(newDelta.Angle3, Math.PI / 2)
			record.Deltas.Add(newDelta)
		Next delta
		For Each core As NFCore In template.Cores
			Dim newCore As NFCore = core
			newCore.X = (core.Y)
			newCore.Y = CUShort(templateWidth - core.X - 1)
			newCore.Angle = RotateFeatureAngle(newCore.Angle, Math.PI / 2)
			record.Cores.Add(newCore)
		Next core
		For Each doubleCore As NFDoubleCore In template.DoubleCores
			Dim newDoubleCore As NFDoubleCore = doubleCore
			newDoubleCore.X = (doubleCore.Y)
			newDoubleCore.Y = CUShort(templateWidth - doubleCore.X - 1)
			record.DoubleCores.Add(newDoubleCore)
		Next doubleCore

		Return record
	End Function

	Public Shared Function Crop(ByVal template As NFRecord, ByVal image As NImage, ByVal x As UInteger, ByVal y As UInteger, ByVal width As UInteger, ByVal height As UInteger) As NFRecord
		Dim record = New NFRecord(CUShort(image.Width), CUShort(image.Height), template.HorzResolution, template.VertResolution)
		record.MinutiaFormat = template.MinutiaFormat
		record.CbeffProductType = template.CbeffProductType
		record.RidgeCountsType = template.RidgeCountsType

		For Each minutia As NFMinutia In template.Minutiae
			If minutia.X > x AndAlso minutia.Y > y AndAlso minutia.X - x < width AndAlso minutia.Y - y < height Then
				Dim newMinutia As NFMinutia = minutia
				newMinutia.X = CUShort(minutia.X - x)
				newMinutia.Y = CUShort(minutia.Y - y)
				record.Minutiae.Add(newMinutia)
			End If
		Next minutia
		For Each delta As NFDelta In template.Deltas
			If delta.X > x AndAlso delta.Y > y AndAlso delta.X - x < width AndAlso delta.Y - y < height Then
				Dim newDelta As NFDelta = delta
				newDelta.X = CUShort(delta.X - x)
				newDelta.Y = CUShort(delta.Y - y)
				record.Deltas.Add(newDelta)
			End If
		Next delta
		For Each core As NFCore In template.Cores
			If core.X > x AndAlso core.Y > y AndAlso core.X - x < width AndAlso core.Y - y < height Then
				Dim newCore As NFCore = core
				newCore.X = CUShort(core.X - x)
				newCore.Y = CUShort(core.Y - y)
				record.Cores.Add(newCore)
			End If
		Next core
		For Each doubleCore As NFDoubleCore In template.DoubleCores
			If doubleCore.X > x AndAlso doubleCore.Y > y AndAlso doubleCore.X - x < width AndAlso doubleCore.Y - y < height Then
				Dim newDoubleCore As NFDoubleCore = doubleCore
				newDoubleCore.X = CUShort(doubleCore.X - x)
				newDoubleCore.Y = CUShort(doubleCore.Y - y)
				record.DoubleCores.Add(newDoubleCore)
			End If
		Next doubleCore

		Return record
	End Function

	#End Region
End Class

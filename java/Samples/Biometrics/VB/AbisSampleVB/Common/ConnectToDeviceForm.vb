Imports System
Imports System.Windows.Forms
Imports Neurotec.Plugins
Imports Neurotec.Devices
Imports Neurotec.ComponentModel

Partial Public Class ConnectToDeviceForm
	Inherits Form
#Region "Private fields"

	Private _parameters() As NParameterDescriptor

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		pluginComboBox.BeginUpdate()
		For Each plugin As NPlugin In NDeviceManager.PluginManager.Plugins
			If plugin.State = NPluginState.Plugged AndAlso NDeviceManager.IsConnectToDeviceSupported(plugin) Then
				pluginComboBox.Items.Add(plugin)
			End If
		Next plugin
		If pluginComboBox.Items.Count <> 0 Then
			pluginComboBox.SelectedIndex = 0
		Else
			OnSelectedPluginChanged()
		End If
		pluginComboBox.EndUpdate()
	End Sub

#End Region

#Region "Private methods"

	Private Sub OnSelectedPluginChanged()
		Dim plugin As NPlugin = SelectedPlugin
		_parameters = If(plugin Is Nothing, Nothing, NDeviceManager.GetConnectToDeviceParameters(plugin))
		propertyGrid.SelectedObject = If(_parameters Is Nothing, Nothing, New NParameterBag(_parameters))
		propertyGrid.Enabled = plugin IsNot Nothing
		btnOK.Enabled = propertyGrid.Enabled
	End Sub

#End Region

#Region "Public properties"

	Public Property SelectedPlugin() As NPlugin
		Get
			Return CType(pluginComboBox.SelectedItem, NPlugin)
		End Get
		Set(ByVal value As NPlugin)
			If pluginComboBox.Items.Contains(value) Then
				pluginComboBox.SelectedItem = value
			End If
		End Set
	End Property

	Public Property Parameters() As NPropertyBag
		Get
			Dim parameterBag = CType(propertyGrid.SelectedObject, NParameterBag)
			Return If(parameterBag Is Nothing, Nothing, parameterBag.ToPropertyBag())
		End Get
		Set(ByVal value As NPropertyBag)
			Dim parameterBag = CType(propertyGrid.SelectedObject, NParameterBag)
			If parameterBag IsNot Nothing Then
				parameterBag.Apply(value, True)
			End If
		End Set
	End Property

#End Region

	Private Sub pluginComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles pluginComboBox.SelectedIndexChanged
		OnSelectedPluginChanged()
	End Sub

	Private Sub ConnectToDeviceForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
		If DialogResult <> System.Windows.Forms.DialogResult.OK Then
			Return
		End If
		If _parameters IsNot Nothing Then
			Dim parameterBag = CType(propertyGrid.SelectedObject, NParameterBag)
			For i As Integer = 0 To _parameters.Length - 1
				If (_parameters(i).Attributes And NAttributes.Optional) <> NAttributes.Optional Then
					If parameterBag.Values(i) Is Nothing Then
						MessageBox.Show(String.Format("{0} value not specified", _parameters(i).Name), Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
						DialogResult = System.Windows.Forms.DialogResult.None
						e.Cancel = True
					End If
				End If
			Next i
		End If
	End Sub
End Class

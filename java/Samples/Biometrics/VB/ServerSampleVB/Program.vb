Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms

Friend Class Program
	<STAThread()> _
	Shared Sub Main(ByVal args() As String)
		Application.EnableVisualStyles()
		Application.SetCompatibleTextRenderingDefault(False)
		Application.Run(New MainForm())
	End Sub
End Class

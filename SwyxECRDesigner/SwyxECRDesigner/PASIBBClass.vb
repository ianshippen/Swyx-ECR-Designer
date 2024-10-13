Public Class PASIBBClass
    Inherits SIBBClass

    Public Const DTMF_PRESSED_INDEX As Integer = 1

    Public filename As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.new(SIBBTypeClass.SIBBTYPE.PA, p_bitmap)

        filename = ""
    End Sub

    Public Overrides Sub PackData()
        PackData(filename)
    End Sub

    Public Overrides Sub UnpackData()
        UnpackData(filename)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(filename, "Filename")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        filename = p_tabPage.Controls(0).Text
        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Dim result As Integer = -1

        If p_data = "" Then
            result = 0
        Else
            MsgBox("Play Announcement: " & WrapInQuotes(p_data))
            result = 0
        End If

        Return result
    End Function
End Class

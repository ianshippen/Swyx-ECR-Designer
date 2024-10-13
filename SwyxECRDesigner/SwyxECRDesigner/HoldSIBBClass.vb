Public Class HoldSIBBClass
    Inherits SIBBClass

    Public sound As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.New(SIBBTypeClass.SIBBTYPE.HOLD, p_bitmap)

        sound = "*hold*"
    End Sub

    Public Overrides Sub PackData()
        PackData(sound)
    End Sub

    Public Overrides Sub UnpackData()
        UnpackData(sound)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(sound, "Sound")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        sound = p_tabPage.Controls(0).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        MsgBox("Hold with audio file " & WrapInQuotes(p_data))
    End Function
End Class

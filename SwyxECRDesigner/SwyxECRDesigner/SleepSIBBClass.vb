Public Class SleepSIBBClass
    Inherits SIBBClass

    Public sleepTimeMs As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.New(SIBBTypeClass.SIBBTYPE.SLEEP, p_bitmap)

        sleepTimeMs = "0"
    End Sub

    Public Overrides Sub PackData()
        PackData(sleepTimeMs)
    End Sub

    Public Overrides Sub UnpackData()
        sleepTimeMs = "0"

        UnpackData(sleepTimeMs)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(sleepTimeMs, "Sleep Time / ms")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        sleepTimeMs = p_tabPage.Controls(0).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub
End Class

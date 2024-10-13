Public Class EndCallClass
    Inherits SIBBClass

    Public reason As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.New(SIBBTypeClass.SIBBTYPE.END_CALL, p_bitmap)

        reason = 0
    End Sub

    Public Overrides Sub PackData()
        PackData(reason)
    End Sub

    Public Overrides Sub UnpackData()
        Dim UnpackData(reason)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(reason, "Reason Code")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        reason = p_tabPage.Controls(0).Text
        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Return -1
    End Function
End Class

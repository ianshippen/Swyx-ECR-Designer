Public Class LogAgentStatusSIBBClass
    Inherits SIBBClass

    Public target As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.New(SIBBTypeClass.SIBBTYPE.LOG_AGENT_STATUS, p_bitmap)

        target = ""
    End Sub

    Public Overrides Sub PackData()
        PackData(target)
    End Sub

    Public Overrides Sub UnpackData()
        UnpackData(target)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)
        z.AddTextBoxLabelPair(target, "Target")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        target = p_tabPage.Controls(0).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Return -1
    End Function
End Class

Public Class FirstTimeSIBBClass
    Inherits SIBBClass

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.New(SIBBTypeClass.SIBBTYPE.FIRST_TIME, p_bitmap)
    End Sub

    Public Overrides Sub PackData()
        PackData("")
    End Sub

    Public Overrides Sub UnpackData()
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        MyBase.SetupForm(p_tabPage)
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Return -1
    End Function
End Class

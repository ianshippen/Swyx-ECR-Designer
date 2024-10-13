Public Class DayOfWeekClass
    Inherits SIBBClass

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.New(SIBBTypeClass.SIBBTYPE.DAY_OF_WEEK, p_bitmap)
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
        Dim result As Integer = Now.DayOfWeek - 1
        Dim z As String = DesignerForm.GetServiceWideVariableValue("$testDateTime")

        If result = -1 Then result = 6

        If z <> "" Then
            result = (DatePart("w", CDate(z)) - 2)

            If result = -1 Then result = 6
        End If

        Return result
    End Function
End Class

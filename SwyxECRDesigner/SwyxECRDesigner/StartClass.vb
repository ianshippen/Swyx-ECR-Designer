Public Class StartClass
    Inherits SIBBClass

    Public inheritsFrom As String
    Public code As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.New(SIBBTypeClass.SIBBTYPE.START, p_bitmap)
    End Sub

    Public Overrides Sub PackData()
        PackDataDirect(code)
    End Sub

    Public Overrides Sub UnpackData()
        code = UnpackDataDirect()
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(inheritsFrom, "Inherits From ..")
        z.AddEditTextBoxLabelPair(code, "Code")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        inheritsFrom = p_tabPage.Controls(0).Text
        code = p_tabPage.Controls(2).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Return 0
    End Function
End Class

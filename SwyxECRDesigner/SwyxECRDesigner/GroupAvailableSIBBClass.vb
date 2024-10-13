Public Class GroupAvailableSIBBClass
    Inherits SIBBClass

    Public target As String
    Public rule As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.New(SIBBTypeClass.SIBBTYPE.GROUP_AVAILABLE, p_bitmap)

        target = "$primaryTeam"
        rule = "$yellowOption"
    End Sub

    Public Overrides Sub PackData()
        PackData(target, rule)
    End Sub

    Public Overrides Sub UnpackData()
        UnpackData(target, rule)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)
        z.AddTextBoxLabelPair(target, "Target")
        z.AddTextBoxLabelPair(rule, "Rule")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        target = p_tabPage.Controls(0).Text
        rule = p_tabPage.Controls(2).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Dim result As Integer = -1
        Dim myArray() As String = p_data.Split(",")

        With GroupAvailableForm
            .TextBox1.Text = myArray(0)
            .TextBox2.Text = TestForm.SafeGetItem(myArray(0))
            .TextBox4.Text = myArray(1)
            .TextBox3.Text = TestForm.SafeGetItem(myArray(1))

            If .ShowDialog = DialogResult.OK Then result = .result
        End With

        Return result
    End Function
End Class

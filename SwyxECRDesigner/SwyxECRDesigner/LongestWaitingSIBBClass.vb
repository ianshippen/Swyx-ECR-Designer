Public Class LongestWaitingSIBBClass
    Inherits SIBBClass

    Public target As String
    Public agentTimeout As String
    Public groupTimeout As String
    Public rule As String
    Public alertSound As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.New(SIBBTypeClass.SIBBTYPE.LONGEST_WAITING_DIST, p_bitmap)

        target = "$primaryTeam"
        agentTimeout = "$agentTimeout"
        groupTimeout = "$groupTimeout"
        rule = "$yellowOption"
    End Sub

    Public Overrides Sub PackData()
        PackData(target, agentTimeout, groupTimeout, rule, alertSound)
    End Sub

    Public Overrides Sub UnpackData()
        alertSound = ""
        UnpackData(target, agentTimeout, groupTimeout, rule, alertSound)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(target, "Target")
        z.AddTextBoxLabelPair(agentTimeout, "Agent Timeout")
        z.AddTextBoxLabelPair(groupTimeout, "Group Timeout")
        z.AddTextBoxLabelPair(rule, "Rule")
        z.AddTextBoxLabelPair(alertSound, "Alert Sound")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        target = p_tabPage.Controls(0).Text
        agentTimeout = p_tabPage.Controls(2).Text
        groupTimeout = p_tabPage.Controls(4).Text
        rule = p_tabPage.Controls(6).Text
        alertSound = p_tabPage.Controls(8).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Dim result As Integer = -1
        Dim myArray() As String = p_data.Split(",")

        With LongestWaitingForm
            .TextBox2.Text = myArray(0)
            .TextBox4.Text = myArray(1)
            .TextBox6.Text = myArray(2)
            .TextBox8.Text = myArray(3)

            If myArray.Length > 4 Then .TextBox10.Text = myArray(4)

            .TextBox1.Text = TestForm.SafeGetItem(myArray(0))
            .TextBox3.Text = TestForm.SafeGetItem(myArray(1))
            .TextBox5.Text = TestForm.SafeGetItem(myArray(2))
            .TextBox7.Text = TestForm.SafeGetItem(myArray(3))

            If myArray.Length > 4 Then .TextBox9.Text = TestForm.SafeGetItem(myArray(4))

            If myArray(3).Length > 1 Then
                If (myArray(3).StartsWith("$")) Then
                    Dim rule As String = TestForm.SafeGetItem(myArray(3))
                End If
            End If
        End With

        If LongestWaitingForm.ShowDialog = DialogResult.OK Then
            result = LongestWaitingForm.result
        End If

        Return result
    End Function
End Class

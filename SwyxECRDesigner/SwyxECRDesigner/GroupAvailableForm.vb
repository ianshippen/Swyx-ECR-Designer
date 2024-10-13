Public Class GroupAvailableForm
    Public result As Integer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        result = 0
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub GroupAvailableForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        result = -1
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        result = 1
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class
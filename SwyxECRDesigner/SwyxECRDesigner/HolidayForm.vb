Public Class HolidayForm
    Public result As Integer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        result = 0
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        result = 1
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        result = 2
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        result = 3
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub HolidayForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        result = -1
    End Sub
End Class
Public Class ViewCodeForm
    Dim x, y As Integer
    Dim allowResize As Boolean = False

    Private Sub viewCodeTextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles viewCodeTextBox.KeyPress
        If e.KeyChar = Convert.ToChar(1) Then
            DirectCast(sender, TextBox).SelectAll()
            e.Handled = True
        End If
    End Sub

    Private Sub ViewCodeForm_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If allowResize Then
            viewCodeTextBox.Size = New Point(Size.Width - x, Size.Height - y)
        End If
    End Sub

    Private Sub ViewCodeForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        x = Size.Width - viewCodeTextBox.Size.Width
        y = Size.Height - viewCodeTextBox.Size.Height
        allowResize = True
    End Sub
End Class
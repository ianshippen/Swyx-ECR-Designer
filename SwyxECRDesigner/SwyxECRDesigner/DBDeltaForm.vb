Public Class DBDeltaForm
    Private okXDelta, okYDelta, textXDelta, textYDelta As Integer
    Private okToResize As Boolean = False

    Private Sub DBDeltaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        okXDelta = Width - okButton.Location.X
        okYDelta = Height - okButton.Location.Y
        textXDelta = Width - RichTextBox.Width
        textYDelta = Height - RichTextBox.Height
        okToResize = True
    End Sub

    Private Sub DBDeltaForm_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If okToResize Then
            okButton.Location = New Point(Width - okXDelta, Height - okYDelta)
            RichTextBox.Width = Width - textXDelta
            RichTextBox.Height = Height - textYDelta
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        ComboBox1.Enabled = Not CheckBox1.Checked
    End Sub
End Class
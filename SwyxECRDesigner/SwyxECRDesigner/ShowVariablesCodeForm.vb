Public Class ShowVariablesCodeForm
    Private xOffset, yOffset, buttonsYOffset, cancelXOffset, okXOffset As Integer
    Private ready As Boolean = False

    Private Sub ShowVariablesCodeForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        xOffset = Me.Width - showVariablesCodeRichTextBox.Width
        yOffset = Me.Height - showVariablesCodeRichTextBox.Height
        buttonsYOffset = Height - cancelButton.Location.Y
        cancelXOffset = Width - cancelButton.Location.X
        okXOffset = Width - okButton.Location.X
        ready = True
    End Sub

    Private Sub ShowVariablesCodeForm_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If ready Then
            showVariablesCodeRichTextBox.Width = Me.Width - xOffset
            showVariablesCodeRichTextBox.Height = Me.Height - yOffset
            cancelButton.Location = New Point(Width - cancelXOffset, Height - buttonsYOffset)
            okButton.Location = New Point(Width - okXOffset, Height - buttonsYOffset)
        End If
    End Sub
End Class
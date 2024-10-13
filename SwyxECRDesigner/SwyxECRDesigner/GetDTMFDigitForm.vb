Public Class GetDTMFDigitForm
    Public result As Integer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click, Button2.Click, Button3.Click, Button4.Click, _
    Button5.Click, Button6.Click, Button7.Click, Button8.Click, Button9.Click, Button10.Click, Button11.Click, Button12.Click
        Dim myRef As Button = sender

        Select Case myRef.Text
            Case "*"
                result = 10

            Case "#"
                result = 11

            Case Else
                result = CInt(myRef.Text)
        End Select

        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub GetDTMFDigitForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        result = -1
    End Sub
End Class
Public Class PhoneForm

    Private Sub clearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clearButton.Click
        TextBox1.Clear()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click, Button2.Click, Button3.Click, Button4.Click, Button5.Click, Button6.Click, Button7.Click, Button8.Click, Button9.Click, Button10.Click, Button11.Click, Button12.Click
        Dim buttonRef As Button = sender
        Dim myWavFile As String = buttonRef.Text

        If myWavFile = "*" Then myWavFile = "star"

        myWavFile &= ".wav"
        My.Computer.Audio.Play("C:\SimulatorWavFiles\DTMF\" & myWavFile)
        TextBox1.Text &= buttonRef.Text
    End Sub

    Private Sub dialButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dialButton.Click
        With SimulatorForm.myCDR
            Dim myButtonRef As Button = sender
            Dim myPhoneRef As PhoneForm = myButtonRef.Parent
            Dim myPhoneIndex As Integer = myPhoneRef.Tag

            dialButton.Enabled = False

            Dim connected As Boolean = SimulatorForm.GenericDialHandler(myPhoneIndex, TextBox1.Text, "", "")

            If connected Then
                releaseButton.Enabled = True
            Else
                .SetEndTime()
                .WriteToDatabase()
                TextBox1.Clear()
                dialButton.Enabled = True
            End If
        End With
    End Sub

    Private Sub PhoneForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dialButton.Enabled = True
        releaseButton.Enabled = False
    End Sub

    Private Sub releaseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles releaseButton.Click
        With SimulatorForm.myCDR
            .SetEndTime()
            .WriteToDatabase()
            TextBox1.Clear()
            dialButton.Enabled = True
            releaseButton.Enabled = False

            'If myErrorString <> "" Then MsgBox(myErrorString)
        End With
    End Sub
End Class
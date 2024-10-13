Public Class RestoreForm
    Public myBackups As New Dictionary(Of String, List(Of String))

    Private Sub scriptNameComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles scriptNameComboBox.SelectedIndexChanged
        Dim myScriptName As String = scriptNameComboBox.Text

        backupComboBox.Items.Clear()

        If myScriptName <> "" Then
            For Each myBackupScriptName As String In myBackups.Item(myScriptName)
                backupComboBox.Items.Add(myBackupScriptName)
            Next

            If backupComboBox.Items.Count > 0 Then backupComboBox.SelectedIndex = 0
        End If
    End Sub
End Class
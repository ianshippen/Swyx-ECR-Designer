Public Class CopyFunctionFromDatabaseForm
    Private myScriptList As List(Of String) = Nothing
    Private scriptsPath As String = ""

    Private Sub CopyFunctionFromDatabaseForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckBox1.Checked = True
        myScriptList = DesignerForm.GetScriptNamesInDatabase()
        scriptsPath = DesignerForm.GetScriptsFolder
        ShowFunctions()
    End Sub

    Private Sub ShowFunctions()
        If myScriptList IsNot Nothing Then
            ComboBox1.Items.Clear()

            For Each scriptName As String In myScriptList
                Dim addIt As Boolean = True

                If CheckBox1.Checked Then
                    If DesignerForm.FindScriptFile(scriptsPath, scriptName) <> "" Then addIt = False
                End If

                If addIt Then ComboBox1.Items.Add(scriptName)
            Next

            If ComboBox1.Items.Count > 0 Then ComboBox1.SelectedIndex = 0
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        ShowFunctions()
    End Sub

    Private Sub okButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles okButton.Click
        Dim myScriptName As String = ComboBox1.SelectedItem
        Dim x As New FolderBrowserDialog

        x.SelectedPath = scriptsPath

        If x.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim myFileName As String = x.SelectedPath.TrimEnd("\") & "\" & myScriptName & ".txt"
            Dim y As New IO.StreamWriter(myFileName)
            Dim myCode As List(Of String) = DesignerForm.GetScriptFromDatabase(myScriptName)

            For Each myLine As String In myCode
                y.WriteLine(myLine)
            Next

            y.Close()
            MsgBox("Script " & myScriptName & " has been copied from the database Scripts table to" & vbCrLf & myFileName)
        End If

        DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class
Public Class ScriptNameToFileNameForm
    Dim myDictionary As New Dictionary(Of String, String)

    Private Sub ScriptNameToFileNameForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mySql As New SQLStatementClass
        Dim myTable As New DataTable

        scriptNameComboBox.Items.Clear()
        filenameTextBox.Clear()
        myDictionary.Clear()

        ' Get all script names and their associated source filenames from database
        mySql.SetPrimaryTable("ServiceBuilderTable")
        mySql.AddSelectString("scriptName", "")
        mySql.AddSelectString("data", "")
        mySql.AddCondition("nodeType = 'SIBB_Start'")
        mySql.AddCondition("scriptName NOT LIKE '** % **'")
        mySql.AddOrderByString("scriptName")

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql.GetSQLStatement, myTable) Then
            For i = 0 To myTable.Rows.Count - 1
                With myTable.Rows(i)
                    Dim myScriptName As String = .Item("scriptName")
                    Dim myFileName As String = .Item("data")

                    If myFileName.StartsWith("'Build Info : Source = ") Then
                        myFileName = myFileName.Substring(myFileName.IndexOf(Chr(34)) + 1)
                        myFileName = myFileName.Substring(0, myFileName.IndexOf(Chr(34)))
                        scriptNameComboBox.Items.Add(myScriptName)
                        myDictionary.Add(myScriptName, myFileName)
                    End If
                End With
            Next
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles scriptNameComboBox.SelectedIndexChanged
        Dim myScriptName As String = scriptNameComboBox.SelectedItem

        filenameTextBox.Clear()

        If myDictionary.ContainsKey(myScriptName) Then
            filenameTextBox.Text = myDictionary.Item(myScriptName)
        End If
    End Sub
End Class
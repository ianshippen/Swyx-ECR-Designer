Public Class InfoForm
    Private Sub InfoForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myTimeStamp As String = ""
        Dim myNewCallFlow As Boolean = False
        Dim myScriptNames As New List(Of String)

        infoLabel.Text = sibbList.Count & " nodes"
        pathTextBox.Text = DesignerForm.openFilename

        If pathTextBox.Text <> "" Then
            fileTextBox.Text = pathTextBox.Text.Substring(pathTextBox.Text.LastIndexOf("\") + 1)
        Else
            fileTextBox.Text = "< New Call Flow >"
        End If

        usedByTextBox.Clear()

        If DesignerForm.GetBuildInfoFromDatabase(pathTextBox.Text, myTimeStamp, myNewCallFlow, myScriptNames) Then
            For i = 0 To myScriptNames.Count - 1
                If i > 0 Then usedByTextBox.Text &= vbCrLf

                usedByTextBox.Text &= myScriptNames(i)
            Next
        End If
    End Sub

    Private Sub showIndexCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles showIndexCheckBox.CheckedChanged
        RepaintAll(True)
    End Sub

    Private Sub searchNodesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles searchNodesButton.Click
        If searchTextBox.Text = "" Then
            MsgBox("No search text specified", MsgBoxStyle.Exclamation)
        Else
            Dim foundIt As Boolean = False

            ' Go through all nodes
            ClearAnySelectedNode()
            RepaintAll()

            For i = 0 To sibbList.Count - 1
                With sibbList(i)
                    Dim x As String = searchTextBox.Text.ToLower.Trim
                    Dim y As String = ""

                    If .GetData.ToLower.Contains(x) Then y = "Data"

                    If .GetNodeTitle.ToLower.Contains(x) Then
                        If y <> "" Then y &= ", "
                        y &= "Title"
                    End If

                    If .GetInternalReference.ToLower.Contains(x) Then
                        If y <> "" Then y &= ", "
                        y &= "Internal Reference"
                    End If

                    If .GetFooterTitle.ToLower.Contains(x) Then
                        If y <> "" Then y &= ", "
                        y &= "SIBB Type Footer Title"
                    End If

                    If .GetTypeName.ToLower.Contains(x) Then
                        If y <> "" Then y &= ", "
                        y &= "SIBB Type Name"
                    End If


                    If y <> "" Then
                        .Hilight(True)
                        RepaintAll()
                        MsgBox("Match on found in Node " & i & " [" & .GetNodeTitle & "] " & y, MsgBoxStyle.Information)
                        foundIt = True
                    End If
                End With
            Next

            If foundIt Then
                MsgBox("Search complete ..")
            Else
                MsgBox("No match found", MsgBoxStyle.Information)
            End If
        End If
    End Sub

    Private Sub searchCDRsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CDRSearchForm.ShowDialog()
    End Sub
End Class
Public Class AddComponentsForm
    Dim targetList As New List(Of String)
    Public fromFile As String = ""
    Public modifiedFlag As Boolean = False

    Private Sub ListBox1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles availableComponentsListView.DoubleClick
        modifiedFlag = True

        With availableComponentsListView
            If .SelectedItems.Count >= 0 Then
                If Not Form1.ListViewContainsText(selectedComponentsListView, .SelectedItems(0).Text) Then
                    selectedComponentsListView.Items.Add(.SelectedItems(0).Text)
                    targetList.Add(.SelectedItems(0).Text)

                    ' Check for any needs
                    Dim myIndex As Integer = GetIndexOfComponentFromName(.SelectedItems(0).Text)

                    If myIndex >= 0 Then
                        With Form1.componentList(myIndex)
                            DigNeeds(.needs)
                        End With
                    End If
                End If
            End If
        End With

        With selectedComponentsListView
            For i = 0 To .Items.Count - 1
                Dim flag As Boolean = False
                Dim myIndex As Integer = GetIndexOfComponentFromName(.Items.Item(i).Text)

                ' Does anybody need us ?
                For j = 0 To Form1.componentList.Count - 1
                    If j <> myIndex Then
                        If Form1.componentList(j).needs.Contains(Form1.componentList(myIndex).id) Then
                            flag = True
                            Exit For
                        End If
                    End If
                Next

                .Items.Item(i).ForeColor = Color.Black

                If flag Then
                    ' Somebody needs us. Do we need anybody else ?
                    If Form1.componentList(myIndex).needs.Count > 0 Then .Items.Item(i).ForeColor = Color.RoyalBlue
                Else
                    ' Nobody needs us
                    .Items.Item(i).ForeColor = Color.Fuchsia
                End If
            Next
        End With
    End Sub

    Private Sub ListView2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectedComponentsListView.DoubleClick
        modifiedFlag = True

        With selectedComponentsListView
            If .SelectedIndices.Count >= 0 Then
                Dim myItem As String = .Items(.SelectedIndices(0)).Text

                targetList.Remove(myItem)
                .Items.RemoveAt(.SelectedIndices(0))
            End If
        End With
    End Sub

    Private Sub ListView1_Click(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles availableComponentsListView.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            With availableComponentsListView
                If .SelectedItems.Count >= 0 Then
                    Dim myIndex As Integer = GetIndexOfComponentFromName(.SelectedItems(0).Text)

                    With Form1.componentList(myIndex)
                        If .notes <> "" Then
                            MsgBox(.name & vbCrLf & .notes)
                        Else
                            MsgBox(.name)
                        End If
                    End With
                End If
            End With
        End If
    End Sub

    Public Function GetIndexOfComponentFromName(ByRef p_name As String) As Integer
        Dim result As Integer = -1

        For i = 0 To Form1.componentList.Count - 1
            If Form1.componentList(i).name = p_name Then
                result = i
                Exit For
            End If
        Next

        Return result
    End Function

    Public Function GetIndexOfComponentFromFilename(ByRef p_filename As String) As Integer
        Dim result As Integer = -1

        For i = 0 To Form1.componentList.Count - 1
            If Form1.componentList(i).filename = p_filename Then
                result = i
                Exit For
            End If
        Next

        Return result
    End Function

    Public Function GetIndexOfComponentFromId(ByVal p_id As ComponentClass.IDType) As Integer
        Dim result As Integer = -1

        For i = 0 To Form1.componentList.Count - 1
            If Form1.componentList(i).id = p_id Then
                result = i
                Exit For
            End If
        Next

        Return result
    End Function

    Public Function GetNameOfComponent(ByVal p_id As ComponentClass.IDType) As String
        Dim result As String = ""

        For i = 0 To Form1.componentList.Count - 1
            If Form1.componentList(i).id = p_id Then
                result = Form1.componentList(i).name
                Exit For
            End If
        Next

        Return result
    End Function

    Private Sub DigNeeds(ByRef p_needs As List(Of ComponentClass.IDType))
        For i = 0 To p_needs.Count - 1
            Dim myIndex As Integer = GetIndexOfComponentFromId(p_needs(i))

            If myIndex >= 0 Then DigNeeds(Form1.componentList(myIndex).needs)

            If Not Form1.ListViewContainsText(selectedComponentsListView, GetNameOfComponent(p_needs(i))) Then selectedComponentsListView.Items.Add(GetNameOfComponent(p_needs(i)))
        Next
    End Sub

    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripMenuItem.Click
        selectedComponentsListView.Items.Clear()
        targetList.Clear()
        fromFile = ""
        modifiedFlag = False
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        Dim x As New SaveFileDialog

        x.Filter = "Swyx ECR Designer Config Files|*.cfg"

        If x.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim y As New IO.StreamWriter(x.FileName)

            For i = 0 To targetList.Count - 1
                y.WriteLine(targetList(i))
            Next

            y.Close()
            fromFile = x.FileName
            modifiedFlag = False

            If fromFile.Contains("\") Then fromFile = fromFile.Substring(fromFile.LastIndexOf("\") + 1)
        End If
    End Sub

    Private Sub OpenSwyxECRConfigFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        Dim myOpenFileDialog As New OpenFileDialog

        fromFile = ""
        modifiedFlag = False
        myOpenFileDialog.Filter = "Swyx ECR Designer Config Files|*.cfg"

        If myOpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim myStreamReader As New IO.StreamReader(myOpenFileDialog.FileName)
            Dim reading As Boolean = True

            fromFile = myOpenFileDialog.SafeFileName
            selectedComponentsListView.Items.Clear()
            targetList.Clear()

            While reading
                Dim myLine As String = myStreamReader.ReadLine

                If myLine Is Nothing Then
                    reading = False
                Else
                    targetList.Add(myLine)
                End If
            End While

            myStreamReader.Close()

            For Each myItem As String In targetList
                If Not Form1.ListViewContainsText(selectedComponentsListView, myItem) Then
                    selectedComponentsListView.Items.Add(myItem)

                    ' Check for any needs
                    Dim myIndex As Integer = GetIndexOfComponentFromName(myItem)

                    If myIndex >= 0 Then
                        With Form1.componentList(myIndex)
                            DigNeeds(.needs)
                        End With
                    End If
                End If
            Next

            With selectedComponentsListView
                For i = 0 To .Items.Count - 1
                    Dim flag As Boolean = False
                    Dim myIndex As Integer = GetIndexOfComponentFromName(.Items.Item(i).Text)

                    ' Does anybody need us ?
                    For j = 0 To Form1.componentList.Count - 1
                        If j <> myIndex Then
                            If Form1.componentList(j).needs.Contains(Form1.componentList(myIndex).id) Then
                                flag = True
                                Exit For
                            End If
                        End If
                    Next

                    .Items.Item(i).ForeColor = Color.Black

                    If flag Then
                        ' Somebody needs us. Do we need anybody else ?
                        If Form1.componentList(myIndex).needs.Count > 0 Then .Items.Item(i).ForeColor = Color.RoyalBlue
                    Else
                        ' Nobody needs us
                        .Items.Item(i).ForeColor = Color.Fuchsia
                    End If
                Next
            End With
        End If
    End Sub

    Private Sub AddComponentsForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.ForeColor = Color.Black
        Label2.ForeColor = Color.RoyalBlue
        Label3.ForeColor = Color.Fuchsia
    End Sub
End Class
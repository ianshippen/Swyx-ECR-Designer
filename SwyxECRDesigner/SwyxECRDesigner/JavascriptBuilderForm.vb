Public Class JavascriptBuilderForm
    Private myCommonRoot As String = "C:\Inetpub\wwwroot\Common\"

    Private Sub JavascriptBuilderForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mySourceFolders As New List(Of String)

        commonRootTextBox.Text = myCommonRoot
        mySourceFolders.Add(myCommonRoot & "OpenGL")
        mySourceFolders.Add(myCommonRoot & "Charts")
        mySourceFolders.Add(myCommonRoot & "Shaders")

        ListBox1.Items.Clear()
        ListBox2.Items.Clear()

        For Each myFolderName As String In mySourceFolders
            Dim myFileNames() As String = IO.Directory.GetFiles(myFolderName)

            For Each myFileName As String In myFileNames
                If myFileName.ToLower.EndsWith(".js") Or myFileName.ToLower.EndsWith(".glsl") Then ListBox1.Items.Add(myFileName.Substring(myCommonRoot.Length))
            Next
        Next
    End Sub

    Private Sub ListBox1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListBox1.MouseDoubleClick
        If Not ListBox2.Items.Contains(ListBox1.SelectedItem) Then ListBox2.Items.Add(ListBox1.SelectedItem)
    End Sub

    Private Sub ListBox2_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListBox2.MouseDoubleClick
        ListBox2.Items.Remove(ListBox2.SelectedItem)
    End Sub

    Private Sub okButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles okButton.Click
        Dim mySaveFileDialog As New SaveFileDialog

        mySaveFileDialog.Filter = "Javascript Files|*.js"

        If ListBox2.Items.Count = 0 Then
            MsgBox("Error: No source selected")

            Return
        End If

        If mySaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim myStreamWriter As New IO.StreamWriter(mySaveFileDialog.FileName)
            Dim firstShader As Boolean = True

            myStreamWriter.WriteLine(WrapInQuotes("use strict") & ";")

            For Each myFileName As String In ListBox2.Items
                myFileName = myCommonRoot & myFileName

                Dim z As New IO.StreamReader(myFileName)
                Dim reading As Boolean = True
                Dim shaderFile As Boolean = False

                If myFileName.ToLower.EndsWith(".glsl") Then shaderFile = True

                myStreamWriter.WriteLine()

                If shaderFile Then
                    Dim myShaderText As String = ""

                    If firstShader Then
                        myStreamWriter.WriteLine("var myShaderDictionary = {};")
                        myStreamWriter.WriteLine()
                        firstShader = False
                    End If

                    While reading
                        Dim myLine As String = z.ReadLine

                        If myLine Is Nothing Then
                            reading = False
                        Else
                            myShaderText &= myLine & "\n"
                        End If
                    End While

                    If myShaderText <> "" Then
                        Dim shortFileName As String = myFileName.Substring(myFileName.LastIndexOf("\") + 1)
                        Dim myVariableName As String = shortFileName.Substring(0, shortFileName.LastIndexOf("."))
                        Dim shaderType As String = "x-shader/x-vertex"

                        If myVariableName.ToLower.Contains("fragment") Then shaderType = "x-shader/x-fragment"

                        myStreamWriter.WriteLine("myShaderDictionary[" & WrapInQuotes(myVariableName) & "] = {type: " & WrapInQuotes(shaderType) & ",  text: " & WrapInQuotes(myShaderText) & "};")
                    End If
                Else
                    While reading
                        Dim myLine As String = z.ReadLine

                        If myLine Is Nothing Then
                            reading = False
                        Else
                            myStreamWriter.WriteLine(myLine)
                        End If
                    End While
                End If

                z.Close()
            Next

            myStreamWriter.Close()
        End If
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        Dim myOpenFileDialog As New OpenFileDialog

        myOpenFileDialog.Filter = "Javascript Builder Config Files|*.jfg"

        If myOpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim myStreamReader As New IO.StreamReader(myOpenFileDialog.FileName)
            Dim reading As Boolean = True

            ListBox2.Items.Clear()

            While reading
                Dim myLine As String = myStreamReader.ReadLine

                If myLine Is Nothing Then
                    reading = False
                Else
                    ListBox2.Items.Add(myLine.Substring(myCommonRoot.Length))
                End If
            End While

            myStreamReader.Close()
            Text = "Javascript Builder: " & myOpenFileDialog.FileName
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        Dim mySaveFileDialog As New SaveFileDialog

        mySaveFileDialog.Filter = "Javascript Builder Config Files|*.jfg"

        If mySaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim y As New IO.StreamWriter(mySaveFileDialog.FileName)

            For Each myFileName As String In ListBox2.Items
                y.WriteLine(myCommonRoot & myFileName)
            Next

            y.Close()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ListBox2.SelectedIndex > 0 Then
            Dim myIndex As Integer = ListBox2.SelectedIndex
            Dim temp As String = ListBox2.SelectedItem

            ListBox2.Items(myIndex) = ListBox2.Items(myIndex - 1)
            ListBox2.Items(myIndex - 1) = temp
            ListBox2.SelectedIndex = myIndex - 1
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ListBox2.SelectedIndex < (ListBox2.Items.Count - 1) Then
            Dim myIndex As Integer = ListBox2.SelectedIndex
            Dim temp As String = ListBox2.SelectedItem

            ListBox2.Items(myIndex) = ListBox2.Items(myIndex + 1)
            ListBox2.Items(myIndex + 1) = temp
            ListBox2.SelectedIndex = myIndex + 1
        End If

    End Sub

    Private Sub searchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles searchButton.Click
        If searchTextBox.Text <> "" Then
            ListBox3.Items.Clear()

            For Each myFilename As String In ListBox2.Items
                myFilename = myCommonRoot & myFilename

                Dim x As New IO.StreamReader(myFilename)
                Dim reading As Boolean = True
                Dim myLineNumber As Integer = 0

                While reading
                    Dim myLine As String = x.ReadLine

                    If myLine Is Nothing Then
                        reading = False
                    Else
                        myLineNumber += 1

                        If myLine.ToLower.Contains(searchTextBox.Text.ToLower) Then ListBox3.Items.Add(myFilename.Substring(myCommonRoot.Length) & " : Line " & myLineNumber & " : " & myLine.Trim)
                    End If
                End While

                x.Close()
            Next
        End If
    End Sub
End Class
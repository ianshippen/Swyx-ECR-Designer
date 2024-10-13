Public Class VBEditorForm
    Enum EditorState
        NULL
        GATHERING_TEXT
        STARTING_PARMS
        GETTING_PARMS
    End Enum

    Private Const FONT_SIZE As Integer = 23
    Private Const NORMAL_FONT_ATRRIBUTE As Integer = (0 * 16)
    Private Const BOLD_FONT_ATTRIBUTE As Integer = (1 * 16)
    Private Const BLACK_COLOUR As Integer = 0
    Private Const GREEN_COLOUR As Integer = 1
    Private Const RED_COLOUR As Integer = 2
    Private Const BLUE_COLOUR As Integer = 3

    Dim allowResize As Boolean = False
    Dim xDelta, yDelta, xButtonCancelDelta, xButtonOKDelta, yButtonDelta As Integer
    Dim myFont As Font = vbscriptFont
    Dim myBoldFont As Font = Nothing
    Dim myEditorState As EditorState = EditorState.NULL
    Dim myText As String = ""
    Dim myFunctions() As String = {"SetVar(,)", "GetVar()", "AddVar(,)", "IsVar(,)", "IsVarTrue()", "IsVarInt()", "IncVar()", "DecVar()"}
    Dim myStartIndex As Integer = -1
    Dim myEndIndex As Integer = -1
    Public checkChanges As Boolean = False
    Dim insertIndex As Integer = -1
    Public myRTF As New RTFClass(FONT_SIZE)
    Dim functionStack As New List(Of FunctionDefClass)
    Private myCopiedText As List(Of List(Of CharAndAttributeClass)) = Nothing

    Private Sub VBEditorForm_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If allowResize Then
            With RichTextBox1
                .Size = New Point(Size.Width - xDelta, Size.Height - yDelta)
                cancelButton.Location = New Point(Size.Width - xButtonCancelDelta, Size.Height - yButtonDelta)
                okButton.Location = New Point(Size.Width - xButtonOKDelta, Size.Height - yButtonDelta)
            End With
        End If
    End Sub

    Private Sub VBEditorForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        checkChanges = False
        xDelta = Size.Width - RichTextBox1.Width
        yDelta = Size.Height - RichTextBox1.Height
        xButtonCancelDelta = Size.Width - cancelButton.Location.X
        xButtonOKDelta = Size.Width - okButton.Location.X
        yButtonDelta = Size.Height - cancelButton.Location.Y
        allowResize = True
        myEditorState = EditorState.NULL
        myText = ""
        myBoldFont = New Font(myFont.FontFamily, myFont.Size, FontStyle.Bold)

        ListBox1.Items.Clear()
        ListBox1.Hide()
        VariablesToolStripMenuItem.DropDownItems.Clear()

        With DesignerForm
            For i = 0 To .GetNumberOfServiceWideVariables - 1
                Dim x As New ToolStripMenuItem(.GetServiceWideVariableName(i))

                AddHandler x.Click, AddressOf MyHandler
                VariablesToolStripMenuItem.DropDownItems.Add(x)
                'ListBox1.Items.Add()
            Next
        End With

        myRTF.ClearFonts()
        myRTF.AddFont("Courier New")
        myRTF.AddFont("Courier New Bold")
        ParseVBEditorTextBox()
        RichTextBox1.Rtf = myRTF.Render()
        checkChanges = True
    End Sub

    Private Sub CallMachineText(ByRef p As String)
        Dim myText = "myCallMachine." & p & ".Run"

        myRTF.AddText(myText)
        Render()
    End Sub

    Private Sub StartToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartToolStripMenuItem.Click
        CallMachineText("onCallStart")
    End Sub

    Private Sub InHoursToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InHoursToolStripMenuItem.Click
        CallMachineText("onInHours")
    End Sub

    Private Sub OutOfHoursToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutOfHoursToolStripMenuItem.Click
        CallMachineText("onOutOfHours")
    End Sub

    Private Sub DeliveredToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeliveredToolStripMenuItem.Click
        CallMachineText("onCallDelivered")
    End Sub

    Private Sub ConnectedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectedToolStripMenuItem.Click
        CallMachineText("onCallConnect")
    End Sub

    Private Sub DisconnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisconnectToolStripMenuItem.Click
        CallMachineText("onCallDisconnect")
    End Sub

    Private Sub InitialisationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InitialisationToolStripMenuItem.Click
        With RichTextBox1
            .Text &= "Dim myCallMachine"
            .Text &= vbCrLf & "Set myCallMachine = New CallMachineClass"

            .Text &= vbCrLf & "myCallMachine.onCallStart.BindTargetFromDB"
            .Text &= vbCrLf & "myCallMachine.onInHours.BindTargetFromDB"
            .Text &= vbCrLf & "myCallMachine.onOutOfHours.BindTargetFromDB"
            .Text &= vbCrLf & "myCallMachine.onCallDelivered.BindTargetFromDB"
            .Text &= vbCrLf & "myCallMachine.onCallConnect.BindTargetFromDB"
            .Text &= vbCrLf & "myCallMachine.onCallDisconnect.BindTargetFromDB"

            .Text &= vbCrLf & "myCallMachine.SetDebug True"
            .Text &= vbCrLf & "myCallMachine.onCallStart.Run"
        End With
    End Sub

    Private Sub RichTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.TextChanged
        If checkChanges Then ParseVBEditorTextBox()
    End Sub

    Private Function GetFunctionName(ByVal p_index As Integer) As String
        Return myFunctions(p_index).Substring(0, myFunctions(p_index).IndexOf("("))
    End Function

    Private Function GetNumberOfParmsInFunction(ByVal p_index As Integer) As Integer
        Dim result As Integer = 1

        For i = 0 To myFunctions(p_index).Length - 1
            If myFunctions(p_index)(i) = "," Then result += 1
        Next

        Return result
    End Function

    Private Sub ParseVBEditorTextBox()
        ' Loop over each line of text
        For i = 0 To myRTF.GetNumberOfLines - 1
            Dim myState As EditorState = EditorState.NULL
            Dim myLine As String = myRTF.GetLine(i)

            functionStack.Clear()

            ' Process the line
            For j = 0 To myLine.Length - 1
                Dim myKey As Char = myLine(j)

                Select Case myState
                    Case EditorState.NULL
                        Select Case myKey
                            Case " ", Chr(9)
                                ' Do nothing

                            Case Else
                                myText = myKey
                                myState = EditorState.GATHERING_TEXT
                        End Select

                    Case EditorState.GATHERING_TEXT
                        myText &= myKey

                        Select Case myKey
                            Case " ", Chr(9)
                                myText = ""

                            Case "("
                                ' Is is one of our recognised functions ?
                                Dim myIndex As Integer = -1
                                Dim myAttribute As Byte = BOLD_FONT_ATTRIBUTE

                                For k = 0 To myFunctions.Length - 1
                                    Dim myFunctionAsLower As String = GetFunctionName(k).ToLower

                                    If myText.Substring(0, myText.Length - 1).ToLower = myFunctionAsLower Then
                                        ' Yes
                                        myIndex = k
                                        Exit For
                                    End If
                                Next

                                Dim x As New FunctionDefClass

                                If myIndex >= 0 Then
                                    myText = GetFunctionName(myIndex) & "("
                                    x.numberOfParms = GetNumberOfParmsInFunction(myIndex)
                                    x.ourFunction = True
                                    myAttribute += GREEN_COLOUR
                                Else
                                    myState = EditorState.GETTING_PARMS
                                    myAttribute += BLUE_COLOUR
                                End If

                                functionStack.Insert(0, x)
                                myRTF.SetText(i, j - (myText.Length - 1), myText)
                                myRTF.SetAttribute(i, j - (myText.Length - 1), myText.Length, myAttribute)
                                myText = ""
                                myState = EditorState.GETTING_PARMS
                        End Select

                    Case EditorState.GETTING_PARMS
                        Dim myAttribute As Byte = NORMAL_FONT_ATRRIBUTE + 2

                        myText &= myKey

                        Select Case myKey
                            Case " ", Chr(9)
                                myText = ""

                            Case ")"
                                ' Work out if function is ours or not 
                                Dim isOurs As Boolean = functionStack(0).ourFunction

                                ' Pop the function from the stack
                                functionStack.RemoveAt(0)

                                If functionStack.Count = 0 Then myState = EditorState.NULL

                                myAttribute = BOLD_FONT_ATTRIBUTE

                                If isOurs Then
                                    myAttribute += GREEN_COLOUR
                                Else
                                    myAttribute += BLUE_COLOUR
                                End If

                                myRTF.SetAttribute(i, j, 1, myAttribute)

                            Case "("
                                ' Is is one of our recognised functions ?
                                Dim myIndex As Integer = -1

                                myAttribute = BOLD_FONT_ATTRIBUTE

                                For k = 0 To myFunctions.Length - 1
                                    Dim myFunctionAsLower As String = GetFunctionName(k).ToLower

                                    If myText.Substring(0, myText.Length - 1).ToLower = myFunctionAsLower Then
                                        ' Yes
                                        myIndex = k
                                        Exit For
                                    End If
                                Next

                                Dim x As New FunctionDefClass

                                If myIndex >= 0 Then
                                    myText = GetFunctionName(myIndex) & "("
                                    x.numberOfParms = GetNumberOfParmsInFunction(myIndex)
                                    x.ourFunction = True
                                    myAttribute += GREEN_COLOUR
                                Else
                                    myState = EditorState.GETTING_PARMS
                                    myAttribute += BLUE_COLOUR
                                End If

                                functionStack.Insert(0, x)
                                myRTF.SetText(i, j - (myText.Length - 1), myText)
                                myRTF.SetAttribute(i, j - (myText.Length - 1), myText.Length, myAttribute)
                                myText = ""

                            Case Else
                                myRTF.SetAttribute(i, j, 1, myAttribute)
                        End Select

                End Select
            Next
        Next
    End Sub

    Private Sub ListBox1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.DoubleClick
        Dim myText As String = WrapInQuotes(ListBox1.Text)


        myRTF.AddText(myText)
        Render()
        ListBox1.Hide()
        RichTextBox1.Focus()
    End Sub

    Private Sub RichTextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles RichTextBox1.KeyPress
        Dim myKey As Char = e.KeyChar
        Dim doRender As Boolean = True
        Dim myCode As Integer = Asc(myKey)

        Select Case myCode
            Case 3
                ' CTRL-C Copy
                doRender = False
                Dim a As String = RichTextBox1.Rtf

                myCopiedText = myRTF.GetSelection(RichTextBox1.SelectionStart, RichTextBox1.SelectionLength)

            Case 8
                ' DEL key
                myRTF.Delete()

            Case 13
                ' Start a new line
                myRTF.NewLine()

            Case 22
                ' CTRL-V Paste
                If myCopiedText IsNot Nothing Then
                    Dim myIndex As Integer = RichTextBox1.SelectionStart
                    Dim myLineIndex As Integer = myRTF.MapIndexToLineIndex(myIndex)

                    myRTF.InsertText(myCopiedText, myLineIndex, 0)
                End If

                doRender = False

            Case 27
                ListBox1.Hide()
                doRender = False

            Case Else
                myRTF.Add(myKey)
        End Select

        If doRender Then Render()

        e.Handled = True
    End Sub

    Private Sub Render()
        ParseVBEditorTextBox()
        RichTextBox1.Rtf = myRTF.Render

        Dim myPos As Integer = myRTF.GetCurrentLineLength

        If myPos < 0 Then myPos = 0

        RichTextBox1.Select(myPos, 0)
    End Sub

    Private Sub AddVarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddVarToolStripMenuItem.Click, SetVarToolStripMenuItem.Click, GetVarToolStripMenuItem.Click, IsVarToolStripMenuItem.Click, IsVarIntToolStripMenuItem.Click, IsVarTrueToolStripMenuItem.Click, IncVarToolStripMenuItem.Click, DecVarToolStripMenuItem.Click
        myRTF.AddText(sender.Text & "(")
        Render()
    End Sub

    Private Sub MyHandler(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim myText As String = ""

        If functionStack.Count > 0 Then
            With functionStack(0)
                If .currentParmIndex < .numberOfParms Then
                    If .currentParmIndex < (.numberOfParms - 1) Then
                        ' Not the last parm
                        myText = ", "
                        .currentParmIndex += 1
                    Else
                    End If
                End If
            End With
        End If

        myRTF.AddText(WrapInQuotes(sender.text))
        Render()
    End Sub

    Private Sub RichTextBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.Click
        Dim myIndex As Integer = RichTextBox1.SelectionStart
        Dim myLineIndex As Integer = myRTF.MapIndexToLineIndex(myIndex)

        myRTF.SetLineIndex(myLineIndex)
    End Sub

    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripMenuItem.Click
        myRTF.Clear()
        Render()
    End Sub
End Class
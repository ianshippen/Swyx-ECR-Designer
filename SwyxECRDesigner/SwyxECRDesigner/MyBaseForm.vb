Public Class MyBaseForm
    Private Sub BaseForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myRef As SIBBClass = sibbList(CInt(Tag))

        nodePropertiesDataGridView.Rows.Clear()

        With myRef
            titleTextBox.Text = .GetNodeTitle
            internalReferenceTextBox.Text = .GetInternalReference
            hideInCallTraceCheckBox.Checked = Not .GetShowInCallTrace

            nodeTypeLabel.Text = DesignerForm.sibbTypeList(DesignerForm.GetIndexForType(.GetSIBBType)).GetTypeName
            nodeNumberLabel.Text = Tag

            For i = 0 To .GetOutputCount - 1
                Dim myOrder As Integer = .GetOutputOrder(i)
                Dim myNextNodeIndex As Integer = .GetOutputNextNode(i)
                Dim myNextNodeData As String = ""

                If myOrder = -1 Then myOrder = i

                If myNextNodeIndex >= 0 Then myNextNodeData = sibbList(.GetOutputNextNode(i)).GetNodeTitle & " (Node " & .GetOutputNextNode(i) & ")"

                Dim myRow As String() = {.OutputIsVisible(i), .GetOutputFixedName(i), .GetOutputName(i), myNextNodeData, myOrder}

                nodePropertiesDataGridView.Rows.Add(myRow)
            Next

            nodePropertiesDataGridView.Size = New Point(nodePropertiesDataGridView.Size.Width, 20 + nodePropertiesDataGridView.Rows.Count * 20)
            .SetupForm(TabControl1.TabPages(1))

            Dim myLabelText As String = ""

            For i = 0 To sibbList.Count - 1
                With sibbList(i)
                    For j = 0 To .GetOutputCount - 1
                        If .outputs(j).nextNodeIndex = CInt(Tag) Then
                            If myLabelText <> "" Then myLabelText &= ", "

                            myLabelText &= i & "(" & j & ")"
                        End If
                    Next
                End With
            Next

            fromNodesLabel.Text = "Fed from nodes: " & myLabelText
        End With

        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub okButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles okButton.Click
        Dim myRef As SIBBClass = sibbList(CInt(Tag))

        With myRef
            ' Check that the order numbers are OK
            Dim myOrderList As New List(Of Integer)
            Dim orderIsOK As Boolean = True

            For i = 0 To .GetOutputCount - 1
                Dim myRow As DataGridViewRow = nodePropertiesDataGridView.Rows(i)
                Dim myOrderString As String = myRow.Cells(4).Value
                Dim myOrderOK As Boolean = False

                If IsInteger(myOrderString) Then
                    Dim myOrder As Integer = CInt(myOrderString)

                    If myOrder >= 0 And myOrder < .GetOutputCount Then
                        If Not myOrderList.Contains(myOrder) Then myOrderOK = True
                    End If
                End If

                If myOrderOK Then
                    myOrderList.Add(CInt(myOrderString))
                Else
                    orderIsOK = False
                End If
            Next

            If orderIsOK Then
                .SetNodeTitle(titleTextBox.Text)
                .SetInternalReference(internalReferenceTextBox.Text)
                .SetShowInCallTrace(Not hideInCallTraceCheckBox.Checked)

                ' Copy the link information back
                For i = 0 To .GetOutputCount - 1
                    Dim myRow As DataGridViewRow = nodePropertiesDataGridView.Rows(i)

                    .SetOutputVisible(i, CBool(myRow.Cells(0).Value))
                    .SetOutputName(i, myRow.Cells(2).Value)
                    .SetOutputOrder(i, CInt(myRow.Cells(4).Value))
                Next

                .TakedownForm(TabControl1.TabPages(1))

                DialogResult = Windows.Forms.DialogResult.OK
            Else
                MsgBox("Error: Order numbers must be unique and range from 0 to " & .GetOutputCount - 1)
            End If
        End With

    End Sub

    Private Sub cancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cancelButton.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Public Sub textbox_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim myTextBoxRef As TextBox = TabControl1.TabPages(1).Controls(0)
        Dim first As Boolean = True

        With VBEditorForm
            With .RichTextBox1
                .Font = vbscriptFont
                .ForeColor = vbscriptFontColour
                .Text = ""
                VBEditorForm.checkchanges = False
            End With

            With .myRTF
                .Clear()

                For Each myLine As String In myTextBoxRef.Lines
                    .AddLine(myLine)
                Next
            End With

            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                myTextBoxRef.Clear()

                For i = 0 To .myRTF.GetNumberOfLines - 1
                    Dim myLine As String = .myRTF.GetLine(i)
                    Dim myText As String = ""

                    If Not first Then myText = vbCrLf

                    myText &= myLine
                    myTextBoxRef.AppendText(myText)
                    first = False
                Next

                Return
                For Each myLine As String In VBEditorForm.RichTextBox1.Lines
                    Dim myText As String = ""

                    If Not first Then myText = vbCrLf

                    myText &= myLine
                    myTextBoxRef.AppendText(myText)
                    first = False
                Next
            End If
        End With
    End Sub
End Class
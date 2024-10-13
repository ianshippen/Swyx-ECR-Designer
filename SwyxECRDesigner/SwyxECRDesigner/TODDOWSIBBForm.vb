Public Class TODDOWSIBBForm
    Private Const EDIT_OPTION As Integer = 0
    Private myOptions() As String = {"Edit ..", EDIT_OPTION}

    Private Sub TODDOWSIBBForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If myTree.Nodes.Count = 0 Then
            myTree.Nodes.Add("")

            With myTree.Nodes(0).Nodes
                .Add("")
                .Add("")
            End With

            With myTree.Nodes(0).Nodes(0).Nodes
                .Add("M")
                .Add("T")
                .Add("")
                .Add("")
                .Add("")
            End With

            With myTree.Nodes(0).Nodes(1).Nodes
                .Add("")
                .Add("")
            End With

            AddContextMenuHandlers()
        End If

        UpdateTree()
    End Sub

    Private Sub AddContextMenuHandlers()
        With myTree
            ' All Days
            .Nodes(0).ContextMenuStrip = GetContextMenuStrip(myOptions, 0)

            ' Week Days
            .Nodes(0).Nodes(0).ContextMenuStrip = GetContextMenuStrip(myOptions, 1)

            ' Week Ends
            .Nodes(0).Nodes(1).ContextMenuStrip = GetContextMenuStrip(myOptions, 2)

            ' Mon - Fri
            .Nodes(0).Nodes(0).Nodes(0).ContextMenuStrip = GetContextMenuStrip(myOptions, 3)
            .Nodes(0).Nodes(0).Nodes(1).ContextMenuStrip = GetContextMenuStrip(myOptions, 4)
            .Nodes(0).Nodes(0).Nodes(2).ContextMenuStrip = GetContextMenuStrip(myOptions, 5)
            .Nodes(0).Nodes(0).Nodes(3).ContextMenuStrip = GetContextMenuStrip(myOptions, 6)
            .Nodes(0).Nodes(0).Nodes(4).ContextMenuStrip = GetContextMenuStrip(myOptions, 7)

            ' Sat - Sun
            .Nodes(0).Nodes(1).Nodes(0).ContextMenuStrip = GetContextMenuStrip(myOptions, 8)
            .Nodes(0).Nodes(1).Nodes(1).ContextMenuStrip = GetContextMenuStrip(myOptions, 9)
        End With
    End Sub

    Private Function GetContextMenuStrip(ByRef p_array() As String, ByRef p_tagBase As String) As ContextMenuStrip
        Dim x As New ContextMenuStrip
        Dim myTagBase As String = p_tagBase

        If Not myTagBase = "" Then myTagBase &= ","

        For i = 0 To p_array.Count - 1 Step 2
            x.Items.Add(p_array(i), Nothing, AddressOf MyContextMenuStripEventHandler)
            x.Items(i \ 2).Tag = myTagBase & p_array(i + 1)
        Next

        Return x
    End Function

    Sub MyContextMenuStripEventHandler(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim myRef As TODDOWSIBBClass = sibbList(CInt(Tag))

        With myRef
            If sender.tag IsNot Nothing Then
                Dim myTag As String = sender.tag
                Dim myArray() As String = myTag.Split(",")
                Dim myPtr As OpeningTimesClass = Nothing

                Select Case myArray(0)
                    Case "0"
                        myPtr = .allDaysOpeningTimes

                    Case "1"
                        myPtr = .weekDaysOpeningTimes

                    Case "2"
                        myPtr = .weekEndsOpeningTimes

                    Case "3"
                        myPtr = .monOpeningTimes

                    Case "4"
                        myPtr = .tueOpeningTimes

                    Case "5"
                        myPtr = .wedOpeningTimes

                    Case "6"
                        myPtr = .thuOpeningTimes

                    Case "7"
                        myPtr = .friOpeningTimes

                    Case "8"
                        myPtr = .satOpeningTimes

                    Case "9"
                        myPtr = .sunOpeningTimes
                End Select

                If myPtr IsNot Nothing Then
                    CopyToTimeForm(myPtr)

                    '   If TimeForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    CopyFromTimeForm(myPtr)
                    UpdateTree()
                    'End If
                End If
            End If
        End With

        UpdateTree()
    End Sub

    Private Sub CopyToTimeForm(ByRef p As OpeningTimesClass)
        'TimeForm.openingTimeTextBox.Text = p.openingMinute.ToString
        ' TimeForm.closingTimeTextBox.Text = p.closingMinute.ToString
    End Sub

    Private Sub CopyFromTimeForm(ByRef p As OpeningTimesClass)
        '    p.openingMinute.Set(TimeForm.openingTimeTextBox.Text)
        '   p.closingMinute.Set(TimeForm.closingTimeTextBox.Text)
    End Sub

    Private Sub UpdateTree()
        Dim myRef As TODDOWSIBBClass = sibbList(CInt(Tag))

        With myTree
            .Nodes(0).Text = "All Days " & myRef.allDaysOpeningTimes.ToString

            With .Nodes(0)
                .Nodes(0).Text = "Week Days " & myRef.weekDaysOpeningTimes.ToString
                .Nodes(1).Text = "Week Ends " & myRef.weekEndsOpeningTimes.ToString

                With .Nodes(0)
                    .Nodes(0).Text = "Monday    " & myRef.monOpeningTimes.ToString
                    .Nodes(1).Text = "Tuesday   " & myRef.tueOpeningTimes.ToString
                    .Nodes(2).Text = "Wednesday " & myRef.wedOpeningTimes.ToString
                    .Nodes(3).Text = "Thursday  " & myRef.thuOpeningTimes.ToString
                    .Nodes(4).Text = "Friday    " & myRef.friOpeningTimes.ToString
                End With

                With .Nodes(1)
                    .Nodes(0).Text = "Saturday  " & myRef.satOpeningTimes.ToString
                    .Nodes(1).Text = "Sunday    " & myRef.sunOpeningTimes.ToString
                End With
            End With
        End With
    End Sub
End Class
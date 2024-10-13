Module Keyboard
    Public Sub KeyPressedHandler(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Select Case Asc(e.KeyChar)
            ' Check for the Escape key pressed
            Case 27
                Select Case GetGUIState()
                    Case GUIStateType.OUTPUT_SELECTED, GUIStateType.HEADING_SELECTED, GUIStateType.HEADINGS_SELECTED, GUIStateType.MOVING_NODE
                        ClearAnySelectedNode()
                        SetGUIState(GUIStateType.NULL)
                        RepaintAll()

                    Case GUIStateType.DROPPING_NEW_NODE
                        ' Remove it from the end of the node list
                        sibbList(sibbList.Count - 1).EraseFrame()
                        sibbList.RemoveAt(sibbList.Count - 1)
                        SetGUIState(GUIStateType.NULL)
                        MyRefresh()

                    Case GUIStateType.SELECTION_BOX_DRAWING, GUIStateType.SELECTION_COMPLETE
                        SetGUIState(GUIStateType.NULL)
                        MyRefresh()
                End Select
        End Select
    End Sub

    Public Sub KeyDownHandler(ByVal e As System.Windows.Forms.KeyEventArgs)
        Select Case e.KeyCode
            Case 27
                ' Escape
                DebugForm.RemoveToolTip()
                DebugForm.UnselectAnySelectedNodes()
                DebugForm.ResetDebug()
                RepaintAll()

            Case 116
                ' F5 - Run
                Dim tableName As String = "ServiceBuilderDebugTable"

                DebugForm.DebugMode = DebugForm.DebugModeEnum.TRACE_MODE

                If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), "update " & tableName & " set state = 'run' where callId = 0") Then
                End If

            Case 118
                ' F7 Step backwards
                DebugForm.ReplayStepHandler(-1)

            Case 119
                ' F8 - Step
                Dim debugMode As Boolean = False

                If debugMode Then
                    Dim tableName As String = "ServiceBuilderDebugTable"

                    For i = 0 To sibbList.Count - 1
                        If sibbList(i).IsStepDebugSelected Then
                            sibbList(i).DebugStepSelect(False)
                            sibbList(i).DebugSelect(True)
                            MyRefresh()
                            Exit For
                        End If
                    Next

                    If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), "update " & tableName & " set state = 'step' where callId = 0") Then
                    End If
                Else
                    DebugForm.ReplayStepHandler(1)
                End If
        End Select
    End Sub
End Module

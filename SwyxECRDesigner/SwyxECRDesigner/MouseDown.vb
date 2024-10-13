Module MouseDown
    Public lastMousePosition As New ScreenLocationClass

    Public Sub MouseDownHandler(ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim mousePosition As New ScreenLocationClass(e.X, e.Y)

        Select Case e.Button
            Case MouseButtons.Left
                MouseLeftButtonDownHandler(mousePosition)

            Case MouseButtons.Right
                MouseRightButtonDownHandler(mousePosition)
        End Select
    End Sub

    Private Sub MouseLeftButtonDownHandler(ByRef p_mousePosition As ScreenLocationClass)
        Dim doRefresh As Boolean = False
        Dim mySIBBIndex As Integer = -1
        Dim outputIndex As Integer = -1
        Dim headingSelected As Boolean = False
        Dim footerSelected As Boolean = False
        Dim outputSelected As Boolean = False
        Dim repaint As Boolean = False
        Dim allDone As Boolean = False

        ' Get some generic info on where we are
        For i = 0 To sibbList.Count - 1
            With sibbList(i)
                If .MouseInRangeOfTitle(p_mousePosition) Then
                    ' We have clicked on a SIBB heading
                    mySIBBIndex = i
                    headingSelected = True
                Else
                    If .MouseInRangeOfFooter(p_mousePosition) Then
                        ' We have clicked on a SIBB footer
                        mySIBBIndex = i
                        footerSelected = True
                    Else
                        outputIndex = .MouseInRangeOfOutput(p_mousePosition)

                        If outputIndex >= 0 Then
                            ' We have clicked on a SIBB output
                            mySIBBIndex = i
                            outputSelected = True
                        End If
                    End If
                End If
            End With

            If mySIBBIndex >= 0 Then Exit For
        Next

        Select Case GetGUIState()
            Case GUIStateType.NULL
                If headingSelected Or footerSelected Then
                    sibbList(mySIBBIndex).Hilight(True)
                    mouseDownPos.Set(p_mousePosition)
                    SetGUIState(GUIStateType.HEADING_SELECTED_MOUSE_DOWN)
                Else
                    If outputSelected Then
                        ClearAnySelectedNode()

                        sibbList(mySIBBIndex).SetOutputSelected(outputIndex, True)
                        sibbList(mySIBBIndex).SetOutputDebugSelected(outputIndex, True)
                        SetGUIState(GUIStateType.OUTPUT_SELECTED)
                        sibbList(mySIBBIndex).PaintYourself(True)
                        MyRefresh()
                        allDone = True
                    Else
                        ' Outside a node, start drawing a selection box
                        ClearAnySelectedNode()
                        mouseDownPos.Set(p_mousePosition)
                        SetGUIState(GUIStateType.SELECTION_BOX_MOUSE_DOWN)
                    End If
                End If


            Case GUIStateType.HEADING_SELECTED
                If headingSelected Or footerSelected Then
                    Dim previousSelection As Integer = -1

                    For i = 0 To sibbList.Count - 1
                        If sibbList(i).IsHilighted Then
                            previousSelection = i
                            Exit For
                        End If
                    Next

                    If mySIBBIndex = previousSelection Then
                        sibbList(mySIBBIndex).Hilight(False)
                        SetGUIState(GUIStateType.NULL)
                    Else
                        If Control.ModifierKeys = Keys.Control Then
                            sibbList(mySIBBIndex).Hilight(True)
                            SetGUIState(GUIStateType.HEADINGS_SELECTED)
                        Else
                            If previousSelection >= 0 Then sibbList(previousSelection).Hilight(False)

                            sibbList(mySIBBIndex).Hilight(True)
                        End If
                    End If
                Else
                    ' Clear any selected nodes
                    ClearAnySelectedNode()
                    SetGUIState(GUIStateType.NULL)
                End If

            Case GUIStateType.HEADINGS_SELECTED
                If headingSelected Or footerSelected Then
                    ' Have we selected a node that was already selected ?
                    If sibbList(mySIBBIndex).IsHilighted Then
                        ' Yes. Deselect it
                        Dim nodesSelected As Integer = 0

                        sibbList(mySIBBIndex).Hilight(False)

                        ' If there is only one node selected, change state
                        For i = 0 To sibbList.Count - 1
                            If sibbList(i).IsHilighted Then nodesSelected += 1
                        Next

                        If nodesSelected = 1 Then SetGUIState(GUIStateType.HEADING_SELECTED)
                    Else
                        If Control.ModifierKeys = Keys.Control Then
                            sibbList(mySIBBIndex).Hilight(True)
                        Else
                            ClearAnySelectedNode()
                            sibbList(mySIBBIndex).Hilight(True)
                            SetGUIState(GUIStateType.HEADING_SELECTED)
                        End If
                    End If
                End If

            Case GUIStateType.OUTPUT_SELECTED
                If headingSelected Or footerSelected Then
                    Dim sourceNodeIndex As Integer = -1
                    Dim sourceOutputIndex As Integer = -1

                    For i = 0 To sibbList.Count - 1
                        For j = 0 To sibbList(i).GetOutputCount - 1
                            If sibbList(i).OutputIsSelected(j) Then
                                sourceNodeIndex = i
                                sourceOutputIndex = j
                                Exit For
                            End If
                        Next

                        If sourceNodeIndex >= 0 Then Exit For
                    Next

                    sibbList(sourceNodeIndex).SetOutputNextNode(sourceOutputIndex, mySIBBIndex)
                    sibbList(sourceNodeIndex).SetOutputSelected(sourceOutputIndex, False)
                    sibbList(sourceNodeIndex).SetOutputDebugSelected(sourceOutputIndex, False)
                    SetGUIState(GUIStateType.NULL)
                End If

                If outputSelected Then
                    sibbList(mySIBBIndex).SetOutputSelected(outputIndex, False)
                    sibbList(mySIBBIndex).SetOutputDebugSelected(outputIndex, False)
                    SetGUIState(GUIStateType.NULL)
                End If

            Case GUIStateType.MOVING_NODE
                mySIBBIndex = -1

                For i = 0 To sibbList.Count - 1
                    If sibbList(i).IsHilighted Then
                        mySIBBIndex = i
                        Exit For
                    End If
                Next

                If mySIBBIndex >= 0 Then
                    Dim mouseDelta As New DeltaLocationClass(mouseDownPos, p_mousePosition)

                    sibbList(mySIBBIndex).absoluteLocation += mouseDelta
                    SetGUIState(GUIStateType.NULL)
                End If

            Case GUIStateType.DROPPING_NEW_NODE
                mySIBBIndex = sibbList.Count - 1

                sibbList(mySIBBIndex).EraseFrame()
                sibbList(mySIBBIndex).SetPos(lastFramePos)
                sibbList(mySIBBIndex).PaintYourself()
                SetGUIState(GUIStateType.NULL)
                doRefresh = True
        End Select

        If Not allDone Then
            If mySIBBIndex >= 0 Then
                'sibbList(mySIBBIndex).PaintYourself()
                'MyRefresh()
                repaint = True
                doRefresh = True
            End If
        End If

        If repaint Then RepaintAll()

        If doRefresh Then MyRefresh()
    End Sub

    Private Sub MouseRightButtonDownHandler(ByRef p_mousePosition As ScreenLocationClass)
        lastMousePosition.CopyFrom(p_mousePosition)
    End Sub
End Module

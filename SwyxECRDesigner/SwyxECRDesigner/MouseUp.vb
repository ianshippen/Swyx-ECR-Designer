Module MouseUp
    Public Sub MouseUpHandler(ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim mousePosition As New ScreenLocationClass(e.X, e.Y)

        Select Case e.Button
            Case MouseButtons.Left
                MouseLeftButtonUpHandler(mousePosition)
        End Select
    End Sub

    Private Sub MouseLeftButtonUpHandler(ByRef p_mousePosition As ScreenLocationClass)
        Dim doRefresh As Boolean = False

        Select Case GetGUIState()
            Case GUIStateType.HEADING_SELECTED_MOUSE_DOWN
                SetGUIState(GUIStateType.HEADING_SELECTED)

            Case GUIStateType.MOVING_NODE
                Dim myIndex As Integer = -1

                For i = 0 To sibbList.Count - 1
                    If sibbList(i).IsHilighted Then
                        myIndex = i
                        Exit For
                    End If
                Next

                If myIndex >= 0 Then
                    Dim mouseDelta As New DeltaLocationClass(mouseDownPos, p_mousePosition)

                    sibbList(myIndex).absoluteLocation.Add(mouseDelta)
                    sibbList(myIndex).Hilight(False)
                    RepaintAll()
                End If

                SetGUIState(GUIStateType.NULL)

            Case GUIStateType.SELECTION_BOX_MOUSE_DOWN
                SetGUIState(GUIStateType.NULL)

            Case GUIStateType.SELECTION_BOX_DRAWING
                ' Select all the nodes inside the box
                Dim nodesSelected As Integer = 0

                For i = 0 To sibbList.Count - 1
                    With sibbList(i)
                        If drawFrameObject.GetX < .absoluteLocation.GetX And drawFrameObject.GetY < .absoluteLocation.GetY Then
                            If lastFramePos.GetX + drawFrameObject.GetWidth > .absoluteLocation.GetX + GetSIBBWidth() And lastFramePos.GetY + drawFrameObject.GetHeight > .absoluteLocation.GetY + .GetHeight Then
                                nodesSelected += 1
                                .Hilight(True)
                                doRefresh = True
                            End If
                        End If
                    End With
                Next

                If nodesSelected > 2 Then nodesSelected = 2

                Select Case nodesSelected
                    Case 0
                        SetGUIState(GUIStateType.NULL)

                    Case 1
                        SetGUIState(GUIStateType.HEADING_SELECTED)

                    Case 2
                        SetGUIState(GUIStateType.HEADINGS_SELECTED)
                End Select

                doRefresh = True
        End Select

        If doRefresh Then MyRefresh()
    End Sub
End Module

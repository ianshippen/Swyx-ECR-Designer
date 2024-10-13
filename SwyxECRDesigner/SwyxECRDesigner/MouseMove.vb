Imports System.Math

Module MouseMove
    Public Sub MouseMoveHandler(ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim doRefresh As Boolean = False
        Dim mousePosition As New ScreenLocationClass(e.X, e.Y)
        Dim mouseDelta As New DeltaLocationClass(mouseDownPos, mousePosition)

        Select Case GetGUIState()
            Case GUIStateType.HEADING_SELECTED_MOUSE_DOWN

                If mouseDelta.Length > INITIAL_MOUSE_MOVE Then
                    Dim myIndex As Integer = -1

                    ' Get the index of the hilighted node we are moving
                    For i = 0 To sibbList.Count - 1
                        If sibbList(i).IsHilighted Then
                            myIndex = i
                            Exit For
                        End If
                    Next

                    If myIndex >= 0 Then
                        With sibbList(myIndex)
                            .EraseMe()
                            .DrawFrame(.GetPos() + mouseDelta)
                            doRefresh = True
                        End With
                    End If

                    SetGUIState(GUIStateType.MOVING_NODE)
                End If

            Case GUIStateType.MOVING_NODE
                Dim myIndex As Integer = -1

                ' Get the index of the hilighted node we are moving
                For i = 0 To sibbList.Count - 1
                    If sibbList(i).IsHilighted Then
                        myIndex = i
                        Exit For
                    End If
                Next

                If myIndex >= 0 Then
                    With sibbList(myIndex)
                        .EraseFrame()
                        .DrawFrame(sibbList(myIndex).GetPos() + mouseDelta)
                        doRefresh = True
                    End With
                End If

                ' Drawing a connection ?
                If connectingFromIndex >= 0 Then
                    Dim myGraphics As Graphics = Graphics.FromImage(DesignerForm.myMasterBitmap)
                    Dim p0 As AbsoluteLocationClass = sibbList(connectingFromIndex).GetConnectionStartPosition(connectingFromOutputIndex)

                    ' myGraphics.DrawLine(Pens.Red, p0.x, p0.y, e.X, e.Y)
                    myGraphics.Dispose()
                    doRefresh = True
                End If

            Case GUIStateType.DROPPING_NEW_NODE
                Dim myIndex As Integer = sibbList.Count - 1

                If myIndex >= 0 Then
                    lastFramePos.Set(ScreenToAbsolutePosition(mousePosition))
                    sibbList(myIndex).EraseFrame()
                    sibbList(myIndex).DrawFrame(lastFramePos)
                    doRefresh = True
                End If

            Case GUIStateType.SELECTION_BOX_MOUSE_DOWN
                If Abs(mouseDelta.GetX) >= INITIAL_MOUSE_MOVE Then
                    If Abs(mouseDelta.GetY) >= INITIAL_MOUSE_MOVE Then
                        ' Draw the first selection frame
                        drawFrameObject = New DrawFrameClass(DesignerForm.myMasterBitmap)
                        drawFrameObject.DrawFrame(ScreenToAbsolutePosition(mouseDownPos), mouseDelta.GetX, mouseDelta.GetY)

                        SetGUIState(GUIStateType.SELECTION_BOX_DRAWING)
                        doRefresh = True
                    End If
                End If

            Case GUIStateType.SELECTION_BOX_DRAWING
                    drawFrameObject.EraseFrame()
                    drawFrameObject.DrawFrame(ScreenToAbsolutePosition(mouseDownPos), mouseDelta.GetX, mouseDelta.GetY)
                    doRefresh = True
        End Select

        If doRefresh Then MyRefresh()
    End Sub
End Module

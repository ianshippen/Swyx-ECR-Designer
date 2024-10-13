Module SharedGUI
    Enum GUIStateType
        NULL
        HEADING_SELECTED
        HEADINGS_SELECTED
        OUTPUT_SELECTED
        MOVING_NODE
        HEADING_SELECTED_MOUSE_DOWN
        DROPPING_NEW_NODE
        SELECTION_BOX_MOUSE_DOWN
        SELECTION_BOX_DRAWING
        SELECTION_COMPLETE
    End Enum

    Public Const DISPLAY_STATE As Boolean = False
    Public Const INITIAL_MOUSE_MOVE As Integer = 4

    Public sibbList As New List(Of SIBBClass)
    Public lastFramePos As New AbsoluteLocationClass
    Public draggingNodeIndex As Integer = -1
    Public connectingFromIndex As Integer = -1
    Public connectingFromOutputIndex As Integer = -1
    Private guiState As GUIStateType = GUIStateType.NULL
    Public drawFrameObject As DrawFrameClass = Nothing
    Public mouseDownPos As New ScreenLocationClass

    Public Function GetGUIState() As GUIStateType
        Return guiState
    End Function

    Public Sub SetGUIState(ByVal p_guiState As GUIStateType)
        guiState = p_guiState

        If p_guiState = GUIStateType.NULL Then
            If Not drawFrameObject Is Nothing Then
                drawFrameObject.EraseFrame()
                drawFrameObject = Nothing
            End If
        End If

        If DISPLAY_STATE Then DesignerForm.Text = p_guiState.ToString
    End Sub

    Public Function ClearAnySelectedNode() As Boolean
        Dim result As Boolean = False

        For i = 0 To sibbList.Count - 1
            With sibbList(i)
                If .IsHilighted Then
                    .Hilight(False)
                    result = True
                End If

                For j = 0 To .GetOutputCount - 1
                    If .OutputIsSelected(j) Then
                        .SetOutputSelected(j, False)
                        .SetOutputDebugSelected(j, False)
                        result = True
                    End If
                Next
            End With
        Next

        Return result
    End Function

    Public Sub RepaintAll(Optional ByVal p_preserveRoutes As Boolean = False)
        Dim maxX As Integer = 0
        Dim maxY As Integer = 0

        DesignerForm.ClearMasterBitmap()
        DrawAllConnections(p_preserveRoutes)

        For i = 0 To sibbList.Count - 1
            With sibbList(i)
                Dim newX As Integer = .absoluteLocation.GetX + GetSIBBWidth()
                Dim newY As Integer = .absoluteLocation.GetY + .GetHeight

                .PaintYourself()

                If newX > maxX Then maxX = newX
                If newY > maxY Then maxY = newY
            End With
        Next

        ' If any connection is selected then repaint it
        For i = 0 To sibbList.Count - 1
            With sibbList(i)
                For j = 0 To .GetOutputCount - 1
                    If .OutputIsSelected(j) Then
                        ' Ask this SIBB to repaint itself
                        .PaintYourself()
                    End If
                Next
            End With
        Next

        MyRefresh()
    End Sub

    Public Function ScreenToAbsolutePosition(ByRef p As ScreenLocationClass) As AbsoluteLocationClass
        Dim z As New AbsoluteLocationClass(p.GetX + DesignerForm.HScrollBar1.Value, p.GetY + DesignerForm.VScrollBar1.Value)

        Return z
    End Function

    Public Function AbsoluteToScreenPosition(ByRef p As AbsoluteLocationClass) As ScreenLocationClass
        Dim z As New ScreenLocationClass(p.GetX - DesignerForm.HScrollBar1.Value, p.GetY - DesignerForm.VScrollBar1.Value)

        Return z
    End Function

    Public Function GetScrollValues() As ComplexClass
        Dim z As New ComplexClass

        With DesignerForm
            z.Set(.HScrollBar1.Value, .VScrollBar1.Value)
        End With

        Return z
    End Function

    Public Sub MyRefresh()
        Dim g As Graphics = Graphics.FromImage(DesignerForm.myViewBitmap)

        Dim sourceRectangle As New Rectangle(DesignerForm.HScrollBar1.Value, DesignerForm.VScrollBar1.Value, DesignerForm.myViewBitmap.Width, DesignerForm.myViewBitmap.Height)
        Dim destRectangle As New Rectangle(0, 0, DesignerForm.myViewBitmap.Width, DesignerForm.myViewBitmap.Height)

        g.DrawImage(DesignerForm.myMasterBitmap, destRectangle, sourceRectangle, GraphicsUnit.Pixel)
        g = Nothing

        SetScrollBars()
        DesignerForm.Refresh()
    End Sub

    Private Sub SetScrollBars()
        With DesignerForm
            .HScrollBar1.Maximum = .myMasterBitmap.Width - 1
            .HScrollBar1.LargeChange = .myViewBitmap.Width

            .VScrollBar1.Maximum = .myMasterBitmap.Height - 1
            .VScrollBar1.LargeChange = .myViewBitmap.Height
        End With
    End Sub

    Public Sub DrawSelectionBox()

    End Sub

    Public Sub SafeSetPixel(ByRef p_bitmapRef As Bitmap, ByVal p_x As Integer, ByVal p_y As Integer, ByVal p_colour As Integer)
        If p_x >= 0 And p_x < p_bitmapRef.Width And p_y >= 0 And p_y < p_bitmapRef.Height Then
            p_bitmapRef.SetPixel(p_x, p_y, Color.FromArgb(p_colour))
        End If
    End Sub

    Public Function SafeGetPixel(ByRef p_bitmapRef As Bitmap, ByVal p_x As Integer, ByVal p_y As Integer) As Integer
        Dim result As Integer = 0

        If p_x >= 0 And p_x < p_bitmapRef.Width And p_y >= 0 And p_y < p_bitmapRef.Height Then
            result = p_bitmapRef.GetPixel(p_x, p_y).ToArgb
        End If

        Return result
    End Function

    Public Function WithinBounds(ByRef p As LocationBaseClass, ByRef topLeft As LocationBaseClass, ByVal p_width As Integer, ByVal p_height As Integer) As Boolean
        Return WithinBounds(p, topLeft.GetX, topLeft.GetY, p_width, p_height)
    End Function

    Public Function WithinBounds(ByRef p As LocationBaseClass, ByRef p_x As Integer, ByVal p_y As Integer, ByVal p_width As Integer, ByVal p_height As Integer) As Boolean
        Dim result As Boolean = False

        If p.GetX >= p_x Then
            If p.GetX < p_x + p_width Then
                If p.GetY >= p_y Then
                    If p.GetY < p_y + p_height Then result = True
                End If
            End If
        End If

        Return result
    End Function

    Public mySimulatorFormRef As SimulatorForm = Nothing
End Module

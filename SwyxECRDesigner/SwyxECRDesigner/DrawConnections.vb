Module DrawConnections
    Public Enum Directions
        NONE
        NORTH
        EAST
        SOUTH
        WEST
    End Enum

    Public Const USE_NEW_DRAW As Boolean = False
    Private Const MERGE_EXISTING_LINE_SEGMENTS As Boolean = True
    Private Const HANDLE_CROSSOVERS As Boolean = True
    Private Const DUMP_MAP As Boolean = False

    Private myLineSegmentList As New List(Of LineSegmentClass)

    Private Const GAP As Integer = 10

    ' Define the colours of the connection lines
    Private rgbValues() As Color = {Color.FromArgb(171, 72, 0), Color.FromArgb(255, 13, 0), Color.FromArgb(255, 153, 51), Color.FromArgb(9, 186, 0), _
                                Color.FromArgb(255, 140, 230), Color.FromArgb(133, 140, 140), Color.FromArgb(145, 0, 153), Color.FromArgb(0, 0, 0), _
                                Color.FromArgb(0, 29, 242), Color.FromArgb(25, 158, 182), Color.FromArgb(123, 232, 158)}

    Private rgbIndexes() As Integer = {7, 4, 9, 0, 1, 2, 3, 5, 6, 10, 8}

    Private Function GetOppositeDirection(ByVal p_direction As Directions) As Directions
        Dim result As Directions = Directions.NONE

        Select Case p_direction
            Case Directions.NORTH
                result = Directions.SOUTH

            Case Directions.SOUTH
                result = Directions.NORTH

            Case Directions.EAST
                result = Directions.WEST

            Case Directions.WEST
                result = Directions.EAST
        End Select

        Return result
    End Function

    Private Function GetLowest(ByVal p1 As Integer, ByVal p2 As Integer) As Integer
        Dim result = p1

        If p2 < p1 Then result = p2

        Return result
    End Function

    Private Function GetHighest(ByVal p1 As Integer, ByVal p2 As Integer) As Integer
        Dim result = p1

        If p2 > p1 Then result = p2

        Return result
    End Function

    Public Sub DrawConnection(ByRef p_sibbRef As SIBBClass, ByRef p_graphics As Graphics, ByVal p_outputIndex As Integer)
        With p_sibbRef
            Dim points As New List(Of AbsoluteLocationClass)
            Dim myColour = rgbValues(rgbIndexes((p_sibbRef.GetNodeIndex + p_outputIndex) Mod rgbValues.Count))
            Dim myGapIndex As Integer = -1

            Dim pStart As AbsoluteLocationClass = .GetConnectionStartPosition(p_outputIndex)
            Dim pEnd As AbsoluteLocationClass = sibbList(.outputs(p_outputIndex).nextNodeIndex).GetConnectionEndPosition()
            Dim p3, p4, p5, p6 As AbsoluteLocationClass

            p3 = pStart + New AbsoluteLocationClass(GAP, 0)

            ' Is there already a previous output from this node going to the same next node - if so, use the same colour
            For i = 0 To p_outputIndex - 1
                If .outputs(i).nextNodeIndex = .outputs(p_outputIndex).nextNodeIndex Then
                    myColour = rgbValues(rgbIndexes((.GetNodeIndex + i) Mod rgbValues.Count))
                    Exit For
                End If
            Next

            ' Is there already an output from a previous node going to the same next node - if so, use the same colour
            Dim foundIt As Boolean = False

            For i = 0 To .GetNodeIndex - 1
                With sibbList(i)
                    For j = 0 To .outputs.Count - 1
                        If .outputs(j).nextNodeIndex = p_sibbRef.outputs(p_outputIndex).nextNodeIndex Then
                            myColour = rgbValues(rgbIndexes((i + j) Mod rgbValues.Count))
                            foundIt = True
                            Exit For
                        End If
                    Next
                End With

                If foundIt Then Exit For
            Next

            ' Work out how far we need to space ourselves to clear existing connections from this node
            Dim myList As New List(Of Integer)
            Dim myNextNode As Integer = .outputs(p_outputIndex).nextNodeIndex

            ' Loop over all the outputs before us that are already connected, and build list of unique nextNode indexes
            For i = 0 To p_outputIndex - 1
                With .outputs(i)
                    If .nextNodeIndex >= 0 Then
                        If Not myList.Contains(.nextNodeIndex) Then myList.Add(.nextNodeIndex)
                    End If
                End With
            Next

            ' Are we about to connect to an existing nextNode for our node - i.e. is there already a connection to it ?
            If myList.Contains(myNextNode) Then
                ' Yes - use the same gap index
                myGapIndex = myList.IndexOf(myNextNode)
            Else
                ' No - use the next gap index
                myGapIndex = myList.Count
            End If

            p3.Add(GAP * myGapIndex, 0)

            If pStart.GetX < pEnd.GetX Then
                ' Left to right
                p4 = p3 + New AbsoluteLocationClass(0, pEnd.GetY() - p3.GetY())

                points.Add(pStart)
                points.Add(p3)
                points.Add(p4)
                points.Add(pEnd)
            Else
                ' Right to left
                p5 = pEnd + New AbsoluteLocationClass(-GAP, (sibbList(.outputs(p_outputIndex).nextNodeIndex).GetYPos - GAP) - pEnd.GetY)
                p6 = New AbsoluteLocationClass
                p4 = New AbsoluteLocationClass
                p6.Set(p5.GetX, pEnd.GetY)
                p4.Set(p3.GetX, p5.GetY)

                points.Add(pStart)
                points.Add(p3)
                points.Add(p4)

                For i = 0 To sibbList.Count - 1
                    With sibbList(i)
                        If .GetXPos >= pEnd.GetX And (.GetXPos + GetSIBBWidth()) <= pStart.GetX Then
                            If p4.GetY >= .GetYPos And p4.GetY <= (.GetYPos + .GetHeight) Then
                                Dim p7, p8, p9, p10 As New AbsoluteLocationClass

                                p7.Set(.GetXPos + GetSIBBWidth() + GAP, p4.GetY)
                                p8.Set(p7.GetX, .GetYPos - GAP)
                                p9.Set(.GetXPos - GAP, p8.GetY)
                                p10.Set(p9.GetX, p4.GetY)

                                points.Add(p7)
                                points.Add(p8)
                                points.Add(p9)
                                points.Add(p10)
                                Exit For
                            End If
                        End If
                    End With
                Next

                points.Add(p5)
                points.Add(p6)
                points.Add(pEnd)
            End If

            DrawRoute(p_sibbRef, p_graphics, myColour, points)
            DrawArrow(p_graphics, myColour, pEnd)
        End With
    End Sub

    Private Sub DrawRouteNew(ByRef p_sibbRef As SIBBClass, ByRef p_graphics As Graphics, ByVal p_colour As Color, ByRef p_lineSegments As List(Of Integer), ByVal p_hilightRoute As Boolean)
        Dim myPenWidth As Integer = 1
        Dim myPen As New Pen(p_colour, myPenWidth)

        For i = 0 To p_lineSegments.Count - 1
            If MyLineNew(p_sibbRef, p_graphics, myPen, p_lineSegments(i), i, p_hilightRoute) Then Exit For
        Next
    End Sub

    Private Sub DrawRoute(ByRef p_sibbRef As SIBBClass, ByRef p_graphics As Graphics, ByVal p_colour As Color, ByRef p_points As List(Of AbsoluteLocationClass))
        Dim myPen As New Pen(p_colour, 1)

        For i = 0 To p_points.Count - 2
            If MyLine(p_sibbRef, p_graphics, myPen, p_points, i) Then Exit For
        Next
    End Sub

    Public Sub DumpLines()
        ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), "delete from hLineListTable")

        For i = 0 To DesignerForm.lineList.hLineList.Count - 1
            With DesignerForm.lineList.hLineList(i)
                Dim x As New SQLStatementClass

                x.SetInsertIntoTable("hLineListTable")
                x.AddValue(.GetNodeIndex)
                x.AddValue(.startPos.GetX)
                x.AddValue(.startPos.GetY)
                x.AddValue(.endPos.GetX)
                x.AddValue(.endPos.GetY)
                x.AddValue(.GetNextNodeIndex)

                ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), x.GetSQLStatement)
            End With
        Next
    End Sub

    Private Function MyLine(ByRef p_sibbRef As SIBBClass, ByRef p_graphics As Graphics, ByRef p_pen As Pen, ByVal p_points As List(Of AbsoluteLocationClass), ByVal p_index As Integer) As Boolean
        Dim breakList As List(Of AbsoluteLocationClass)
        Dim LINE_GAP As Integer = 4
        Dim p1 As New AbsoluteLocationClass(p_points(p_index))
        Dim p2 As New AbsoluteLocationClass(p_points(p_index + 1))
        Dim allDone As Boolean = False
        Dim alreadyExists As Boolean = False

        allDone = DesignerForm.lineList.AddLine(p_sibbRef.GetNodeIndex, p1, p2, alreadyExists)

        If Not alreadyExists Then
            breakList = DesignerForm.lineList.LatestCrossesAnything()

            If breakList.Count = 0 Then
                Dim horizontal As Boolean = IsHorizontal(p1, p2)

                p_graphics.DrawLine(p_pen, p1.GetX, p1.GetY, p2.GetX, p2.GetY)

                If horizontal Then
                    p_graphics.DrawLine(p_pen, p1.GetX, p1.GetY - 1, p2.GetX, p2.GetY - 1)
                    p_graphics.DrawLine(p_pen, p1.GetX, p1.GetY + 1, p2.GetX, p2.GetY + 1)
                Else
                    p_graphics.DrawLine(p_pen, p1.GetX - 1, p1.GetY - 1, p2.GetX - 1, p2.GetY - 1)
                    p_graphics.DrawLine(p_pen, p1.GetX + 1, p1.GetY - 1, p2.GetX + 1, p2.GetY - 1)
                End If
            Else
                For i = 0 To breakList.Count
                    Dim sourceX As Integer = p1.GetX
                    Dim sourceY As Integer = p1.GetY
                    Dim destX As Integer = p2.GetX
                    Dim destY As Integer = p2.GetY

                    If i > 0 Then
                        sourceX = breakList(i - 1).GetX + (LINE_GAP * Math.Sign(p2.GetX - p1.GetX))
                        sourceY = breakList(i - 1).GetY + (LINE_GAP * Math.Sign(p2.GetY - p1.GetY))
                    End If

                    If i < breakList.Count Then
                        destX = breakList(i).GetX - (LINE_GAP * Math.Sign(p2.GetX - p1.GetX))
                        destY = breakList(i).GetY - (LINE_GAP * Math.Sign(p2.GetY - p1.GetY))
                    End If

                    Dim horizontal As Boolean = False

                    If sourceY = destY Then horizontal = True

                    p_graphics.DrawLine(p_pen, sourceX, sourceY, destX, destY)

                    If horizontal Then
                        p_graphics.DrawLine(p_pen, sourceX, sourceY - 1, destX, destY - 1)
                        p_graphics.DrawLine(p_pen, sourceX, sourceY + 1, destX, destY + 1)
                    Else
                        p_graphics.DrawLine(p_pen, sourceX - 1, sourceY, destX - 1, destY)
                        p_graphics.DrawLine(p_pen, sourceX + 1, sourceY, destX + 1, destY)
                    End If
                Next
            End If
        End If

        Return allDone
    End Function

    Private Function MyLineNew(ByRef p_sibbRef As SIBBClass, ByRef p_graphics As Graphics, ByVal p_pen As Pen, ByVal p_lineSegment As Integer, ByVal p_index As Integer, ByVal p_wideLine As Boolean) As Boolean
        Dim breakList As List(Of AbsoluteLocationClass)
        Dim LINE_GAP As Integer = 4
        Dim p1 As New AbsoluteLocationClass(myLineSegmentList(p_lineSegment).myStart)
        Dim p2 As New AbsoluteLocationClass(myLineSegmentList(p_lineSegment).myEnd)
        Dim allDone As Boolean = False
        Dim alreadyExists As Boolean = False

        'allDone = DesignerForm.lineList.AddLine(p_sibbRef.GetNodeIndex, p1, p2, alreadyExists)

        If Not alreadyExists Then
            'breakList = DesignerForm.lineList.LatestCrossesAnything()
            breakList = New List(Of AbsoluteLocationClass)

            If breakList.Count = 0 Then
                Dim horizontal As Boolean = IsHorizontal(p1, p2)

                p_graphics.DrawLine(p_pen, p1.GetX, p1.GetY, p2.GetX, p2.GetY)

                If horizontal Then
                    p_graphics.DrawLine(p_pen, p1.GetX, p1.GetY - 1, p2.GetX, p2.GetY - 1)
                    p_graphics.DrawLine(p_pen, p1.GetX, p1.GetY + 1, p2.GetX, p2.GetY + 1)

                    If p_wideLine Then
                        p_graphics.DrawLine(p_pen, p1.GetX, p1.GetY - 2, p2.GetX, p2.GetY - 2)
                        p_graphics.DrawLine(p_pen, p1.GetX, p1.GetY + 2, p2.GetX, p2.GetY + 2)
                    End If
                Else
                    Dim myP1YOffset As Integer = -1
                    Dim myP2YOffset As Integer = -1

                    If p2.GetY > p1.GetY Then
                        myP2YOffset = 1
                    Else
                        myP1YOffset = 1
                    End If

                    If p_wideLine Then
                        myP1YOffset *= 2
                        myP2YOffset *= 2
                    End If

                    p_graphics.DrawLine(p_pen, p1.GetX - 1, p1.GetY + myP1YOffset, p2.GetX - 1, p2.GetY + myP2YOffset)
                    p_graphics.DrawLine(p_pen, p1.GetX + 1, p1.GetY + myP1YOffset, p2.GetX + 1, p2.GetY + myP2YOffset)

                    If p_wideLine Then
                        p_graphics.DrawLine(p_pen, p1.GetX - 2, p1.GetY + myP1YOffset, p2.GetX - 2, p2.GetY + myP2YOffset)
                        p_graphics.DrawLine(p_pen, p1.GetX + 2, p1.GetY + myP1YOffset, p2.GetX + 2, p2.GetY + myP2YOffset)
                    End If
                End If
            Else
                For i = 0 To breakList.Count
                    Dim sourceX As Integer = p1.GetX
                    Dim sourceY As Integer = p1.GetY
                    Dim destX As Integer = p2.GetX
                    Dim destY As Integer = p2.GetY

                    If i > 0 Then
                        sourceX = breakList(i - 1).GetX + (LINE_GAP * Math.Sign(p2.GetX - p1.GetX))
                        sourceY = breakList(i - 1).GetY + (LINE_GAP * Math.Sign(p2.GetY - p1.GetY))
                    End If

                    If i < breakList.Count Then
                        destX = breakList(i).GetX - (LINE_GAP * Math.Sign(p2.GetX - p1.GetX))
                        destY = breakList(i).GetY - (LINE_GAP * Math.Sign(p2.GetY - p1.GetY))
                    End If

                    Dim horizontal As Boolean = False

                    If sourceY = destY Then horizontal = True

                    p_graphics.DrawLine(p_pen, sourceX, sourceY, destX, destY)

                    If horizontal Then
                        p_graphics.DrawLine(p_pen, sourceX, sourceY - 1, destX, destY - 1)
                        p_graphics.DrawLine(p_pen, sourceX, sourceY + 1, destX, destY + 1)
                    Else
                        p_graphics.DrawLine(p_pen, sourceX - 1, sourceY, destX - 1, destY)
                        p_graphics.DrawLine(p_pen, sourceX + 1, sourceY, destX + 1, destY)
                    End If
                Next
            End If
        End If

        Return allDone
    End Function

    Private Sub DrawArrow(ByRef p_graphics As Graphics, ByRef p_colour As Color, ByRef p As AbsoluteLocationClass)
        Dim myPen As New Pen(p_colour, 1)

        For j = 1 To 5
            p_graphics.DrawLine(myPen, p.GetX - j, p.GetY - j, p.GetX - j, p.GetY + j)
        Next
    End Sub

    Private Function AreWeDumpingToDebugTable() As Boolean
        Dim result As Boolean = False

        'If LAB_MODE = True Then result = True

        Return result
    End Function

    Private myLayoutBitmap As BitmapClass = Nothing

    Private Sub CreateLayoutBitmap()
        Dim maxX As Integer = 0
        Dim maxY As Integer = 0

        For i = 0 To sibbList.Count - 1
            With sibbList(i)
                If .GetRight > maxX Then maxX = .GetRight
                If .GetBottom > maxY Then maxY = .GetBottom
            End With
        Next

        maxX += 100
        maxY += 100

        myLayoutBitmap = New BitmapClass(maxX + 1, maxY + 1)
        myLayoutBitmap.SetInvertY(True)

        For y = 0 To maxY - 1
            For x = 0 To maxX - 1
                'myLayoutBitmap(x, y) = 0
            Next
        Next

        For i = 0 To sibbList.Count - 1
            With sibbList(i)
                For x = 0 To GetSIBBWidth() - 1
                    myLayoutBitmap.Plot(.GetXPos + x, .GetTop, &HFFFFFF)
                    myLayoutBitmap.Plot(.GetXPos + x, .GetBottom, &HFFFFFF)
                Next

                For y = 0 To .GetHeight - 1
                    myLayoutBitmap.Plot(.GetLeft, .GetTop + y, &HFFFFFF)
                    myLayoutBitmap.Plot(.GetRight, .GetTop + y, &HFFFFFF)
                Next

                For x = (-GAP) To GetSIBBWidth() + GAP - 1
                    If .GetTop >= GAP And .GetXPos + x >= 0 Then myLayoutBitmap.Plot(.GetXPos + x, .GetTop - GAP, &HFF0000)
                    If .GetXPos + x >= 0 Then myLayoutBitmap.Plot(.GetXPos + x, .GetBottom + GAP, &HFF0000)
                Next

                For y = (-GAP) To .GetHeight + GAP - 1
                    If .GetLeft >= GAP And .GetTop + y >= 0 Then myLayoutBitmap.Plot(.GetLeft - GAP, .GetTop + y, &HFF0000)
                    If .GetTop + y >= 0 Then myLayoutBitmap.Plot(.GetRight + GAP, .GetTop + y, &HFF0000)
                Next
            End With
        Next
    End Sub

    Private Sub RenderLayoutBitmap()
        Dim x As New SaveFileDialog

        If x.ShowDialog = DialogResult.OK Then
            myLayoutBitmap.Save(x.FileName)
        End If
    End Sub

    Private Sub DumpConnections()
        Dim x As New SaveFileDialog

        If x.ShowDialog = DialogResult.OK Then
            Dim y As New IO.StreamWriter(x.FileName)

            y.WriteLine(WrapInQuotes("SIBB") & "," & WrapInQuotes("xPos") & "," & WrapInQuotes("yPos") & "," & WrapInQuotes("Output") & "," & WrapInQuotes("nextNodeIndex") & "," & WrapInQuotes("Line Segment Index") & "," & WrapInQuotes("xStart") & "," & WrapInQuotes("yStart") & "," & WrapInQuotes("xEnd") & "," & WrapInQuotes("yEnd"))

            For i = 0 To sibbList.Count - 1
                With sibbList(i)
                    For j = 0 To .GetOutputCount - 1
                        With .outputs(j)
                            For k = 0 To .lineSegmentIndexes.Count - 1
                                Dim myLineSegmentIndex As Integer = .lineSegmentIndexes(k)

                                With myLineSegmentList(myLineSegmentIndex)
                                    y.WriteLine(i & "," & sibbList(i).absoluteLocation.GetX & "," & sibbList(i).absoluteLocation.GetY & "," & j & "," & .myDestNodeIndex & "," & myLineSegmentIndex & "," & .myStart.GetX & "," & .myStart.GetY & "," & .myEnd.GetX & "," & .myEnd.GetY)
                                End With
                            Next
                        End With
                    Next
                End With
            Next

            y.Close()
        End If
    End Sub

    Public Sub DrawAllConnections(ByVal p_preserveRoutes As Boolean)
        Dim myGraphics As Graphics = Graphics.FromImage(DesignerForm.myMasterBitmap)
        Dim minIndex As Integer = 0
        Dim maxIndex As Integer = 120

        If USE_NEW_DRAW Then
            'CreateLayoutBitmap()

            If maxIndex > (sibbList.Count - 1) Then maxIndex = sibbList.Count - 1
        Else
            minIndex = 0
            maxIndex = sibbList.Count - 1
        End If

        If AreWeDumpingToDebugTable() Then
            ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), "delete from sibbdebugtable")
            ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), "delete from sibbdebugoutputtable")
        End If

        If Not p_preserveRoutes Then myLineSegmentList.Clear()

        ' Loop over each SIBB
        For i = minIndex To maxIndex
            With sibbList(i)
                'MsgBox(.GetNodeTitle)
                If AreWeDumpingToDebugTable() Then
                    Dim mySql As New SQLStatementClass

                    mySql.SetInsertIntoTable("SIBBDebugTable")
                    mySql.AddValue("GetDate()")
                    mySql.AddValue(WrapInSingleQuotes(.GetNodeTitle))
                    mySql.AddValue(WrapInSingleQuotes(.GetFooterTitle))
                    mySql.AddValue(i)

                    ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql.GetSQLStatement)
                End If

                'If .GetFooterTitle = "Log Point" Then MsgBox(0)
                ' Loop over each output for this SIBB
                For j = 0 To .GetOutputCount - 1
                    'For j = 0 To LowestOf(0, .GetOutputCount - 1)
                    If .outputs(j).nextNodeIndex >= 0 Then
                        If .outputs(j).visible Then
                            ' If the output is connected to another node, and is visible then draw it
                            If AreWeDumpingToDebugTable() Then
                                Dim mySql As New SQLStatementClass

                                mySql.Clear()
                                mySql.SetInsertIntoTable("SIBBDebugOutputTable")

                                mySql.AddValue(i)
                                mySql.AddValue(j)
                                mySql.AddValue(.outputs(j).nextNodeIndex)
                                mySql.AddValue("''")
                                mySql.AddValue("''")

                                ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql.GetSQLStatement)
                            End If

                            If USE_NEW_DRAW Then
                                DrawConnectionNew(myGraphics, i, j, p_preserveRoutes)
                            Else
                                DrawAllConnection1(i, myGraphics, j)
                            End If
                        End If
                    End If
                Next
            End With
        Next

        If USE_NEW_DRAW Then
            If Not p_preserveRoutes Then
                ' For i = 0 To sibbList.Count - 1
                'With sibbList(i)
                'For j = 0 To .GetOutputCount - 1
                ' RemoveSparePoints(.outputs(j).points)
                ' Next
                ' End With
                ' Next

                CheckForOverlap()
            End If
        End If

        ' If USE_NEW_DRAW Then RenderLayoutBitmap()

        myGraphics = Nothing

        If DUMP_MAP Then DumpConnections()
    End Sub

    Public Function GetColour(ByVal p_nodeIndex As Integer, ByVal p_outputIndex As Integer) As Color
        Return rgbValues(rgbIndexes((p_nodeIndex + p_outputIndex) Mod rgbValues.Count))
    End Function

    Private Function IsBelow(ByRef p0 As AbsoluteLocationClass, ByRef p1 As AbsoluteLocationClass)
        Dim result As Boolean = False

        If p1.GetY > p0.GetY Then result = True

        Return result
    End Function

    Private Function IsRight(ByRef p0 As AbsoluteLocationClass, ByRef p1 As AbsoluteLocationClass)
        Dim result As Boolean = False

        If p1.GetX > p0.GetX Then result = True

        Return result
    End Function

    Private Sub DetectDiagonal(ByRef p As List(Of AbsoluteLocationClass))
        Dim myState As Integer = 0
        Dim myX, myY As Integer
        Dim myStart As Integer = -1
        Dim myEnd As Integer = -1

        For i = 0 To p.Count - 1
            Select Case myState
                Case 0
                    myX = p(i).GetX
                    myY = p(i).GetY
                    myState = 1
                    myStart = i

                Case 1
                    If p(i).GetX = myX - 1 And p(i).GetY = myY Then
                        myState = 2
                        myEnd = i
                    Else
                        myState = 0

                        If myEnd >= 0 Then Exit For
                    End If

                Case 2
                    If p(i).GetX = myX - 1 And p(i).GetY = myY + 1 Then
                        myState = 1
                        myX = p(i).GetX
                        myY = p(i).GetY
                    Else
                        myState = 0

                        If myEnd >= 0 Then Exit For
                    End If

            End Select
        Next

        If myEnd >= 0 Then
            myEnd -= 1

            If myEnd - myStart > 1 Then p.RemoveRange(myStart + 1, myEnd - myStart - 1)
        End If
    End Sub

    Public Sub DrawConnectionNew(ByRef p_graphics As Graphics, ByVal p_sibbIndex As Integer, ByVal p_outputIndex As Integer, ByVal p_preserveRoute As Boolean)
        With sibbList(p_sibbIndex)
            Dim myLineSegmentsRef As List(Of Integer) = .outputs(p_outputIndex).lineSegmentIndexes
            Dim nextNodeIndex As Integer = .outputs(p_outputIndex).nextNodeIndex
            Dim pEnd As AbsoluteLocationClass = sibbList(nextNodeIndex).GetConnectionEndPosition()

            'If p_sibbIndex = 40 And p_outputIndex = 2 Then MsgBox(p_sibbIndex)

            If Not p_preserveRoute Then
                Dim MAX_X As Integer = DesignerForm.myMasterBitmap.Width - 5
                Dim MAX_Y As Integer = DesignerForm.myMasterBitmap.Height - 5
                Dim nextNodeRef As SIBBClass = sibbList(nextNodeIndex)
                Dim pStart As AbsoluteLocationClass = .GetConnectionStartPosition(p_outputIndex)
                Dim pEndOffset As New AbsoluteLocationClass(pEnd.GetX - (GAP + 0), pEnd.GetY)
                Dim myAngle As Integer = 0  ' 0 = Right, 90 = Down, 180 = Left, 270 = Up
                Dim moving As Boolean = True
                Dim myPreviousOutputIndex As Integer = -1
                Dim myOutputGroups As New Dictionary(Of Integer, List(Of Integer))  ' Key is nextNodeIndex and list is of outputNodeIndexes
                Dim myOutputGroupIndex As Integer = -1
                Dim myPoints As New List(Of AbsoluteLocationClass)

                myLineSegmentsRef.Clear()

                ' Set the default colour
                .outputs(p_outputIndex).routeColour = GetColour(.GetNodeIndex, p_outputIndex)  ' The default colour

                For i = 0 To p_sibbIndex - 1
                    Dim foundIt As Boolean = False

                    With sibbList(i)
                        For j = 0 To .GetOutputCount - 1
                            If .outputs(j).nextNodeIndex = nextNodeIndex Then
                                foundIt = True
                                sibbList(p_sibbIndex).outputs(p_outputIndex).routeColour = .outputs(j).routeColour
                                Exit For
                            End If
                        Next
                    End With

                    If foundIt Then Exit For
                Next

                ' Build up the output groups
                For i = 0 To p_outputIndex
                    If .outputs(i).nextNodeIndex >= 0 Then
                        If Not myOutputGroups.ContainsKey(.outputs(i).nextNodeIndex) Then myOutputGroups.Add(.outputs(i).nextNodeIndex, New List(Of Integer))

                        myOutputGroups.Item(.outputs(i).nextNodeIndex).Add(i)
                    End If
                Next

                ' Has a previous output from this node already connected to the same next node ?
                ' If so get the next previous output to us, and take the colour of the first output going to the same next node
                ' Duplicate the common route so we can hilight from every output
                With myOutputGroups(nextNodeIndex)
                    If .Count > 1 Then
                        myPreviousOutputIndex = .Item(.Count - 2)
                        sibbList(p_sibbIndex).outputs(p_outputIndex).routeColour = sibbList(p_sibbIndex).outputs(.Item(0)).routeColour
                    End If
                End With

                ' Calculate how far to tab each output
                For i = 0 To myOutputGroups.Keys.Count - 1
                    If myOutputGroups.Item(myOutputGroups.Keys(i)).Contains(p_outputIndex) Then
                        myOutputGroupIndex = i
                        Exit For
                    End If
                Next

                myPoints.Add(pStart)

                Dim currentPoint As New AbsoluteLocationClass(pStart, GAP * (myOutputGroupIndex + 1), 0)
                Dim m As Double = (pEnd.GetY - currentPoint.GetY) / (pEnd.GetX - currentPoint.GetX)
                Dim c = currentPoint.GetY - (m * currentPoint.GetX)

                myPoints.Add(New AbsoluteLocationClass(currentPoint))

                If myPreviousOutputIndex >= 0 Then
                    ' For i = 1 To .outputs(myPreviousOutputIndex).points.Count - 1
                    'currentPoint.Set(.outputs(myPreviousOutputIndex).points(i))
                    'myPointsRef.Add(New AbsoluteLocationClass(currentPoint))
                    'Next
                Else
                    Dim looping As Boolean = True
                    Dim upDown As Boolean = True
                    Dim loopCount As Integer = 0
                    Dim blockingSIBBIndex As Integer = -1

                    If currentPoint.GetX >= pEndOffset.GetX Then
                        If currentPoint.GetY >= pEndOffset.GetY Then
                            myAngle = 270
                        Else
                            myAngle = 90
                        End If
                    End If

                    While looping
                        Select Case myAngle
                            Case 0
                                ' Go right until we hit a SIBB, reach x = pEnd.GetX() - GAP, or reach the right hand side of the display
                                Dim tempBlockingSIBBIndex As Integer = -1
                                Dim myX As Integer = CheckRight(nextNodeIndex, currentPoint, MAX_X, tempBlockingSIBBIndex)

                                If blockingSIBBIndex >= 0 And blockingSIBBIndex <> nextNodeIndex Then
                                    myX = sibbList(blockingSIBBIndex).GetRight + GAP
                                End If

                                blockingSIBBIndex = tempBlockingSIBBIndex
                                currentPoint.SetX(myX)

                                myPoints.Add(New AbsoluteLocationClass(currentPoint))

                                If pEnd.GetY < currentPoint.GetY Then
                                    myAngle = 270   ' Go up
                                Else
                                    myAngle = 90    ' Go down
                                End If

                            Case 90
                                ' Go down until we hit a SIBB, or are above the destination SIBB if we are to the right of pEndOffset, or reach the bottom of the display
                                Dim tempBlockingSIBBIndex As Integer = -1
                                Dim myY As Integer = CheckBelow(nextNodeIndex, currentPoint, MAX_Y, tempBlockingSIBBIndex)

                                If blockingSIBBIndex >= 0 And blockingSIBBIndex <> nextNodeIndex Then
                                    myY = sibbList(blockingSIBBIndex).GetBottom + GAP
                                End If

                                blockingSIBBIndex = tempBlockingSIBBIndex
                                currentPoint.SetY(myY)

                                myPoints.Add(New AbsoluteLocationClass(currentPoint))

                                If pEndOffset.GetX < currentPoint.GetX Then
                                    myAngle = 180
                                Else
                                    myAngle = 0
                                End If

                            Case 180
                                ' Go left until we hit a SIBB, reach x = pEnd.GetX() - GAP, or reach the left hand side of the display
                                Dim tempBlockingSIBBIndex As Integer = -1
                                Dim myX = CheckLeft(nextNodeIndex, currentPoint, 5, tempBlockingSIBBIndex)

                                If blockingSIBBIndex >= 0 And blockingSIBBIndex <> nextNodeIndex Then
                                    myX = sibbList(blockingSIBBIndex).GetLeft - (2 * GAP)
                                End If

                                blockingSIBBIndex = tempBlockingSIBBIndex
                                currentPoint.SetX(myX)

                                myPoints.Add(New AbsoluteLocationClass(currentPoint))

                                If pEnd.GetY < currentPoint.GetY Then
                                    myAngle = 270   ' Go up
                                Else
                                    myAngle = 90    ' Go down
                                End If

                            Case 270
                                ' Go up until we reach the top of the display, or reach y = pEnd.GetY()
                                Dim myY As Integer = 5

                                If blockingSIBBIndex >= 0 Then
                                    Dim bypassBlock As Boolean = False

                                    If blockingSIBBIndex <> nextNodeIndex Then
                                        ' Can we reach the destination SIBB anyway ?
                                        Dim tempBlockingSIBBIndex As Integer = -1

                                        myY = CheckAbove(nextNodeIndex, currentPoint, myY, tempBlockingSIBBIndex)

                                        If myY = pEnd.GetY And tempBlockingSIBBIndex = -1 Then
                                            Dim myX = CheckRight(nextNodeIndex, currentPoint, MAX_X, tempBlockingSIBBIndex)

                                            If myX = pEndOffset.GetX Then bypassBlock = True
                                        End If

                                        If bypassBlock Then
                                            currentPoint.SetY(myY)
                                        Else
                                            currentPoint.SetY(sibbList(blockingSIBBIndex).GetTop - GAP)
                                        End If
                                    End If

                                    blockingSIBBIndex = -1
                                Else
                                    myY = CheckAbove(nextNodeIndex, currentPoint, myY, blockingSIBBIndex)
                                    currentPoint.SetY(myY)
                                End If

                                myPoints.Add(New AbsoluteLocationClass(currentPoint))

                                If pEndOffset.GetX < currentPoint.GetX Then
                                    myAngle = 180
                                Else
                                    myAngle = 0
                                End If
                        End Select


                        If currentPoint.IsEqualTo(pEndOffset) Then
                            myPoints.Add(New AbsoluteLocationClass(pEnd))
                            looping = False
                        End If

                        loopCount += 1

                        If loopCount = 10 Then
                            MsgBox("Loop count maxed " & p_sibbIndex & " " & p_outputIndex)
                            looping = False
                        End If

                        upDown = Not upDown
                    End While
                End If

                'RemoveSparePoints(myPointsRef)

                ' Does it cross any other existing routes from other SIBBs to the same destination SIBB ?
                Dim foundCrossover As Boolean = False
                Dim pickupCrossoverFrom As New List(Of Integer)

                ' Replacement for below
                If HANDLE_CROSSOVERS Then
                    For i = 0 To myPoints.Count - 2
                        For j = 0 To myLineSegmentList.Count - 1
                            With myLineSegmentList(j)
                                If .myDestNodeIndex = nextNodeIndex Then
                                    Dim myCrossingPoint As AbsoluteLocationClass = DoLinesCross(.myStart, .myEnd, myPoints(i), myPoints(i + 1))

                                    If Not myCrossingPoint Is Nothing Then
                                        Dim myNewSegment As New LineSegmentClass(p_sibbIndex, p_outputIndex, nextNodeIndex)

                                        foundCrossover = True

                                        ' Set my end point to be the crossover point
                                        myPoints(i + 1).Set(myCrossingPoint)

                                        ' Remove all of our points after the crossover point as we will be using existing line segments
                                        myPoints.RemoveRange(i + 2, myPoints.Count - (i + 2))

                                        ' Set the start of the new segment to be the crossover point and the end to be the existing end
                                        myNewSegment.myStart.Set(myCrossingPoint)
                                        myNewSegment.myEnd.Set(.myEnd)

                                        ' Set the end of the existing segment to be the crossing point
                                        .myEnd.Set(myCrossingPoint)

                                        ' Add the new segment to the global list
                                        myLineSegmentList.Add(myNewSegment)

                                        ' What about other existing SIBBs that used this line segment that has now been segmented ?
                                        Dim myNewLineSegmentIndex As Integer = myLineSegmentList.Count - 1

                                        For k = 0 To .myUsedByList.Count - 1
                                            Dim mySibbIndex As Integer = .myUsedByList(k).mySibbIndex
                                            Dim myOutputIndex As Integer = .myUsedByList(k).myOutputIndex

                                            With sibbList(mySibbIndex).outputs(myOutputIndex)
                                                For m = 0 To .lineSegmentIndexes.Count - 1
                                                    If .lineSegmentIndexes(m) = j Then
                                                        ' Insert reference to our line segment just after this one for this SIBB
                                                        .lineSegmentIndexes.Insert(m + 1, myNewLineSegmentIndex)

                                                        ' Add backward reference to this SIBB from the new line segment
                                                        myLineSegmentList(myNewLineSegmentIndex).myUsedByList.Add(New UsedByClass(mySibbIndex, myOutputIndex))
                                                    End If
                                                Next
                                            End With
                                        Next

                                        ' Make a list of the remaining line segment indexes that we need
                                        With sibbList(.myUsedByList(0).mySibbIndex).outputs(.myUsedByList(0).myOutputIndex)
                                            Dim foundIt As Boolean = False

                                            For k = 0 To .lineSegmentIndexes.Count - 1
                                                If foundIt Then
                                                    pickupCrossoverFrom.Add(.lineSegmentIndexes(k))
                                                Else
                                                    If .lineSegmentIndexes(k) = j Then foundIt = True
                                                End If
                                            Next
                                        End With

                                        Exit For
                                    End If
                                End If
                            End With
                        Next

                        If foundCrossover Then Exit For
                    Next
                End If

                If myPreviousOutputIndex >= 0 Then myPoints.Add(New AbsoluteLocationClass(myLineSegmentList(.outputs(myPreviousOutputIndex).lineSegmentIndexes(0)).myEnd))

                Dim allDone As Boolean = False
                Dim myInheritLineSegmentIndexesFrom As Integer = -1

                ' Loop over each pair of points that are each going to create their own line segment
                For i = 0 To myPoints.Count - 2
                    Dim myIndex As Integer = -1

                    ' Our start is myPoints(i), and our end is myPoints(i + 1)
                    If MERGE_EXISTING_LINE_SEGMENTS Then
                        ' Before we add this line segment, is there an existing line segment that we can use ?
                        For j = 0 To myLineSegmentList.Count - 1
                            With myLineSegmentList(j)
                                ' Case when destination matches and end of our line and an existing line matches
                                If .myEnd.IsEqualTo(myPoints(i + 1)) And .myDestNodeIndex = nextNodeIndex Then
                                    ' Case when start of our line and an existing line matches
                                    If .myStart.IsEqualTo(myPoints(i)) Then
                                        myPoints.RemoveRange(i + 1, myPoints.Count - (i + 1))
                                        myInheritLineSegmentIndexesFrom = j
                                        allDone = True
                                        Exit For
                                    Else
                                        ' Are we both in the same direction ?
                                        If GetAngle(myPoints(i), myPoints(i + 1)) = .GetAngle Then
                                            Dim ourStartInMiddle As Boolean = False

                                            If .IsHorizontal Then
                                                If IsBetween(myPoints(i).GetX, .myStart.GetX, .myEnd.GetX) Then ourStartInMiddle = True
                                            Else
                                                If IsBetween(myPoints(i).GetY, .myStart.GetY, .myEnd.GetY) Then ourStartInMiddle = True
                                            End If

                                            If ourStartInMiddle Then
                                                Dim myNewSegment As New LineSegmentClass(p_sibbIndex, p_outputIndex, nextNodeIndex)

                                                ' Set the end of the existing segment to be our start point
                                                myLineSegmentList(j).myEnd.Set(myPoints(i))

                                                ' Set the start of the new segment to be our start point and the end to be our end point
                                                myNewSegment.myStart.Set(myPoints(i))
                                                myNewSegment.myEnd.Set(myPoints(i + 1))

                                                ' Add the new segment to the global list
                                                myLineSegmentList.Add(myNewSegment)

                                                ' Look for existing SIBBs that use this line segment that has now been segmented and update accordingly
                                                Dim myNewLineSegmentIndex As Integer = myLineSegmentList.Count - 1

                                                For k = 0 To .myUsedByList.Count - 1
                                                    Dim mySibbIndex As Integer = .myUsedByList(k).mySibbIndex
                                                    Dim myOutputIndex As Integer = .myUsedByList(k).myOutputIndex

                                                    With sibbList(mySibbIndex).outputs(myOutputIndex)
                                                        For m = 0 To .lineSegmentIndexes.Count - 1
                                                            If .lineSegmentIndexes(m) = j Then
                                                                ' Insert reference to our line segment just after this one for this SIBB
                                                                .lineSegmentIndexes.Insert(m + 1, myNewLineSegmentIndex)

                                                                ' Add backward reference to this SIBB from the new line segment
                                                                myLineSegmentList(myNewLineSegmentIndex).myUsedByList.Add(New UsedByClass(mySibbIndex, myOutputIndex))
                                                            End If
                                                        Next
                                                    End With
                                                Next

                                                myPoints.RemoveRange(i + 1, myPoints.Count - (i + 1))
                                                myInheritLineSegmentIndexesFrom = myNewLineSegmentIndex
                                                allDone = True
                                                Exit For
                                            Else
                                                myPoints(i + 1).Set(.myStart)
                                                myPoints.RemoveRange(i + 2, myPoints.Count - (i + 2))
                                                myInheritLineSegmentIndexesFrom = j
                                                allDone = True
                                                Exit For
                                            End If
                                        End If
                                    End If
                                End If ' Ends match and dest node matches
                            End With
                        Next
                    End If ' If MERGE_EXISTING_LINE_SEGMENTS ..

                    If allDone Then Exit For
                Next ' Loop over myPoints

                For i = 0 To myPoints.Count - 2
                    Dim myLineSegment As New LineSegmentClass(p_sibbIndex, p_outputIndex, nextNodeIndex)

                    myLineSegment.myStart.Set(myPoints(i))
                    myLineSegment.myEnd.Set(myPoints(i + 1))
                    myLineSegmentList.Add(myLineSegment)
                    myLineSegmentsRef.Add(myLineSegmentList.Count - 1)
                Next

                If myPreviousOutputIndex >= 0 Then
                    For i = 1 To .outputs(myPreviousOutputIndex).lineSegmentIndexes.Count - 1
                        myLineSegmentsRef.Add(.outputs(myPreviousOutputIndex).lineSegmentIndexes(i))
                    Next
                End If

                myLineSegmentsRef.AddRange(pickupCrossoverFrom)

                InheritLineSegmentIndexesFrom(myLineSegmentsRef, myInheritLineSegmentIndexesFrom)
            End If ' If Not p_preserveRoute ..

            DrawRouteNew(sibbList(p_sibbIndex), p_graphics, .outputs(p_outputIndex).routeColour, myLineSegmentsRef, .outputs(p_outputIndex).selected)

            'If myPreviousOutputIndex = -1 Then DrawArrow(p_graphics, .outputs(p_outputIndex).routeColour, pEnd)
            DrawArrow(p_graphics, .outputs(p_outputIndex).routeColour, pEnd)
        End With
    End Sub

    Private Sub InheritLineSegmentIndexesFrom(ByRef p_lineSegmentsRef As List(Of Integer), ByVal p_segmentIndex As Integer, Optional ByVal p_after As Boolean = False)
        Dim allDone As Boolean = False

        If p_segmentIndex = -1 Then Return

        ' Find a SIBB and an output that uses this line segment index
        For i = 0 To sibbList.Count - 1
            With sibbList(i)
                For j = 0 To .GetOutputCount - 1
                    With .outputs(j)
                        For k = 0 To .lineSegmentIndexes.Count - 1
                            If .lineSegmentIndexes(k) = p_segmentIndex Then
                                Dim myOffset As Integer = 0

                                If p_after Then myOffset = 1

                                For m = k + myOffset To .lineSegmentIndexes.Count - 1
                                    p_lineSegmentsRef.Add(.lineSegmentIndexes(m))
                                Next

                                allDone = True
                                Exit For
                            End If
                        Next
                    End With

                    If allDone Then Exit For
                Next
            End With

            If allDone Then Exit For
        Next
    End Sub

    Private Function IsBetween(ByVal p_value As Integer, ByVal p_0 As Integer, ByVal p_1 As Integer) As Boolean
        Dim result As Boolean = False

        If (p_value >= p_0 And p_value <= p_1) Or (p_value >= p_1 And p_value <= p_0) Then result = True

        Return result
    End Function

    Private Function GetAngle(ByRef p_start As AbsoluteLocationClass, ByRef p_end As AbsoluteLocationClass) As Integer
        Dim result As Integer = 0

        If p_start.GetY = p_end.GetY Then
            If p_end.GetX < p_start.GetX Then result = 180
        Else
            result = 90

            If p_end.GetY < p_start.GetY Then result = 270
        End If

        Return result
    End Function

    Private Sub RemoveSparePoints(ByRef p_points As List(Of AbsoluteLocationClass))
        Dim looping As Boolean = True

        While looping
            Dim myIndex As Integer = -1

            For i = 0 To p_points.Count - 3
                If p_points(i).GetY = p_points(i + 1).GetY And p_points(i).GetY = p_points(i + 2).GetY Then
                    myIndex = i + 1
                    Exit For
                End If
            Next

            If myIndex >= 0 Then
                p_points.RemoveAt(myIndex)
            Else
                looping = False
            End If
        End While
    End Sub

    Private Function DoLinesCross(ByRef p0 As AbsoluteLocationClass, ByRef p1 As AbsoluteLocationClass, ByRef p2 As AbsoluteLocationClass, ByRef p3 As AbsoluteLocationClass) As AbsoluteLocationClass
        Dim result As AbsoluteLocationClass = Nothing

        If p0.GetX = p1.GetX Then
            ' First line is vertical - is second line horizontal ?
            If p2.GetY = p3.GetY Then
                ' Yes
                If (p2.GetY > p0.GetY And p2.GetY < p1.GetY) Or (p2.GetY > p1.GetY And p2.GetY < p0.GetY) Then
                    If (p0.GetX > p2.GetX And p0.GetX < p3.GetX) Or (p0.GetX > p3.GetX And p0.GetX < p2.GetX) Then result = New AbsoluteLocationClass(p0.GetX, p2.GetY)
                End If
            End If
        Else
            ' First line is horizontal - is the second line vertical ?
            If p2.GetX = p3.GetX Then
                ' Yes
                If (p0.GetY > p2.GetY And p0.GetY < p3.GetY) Or (p0.GetY > p3.GetY And p0.GetY < p2.GetY) Then
                    If (p2.GetX > p0.GetX And p2.GetX < p1.GetX) Or (p2.GetX > p1.GetX And p2.GetX < p0.GetX) Then result = New AbsoluteLocationClass(p2.GetX, p0.GetY)
                End If
            End If
        End If

        Return result
    End Function

    Private Function LookForSIBBBelow(ByRef p_currentPoint As AbsoluteLocationClass, ByVal p_myY As Integer) As Integer
        Dim result As Integer = p_myY
        Dim myY As Integer = -1

        For i = 0 To sibbList.Count - 1
            With sibbList(i)
                If p_currentPoint.GetX > (.GetLeft - GAP) Then
                    If p_currentPoint.GetX <= (.GetRight + GAP) Then
                        If p_currentPoint.GetY <= (.GetTop - GAP) Then
                            Dim y As Integer = .GetTop - GAP

                            If myY = -1 Then
                                myY = y
                            Else
                                If y < myY Then myY = y
                            End If
                        End If
                    End If
                End If
            End With
        Next

        If myY >= 0 Then
            If myY < p_myY Then result = myY
        End If

        Return result
    End Function

    Private Function LevelWithDestinationSIBB(ByVal p_destNodeIndex As Integer, ByRef p_currentPoint As AbsoluteLocationClass, ByVal p_myY As Integer) As Integer
        Dim result As Integer = p_myY

        With sibbList(p_destNodeIndex)
            If p_currentPoint.GetX > (.GetLeft - GAP) Then
                Dim y As Integer = .GetTop - GAP

                If y < p_myY Then result = y
            End If
        End With

        Return result
    End Function

    Private Function LeftOfAndLevelWith(ByVal p_destNodeIndex As Integer, ByRef p_currentPoint As AbsoluteLocationClass, ByVal p_myY As Integer) As Integer
        Dim result As Integer = p_myY

        With sibbList(p_destNodeIndex)
            If p_currentPoint.GetX <= (.GetLeft - GAP) Then
                Dim y As Integer = .GetConnectionEndPosition.GetY

                If y < p_myY Then result = y
            End If
        End With

        Return result
    End Function

    Private Function TryToReachToRight(ByVal p_destNodeIndex As Integer, ByRef p_currentPoint As AbsoluteLocationClass, ByVal p_myX As Integer) As Integer
        Dim result As Integer = p_myX

        With sibbList(p_destNodeIndex)
            Dim pEnd As AbsoluteLocationClass = .GetConnectionEndPosition

            Dim x As Integer = pEnd.GetX

            For i = 0 To sibbList.Count - 1
                With sibbList(i)
                    If p_currentPoint.GetY >= .GetTop Then
                        If p_currentPoint.GetY <= .GetBottom Then
                            If .GetLeft > p_currentPoint.GetX Then
                                If .GetRight < pEnd.GetX Then
                                    If .GetLeft < result Then result = .GetLeft
                                End If
                            End If
                        End If
                    End If
                End With
            Next
        End With

        Return result
    End Function

    Private Function CheckRight(ByVal p_destNodeIndex As Integer, ByRef p_currentPoint As AbsoluteLocationClass, ByVal p_myX As Integer, ByRef p_sibbHitIndex As Integer) As Integer
        Dim result As Integer = p_myX

        With sibbList(p_destNodeIndex)
            Dim pEnd As AbsoluteLocationClass = .GetConnectionEndPosition
            Dim x As Integer = pEnd.GetX - GAP

            If x < result Then result = x

            For i = 0 To sibbList.Count - 1
                If i <> p_destNodeIndex Then
                    With sibbList(i)
                        If p_currentPoint.GetY >= .GetTop Then
                            If p_currentPoint.GetY <= .GetBottom Then
                                x = .GetLeft - (2 * GAP)

                                If x > p_currentPoint.GetX Then
                                    If x < result Then
                                        result = x
                                        p_sibbHitIndex = i
                                    End If
                                End If
                            End If
                        End If
                    End With
                End If
            Next
        End With

        Return result
    End Function

    Private Function CheckLeft(ByVal p_destNodeIndex As Integer, ByRef p_currentPoint As AbsoluteLocationClass, ByVal p_myX As Integer, ByRef p_sibbHitIndex As Integer) As Integer
        Dim result As Integer = p_myX

        With sibbList(p_destNodeIndex)
            Dim pEnd As AbsoluteLocationClass = .GetConnectionEndPosition
            Dim x As Integer = pEnd.GetX - GAP

            If x > result Then result = x

            For i = 0 To sibbList.Count - 1
                With sibbList(i)
                    If p_currentPoint.GetY >= .GetTop Then
                        If p_currentPoint.GetY <= .GetBottom Then
                            x = .GetRight + GAP

                            If x < p_currentPoint.GetX Then
                                If x > result Then
                                    result = x
                                    p_sibbHitIndex = i
                                End If
                            End If
                        End If
                    End If
                End With
            Next
        End With

        Return result
    End Function

    Private Function CheckAbove(ByVal p_destNodeIndex As Integer, ByRef p_currentPoint As AbsoluteLocationClass, ByVal p_myY As Integer, ByRef p_sibbHitIndex As Integer) As Integer
        Dim result As Integer = p_myY

        With sibbList(p_destNodeIndex)
            Dim pEnd As AbsoluteLocationClass = .GetConnectionEndPosition
            Dim y As Integer = pEnd.GetY

            If y > result Then result = y

            For i = 0 To sibbList.Count - 1
                With sibbList(i)
                    If p_currentPoint.GetX >= .GetLeft Then
                        If p_currentPoint.GetX <= .GetRight Then
                            y = .GetBottom + GAP

                            If y < p_currentPoint.GetY Then
                                If y > result Then
                                    result = y
                                    p_sibbHitIndex = i
                                End If
                            End If
                        End If
                    End If
                End With
            Next
        End With

        Return result
    End Function

    Private Function CheckBelow(ByVal p_destNodeIndex As Integer, ByRef p_currentPoint As AbsoluteLocationClass, ByVal p_myY As Integer, ByRef p_sibbHitIndex As Integer) As Integer
        Dim result As Integer = p_myY

        With sibbList(p_destNodeIndex)
            Dim pEnd As AbsoluteLocationClass = .GetConnectionEndPosition
            Dim pEndOffset As New AbsoluteLocationClass(pEnd.GetX - (GAP + 0), pEnd.GetY)
            Dim y As Integer = pEnd.GetY

            If y < result Then result = y

            If p_currentPoint.GetX > pEndOffset.GetX Then
                y = sibbList(p_destNodeIndex).GetTop - GAP

                If y < result Then result = y
            End If

            For i = 0 To sibbList.Count - 1
                With sibbList(i)
                    If p_currentPoint.GetX >= .GetLeft Then
                        If p_currentPoint.GetX <= .GetRight Then
                            y = .GetTop - GAP

                            If y > p_currentPoint.GetY Then
                                If y < result Then
                                    result = y
                                    p_sibbHitIndex = i
                                End If
                            End If
                        End If
                    End If
                End With
            Next
        End With

        Return result
    End Function

    Private Sub CheckForOverlap()
        For i = 0 To myLineSegmentList.Count - 1
            With myLineSegmentList(i)
                ' Is it a horizontal line ?
                If .IsHorizontal Then
                    For j = 0 To myLineSegmentList.Count - 1
                        If i <> j Then
                            If myLineSegmentList(j).IsHorizontal Then
                                Dim z As LineSegmentClass = myLineSegmentList(j)

                                If .myStart.GetY = z.myStart.GetY Then
                                    Dim x0 As Integer = .myStart.GetX
                                    Dim x1 As Integer = .myEnd.GetX
                                    Dim x2 As Integer = z.myStart.GetX
                                    Dim x3 As Integer = z.myEnd.GetX

                                    Order(x0, x1)
                                    Order(x2, x3)

                                    If (x2 > x0 And x2 < x1) Or (x3 > x0 And x3 < x1) Or (x2 < x0 And x3 > x1) Then
                                        'MsgBox(i & ", " & j)
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
            End With
        Next
    End Sub

    Private Sub FindWhoUsesLine(ByVal p_lineIndex As Integer)
        For i = 0 To sibbList.Count - 1
            With sibbList(i)
                For j = 0 To .GetOutputCount - 1
                    With .outputs(j)
                        For k = 0 To .lineSegmentIndexes.Count - 1
                            If .lineSegmentIndexes(k) = p_lineIndex Then
                                MsgBox(p_lineIndex & ": " & i & ", " & j & ", " & k)
                            End If
                        Next
                    End With
                Next
            End With
        Next
    End Sub

    Private Sub CheckForOverlapOld()
        For i = 0 To sibbList.Count - 1
            With sibbList(i)
                For j = 0 To .GetOutputCount - 1
                    With .outputs(j)
                        For k = 0 To .lineSegmentIndexes.Count - 1
                            ' Is it a horizontal line ?
                            If myLineSegmentList(.lineSegmentIndexes(k)).IsHorizontal Then
                                ' Yes - look for all other horizontal lines
                                For ii = 0 To sibbList.Count - 1
                                    For jj = 0 To sibbList(ii).GetOutputCount - 1
                                        If .nextNodeIndex <> sibbList(ii).outputs(jj).nextNodeIndex Then
                                            Dim z As List(Of Integer) = sibbList(ii).outputs(jj).lineSegmentIndexes

                                            For kk = 0 To z.Count - 1
                                                If myLineSegmentList(z(kk)).IsHorizontal Then
                                                    ' Another horizontal line to a different destination - is it on the same line as us ?
                                                    If myLineSegmentList(.lineSegmentIndexes(k)).myStart.GetY = myLineSegmentList(z(kk)).myStart.GetY Then
                                                        ' Do these lines overlap ?
                                                        Dim x0 As Integer = myLineSegmentList(.lineSegmentIndexes(k)).myStart.GetX
                                                        Dim x1 As Integer = myLineSegmentList(.lineSegmentIndexes(k)).myEnd.GetX
                                                        Dim x2 As Integer = myLineSegmentList(z(kk)).myStart.GetX
                                                        Dim x3 As Integer = myLineSegmentList(z(kk)).myEnd.GetX

                                                        Order(x0, x1)
                                                        Order(x2, x3)

                                                        If (x2 >= x0 And x2 <= x1) Or (x3 >= x0 And x3 <= x1) Or (x2 < x0 And x3 > x1) Then
                                                            'MsgBox(i & ", " & j & ", " & k & ", " & .lineSegmentIndexes(k) & ", " & ii & ", " & jj & ", " & kk & ", " & z(kk))
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        End If
                                    Next
                                Next
                            Else
                            End If
                        Next
                    End With
                Next
            End With
        Next
    End Sub

    Private Sub Order(ByRef p0 As Integer, ByRef p1 As Integer)
        If p0 > p1 Then
            Dim temp As Integer = p0

            p0 = p1
            p1 = temp
        End If
    End Sub

    Public Sub DrawConnectionNewPrevious(ByRef p_graphics As Graphics, ByVal p_sibbIndex As Integer, ByVal p_outputIndex As Integer)
        With sibbList(p_sibbIndex)
            Dim myColour = GetColour(.GetNodeIndex, p_outputIndex)  ' The default colour
            Dim nextNodeIndex As Integer = .outputs(p_outputIndex).nextNodeIndex
            Dim nextNodeRef As SIBBClass = sibbList(nextNodeIndex)
            Dim pStart As AbsoluteLocationClass = .GetConnectionStartPosition(p_outputIndex)
            Dim pEnd As AbsoluteLocationClass = sibbList(nextNodeIndex).GetConnectionEndPosition()
            Dim pEndOffset As New AbsoluteLocationClass(pEnd.GetX - (GAP + 1), pEnd.GetY)
            Dim myPoints As New List(Of AbsoluteLocationClass)
            Dim outputJoinedTo As Integer = -1
            Dim myGroupedOutputs As New Dictionary(Of Integer, List(Of Integer))
            Dim mySharedDestinationNodeIndex As Integer = -1
            Dim myAngle As Integer = 0  ' 0 = Right, 90 = Down, 180 = Left, 270 = Up
            Dim myNextAngle As Integer = -1
            Dim moving As Boolean = True
            Dim currentPoint As New AbsoluteLocationClass(pStart, GAP, 0)

            ' If a previous node has already connected to our destination then use the same colour
            Dim foundIt As Boolean = False

            For i = 0 To p_sibbIndex - 1
                With sibbList(i)
                    For j = 0 To .GetOutputCount - 1
                        If .GetOutputNextNode(j) = sibbList(p_sibbIndex).GetOutputNextNode(p_outputIndex) Then
                            myColour = GetColour(i, j)
                            foundIt = True
                            Exit For
                        End If
                    Next
                End With

                If foundIt Then Exit For
            Next

            If Not foundIt Then
                For i = 0 To p_outputIndex - 1
                    If .GetOutputNextNode(i) = .GetOutputNextNode(p_outputIndex) Then
                        myColour = GetColour(p_sibbIndex, i)
                        Exit For
                    End If
                Next
            End If

            myPoints.Add(pStart)
            myPoints.Add(New AbsoluteLocationClass(currentPoint))

            If IsBelow(pStart, pEnd) Then
                myAngle = 90
            Else
                myAngle = 270
            End If

            If IsRight(currentPoint, pEndOffset) Then
                myNextAngle = 0
            Else
                myNextAngle = 180
            End If

            myLayoutBitmap.Plot(currentPoint.GetX, currentPoint.GetY, &HFF)

            While moving
                Dim changeAngle As Boolean = False
                Dim proposeChangeAngle As Boolean = False

                currentPoint.Move(myAngle)

                'If currentPoint.GetX = 15 Then MsgBox(0)

                Dim proposedPoint As New AbsoluteLocationClass(currentPoint)

                proposedPoint.Move(myAngle)

                ' Go as far as we can in this direction
                'MsgBox(proposedPoint.GetX & ", " & proposedPoint.GetY & ", " & myLayoutBitmap.ReadPixel(proposedPoint.GetX, proposedPoint.GetY))
                ' Change angle if ..
                ' We hit an internal boundary i.e. proposed point is on boundary
                ' myAngle = 0 and we are on the last column
                ' myAngle = 90 and we are on the last row
                ' myAngle = 180 and we are on the first column
                ' myAngle = 270 and we are on the first row
                ' myAngle = 180 or 0 and our x = pEnd.x - GAP
                ' myAngle = 90 or 270 and our y = pEnd.y

                ' Have we arrived at the end point ?
                If currentPoint.GetX = pEnd.GetX - (GAP + 1) And currentPoint.GetY = pEnd.GetY Then
                    moving = False
                    myPoints.Add(New AbsoluteLocationClass(currentPoint))
                    myPoints.Add(pEnd)
                Else
                    ' Work out if we should be changing direction
                    Select Case myAngle
                        Case 0
                            ' Heading East and in line with the end point x point
                            If currentPoint.GetX = pEndOffset.GetX Then proposeChangeAngle = True

                        Case 90
                            ' Heading South and in line with the end point y point
                            If currentPoint.GetY = pEndOffset.GetY Then proposeChangeAngle = True

                        Case 180
                            ' Heading West and hit the left hand edge
                            If currentPoint.GetX = 0 Then changeAngle = True

                            ' Heading West and in line with the end point x point
                            If currentPoint.GetX = pEndOffset.GetX Then proposeChangeAngle = True

                        Case 270
                            ' Heading North and hit the top edge
                            If currentPoint.GetY = 0 Then changeAngle = True

                            ' Heading North and in line with the end point y point
                            If currentPoint.GetY = pEndOffset.GetY Then proposeChangeAngle = True
                    End Select

                    If Not changeAngle Then
                        ' Are we about to hit a red line ?
                        If myLayoutBitmap.ReadPixel(proposedPoint.GetX, proposedPoint.GetY) = &HFF0000 Then changeAngle = True
                    End If

                    If Not changeAngle Then
                        If proposeChangeAngle Then
                            proposedPoint = New AbsoluteLocationClass(currentPoint)
                            proposedPoint.Move(myNextAngle)

                            If myLayoutBitmap.ReadPixel(proposedPoint.GetX, proposedPoint.GetY) < 256 Then
                                changeAngle = True
                            End If
                        End If

                        If myNextAngle >= 0 Then
                            proposedPoint = New AbsoluteLocationClass(currentPoint)
                            proposedPoint.Move(myNextAngle)

                            If myLayoutBitmap.ReadPixel(proposedPoint.GetX, proposedPoint.GetY) < 256 Then
                                changeAngle = True
                            End If
                        End If
                    End If
                End If

                If changeAngle Then
                    myPoints.Add(New AbsoluteLocationClass(currentPoint))
                    myLayoutBitmap.Plot(currentPoint.GetX, currentPoint.GetY, &HFF)

                    ' Work out the next angle
                    Select Case myAngle Mod 180
                        Case 0
                            ' Change left-right angle to up-down angle
                            If IsBelow(currentPoint, pEndOffset) Then
                                myAngle = 90
                            Else
                                myAngle = 270
                            End If

                        Case 90
                            ' Change up-down angle to left-right angle
                            If IsRight(currentPoint, pEndOffset) Then
                                myAngle = 0
                            Else
                                myAngle = 180
                            End If

                    End Select

                    ' Work out the one after this
                    myNextAngle = -1
                End If
            End While

            If False Then
                ' myGroupedOutputs will contain a list of our output indexes against the destination node index
                For i = 0 To .GetOutputCount - 1
                    Dim myNextNodeIndex As Integer = .outputs(i).nextNodeIndex

                    If Not myGroupedOutputs.ContainsKey(myNextNodeIndex) Then myGroupedOutputs.Add(myNextNodeIndex, New List(Of Integer))

                    myGroupedOutputs(myNextNodeIndex).Add(i)
                Next

                ' Is our output shared with any other output to the same destination ?
                For Each myDestinationNodeIndex As Integer In myGroupedOutputs.Keys
                    With myGroupedOutputs(myDestinationNodeIndex)
                        If .Contains(p_outputIndex) Then
                            If .Count > 1 Then
                                ' Yes
                                mySharedDestinationNodeIndex = myDestinationNodeIndex
                                Exit For
                            End If
                        End If
                    End With
                Next

                If mySharedDestinationNodeIndex = -1 Then
                    myPoints.Add(pStart)
                    myPoints.Add(New AbsoluteLocationClass(pStart, GAP, 0))
                    'myPoints.Add(New AbsoluteLocationClass(pStart.GetX + GAP, midPoint.GetY))
                    'myPoints.Add(New AbsoluteLocationClass(pEnd.GetX - GAP, midPoint.GetY))
                    myPoints.Add(New AbsoluteLocationClass(pEnd, -GAP, 0))
                    myPoints.Add(pEnd)

                    'DrawRouteNew(sibbList(p_sibbIndex), p_graphics, myColour, myPoints, False)
                Else
                    ' Are we the last one of the shared connections ?
                    If myGroupedOutputs(mySharedDestinationNodeIndex).IndexOf(p_outputIndex) = myGroupedOutputs(mySharedDestinationNodeIndex).Count - 1 Then
                        myPoints.Add(pStart)
                        myPoints.Add(New AbsoluteLocationClass(pStart, GAP, 0))
                        ' myPoints.Add(New AbsoluteLocationClass(pStart.GetX + GAP, midPoint.GetY))
                        ' myPoints.Add(New AbsoluteLocationClass(pEnd.GetX - GAP, midPoint.GetY))
                        myPoints.Add(New AbsoluteLocationClass(pEnd, -GAP, 0))
                        myPoints.Add(pEnd)
                    Else
                        myPoints.Add(pStart)
                        myPoints.Add(New AbsoluteLocationClass(pStart, GAP, 0))
                        myPoints.Add(New AbsoluteLocationClass(.GetConnectionStartPosition(myGroupedOutputs(mySharedDestinationNodeIndex).Item(myGroupedOutputs(mySharedDestinationNodeIndex).IndexOf(p_outputIndex) + 1)), GAP, 0))
                    End If
                End If

                ' Work out our colour
                'myColour = GetColour(.GetNodeIndex, myGroupedOutputs(mySharedDestinationNodeIndex).Item(0))
            End If

            'myPoints.RemoveRange(3, 247)
            'DetectDiagonal(myPoints)
            'DrawRouteNew(sibbList(p_sibbIndex), p_graphics, myColour, myPoints, False)
            DrawArrow(p_graphics, myColour, pEnd)
            'If outputJoinedTo = -1 Then DrawArrow(p_graphics, myColour, pEnd)
        End With
    End Sub

    Public Sub DrawAllConnection1(ByVal p_sibbIndex As Integer, ByRef p_graphics As Graphics, ByVal p_outputIndex As Integer)
        With sibbList(p_sibbIndex)
            Dim foundIt As Boolean = False
            Dim myColour = GetColour(.GetNodeIndex, p_outputIndex)  ' The default colour
            Dim nextNodeIndex As Integer = .outputs(p_outputIndex).nextNodeIndex
            Dim pStart As AbsoluteLocationClass = .GetConnectionStartPosition(p_outputIndex)
            Dim pEnd As AbsoluteLocationClass = sibbList(nextNodeIndex).GetConnectionEndPosition()
            Dim points As New List(Of AbsoluteLocationClass)
            Dim previousOutputDestinations As New Dictionary(Of Integer, Integer)
            Dim mySignDirection As Integer = 0
            Dim colourFixed As Boolean = False
            Dim outputJoinedTo As Integer = -1

            .outputs(p_outputIndex).tabValue = 1

            ' If .GetNodeTitle = "Get Queue Length" And p_outputIndex = 1 Then MsgBox(0)

            ' Is there already an output drawn from a previous SIBB to the same destination ?
            ' Loop over each of the previous sibb indexes
            For i = 0 To p_sibbIndex - 1
                With sibbList(i)
                    For j = 0 To .outputs.Count - 1
                        Dim myNextNodeIndex = .outputs(j).nextNodeIndex

                        ' Look for a match on the same destination node as our output
                        If myNextNodeIndex = nextNodeIndex Then
                            ' Found a match - fix our colour to be the same
                            myColour = GetColour(i, j)
                            colourFixed = True
                            Exit For
                        End If
                    Next
                End With
            Next

            ' Is there already a previous output from this node going to the same next node - if so, use the same colour
            ' Also, tab all the connections to the same next node to the same length
            ' Loop over all the previous connected outputs from this node
            For i = 0 To p_outputIndex - 1
                Dim thisOutputsNextNode As Integer = .outputs(i).nextNodeIndex

                If thisOutputsNextNode >= 0 Then
                    ' Work out the initial direction for this output (up is -1, and down is +1)
                    mySignDirection = -1

                    If .GetConnectionStartPosition(i).GetY < sibbList(.outputs(i).nextNodeIndex).GetConnectionEndPosition.GetY Then mySignDirection = 1

                    ' Check if this previous output is connecting to the same nextNode as us
                    If thisOutputsNextNode = .outputs(p_outputIndex).nextNodeIndex Then
                        ' Yes. Use the same tab index and colour
                        If Not colourFixed Then
                            myColour = GetColour(.GetNodeIndex, i)
                            colourFixed = True
                        End If

                        outputJoinedTo = i
                    Else
                        ' Adjust myNextNode to run from 1 instead of zero so we can have a negative version, and then make +ve or -ve for direction
                        thisOutputsNextNode = (thisOutputsNextNode + 1) * mySignDirection

                        ' Create the key if not already there and set count to zero
                        If Not previousOutputDestinations.ContainsKey(thisOutputsNextNode) Then previousOutputDestinations.Add(thisOutputsNextNode, 0)

                        ' Increment the count for this key
                        previousOutputDestinations(thisOutputsNextNode) += 1

                        ' Convert thisOutputsNextNode back to +ve zero based index
                        thisOutputsNextNode = Math.Abs(thisOutputsNextNode) - 1
                    End If
                End If
            Next

            If outputJoinedTo >= 0 Then
                Dim p As New AbsoluteLocationClass(pStart.GetX + (.outputs(outputJoinedTo).tabValue * GAP), pStart.GetY)

                points.Add(pStart)
                points.Add(p)
                pEnd = .GetConnectionStartPosition(outputJoinedTo)
                pEnd.SetX(p.GetX)
                points.Add(pEnd)
            Else
                ' Work out the initial direction for our output
                mySignDirection = -1

                If .GetConnectionStartPosition(p_outputIndex).GetY < sibbList(nextNodeIndex).GetConnectionEndPosition.GetY Then mySignDirection = 1

                ' Each key is for a unique next node and direction combination from this node
                For Each myKey As Integer In previousOutputDestinations.Keys
                    Select Case Math.Sign(myKey)
                        Case -1
                            Select Case mySignDirection
                                Case -1
                                    .outputs(p_outputIndex).tabValue += 1
                            End Select

                        Case 1
                            Select Case mySignDirection
                                Case -1
                                    .outputs(p_outputIndex).tabValue += 1

                                Case 1
                                    .outputs(p_outputIndex).tabValue += 1
                            End Select
                    End Select
                Next

                ' Is there already an output from a previous node going to the same next node - if so, use the same colour
                If Not foundIt Then
                    For i = 0 To .GetNodeIndex - 1
                        With sibbList(i)
                            For j = 0 To .outputs.Count - 1
                                If .outputs(j).nextNodeIndex = sibbList(p_sibbIndex).outputs(p_outputIndex).nextNodeIndex Then
                                    myColour = rgbValues(rgbIndexes((i + j) Mod rgbValues.Count))
                                    foundIt = True
                                    Exit For
                                End If
                            Next
                        End With

                        If foundIt Then Exit For
                    Next
                End If

                points.Add(pStart)

                ' Trivial case - direct connection to the right with no obstacles
                If pStart.GetY = pEnd.GetY And pEnd.GetX > pStart.GetX Then
                    points.Add(pEnd)
                Else
                    Dim plotting As Boolean = True
                    Dim doNorthSouth As Boolean = True
                    Dim blockingSibbIndex As Integer = -1

                    ' Add stub and then decide where to go
                    Dim p As New AbsoluteLocationClass(pStart.GetX + (.outputs(p_outputIndex).tabValue * GAP), pStart.GetY)

                    points.Add(p)
                    pEnd.SetX(pEnd.GetX - GAP)

                    While plotting
                        Dim lastIndex As Integer = points.Count - 1
                        Dim lastPointRef As New AbsoluteLocationClass(points(lastIndex))

                        ' Can we get to the end point in a single horizontal or vertical line without interruption ?
                        If False Then
                            Dim nextPoint As New AbsoluteLocationClass(pEnd)

                            points.Add(nextPoint)
                            plotting = False
                        Else
                            Dim myDirection As Directions = Directions.NONE
                            Dim nextPoint As New AbsoluteLocationClass

                            If doNorthSouth Then
                                If p.GetY < pEnd.GetY Then
                                    myDirection = Directions.SOUTH
                                Else
                                    myDirection = Directions.NORTH
                                End If
                            Else
                                If p.GetX < pEnd.GetX Then
                                    myDirection = Directions.EAST
                                Else
                                    myDirection = Directions.WEST
                                End If
                            End If

                            Select Case myDirection
                                Case Directions.SOUTH
                                    ' Anything in the way ?
                                    Dim nextY As Integer = pEnd.GetY

                                    ' Were we blocked before ?
                                    If blockingSibbIndex >= 0 Then
                                        ' Yes. Work out which way to go
                                        With sibbList(blockingSibbIndex)
                                            If nextY > (.GetYPos + .GetBottom) \ 2 Then
                                                ' Go underneath
                                                nextY = .GetBottom + SIBBClass.DODGE_GAP
                                            Else
                                                ' Go above
                                                nextY = .GetYPos - SIBBClass.DODGE_GAP
                                            End If
                                        End With

                                        blockingSibbIndex = -1
                                    End If

                                    For i = 0 To sibbList.Count - 1
                                        With sibbList(i)
                                            Dim result As Integer = .CollideWithTopSide(lastPointRef.GetX, lastPointRef.GetY, nextY)

                                            If result >= 0 Then
                                                If result < nextY Then
                                                    nextY = result
                                                    blockingSibbIndex = i
                                                    nextPoint.SetDetour()
                                                End If
                                            End If
                                        End With
                                    Next

                                    nextPoint.Set(lastPointRef.GetX, nextY)

                                Case Directions.NORTH
                                    ' Anything in the way ?
                                    Dim nextY As Integer = pEnd.GetY

                                    ' Were we blocked before ?
                                    If blockingSibbIndex >= 0 Then
                                        ' Yes. Work out which way to go
                                        With sibbList(blockingSibbIndex)
                                            If nextY > (.GetYPos + .GetBottom) \ 2 Then
                                                ' Go underneath
                                                nextY = .GetBottom + SIBBClass.DODGE_GAP
                                            Else
                                                ' Go above
                                                nextY = .GetYPos - SIBBClass.DODGE_GAP
                                            End If
                                        End With

                                        blockingSibbIndex = -1
                                    End If

                                    For i = 0 To sibbList.Count - 1
                                        With sibbList(i)
                                            Dim result As Integer = .CollideWithBottomSide(lastPointRef.GetX, lastPointRef.GetY, nextY)

                                            If result >= 0 Then
                                                If result > nextY Then
                                                    nextY = result
                                                    blockingSibbIndex = i
                                                    nextPoint.SetDetour()
                                                End If
                                            End If
                                        End With
                                    Next

                                    nextPoint.Set(lastPointRef.GetX, nextY)

                                Case Directions.WEST
                                    ' Anything in the way ?
                                    Dim nextX As Integer = pEnd.GetX

                                    ' Were we blocked before ?
                                    If blockingSibbIndex >= 0 Then
                                        ' Yes. Work out which way to go
                                        With sibbList(blockingSibbIndex)
                                            If nextX > (.GetXPos + .GetXPos + GetSIBBWidth()) \ 2 Then
                                                ' Go to the right
                                                nextX = .GetXPos + GetSIBBWidth() + SIBBClass.DODGE_GAP
                                            Else
                                                ' Go to the left
                                                nextX = .GetXPos - SIBBClass.DODGE_GAP

                                                nextX = LowestOf(nextX, pEnd.GetX)
                                            End If
                                        End With

                                        blockingSibbIndex = -1
                                    End If

                                    For i = 0 To sibbList.Count - 1
                                        With sibbList(i)
                                            Dim result As Integer = .CollideWithRightSide(lastPointRef.GetX, nextX, lastPointRef.GetY)

                                            If result >= 0 Then
                                                If result > nextX Then
                                                    nextX = result
                                                    blockingSibbIndex = i
                                                    nextPoint.SetDetour()
                                                End If
                                            End If
                                        End With
                                    Next

                                    nextPoint.Set(nextX, lastPointRef.GetY)

                                Case Directions.EAST
                                    ' Anything in the way ?
                                    Dim nextX As Integer = pEnd.GetX

                                    ' Were we blocked before ?
                                    If blockingSibbIndex >= 0 Then
                                        ' Yes. Work out which way to go
                                        With sibbList(blockingSibbIndex)
                                            If nextX > (.GetXPos + .GetXPos + GetSIBBWidth()) \ 2 Then
                                                ' Go to the right
                                                nextX = .GetXPos + GetSIBBWidth() + SIBBClass.DODGE_GAP
                                            Else
                                                ' Go to the left
                                                nextX = .GetXPos - SIBBClass.DODGE_GAP
                                            End If
                                        End With

                                        blockingSibbIndex = -1
                                    End If

                                    For i = 0 To sibbList.Count - 1
                                        With sibbList(i)
                                            Dim result As Integer = .CollideWithLeftSide(lastPointRef.GetX, nextX, lastPointRef.GetY)

                                            If result >= 0 Then
                                                If result < nextX Then
                                                    nextX = result
                                                    blockingSibbIndex = i
                                                    nextPoint.SetDetour()
                                                End If
                                            End If
                                        End With
                                    Next

                                    nextPoint.Set(nextX, lastPointRef.GetY)
                            End Select

                            points.Add(nextPoint)
                            doNorthSouth = Not doNorthSouth

                            If points.Count > 100 Then
                                MsgBox("> 100 points exceeded")
                                plotting = False
                            End If

                            If nextPoint.IsEqualTo(pEnd) Then plotting = False
                        End If
                    End While

                    pEnd.SetX(pEnd.GetX + GAP)
                End If

                points.Add(pEnd)
            End If

            For i = 0 To points.Count - 1
                points(i).SetNodeDest(nextNodeIndex)
            Next

            If AreWeDumpingToDebugTable() Then
                Dim pointsString As String = ""

                For i = 0 To points.Count - 1
                    Dim openChar As String = "("
                    Dim closeChar As String = ")"

                    If pointsString <> "" Then pointsString &= ", "

                    If points(i).IsDetour Then
                        openChar = "["
                        closeChar = "]"
                    End If

                    pointsString &= openChar & points(i).GetX & ", " & points(i).GetY & closeChar
                Next

                Dim mySql As String = "update sibbdebugoutputtable set points = " & WrapInSingleQuotes(pointsString) & " where sibbIndex = " & p_sibbIndex & " and outputIndex = " & p_outputIndex

                ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql)
            End If

            ' Filter the points
            FilterPoints(points)

            If AreWeDumpingToDebugTable() Then
                Dim pointsString As String = ""

                For i = 0 To points.Count - 1
                    Dim openChar As String = "("
                    Dim closeChar As String = ")"

                    If pointsString <> "" Then pointsString &= ", "

                    If points(i).IsDetour Then
                        openChar = "["
                        closeChar = "]"
                    End If

                    pointsString &= openChar & points(i).GetX & ", " & points(i).GetY & closeChar
                Next

                Dim mySql As String = "update sibbdebugoutputtable set filteredPoints = " & WrapInSingleQuotes(pointsString) & " where sibbIndex = " & p_sibbIndex & " and outputIndex = " & p_outputIndex

                ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql)
            End If

            'If .GetFooterTitle = "Day Of Week" Then DumpLines()

            DrawRoute(sibbList(p_sibbIndex), p_graphics, myColour, points)

            If outputJoinedTo = -1 Then DrawArrow(p_graphics, myColour, pEnd)
        End With
    End Sub

    Private Sub FilterPoints(ByRef p_points As List(Of AbsoluteLocationClass))
        Dim keepLooking As Boolean = True

        ' Remove any duplite points
        While keepLooking
            keepLooking = False

            For i = 0 To p_points.Count - 2
                If p_points(i).IsEqualTo(p_points(i + 1)) Then
                    p_points.RemoveAt(i + 1)
                    keepLooking = True
                    Exit For
                End If
            Next
        End While

        GenericFilter(p_points, Directions.EAST)
        GenericFilter(p_points, Directions.SOUTH)
        GenericFilter(p_points, Directions.SOUTH, GetSIBBWidth)
        GenericFilter(p_points, Directions.NORTH, GetSIBBWidth)
        StaircaseFilter(p_points, Directions.NORTH, Directions.WEST)
        StaircaseFilter(p_points, Directions.NORTH, Directions.EAST)
    End Sub

    Private Sub GenericFilter(ByRef p_points As List(Of AbsoluteLocationClass), ByVal p_direction As Directions, Optional ByVal p_maxWidth As Integer = 0)
        Dim keepLooking As Boolean = True

        While keepLooking
            keepLooking = False

            If p_maxWidth = 0 Then
                For i = 0 To p_points.Count - 3
                    If GetDirection(p_points(i), p_points(i + 1)) = p_direction Then
                        If GetDirection(p_points(i + 1), p_points(i + 2)) = GetOppositeDirection(p_direction) Then
                            p_points.RemoveAt(i + 1)
                            keepLooking = True
                            Exit For
                        End If
                    End If
                Next
            Else
                For i = 0 To p_points.Count - 4
                    If GetDirection(p_points(i), p_points(i + 1)) = p_direction Then
                        If GetDirection(p_points(i + 1), p_points(i + 2)) = Directions.EAST Or GetDirection(p_points(i + 1), p_points(i + 2)) = Directions.WEST Then
                            'If Math.Abs(p_points(i + 2).GetX - p_points(i + 1).GetX) <= p_maxWidth Then
                            If Not p_points(i).IsDetour Then
                                If GetDirection(p_points(i + 2), p_points(i + 3)) = GetOppositeDirection(p_direction) Then
                                    Dim removeIndex As Integer = 1
                                    Dim setToIndex As Integer = 2
                                    Dim setFromIndex = 0
                                    Dim secondRemoveIndex = 0

                                    If p_points(i).GetY < p_points(i + 3).GetY Then
                                        removeIndex = 3 - removeIndex
                                        setToIndex = 3 - setToIndex
                                        setFromIndex = 3 - setFromIndex
                                        secondRemoveIndex = 2
                                    End If

                                    p_points(i + setToIndex).SetY(p_points(i + setFromIndex).GetY)
                                    p_points.RemoveAt(i + removeIndex)

                                    If i > 0 Then p_points.RemoveAt(i + secondRemoveIndex)

                                    keepLooking = True
                                    Exit For
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        End While
    End Sub

    Private Sub StaircaseFilter(ByRef p_points As List(Of AbsoluteLocationClass), ByVal p_direction1 As Directions, ByVal p_direction2 As Directions)
        Dim keepLooking As Boolean = True

        While keepLooking
            keepLooking = False

            For i = 0 To p_points.Count - 5
                If GetDirection(p_points(i), p_points(i + 1)) = p_direction1 Then
                    If GetDirection(p_points(i + 1), p_points(i + 2)) = p_direction2 Then
                        If GetDirection(p_points(i + 2), p_points(i + 3)) = p_direction1 Then
                            If GetDirection(p_points(i + 3), p_points(i + 4)) = p_direction2 Then
                                p_points(i + 2).Set(p_points(i + 1).GetX, p_points(i + 3).GetY)
                                p_points.RemoveAt(i + 3)
                                p_points.RemoveAt(i + 1)
                                keepLooking = True
                                Exit For
                            End If
                        End If
                    End If
                End If
            Next
        End While
    End Sub

    Public Function GetDirection(ByRef p_from As AbsoluteLocationClass, ByRef p_to As AbsoluteLocationClass) As Directions
        Dim result As Directions = Directions.NONE

        If p_from.GetX = p_to.GetX Then
            If p_from.GetY < p_to.GetY Then
                result = Directions.SOUTH
            Else
                result = Directions.NORTH
            End If
        Else
            If p_from.GetX < p_to.GetX Then
                result = Directions.EAST
            Else
                result = Directions.WEST
            End If
        End If

        Return result
    End Function

    Public Sub DrawAllConnection(ByRef p_sibbRef As SIBBClass, ByRef p_graphics As Graphics, ByVal p_outputIndex As Integer)
        With p_sibbRef
            Dim points As New List(Of AbsoluteLocationClass)
            Dim myColour = rgbValues(rgbIndexes((p_sibbRef.GetNodeIndex + p_outputIndex) Mod rgbValues.Count))
            Dim myGapIndex As Integer = -1
            Dim pStart As AbsoluteLocationClass = .GetConnectionStartPosition(p_outputIndex)
            Dim pEnd As AbsoluteLocationClass = sibbList(.outputs(p_outputIndex).nextNodeIndex).GetConnectionEndPosition()
            Dim p3 As AbsoluteLocationClass = pStart + New AbsoluteLocationClass(GAP, 0)

            ' Is there already a previous output from this node going to the same next node - if so, use the same colour
            For i = 0 To p_outputIndex - 1
                If .outputs(i).nextNodeIndex = .outputs(p_outputIndex).nextNodeIndex Then
                    myColour = rgbValues(rgbIndexes((.GetNodeIndex + i) Mod rgbValues.Count))
                    Exit For
                End If
            Next

            ' Is there already an output from a previous node going to the same next node - if so, use the same colour
            Dim foundIt As Boolean = False

            For i = 0 To .GetNodeIndex - 1
                With sibbList(i)
                    For j = 0 To .outputs.Count - 1
                        If .outputs(j).nextNodeIndex = p_sibbRef.outputs(p_outputIndex).nextNodeIndex Then
                            myColour = rgbValues(rgbIndexes((i + j) Mod rgbValues.Count))
                            foundIt = True
                            Exit For
                        End If
                    Next
                End With

                If foundIt Then Exit For
            Next

            ' Work out how far we need to space ourselves to clear existing connections from this node
            Dim myList As New List(Of Integer)
            Dim myNextNode As Integer = .outputs(p_outputIndex).nextNodeIndex

            For i = 0 To p_outputIndex - 1
                With .outputs(i)
                    If .nextNodeIndex >= 0 Then
                        If Not myList.Contains(.nextNodeIndex) Then myList.Add(.nextNodeIndex)
                    End If
                End With
            Next

            If myList.Contains(myNextNode) Then
                myGapIndex = myList.IndexOf(myNextNode)
            Else
                myGapIndex = myList.Count
            End If

            p3.Add(GAP * myGapIndex, 0)

            ' Trivial case - direct connection to the right with no obstacles
            If pStart.GetY = pEnd.GetY And pEnd.GetX > pStart.GetX Then
                points.Add(pStart)
                points.Add(pEnd)
            Else
                ' To the right
                If pEnd.GetX > pStart.GetX Then
                    Dim p1 As AbsoluteLocationClass = p3
                    Dim p2 As New AbsoluteLocationClass(p1.GetX, pEnd.GetY)

                    points.Add(pStart)
                    points.Add(p1)
                    points.Add(p2)
                    points.Add(pEnd)
                Else
                    Dim p4, p5, p6 As New AbsoluteLocationClass

                    If .GetYPos > sibbList(myNextNode).GetBottom Then
                        p5 = New AbsoluteLocationClass(pEnd.GetX - GAP, (.GetYPos + sibbList(myNextNode).GetBottom) \ 2)
                    Else
                        p5 = pEnd + New AbsoluteLocationClass(-GAP, (sibbList(myNextNode).GetYPos - GAP) - pEnd.GetY)
                    End If

                    p6 = New AbsoluteLocationClass
                    p4 = New AbsoluteLocationClass
                    p6.Set(p5.GetX, pEnd.GetY)
                    p4.Set(p3.GetX, p5.GetY)

                    points.Add(pStart)
                    points.Add(p3)
                    points.Add(p4)

                    ' Is there a clear gap to aim for in the return path ?
                    Dim lineList As New List(Of Integer)

                    For y = .GetBottom + GAP To p5.GetY
                        lineList.Add(y)
                    Next

                    For i = 0 To sibbList.Count - 1
                        With sibbList(i)
                            If .GetBottom > p_sibbRef.GetBottom Then
                                If .GetTop < p5.GetY Then
                                    If .absoluteLocation.GetX + GetSIBBWidth() > p5.GetX Then
                                        If .absoluteLocation.GetX < p4.GetX Then
                                            For y = .absoluteLocation.GetY To .GetBottom
                                                If lineList.Contains(y) Then lineList.Remove(y)
                                            Next
                                        End If
                                    End If
                                End If
                            End If
                        End With
                    Next

                    If lineList.Count > 0 Then
                        Dim y As Integer = lineList(lineList.Count \ 2)

                        p4.SetY(y)
                        p5.SetY(y)
                    End If

                    For i = 0 To sibbList.Count - 1
                        With sibbList(i)
                            If .GetXPos >= pEnd.GetX And (.GetXPos + GetSIBBWidth()) <= pStart.GetX Then
                                If p4.GetY >= .GetYPos And p4.GetY <= (.GetYPos + .GetHeight) Then
                                    Dim p7, p8, p9, p10 As New AbsoluteLocationClass

                                    p7.Set(.GetXPos + GetSIBBWidth() + GAP, p4.GetY)
                                    p8.Set(p7.GetX, .GetYPos - GAP)
                                    p9.Set(.GetXPos - GAP, p8.GetY)
                                    p10.Set(p9.GetX, p4.GetY)

                                    points.Add(p7)
                                    points.Add(p8)
                                    points.Add(p9)
                                    points.Add(p10)
                                    Exit For
                                End If
                            End If
                        End With
                    Next

                    points.Add(p5)
                    points.Add(p6)
                    points.Add(pEnd)
                End If
            End If

            DrawRoute(p_sibbRef, p_graphics, myColour, points)
            DrawArrow(p_graphics, myColour, pEnd)
        End With
    End Sub

    Private Function IsHorizontal(ByRef p1 As AbsoluteLocationClass, ByRef p2 As AbsoluteLocationClass) As Boolean
        Dim result As Boolean = False

        If p1.GetY = p2.GetY Then result = True

        Return result
    End Function
End Module

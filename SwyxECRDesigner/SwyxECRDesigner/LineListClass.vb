Public Class LineListClass
    Public hLineList, vLineList, dLineList As New List(Of LineClass)
    Private hLastIndex, vLastIndex, dLastIndex As Integer

    Const MERGE_LINES As Boolean = True

    Public Sub Clear()
        hLineList.Clear()
        vLineList.Clear()
        dLineList.Clear()
    End Sub

    Public Function AddLine(ByVal p_nodeIndex As Integer, ByRef p_startPos As AbsoluteLocationClass, ByRef p_endPos As AbsoluteLocationClass, ByRef p_alreadyExists As Boolean) As Boolean
        Dim myNextNodeIndex As Integer = p_startPos.GetNodeDest
        Dim allDone As Boolean = False

        ' Does this line already exist ?
        p_alreadyExists = False

        For i = 0 To hLineList.Count - 1
            With hLineList(i)
                If .GetNodeIndex = p_nodeIndex Then
                    If .GetNextNodeIndex = myNextNodeIndex Then
                        If .startPos.IsEqualTo(p_startPos) Then
                            If .endPos.IsEqualTo(p_endPos) Then
                                p_alreadyExists = True
                            End If
                        End If
                    End If
                End If
            End With
        Next

        If Not p_alreadyExists Then
            Dim myLine As New LineClass(p_nodeIndex, p_startPos, p_endPos, myNextNodeIndex)
            Dim myList As New List(Of Integer)

            hLastIndex = -1 : vLastIndex = -1 : dLastIndex = -1

            If p_nodeIndex = 33 Or p_nodeIndex = 49 Then
                ' MsgBox(p_nodeIndex & ", " & p_startPos.GetNodeDest)
            End If
            ' Is it vertical ?
            If p_startPos.GetX = p_endPos.GetX Then
                ' Yes. Do we touch a horiztonal line to same destination on the way ?
                If MERGE_LINES Then
                    For i = 0 To hLineList.Count - 1
                        With hLineList(i)
                            If .IsXInRange(myLine.startPos.GetX) Then
                                If myLine.IsYInRange(.startPos.GetY) Then
                                    If .GetNextNodeIndex = myNextNodeIndex Then
                                        If GetDirection(p_startPos, p_endPos) = Directions.NORTH Then
                                            myList.Add(.startPos.GetY)
                                        Else
                                        End If
                                    End If
                                End If
                            End If
                        End With
                    Next

                    myList.Sort()

                    If myList.Count > 0 Then
                        Dim myY As Integer = myList(myList.Count - 1)

                        myLine.endPos.SetY(myY)
                        p_endPos.SetY(myY)
                        allDone = True
                    End If
                End If

                vLineList.Add(myLine)
                vLastIndex = vLineList.Count() - 1
            Else
                ' No - is it horizontal ?
                If p_startPos.GetY = p_endPos.GetY Then
                    ' Yes. Do we touch a vertical line to same destination on the way ?
                    If MERGE_LINES Then
                        For i = 0 To vLineList.Count - 1
                            With vLineList(i)
                                If .IsYInRange(myLine.startPos.GetY) Then
                                    If myLine.IsXInRange(.startPos.GetX) Then
                                        If .GetNextNodeIndex = myNextNodeIndex Then
                                            If GetDirection(p_startPos, p_endPos) = Directions.EAST Then
                                                myList.Add(.startPos.GetY)
                                            End If
                                        End If
                                    End If
                                End If
                            End With
                        Next

                        myList.Sort()

                        If myList.Count > 0 Then
                            Dim myX As Integer = myList(0)

                            myLine.endPos.SetX(myX)
                            p_endPos.SetX(myX)
                            allDone = True
                        End If
                    End If

                    hLineList.Add(myLine)
                    hLastIndex = hLineList.Count() - 1
                Else
                    ' No - diagonal
                    dLineList.Add(myLine)
                    dLastIndex = dLineList.Count() - 1
                End If
            End If
        End If

        Return allDone
    End Function

    Public Function LatestCrossesAnything() As List(Of AbsoluteLocationClass)
        Dim result As New List(Of AbsoluteLocationClass)
        Dim latestIndex As Integer = GreatestOf(hLastIndex, vLastIndex, dLastIndex)
        Dim latestIsVertical As Boolean = (vLastIndex > hLastIndex)
        Dim breakList As New List(Of Integer)
        Dim myRef As LineClass = Nothing
        Dim myList As New List(Of Integer)

        'Return result

        ' We are a vertical line
        If latestIsVertical Then
            myRef = vLineList(latestIndex)

            For i = 0 To hLineList.Count - 1
                With hLineList(i)
                    If .IsXInRange(myRef.startPos.GetX) Then
                        If myRef.IsYInRange(.startPos.GetY) Then myList.Add(.startPos.GetY)
                    End If
                End With
            Next

            myList.Sort()

            If myRef.startPos.GetY < myRef.endPos.GetY Then
                For i = 0 To myList.Count - 1
                    Dim x As New AbsoluteLocationClass(myRef.startPos.GetX, myList(i))

                    result.Add(x)
                Next
            Else
                For i = myList.Count - 1 To 0 Step -1
                    Dim x As New AbsoluteLocationClass(myRef.startPos.GetX, myList(i))

                    result.Add(x)
                Next
            End If
        Else
            myRef = hLineList(latestIndex)

            For i = 0 To vLineList.Count - 1
                With vLineList(i)
                    If .IsYInRange(myRef.startPos.GetY) Then
                        If myRef.IsXInRange(.startPos.GetX) Then myList.Add(.startPos.GetX)
                    End If
                End With
            Next

            myList.Sort()

            If myRef.startPos.GetX < myRef.endPos.GetX Then
                For i = 0 To myList.Count - 1
                    Dim x As New AbsoluteLocationClass(myList(i), myRef.startPos.GetY)

                    result.Add(x)
                Next
            Else
                For i = myList.Count - 1 To 0 Step -1
                    Dim x As New AbsoluteLocationClass(myList(i), myRef.startPos.GetY)

                    result.Add(x)
                Next
            End If
        End If

        Return result
    End Function

    Private Function IsVertical(ByVal p_index As Integer) As Boolean
        Dim result As Boolean = False

        '  With lineList(p_index)
        ' If .startPos.x = .endPos.x Then result = True
        '  End With

        Return result
    End Function

    Public Function GetVerticalLineCount() As Integer
        Return vLineList.Count
    End Function

    Public Sub GetVLine(ByVal p_index As Integer, ByRef p_x As Integer, ByRef p_y0 As Integer, ByRef p_y1 As Integer)
        With vLineList(p_index)
            p_x = .startPos.GetX
            p_y0 = .startPos.GetY
            p_y1 = .endPos.GetY
        End With
    End Sub

    Public Function GetHorizontalLineCount() As Integer
        Return hLineList.Count
    End Function

    Public Sub GetHLine(ByVal p_index As Integer, ByRef p_x0 As Integer, ByRef p_x1 As Integer, ByRef p_y As Integer)
        With hLineList(p_index)
            p_x0 = .startPos.GetX
            p_x1 = .endPos.GetX
            p_y = .endPos.GetY
        End With
    End Sub

    Public Sub RemoveForNode(ByVal p_nodeIndex)
        RemoveForNodeInLineList(hLineList, p_nodeIndex)
        RemoveForNodeInLineList(vLineList, p_nodeIndex)
        RemoveForNodeInLineList(dLineList, p_nodeIndex)
    End Sub

    Private Sub RemoveForNodeInLineList(ByRef p_lineList As List(Of LineClass), ByVal p_nodeIndex As Integer)
        Dim myList As New List(Of Integer)

        For i = 0 To p_lineList.Count - 1
            If p_lineList(i).GetnodeIndex = p_nodeIndex Then myList.Add(i)
        Next

        For i = myList.Count - 1 To 0 Step -1
            p_lineList.RemoveAt(myList(i))
        Next
    End Sub
End Class

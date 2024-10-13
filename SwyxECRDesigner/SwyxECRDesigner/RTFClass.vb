Public Class RTFClass
    Private myLines As New List(Of List(Of CharAndAttributeClass))
    Private currentLineIndex As Integer = -1
    Private currentAttribute As Byte = 0
    Private fontSize As Integer = 0
    Private fontList As New List(Of String)

    Public Sub New(ByVal p_fontSize As Integer)
        Clear()
        fontSize = p_fontSize
    End Sub

    Public Sub Clear()
        myLines.Clear()
        currentAttribute = 0
        currentLineIndex = -1
    End Sub

    Private Function GetLineIndex() As Integer
        Dim myLineIndex As Integer = 0

        If myLines.Count > 2 Then myLineIndex = myLines.Count - 2

        Return myLineIndex
    End Function

    Public Sub SetLineIndex(ByVal p_index As Integer)
        currentLineIndex = p_index
    End Sub

    Public Sub Add(ByVal p As Char, Optional ByVal p_attribute As Integer = -1)
        Dim myAttribute As Byte = currentAttribute

        If p_attribute >= 0 Then myAttribute = p_attribute

        Dim y As New CharAndAttributeClass(p, myAttribute)

        Add(y)
    End Sub

    Public Sub Add(ByVal p As CharAndAttributeClass)
        If myLines.Count = 0 Then
            Dim x As New List(Of CharAndAttributeClass)

            myLines.Add(x)
            currentLineIndex = 0
        End If

        myLines(currentLineIndex).Add(p)
    End Sub

    Public Sub AddText(ByRef p_text As String)
        For i = 0 To p_text.Length - 1
            Add(p_text(i))
        Next
    End Sub

    Public Sub AddLine(ByRef p As String)
        Dim y As New List(Of CharAndAttributeClass)

        For i = 0 To p.Length - 1
            Dim x As New CharAndAttributeClass(p(i), 0)

            y.Add(x)
        Next

        myLines.Add(y)
    End Sub

    Public Sub Delete()
        ' Is there anything to delete ?
        Dim myLength As Integer = myLines(currentLineIndex).Count

        If myLength = 0 Then
            ' Remove this line
            myLines.RemoveAt(currentLineIndex)
            currentLineIndex -= 1
        Else
            ' Remove the last character from this line
            myLines(currentLineIndex).RemoveAt(myLines(currentLineIndex).Count - 1)
        End If
    End Sub

    Public Sub NewLine()
        Dim x As New List(Of CharAndAttributeClass)

        myLines.Add(x)
        currentLineIndex += 1
    End Sub

    Public Function GetNumberOfLines() As Integer
        Dim result As Integer = myLines.Count

        Return result
    End Function

    Public Function GetLine(ByVal p_index As Integer) As String
        Dim result As String = ""

        For i = 0 To myLines(p_index).Count - 1
            result &= myLines(p_index)(i).myChar
        Next

        Return result
    End Function

    Public Function GetSelection(ByVal p_startIndex As Integer, ByVal p_length As Integer) As List(Of List(Of CharAndAttributeClass))
        Dim result As New List(Of List(Of CharAndAttributeClass))
        Dim lookingForStart As Boolean = True
        Dim myLineIndex As Integer = 0
        Dim startLineIndex As Integer = -1

        ' Get line index and offset within line of the start selection
        While lookingForStart
            If p_startIndex < (myLines(myLineIndex).Count + 1) Then
                lookingForStart = False
                startLineIndex = myLineIndex
            Else
                p_startIndex -= (myLines(myLineIndex).Count + 1)
                myLineIndex += 1
            End If
        End While

        If startLineIndex >= 0 Then
            Dim charsLeftInLine As Integer = myLines(startLineIndex).Count - p_startIndex

            For i = 0 To p_length - 1
                While charsLeftInLine <= 0
                    ' Start reading from next line
                    Dim x As New List(Of CharAndAttributeClass)

                    startLineIndex += 1
                    p_startIndex = 0
                    charsLeftInLine = myLines(startLineIndex).Count
                    i += 1
                    result.Add(x)
                End While

                result(result.Count - 1).Add(GetCharAndAttribute(startLineIndex, p_startIndex))
                p_startIndex += 1
                charsLeftInLine -= 1
            Next
        End If

        Return result
    End Function

    Public Sub InsertText(ByRef p As List(Of List(Of CharAndAttributeClass)), ByVal p_lineIndex As Integer, ByVal p_index As Integer)
        For i = 0 To p.Count - 1
            While myLines.Count <= p_lineIndex
                Dim x As New List(Of CharAndAttributeClass)

                myLines.Add(x)
            End While

            For j = 0 To p(i).Count - 1
                myLines(p_lineIndex + i).Add(p(i)(j))
            Next
        Next
    End Sub

    Public Function GetLineWithColour(ByVal p_index As Integer) As String
        Dim result As String = ""
        Dim lastColour As Integer = -1
        Dim lastFont As Integer = -1

        For i = 0 To myLines(p_index).Count - 1
            Dim thisColour As Integer = myLines(p_index)(i).myAttribute And 15
            Dim thisFont As Integer = myLines(p_index)(i).myAttribute \ 16
            Dim trimIt As Boolean = False

            If thisColour <> lastColour Then
                result &= "\cf" & thisColour + 1 & " "
                lastColour = thisColour
                trimIt = True
            End If

            If thisFont <> lastFont Then
                If trimIt Then result = result.TrimEnd

                result &= "\f" & thisFont & " "
                lastFont = thisFont
            End If

            result &= myLines(p_index)(i).myChar
        Next

        Return result
    End Function

    Public Function GetCurrentLineLength() As Integer
        Dim result As Integer = 0

        For i = 0 To currentLineIndex
            result += myLines(i).Count
        Next

        result += currentLineIndex

        Return result
    End Function

    Public Function MapIndexToLineIndex(ByVal p_index As Integer) As Integer
        Dim result As Integer = -1

        For i = 0 To myLines.Count - 1
            p_index -= myLines(i).Count

            If p_index <= 0 Then
                result = i
                Exit For
            Else
                p_index -= 1
            End If
        Next

        Return result
    End Function

    Public Sub ClearFonts()
        fontList.Clear()
    End Sub

    Public Sub AddFont(ByRef p_font As String)
        fontList.Add(p_font)
    End Sub

    Public Sub SetAttribute(ByVal p_lineIndex As Integer, ByVal p_index As Integer, ByVal p_length As Integer, ByVal p_attribute As Byte)
        For i = 0 To p_length - 1
            myLines(p_lineIndex)(p_index + i).myAttribute = p_attribute
        Next
    End Sub

    Public Sub SetText(ByVal p_lineIndex As Integer, ByVal p_index As Integer, ByRef p_text As String)
        For i = 0 To p_text.Length - 1
            myLines(p_lineIndex)(p_index + i).myChar = p_text(i)
        Next
    End Sub

    Public Function GetAttribute(ByVal p_lineIndex As Integer, ByVal p_index As Integer) As Integer
        Return myLines(p_lineIndex)(p_index).myAttribute
    End Function

    Public Function GetCharAndAttribute(ByVal p_lineIndex As Integer, ByVal p_index As Integer) As CharAndAttributeClass
        Dim z As New CharAndAttributeClass(myLines(p_lineIndex)(p_index).myChar, myLines(p_lineIndex)(p_index).myAttribute)

        Return z
    End Function

    Public Function Render() As String
        Dim x As String = "{\rtf1\ansi\ansicpg1252\deff0\deflang2057{\fonttbl"

        For i = 0 To fontList.Count - 1
            x &= "{\f" & i & "\fnil"

            If i = 0 Then x &= "\fcharset0"

            x &= " " & fontList(i) & ";}"
        Next

        x &= "}{\colortbl ;\red0\green0\blue0;\red60\green179\blue113;\red255\green0\blue0;\red0\green0\blue255;}"
        x &= "\viewkind4\uc1"

        For i = 0 To myLines.Count - 1
            Select Case i
                Case 0
                    x &= "\pard\fs" & fontSize & GetLineWithColour(i)

                Case Else
                    x &= "\par" & GetLineWithColour(i)
            End Select
        Next

        'x &= "\par"
        x &= "}"

        Return x
    End Function
End Class

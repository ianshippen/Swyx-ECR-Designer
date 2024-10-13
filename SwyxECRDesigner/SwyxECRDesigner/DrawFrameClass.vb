Public Class DrawFrameClass
    Private refreshData As New RefreshDataClass
    Private bitmapRef As Bitmap = Nothing
    Private m_width As Integer
    Private m_height As Integer
    Private m_x, m_y As Integer

    Public Sub New(ByVal p_bitmapRef As Bitmap)
        bitmapRef = p_bitmapRef
    End Sub

    Public Sub SetBitmapRef(ByVal p_bitmapRef As Bitmap)
        bitmapRef = p_bitmapRef
    End Sub

    Public Sub DrawFrame(ByRef p_position As AbsoluteLocationClass, ByVal p_width As Integer, ByVal p_height As Integer)
        Dim xStart, xEnd, yStart, yEnd As Integer

        refreshData.Init(p_position, p_width, p_height)

        If p_width >= 0 Then
            xStart = 0
            xEnd = p_width - 1
        Else
            xStart = p_width + 1
            xEnd = 0
        End If

        If p_height >= 0 Then
            yStart = 0
            yEnd = p_height - 1
        Else
            yStart = p_height + 1
            yEnd = 0
        End If

        ' Draw the top and bottom horizontal lines
        For i = xStart To xEnd
            refreshData.SetTopPixel(i - xStart, SafeGetPixel(bitmapRef, p_position.GetX + i, p_position.GetY + yStart))
            refreshData.SetBottomPixel(i - xStart, SafeGetPixel(bitmapRef, p_position.GetX + i, p_position.GetY + yEnd))
            SafeSetPixel(bitmapRef, p_position.GetX + i, p_position.GetY + yStart, Color.Black.ToArgb)
            SafeSetPixel(bitmapRef, p_position.GetX + i, p_position.GetY + yEnd, Color.Black.ToArgb)
        Next

        ' Draw the left and right verticle lines
        'For i = 1 To p_height - 2
        For i = yStart + 1 To yEnd - 1
            refreshData.SetLeftPixel(i - (yStart + 1), SafeGetPixel(bitmapRef, p_position.GetX + xStart, p_position.GetY + i))
            refreshData.SetRightPixel(i - (yStart + 1), SafeGetPixel(bitmapRef, p_position.GetX + xEnd, p_position.GetY + i))
            SafeSetPixel(bitmapRef, p_position.GetX + xStart, p_position.GetY + i, Color.Black.ToArgb)
            SafeSetPixel(bitmapRef, p_position.GetX + xEnd, p_position.GetY + i, Color.Black.ToArgb)
        Next

        refreshData.MakeValid()
        m_width = p_width
        m_height = p_height
        m_x = p_position.GetX
        m_y = p_position.GetY
    End Sub

    Public Sub EraseFrame()
        If refreshData.IsValid Then
            Dim xStart, xEnd, yStart, yEnd As Integer
            Dim myWidth As Integer = refreshData.GetWidth()
            Dim myHeight As Integer = refreshData.GetHeight()
            Dim myPosition As AbsoluteLocationClass = refreshData.GetPosition

            If myWidth >= 0 Then
                xStart = 0
                xEnd = myWidth - 1
            Else
                xStart = myWidth + 1
                xEnd = 0
            End If

            If myHeight >= 0 Then
                yStart = 0
                yEnd = myHeight - 1
            Else
                yStart = myHeight + 1
                yEnd = 0
            End If

            ' Restore the top and bottom edges
            For i = xStart To xEnd
                SafeSetPixel(bitmapRef, myPosition.GetX + i, myPosition.GetY + yStart, refreshData.GetTopPixel(i - xStart))
                SafeSetPixel(bitmapRef, myPosition.GetX + i, myPosition.GetY + yEnd, refreshData.GetBottomPixel(i - xStart))
            Next

            ' Restore the left and right edges
            For i = yStart + 1 To yEnd - 1
                SafeSetPixel(bitmapRef, myPosition.GetX + xStart, myPosition.GetY + i, refreshData.GetLeftPixel(i - (yStart + 1)))
                SafeSetPixel(bitmapRef, myPosition.GetX + xEnd, myPosition.GetY + i, refreshData.GetRightPixel(i - (yStart + 1)))
            Next

            refreshData.MakeInvalid()
        End If
    End Sub

    Public Function GetWidth() As Integer
        Return m_width
    End Function

    Public Function GetHeight() As Integer
        Return m_height
    End Function

    Public Function GetX() As Integer
        Return m_x
    End Function

    Public Function GetY() As Integer
        Return m_y
    End Function
End Class

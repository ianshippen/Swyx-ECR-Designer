Module Globals
    Private sibbWidth As Integer = 120
    Private sibbTitleHeight As Integer = 30
    Private sibbOutputHeight As Integer = 20
    Private sibbFooterHeight As Integer = 30
    Private fontSize As Integer = 10
    Private scale As String = "Normal"

    Public Function GetSIBBWidth() As Integer
        Return sibbWidth
    End Function

    Public Function GetSIBBTitleHeight() As Integer
        Return sibbTitleHeight
    End Function

    Public Function GetSIBBOutputHeight() As Integer
        Return sibbOutputHeight
    End Function

    Public Function GetSIBBFooterHeight() As Integer
        Return sibbFooterHeight
    End Function

    Public Function GetFontSize() As Integer
        Return fontSize
    End Function

    Public Sub SetToSmall()
        sibbWidth = 90
        sibbTitleHeight = 22
        sibbOutputHeight = 15
        sibbFooterHeight = 22
        fontSize = 8
        scale = "Small"
    End Sub

    Public Sub SetToNormal()
        sibbWidth = 120
        sibbTitleHeight = 30
        sibbOutputHeight = 20
        sibbFooterHeight = 30
        fontSize = 10
        scale = "Normal"
    End Sub

    Public Function GetScale() As String
        Return scale
    End Function

    Public Sub SetScale(ByRef p As String)
        Select Case p
            Case "Normal"
                SetToNormal()

            Case "Small"
                SetToSmall()
        End Select
    End Sub
End Module

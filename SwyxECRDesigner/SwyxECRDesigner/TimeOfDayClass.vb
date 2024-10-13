Public Class TimeOfDayClass
    Inherits SIBBClass

    Public startTime, endTime As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.New(SIBBTypeClass.SIBBTYPE.TIME_OF_DAY, p_bitmap)

        startTime = "00:00:00"
        endTime = "23:59:59"
    End Sub

    Public Overrides Sub PackData()
        PackData(startTime, endTime)
    End Sub

    Public Overrides Sub UnpackData()
        UnpackData(startTime, endTime)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(startTime, "Start Time")
        z.AddTextBoxLabelPair(endTime, "End Time")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        startTime = p_tabPage.Controls(0).Text
        endTime = p_tabPage.Controls(2).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Dim result As Integer = -1
        Dim myArray() As String = p_data.Split(",")
        Dim seconds As Integer = DateAndTime.Timer

        If myArray.Length = 2 Then
            Dim startSecond, endSecond As Integer
            Dim doCheck As Boolean = True

            result = 1

            If myArray(0) = "" Then
                If myArray(1) = "" Then
                    doCheck = False
                Else
                    startSecond = 0
                    endSecond = (3600 * CInt(Left(myArray(1), 2))) + (60 * CInt(Mid(myArray(1), 4, 2))) + CInt(Mid(myArray(1), 7))
                End If
            Else
                startSecond = (3600 * CInt(Left(myArray(0), 2))) + (60 * CInt(Mid(myArray(0), 4, 2))) + CInt(Mid(myArray(0), 7))

                If myArray(1) = "" Then
                    endSecond = 86399
                Else
                    endSecond = (3600 * CInt(Left(myArray(1), 2))) + (60 * CInt(Mid(myArray(1), 4, 2))) + CInt(Mid(myArray(1), 7))
                End If
            End If

            If doCheck Then
                If seconds >= startSecond And seconds <= endSecond Then result = 0
            End If
        End If

        Return result
    End Function
End Class

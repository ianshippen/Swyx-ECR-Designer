Public Class TODDOWSIBBClass
    Inherits SIBBClass

    Private daysOfWeek() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}
    Private openingTimes(daysOfWeek.Count - 1) As OpeningTimesClass
    Public allDaysOpeningTimes As New OpeningTimesClass
    Public weekDaysOpeningTimes As New OpeningTimesClass
    Public weekEndsOpeningTimes As New OpeningTimesClass
    Public monOpeningTimes As New OpeningTimesClass
    Public tueOpeningTimes As New OpeningTimesClass
    Public wedOpeningTimes As New OpeningTimesClass
    Public thuOpeningTimes As New OpeningTimesClass
    Public friOpeningTimes As New OpeningTimesClass
    Public satOpeningTimes As New OpeningTimesClass
    Public sunOpeningTimes As New OpeningTimesClass

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.new(SIBBTypeClass.SIBBTYPE.TODDOW, p_bitmap)

        For i = 0 To openingTimes.Count - 1
            openingTimes(i) = New OpeningTimesClass
        Next

    End Sub

    Public Overrides Sub PackData()
        PackData("")

        For i = 0 To openingTimes.Count - 1
            PackedDataAdd(openingTimes(i).ToString)
        Next
    End Sub

    Public Overrides Sub UnPackData()
        Dim myList As New List(Of String)

        UnPackData(myList)

        For i = 0 To openingTimes.Count - 1
            openingTimes(i).Set(myList(i))
        Next
    End Sub
End Class

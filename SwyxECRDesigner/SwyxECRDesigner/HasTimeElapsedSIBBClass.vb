Public Class HasTimeElapsedSIBBClass
    Inherits SIBBClass

    Public fromTime As String
    Public toTime As String
    Public thresholdSeconds As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.new(SIBBTypeClass.SIBBTYPE.HAS_TIME_ELAPSED, p_bitmap)

        fromtime = ""
        toTime = ""
        thresholdSeconds = 0
    End Sub

    Public Overrides Sub packdata()
        packdata(fromtime, toTime, thresholdSeconds)
    End Sub

    Public Overrides Sub UnpackData()
        Dim myList As New List(Of String)

        UnpackData(myList)

        fromtime = myList(0)
        toTime = myList(1)
        thresholdSeconds = myList(2)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(fromtime, "From Time")
        z.AddTextBoxLabelPair(toTime, "To Time")
        z.AddTextBoxLabelPair(thresholdSeconds, "Threshold in seconds")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        fromtime = p_tabPage.Controls(0).Text
        toTime = p_tabPage.Controls(2).Text
        thresholdSeconds = p_tabPage.Controls(4).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Dim result As Integer = -1
    End Function
End Class

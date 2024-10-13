Public Class GetDTMFDigitClass
    Inherits SIBBClass

    Public playAnnouncement As Boolean
    Public announcement As String
    Public repetitions As Integer
    Public interval As Integer
    Public maxDetectionTime As Boolean
    Public detectionTime As Integer
    Public saveInputInVariable As Boolean
    Public variableName As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.New(SIBBTypeClass.SIBBTYPE.GET_DTMF_DIGIT, p_bitmap)
        playAnnouncement = False
        announcement = ""
        repetitions = 0
        interval = 1
        maxDetectionTime = True
        detectionTime = 10
        saveInputInVariable = False
        variableName = ""
    End Sub

    Public Overrides Sub PackData()
        Dim myData As String = ""

        For i = 0 To GetOutputCount() - 1
            If OutputIsVisible(i) Then
                If i <= 9 Then
                    myData &= i
                Else
                    If i = 10 Then myData &= "*"
                    If i = 11 Then myData &= "#"
                End If
            End If
        Next

        PackData(playAnnouncement, announcement, repetitions, interval, maxDetectionTime, detectionTime, saveInputInVariable, variableName, "True", myData)
    End Sub

    Public Overrides Sub UnpackData()
        Dim myList As New List(Of String)

        UnpackData(mylist)

        playAnnouncement = CBool(mylist(0))
        announcement = myList(1)
        repetitions = myList(2)
        interval = myList(3)
        maxDetectionTime = CBool(myList(4))
        detectionTime = myList(5)
        saveInputInVariable = CBool(myList(6))
        variableName = myList(7)

        ' What about parameters 8 and 9 ?
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddCheckBoxLabelPair(playAnnouncement, "Play Announcement")
        z.AddTextBoxLabelPair(announcement, "Announcement name")
        z.AddTextBoxLabelPair(repetitions, "Repetitions")
        z.AddTextBoxLabelPair(interval, "Interval between repetitions")
        z.AddCheckBoxLabelPair(maxDetectionTime, "Max detection time")
        z.AddTextBoxLabelPair(detectionTime, "Detection time")
        z.AddCheckBoxLabelPair(saveInputInVariable, "Save input in variable")
        z.AddTextBoxLabelPair(variableName, "Variable name (in quotes)")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim x As CheckBox = p_tabPage.Controls(0)

        playAnnouncement = x.Checked
        announcement = p_tabPage.Controls(1).Text
        repetitions = p_tabPage.Controls(3).Text
        interval = p_tabPage.Controls(5).Text

        x = p_tabPage.Controls(7)
        maxDetectionTime = x.Checked

        detectionTime = p_tabPage.Controls(8).Text

        x = p_tabPage.Controls(10)
        saveInputInVariable = x.Checked

        variableName = p_tabPage.Controls(11).Text
        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Dim result As Integer = -1
        Dim myArray() As String = p_data.Split(",")
        Dim playAnnouncement As Boolean = CBool(myArray(0))
        Dim sound As String = "" : If playAnnouncement Then sound = myArray(1)
        Dim repetitions As Integer = myArray(2)
        Dim interval As Integer = myArray(3)
        Dim timeout As Integer = myArray(5)
        Dim variableName As String = myArray(7)
        Dim resetContent As Boolean = Not CBool(myArray(8))
        Dim mask As String = myArray(9)
        Dim myLocal As String = ""
        Dim storeToVariable As Boolean = False : If variableName <> "" Then storeToVariable = True

        With GetDTMFDigitForm
            .TextBox1.Text = playAnnouncement
            .TextBox2.Text = sound
            .TextBox3.Text = repetitions
            .TextBox4.Text = timeout
            .TextBox5.Text = variableName
            .TextBox6.Text = mask
            .TextBox7.Text = interval

            If .ShowDialog = DialogResult.OK Then result = GetDTMFDigitForm.result
        End With

        If variableName <> "" Then TestForm.SetVar(variableName, myLocal)

        Return result
    End Function
End Class

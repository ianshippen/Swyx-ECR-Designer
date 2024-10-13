Public Class GetDTMFStringClass
    Inherits SIBBClass

    Public playAnnouncement As Boolean
    Public announcement As String
    Public variableName As String
    Public append As Boolean
    Public tagData As String
    Public maxLength As Integer
    Public delimiter As String
    Public detectionTime As Integer

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.new(SIBBTypeClass.SIBBTYPE.GET_DTMF_STRING, p_bitmap)

        playAnnouncement = False
        announcement = ""
        variableName = ""
        append = False
        tagData = ""
        maxLength = -1
        delimiter = ""
        detectionTime = -1
    End Sub

    Public Overrides Sub PackData()
        PackData(playAnnouncement.ToString, announcement, variableName, append.ToString, tagData, maxLength, delimiter, detectionTime)
    End Sub

    Public Overrides Sub UnpackData()
        Dim myList As New List(Of String)

        UnpackData(myList)

        If myList.Count = 8 Then
            playAnnouncement = myList(0)
            announcement = myList(1)
            variableName = myList(2)
            append = myList(3)
            tagData = myList(4)
            maxLength = myList(5)
            delimiter = myList(6)
            detectionTime = myList(7)
        End If
    End Sub


    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddCheckBoxLabelPair(playAnnouncement, "Play Announcement")
        z.AddTextBoxLabelPair(announcement, "Announcement name")
        z.AddTextBoxLabelPair(variableName, "Variable name (in quotes)")
        z.AddCheckBoxLabelPair(append, "Append string")
        z.AddTextBoxLabelPair(tagData, "Tag data")
        z.AddTextBoxLabelPair(maxLength, "Maximum string length")
        z.AddTextBoxLabelPair(delimiter, "Delimiter")
        z.AddTextBoxLabelPair(detectionTime, "Detection time in seconds")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim x As CheckBox = p_tabPage.Controls(0)

        playAnnouncement = x.Checked
        announcement = p_tabPage.Controls(1).Text
        variableName = p_tabPage.Controls(3).Text

        x = p_tabPage.Controls(5)
        append = x.Checked

        tagData = p_tabPage.Controls(6).Text
        maxLength = p_tabPage.Controls(8).Text
        delimiter = p_tabPage.Controls(10).Text
        detectionTime = p_tabPage.Controls(12).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Dim myArray() As String = p_data.Split(",")
        Dim result As Integer = -1
        Dim playAnnouncement As Boolean = CBool(myArray(0))
        Dim sound As String = myArray(1)
        Dim variableName As String = myArray(2)
        Dim resetContent As Boolean = Not CBool(myArray(3))
        Dim maxLength As Integer = myArray(5)
        Dim delimiter As String = myArray(6)
        Dim timeout As Integer = myArray(7)
        Dim stopChar As String = ""
        Dim stopOnChar, stopOnLength As Boolean
        Dim myLocal

        If delimiter = "" Then
            stopChar = "#"
            stopOnChar = False
        Else
            stopChar = delimiter
            stopOnChar = True
        End If

        If maxLength >= 0 Then
            stopOnLength = True
        Else
            stopOnLength = False
            maxLength = 0
        End If

        With GetDTMFStringForm
            .TextBox1.Text = playAnnouncement
            .TextBox2.Text = announcement
            .TextBox3.Text = variableName
            .TextBox4.Text = resetContent
            .TextBox5.Text = maxLength
            .TextBox10.Text = delimiter
            .TextBox9.Text = timeout
            .TextBox8.Text = stopChar
            .TextBox7.Text = stopOnChar
            .TextBox6.Text = stopOnLength

            If .ShowDialog = DialogResult.OK Then
                result = 0

                If variableName <> "" Then
                    If Left(variableName, 1) = Chr(34) Then variableName = Right(variableName, Len(variableName) - 1)
                    If Right(variableName, 1) = Chr(34) Then variableName = Left(variableName, Len(variableName) - 1)

                    myLocal = GetDTMFStringForm.resultTextBox.Text

                    TestForm.SetVar(variableName, myLocal)
                End If
            End If
        End With

        Return result
    End Function
End Class

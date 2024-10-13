Public Class VoicemailSIBBClass
    Inherits SIBBClass

    Public useStdVoicemail As Boolean
    Public playWelcomeAnnouncement As Boolean
    Public welcomeAnnouncement As String
    Public useDTMFasCallerId As Boolean
    Public callerIdAnnouncement As String
    Public useAnnouncement As Boolean
    Public announcement As String
    Public maxDuration As Integer
    Public emailAddress As String
    Public saveFilenameInVariable As Boolean

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.new(SIBBTypeClass.SIBBTYPE.VOICEMAIL, p_bitmap)

        useStdVoicemail = True
        playWelcomeAnnouncement = False
        welcomeAnnouncement = ""
        useDTMFasCallerId = False
        callerIdAnnouncement = ""
        useAnnouncement = False
        announcement = ""
        maxDuration = 300
        emailAddress = ""
        saveFilenameInVariable = False
    End Sub

    Public Overrides Sub PackData()
        PackData(useStdVoicemail, playWelcomeAnnouncement, welcomeAnnouncement, useDTMFasCallerId, callerIdAnnouncement, useAnnouncement, announcement, maxDuration, emailAddress, saveFilenameInVariable)
    End Sub

    Public Overrides Sub UnpackData()
        Dim myList As New List(Of String)

        UnpackData(myList)

        If myList.Count = 10 Then
            useStdVoicemail = CBool(myList(0))
            playWelcomeAnnouncement = CBool(myList(1))
            welcomeAnnouncement = myList(2)
            useDTMFasCallerId = CBool(myList(3))
            callerIdAnnouncement = myList(4)
            useAnnouncement = CBool(myList(5))
            announcement = myList(6)
            maxDuration = CInt(myList(7))
            emailAddress = myList(8)
            saveFilenameInVariable = CBool(myList(9))
        End If
    End Sub
End Class

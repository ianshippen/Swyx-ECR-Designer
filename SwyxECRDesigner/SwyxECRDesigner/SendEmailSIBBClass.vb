Public Class SendEmailSIBBClass
    Inherits SIBBClass

    Public emailTo As String
    Public emailSubject As String
    Public emailBody As String
    Public emailUserName As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.New(SIBBTypeClass.SIBBTYPE.SEND_EMAIL, p_bitmap)

        emailTo = ""
        emailSubject = ""
        emailBody = ""
        emailUserName = ""
    End Sub

    Public Overrides Sub packdata()
        packdata(emailTo, emailSubject, emailBody, emailUserName)
    End Sub

    Public Overrides Sub UnpackData()
        Dim myList As New List(Of String)

        UnpackData(myList)

        emailTo = myList(0)
        emailSubject = myList(1)
        emailBody = myList(2)

        If myList.Count >= 4 Then emailUserName = myList(3)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(emailTo, "Email To")
        z.AddTextBoxLabelPair(emailSubject, "Email Subject")
        z.AddTextBoxLabelPair(emailBody, "Email Body")
        z.AddTextBoxLabelPair(emailUserName, "Email User Name")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        emailTo = p_tabPage.Controls(0).Text
        emailSubject = p_tabPage.Controls(2).Text
        emailBody = p_tabPage.Controls(4).Text
        emailUserName = p_tabPage.Controls(6).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub
End Class

Public Class ConnectSIBBClass
    Inherits SIBBClass

    Public destination As String
    Public timeout As String
    Public alertSound As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.new(SIBBTypeClass.SIBBTYPE.CONNECT, p_bitmap)

        destination = ""
        timeout = 30
    End Sub

    Public Overrides Sub packdata()
        packdata(destination, timeout, alertSound)
    End Sub

    Public Overrides Sub UnpackData()
        Dim myList As New List(Of String)

        UnpackData(myList)

        destination = myList(0)
        timeout = myList(1)
        alertSound = ""

        If myList.Count = 3 Then alertSound = myList(2)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(destination, "Destination")
        z.AddTextBoxLabelPair(timeout, "Timeout")
        z.AddTextBoxLabelPair(alertSound, "Alert Sound")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        destination = p_tabPage.Controls(0).Text
        timeout = p_tabPage.Controls(2).Text
        alertSound = p_tabPage.Controls(4).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Dim result As Integer = -1
        Dim myArray() As String = p_data.Split(",")
        Dim dest As String = ""
        Dim timeout As String = ""
        Dim alertSound As String = ""
        Dim useAlertSound As Boolean = False

        If myArray.Length > 1 Then
            dest = myArray(0)
            timeout = CInt(myArray(1))

            If myArray.Length = 3 Then
                If myArray(2) <> "" Then
                    alertSound = myArray(2)
                    useAlertSound = True
                End If
            End If
        End If

        With ConnectForm
            .TextBox2.Text = myArray(0)
            .TextBox4.Text = myArray(1)
            .TextBox6.Text = useAlertSound
            .TextBox8.Text = myArray(2)

            .TextBox1.Text = .TextBox2.Text
            .TextBox3.Text = .TextBox4.Text
            .TextBox5.Text = .TextBox6.Text
            .TextBox7.Text = .TextBox8.Text
        End With

        If ConnectForm.ShowDialog = DialogResult.OK Then
            result = ConnectForm.result
        End If

        Return result
    End Function
End Class

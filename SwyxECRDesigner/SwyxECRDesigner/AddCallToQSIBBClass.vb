Public Class AddCallToQueueSIBBClass
    Inherits SIBBClass

    Public callId As String
    Public queueId As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.new(SIBBTypeClass.SIBBTYPE.ADD_CALL_TO_QUEUE, p_bitmap)

        callId = 0
        queueId = ""
    End Sub

    Public Overrides Sub packdata()
        packdata(callId, queueId)
    End Sub

    Public Overrides Sub UnpackData()
        Dim myList As New List(Of String)

        UnpackData(myList)

        callId = myList(0)
        queueId = myList(1)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(callId, "Call Id  [0 = Current Call]")
        z.AddTextBoxLabelPair(queueId, "Queue Id")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        callId = p_tabPage.Controls(0).Text
        queueId = p_tabPage.Controls(2).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Dim result As Integer = -1
    End Function
End Class

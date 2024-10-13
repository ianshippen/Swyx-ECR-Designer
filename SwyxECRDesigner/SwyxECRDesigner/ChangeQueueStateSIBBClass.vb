Public Class ChangeQueueStateSIBBClass
    Inherits SIBBClass

    Private callId As String
    Private queueState As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.new(SIBBTypeClass.SIBBTYPE.CHANGE_QUEUE_STATE, p_bitmap)

        callId = 0
        queueState = ""
    End Sub

    Public Overrides Sub packdata()
        packdata(callId, queueState)
    End Sub

    Public Overrides Sub UnpackData()
        Dim myList As New List(Of String)

        UnpackData(myList)

        callId = myList(0)
        queueState = myList(1)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)
        Dim myList As New List(Of String)

        MyBase.SetupForm(p_tabPage)

        myList.Add("Answered")
        myList.Add("Queueing")
        myList.Add("Ringing")
        myList.Add("NotInQueue")

        myList.Sort()
        z.AddTextBoxLabelPair(callId, "Call Id  [0 = Current Call]")
        z.AddTextBoxLabelPair(queueState, "Queue State", True, myList)
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        callId = p_tabPage.Controls(0).Text
        queueState = p_tabPage.Controls(2).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Dim result As Integer = -1
    End Function
End Class

Public Class RemoveCallFromQueueSIBBClass
    Inherits SIBBClass

    Public callId As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.new(SIBBTypeClass.SIBBTYPE.REMOVE_CALL_FROM_QUEUE, p_bitmap)

        callId = 0
    End Sub

    Public Overrides Sub packdata()
        packdata(callId)
    End Sub

    Public Overrides Sub UnpackData()
        Dim myList As New List(Of String)

        UnpackData(myList)

        callId = myList(0)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(callId, "Call Id  [0 = This Call]")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        callId = p_tabPage.Controls(0).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Dim result As Integer = -1
    End Function
End Class

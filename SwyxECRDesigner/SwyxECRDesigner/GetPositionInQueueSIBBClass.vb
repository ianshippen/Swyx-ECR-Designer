Public Class GetPositionInQueueSIBBClass
    Inherits SIBBClass

    Public callId As String
    Public timeout As String
    Public maxQLength As String
    Public storeInVariable As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.new(SIBBTypeClass.SIBBTYPE.GET_POSITION_IN_QUEUE, p_bitmap)

        callId = 0
        timeout = 0
        maxQLength = 0
        storeInVariable = ""
    End Sub

    Public Overrides Sub packdata()
        packdata(callId, timeout, maxQLength, storeInVariable)
    End Sub

    Public Overrides Sub UnpackData()
        Dim myList As New List(Of String)

        UnpackData(myList)

        callId = myList(0)
        timeout = myList(1)
        maxQLength = myList(2)
        storeInVariable = myList(3)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(callId, "Call Id  [0 = This Call]")
        z.AddTextBoxLabelPair(timeout, "Timeout / Seconds")
        z.AddTextBoxLabelPair(maxQLength, "Maximum Queue Length")
        z.AddTextBoxLabelPair(storeInVariable, "Store In Variable")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        callId = p_tabPage.Controls(0).Text
        timeout = p_tabPage.Controls(2).Text
        maxQLength = p_tabPage.Controls(4).Text
        storeInVariable = p_tabPage.Controls(6).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Dim result As Integer = -1
    End Function
End Class

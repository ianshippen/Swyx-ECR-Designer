Public Class TopOfQueueSIBBClass
    Inherits SIBBClass

    Public callId As String
    Public queueTimeout As String
    Public maxQueueLength As String
    Public storeInVariable As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.new(SIBBTypeClass.SIBBTYPE.TOP_OF_QUEUE, p_bitmap)

        callId = 0
        queueTimeout = 0
        maxQueueLength = 0
        storeInVariable = ""
    End Sub

    Public Overrides Sub packdata()
        packdata(callId, queueTimeout, maxQueueLength, storeInVariable)
    End Sub

    Public Overrides Sub UnpackData()
        Dim myList As New List(Of String)

        UnpackData(myList)

        callId = myList(0)
        queueTimeout = myList(1)
        maxQueueLength = myList(2)
        storeInVariable = myList(3)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(callId, "Call Id  [0 = Current Call]")
        z.AddTextBoxLabelPair(queueTimeout, "Queue timeout seconds")
        z.AddTextBoxLabelPair(maxQueueLength, "Max queue length")
        z.AddTextBoxLabelPair(storeInVariable, "Store Q Pos In Variable")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        callId = p_tabPage.Controls(0).Text
        queueTimeout = p_tabPage.Controls(2).Text
        maxQueueLength = p_tabPage.Controls(4).Text
        storeInVariable = p_tabPage.Controls(6).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Dim result As Integer = -1
    End Function
End Class

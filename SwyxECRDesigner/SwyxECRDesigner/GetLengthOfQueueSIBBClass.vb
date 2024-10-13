Public Class GetQueueLength
    Inherits SIBBClass

    Public queueId As String
    Public threshold As String
    Public storeInVariable As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.new(SIBBTypeClass.SIBBTYPE.GET_LENGTH_OF_QUEUE, p_bitmap)

        queueId = ""
        threshold = 0
        storeInVariable = ""
    End Sub

    Public Overrides Sub packdata()
        packdata(queueId, threshold, storeInVariable)
    End Sub

    Public Overrides Sub UnpackData()
        Dim myList As New List(Of String)

        UnpackData(myList)

        queueId = myList(0)
        threshold = myList(1)
        storeInVariable = myList(2)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(queueId, "Queue Id")
        z.AddTextBoxLabelPair(threshold, "Threshold")
        z.AddTextBoxLabelPair(storeInVariable, "Store In Variable")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        queueId = p_tabPage.Controls(0).Text
        threshold = p_tabPage.Controls(2).Text
        storeInVariable = p_tabPage.Controls(4).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Dim result As Integer = -1
    End Function
End Class

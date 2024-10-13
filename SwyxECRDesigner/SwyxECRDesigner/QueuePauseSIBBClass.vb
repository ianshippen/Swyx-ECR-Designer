Public Class QueuePauseSIBBClass
    Inherits SIBBClass

    Public shortTimer As Integer
    Public longTimer As Integer
    Public rule As String
    Public queueTimeout As Integer
    Public maxQLength As Integer
    Public mediumTimer As Integer
    Public mediumQThreshold As Integer

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.new(SIBBTypeClass.SIBBTYPE.QUEUE_PAUSE, p_bitmap)

        shortTimer = 5
        longTimer = 30
        rule = "2"
        queueTimeout = 300
        maxQLength = 100
        mediumTimer = 15
        mediumQThreshold = 3
    End Sub

    Public Overrides Sub packdata()
        packdata(shortTimer, longTimer, rule, queueTimeout, maxQLength, mediumTimer, mediumQThreshold)
    End Sub

    Public Overrides Sub UnpackData()
        Dim myList As New List(Of String)

        UnpackData(myList)

        shortTimer = CInt(myList(0))
        longTimer = CInt(myList(1))
        rule = myList(2)
        queueTimeout = CInt(myList(3))
        maxQLength = CInt(myList(4))
        mediumTimer = CInt(myList(5))
        mediumQThreshold = CInt(myList(6))
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(shortTimer, "Short Timer")
        z.AddTextBoxLabelPair(longTimer, "Long Timer")
        z.AddTextBoxLabelPair(rule, "Rule")
        z.AddTextBoxLabelPair(queueTimeout, "Queue Timeout")
        z.AddTextBoxLabelPair(maxQLength, "Max Q Length")
        z.AddTextBoxLabelPair(mediumTimer, "Medium Timer")
        z.AddTextBoxLabelPair(mediumQThreshold, "Medium Q Threshold")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        shortTimer = CInt(p_tabPage.Controls(0).Text)
        longTimer = CInt(p_tabPage.Controls(2).Text)
        rule = p_tabPage.Controls(4).Text
        queueTimeout = CInt(p_tabPage.Controls(6).Text)
        maxQLength = CInt(p_tabPage.Controls(8).Text)
        mediumTimer = CInt(p_tabPage.Controls(10).Text)
        mediumQThreshold = CInt(p_tabPage.Controls(12).Text)

        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Dim result As Integer = -1
    End Function
End Class

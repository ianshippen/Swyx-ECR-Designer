Public Class LogPointSIBBClass
    Inherits SIBBClass

    Public point As String
    Public firstOnly As Boolean

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.New(SIBBTypeClass.SIBBTYPE.LOG_POINT, p_bitmap)

        point = "0"
        firstOnly = False
    End Sub

    Public Overrides Sub PackData()
        PackData(point, firstOnly)
    End Sub

    Public Overrides Sub UnpackData()
        Dim myList As New List(Of String)

        UnpackData(myList)

        point = myList(0)
        firstOnly = CBool(myList(1))
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(point, "Log Point Number")
        z.AddCheckBoxLabelPair(firstOnly, "Log First Time Only")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim x As CheckBox = p_tabPage.Controls(2)

        point = p_tabPage.Controls(0).Text
        firstOnly = x.Checked

        MyBase.TakedownForm(p_tabPage)
    End Sub

End Class

Public Class PauseSIBBClass
    Inherits SIBBClass

    Public target As String
    Public shortTimer As Integer
    Public longTimer As Integer
    Public rule As String

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.New(SIBBTypeClass.SIBBTYPE.PAUSE, p_bitmap)

        target = "$primaryTeam"
        shortTimer = 5
        longTimer = 20
        rule = "$yellowOption"
    End Sub

    Public Overrides Sub PackData()
        PackData(target, shortTimer, longTimer, rule)
    End Sub

    Public Overrides Sub UnpackData()
        UnpackData(target, shortTimer, longTimer, rule)
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        Dim z As New GUIBuilderClass(p_tabPage)

        MyBase.SetupForm(p_tabPage)

        z.AddTextBoxLabelPair(target, "Target")
        z.AddTextBoxLabelPair(shortTimer, "Short Timer")
        z.AddTextBoxLabelPair(longTimer, "Long Timer")
        z.AddTextBoxLabelPair(rule, "Rule")
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        target = p_tabPage.Controls(0).Text
        shortTimer = p_tabPage.Controls(2).Text
        longTimer = p_tabPage.Controls(4).Text
        rule = p_tabPage.Controls(6).Text

        MyBase.TakedownForm(p_tabPage)
    End Sub

End Class

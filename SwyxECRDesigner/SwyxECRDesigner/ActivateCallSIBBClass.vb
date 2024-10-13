Public Class ActivateCallSIBBClass
    Inherits SIBBClass

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.New(SIBBTypeClass.SIBBTYPE.ACTIVATE_CALL, p_bitmap)
    End Sub
End Class

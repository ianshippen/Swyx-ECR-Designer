Public Class SIBBClass
    ' Public Const GetSIBBWidth() As Integer = 120 * 0.75
    'Private Const SIBB_TITLE_HEIGHT As Integer = 30 * 0.75
    'Private Const SIBB_OUTPUT_HEIGHT As Integer = 20 * 0.75
    ' Private Const SIBB_FOOTER_HEIGHT As Integer = 30 * 0.75
    Private Const LINE_LIMIT As Integer = 10
    Private Const AUX_WIDTH As Integer = 10
    Public Const DODGE_GAP As Integer = 5

    Public absoluteLocation As New AbsoluteLocationClass
    Private type As SIBBTypeClass.SIBBTYPE = SIBBTypeClass.SIBBTYPE.NULL
    Private typeIndex As Integer
    Private nodeTitle As String
    Public outputs As New List(Of SIBBOutputClass)
    Private bitmapRef As Bitmap
    Private hilighted As Boolean
    Private data As String
    Private debugSelected, stepDebugSelected, debugHistorySelected As Boolean
    Private nodeIndex As Integer
    Private internalReference As String
    Private drawFrameObject As DrawFrameClass
    Private showInCallTrace As Boolean

    Public Sub New(ByVal p_type As SIBBTypeClass.SIBBTYPE, ByRef p_bitmap As Bitmap)
        ' Get the index of this SIBB type
        typeIndex = -1

        For i = 0 To DesignerForm.sibbTypeList.Count - 1
            If DesignerForm.sibbTypeList(i).GetSIBBType = p_type Then
                typeIndex = i
                Exit For
            End If
        Next

        absoluteLocation.Set(0, 0)
        nodeTitle = DesignerForm.sibbTypeList(typeIndex).getfooterTitle
        type = p_type
        bitmapRef = p_bitmap
        hilighted = False
        data = ""
        debugSelected = False
        debugHistorySelected = False
        stepDebugSelected = False
        nodeIndex = -1
        internalReference = ""
        showInCallTrace = True

        ' Copy the editable output names
        With DesignerForm.sibbTypeList(typeIndex)
            For i = 0 To .GetNumberOfOutputNames - 1
                outputs.Add(New SIBBOutputClass(.GetOutputName(i)))

                If .GetOutputName(i) = "Disconnected" Then outputs(outputs.Count - 1).visible = False

                If p_type = SIBBTypeClass.SIBBTYPE.VBSCRIPT Then
                    If .GetOutputName(i).StartsWith("Return") Then outputs(outputs.Count - 1).visible = False
                End If
            Next
        End With

        drawframeobject = New DrawFrameClass(p_bitmap)
    End Sub

    Public Function Clone() As SIBBClass
        Dim x As SIBBClass = Nothing

        With DesignerForm
            x = GenerateDerivedClass(.sibbTypeList(typeIndex).GetXMLName, .myMasterBitmap)
        End With

        With x
            .nodeTitle = "Copy of " & nodeTitle
            PackData()
            .data = data
            .UnpackData()

            For i = 0 To outputs.Count - 1
                .outputs(i).visible = outputs(i).visible
                .outputs(i).name = outputs(i).name
            Next
        End With

        Return x
    End Function

    Public Sub SetBitmapRef(ByRef p_bitmap)
        bitmapRef = p_bitmap
        drawFrameObject.SetBitmapRef(p_bitmap)
    End Sub

    Public Overridable Sub PackData()
    End Sub

    Public Overridable Sub UnpackData()
    End Sub

    Public Overridable Sub SetupForm(ByRef p_tabPage As TabPage)
        p_tabPage.Controls.Clear()
    End Sub

    Public Overridable Sub TakedownForm(ByRef p_tabPage As TabPage)
        With p_tabPage
            For i = 0 To .Controls.Count - 1
                With .Controls(i)
                    If .Tag IsNot Nothing Then
                        If .Tag = "Validate" Then
                            Dim foundIt As Boolean = False
                            Dim myName As String = .Text

                            If myName.StartsWith("$") Then
                                For j = 0 To DesignerForm.GetNumberOfServiceWideVariables - 1
                                    If myName = DesignerForm.GetServiceWideVariableName(j) Then
                                        foundIt = True
                                        Exit For
                                    End If
                                Next

                                If Not foundIt Then
                                    Select Case MsgBox("Warning: Variable " & WrapInQuotes(.Text) & " referred to in the parameters for this SIBB does not exist" & vbCrLf & "Do you want to create it now ?", MsgBoxStyle.YesNo)
                                        Case MsgBoxResult.No

                                        Case MsgBoxResult.Yes
                                            With EditVarForm
                                                Dim oldText As String = .Text

                                                .Text = "Add Service Wide Variable"
                                                .varNameTextBox.Text = myName
                                                .varValueTextBox.Clear()
                                                EditVarForm.SetVarTypeComboBox("")

                                                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                                                    Dim x As New ServiceWideVariableClass

                                                    x.SetMyType(.varTypeComboBox.Text)
                                                    x.SetName(.varNameTextBox.Text)
                                                    x.SetValue(.varValueTextBox.Text)

                                                    DesignerForm.AddNewServiceWideVariable(x)
                                                End If

                                                .Text = oldText
                                            End With
                                    End Select
                                End If
                            End If
                        End If
                    End If
                End With
            Next
        End With
    End Sub

    Public Function GetNodeIndex() As Integer
        Return nodeIndex
    End Function

    Public Sub SetNodeIndex(ByVal p_nodeIndex As Integer)
        nodeIndex = p_nodeIndex
    End Sub

    Public Sub PaintYourself(Optional ByVal p_preserveRoutes As Boolean = False)
        Dim myGraphics As Graphics = Graphics.FromImage(bitmapRef)
        Dim myFont As New Font("arial", GetFontSize, FontStyle.Bold)
        Dim myX As Integer = absoluteLocation.GetX
        Dim myY As Integer = absoluteLocation.GetY
        Dim myRect As New Rectangle(absoluteLocation.GetX, absoluteLocation.GetY, GetSIBBWidth(), GetSIBBTitleHeight)
        Dim myFormat As New StringFormat
        Dim myBrush As New System.Drawing.Drawing2D.LinearGradientBrush(myRect, Colours.headingTopFade, Colours.headingBottomFade, Drawing2D.LinearGradientMode.Vertical)
        Dim outputsDisplayed As Integer = 0
        Dim titleBrush As Brush = Brushes.White

        If hilighted Then myBrush = New System.Drawing.Drawing2D.LinearGradientBrush(myRect, Color.DarkRed, Color.Red, Drawing2D.LinearGradientMode.Vertical)

        ' Gold colour - used for SIBB heading for replay and live call trace
        If debugSelected Then myBrush = New System.Drawing.Drawing2D.LinearGradientBrush(myRect, Color.FromArgb(255, 156, 152, 58), Color.FromArgb(255, 222, 216, 42), Drawing2D.LinearGradientMode.Vertical)
        'If debugHistorySelected Then myBrush = New System.Drawing.Drawing2D.LinearGradientBrush(myRect, Color.FromArgb(255, 70, 70, 8), Color.FromArgb(255, 142, 126, 0), Drawing2D.LinearGradientMode.Vertical)

        ' Peach colour - used for SIBB heading for live call trace to show previously hit SIBBs
        If debugHistorySelected Then myBrush = New System.Drawing.Drawing2D.LinearGradientBrush(myRect, Color.FromArgb(255, 241, 157, 73), Color.FromArgb(255, 238, 134, 30), Drawing2D.LinearGradientMode.Vertical)

        If stepDebugSelected Then
            myBrush = New System.Drawing.Drawing2D.LinearGradientBrush(myRect, Color.Yellow, Color.Yellow, Drawing2D.LinearGradientMode.Vertical)
            titleBrush = Brushes.Red
        End If

        ' myBrush.SetSigmaBellShape(0.5)

        myFormat.Alignment = StringAlignment.Center
        myFormat.LineAlignment = StringAlignment.Center
        myGraphics.FillRectangle(myBrush, myRect)
        myGraphics.DrawString(nodeTitle, myFont, titleBrush, myRect, myFormat)

        If InfoForm.showIndexCheckBox.Checked Then
            Dim auxFormat As New StringFormat
            Dim auxFont As New Font("arial", 9, FontStyle.Regular)
            Dim auxBrush As Brush = Brushes.Yellow
            Dim auxWidth As Integer = AUX_WIDTH * nodeIndex.ToString.Length
            'Dim auxRect As New Rectangle(absoluteLocation.GetX - auxWidth, absoluteLocation.GetY - 2, auxWidth, GetSIBBTitleHeight() - 2)
            Dim auxRect As New Rectangle(absoluteLocation.GetX + 1, absoluteLocation.GetY, auxWidth, GetSIBBTitleHeight() - 2)
            Dim backgroundBrush As Brush = Brushes.Black
            Dim widthDelta As Integer = 0

            If nodeIndex < 10 Then widthDelta = 2

            Dim backgroundRect As New Rectangle(auxRect.Location.X - 1, auxRect.Location.Y, auxRect.Width + widthDelta, (auxRect.Height \ 2) + 1)

            auxFormat.Alignment = StringAlignment.Near
            auxFormat.LineAlignment = StringAlignment.Near
            myGraphics.FillRectangle(backgroundBrush, backgroundRect)
            myGraphics.DrawString(nodeIndex, auxFont, auxBrush, auxRect, auxFormat)
        End If

        'myPen.StartCap = Drawing2D.LineCap.Square
        'myPen.EndCap = Drawing2D.LineCap.ArrowAnchor

        ' Remove any data on output lines as we will be redrawing them
        DesignerForm.lineList.RemoveForNode(nodeIndex)

        ' Do each output, if visible, and deal with the new order parameter
        For i = 0 To outputs.Count - 1
            Dim myOutputIndex As Integer = i

            For j = 0 To outputs.Count - 1
                If outputs(j).order = i Then
                    myOutputIndex = j
                    Exit For
                End If
            Next

            With outputs(myOutputIndex)
                If .visible Then
                    myRect = New Rectangle(myX, myY + GetSIBBTitleHeight() + (outputsDisplayed * GetSIBBOutputHeight()), GetSIBBWidth(), GetSIBBOutputHeight)

                    Dim myOutputBrush As New System.Drawing.Drawing2D.LinearGradientBrush(myRect, Colours.outputTopFade, Colours.outputBottomFade, Drawing2D.LinearGradientMode.Vertical)

                    If .nextNodeIndex = -1 Then myOutputBrush = New System.Drawing.Drawing2D.LinearGradientBrush(myRect, Colours.unconnectedOutputTopFade, Colours.unconnectedOutputBottomFade, Drawing2D.LinearGradientMode.Vertical)
                    'If .debugSelected Then myOutputBrush = New System.Drawing.Drawing2D.LinearGradientBrush(myRect, Color.Green, Color.LightGreen, Drawing2D.LinearGradientMode.Vertical)

                    ' Green - used for showing output used for call replay and call trace
                    If .debugSelected Then myOutputBrush = New System.Drawing.Drawing2D.LinearGradientBrush(myRect, Colours.traceOutputTopFade, Colours.traceOutputBottomFade, Drawing2D.LinearGradientMode.Vertical)

                    myGraphics.FillRectangle(myOutputBrush, myRect)

                    myGraphics.DrawString(.name, myFont, Brushes.White, myRect, myFormat)

                    ' Draw the connecting line if the output is connected and not selected
                    If .nextNodeIndex >= 0 Then
                        ' If Not .selected Then DrawConnection(Me, myGraphics, i)
                    End If

                    outputsDisplayed += 1
                End If
            End With
        Next

        ' Draw any selected outputs
        For i = 0 To outputs.Count - 1
            If outputs(i).nextNodeIndex >= 0 Then
                If USE_NEW_DRAW Then
                    If outputs(i).selected Then DrawConnectionNew(myGraphics, Me.nodeIndex, i, p_preserveRoutes)
                Else
                    If outputs(i).selected Then DrawConnection(Me, myGraphics, i)
                End If
            End If
        Next

        ' Do the footer
        Dim footerRect As New Rectangle(myX, myY + GetSIBBTitleHeight() + (outputsDisplayed * GetSIBBOutputHeight()), GetSIBBWidth(), GetSIBBFooterHeight)
        Dim footerBrush As New System.Drawing.Drawing2D.LinearGradientBrush(footerRect, Colours.footerTopFade, Colours.footerBottomFade, Drawing2D.LinearGradientMode.Vertical)

        myGraphics.FillRectangle(footerBrush, footerRect)
        myGraphics.DrawString(GetFooterTitle(), myFont, Brushes.White, footerRect, myFormat)

        ' Soften the corners
        SafeSetPixel(bitmapRef, myX, myY, Color.WhiteSmoke.ToArgb)
        SafeSetPixel(bitmapRef, myX + GetSIBBWidth() - 1, myY, Color.WhiteSmoke.ToArgb)
        SafeSetPixel(bitmapRef, myX, myY + GetHeight() - 1, Color.WhiteSmoke.ToArgb)
        SafeSetPixel(bitmapRef, myX + GetSIBBWidth() - 1, myY + GetHeight() - 1, Color.WhiteSmoke.ToArgb)

        myGraphics.Dispose()
    End Sub

    Public Function MouseInRange(ByRef p As ScreenLocationClass) As Boolean
        Dim z As ScreenLocationClass = AbsoluteToScreenPosition(absoluteLocation)

        Return WithinBounds(p, z, GetSIBBWidth(), GetSIBBTitleHeight() + (GetOutputsDisplayed() * GetSIBBOutputHeight()))
    End Function

    Public Function MouseInRangeOfTitle(ByRef p As ScreenLocationClass) As Boolean
        Dim z As ScreenLocationClass = AbsoluteToScreenPosition(absoluteLocation)

        Return WithinBounds(p, z, GetSIBBWidth(), GetSIBBTitleHeight)
    End Function

    Public Function MouseInRangeOfFooter(ByRef p As ScreenLocationClass) As Boolean
        Dim z As ScreenLocationClass = AbsoluteToScreenPosition(absoluteLocation)

        Return WithinBounds(p, z.GetX, z.GetY + GetHeight() - GetSIBBFooterHeight(), GetSIBBWidth(), GetSIBBTitleHeight)
    End Function

    Public Function MouseInRangeOfOutput(ByRef p As ScreenLocationClass) As Integer
        Dim result As Integer = -1
        Dim z As ScreenLocationClass = AbsoluteToScreenPosition(absoluteLocation)
        Dim outputsDisplayed As Integer = 0

        For i = 0 To outputs.Count - 1
            If outputs(i).visible Then
                If WithinBounds(p, z.GetX, z.GetY + GetSIBBTitleHeight() + (outputsDisplayed * GetSIBBOutputHeight()), GetSIBBWidth(), GetSIBBOutputHeight) Then
                    result = i
                    Exit For
                End If

                outputsDisplayed += 1
            End If
        Next

        Return result
    End Function

    Public Function GetSIBBType() As SIBBTypeClass.SIBBTYPE
        Return type
    End Function

    Public Function GetPos() As AbsoluteLocationClass
        Dim myZ As New AbsoluteLocationClass

        myZ.Set(absoluteLocation)

        Return myZ
    End Function

    Public Function GetXPos() As Integer
        Return absoluteLocation.GetX
    End Function

    Public Function GetYPos() As Integer
        Return absoluteLocation.GetY
    End Function

    Public Function GetLeft() As Integer
        Return GetXPos()
    End Function

    Public Function GetRight() As Integer
        Return GetXPos() + GetSIBBWidth() - 1
    End Function

    Public Sub SetPos(ByVal p_x As Integer, ByVal p_y As Integer)
        absoluteLocation.Set(p_x, p_y)
    End Sub

    Public Sub SetPos(ByRef p_z As AbsoluteLocationClass)
        absoluteLocation.Set(p_z)
    End Sub

    Public Sub Hilight(ByVal p_on As Boolean)
        hilighted = p_on
        PaintYourself()
    End Sub

    Public Function IsHilighted() As Boolean
        Return hilighted
    End Function

    Public Sub DebugSelect(ByVal p_on As Boolean, Optional ByVal p_output As Integer = -1)
        debugSelected = p_on

        If debugSelected Then
            'If p_output >= 0 Then outputs(p_output).debugSelected = True
            If p_output >= 0 Then
                For i = 0 To outputs.Count - 1
                    outputs(i).debugSelected = False
                Next

                outputs(p_output).debugSelected = True
            End If
        Else
            For i = 0 To outputs.Count - 1
                'outputs(i).debugSelected = False
            Next
        End If

        PaintYourself()
    End Sub

    Public Sub DebugHistorySelect(ByVal p_on As Boolean)
        debugHistorySelected = p_on

        'PaintYourself()
    End Sub

    Public Function IsDebugSelected() As Boolean
        Return debugSelected
    End Function

    Public Function IsDebugHistorySelected() As Boolean
        Return debugHistorySelected
    End Function

    Public Sub DebugStepSelect(ByVal p_on As Boolean)
        stepDebugSelected = p_on

        PaintYourself()
    End Sub

    Public Function IsStepDebugSelected() As Boolean
        Return stepDebugSelected
    End Function

    Public Sub Move(ByVal p_deltaX As Integer, ByVal p_deltaY As Integer)
        ' Erase the whole canvas
        DesignerForm.ClearMasterBitmap()

        absoluteLocation.Add(p_deltaX, p_deltaY)

        For i = 0 To sibbList.Count - 1
            With sibbList(i)
                .PaintYourself()
            End With
        Next
    End Sub

    Public Sub Move(ByRef p_z As DeltaLocationClass)
        Move(p_z.GetX, p_z.GetY)
    End Sub

    Public Sub SetOutput(ByVal p_index As Integer, ByVal p_nextNodeIndex As Integer)
        outputs(p_index).nextNodeIndex = p_nextNodeIndex
    End Sub

    Public Sub EraseMe()
        Dim myGraphics As Graphics = Graphics.FromImage(bitmapRef)
        Dim scrollX As Integer = DesignerForm.HScrollBar1.Value
        Dim scrollY As Integer = DesignerForm.VScrollBar1.Value
        Dim zx As Integer = absoluteLocation.GetX - scrollX
        Dim zy As Integer = absoluteLocation.GetY - scrollY

        myGraphics.FillRectangle(Brushes.WhiteSmoke, New Rectangle(zx, zy, GetSIBBWidth(), GetHeight))
        myGraphics.Dispose()
    End Sub

    Public Sub EraseFrame()
        drawFrameObject.EraseFrame()
    End Sub

    Public Sub DrawFrame(ByRef p_position As AbsoluteLocationClass)
        drawFrameObject.DrawFrame(p_position, GetSIBBWidth(), GetHeight())
    End Sub

    Private Function GetOutputsDisplayed() As Integer
        Dim result As Integer = 0

        For i = 0 To outputs.Count - 1
            If outputs(i).visible Then result += 1
        Next

        Return result
    End Function

    Public Function GetHeight() As Integer
        Return GetSIBBTitleHeight() + (GetOutputsDisplayed() * GetSIBBOutputHeight()) + GetSIBBFooterHeight()
    End Function

    Public Function GetConnectionStartPosition(ByVal p_outputIndex As Integer) As AbsoluteLocationClass
        Dim myZ As New AbsoluteLocationClass

        For i = 0 To p_outputIndex - 1
            If Not outputs(i).visible Then p_outputIndex -= 1
        Next

        myZ.Set(absoluteLocation.GetX + GetSIBBWidth(), absoluteLocation.GetY + GetSIBBTitleHeight() + (p_outputIndex * GetSIBBOutputHeight()) + (GetSIBBOutputHeight() \ 2))

        Return myZ
    End Function

    Public Function GetConnectionEndPosition() As AbsoluteLocationClass
        Dim myZ As New AbsoluteLocationClass

        myZ.Set(absoluteLocation.GetX, absoluteLocation.GetY + (GetSIBBTitleHeight() \ 2))

        Return myZ
    End Function

    Public Function GetTypeName() As String
        Return DesignerForm.sibbTypeList(typeIndex).GetTypeName
    End Function

    Public Function GetFooterTitle() As String
        Return DesignerForm.sibbTypeList(typeIndex).GetFooterTitle
    End Function

    Public Function GetData() As String
        Return data
    End Function

    Public Sub SetData(ByRef p_data)
        data = p_data
    End Sub

    Public Function CanDelete() As Boolean
        Return DesignerForm.sibbTypeList(typeIndex).getcanDelete
    End Function

    Public Function GetOutputFixedName(ByVal p_index As Integer) As String
        Return DesignerForm.sibbTypeList(typeIndex).GetOutputName(p_index)
    End Function

    Public Function GetDisconnectOutputIndex() As Integer
        Return DesignerForm.sibbTypeList(typeIndex).GetDisconnectOutputIndex
    End Function

    Public Function GetInternalReference() As String
        Return internalReference
    End Function

    Public Sub SetInternalReference(ByRef p As String)
        internalReference = p
    End Sub

    Public Sub PackDataDirect(ByRef p As String)
        data = p
    End Sub

    Public Sub PackData(ByVal ParamArray p_items() As String)
        data = ""

        For Each myItem As String In p_items
            PackedDataAdd(myItem)
        Next
    End Sub

    Public Sub PackedDataAdd(ByRef p As String)
        If data <> "" Then data &= ","

        data &= p
    End Sub

    Public Sub PackedDataAddVbCrLf(ByRef p As String)
        If data <> "" Then data &= vbCrLf

        data &= p
    End Sub

    Public Function UnpackDataDirect() As String
        Return data
    End Function

    Public Sub UnpackData(ByRef p_list As List(Of String))
        Dim myArray() As String = data.Split(",")

        For Each myItem As String In myArray
            p_list.Add(myItem)
        Next
    End Sub

    Public Sub UnpackDataVbCrLf(ByRef p_list As List(Of String))
        Dim myArray As String() = System.Text.RegularExpressions.Regex.Split(data, vbCrLf)

        For Each myItem As String In myArray
            p_list.Add(myItem)
        Next
    End Sub

    Public Sub UnpackData(ByRef p0 As String)
        Dim myList As New List(Of String)

        UnpackData(myList)
        If myList.Count > 0 Then p0 = myList(0)
    End Sub

    Public Sub UnpackData(ByRef p0 As String, ByRef p1 As String)
        Dim myList As New List(Of String)

        UnpackData(myList)
        If myList.Count > 0 Then p0 = myList(0)
        If myList.Count > 1 Then p1 = myList(1)
    End Sub

    Public Sub UnpackData(ByRef p0 As String, ByRef p1 As String, ByRef p2 As String, ByRef p3 As String)
        Dim myList As New List(Of String)

        UnpackData(myList)
        If myList.Count > 0 Then p0 = myList(0)
        If myList.Count > 1 Then p1 = myList(1)
        If myList.Count > 2 Then p2 = myList(2)
        If myList.Count > 3 Then p3 = myList(3)
    End Sub

    Public Sub UnpackData(ByRef p0 As String, ByRef p1 As String, ByRef p2 As String, ByRef p3 As String, ByRef p4 As String)
        Dim myList As New List(Of String)

        UnpackData(myList)
        If myList.Count > 0 Then p0 = myList(0)
        If myList.Count > 1 Then p1 = myList(1)
        If myList.Count > 2 Then p2 = myList(2)
        If myList.Count > 3 Then p3 = myList(3)
        If myList.Count > 4 Then p4 = myList(4)
    End Sub

    Public Function GetDataAsString() As String
        Return data
    End Function

    Public Sub SetDataFromString(ByRef p As String)
        data = p
    End Sub

    Public Function GetNodeTitle() As String
        Return nodeTitle
    End Function

    Public Sub SetNodeTitle(ByRef p As String)
        nodeTitle = p
    End Sub

    Public Function GetOutputCount() As Integer
        Return outputs.Count
    End Function

    Public Function GetOutputNextNode(ByVal p_index As Integer) As Integer
        Return outputs(p_index).nextNodeIndex
    End Function

    Public Sub SetOutputNextNode(ByVal p_index As Integer, ByVal p_nextNodeIndex As Integer)
        outputs(p_index).nextNodeIndex = p_nextNodeIndex
    End Sub

    Public Sub SetOutputNextNodeToNull(ByVal p_index As Integer)
        outputs(p_index).nextNodeIndex = -1
    End Sub

    Public Sub DecOutputNextNode(ByVal p_index As Integer)
        outputs(p_index).nextNodeIndex -= 1
    End Sub

    Public Sub SetOutputSelected(ByVal p_index As Integer, ByVal p_value As Boolean)
        outputs(p_index).selected = p_value
    End Sub

    Public Function OutputIsSelected(ByVal p_index As Integer) As Boolean
        Return outputs(p_index).selected
    End Function

    Public Sub RemoveOutput(ByVal p_index As Integer)
        outputs(p_index).Remove()
    End Sub

    Public Function GetOutputName(ByVal p_index As Integer) As String
        Return outputs(p_index).name
    End Function

    Public Function GetOutputOrder(ByVal p_index As Integer) As Integer
        Return outputs(p_index).order
    End Function

    Public Sub SetOutputName(ByVal p_index As Integer, ByRef p_name As String)
        outputs(p_index).name = p_name
    End Sub

    Public Sub SetOutputOrder(ByVal p_index As Integer, ByVal p_order As Integer)
        outputs(p_index).order = p_order
    End Sub

    Public Function OutputIsVisible(ByVal p_index As Integer) As Boolean
        Return outputs(p_index).visible
    End Function

    Public Sub SetOutputVisible(ByVal p_index As Integer, ByVal p_value As Boolean)
        outputs(p_index).visible = p_value
    End Sub

    Public Sub SetOutputDebugSelected(ByVal p_index As Integer, ByVal p_value As Boolean)
        outputs(p_index).debugSelected = p_value
    End Sub

    Public Sub AddOutput(ByRef p_name As String)
        outputs.Add(New SIBBOutputClass(p_name))
    End Sub

    Public Overridable Function Run(ByRef p_data As String) As Integer
        Return -1
    End Function

    Public Function GetShowInCallTrace() As Boolean
        Return showInCallTrace
    End Function

    Public Sub SetShowInCallTrace(ByVal p As Boolean)
        showInCallTrace = p
    End Sub

    Public Function GetTop() As Integer
        Return absoluteLocation.GetY
    End Function

    Public Function GetBottom() As Integer
        Return absoluteLocation.GetY + GetHeight()
    End Function

    Public Function CollideWithLeftSide(ByVal p_xStart As Integer, ByVal p_xEnd As Integer, ByVal p_y As Integer) As Integer
        Dim result As Integer = -1
        Dim myXpos As Integer = GetXPos()
        Dim myYPos As Integer = GetYPos()
        Dim myBottom As Integer = GetBottom()

        If myXpos < (p_xEnd + DODGE_GAP) Then
            If myXpos >= p_xStart Then
                If p_y > (myYPos - DODGE_GAP) Then
                    If p_y < (myBottom + DODGE_GAP) Then result = myXpos - DODGE_GAP
                End If
            End If
        End If

        Return result
    End Function

    Public Function CollideWithRightSide(ByVal p_xStart As Integer, ByVal p_xEnd As Integer, ByVal p_y As Integer) As Integer
        Dim result As Integer = -1
        Dim myXpos As Integer = GetXPos() + GetSIBBWidth()
        Dim myYPos As Integer = GetYPos()
        Dim myBottom As Integer = GetBottom()

        If myXpos > (p_xEnd - DODGE_GAP) Then
            If myXpos <= p_xStart Then
                If p_y > (myYPos - DODGE_GAP) Then
                    If p_y < (myBottom + DODGE_GAP) Then result = myXpos + DODGE_GAP
                End If
            End If
        End If

        Return result
    End Function

    Public Function CollideWithTopSide(ByVal p_x As Integer, ByVal p_yStart As Integer, ByVal p_yEnd As Integer) As Integer
        Dim result As Integer = -1
        Dim myXpos As Integer = GetXPos()
        Dim myRight As Integer = myXpos + GetSIBBWidth()
        Dim myYPos As Integer = GetYPos()

        If myYPos < (p_yEnd + DODGE_GAP) Then
            If myYPos >= p_yStart Then
                If p_x > (myXpos - DODGE_GAP) Then
                    If p_x < (myRight + DODGE_GAP) Then result = myYPos - DODGE_GAP
                End If
            End If
        End If

        Return result
    End Function

    Public Function CollideWithBottomSide(ByVal p_x As Integer, ByVal p_yStart As Integer, ByVal p_yEnd As Integer) As Integer
        Dim result As Integer = -1
        Dim myXpos As Integer = GetXPos()
        Dim myRight As Integer = myXpos + GetSIBBWidth()
        Dim myYPos As Integer = GetBottom()

        If myYPos > (p_yEnd - DODGE_GAP) Then
            If myYPos <= p_yStart Then
                If p_x > (myXpos - DODGE_GAP) Then
                    If p_x < (myRight + DODGE_GAP) Then result = myYPos + DODGE_GAP
                End If
            End If
        End If

        Return result
    End Function
End Class

Public Class VoicemailForm
    Private Sub VoicemailForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myRef As VoicemailSIBBClass = sibbList(CInt(Tag))

        nodePropertiesDataGridView.Rows.Clear()

        With myRef
            RadioButton1.Checked = .useStdVoicemail
            RadioButton2.Checked = Not .useStdVoicemail
            playWelcomeAnnouncementCheckBox.Checked = .playWelcomeAnnouncement
            welcomeAnnouncementTextBox.Text = .welcomeAnnouncement
            dtmfCheckBox.Checked = .useDTMFasCallerId
            dtmfTextBox.Text = .callerIdAnnouncement
            playAnnouncementCheckBox.Checked = .useAnnouncement
            announcementTextBox.Text = .announcement
            maxMessageLengthTextBox.Text = .maxDuration
            emailTextBox.Text = .emailAddress
            saveFilenameCheckBox.Checked = .saveFilenameInVariable
            titleTextBox.Text = .GetNodeTitle
            internalReferenceTextBox.Text = .GetInternalReference
            hideInCallTraceCheckBox.Checked = Not .GetShowInCallTrace
            nodeTypeLabel.Text = DesignerForm.sibbTypeList(DesignerForm.GetIndexForType(.GetSIBBType)).GetTypeName
            nodeNumberLabel.Text = Tag

            For i = 0 To .GetOutputCount - 1
                Dim myOrder As Integer = .GetOutputOrder(i)
                Dim myNextNodeIndex As Integer = .GetOutputNextNode(i)
                Dim myNextNodeData As String = ""

                If myOrder = -1 Then myOrder = i

                If myNextNodeIndex >= 0 Then myNextNodeData = sibbList(.GetOutputNextNode(i)).GetNodeTitle & " (Node " & .GetOutputNextNode(i) & ")"

                Dim myRow As String() = {.OutputIsVisible(i), .GetOutputFixedName(i), .GetOutputName(i), myNextNodeData, myOrder}

                nodePropertiesDataGridView.Rows.Add(myRow)
            Next

            nodePropertiesDataGridView.Size = New Point(nodePropertiesDataGridView.Size.Width, 20 + nodePropertiesDataGridView.Rows.Count * 20)
            GroupBox1.Enabled = Not .useStdVoicemail
        End With
    End Sub

    Private Sub okButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles okButton.Click
        Dim myRef As VoicemailSIBBClass = sibbList(CInt(Tag))

        With myRef
            .useStdVoicemail = RadioButton1.Checked
            .playWelcomeAnnouncement = playWelcomeAnnouncementCheckBox.Checked
            .welcomeAnnouncement = welcomeAnnouncementTextBox.Text
            .useDTMFasCallerId = dtmfCheckBox.Checked
            .callerIdAnnouncement = dtmfTextBox.Text
            .useAnnouncement = playAnnouncementCheckBox.Checked
            .announcement = announcementTextBox.Text
            .maxDuration = maxMessageLengthTextBox.Text
            .emailAddress = emailTextBox.Text
            .saveFilenameInVariable = saveFilenameCheckBox.Checked
            .SetNodeTitle(titleTextBox.Text)
        End With

        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub specialVoicemailRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        GroupBox1.Enabled = RadioButton2.Checked
    End Sub

    Private Sub stdVoicemailRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
End Class
USE [OpenQueue]
GO

/****** Object:  StoredProcedure [dbo].[SP_CHANGE_QUEUE_STATE]    Script Date: 06/03/2016 23:59:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CHANGE_QUEUE_STATE]
	-- Add the parameters for the stored procedure here
	@p_callId int,
	@p_state varchar(256)
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if @p_state <> ''
	begin
		if LEFT(@p_state, 1) = 'q' set @p_state = 'Queueing'
		if LEFT(@p_state, 1) = 'n' set @p_state = 'NotInQueue'
		if LEFT(@p_state, 1) = 'r' set @p_state = 'Ringing'
		if LEFT(@p_state, 1) = 'a' set @p_state = 'Answered'
		if LEFT(@p_state, 1) = 'd' set @p_state = 'Done'
	end
	
	if @p_state <> '' update CallQueueTable set State = @p_state, lastHeartBeat = GETDATE() where CallId = @p_callId
	else update CallQueueTable set lastHeartBeat = GETDATE() where CallId = @p_callId
END

GO


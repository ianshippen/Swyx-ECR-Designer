USE [OpenQueue]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_QUEUE_LENGTH]    Script Date: 06/15/2016 16:41:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GET_QUEUE_LENGTH] 
	-- Add the parameters for the stored procedure here
	@p_queueId int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select COUNT(*) as length from CallQueueTable as a 
	left join IpPbxCdr as b on a.CallId = b.CallId
	left join IpPbxCdr as c on b.TransferredToCallId = c.callid
	where QueueId = @p_queueId
	and a.State in ('Ringing', 'Queueing') 
	and ((b.StartTime is null) or (b.TransferredToCallId > 0 and c.startTime is null))
	and DATEDIFF(s, lastHeartBeat, getdate()) < 60 
END

GO


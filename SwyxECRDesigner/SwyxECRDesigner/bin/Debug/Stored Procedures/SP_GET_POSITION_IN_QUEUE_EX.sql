USE [OpenQueue]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_POSITION_IN_QUEUE_EX]    Script Date: 06/05/2016 00:30:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GET_POSITION_IN_QUEUE_EX] 
	-- Add the parameters for the stored procedure here
	@p_callId int,
	@p_timeout int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	-- Returns 0 if at from of queue, then 1, 2, 3 etc
	-- Returns -1 if call not in queue, or has rang 6 times or more
	SET NOCOUNT ON;
	declare @myTime datetime
	declare @myFormattedDate varchar(256)
	declare @myQueueId int
	
	set @myTime = (select timestamp from CallQueueTable where CallId = @p_callId)
	set @myFormattedDate = CONVERT(varchar, GETDATE(), 103)
	
	if @p_timeout > 0
	  set @myQueueId = (select QueueId from CallQueueTable where CallId = @p_callId and State in ('Ringing', 'Queueing') and datediff(s, timestamp, getdate()) < @p_timeout)
	else
	  set @myQueueId = (select QueueId from CallQueueTable where CallId = @p_callId and State in ('Ringing', 'Queueing'))
	
    -- Look for entry with this Call Id, i.e. are we even valid ?
    if @myQueueId is not null
    begin
		-- Count number of entries that arrived before us that are either Ringing or Queueing, with no CDR written (i.e. call is still alive), for today, with last heartbeat less than 30 seconds ago, and less than 6 rings
		select COUNT(*) as position from CallQueueTable as a 
		left join IpPbxCdr as b on a.CallId = b.CallId
		left join IpPbxCdr as c on b.TransferredToCallId = c.callid
		where QueueId = @myQueueId
		and timestamp < @myTime 
		and a.State in ('Ringing', 'Queueing') 
		and ((b.StartTime is null) or (b.TransferredToCallId > 0 and c.startTime is null))
		and CONVERT(varchar, timestamp, 103) = @myFormattedDate
		and DATEDIFF(s, lastHeartBeat, getdate()) < 60 
	end
	else select -1 as position
END

GO


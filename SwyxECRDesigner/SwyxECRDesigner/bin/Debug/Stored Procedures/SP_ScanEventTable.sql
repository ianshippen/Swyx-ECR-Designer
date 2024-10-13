USE [OpenQueue]
GO

/****** Object:  StoredProcedure [dbo].[SP_ScanEventTable]    Script Date: 06/27/2016 21:07:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ScanEventTable] 
	-- Add the parameters for the stored procedure here
	@p_callId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
create table #t (myKey int identity(1, 1) primary key, 
                 timestamp datetime, 
                 scriptName varchar(256), 
                 callId int, 
                 node int, 
                 data varchar(4096), 
                 output int, 
                 nextNode int)

declare @myCount int, @myNode int, @rowsProcessed int, @myTimestamp datetime

if @p_callId < 1 set @p_callId = (select top(1) callId from ServiceBuilderEventTable order by timestamp desc)

set @myCount = (select COUNT(*) from ServiceBuilderEventTable where callId = @p_callId)
set @myNode = 0
set @rowsProcessed = 0
set @myTimestamp = (select top (1) timestamp from ServiceBuilderEventTable where callid = @p_callId order by timestamp)

while (@rowsProcessed < @myCount)
begin
	if @rowsProcessed % 2 = 0
	begin
		insert into #t select top(1) * from ServiceBuilderEventTable where node = @myNode and nextNode is NULL and callId = @p_callId 
		and timestamp >= @myTimestamp
		order by timestamp
	end
	else
	begin
		insert into #t select top (1) * from ServiceBuilderEventTable where node = @myNode and nextNode is not NULL and callId = @p_callId
		and timestamp >= @myTimestamp
		order by timestamp

		set @myNode = (select top (1) nextNode from #t order by myKey desc)
		set @myTimestamp = (select top (1) timestamp from #t order by myKey desc)
	end
	
	set @rowsProcessed = @rowsProcessed + 1
end

select callId, a.scriptname, timestamp, node, a.data, output, nextnode, nodetype, b.data, title from #t as a left join servicebuildertable as b on a.scriptName = b.scriptName and a.node = b.nodeNumber order by myKey

drop table #t
END






GO


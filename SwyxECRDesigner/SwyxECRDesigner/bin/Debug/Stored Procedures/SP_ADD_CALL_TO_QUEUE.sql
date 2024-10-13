/****** Object:  StoredProcedure [dbo].[SP_ADD_CALL_TO_QUEUE]    Script Date: 06/03/2016 17:34:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ADD_CALL_TO_QUEUE] 
	-- Add the parameters for the stored procedure here
	@p_callId int,
	@p_queueId int,
	@p_tag varchar(128)
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

-- Convert all integer parameters to string equivalents
declare @mySql varchar(max)
declare @callIdString varchar(256)
declare @queueIdString varchar(256)
declare @tagString varchar(256)
declare @timeStampString varchar(256)

set @callIdString = RTRIM(@p_callId)
set @queueIdString = RTRIM(@p_queueId)
set @tagString = '''' + @p_tag + ''''
set @timeStampString = '''' + CONVERT(varchar, GETDATE(), 20) + ''''

set @mySql = 'if ((select count(*) from CallQueueTable as a where a.callId = ' + @callIdString + ') = 0)
begin
	insert into CallQueueTable values(' + @callIdString + ', ' + @timeStampString + ', ' + @queueIdString + ', ' + @tagString + ', ''Queueing'', 0, ' + @timeStampString + ')
end'

exec(@mySql)

END

GO


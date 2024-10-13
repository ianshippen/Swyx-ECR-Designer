USE [OpenQueue]
GO

/****** Object:  StoredProcedure [dbo].[SP_UPDATE_SB_LOG_POINT_TABLE]    Script Date: 07/05/2016 20:44:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UPDATE_SB_LOG_POINT_TABLE] 
	-- Add the parameters for the stored procedure here
	@p_callId INT,
	@p_timeStamp DATETIME,
	@p_logPoint INT,
	@p_firstTimeOnly VARCHAR(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare @mySql varchar(max)
    declare @callIdString varchar(256)
    declare @logPointString varchar(256)
    declare @timeStampString varchar(256)

    set @callIdString = RTRIM(@p_callId)
    set @logPointString = 'logPoint_' + RTRIM(@p_logPoint)
    set @timeStampString = '''' + convert(varchar, @p_timeStamp, 121) + ''''

    set @mySql = 'if exists (select * from ServiceBuilderLogPointTable where callId = ' + @callIdString + ')
    begin '
    if @p_firstTimeOnly = 'True' set @mySql = @mySql + 'if (select ' + @logPointString + ' from ServiceBuilderLogPointTable where callId = ' + @callIdString + ') IS NULL'
    
    set @mySql = @mySql + ' update ServiceBuilderLogPointTable set ' + @logPointString + ' = ' + @timeStampString + ' where callId = ' + @callIdString + '
      end
    else insert into ServiceBuilderLogPointTable (callId, ' + @logPointString + ') values (' + @callIdString + ', ' + @timeStampString +')'

  exec(@mySql)
END

GO


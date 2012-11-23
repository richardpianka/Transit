SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE FUNCTION [dbo].[Length]
(
	@aLatitude  DECIMAL(9, 6),
	@aLongitude DECIMAL(9, 6),
	@bLatitude  DECIMAL(9, 6),
	@bLongitude DECIMAL(9, 6)
)
RETURNS TABLE
AS
	RETURN SELECT ABS(@bLongitude - @aLongitude) + ABS(@bLatitude - @aLatitude) AS val
GO

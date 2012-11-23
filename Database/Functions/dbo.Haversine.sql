SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE FUNCTION [dbo].[Haversine]
(
	@aLatitude  DECIMAL(9, 6),
	@aLongitude DECIMAL(9, 6),
	@bLatitude  DECIMAL(9, 6),
	@bLongitude DECIMAL(9, 6)
)
RETURNS TABLE
AS
	--RETURN  SELECT (2 * 3956.6 * 5280 * ATN2(SQRT(SQUARE(SIN(RADIANS(@bLatitude - @aLatitude) / 2)) + SQUARE(SIN(RADIANS(@bLongitude - @aLongitude) / 2)) * COS(RADIANS(@aLatitude))),SQRT(1-SQUARE(SIN(RADIANS(@bLatitude - @aLatitude) / 2)) + SQUARE(SIN(RADIANS(@bLongitude - @aLongitude) / 2)) * COS(RADIANS(@aLatitude))))) AS val

	RETURN  WITH cte AS (
				SELECT SQUARE(SIN(RADIANS(@bLatitude - @aLatitude) / 2)) +
					   SQUARE(SIN(RADIANS(@bLongitude - @aLongitude) / 2)) * COS(RADIANS(@aLatitude))
				AS x
			)
			SELECT (2 * 3956.6 * 5280 * ATN2(SQRT(cte.x), SQRT(1-cte.x))) AS val
			FROM cte

GO

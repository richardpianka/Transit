SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[GetGpsCapture]
	@DeviceName AS VARCHAR(20),
	@StartDate AS DATETIME,
	@EndDate AS DATETIME
AS
BEGIN
	SET NOCOUNT ON;

    SELECT  [Date] ,
            StopId ,
            ( SELECT    val
              FROM      dbo.Haversine(Latitude, Longitude, StopLat, StopLon)
            ) AS Distance ,
            Latitude ,
            Longitude
    INTO    #Distances
    FROM    dbo.Gps ,
            dbo.Stops
    WITH    (NOLOCK)
    WHERE   (SELECT val FROM dbo.Length(Latitude, Longitude, StopLat, StopLon)) < .003
            AND Date BETWEEN @StartDate AND @EndDate
            AND DeviceName = @DeviceName

    SELECT  d.[Date] ,
			[StopId] ,
			[Distance] ,
			Latitude ,
			Longitude
    FROM    #Distances d
    WITH    (NOLOCK)
            JOIN ( SELECT   [Date] ,
                            MIN(Distance) minDist
                   FROM     #Distances
                   WITH     (NOLOCK)
                   GROUP BY [Date]
                 ) b ON ( d.[Date] = b.[date] )
                        AND ( d.Distance = b.minDist )
    ORDER BY d.Date

    DROP TABLE #Distances
END
GO

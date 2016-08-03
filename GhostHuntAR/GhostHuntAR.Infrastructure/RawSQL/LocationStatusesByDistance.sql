WITH temp AS (SELECT TOP({3}) *, ( 6371 * acos( cos( radians({0}) ) * cos( radians( Latitude ) ) * cos( radians( Longitude ) - radians({1}) ) + sin( radians({0}) ) * sin( radians( Latitude ) ) ) ) as Distance
FROM GHLocations)

SELECT GHLocationID,
CONVERT(NVARCHAR(30), DateLastModified, 127) AS DateLastModified
FROM temp
WHERE Distance < {2}
ORDER BY Distance;


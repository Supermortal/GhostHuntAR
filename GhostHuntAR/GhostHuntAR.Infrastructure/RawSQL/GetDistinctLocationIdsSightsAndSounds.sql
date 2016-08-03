WITH temp AS (SELECT DISTINCT GHLocationID 
FROM Sounds
WHERE UserName = {0}

UNION

SELECT DISTINCT GHLocationID 
FROM Images
WHERE UserName = {0}

UNION

SELECT DISTINCT GHLocationID 
FROM Texts
WHERE UserName = {0})

SELECT ghl.*, 
(SELECT COUNT(*) FROM Sounds WHERE GHLocationID = ghl.GHLocationID) AS 'SoundsCount',
(SELECT COUNT(*) FROM Images WHERE GHLocationID = ghl.GHLocationID) AS 'ImagesCount',
(SELECT COUNT(*) FROM Texts WHERE GHLocationID = ghl.GHLocationID) AS 'TextsCount'
FROM GHLocations ghl
WHERE GHLocationID IN
(SELECT DISTINCT GHLocationID FROM temp)
OR GHLocationID IN 
(SELECT GHLocationID 
FROM GHLocations
WHERE CreatedByUserID = (SELECT UserId FROM UserProfile WHERE UserName = {0}));
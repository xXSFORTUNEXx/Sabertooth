/*
Created 2019-01-25 17:10:23.360
Creates vMapBlobInfo view in Sabertooths Database
Created by Steven M Fortune
Updated 2019-02-27 10:53:50.457
*/
IF EXISTS(SELECT * FROM SYS.views WHERE name = 'vMapBlobInfo')
BEGIN
	DROP VIEW vMapBlobInfo
END
GO
CREATE VIEW vMapBlobInfo
AS
SELECT MAPS.ID,
	MAPS.NAME,
	MAPS.NPC,
	MAPS.ITEM,
	MAPS.GROUND,
	MAPS.MASK,
	MAPS.MASKA,
	MAPS.FRINGE,
	MAPS.FRINGEA
FROM MAPS

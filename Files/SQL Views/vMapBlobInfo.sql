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
SELECT Maps.ID,
	Maps.Name,
	Maps.Npc,
	Maps.Item,
	Maps.Ground,
	Maps.Mask,
	Maps.Mask_A,
	Maps.Fringe,
	Maps.Fringe_A
FROM Maps

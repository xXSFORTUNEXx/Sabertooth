/*
Created 2019-01-25 17:07:52.593
Creates vMapInfo view in Sabertooths Database
Updated 2019-02-27 10:53:50.457
*/
IF EXISTS(SELECT * FROM SYS.views WHERE name = 'vMapInfo')
BEGIN
	DROP VIEW vMapInfo
END
GO
CREATE VIEW vMapInfo
AS
SELECT Maps.ID,
	Maps.Name,
	Maps.Revision,
	Maps.UP,
	Maps.DOWN,
	Maps.Left_Side,
	Maps.Right_Side
FROM Maps

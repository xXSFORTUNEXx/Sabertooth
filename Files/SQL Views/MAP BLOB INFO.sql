/*
Created 2019-01-25 17:10:23.360
Creates MAP BLOB INFO view in Sabertooths Database
Created by Steven M Fortune
*/
CREATE VIEW [MAP BLOB INFO]
AS
SELECT ID,
	NAME,
	NPC,
	ITEM,
	GROUND,
	MASK,
	MASKA,
	FRINGE,
	FRINGEA
FROM MAPS

/*
Created 9/7/2018 3:52PM
Inserts chat data into database
Created by Steven M Fortune
*/

SELECT ID,
	NAME,
	X,
	Y,
	DIRECTION,
	SPRITE,
	STEP,
	OWNER,
	BEHAVIOR,
	SPAWNTIME,
	HEALTH,
	MAXHEALTH,
	DAMAGE,
	DESX,
	DESY,
	EXP,
	MONEY,
	RANGE,
	SHOPNUM,
	CHATNUM
FROM NPCS
WHERE ID = @id

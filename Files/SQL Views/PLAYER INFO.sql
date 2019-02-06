/*
Created 9/2/2018 5:18AM
Creates PLAYER INFO view in Sabertooths Database
Created by Steven M Fortune
Updated 2019-01-21 14:33:00.010
*/
CREATE VIEW [PLAYER INFO]
AS
SELECT PLAYERS.ID,
	PLAYERS.NAME,
	PLAYERS.EMAILADDRESS,
	PLAYERS.LEVEL,
	PLAYERS.HEALTH,
	PLAYERS.MAXHEALTH,
	PLAYERS.EXPERIENCE,
	PLAYERS.MONEY,
	PLAYERS.ARMOR,
	PLAYERS.HUNGER,
	PLAYERS.HYDRATION,
	PLAYERS.STRENGTH,
	PLAYERS.AGILITY,
	PLAYERS.ENDURANCE,
	PLAYERS.STAMINA
FROM PLAYERS

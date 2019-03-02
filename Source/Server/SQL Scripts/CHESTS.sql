/*
Created 9/2/2018 5:18AM
Creates CHESTS table in Sabertooths Database
Created by Steven M Fortune
*/

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Chests')
CREATE TABLE Chests (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	Name TEXT,
	Money INTEGER,
	Experience INTEGER,
	Required_Level INTEGER,
	Trap_Level INTEGER,
	Req_Key INTEGER,
	Damage INTEGER,
	Npc_Spawn INTEGER,
	Spawn_Amount INTEGER,
	Chest_Item VARBINARY(MAX)
	)

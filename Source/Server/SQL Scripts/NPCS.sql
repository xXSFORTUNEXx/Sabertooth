/*
Created 9/2/2018 5:08AM
Creates NPCS table in Sabertooths Database
Created by Steven M Fortune
*/

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Npcs')
CREATE TABLE Npcs (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	Name TEXT,
	X INTEGER,
	Y INTEGER,
	Direction INTEGER,
	Sprite INTEGER,
	Step INTEGER,
	Owner INTEGER,
	Behavior INTEGER,
	Spawn_Time INTEGER,
	Health INTEGER,
	Max_Health INTEGER,
	Damage INTEGER,
	Des_X INTEGER,
	Des_Y INTEGER,
	Exp INTEGER,
	Money INTEGER,
	Range INTEGER,
	Shop_Num INTEGER,
	Chat_Num INTEGER,
	Speed INTEGER
	)
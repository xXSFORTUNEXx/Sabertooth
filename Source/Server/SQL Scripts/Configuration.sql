/*
Created 9/2/2018 5:18AM
Creates configuration table in Sabertooths Database
Created by Steven M Fortune
*/

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Configuration')
CREATE TABLE Configuration (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	Regen_Time INTEGER,
	Save_Time INTEGER,
	Spawn_Time INTEGER,
	AiTime INTEGER
	)

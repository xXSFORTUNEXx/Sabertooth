/*
Created 9/2/2018 5:12AM
Creates hotbar table in Sabertooths Database
Created by Steven M Fortune
*/

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Hotbar')
	CREATE TABLE Hotbar (
		ID INT IDENTITY(1, 1) PRIMARY KEY,
		PlayerID INTEGER,
		HotBarNumber INTEGER,
		HotKey VARBINARY(MAX),
		SpellNumber INTEGER,
		InvNumber INTEGER
		)
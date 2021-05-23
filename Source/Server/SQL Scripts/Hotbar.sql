/*
Created 9/2/2018 5:12AM
Creates hotbar table in Sabertooths Database
Created by Steven M Fortune
*/

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Hotbar')
	CREATE TABLE Hotbar (
		ID INT IDENTITY(1, 1) PRIMARY KEY,
		PlayerID INTEGER,
		
		HotKey_1 VARBINARY(MAX),
		SpellNumber_1 INTEGER,
		InvNumber_1 INTEGER,

		HotKey_2 VARBINARY(MAX),
		SpellNumber_2 INTEGER,
		InvNumber_2 INTEGER,

		HotKey_3 VARBINARY(MAX),
		SpellNumber_3 INTEGER,
		InvNumber_3 INTEGER,

		HotKey_4 VARBINARY(MAX),
		SpellNumber_4 INTEGER,
		InvNumber_4 INTEGER,

		HotKey_5 VARBINARY(MAX),
		SpellNumber_5 INTEGER,
		InvNumber_5 INTEGER,

		HotKey_6 VARBINARY(MAX),
		SpellNumber_6 INTEGER,
		InvNumber_6 INTEGER,

		HotKey_7 VARBINARY(MAX),
		SpellNumber_7 INTEGER,
		InvNumber_7 INTEGER,

		HotKey_8 VARBINARY(MAX),
		SpellNumber_8 INTEGER,
		InvNumber_8 INTEGER,

		HotKey_9 VARBINARY(MAX),
		SpellNumber_9 INTEGER,
		InvNumber_9 INTEGER,

		HotKey_10 VARBINARY(MAX),
		SpellNumber_10 INTEGER,
		InvNumber_10 INTEGER,
		)
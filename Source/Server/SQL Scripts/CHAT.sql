/*
Created 9/2/2018 5:18AM
Creates CHAT table in Sabertooths Database
Created by Steven M Fortune
Updated 2019-02-20 15:31:30.060
*/

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'CHAT')
CREATE TABLE CHAT (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	NAME TEXT,
	MAINMESSAGE TEXT,
	OPTIONA TEXT,
	OPTIONB TEXT,
	OPTIONC TEXT,
	OPTIOND TEXT,
	NEXTCHATA INTEGER,
	NEXTCHATB INTEGER,
	NEXTCHATC INTEGER,
	NEXTCHATD INTEGER,
	SHOPNUM INTEGER,
	QUESTNUM INTEGER,
	ITEMA INTEGER,
	ITEMB INTEGER,
	ITEMC INTEGER,
	VALA INTEGER,
	VALB INTEGER,
	VALC INTEGER,
	MONEY INTEGER,
	TYPE INTEGER
	)

/*
Created 9/2/2018 5:18AM
Creates CHAT table in Sabertooths Database
Created by Steven M Fortune
Updated 2019-02-20 15:31:30.060
*/

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Chat')
CREATE TABLE Chat (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	Name TEXT,
	Main_Message TEXT,
	Option_A TEXT,
	Option_B TEXT,
	Option_C TEXT,
	Option_D TEXT,
	Next_Chat_A INTEGER,
	Next_Chat_B INTEGER,
	Next_Chat_C INTEGER,
	Next_Chat_D INTEGER,
	Shop_Num INTEGER,
	Quest_Num INTEGER,
	Item_A INTEGER,
	Item_B INTEGER,
	Item_C INTEGER,
	Value_A INTEGER,
	Value_B INTEGER,
	Value_C INTEGER,
	Money INTEGER,
	Type INTEGER
	)

/*
Created 2019-02-21 16:56:16.077
Creates QUESTLIST table in Sabertooths Database
Created by Steven M Fortune
*/

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Quest_List')
CREATE TABLE Quest_List (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	Name VARCHAR(25),
	Quest_1 INTEGER,
	Quest_1_Status INTEGER,
	Quest_2 INTEGER,
	Quest_2_Status INTEGER,
	Quest_3 INTEGER,
	Quest_3_Status INTEGER,
	Quest_4 INTEGER,
	Quest_4_Status INTEGER,
	Quest_5 INTEGER,
	Quest_5_Status INTEGER,
	Quest_6 INTEGER,
	Quest_6_Status INTEGER,
	Quest_7 INTEGER,
	Quest_7_Status INTEGER,
	Quest_8 INTEGER,
	Quest_8_Status INTEGER,
	Quest_9 INTEGER,
	Quest_9_Status INTEGER,
	Quest_10 INTEGER,
	Quest_10_Status INTEGER	
	)
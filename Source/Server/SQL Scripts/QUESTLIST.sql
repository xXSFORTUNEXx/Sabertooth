/*
Created 2019-02-21 16:56:16.077
Creates QUESTLIST table in Sabertooths Database
Created by Steven M Fortune
*/

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'QUESTLIST')
CREATE TABLE QUESTLIST (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	NAME VARCHAR(25),
	QUEST1 INTEGER,
	QUEST1STATUS INTEGER,
	QUEST2 INTEGER,
	QUEST2STATUS INTEGER,
	QUEST3 INTEGER,
	QUEST3STATUS INTEGER,
	QUEST4 INTEGER,
	QUEST4STATUS INTEGER,
	QUEST5 INTEGER,
	QUEST5STATUS INTEGER,
	QUEST6 INTEGER,
	QUEST6STATUS INTEGER,
	QUEST7 INTEGER,
	QUEST7STATUS INTEGER,
	QUEST8 INTEGER,
	QUEST8STATUS INTEGER,
	QUEST9 INTEGER,
	QUEST9STATUS INTEGER,
	QUEST10 INTEGER,
	QUEST10STATUS INTEGER
	)
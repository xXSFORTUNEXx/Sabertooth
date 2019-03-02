/*
Created 9/8/2018 7:55AM
Creates STATS table in Sabertooths Database
Created by Steven M Fortune
*/

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Stats')
	CREATE TABLE Stats (
		ID INT IDENTITY(1, 1) PRIMARY KEY,
		Name VARCHAR(25),
		Kills INTEGER,
		Points INTEGER,
		Days INTEGER,
		Hours INTEGER,
		Minutes INTEGER,
		Seconds INTEGER,
		Longest_Days INTEGER,
		Longest_Hours INTEGER,
		Longest_Minutes INTEGER,
		Longest_Seconds INTEGER,
		Longest_Life_Days INTEGER,
		Longest_Life_Hours INTEGER,
		Longest_Life_Minutes INTEGER,
		Longest_Life_Seconds INTEGER,
		)
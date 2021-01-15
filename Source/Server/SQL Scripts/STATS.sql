/*
Created 9/8/2018 7:55AM
Creates STATS table in Sabertooths Database
Created by Steven M Fortune
*/

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Stats')
	CREATE TABLE Stats (
		ID INT IDENTITY(1, 1) PRIMARY KEY,
		Name VARCHAR(25),
		Days INTEGER,
		Hours INTEGER,
		Minutes INTEGER,
		Seconds INTEGER
		)
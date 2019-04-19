/*
Created 9/2/2018 5:08AM
Creates Version table in Sabertooths Database
Created by Steven M Fortune
*/

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Version')
CREATE TABLE Version (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	Version VARCHAR(10)
	)
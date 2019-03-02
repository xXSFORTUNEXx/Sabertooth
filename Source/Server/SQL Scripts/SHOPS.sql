/*
Created 9/2/2018 5:18AM
Creates SHOPS table in Sabertooths Database
Created by Steven M Fortune
*/

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Shops')
CREATE TABLE Shops (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	Name TEXT,
	Item_Data VARBINARY(MAX)
	)

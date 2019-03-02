/*
Created 9/2/2018 5:18AM
Creates PROJECTILES table in Sabertooths Database
Created by Steven M Fortune
*/

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Projectiles')
CREATE TABLE Projectiles (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	Name TEXT,
	Damage INTEGER,
	Range INTEGER,
	Sprite INTEGER,
	Type INTEGER,
	Speed INTEGER
	)

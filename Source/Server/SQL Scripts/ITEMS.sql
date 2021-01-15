/*
Created 9/2/2018 5:18AM
Creates ITEMS table in Sabertooths Database
Created by Steven M Fortune
*/

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Items')
CREATE TABLE Items (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	Name TEXT,
	Sprite INTEGER,
	Damage INTEGER,
	Armor INTEGER,
	Type INTEGER,
	Attack_Speed INTEGER,
	Health_Restore INTEGER,
	Strength INTEGER,
	Agility INTEGER,
	Endurance INTEGER,
	Stamina INTEGER,
	Value INTEGER,
	Price INTEGER,
	Rarity INTEGER
	)

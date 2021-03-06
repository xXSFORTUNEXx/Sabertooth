/*
Created 9/2/2018 5:18AM
Creates SECONDARYWEAPONS table in Sabertooths Database
Created by Steven M Fortune
*/

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Secondary_Weapons')
CREATE TABLE Secondary_Weapons (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	Owner VARCHAR(25),
	Name TEXT,
	Clip INTEGER,
	Max_Clip INTEGER,
	Sprite INTEGER,
	Damage INTEGER,
	Armor INTEGER,
	Type INTEGER,
	Attack_Speed INTEGER,
	Reload_Speed INTEGER,
	Health_Restore INTEGER,
	Hunger_Restore INTEGER,
	Hydrate_Restore INTEGER,
	Strength INTEGER,
	Agility INTEGER,
	Endurance INTEGER,
	Stamina INTEGER,
	Ammo_Type INTEGER,
	Value INTEGER,
	Proj INTEGER,
	Price INTEGER,
	Rarity INTEGER
	)

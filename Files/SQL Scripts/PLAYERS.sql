/*
Created 9/2/2018 5:12AM
Creates PLAYERS table in Sabertooths Database
Created by Steven M Fortune
*/

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Players')
	CREATE TABLE Players (
		ID INT IDENTITY(1, 1) PRIMARY KEY,
		Name VARCHAR(25),
		Password VARCHAR(25),
		Email_Address VARCHAR(255),
		X INTEGER,
		Y INTEGER,
		Map INTEGER,
		Direction INTEGER,
		Aim_Direction INTEGER,
		Sprite INTEGER,
		Level INTEGER,
		Health INTEGER,
		Max_Health INTEGER,
		Experience INTEGER,
		Money INTEGER,
		Armor INTEGER,
		Hunger INTEGER,
		Hydration INTEGER,
		Strength INTEGER,
		Agility INTEGER,
		Endurance INTEGER,
		Stamina INTEGER,
		Pistol_Ammo INTEGER,
		Assault_Ammo INTEGER,
		Rocket_Ammo INTEGER,
		Grenade_ammo INTEGER,
		Light_Radius INTEGER,
		Last_Logged TEXT,
		Account_Key VARCHAR(25),
		Active VARCHAR(1)
		)
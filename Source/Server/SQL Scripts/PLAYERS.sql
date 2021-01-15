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
		Mana INTEGER,
		Max_Mana INTEGER,
		Experience INTEGER,
		Wallet INTEGER,
		Armor INTEGER,
		Strength INTEGER,
		Agility INTEGER,
		Intelligence INTEGER,
		Stamina INTEGER,
		Energy INTEGER,
		Light_Radius INTEGER,
		Last_Logged TEXT,
		Account_Key VARCHAR(25),
		Active VARCHAR(1)
		)
/*
Created 2019-02-17 12:52:35.907
Creates QUESTS table in Sabertooths Database
Created by Steven M Fortune
*/
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Quests')
CREATE TABLE Quests (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	Name VARCHAR(25) NOT NULL,
	Start_Message VARCHAR(255) NOT NULL,
	Inprogress_Message VARCHAR(255) NOT NULL,
	Complete_Message VARCHAR(255) NOT NULL,
	Description VARCHAR(255) NOT NULL,
	Prerequisite_Quest INTEGER,
	Level_Required INTEGER,
	Required_Item_1 INTEGER,
	Required_Item_2 INTEGER,
	Required_Item_3 INTEGER,
	Required_Item_4 INTEGER,
	Required_Item_5 INTEGER,
	Required_Item_Value_1 INTEGER,
	Required_Item_Value_2 INTEGER,
	Required_Item_Value_3 INTEGER,
	Required_Item_Value_4 INTEGER,
	Required_Item_Value_5 INTEGER,
	Required_Npc_1 INTEGER,
	Required_Npc_2 INTEGER,
	Required_Npc_3 INTEGER,
	Required_Npc_Value_1 INTEGER,
	Required_Npc_Value_2 INTEGER,
	Required_Npc_Value_3 INTEGER,
	Reward_Item_1 INTEGER,
	Reward_Item_2 INTEGER,
	Reward_Item_3 INTEGER,
	Reward_Item_4 INTEGER,
	Reward_Item_5 INTEGER,
	Reward_Item_Value_1 INTEGER,
	Reward_Item_Value_2 INTEGER,
	Reward_Item_Value_3 INTEGER,
	Reward_Item_Value_4 INTEGER,
	Reward_Item_Value_5 INTEGER,
	Experience INTEGER,
	Money INTEGER,
	Quest_Type INTEGER
	)
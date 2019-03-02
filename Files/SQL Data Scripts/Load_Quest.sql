/*
Created 2019-02-18 12:42:58.543
Load chat data into database
Created by Steven M Fortune
*/
SELECT ID,
	Name,
	Start_Message,
	Inprogress_Message,
	Complete_Message,
	Description,
	Prerequisite_Quest,
	Level_Required,
	Required_Item_1,
	Required_Item_2,
	Required_Item_3,
	Required_Item_4,
	Required_Item_5,
	Required_Item_Value_1,
	Required_Item_Value_2,
	Required_Item_Value_3,
	Required_Item_Value_4,
	Required_Item_Value_5,
	Required_Npc_1,
	Required_Npc_2,
	Required_Npc_3,
	Required_Npc_Value_1,
	Required_Npc_Value_2,
	Required_Npc_Value_3,
	Reward_Item_1,
	Reward_Item_2,
	Reward_Item_3,
	Reward_Item_4,
	Reward_Item_5,
	Reward_Item_Value_1,
	Reward_Item_Value_2,
	Reward_Item_Value_3,
	Reward_Item_Value_4,
	Reward_Item_Value_5,
	Experience,
	Money,
	Quest_Type
FROM Quests
WHERE ID = @id

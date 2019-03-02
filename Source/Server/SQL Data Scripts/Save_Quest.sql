/*
Created 2019-02-18 12:42:58.543
Saves QUEST data into database
Created by Steven M Fortune
*/
UPDATE QUESTS
SET Name = @name,
	Start_Message = @startmsg,
	Inprogress_Message = @inprogressmsg,
	Complete_Message = @completemsg,
	Description = @desc,
	Prerequisite_Quest = @prereqquest,
	Level_Required = @levelreq,
	Required_Item_1 = @reqitem1,
	Required_Item_2 = @reqitem2,
	Required_Item_3 = @reqitem3,
	Required_Item_4 = @reqitem4,
	Required_Item_5 = @reqitem5,
	Required_Item_Value_1 = @reqitemvalue1,
	Required_Item_Value_2 = @reqitemvalue2,
	Required_Item_Value_3 = @reqitemvalue3,
	Required_Item_Value_4 = @reqitemvalue4,
	Required_Item_Value_5 = @reqitemvalue5,
	Required_Npc_1 = @reqnpc1,
	Required_Npc_2 = @reqnpc2,
	Required_Npc_3 = @reqnpc3,
	Required_Npc_Value_1 = @reqnpcvalue1,
	Required_Npc_Value_2 = @reqnpcvalue2,
	Required_Npc_Value_3 = @reqnpcvalue3,
	Reward_Item_1 = @rewarditem1,
	Reward_Item_2 = @rewarditem2,
	Reward_Item_3 = @rewarditem3,
	Reward_Item_4 = @rewarditem4,
	Reward_Item_5 = @rewarditem5,
	Reward_Item_Value_1 = @rewarditemvalue1,
	Reward_Item_Value_2 = @rewarditemvalue2,
	Reward_Item_Value_3 = @rewarditemvalue3,
	Reward_Item_Value_4 = @rewarditemvalue4,
	Reward_Item_Value_5 = @rewarditemvalue5,
	Experience = @exp,
	Money = @money,
	Quest_Type = @type
WHERE ID = @id

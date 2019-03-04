/*
Created 2019-02-26 15:46:39.917
Creates vQuestInfo view in Sabertooths Database
Created by Steven M Fortune
Updated 2019-02-27 10:53:50.457
*/
IF EXISTS(SELECT * FROM SYS.views WHERE name = 'vQuestInfo')
BEGIN
	DROP VIEW vQuestInfo
END
GO
CREATE VIEW vQuestInfo
AS
SELECT Quests.ID,
	Quests.Name,
	Quests.Start_Message,
	Quests.Inprogress_Message,
	Quests.Complete_Message,
	Quests.Prerequisite_Quest,
	Quests.Level_Required, 
	Quests.Quest_Type,
	Quests.Description
FROM Quests
/*
Created 2019-02-21 16:56:16.077
Creates QUESTLIST table in Sabertooths Database
Created by Steven M Fortune
*/

UPDATE Quest_List
SET Name = @name,
	Quest_1 = @quest1,
	Quest_1_Status = @quest1status,
	Quest_2 = @quest2,
	Quest_2_Status = @quest2status,
	Quest_3 = @quest3,
	Quest_3_Status = @quest3status,
	Quest_4 = @quest4,
	Quest_4_Status = @quest4status,
	Quest_5 = @quest5,
	Quest_5_Status = @quest5status,
	Quest_6 = @quest6,
	Quest_6_Status = @quest6status,
	Quest_7 = @quest7,
	Quest_7_Status = @quest7status,
	Quest_8 = @quest8,
	Quest_8_Status = @quest8status,
	Quest_9 = @quest9,
	Quest_9_Status = @quest9status,
	Quest_10 = @quest10,
	Quest_10_Status = @quest10status
WHERE Name = @name

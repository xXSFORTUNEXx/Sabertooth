/*
Created 2019-02-21 16:56:16.077
Creates QUESTLIST table in Sabertooths Database
Created by Steven M Fortune
*/

SELECT ID,
	Name,
	Quest_1,
	Quest_1_Status,
	Quest_2,
	Quest_2_Status,
	Quest_3,
	Quest_3_Status,
	Quest_4,
	Quest_4_Status,
	Quest_5,
	Quest_5_Status,
	Quest_6,
	Quest_6_Status,
	Quest_7,
	Quest_7_Status,
	Quest_8,
	Quest_8_Status,
	Quest_9,
	Quest_9_Status,
	Quest_10,
	Quest_10_Status
FROM Quest_List
WHERE Name = @name

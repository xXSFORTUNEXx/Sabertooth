/*
Created 2019-02-21 16:56:16.077
Creates QUESTLIST table in Sabertooths Database
Created by Steven M Fortune
*/

INSERT INTO Quest_List (
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
	)
VALUES (
	@name,
	@quest1,
	@quest1status,
	@quest2,
	@quest2status,
	@quest3,
	@quest3status,
	@quest4,
	@quest4status,
	@quest5,
	@quest5status,
	@quest6,
	@quest6status,
	@quest7,
	@quest7status,
	@quest8,
	@quest8status,
	@quest9,
	@quest9status,
	@quest10,
	@quest10status
	)
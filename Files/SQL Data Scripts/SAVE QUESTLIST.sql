/*
Created 2019-02-21 16:56:16.077
Creates QUESTLIST table in Sabertooths Database
Created by Steven M Fortune
*/

UPDATE QUESTLIST
SET NAME = @name,
	QUEST1 = @quest1,
	QUEST1STATUS = @quest1status,
	QUEST2 = @quest2,
	QUEST2STATUS = @quest2status,
	QUEST3 = @quest3,
	QUEST3STATUS = @quest3status,
	QUEST4 = @quest4,
	QUEST4STATUS = @quest4status,
	QUEST5 = @quest5,
	QUEST5STATUS = @quest5status,
	QUEST6 = @quest6,
	QUEST6STATUS = @quest6status,
	QUEST7 = @quest7,
	QUEST7STATUS = @quest7status,
	QUEST8 = @quest8,
	QUEST8STATUS = @quest8status,
	QUEST9 = @quest9,
	QUEST9STATUS = @quest9status,
	QUEST10 = @quest10,
	QUEST10STATUS = @quest10status
WHERE NAME = @name

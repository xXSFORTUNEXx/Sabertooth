/*
Created 2019-02-21 16:56:16.077
Creates QUESTLIST table in Sabertooths Database
Created by Steven M Fortune
*/

INSERT INTO QUESTLIST (
	NAME,
	QUEST1,
	QUEST1STATUS,
	QUEST2,
	QUEST2STATUS,
	QUEST3,
	QUEST3STATUS,
	QUEST4,
	QUEST4STATUS,
	QUEST5,
	QUEST5STATUS,
	QUEST6,
	QUEST6STATUS,
	QUEST7,
	QUEST7STATUS,
	QUEST8,
	QUEST8STATUS,
	QUEST9,
	QUEST9STATUS,
	QUEST10,
	QUEST10STATUS
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
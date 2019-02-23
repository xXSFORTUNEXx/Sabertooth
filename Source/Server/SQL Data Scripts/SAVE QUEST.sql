/*
Created 2019-02-18 12:42:58.543
Saves QUEST data into database
Created by Steven M Fortune
*/
UPDATE QUESTS
SET NAME = @name,
	STARTMESSAGE = @startmsg,
	INPROGRESSMESSAGE = @inprogressmsg,
	COMPLETEMESSAGE = @completemsg,
	DESCRIPTION = @desc,
	PREREQUISITEQUEST = @prereqquest,
	LEVELREQUIRED = @levelreq,
	REQITEM1 = @reqitem1,
	REQITEM2 = @reqitem2,
	REQITEM3 = @reqitem3,
	REQITEM4 = @reqitem4,
	REQITEM5 = @reqitem5,
	REQITEMVALUE1 = @reqitemvalue1,
	REQITEMVALUE2 = @reqitemvalue2,
	REQITEMVALUE3 = @reqitemvalue3,
	REQITEMVALUE4 = @reqitemvalue4,
	REQITEMVALUE5 = @reqitemvalue5,
	REQNPC1 = @reqnpc1,
	REQNPC2 = @reqnpc2,
	REQNPC3 = @reqnpc3,
	REQNPCVALUE1 = @reqnpcvalue1,
	REQNPCVALUE2 = @reqnpcvalue2,
	REQNPCVALUE3 = @reqnpcvalue3,
	REWARDITEM1 = @rewarditem1,
	REWARDITEM2 = @rewarditem2,
	REWARDITEM3 = @rewarditem3,
	REWARDITEM4 = @rewarditem4,
	REWARDITEM5 = @rewarditem5,
	REWARDITEMVALUE1 = @rewarditemvalue1,
	REWARDITEMVALUE2 = @rewarditemvalue2,
	REWARDITEMVALUE3 = @rewarditemvalue3,
	REWARDITEMVALUE4 = @rewarditemvalue4,
	REWARDITEMVALUE5 = @rewarditemvalue5,
	EXPERIENCE = @exp,
	MONEY = @money,
	QUESTTPYE = @type
WHERE ID = @id

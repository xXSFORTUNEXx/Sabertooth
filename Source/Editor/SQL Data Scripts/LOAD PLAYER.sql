﻿/*
Created 9/8/2018 12:11AM
Created by Steven M Fortune
*/

SELECT ID,
	NAME,
	PASSWORD,
	EMAILADDRESS,
	X,
	Y,
	MAP,
	DIRECTION,
	AIMDIRECTION,
	SPRITE,
	LEVEL,
	POINTS,
	HEALTH,
	MAXHEALTH,
	EXPERIENCE,
	MONEY,
	ARMOR,
	HUNGER,
	HYDRATION,
	STRENGTH,
	AGILITY,
	ENDURANCE,
	STAMINA,
	PISTOLAMMO,
	ASSAULTAMMO,
	ROCKETAMMO,
	GRENADEAMMO,
	LIGHTRADIUS,
	DAYS,
	HOURS,
	MINUTES,
	SECONDS,
	LDAYS,
	LHOURS,
	LMINUTES,
	LSECONDS,
	LLDAYS,
	LLHOURS,
	LLMINUTES,
	LLSECONDS,
	LASTLOGGED,
	ACCOUNTKEY,
	ACTIVE
FROM PLAYERS
WHERE NAME = @name

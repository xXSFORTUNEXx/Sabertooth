/*
Created 9/2/2018 5:18AM
Creates BASIC STATS INFO view in Sabertooths Database
Created by Steven M Fortune
Updated 2019-01-21 14:33:00.010
*/
CREATE VIEW [STATS INFO]
AS
SELECT STATS.ID,
	STATS.NAME,
	STATS.KILLS,
	STATS.POINTS,
	PLAYERS.LASTLOGGED,
	STATS.DAYS AS DAYS_PLAYED,
	STATS.HOURS AS HOURS_PLAYED,
	STATS.MINUTES AS MINUTES_PLAYED,
	STATS.SECONDS AS SECONDS_PLAYED,
	STATS.LDAYS AS LONGEST_DAYS,
	STATS.LHOURS AS LONGEST_HOURS,
	STATS.LMINUTES AS LONGEST_MINUTES,
	STATS.LSECONDS AS LONGEST_SECONDS,
	STATS.LLDAYS AS LONGEST_LIFE_DAYS,
	STATS.LLMINUTES AS LONGEST_LIFE_MINUTES,
	STATS.LLSECONDS AS LONGEST_LIFE_SECONDS
FROM STATS
JOIN PLAYERS ON STATS.NAME = PLAYERS.NAME

--SELECT CONVERT(VARCHAR, STATS.DAYS) + ':' + CONVERT(VARCHAR, STATS.HOURS) + ':' + CONVERT(VARCHAR, STATS.MINUTES) + ':' + CONVERT(VARCHAR, STATS.SECONDS) FROM STATS
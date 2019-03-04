/*
Created 9/2/2018 5:18AM
Creates vStatsInfo view in Sabertooths Database
Created by Steven M Fortune
Updated 2019-02-27 10:53:50.457
*/
IF EXISTS(SELECT * FROM SYS.views WHERE name = 'vStatsInfo')
BEGIN
	DROP VIEW vStatsInfo
END
GO
CREATE VIEW vStatsInfo
AS
SELECT Stats.ID,
	Stats.Name,
	Stats.Kills,
	Stats.Points,
	Players.Last_Logged,
	Stats.Days,
	Stats.Hours,
	Stats.Minutes,
	Stats.Seconds,
	Stats.Longest_Days,
	Stats.Longest_Hours,
	Stats.Longest_Minutes,
	Stats.Longest_Seconds,
	Stats.Longest_Life_Days,
	Stats.Longest_Life_Hours,
	Stats.Longest_Life_Minutes,
	Stats.Longest_Life_Seconds
FROM Stats
JOIN Players ON Stats.Name = PLAYERS.Name

--SELECT CONVERT(VARCHAR, Stats.DAYS) + ':' + CONVERT(VARCHAR, Stats.HOURS) + ':' + CONVERT(VARCHAR, Stats.MINUTES) + ':' + CONVERT(VARCHAR, Stats.SECONDS) FROM Stats
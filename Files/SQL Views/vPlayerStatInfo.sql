/*
Created 9/2/2018 5:18AM
Creates vPlayerStatInfo view in Sabertooths Database
Created by Steven M Fortune
Updated 2019-02-27 10:53:50.457
*/
IF EXISTS(SELECT * FROM SYS.views WHERE name = 'vPlayerStatInfo')
BEGIN
	DROP VIEW vPlayerStatInfo
END
GO
CREATE VIEW vPlayerStatInfo
AS
SELECT Players.ID,
	Players.Name,
	Players.Email_Address,
	Players.Level,
	Players.Health,
	Players.Max_Health,
	Players.Experience,
	Players.Money,
	Stats.Kills,
	Stats.Points,
	Players.Armor,
	Players.Hunger,
	Players.Hydration,
	Players.Strength,
	Players.Agility,
	Players.Endurance,
	Players.Stamina,
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
FROM Players
JOIN Stats ON Players.Name = Stats.Name

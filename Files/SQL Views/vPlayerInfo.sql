/*
Created 2019-03-04 15:44:07.190
Creates vPlayerInfo view in Sabertooths Database
Created by Steven M Fortune
*/
IF EXISTS (SELECT * FROM SYS.VIEWS WHERE name = 'vPlayerInfo')
BEGIN
	DROP VIEW vPlayerInfo
END
GO
CREATE VIEW vPlayerInfo
AS
SELECT Players.ID,
	Players.Name,
	Players.Email_Address,
	Players.Level,
	Players.Health,
	Players.Max_Health,
	Players.Experience,
	Players.Money,
	Players.Armor,
	Players.Hunger,
	Players.Hydration,
	Players.Strength,
	Players.Agility,
	Players.Endurance,
	Players.Stamina
FROM Players

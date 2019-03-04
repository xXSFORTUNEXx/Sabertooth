/*
Created 2019-03-04 15:44:07.190
Creates vItemInfo view in Sabertooths Database
Created by Steven M Fortune
*/
IF EXISTS(SELECT * FROM SYS.views WHERE name = 'vNpcInfo')
BEGIN
	DROP VIEW vNpcInfo
END
GO
CREATE VIEW vNpcInfo
AS
SELECT Npcs.ID,
	Npcs.Name,
	Npcs.X,
	Npcs.Y,
	Npcs.Direction,
	Npcs.Sprite,
	Npcs.Step,
	Npcs.Owner,
	Npcs.Behavior,
	Npcs.Spawn_Time,
	Npcs.Health,
	Npcs.Max_Health,
	Npcs.Damage,
	Npcs.Des_X,
	Npcs.Des_Y,
	Npcs.Exp,
	Npcs.Money,
	Npcs.Range,
	Npcs.Shop_Num,
	Npcs.Chat_Num,
	Npcs.Speed
FROM Npcs
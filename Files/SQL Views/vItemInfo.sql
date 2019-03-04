/*
Created 2019-03-04 15:44:07.190
Creates vItemInfo view in Sabertooths Database
Created by Steven M Fortune
*/
IF EXISTS(SELECT * FROM SYS.views WHERE name = 'vItemInfo')
BEGIN
	DROP VIEW vItemInfo
END
GO
CREATE VIEW vItemInfo
AS
SELECT ID,
	Items.Name,
	Items.Sprite,
	Items.Damage,
	Items.Armor,
	Items.Type,
	Items.Attack_Speed,
	Items.Reload_Speed,
	Items.Health_Restore,
	Items.Hunger_Restore,
	Items.Hydrate_Restore,
	Items.Strength,
	Items.Agility,
	Items.Endurance,
	Items.Stamina,
	Items.Clip,
	Items.Max_Clip,
	Items.Ammo_Type,
	Items.Value,
	Items.Proj,
	Items.Price,
	Items.Rarity
FROM Items
/*
Created 2019-03-04 15:44:07.190
Creates vBank view in Sabertooths Database
Created by Steven M Fortune
*/
IF EXISTS(SELECT * FROM SYS.views WHERE name = 'vBank')
BEGIN
	DROP VIEW vBank
END
GO
CREATE VIEW vBank
AS
SELECT Bank.ID,
	Bank.Owner,
	Bank.Slot,
	Bank.Name,
	Bank.Clip,
	Bank.Max_Clip,
	Bank.Sprite,
	Bank.Damage,
	Bank.Armor,
	Bank.Type,
	Bank.Attack_Speed,
	Bank.Reload_Speed,
	Bank.Health_Restore,
	Bank.Hunger_Restore,
	Bank.Hydrate_Restore,
	Bank.Strength,
	Bank.Agility,
	Bank.Endurance,
	Bank.Stamina,
	Bank.Ammo_Type,
	Bank.Value,
	Bank.Proj,
	Bank.Price,
	Bank.Rarity
FROM Bank
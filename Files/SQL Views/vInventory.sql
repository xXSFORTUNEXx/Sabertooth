/*
Created 2019-03-04 15:44:07.190
Creates vInventory view in Sabertooths Database
Created by Steven M Fortune
*/
IF EXISTS(SELECT * FROM SYS.views WHERE name = 'vInventory')
BEGIN
	DROP VIEW vInventory
END
GO
CREATE VIEW vInventory
AS
SELECT Inventory.ID,
	Inventory.Owner,
	Inventory.Slot,
	Inventory.Name,
	Inventory.Clip,
	Inventory.Max_Clip,
	Inventory.Sprite,
	Inventory.Damage,
	Inventory.Armor,
	Inventory.Type,
	Inventory.Attack_Speed,
	Inventory.Reload_Speed,
	Inventory.Health_Restore,
	Inventory.Hunger_Restore,
	Inventory.Hydrate_Restore,
	Inventory.Strength,
	Inventory.Agility,
	Inventory.Endurance,
	Inventory.Stamina,
	Inventory.Ammo_Type,
	Inventory.Value,
	Inventory.Proj,
	Inventory.Price,
	Inventory.Rarity
FROM Inventory
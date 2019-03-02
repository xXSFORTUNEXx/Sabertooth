/*
Created 9/7/2018 2:02PM
Inserts chat data into database
Created by Steven M Fortune
*/

SELECT ID,
	Name,
	Sprite,
	Damage,
	Armor,
	Type,
	Attack_Speed,
	Reload_Speed,
	Health_Restore,
	Hunger_Restore,
	Hydrate_Restore,
	Strength,
	Agility,
	Endurance,
	Stamina,
	Clip,
	Max_Clip,
	Ammo_Type,
	Value,
	Proj,
	Price,
	Rarity
FROM Items
WHERE ID = @id

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
	Health_Restore,
	Strength,
	Agility,
	Endurance,
	Stamina,
	Value,
	Price,
	Rarity
FROM Items
WHERE ID = @id

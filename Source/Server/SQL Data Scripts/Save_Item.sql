/*
Created 9/7/2018 1:58PM
Inserts chat data into database
Created by Steven M Fortune
*/

UPDATE ITEMS
SET Name = @name,
	Sprite = @sprite,
	Damage = @damage,
	Armor = @armor,
	Type = @type,
	Attack_Speed = @attackspeed,
	Health_Restore = @healthrestore,
	Strength = @strength,
	Agility = @agility,
	Endurance = @endurance,
	Stamina = @stamina,
	Value = @value,
	Price = @price,
	Rarity = @rarity
WHERE ID = @id;

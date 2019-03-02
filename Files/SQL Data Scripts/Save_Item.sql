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
	Reload_Speed = @reloadspeed,
	Health_Restore = @healthrestore,
	Hunger_Restore = @hungerrestore,
	Hydrate_Restore = @hydraterestore,
	Strength = @strength,
	Agility = @agility,
	Endurance = @endurance,
	Stamina = @stamina,
	Clip = @clip,
	Max_Clip = @maxclip,
	Ammo_Type = @ammotype,
	Value = @value,
	Proj = @proj,
	Price = @price,
	Rarity = @rarity
WHERE ID = @id;

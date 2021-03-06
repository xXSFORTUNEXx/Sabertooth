﻿SELECT ID,
	Owner,
	Slot,
	Name,
	Clip,
	Max_Clip,
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
	Ammo_Type,
	Value,
	Proj,
	Price,
	Rarity
FROM Equipment
WHERE Owner = @owner
	AND Slot = @slot

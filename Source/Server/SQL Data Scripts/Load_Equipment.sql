SELECT ID,
	Owner,
	Slot,
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
FROM Equipment
WHERE Owner = @owner
	AND Slot = @slot

SELECT ID,
	Owner,
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
FROM Secondary_Weapons
WHERE Owner = @owner

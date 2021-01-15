UPDATE Main_Weapons
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
WHERE Owner = @owner

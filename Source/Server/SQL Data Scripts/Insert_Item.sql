/*
Created 9/7/2018 1:52PM
Inserts chat data into database
Created by Steven M Fortune
*/

INSERT INTO ITEMS (
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
	)
VALUES (
	@name,
	@sprite,
	@damage,
	@armor,
	@type,
	@attackspeed,
	@healthrestore,
	@strength,
	@agility,
	@endurance,
	@stamina,
	@value,
	@price,
	@rarity
	)

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
	Mana_Restore,
	Strength,
	Agility,
	Intelligence,
	Energy,
	Stamina,
	Value,
	Price,
	Rarity,
	CoolDown,
	AddMax_Health,
	AddMax_Mana,
	Bonus_XP,
	Spell_Number,
	Stackable,
	MaxStack
FROM Items
WHERE ID = @id

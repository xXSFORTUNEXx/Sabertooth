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
	Mana_Restore = @manarestore,
	Strength = @strength,
	Agility = @agility,
	Intelligence = @intelligence,
	Energy = @energy,
	Stamina = @stamina,
	Value = @value,
	Price = @price,
	Rarity = @rarity,
	CoolDown = @cooldown,
	AddMax_Health = @addmaxhp,
	AddMax_Mana = @addmaxmp,
	Bonus_XP = @bonusxp,
	Spell_Number = @spellnum,
	Stackable = @stack,
	MaxStack = @maxstack
WHERE ID = @id;

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
	)
VALUES (
	@name,
	@sprite,
	@damage,
	@armor,
	@type,
	@attackspeed,
	@healthrestore,
	@manarestore,
	@strength,
	@agility,
	@intelligence,
	@energy,
	@stamina,
	@value,
	@price,
	@rarity,
	@cooldown,
	@addmaxhp,
	@addmaxmp,
	@bonusxp,
	@spellnum,
	@stack,
	@maxstack
	)
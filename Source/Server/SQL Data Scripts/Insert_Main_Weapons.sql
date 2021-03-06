INSERT INTO Main_Weapons (
	Owner,
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
	@owner,
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

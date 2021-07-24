/*
Created 9/7/2018 1:58PM
Inserts chat data into database
Created by Steven M Fortune
*/

UPDATE Spells
SET Name = @name,
	Level = @level,
	Icon = @icon,
	Vital = @vital,
	HPCost = @hpcost,
	MPCost = @mpcost,
	CoolDown = @cooldown,
	CastTime = @casttime,
	Charges = @charges,
	TotalTick = @totaltick,
	TickInterval = @tickinterval,
	SpellType = @spelltype,
	Range = @range,
	Animation = @animation,
	AOE = @aoe,
	Distance = @distance,
	Projectile = @projectile,
	Sprite = @sprite,
	SelfCast = @selfcast,
	RenderOnTarget = @renderontarget
WHERE ID = @id;	
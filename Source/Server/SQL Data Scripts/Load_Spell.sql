/*
Created 9/7/2018 2:02PM
Inserts chat data into database
Created by Steven M Fortune
*/

SELECT ID,
	Name,
	Level,
	Icon,
	Vital,
	HPCost,
	MPCost,
	CoolDown,
	CastTime,
	Charges,
	TotalTick,
	TickInterval,
	SpellType,
	Range,
	Animation,
	AOE,
	Distance,
	Projectile,
	Sprite,
	SelfCast,
	RenderOnTarget
FROM Spells
WHERE ID = @id

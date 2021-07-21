/*
Created 9/7/2018 1:52PM
Inserts chat data into database
Created by Steven M Fortune
*/

INSERT INTO Spells (
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
	Distance
	)
VALUES (
	@name,
	@level,
	@icon,
	@vital,
	@hpcost,
	@mpcost,
	@cooldown,
	@casttime,
	@charges,
	@totaltick,
	@tickinterval,
	@spelltype,
	@range,
	@animation,
	@aoe,
	@distance
	)

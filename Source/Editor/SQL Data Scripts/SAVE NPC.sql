/*
Created 9/7/2018 3:52PM
Inserts chat data into database
Created by Steven M Fortune
*/

UPDATE NPCS
SET NAME = @name,
	X = @x,
	Y = @y,
	DIRECTION = @direction,
	SPRITE = @sprite,
	STEP = @step,
	OWNER = @owner,
	BEHAVIOR = @behavior,
	SPAWNTIME = @spawntime,
	HEALTH = @health,
	MAXHEALTH = @maxhealth,
	DAMAGE = @damage,
	DESX = @desx,
	DESY = @desy,
	EXP = @exp,
	MONEY = @money,
	RANGE = @range,
	SHOPNUM = @shopnum,
	CHATNUM = @chatnum,
	SPEED = @speed
WHERE ID = @id

/*
Created 9/7/2018 3:52PM
Inserts chat data into database
Created by Steven M Fortune
*/

INSERT INTO NPCS (
	NAME,
	X,
	Y,
	DIRECTION,
	SPRITE,
	STEP,
	OWNER,
	BEHAVIOR,
	SPAWNTIME,
	HEALTH,
	MAXHEALTH,
	DAMAGE,
	DESX,
	DESY,
	EXP,
	MONEY,
	RANGE,
	SHOPNUM,
	CHATNUM,
	SPEED
	)
VALUES (
	@name,
	@x,
	@y,
	@direction,
	@sprite,
	@step,
	@owner,
	@behavior,
	@spawntime,
	@health,
	@maxhealth,
	@damage,
	@desx,
	@desy,
	@exp,
	@money,
	@range,
	@shopnum,
	@chatnum,
	@speed
	)

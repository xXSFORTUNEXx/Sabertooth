/*
Created 9/7/2018 3:52PM
Inserts chat data into database
Created by Steven M Fortune
*/

INSERT INTO Npcs (
	Name,
	X,
	Y,
	Direction,
	Sprite,
	Step,
	Owner,
	Behavior,
	Spawn_Time,
	Health,
	Max_Health,
	Damage,
	Des_X,
	Des_Y,
	Exp,
	Money,
	Range,
	Shop_Num,
	Chat_Num,
	Speed
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

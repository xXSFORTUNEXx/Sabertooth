/*
Created 9/7/2018 3:52PM
Inserts chat data into database
Created by Steven M Fortune
*/

UPDATE Npcs
SET Name = @name,
	X = @x,
	Y = @y,
	Direction = @direction,
	Sprite = @sprite,
	Step = @step,
	Owner = @owner,
	Behavior = @behavior,
	Spawn_Time = @spawntime,
	Health = @health,
	Max_Health = @maxhealth,
	Damage = @damage,
	Des_X = @desx,
	Des_Y = @desy,
	Exp = @exp,
	Money = @money,
	Range = @range,
	Shop_Num = @shopnum,
	Chat_Num = @chatnum,
	Speed = @speed
WHERE ID = @id

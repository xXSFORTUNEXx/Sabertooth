/*
Created 9/7/2018 3:52PM
Inserts chat data into database
Created by Steven M Fortune
*/

SELECT ID,
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
FROM Npcs
WHERE ID = @id

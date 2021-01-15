/*
declare @regentime integer = 60000
declare @hungertime integer = 600000
declare @hydrationtime integer = 30000
declare @savetime integer = 300000
declare @spawntime integer = 1000
declare @aitime integer = 1000
declare @id integer = 1
*/
UPDATE Configuration
SET Regen_Time = @regentime,
	Save_Time = @savetime,
	Spawn_Time = @spawntime,
	AiTime = @aitime
WHERE ID = @id
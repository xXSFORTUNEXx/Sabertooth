--Predefined settings
/*
declare @regentime integer = 60000
declare @hungertime integer = 600000
declare @hydrationtime integer = 30000
declare @savetime integer = 300000
declare @spawntime integer = 1000
declare @aitime integer = 1000
*/
INSERT INTO Configuration (
	Regen_Time,
	Hunger_Time,
	Hydration_Time,
	Save_Time,
	Spawn_Time,
	AiTime
	)
VALUES (
	@regentime,
	@hungertime,
	@hydrationtime,
	@savetime,
	@spawntime,
	@aitime
	)
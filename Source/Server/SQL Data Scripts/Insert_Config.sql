--Predefined settings
/*
declare @regentime integer = 60000
declare @savetime integer = 300000
declare @spawntime integer = 1000
declare @aitime integer = 1000
*/
INSERT INTO Configuration (
	Regen_Time,
	Save_Time,
	Spawn_Time,
	AiTime
	)
VALUES (
	@regentime,
	@savetime,
	@spawntime,
	@aitime
	)
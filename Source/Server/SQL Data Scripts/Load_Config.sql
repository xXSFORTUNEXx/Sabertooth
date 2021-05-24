--declare @id integer = 1
SELECT ID,
	Regen_Time,
	M_Regen_Time,
	Save_Time,
	Spawn_Time,
	AiTime
FROM Configuration
WHERE ID = @id

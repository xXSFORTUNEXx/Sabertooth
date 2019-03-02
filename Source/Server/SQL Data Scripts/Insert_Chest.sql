INSERT INTO Chests (
	Name,
	Money,
	Experience,
	Required_Level,
	Trap_Level,
	Req_Key,
	Damage,
	Npc_Spawn,
	Spawn_Amount,
	Chest_Item
	)
VALUES (
	@name,
	@money,
	@experience,
	@requiredlevel,
	@traplevel,
	@key,
	@damage,
	@npcspawn,
	@spawnamount,
	@chestitem
	)
SELECT ID,
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
FROM Chests
WHERE ID = @id

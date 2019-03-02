UPDATE Chests
SET NAME = @name,
	Money = @money,
	Experience = @experience,
	Required_Level = @requiredlevel,
	Trap_Level = @traplevel,
	Req_Key = @key,
	Damage = @damage,
	Npc_Spawn = @npcspawn,
	Spawn_Amount = @spawnamount,
	Chest_Item = @chestitem
WHERE ID = @id

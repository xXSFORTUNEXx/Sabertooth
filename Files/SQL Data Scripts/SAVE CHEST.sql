UPDATE CHESTS
SET NAME = @name,
	MONEY = @money,
	EXPERIENCE = @experience,
	REQUIREDLEVEL = @requiredlevel,
	TRAPLEVEL = @traplevel,
	REQKEY = @key,
	DAMAGE = @damage,
	NPCSPAWN = @npcspawn,
	SPAWNAMOUNT = @spawnamount,
	CHESTITEM = @chestitem
WHERE ID = @id

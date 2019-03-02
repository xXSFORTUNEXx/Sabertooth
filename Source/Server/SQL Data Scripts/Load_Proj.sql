SELECT ID,
	Name,
	Damage,
	Range,
	Sprite,
	Type,
	Speed
FROM Projectiles
WHERE ID = @id

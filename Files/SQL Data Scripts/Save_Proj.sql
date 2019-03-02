UPDATE Projectiles
SET Name = @name,
	Damage = @damage,
	Range = @range,
	Sprite = @sprite,
	Type = @type,
	Speed = @speed
WHERE ID = @id

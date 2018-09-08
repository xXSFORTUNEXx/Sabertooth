UPDATE PROJECTILES
SET NAME = @name,
	DAMAGE = @damage,
	RANGE = @range,
	SPRITE = @sprite,
	TYPE = @type,
	SPEED = @speed
WHERE ID = @id

SELECT ID,
	NAME,
	DAMAGE,
	RANGE,
	SPRITE,
	TYPE,
	SPEED
FROM PROJECTILES
WHERE ID = @id

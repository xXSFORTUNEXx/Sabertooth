SELECT ID,
	OWNER,
	SLOT,
	NAME,
	SPRITE,
	DAMAGE,
	ARMOR,
	TYPE,
	ATTACKSPEED,
	RELOADSPEED,
	HEALTHRESTORE,
	HUNGERRESTORE,
	HYDRATERESTORE,
	STRENGTH,
	AGILITY,
	ENDURANCE,
	STAMINA,
	CLIP,
	MAXCLIP,
	AMMOTYPE,
	VALUE,
	PROJ,
	PRICE,
	RARITY
FROM BANK
WHERE OWNER = @owner
	AND SLOT = @slot
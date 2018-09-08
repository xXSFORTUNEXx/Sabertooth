/*
Created 9/7/2018 1:52PM
Inserts chat data into database
Created by Steven M Fortune
*/

INSERT INTO ITEMS (
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
	)
VALUES (
	@name,
	@sprite,
	@damage,
	@armor,
	@type,
	@attackspeed,
	@reloadspeed,
	@healthrestore,
	@hungerrestore,
	@hydraterestore,
	@strength,
	@agility,
	@endurance,
	@stamina,
	@clip,
	@maxclip,
	@ammotype,
	@value,
	@proj,
	@price,
	@rarity
	)

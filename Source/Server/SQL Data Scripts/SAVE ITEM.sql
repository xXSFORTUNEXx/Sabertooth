/*
Created 9/7/2018 1:58PM
Inserts chat data into database
Created by Steven M Fortune
*/

UPDATE ITEMS
SET NAME = @name,
	SPRITE = @sprite,
	DAMAGE = @damage,
	ARMOR = @armor,
	TYPE = @type,
	ATTACKSPEED = @attackspeed,
	RELOADSPEED = @reloadspeed,
	HEALTHRESTORE = @healthrestore,
	HUNGERRESTORE = @hungerrestore,
	HYDRATERESTORE = @hydraterestore,
	STRENGTH = @strength,
	AGILITY = @agility,
	ENDURANCE = @endurance,
	STAMINA = @stamina,
	CLIP = @clip,
	MAXCLIP = @maxclip,
	AMMOTYPE = @ammotype,
	VALUE = @value,
	PROJ = @proj,
	PRICE = @price,
	RARITY = @rarity
WHERE ID = @id;

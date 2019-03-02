/*
Created 9/7/2018 1:52PM
Inserts chat data into database
Created by Steven M Fortune
*/

INSERT INTO ITEMS (
	Name,
	Sprite,
	Damage,
	Armor,
	Type,
	Attack_Speed,
	Reload_Speed,
	Health_Restore,
	Hunger_Restore,
	Hydrate_Restore,
	Strength,
	Agility,
	Endurance,
	Stamina,
	Clip,
	Max_Clip,
	Ammo_Type,
	Value,
	Proj,
	Price,
	Rarity
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

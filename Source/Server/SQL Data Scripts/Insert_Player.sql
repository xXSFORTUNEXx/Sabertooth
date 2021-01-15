/*
Created 9/7/2018 3:52PM
Inserts player data database
Created by Steven M Fortune
*/

INSERT INTO Players (
	Name,
	Password,
	Email_Address,
	X,
	Y,
	Map,
	Direction,
	Aim_Direction,
	Sprite,
	Level,
	Health,
	Max_Health,
	Mana,
	Max_Mana,
	Experience,
	Wallet,
	Armor,
	Strength,
	Agility,
	Intelligence,
	Stamina,
	Energy,
	Light_Radius,
	Last_Logged,
	Account_Key,
	Active
	)
VALUES (
	@name,
	@password,
	@email,
	@x,
	@y,
	@map,
	@direction,
	@aimdirection,
	@sprite,
	@level,
	@health,
	@maxhealth,
	@mana,
	@maxmana,
	@experience,
	@wallet,
	@armor,
	@strength,
	@agility,
	@intelligence,
	@stamina,
	@energy,
	@lightradius,
	@lastlogged,
	@accountkey,
	@active
	);

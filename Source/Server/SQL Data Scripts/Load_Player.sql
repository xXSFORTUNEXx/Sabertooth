/*
Created 9/8/2018 12:11AM
Created by Steven M Fortune
*/

SELECT ID,
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
	Experience,
	Money,
	Armor,
	Hunger,
	Hydration,
	Strength,
	Agility,
	Endurance,
	Stamina,
	Pistol_Ammo,
	Assault_Ammo,
	Rocket_Ammo,
	Grenade_ammo,
	Light_Radius,
	Last_Logged,
	Account_Key,
	Active
FROM Players
WHERE Name = @name

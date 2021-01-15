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
FROM Players
WHERE Name = @name

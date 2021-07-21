/*
Created 9/7/2018 2:02PM
Inserts chat data into database
Created by Steven M Fortune
*/

SELECT SpellNumber_1,
	SpellNumber_2,
	SpellNumber_3,
	SpellNumber_4,
	SpellNumber_5,
	SpellNumber_6,
	SpellNumber_7,
	SpellNumber_8,
	SpellNumber_9,
	SpellNumber_10,
	SpellNumber_11,
	SpellNumber_12,
	SpellNumber_13,
	SpellNumber_14,
	SpellNumber_15,
	SpellNumber_16,
	SpellNumber_17,
	SpellNumber_18,
	SpellNumber_19,
	SpellNumber_20,
	SpellNumber_21,
	SpellNumber_22,
	SpellNumber_23,
	SpellNumber_24,
	SpellNumber_25
FROM SpellBook
WHERE PlayerID = @playerid
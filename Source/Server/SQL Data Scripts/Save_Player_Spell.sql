/*
Created 9/7/2018 1:58PM
Inserts chat data into database
Created by Steven M Fortune
*/

UPDATE SpellBook
SET SpellNumber_1 = @spellnumber_1,
	SpellNumber_2 = @spellnumber_2,
	SpellNumber_3 = @spellnumber_3,
	SpellNumber_4 = @spellnumber_4,
	SpellNumber_5 = @spellnumber_5,
	SpellNumber_6 = @spellnumber_6,
	SpellNumber_7 = @spellnumber_7,
	SpellNumber_8 = @spellnumber_8,
	SpellNumber_9 = @spellnumber_9,
	SpellNumber_10 = @spellnumber_10,
	SpellNumber_11 = @spellnumber_11,
	SpellNumber_12 = @spellnumber_12,
	SpellNumber_13 = @spellnumber_13,
	SpellNumber_14 = @spellnumber_14,
	SpellNumber_15 = @spellnumber_15,
	SpellNumber_16 = @spellnumber_16,
	SpellNumber_17 = @spellnumber_17,
	SpellNumber_18 = @spellnumber_18,
	SpellNumber_19 = @spellnumber_19,
	SpellNumber_20 = @spellnumber_20,
	SpellNumber_21 = @spellnumber_21,
	SpellNumber_22 = @spellnumber_22,
	SpellNumber_23 = @spellnumber_23,
	SpellNumber_24 = @spellnumber_24,
	SpellNumber_25 = @spellnumber_25
WHERE PlayerID = @playerid
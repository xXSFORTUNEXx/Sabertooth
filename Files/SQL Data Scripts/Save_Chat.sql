/*
Created 9/2/2018 5:18AM
Saves chat data into database
Created by Steven M Fortune
Updated 2019-02-20 15:31:30.060
*/

UPDATE Chat
SET Name = @name,
	Main_Message = @msg,
	Option_A = @optiona,
	Option_B = @optionb,
	Option_C = @optionc,
	Option_D = @optionc,
	Next_Chat_A = @nextchata,
	Next_Chat_B = @nextchatb,
	Next_Chat_C = @nextchatc,
	Next_Chat_D = @nextchatd,
	Shop_Num = @shopnum,
	Quest_Num = @questnum,
	Item_A = @itema,
	Item_B = @itemb,
	Item_C = @itemc,
	Value_A = @vala,
	Value_B = @valb,
	Value_C = @valc,
	Money = @money,
	Type = @type
WHERE ID = @id

/*
Created 9/2/2018 5:18AM
Saves chat data into database
Created by Steven M Fortune
Updated 2019-02-20 15:31:30.060
*/

UPDATE CHAT
SET NAME = @name,
	MAINMESSAGE = @msg,
	OPTIONA = @optiona,
	OPTIONB = @optionb,
	OPTIONC = @optionc,
	OPTIOND = @optionc,
	NEXTCHATA = @nextchata,
	NEXTCHATB = @nextchatb,
	NEXTCHATC = @nextchatc,
	NEXTCHATD = @nextchatd,
	SHOPNUM = @shopnum,
	QUESTNUM = @questnum,
	ITEMA = @itema,
	ITEMB = @itemb,
	ITEMC = @itemc,
	VALA = @vala,
	VALB = @valb,
	VALC = @valc,
	MONEY = @money,
	TYPE = @type
WHERE ID = @id

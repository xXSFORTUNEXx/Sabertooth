/*
Created 9/2/2018 5:18AM
Inserts chat data into database
Created by Steven M Fortune
*/

INSERT INTO CHAT (
	NAME,
	MAINMESSAGE,
	OPTIONA,
	OPTIONB,
	OPTIONC,
	OPTIOND,
	NEXTCHATA,
	NEXTCHATB,
	NEXTCHATC,
	NEXTCHATD,
	SHOPNUM,
	MISSIONNUM,
	ITEMA,
	ITEMB,
	ITEMC,
	VALA,
	VALB,
	VALC,
	MONEY,
	TYPE
	)
VALUES (
	@name,
	@msg,
	@optiona,
	@optionb,
	@optionc,
	@optiond,
	@nextchata,
	@nextchatb,
	@nextchatc,
	@nextchatd,
	@shopnum,
	@missionnum,
	@itema,
	@itemb,
	@itemc,
	@vala,
	@valb,
	@valc,
	@money,
	@type
	)

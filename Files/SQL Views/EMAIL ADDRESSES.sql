/*
Created 9/2/2018 5:18AM
Creates EMAIL ADDRESSES view in Sabertooths Database
Created by Steven M Fortune
Updated 2019-01-21 14:33:00.010
*/
CREATE VIEW [EMAIL ADDRESSES]
AS
SELECT PLAYERS.ID,
	PLAYERS.NAME,
	PLAYERS.EMAILADDRESS
FROM PLAYERS
/*
Created 9/2/2018 5:18AM
Creates vEmailAddress view in Sabertooths Database
Created by Steven M Fortune
Updated 2019-02-27 10:53:50.457
*/
IF EXISTS(SELECT * FROM SYS.views WHERE name = 'vEmailAddress')
BEGIN
	DROP VIEW vEmailAddress
END
GO
CREATE VIEW vEmailAddress
AS
SELECT PLAYERS.ID,
	PLAYERS.NAME,
	PLAYERS.EMAILADDRESS
FROM PLAYERS
/*
Created 2019-02-15 15:23:53.010
Creates vPlayerMiscInfo view in Sabertooths Database
Created by Steven M Fortune
Updated 2019-02-27 10:53:50.457
*/
IF EXISTS(SELECT * FROM SYS.views WHERE name = 'vPlayerMiscInfo')
BEGIN
	DROP VIEW vPlayerMiscInfo
END
GO
CREATE VIEW vPlayerMiscInfo
AS
SELECT Players.ID,
	Players.Name,
	Players.X,
	Players.Y,
	Players.Aim_Direction,
	Players.Sprite,
	Players.Light_Radius,
	Players.Account_Key,
	Players.Active
FROM Players
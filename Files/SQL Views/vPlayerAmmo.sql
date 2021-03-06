/*
Created 2/2/2019 12:18AM
Creates vPlayerAmmo view in Sabertooths Database
Created by Steven M Fortune
Updated 2019-02-27 10:53:50.457
*/
IF EXISTS(SELECT * FROM SYS.views WHERE name = 'vPlayerAmmo')
BEGIN
	DROP VIEW vPlayerAmmo
END
GO
CREATE VIEW vPlayerAmmo
AS
SELECT Players.ID,
	Players.NAME,
	Players.Pistol_Ammo,
	Players.Assault_Ammo,
	Players.Rocket_Ammo,
	Players.Grenade_ammo
FROM Players
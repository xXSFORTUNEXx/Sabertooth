SELECT HotKey,
	   SpellNumber,
	   InvNumber
FROM HotBar
WHERE PlayerID = @playerid
	AND HotBarNumber = @hotbarnum

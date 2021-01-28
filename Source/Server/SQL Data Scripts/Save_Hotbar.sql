UPDATE Hotbar
SET HotKey = @hotkey,
	SpellNumber = @spellnum,
	InvNumber = @invnum
WHERE PlayerID = @playerid and HotBarNumber = @hotbarnum

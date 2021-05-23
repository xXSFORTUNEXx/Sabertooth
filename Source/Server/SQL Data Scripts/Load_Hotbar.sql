/*
/////////////////////////////////
/// Load Hotbar from database ///
/// Created By Steven Fortune ///
///		5/22/2021 722PM       ///
/////////////////////////////////
*/

SELECT HotKey_1,
	   SpellNumber_1,
	   InvNumber_1,

	   HotKey_2,
	   SpellNumber_2,
	   InvNumber_2,
	   
	   HotKey_3,
	   SpellNumber_3,
	   InvNumber_3,
	   
	   HotKey_4,
	   SpellNumber_4,
	   InvNumber_4,
	   
	   HotKey_5,
	   SpellNumber_5,
	   InvNumber_5,
	   
	   HotKey_6,
	   SpellNumber_6,
	   InvNumber_6,
	   
	   HotKey_7,
	   SpellNumber_7,
	   InvNumber_7,
	   
	   HotKey_8,
	   SpellNumber_8,
	   InvNumber_8,
	   
	   HotKey_9,
	   SpellNumber_9,
	   InvNumber_9,
	   
	   HotKey_10,
	   SpellNumber_10,
	   InvNumber_10
FROM HotBar
WHERE PlayerID = @playerid

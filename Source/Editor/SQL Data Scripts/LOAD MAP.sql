﻿/*
Created 9/2/2018 5:18AM
Loads MAP data into database
Created by Steven M Fortune
*/

SELECT ID,
	NAME,
	REVISION,
	UP,
	DOWN,
	LEFTSIDE,
	RIGHTSIDE,
	BRIGHTNESS,
	NPC,
	ITEM,
	GROUND,
	MASK,
	MASKA,
	FRINGE,
	FRINGEA
FROM MAPS
WHERE ID = @id

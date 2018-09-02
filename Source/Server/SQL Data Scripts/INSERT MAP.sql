/*
Created 9/2/2018 5:18AM
Inserts MAP data into database
Created by Steven M Fortune
*/

INSERT INTO MAPS (
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
	)
VALUES (
	@name,
	@revision,
	@top,
	@bottom,
	@left,
	@right,
	@brightness,
	@npc,
	@item,
	@ground,
	@mask,
	@maska,
	@fringe,
	@fringea
	)
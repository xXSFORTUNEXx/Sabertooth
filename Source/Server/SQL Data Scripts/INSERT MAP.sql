/*
Created 9/2/2018 6:58AM
Inserts MAP data into database
Created by Steven M Fortune
Updated 2019-02-07 14:40:57.820
*/

INSERT INTO MAPS (
	NAME,
	REVISION,
	UP,
	DOWN,
	LEFTSIDE,
	RIGHTSIDE,
	BRIGHTNESS,
	MAXX,
	MAXY,
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
	@maxx,
	@maxy,
	@npc,
	@item,
	@ground,
	@mask,
	@maska,
	@fringe,
	@fringea
	)
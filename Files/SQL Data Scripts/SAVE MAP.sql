/*
Created 9/2/2018 6:58AM
Inserts MAP data into database
Created by Steven M Fortune
Updated 2019-02-07 14:40:57.820
*/

UPDATE MAPS
SET NAME = @name,
	REVISION = @revision,
	UP = @top,
	DOWN = @bottom,
	LEFTSIDE = @left,
	RIGHTSIDE = @right,
	BRIGHTNESS = @brightness,
	MAXX = @maxx,
	MAXY = @maxy,
	NPC = @npc,
	ITEM = @item,
	GROUND = @ground,
	MASK = @mask,
	MASKA = @maska, 
	FRINGE = @fringe,
	FRINGEA = @fringea
WHERE ID = @id

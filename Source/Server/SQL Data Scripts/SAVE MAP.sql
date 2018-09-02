/*
Created 9/2/2018 6:58AM
Saves MAP data into database
Created by Steven M Fortune
*/

UPDATE MAPS
SET NAME = @name,
	REVISION = @revision,
	UP = @top,
	DOWN = @bottom,
	LEFTSIDE = @left,
	RIGHTSIDE = @right,
	BRIGHTNESS = @brightness,
	NPC = @npc,
	ITEM = @item,
	GROUND = @ground,
	MASK = @mask,
	MASKA = @maska, 
	FRINGE = @fringe,
	FRINGEA = @fringea
WHERE ID = @id

/*
Created 9/2/2018 6:58AM
Inserts MAP data into database
Created by Steven M Fortune
Updated 2019-02-07 14:40:57.820
*/

UPDATE MAPS
SET Name = @name,
	Revision = @revision,
	Up = @top,
	Down = @bottom,
	Left_Side = @left,
	Right_Side = @right,
	Brightness = @brightness,
	Max_X = @maxx,
	Max_Y = @maxy,
	Npc = @npc,
	--Item = @item,
	Ground = @ground,
	Mask = @mask,
	Mask_A = @maska, 
	Fringe = @fringe,
	Fringe_A = @fringea
WHERE ID = @id

/*
Created 9/2/2018 6:58AM
Inserts MAP data into database
Created by Steven M Fortune
Updated 2019-02-07 14:40:57.820
*/

INSERT INTO Maps (
	Name,
	Revision,
	Up,
	Down,
	Left_Side,
	Right_Side,
	Brightness,
	Max_X,
	Max_Y,
	Npc,
	--Item,
	Ground,
	Mask,
	Mask_A,
	Fringe,
	Fringe_A
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
	--@item,
	@ground,
	@mask,
	@maska,
	@fringe,
	@fringea
	)
/*
Created 9/2/2018 5:18AM
Loads MAP data into database
Created by Steven M Fortune
Updated 2019-02-07 14:40:57.820
*/

SELECT ID,
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
FROM Maps
WHERE ID = @id

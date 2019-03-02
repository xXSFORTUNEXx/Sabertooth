/*
Created 9/2/2018 5:18AM
Load chat data into database
Created by Steven M Fortune
Updated 2019-02-20 15:31:30.060
*/

SELECT ID,
	Name,
	Main_Message,
	Option_A,
	Option_B,
	Option_C,
	Option_D,
	Next_Chat_A,
	Next_Chat_B,
	Next_Chat_C,
	Next_Chat_D,
	Shop_Num,
	Quest_Num,
	Item_A,
	Item_B,
	Item_C,
	Value_A,
	Value_B,
	Value_C,
	Money,
	Type
FROM Chat
WHERE ID = @id

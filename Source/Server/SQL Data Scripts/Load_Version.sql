/*
Created 9/7/2018 3:52PM
load version data into database
Created by Steven M Fortune
*/

SELECT ID,
	Version
FROM Version
WHERE ID = @id

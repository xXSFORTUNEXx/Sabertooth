/*
Created 9/7/2018 3:52PM
updates version into database
Created by Steven M Fortune
*/

UPDATE Version
SET Version = @version
WHERE ID = @id

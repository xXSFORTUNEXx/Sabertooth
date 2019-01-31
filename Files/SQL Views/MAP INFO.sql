/*
Created 2019-01-25 17:07:52.593
Creates MAP INFO view in Sabertooths Database
Created by Steven M Fortune
*/
CREATE VIEW [MAP INFO]
AS
SELECT ID,
	NAME,
	REVISION,
	UP,
	DOWN,
	LEFTSIDE,
	RIGHTSIDE
FROM MAPS

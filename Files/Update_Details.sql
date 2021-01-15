IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Update_Details')
CREATE TABLE Update_Details (
	ID INT IDENTITY (1, 1) PRIMARY KEY,
	Version VARCHAR(5),
	File_Path VARCHAR(255),
	)
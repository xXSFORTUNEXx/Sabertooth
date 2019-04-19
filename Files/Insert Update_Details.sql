declare @version varchar(5)
declare @file_path varchar(255)
set @version = '1.1'
set @file_path = '/Updates/'

INSERT INTO Update_Details (
	Version,
	File_Path
	)
VALUES (
	@version,
	@file_path
	)
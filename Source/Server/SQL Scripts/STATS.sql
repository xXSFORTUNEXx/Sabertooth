﻿/*
Created 9/8/2018 7:55AM
Creates STATS table in Sabertooths Database
Created by Steven M Fortune
Updated 1/5/2019 9:30PM
*/

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'STATS')
	CREATE TABLE STATS (
		ID INT IDENTITY(1, 1) PRIMARY KEY,
		NAME VARCHAR(25) NOT NULL,
		KILLS INTEGER,
		POINTS INTEGER,
		DAYS INTEGER,
		HOURS INTEGER,
		MINUTES INTEGER,
		SECONDS INTEGER,
		LDAYS INTEGER,
		LHOURS INTEGER,
		LMINUTES INTEGER,
		LSECONDS INTEGER,
		LLDAYS INTEGER,
		LLHOURS INTEGER,
		LLMINUTES INTEGER,
		LLSECONDS INTEGER,
		)
CREATE TABLE #NPCS_TEMP (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	NAME TEXT,
	X INTEGER,
	Y INTEGER,
	DIRECTION INTEGER,
	SPRITE INTEGER,
	STEP INTEGER,
	OWNER INTEGER,
	BEHAVIOR INTEGER,
	SPAWNTIME INTEGER,
	HEALTH INTEGER,
	MAXHEALTH INTEGER,
	DAMAGE INTEGER,
	DESX INTEGER,
	DESY INTEGER,
	EXP INTEGER,
	MONEY INTEGER,
	RANGE INTEGER,
	SHOPNUM INTEGER,
	CHATNUM INTEGER
	)

INSERT INTO #NPCS_TEMP
SELECT NAME,
	X,
	Y,
	DIRECTION,
	SPRITE,
	STEP,
	OWNER,
	BEHAVIOR,
	SPAWNTIME,
	HEALTH,
	MAXHEALTH,
	DAMAGE,
	DESX,
	DESY,
	EXP,
	MONEY,
	RANGE,
	SHOPNUM,
	CHATNUM
FROM NPCS
WHERE ID IS NOT NULL

DROP TABLE NPCS

CREATE TABLE NPCS (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	NAME TEXT,
	X INTEGER,
	Y INTEGER,
	DIRECTION INTEGER,
	SPRITE INTEGER,
	STEP INTEGER,
	OWNER INTEGER,
	BEHAVIOR INTEGER,
	SPAWNTIME INTEGER,
	HEALTH INTEGER,
	MAXHEALTH INTEGER,
	DAMAGE INTEGER,
	DESX INTEGER,
	DESY INTEGER,
	EXP INTEGER,
	MONEY INTEGER,
	RANGE INTEGER,
	SHOPNUM INTEGER,
	CHATNUM INTEGER
	)

INSERT INTO NPCS
SELECT NAME,
	X,
	Y,
	DIRECTION,
	SPRITE,
	STEP,
	OWNER,
	BEHAVIOR,
	SPAWNTIME,
	HEALTH,
	MAXHEALTH,
	DAMAGE,
	DESX,
	DESY,
	EXP,
	MONEY,
	RANGE,
	SHOPNUM,
	CHATNUM,
	1000
FROM #NPCS_TEMP
WHERE ID IS NOT NULL

IF (OBJECT_ID('tempdb..#NPCS_TEMP') IS NOT NULL)
BEGIN
	DROP TABLE #NPCS_TEMP
END

SELECT * FROM #NPCS_TEMP
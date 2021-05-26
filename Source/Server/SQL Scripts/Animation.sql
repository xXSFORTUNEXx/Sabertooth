IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Animation')
CREATE TABLE Animation (
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	Name TEXT,
	SpriteNumber INT,
	FrameCountH INT,
	FrameCountV INT,
	FrameCount INT,
	FrameDuration INT,
	LoopCount INT,
	RenderBelowTarget BIT
	)
SELECT ID,
	Name,
	SpriteNumber,
	FrameCountH,
	FrameCountV,
	FrameCount,
	FrameDuration,
	LoopCount,
	RenderBelowTarget
FROM Animation
WHERE ID = @id
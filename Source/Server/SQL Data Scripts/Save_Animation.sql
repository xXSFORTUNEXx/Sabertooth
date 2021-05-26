UPDATE Animation
SET Name = @name,
	SpriteNumber = @spritenumber,
	FrameCountH = @framecounth,
	FrameCountV = @framecountv,
	FrameCount = @framecount,
	FrameDuration = @frameduration,
	LoopCount = @loopcount,
	RenderBelowTarget = @renderbelowtarget
WHERE ID = @id
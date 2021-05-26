INSERT INTO Animation (
	Name,
	SpriteNumber,
	FrameCountH,
	FrameCountV,
	FrameCount,
	FrameDuration,
	LoopCount,
	RenderBelowTarget
) VALUES (
	@name,
	@spritenumber,
	@framecounth,
	@framecountv,
	@framecount,
	@frameduration,
	@loopcount,
	@renderbelowtarget
	)
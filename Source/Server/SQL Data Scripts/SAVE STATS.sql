UPDATE STATS
SET NAME = @name,
	KILLS = @kills,
	POINTS = @points,
	DAYS = @days,
	HOURS = @hours,
	MINUTES = @minutes,
	SECONDS = @seconds,
	LDAYS = @ldays,
	LHOURS = @lhours,
	LMINUTES = @lminutes,
	LSECONDS = @lseconds,
	LLDAYS = @lldays,
	LLHOURS = @llhours,
	LLMINUTES = @llminutes,
	LLSECONDS = @llseconds
WHERE ID = @id

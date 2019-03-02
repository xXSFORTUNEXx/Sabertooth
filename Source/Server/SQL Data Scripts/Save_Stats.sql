UPDATE STATS
SET Name = @name,
	Kills = @kills,
	Points = @points,
	Days = @days,
	Hours = @hours,
	Minutes = @minutes,
	Seconds = @seconds,
	Longest_Days = @ldays,
	Longest_Hours = @lhours,
	Longest_Minutes = @lminutes,
	Longest_Seconds = @lseconds,
	Longest_Life_Days = @lldays,
	Longest_Life_Hours = @llhours,
	Longest_Life_Minutes = @llminutes,
	Longest_Life_Seconds = @llseconds
WHERE ID = @id

UPDATE STATS
SET Name = @name,
	Days = @days,
	Hours = @hours,
	Minutes = @minutes,
	Seconds = @seconds
WHERE ID = @id

SELECT ID,
	Name,
	Kills,
	Points,
	Days,
	Hours,
	Minutes,
	Seconds,
	Longest_Days,
	Longest_Hours,
	Longest_Minutes,
	Longest_Seconds,
	Longest_Life_Days,
	Longest_Life_Hours,
	Longest_Life_Minutes,
	Longest_Life_Seconds
FROM Stats
WHERE Name = @name
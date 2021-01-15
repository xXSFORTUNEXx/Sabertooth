SELECT ID,
	Name,
	Days,
	Hours,
	Minutes,
	Seconds
FROM Stats
WHERE Name = @name
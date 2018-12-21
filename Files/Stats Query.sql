SELECT
PLAYERS.ID,PLAYERS.NAME,PLAYERS.POINTS,
PLAYERS.DAYS,PLAYERS.HOURS,PLAYERS.MINUTES,PLAYERS.SECONDS,
PLAYERS.LDAYS,PLAYERS.LHOURS,PLAYERS.LMINUTES,PLAYERS.LSECONDS,
PLAYERS.LLDAYS,PLAYERS.LLHOURS,PLAYERS.LLMINUTES,PLAYERS.LLSECONDS
FROM PLAYERS
ORDER BY PLAYERS.POINTS DESC

SELECT
STATS.ID,STATS.NAME,STATS.POINTS,
STATS.DAYS,STATS.HOURS,STATS.MINUTES,STATS.SECONDS,
STATS.LDAYS,STATS.LHOURS,STATS.LMINUTES,STATS.LSECONDS,
STATS.LLDAYS,STATS.LLHOURS,STATS.LLMINUTES,STATS.LLSECONDS
from STATS
ORDER BY STATS.POINTS DESC
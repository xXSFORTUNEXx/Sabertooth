select
PLAYERS.ID, 
PLAYERS.NAME, 
MAINWEAPONS.ID, 
MAINWEAPONS.OWNER, 
MAINWEAPONS.NAME
from 
PLAYERS 
JOIN 
MAINWEAPONS 
on 
PLAYERS.ID = MAINWEAPONS.ID 
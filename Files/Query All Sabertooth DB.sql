--*Query All*
select * from BANK
select * from CHAT
select * from CHESTS
select * from EQUIPMENT
select * from INVENTORY
select * from ITEMS
select * from MAINWEAPONS
select * from MAPS
select * from NPCS
select * from PLAYERS
select * from PROJECTILES
select * from SECONDARYWEAPONS
select * from SHOPS
select * from STATS

--*Query Player Tables*
select * from BANK
select * from EQUIPMENT
select * from INVENTORY
select * from MAINWEAPONS
select * from SECONDARYWEAPONS
select * from PLAYERS
select * from STATS

--*Query Game Content(All)*
select * from CHAT
select * from CHESTS
select * from ITEMS
select * from MAPS
select * from NPCS
select * from PROJECTILES
select * from SHOPS

SELECT COUNT(*) AS INVCOUNT FROM INVENTORY WHERE OWNER='sfortune'
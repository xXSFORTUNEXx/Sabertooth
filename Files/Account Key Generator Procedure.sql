CREATE PROCEDURE GenerateAccountKey @id INTEGER, @email varchar(255)
AS
DECLARE @Length INTEGER
DECLARE @LoopCount INTEGER
DECLARE @CharPool as varchar(255)
DECLARE @PoolLength as varchar(255)
DECLARE @RandomString as varchar(25)

SET @Length = 25

SET @CharPool = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789'
SET @PoolLength = Len(@CharPool)

SET @LoopCount = 0
SET @RandomString = ''

WHILE (@LoopCount <= @Length) BEGIN
    SELECT @RandomString = @RandomString + 
        SUBSTRING(@Charpool, CONVERT(int, RAND() * @PoolLength), 1)
    SELECT @LoopCount = @LoopCount + 1
END

UPDATE PLAYERS SET EMAILADDRESS=@email, ACCOUNTKEY=@RandomString, ACTIVE='Y' WHERE ID=@id

SELECT ID, NAME, EMAILADDRESS, ACCOUNTKEY,LEN(@RandomString) as ACCOUNT_KEY_TOTAL, ACTIVE FROM PLAYERS where ID=@id
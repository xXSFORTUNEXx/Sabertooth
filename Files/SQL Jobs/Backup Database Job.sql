DECLARE @SabertoothBackup VARCHAR(1000)

SELECT @SabertoothBackup = (SELECT 'E:\Backup\Sabertooth DB Backup ' + CONVERT(VARCHAR(500), GETDATE(), 112) + '.bak')

BACKUP DATABASE [Sabertooth] 
TO DISK = @SabertoothBackup
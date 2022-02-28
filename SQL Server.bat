@echo off
sc stop "MSSQL$SFORTUNESQL"
if ERRORLEVEL = 1 goto error
echo Service stopped
timeout /T 5
exit
:error
echo Starting Service
sc start "MSSQL$SFORTUNESQL"
timeout /T 5
exit
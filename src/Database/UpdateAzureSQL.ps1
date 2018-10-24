#
# UpdateAzureSQL.ps1
#
$DatabaseServer = $env:DatabaseServer
$DatabaseName = $env:DatabaseName
$DatabaseAction = $env:DatabaseAction
$DatabaseUser = $env:DatabaseUser
$DatabasePassword = $env:DatabasePassword

Write-Host "Executing & .\scripts\AliaSQL.exe $DatabaseAction $databaseServer $databaseName .\scripts $databaseUser $databasePassword"

& .\scripts\AliaSQL.exe $DatabaseAction $DatabaseServer $DatabaseName .\scripts $DatabaseUser $DatabasePassword
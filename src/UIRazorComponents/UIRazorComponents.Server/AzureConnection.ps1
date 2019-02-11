#
# AzureConnection.ps1
#
$path = $PSScriptRoot
$configFile = "$path\appsettings.json"
$databaseName = $env:DatabaseName
$databaseServer = $env:DatabaseServer
$databaseUser = $env:DatabaseUser
$databasePassword = $env:DatabasePassword
$connection_string = "Server=tcp:$databaseServer,1433;Initial Catalog=$databaseName;Persist Security Info=False;User ID=$databaseUser;Password=$databasePassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

$pathToJson = $configFile
$a = Get-Content $pathToJson | ConvertFrom-Json
$a.ConnectionStrings.DefaultConnection = $connection_string
$a | ConvertTo-Json | set-content $pathToJson

Write-Host "DatabaseServer: $DatabaseServer"
Write-Host "DatabaseName: $DatabaseName"
Write-Host "-----------------------"
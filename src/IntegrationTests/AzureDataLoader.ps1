#
# AzureDataLoader.ps1
#
$path = $PSScriptRoot
$configFile = "$path\ConnectionStrings.config"
$databaseName = $env:DatabaseName
$databaseServer = $env:DatabaseServer
$databaseUser = $env:DatabaseUser
$databasePassword = $env:DatabasePassword
$connection_string = "Server=tcp:$databaseServer,1433;Initial Catalog=$databaseName;Persist Security Info=False;User ID=$databaseUser;Password=$databasePassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

$xpath = "//add[@name='Bootcamp']/@connectionString"
$filePath = Resolve-Path $configFile
$value = $connection_string

[xml] $fileXml = Get-Content $filePath
$node = $fileXml.SelectSingleNode($xpath)
        
if($node.NodeType -eq "Element") {
    $node.InnerText = $value
} else {
    $node.Value = $value
}

$fileXml.Save($filePath) 

Write-Host "DatabaseServer: $DatabaseServer"
Write-Host "DatabaseName: $DatabaseName"
Write-Host "-----------------------"

$nunitPath = ".\NUnit.Console.3.0.1\tools"
& $nunitPath\nunit3-console.exe .\ClearMeasure.Bootcamp.IntegrationTests.dll --where "cat == DataLoader" --noheader 
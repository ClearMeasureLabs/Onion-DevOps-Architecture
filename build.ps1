. .\BuildFunctions.ps1

$projectName = "OnionDevOpsArchitecture"
$base_dir = resolve-path .\
$source_dir = "$base_dir\src"
$unitTestProjectPath = "$source_dir\UnitTests"
$projectConfig = $env:BuildConfiguration
$version = $env:Version
$verbosity = "n"

$build_dir = "$base_dir\build"
$test_dir = "$build_dir\test"

$aliaSql = "$source_dir\Database\scripts\AliaSql.exe"
$databaseAction = $env:DatabaseAction
if ([string]::IsNullOrEmpty($databaseAction)) { $databaseAction = "Rebuild"}
$databaseName = $env:DatabaseName
if ([string]::IsNullOrEmpty($databaseName)) { $databaseName = $projectName}
$databaseServer = $env:DatabaseServer
if ([string]::IsNullOrEmpty($databaseServer)) { $databaseServer = "localhost\SQL2017"}
$databaseScripts = "$source_dir\Database\scripts"
    
if ([string]::IsNullOrEmpty($version)) { $version = "1.0.1"}
if ([string]::IsNullOrEmpty($projectConfig)) {$projectConfig = "Release"}
 
Function Init {
    rd $build_dir -recurse -force  -ErrorAction Ignore
	md $build_dir > $null

	exec {
		& dotnet clean $source_dir\$projectName.sln -nologo -v $verbosity
		}
	exec {
		& dotnet restore $source_dir\$projectName.sln -nologo --interactive -v $verbosity  
		}
    

    Write-Host $projectConfig
    Write-Host $version
}


Function Compile{
	exec {
		& dotnet build $source_dir\$projectName.sln -nologo --no-restore -v $verbosity -maxcpucount --configuration $projectConfig --no-incremental /p:Version=$version /p:Authors="Clear Measure" /p:Product="Onion DevOps Architecture"
	}
}

Function Test{
	Push-Location -Path $unitTestProjectPath

	try {
		exec {
			& dotnet test -nologo -v $verbosity --logger:trx --results-directory $test_dir --no-build --no-restore --configuration $projectConfig
		}
	}
	finally {
		Pop-Location
	}
}

Function MigrateDatabase {
	exec{
		& $aliaSql $databaseAction $databaseServer $databaseName $databaseScripts
	}
}

Init
Compile
Test
MigrateDatabase
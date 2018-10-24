. .\BuildFunctions.ps1

$projectName = "OnionDevOpsArchitecture"
$base_dir = resolve-path .\
$source_dir = "$base_dir\src"
$unitTestProjectPath = "$source_dir\UnitTests"
$integrationTestProjectPath = "$source_dir\IntegrationTests"
$projectConfig = $env:BuildConfiguration
$version = $env:Version
$verbosity = "m"

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

Function IntegrationTest{
	Push-Location -Path $integrationTestProjectPath

	try {
		exec {
			& dotnet test -nologo -v $verbosity --logger:trx --results-directory $test_dir --no-build --no-restore --configuration $projectConfig
		}
	}
	finally {
		Pop-Location
	}
}

Function MigrateDatabaseLocal {
	exec{
		& $aliaSql $databaseAction $databaseServer $databaseName $databaseScripts
	}
}

Function MigrateDatabaseRemote{
	$efConfig = "$source_dir\ConnectionStrings.config"
    $injectedConnectionString = "Server=tcp:$databaseServer,1433;Initial Catalog=$databaseName;Persist Security Info=False;User ID=$databaseUser;Password=$databasePassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
    write-host "Using connection string: $injectedConnectionString"
    if ( Test-Path "$efConfig" ) {
        poke-xml $efConfig "//add[@name='Database']/@connectionString" $injectedConnectionString
    }

	exec {
		& $aliaSql $databaseAction $databaseServer $databaseName $databaseScripts $env:DatabaseUser $env:DatabasePassword
	}
}

Function PrivateBuild{
	Init
	Compile
	Test
	MigrateDatabaseLocal
	IntegrationTest
}

Function CIBuild{
	Init
	MigrateDatabaseRemote
	Compile
	Test
	IntegrationTest
}

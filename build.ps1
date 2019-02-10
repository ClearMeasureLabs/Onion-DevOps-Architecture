. .\BuildFunctions.ps1
$startTime = 
$projectName = "OnionDevOpsArchitecture"
$base_dir = resolve-path .\
$source_dir = "$base_dir\src"
$unitTestProjectPath = "$source_dir\UnitTests"
$integrationTestProjectPath = "$source_dir\IntegrationTests"
$uiProjectPath = "$source_dir\UI"
$databaseProjectPath = "$source_dir\Database"
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
    
if ([string]::IsNullOrEmpty($version)) { $version = "9.9.9"}
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

Function UnitTests{
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
	$appConfig = "$integrationTestProjectPath\app.config"
    $injectedConnectionString = "Server=tcp:$databaseServer,1433;Initial Catalog=$databaseName;Persist Security Info=False;User ID=$env:DatabaseUser;Password=$env:DatabasePassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
    
	write-host "Using connection string: $injectedConnectionString"
    if ( Test-Path "$appConfig" ) {
        poke-xml $appConfig "//add[@key='ConnectionString']/@value" $injectedConnectionString
    }

	exec {
		& $aliaSql $databaseAction $databaseServer $databaseName $databaseScripts $env:DatabaseUser $env:DatabasePassword
	}
}

Function Pack{
	Write-Output "Packaging nuget packages"
	exec{
		& .\tools\octopack\Octo.exe pack --id "$projectName.UI" --version $version --basePath $uiProjectPath --outFolder $build_dir
	}
	exec{
		& .\tools\octopack\Octo.exe pack --id "$projectName.Database" --version $version --basePath $databaseProjectPath --outFolder $build_dir
	}
	exec{
		& .\tools\octopack\Octo.exe pack --id "$projectName.IntegrationTests" --version $version --basePath $integrationTestProjectPath --outFolder $build_dir
	}
}

Function PrivateBuild{
	$sw = [Diagnostics.Stopwatch]::StartNew()
	Init
	Compile
	UnitTests
	MigrateDatabaseLocal
	IntegrationTest
	$sw.Stop()
	write-host "Build time: " $sw.Elapsed.ToString()
}

Function CIBuild{
	Init
	MigrateDatabaseRemote
	Compile
	UnitTests
	IntegrationTest
	Pack
}

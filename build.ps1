. .\BuildFunctions.ps1

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
if ([string]::IsNullOrEmpty($databaseServer)) { $databaseServer = "."}
$databaseScripts = "$source_dir\Database\scripts"

# databases must be applied in this order due to cross-database references.
$orderToApplyDatabases = "OnionDevOpsArchitecture"##,"Database2","Database3","etc"


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

Function ApplyLocalDb {
    $orderToApplyDatabases | ForEach-Object {
        Write-Host "Applying database: $_"
        ApplyLocalDatabase($_)
    }
}

Function DropLocalDb {
    $orderToApplyDatabases | ForEach-Object {
        Write-Host "Dropping database: $_"
        SqlCmd -S "$databaseServer" -Q "Drop Database [$_]"
    }
}

Function BuildDatabaseArtifact($databaseServer, $dbName) {
    $validatedProject = Invoke-DatabaseBuild "$base_dir\src\Database.$dbName\Database.$dbName.sqlproj" -TemporaryDatabase $databaseServer
    $buildArtifact = New-DatabaseBuildArtifact $validatedProject -PackageId "Database.$dbName" -PackageVersion $version
    Export-DatabaseBuildArtifact $buildArtifact -Path $build_dir -Force # "-Force" because msbuild creates an incomplete version of this file with the same name and without the nuspec file that causes that, msbuild crashes.
    return $buildArtifact
}

Function ApplyLocalDatabase($dbName) {
    $targetDb = New-DatabaseConnection -ServerInstance "$databaseServer" -Database $dbName
    $buildArtifact = BuildDatabaseArtifact $targetDb $dbName

    $dbRelease = New-DatabaseReleaseArtifact -Source $buildArtifact -Target $targetDb
    Use-DatabaseReleaseArtifact $dbRelease -DeployTo $targetDb
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
	Init
	Compile
	UnitTests
	ApplyLocalDb
	IntegrationTest
}

Function CIBuild{
	Init
	MigrateDatabaseRemote
	Compile
	UnitTests
	IntegrationTest
	Pack
}

Function FreshDB {
	DropLocalDb
	ApplyLocalDb
}

Function ApplyDB {
	ApplyLocalDb
}

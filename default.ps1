Framework "4.6"

properties {
    $projectName = "OnionDevOpsArchitecture"
    $base_dir = resolve-path .\
    $source_dir = "$base_dir\src"
    $unitTestProjectPath = "$source_dir\UnitTests\UnitTests.csproj"
	$projectConfig = $env:Configuration
    $version = $env:Version
	$verbosity = "m"

    $build_dir = "$base_dir\build"
	$test_dir = "$build_dir\test"
    
    if ([string]::IsNullOrEmpty($version)) { $version = "1.0.0"}
    if ([string]::IsNullOrEmpty($projectConfig)) {$projectConfig = "Release"}
 }

task default -depends Init, Compile, Test

task Init {
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

task Compile -depends Init {
	exec {
		& dotnet build $source_dir\$projectName.sln -nologo --no-restore -v $verbosity -maxcpucount --configuration $projectConfig --no-incremental --output $build_dir
	}
}

task Test -depends Compile {
	exec {
		& dotnet test $unitTestProjectPath -nologo -v $verbosity --logger:trx --results-directory $test_dir --no-build --no-restore --configuration $projectConfig
		}
    
}  
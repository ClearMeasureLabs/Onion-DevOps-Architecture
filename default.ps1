Framework "4.6"

properties {
    $projectName = "OnionDevOpsArchitecture"
    $base_dir = resolve-path .\
    $source_dir = "$base_dir\src"
    $unitTestProjectPath = "$source_dir\UnitTests\UnitTests.csproj"
	$testCopyIgnorePath = ".vs"
	$projectConfig = $env:Configuration
    $version = $env:Version
    $nunitPath = Resolve-Path("$source_dir\packages\nunit.consolerunner\*\tools\")
	$verbosity = "m"

    $build_dir = "$base_dir\build"
    
    if ([string]::IsNullOrEmpty($version)) { $version = "1.0.0"}
    if ([string]::IsNullOrEmpty($projectConfig)) {$projectConfig = "Release"}
 }

task default -depends Init, Compile, Test

task Init {
    rd $build_dir -recurse -force  -ErrorAction Ignore
	md $build_dir > nil

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
		& dotnet build $source_dir\$projectName.sln -nologo -restore:False -v $verbosity -maxcpucount -p:Configuration=$projectConfig 
	}
}

task Test -depends Compile {
	exec {
		& dotnet test $unitTestProjectPath -nologo -v $verbosity --logger:trx --results-directory $build_dir --no-build -p:Configuration=$projectConfig 
		}
    
}  
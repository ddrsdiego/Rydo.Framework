param(
    [string]$packageVersion = "1.0.0", #$null,
    [string]$config = "Debug",
    #[string[]]$targetFrameworks = @("v4.0", "v4.5", "v4.5.1", "v4.6.1"),
    [string[]]$targetFrameworks = @("v4.6.1"),
    [string[]]$platforms = @("AnyCpu"),
    [ValidateSet("rebuild", "build")]
    [string]$target = "rebuild",
    [ValidateSet("quiet", "minimal", "normal", "detailed", "diagnostic")]
    [string]$verbosity = "detailed",
    [bool]$alwaysClean = $true
)

# Diagnostic 
function Write-Diagnostic {
    param([string]$message)

    Write-Host
    Write-Host $message -ForegroundColor Green
    Write-Host
}

function Die([string]$message, [object[]]$output) {
    if ($output) {
        Write-Output $output
        $message += ". See output above."
    }
    Write-Error $message
    exit 1
}

function Create-Folder-Safe {
    param(
        [string]$folder = $(throw "-folder is required.")
    )

    if(-not (Test-Path $folder)) {
        [System.IO.Directory]::CreateDirectory($folder)
    }
}

# Build
function Build-Clean {
    param(
        [string]$rootFolder = $(throw "-rootFolder is required."),
        [string]$folders = "bin,obj"
    )

    Write-Diagnostic "Build: Clean"

    Get-ChildItem $rootFolder -Include $folders -Recurse | ForEach-Object {
       Remove-Item $_.fullname -Force -Recurse 
    }
}

function Build-Bootstrap {
    param(
        [string]$solutionFile = $(throw "-solutionFile is required."),
        [string]$nugetExe = $(throw "-nugetExe is required.")
    )

    Write-Diagnostic "Build: Bootstrap"
 
    $solutionFolder = [System.IO.Path]::GetDirectoryName($solutionFile)

    . $nugetExe config -Set Verbosity=quiet
    . $nugetExe restore $solutionFile -NonInteractive

    Get-ChildItem $solutionFolder -filter packages.config -recurse | 
        Where-Object { -not ($_.PSIsContainer) } | 
        ForEach-Object {

        . $nugetExe restore $_.FullName -NonInteractive -SolutionDirectory $solutionFolder
    }
}

function Build-Nupkg {
    param(
        [string]$rootFolder = $(throw "-rootFolder is required."),
        [string]$project = $(throw "-project is required."),
        [string]$nugetExe = $(throw "-nugetExe is required."),
        [string]$outputFolder = $(throw "-outputFolder is required."),
        [string]$config = $(throw "-config is required."),
        [string]$version = $(throw "-version is required."),
        [string]$platform = $(throw "-platform is required.")
    )

    Write-Diagnostic "Creating nuget package for platform $platform"
    
    $platformOutputFolder = Join-Path $outputFolder "$config\$targetFramework"
    $outputFolder = Join-Path $outputFolder "$config"
    
    Create-Folder-Safe -folder $outputFolder
    
    # http://docs.nuget.org/docs/reference/command-line-reference#Pack_Command
    #. $nugetExe pack $project -OutputDirectory $outputFolder -Symbols -NonInteractive -Build -IncludeReferencedProjects `
	. $nugetExe pack $project -OutputDirectory $outputFolder -Symbols -NonInteractive -Build -IncludeReferencedProjects `
        -Properties "Configuration=$config;Bin=$outputFolder;Platform=$platform" #-Version $version

    if($LASTEXITCODE -ne 0) {
        Die("Build failed: $projectName")
    }

    # Support for multiple build runners
    if(Test-Path env:BuildRunner) {
        $buildRunner = Get-Content env:BuildRunner

        switch -Wildcard ($buildRunner.ToString().ToLower()) {
            "myget" {

                $mygetBuildFolder = Join-Path $rootFolder "Build"

                Create-Folder-Safe -folder $mygetBuildFolder

                Get-ChildItem $outputFolder -filter *.nupkg | 
                Where-Object { -not ($_.PSIsContainer) } | 
                ForEach-Object {
                    $fullpath = $_.FullName
                    $filename = $_.Name

                    cp $fullpath $mygetBuildFolder\$filename
                }
            }
        }
    }
}

function Build-Project {
    param(
        [string]$project = $(throw "-project is required."),
        [string]$outputFolder = $(throw "-outputFolder is required."),
        [string]$nugetExe = $(throw "-nugetExe is required."),
        [string]$config = $(throw "-config is required."),
        [string]$target = $(throw "-target is required."),
        [string[]]$targetFrameworks = $(throw "-targetFrameworks is required."),
        [string[]]$platform = $(throw "-platform is required.")
    )

    $projectPath = [System.IO.Path]::GetFullPath($project)
    $projectName = [System.IO.Path]::GetFileName($projectPath) -ireplace ".csproj$", ""

    Create-Folder-Safe -folder $outputFolder

    if(-not (Test-Path $projectPath)) {
        Die("Could not find csproj: $projectPath")
    }
    
    $targetFrameworks | foreach-object {
        $targetFramework = $_
        $platformOutputFolder = Join-Path $outputFolder "$config\$targetFramework"

        Create-Folder-Safe -folder $platformOutputFolder

        Write-Diagnostic "Build: $projectName ($platform / $config - $targetFramework)"

		& $msbuild `
            $projectPath `
            /p:Configuration=$config `
            /p:OutputPath=$platformOutputFolder `
            /p:TargetFrameworkVersion=$targetFramework `
            /p:Platform=$platform `
            /m `
            /v:M `
            /fl `
            /flp:LogFile=$platformOutputFolder\msbuild.log `
            /nr:false

        if($LASTEXITCODE -ne 0) {
            Die("Build failed: $projectName ($Config - $targetFramework)")
        }
    }
}

function Build-Solution {
    param(
        [string]$rootFolder = $(throw "-rootFolder is required."),
        [string]$solutionFile = $(throw "-solutionFile is required."),
        [string]$outputFolder = $(throw "-outputFolder is required."),
        [string]$packagesFolder = $(throw "-packagesFolder is required."),
        [string]$version = $(throw "-version is required"),
        [string]$config = $(throw "-config is required."),
        [string]$target = $(throw "-target is required."),
        [bool]$alwaysClean = $(throw "-alwaysclean is required"),
        [string[]]$targetFrameworks = $(throw "-targetFrameworks is required."),
        [string[]]$projects = $(throw "-projects is required."),
        [string[]]$platforms = $(throw "-platforms is required.")
    )

    if(-not (Test-Path $solutionFile)) {
        Die("Could not find solution: $solutionFile")
    }

    $solutionFolder = [System.IO.Path]::GetDirectoryName($solutionFile)
    #$nugetExe = if(Test-Path Env:NuGet) { Get-Content env:NuGet } else { Join-Path $rootFolder "\util\nuget\nuget.exe" }

    if($alwaysClean) {
        Build-Clean -root $solutionFolder
    }

    Build-Bootstrap -solutionFile $solutionFile -nugetExe $nugetExe

    $projects | ForEach-Object {

        $project = $_

        $platforms | ForEach-Object {
            $platform = $_
            $buildOutputFolder = Join-Path $outputFolder "$platform"
            $nugetPackagesFolder = Join-Path $packagesFolder "$platform"
            
            Build-Project -rootFolder $solutionFolder -project $project -outputFolder $buildOutputFolder `
                -nugetExe $nugetExe -target $target -config $config `
                -targetFrameworks $targetFrameworks -version $version -platform $platform

            Build-Nupkg -rootFolder $rootFolder -project $project -nugetExe $nugetExe -outputFolder $nugetPackagesFolder `
                -config $config -version $version -platform $platform
        }
    }
}


# Bootstrap

$rootFolder = Split-Path -parent $script:MyInvocation.MyCommand.Definition
$outputFolder = Join-Path $rootFolder "artifacts\bin"
$packagesFolder = Join-Path $rootFolder "artifacts\dist"
$testsFolder = Join-Path $outputFolder "tests"
$config = $config.substring(0, 1).toupper() + $config.substring(1)
$version = $config.trim()
$currentVersion =  $packageVersion
#$msbuild = "$(Get-Content env:windir)\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe"
$msbuild = "MsBuild.exe"
$nugetExe = "Nuget.exe"

if($currentVersion -eq "") {
    Die("Package version cannot be empty")
}

# Build
Build-Solution -solutionFile $rootFolder\Rydo.Framework.sln `
    -projects @( `
		"$rootFolder\Rydo.Framework.MediatR.csproj"
    ) `
    -rootFolder $rootFolder `
    -outputFolder $outputFolder `
    -packagesFolder $packagesFolder `
    -platforms $platforms `
    -version $currentVersion `
    -config $config `
    -target $target `
    -targetFrameworks $targetFrameworks `
    -alwaysClean $alwaysClean
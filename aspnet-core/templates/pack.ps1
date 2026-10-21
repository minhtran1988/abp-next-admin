# Clean up previous builds
if (Test-Path -Path "./aio/LocalNuget") {
    Remove-Item -Path "./aio/LocalNuget/*" -Recurse -Force
}
else {
    New-Item -ItemType Directory -Path "./aio/LocalNuget"
}

if (Test-Path -Path "./micro/LocalNuget") {
    Remove-Item -Path "./micro/LocalNuget/*" -Recurse -Force
}
else {
    New-Item -ItemType Directory -Path "./micro/LocalNuget"
}

# Show selection menu
Write-Host "Please select the template to be packaged:"
Write-Host "1. Microservice Template (PackageName.CompanyName.ProjectName)"
Write-Host "2. AllInOne Template (PackageName.CompanyName.ProjectName.AIO)"
Write-Host "3. Package all"

$choice = Read-Host "Please enter options (1-3)"

switch ($choice) {
    "1" {
        Write-Host "Packaging microservice template..."
        dotnet pack ./micro/PackageName.CompanyName.ProjectName.csproj -c Release -o ./micro/LocalNuget --nologo -p:NoDefaultExcludes=true
    }
    "2" {
        Write-Host "Packaging AllInOne template..."
        dotnet pack ./aio/PackageName.CompanyName.ProjectName.AIO.csproj -c Release -o ./aio/LocalNuget --nologo -p:NoDefaultExcludes=true
    }
    "3" {
        Write-Host "Packaging all templates..."
        dotnet pack ./micro/PackageName.CompanyName.ProjectName.csproj -c Release -o ./micro/LocalNuget --nologo -p:NoDefaultExcludes=true
        dotnet pack ./aio/PackageName.CompanyName.ProjectName.AIO.csproj -c Release -o ./aio/LocalNuget --nologo -p:NoDefaultExcludes=true
    }
    default {
        Write-Host "Invalid option, exit script"
        exit 1
    }
}

# Ask if you want to publish to NuGet server
$publishChoice = Read-Host "Do you want to publish to NuGet server? (Y/N)"

if ($publishChoice -eq "Y" -or $publishChoice -eq "y") {
    # Publish the corresponding package based on the previous selection
    switch ($choice) {
        "1" {
            $packages = Get-ChildItem -Path "./micro/LocalNuget/*.nupkg"
        }
        "2" {
            $packages = Get-ChildItem -Path "./aio/LocalNuget/*.nupkg"
        }
        "3" {
            $packages = @()
            $packages += Get-ChildItem -Path "./micro/LocalNuget/*.nupkg"
            $packages += Get-ChildItem -Path "./aio/LocalNuget/*.nupkg"
        }
    }
    
    foreach ($package in $packages) {
        Write-Host "Publishing package: $($package.Name)"
        dotnet nuget push $package.FullName --source "https://custom.nuget.net/nuget/abp/v3/index.json" --api-key "" --skip-duplicate
    }
    Write-Host "Publish completed!"
}
else {
    Write-Host "Skip publishing step."
    if ($choice -eq "1") {
        Write-Host "The package file has been saved in the ./micro/LocalNuget directory."
    }
    elseif ($choice -eq "2") {
        Write-Host "The package file has been saved in the ./aio/LocalNuget directory."
    }
    else {
        Write-Host "Package files have been saved in the ./micro/LocalNuget and ./aio/LocalNuget directories."
    }
}

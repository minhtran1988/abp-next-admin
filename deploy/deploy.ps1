. "../build/build-aspnetcore-common.ps1"

Write-host "Start deploying the container."

$rootFolder = (Get-Item -Path "../" -Verbose).FullName
$deployPath = $rootFolder + "/deploy";
$buildPath = $rootFolder + "/build";
$aspnetcorePath = $rootFolder + "/aspnet-core";
$vuePath = $rootFolder + "/apps/vben5";

Write-host "root: " + $rootFolder

## Deploy middleware
Write-host "deploy middleware..."
Set-Location $rootFolder
docker-compose -f .\docker-compose.middleware.yml -p labp up -d --build

## Wait 30 seconds for database initialization to complete
Write-host "initial database..."
Start-Sleep -Seconds 30
## Create database
# Write-host "create database..."
# Set-Location $aspnetcorePath
# cmd.exe /c create-database.bat

## Perform database migration
Write-host "migrate database..."
Set-Location $rootFolder
foreach ($solution in $migrationArray) {  
    Set-Location $solution.Path
    dotnet run --no-build
}

## Build the backend docker image
Write-host "publish backend docker image..."
Set-Location $rootFolder
foreach ($docker in $dockerArray) {    
    $image = $docker.Image + ":" + $docker.Version
    Write-host "docker build -f " $docker.Dockerfile " -t " $image " ."
    docker build -f $docker.Dockerfile -t $image .
}
## Run the backend application
Write-host "running backend application..."
Set-Location $rootFolder
docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml -f .\docker-compose.override.configuration.yml -p labp up -d

## Build the front-end project
Write-host "build front project..."
Set-Location $vuePath
Start-Process pnpm -ArgumentList "dev:app" -NoNewWindow

Set-Location $deployPath
Write-host "application is running..."

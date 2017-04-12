# run restore on all project.json files in the src folder including 2>1 to redirect stderr to stdout for badly behaved tools
Get-ChildItem -Path $PSScriptRoot\..\src -Filter *.csproj -Recurse | ForEach-Object { & dotnet restore $_.FullName 2>1 }

# run pack on all project.json files in the src folder including 2>1 to redirect stderr to stdout for badly behaved tools
Get-ChildItem -Path $PSScriptRoot\..\src -Filter *.csproj -Recurse | ForEach-Object { & dotnet build $_.FullName -c Release 2>1 }
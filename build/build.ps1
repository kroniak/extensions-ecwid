param (
	[Parameter(Mandatory=$true)]
	[string]
	$ReleaseVersionNumber
)
# Set the version number in package.json
$ProjectJsonPath = Join-Path -Path $PSScriptRoot\..\ -ChildPath "src\Ecwid\project.json"
(gc -Path $ProjectJsonPath) `
	-replace "(?<=`"version`":\s`")[.\w-]*(?=`",)", "$ReleaseVersionNumber" |
	sc -Path $ProjectJsonPath -Encoding UTF8

# run DNU restore on all project.json files in the src folder including 2>1 to redirect stderr to stdout for badly behaved tools
Get-ChildItem -Path $PSScriptRoot\..\src -Filter project.json -Recurse | ForEach-Object { & dnu restore $_.FullName 2>1 }

# run DNU build on all project.json files in the src folder including 2>1 to redirect stderr to stdout for badly behaved tools
Get-ChildItem -Path $PSScriptRoot\..\src -Filter project.json -Recurse | ForEach-Object { & dnu build $_.FullName 2>1 }

# run DNU build Frontend
& dnu pack $PSScriptRoot\..\src\Ecwid\ --configuration Release --out $PSScriptRoot\..\bin\
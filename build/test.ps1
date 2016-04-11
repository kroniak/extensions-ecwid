# run DNU restore on all project.json files in the src folder including 2>1 to redirect stderr to stdout for badly behaved tools
Get-ChildItem -Path $PSScriptRoot\..\test -Filter project.json -Recurse | ForEach-Object { & dnu restore $_.FullName 2>1 }

# run DNU build on all project.json files in the src folder including 2>1 to redirect stderr to stdout for badly behaved tools
Get-ChildItem -Path $PSScriptRoot\..\test -Filter project.json -Recurse | ForEach-Object { & dnu build $_.FullName 2>1 }

# run tests
& dnx -p $PSScriptRoot\..\test\Ecwid.Test.Dnx\ test 2>1
@call dotnet restore -v m ..\

@if ERRORLEVEL 1 (
echo Error! Restoring dependencies failed.
exit /b 1
) else (
echo Restoring dependencies was successful.
)

@set project=..\src\Ecwid\

@call dotnet build -c Release %project%

@if ERRORLEVEL 1 (
echo Error! Build Ecwid failed.
exit /b 1
)

@set project=..\src\Ecwid.Legacy\

@call dotnet build -c Release %project%

@if ERRORLEVEL 1 (
echo Error! Build Ecwid.Legacy failed.
exit /b 1
)

@set project=..\src\Ecwid.OAuth\

@call dotnet build -c Release %project%

@if ERRORLEVEL 1 (
echo Error! Build Ecwid.OAuth failed.
exit /b 1
)
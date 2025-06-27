@echo off
color 02
cls
echo Runs test projects with code coverage.
echo.
echo Created by Oscar Fernandez Gonzalez a.k.a. Osc@rNET  (OscarNET-SOFTware)
echo ------------------------------------------------------------------------
echo.

setlocal enabledelayedexpansion
set _ScriptsDir=%~dp0
set _RepoDir=%_ScriptsDir:scripts\=%
set _SourceDir=%_RepoDir%src\
set _TestsDir=%_RepoDir%tests\
set _TestsResultsDir=%_RepoDir%testsresults\
set _TestsResultsCoverageDir=%_TestsResultsDir%coverage\
set _TestsResultsHistoryDir=%_TestsResultsDir%history\
set _TestsResultsOutputsDir=%_TestsResultsDir%outputs\
set _DotNetConfiguration=Debug
set _DotNetFramework=net8.0
set _DotNetRuntime=win-x64
set _ReportGeneratorPath=%UserProfile%\.nuget\packages\reportgenerator\5.4.8\tools\net8.0\ReportGenerator.dll

:CREATE_TEST_RESULTS_FOLDERS_IF_APPLICABLE
if not exist %_TestsResultsDir% mkdir %_TestsResultsDir%
if not exist %_TestsResultsHistoryDir% mkdir %_TestsResultsHistoryDir%

:DELETE_OLD_TEST_RESULTS
echo.
echo DELETING OLD RESULTS . . .
echo.
if exist %_TestsResultsCoverageDir% rmdir /s /q %_TestsResultsCoverageDir%
if exist %_TestsResultsOutputsDir% rmdir /s /q %_TestsResultsOutputsDir%

:RUN_TESTS
echo.
echo TESTING THE FOLLOWING PROJECTS:
echo.
for /f "tokens=*" %%a in ('dir %_TestsDir%\*.tests.csproj /s /b /o:n /a-d') do (
    echo    [x]   %%~nxa
    dotnet test %%a ^
        --configuration %_DotNetConfiguration% ^
        --collect "XPlat Code Coverage" ^
        --framework %_DotNetFramework% ^
        --results-directory %_TestsResultsOutputsDir% ^
        --runtime %_DotNetRuntime% ^
        --verbosity quiet ^
        -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.DeterministicReport=true ^
        -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.ExcludeByAttribute=Obsolete,GeneratedCodeAttribute,CompilerGeneratedAttribute ^
        -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=cobertura >nul
)

:GENERATE_CODE_COVERAGE_REPORT
echo.
echo GENERATING THE CODE COVERAGE REPORT . . .
echo.
dotnet %_ReportGeneratorPath% ^
    -reports:%_TestsResultsOutputsDir%**\coverage.cobertura.xml ^
    -sourcedirs:%_SourceDir% ^
    -targetdir:%_TestsResultsCoverageDir% ^
    -historydir:%_TestsResultsHistoryDir% ^
    -reporttypes:HTML;HTMLSummary;Cobertura ^
    -title:Mondongo.Dehesa.Framework ^
    -verbosity:Error

:OPEN_CODE_COVERAGE_REPORT_IF_APPLICABLE
echo.
if exist %_TestsResultsCoverageDir%index.html (
    echo OPENING THE CODE COVERAGE REPORT USING DEFAULT WEB BROWSER . . .
    start %_TestsResultsCoverageDir%index.html
) else (
    echo The code coverage report could not be generated.
)
echo.

:END
endlocal
set _DotNetConfiguration=
set _DotNetFramework=
set _DotNetRuntime=
set _RepoDir=
set _ReportGeneratorPath=
set _ScriptsDir=
set _SourceDir=
set _TestsDir=
set _TestsResultsCoverageDir=
set _TestsResultsDir=
set _TestsResultsHistoryDir=
set _TestsResultsOutputsDir=
echo.
echo Press any key to end . . .
pause > nul
color
cls

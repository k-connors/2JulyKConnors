if not exist "%~dp0GeneratedReports" mkdir "%~dp0GeneratedReports"
 
REM Remove any previous test execution files to prevent issues overwriting
IF EXIST "%~dp0LawnMowersService.trx" del "%~dp0LawnMowersService.trx%"
 
REM Remove any previously created test output directories
CD %~dp0
FOR /D /R %%X IN (%USERNAME%*) DO RD /S /Q "%%X"

call :RunOpenCoverUnitTestMetrics

if %errorlevel% equ 0 (
 call :CoverallsGo
)

REM Remove any previously created test output directories
CD %~dp0
FOR /D /R %%X IN (%USERNAME%*) DO RD /S /Q "%%X"

exit /b %errorlevel%

:RunOpenCoverUnitTestMetrics
"%~dp0\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe" ^
-register:user ^
-targetargs:"/testcontainer:\"%~dp0\LawnMowers.Test\bin\Debug\LawnMowers.Test.dll\" /resultsfile:\"%~dp0LawnMowersService.trx\"" ^
-target:"%VS140COMNTOOLS%\..\IDE\mstest.exe" ^
-filter:"+[LawnMowers.Service]*" ^
-mergebyhash ^
-skipautoprops ^
-output:"%~dp0\GeneratedReports\LawnMowersServiceReport.xml"
exit /b %errorlevel%

:RunReportGeneratorOutput
"%~dp0\packages\ReportGenerator.2.4.5.0\tools\ReportGenerator.exe" ^
-reports:"%~dp0\GeneratedReports\LawnMowersServiceReport.xml" ^
-targetdir:"%~dp0\GeneratedReports\ReportGenerator Output"
exit /b %errorlevel%

:RunLaunchReport
start "report" "%~dp0\GeneratedReports\ReportGenerator Output\index.htm"
exit /b %errorlevel%

:CoverallsGo
"%~dp0\packages\coveralls.net.0.7.0\tools\csmacnz.Coveralls.exe" --opencover -i .\GeneratedReports\LawnMowersServiceReport.xml 

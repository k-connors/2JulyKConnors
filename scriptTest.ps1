$openCover = ".\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe"
$targetArgs = "/testcontainer:.\LawnMowers.Test\bin\Debug\LawnMowers.Test.dll"
$target = "%VS140COMNTOOLS%\..\IDE\mstest.exe"
$filter = "+[LawnMowers.Service]*"
$output = "GeneratedReports\LawnMowers.TestReport.xml"

& $openCover -register:user -targetargs:$targetArgs -target:$target -filter:$filter -mergebyhash -skipautoprops -output:$output

$result = $LASTEXITCODE
$coverageFilePath = Resolve-Path -path $output
$coverageFilePath = $coverageFilePath.ToString()
$coveralls = "packages/coveralls.net.0.7.0/tools/csmacnz.coveralls.exe"
& $coveralls --dynamiccodecoverage -i $coverageFilePath --useRelativePaths
if($result -ne 0){
 exit $result
}
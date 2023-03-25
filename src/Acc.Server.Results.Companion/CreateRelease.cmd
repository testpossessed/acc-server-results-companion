@echo off
if [%1] equ [] goto MISSING_VERSION

if [%2] equ [] goto MISSING_PASSWORD

if [%3] equ [] goto MISSING_GITHUB_TOKEN

dotnet publish --output "C:\_src\desktop\acc-server-results-companion\publish" --configuration "Release"

if %errorlevel% neq 0 goto ERROR

csq pack --packId "ACCServerResultsCompanion" --packAuthors "Mike Hanson" --packVersion "%1" --packDir "C:\_src\desktop\acc-server-results-companion\publish" --releaseDir "C:\_src\desktop\acc-server-results-companion\releases" --framework "net6.0.2" --packTitle "ACC Server Results Companion" --splashImage ".\Splash.png" --appIcon ".\Icons\Icon.ico" --signParams "/f C:\_src\CodeSigningCert.pfx /p %2" --signSkipDll 

if %errorlevel% neq 0 goto ERROR

if [%4] neq [] goto DONE

csq github-up --releaseDir "C:\_src\desktop\acc-server-results-companion\releases" --repoUrl "https://github.com/testpossessed/acc-server-results-companion" --token "%3" --releaseName "v%1"

if %errorlevel% neq 0 goto ERROR
goto DONE

:MISSING_VERSION
@echo A version is required e.g. CreateRelease 1.0.0
goto EXIT

:MISSING_PASSWORD
@echo A password for code signing is missing
goto EXIT

:MISSING_GITHUB_TOKEN
@echo A Github PA Token is missing
goto exit

:ERROR
@echo An unexpected error prevented the release from being built, see output for details
goto EXIT

:DONE
@echo Release v%1 created successfully.

:EXIT

@echo off

cd %~dp0..
setlocal enabledelayedexpansion

set LLNET_MODULE_REMOTE_PATH=https://github.com/LiteLDev-NET/Modules.git

@REM rem Process System Proxy
@REM for /f "tokens=3* delims= " %%i in ('Reg query "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings" /v ProxyEnable') do (
@REM     if %%i==0x1 (
@REM         echo [INFO] System Proxy enabled. Adapting Settings...
@REM         for /f "tokens=3* delims= " %%a in ('Reg query "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings" /v ProxyServer') do set PROXY_ADDR=%%a
@REM         set http_proxy=http://!PROXY_ADDR!
@REM         set https_proxy=http://!PROXY_ADDR!
@REM         echo [INFO] System Proxy enabled. Adapting Settings finished.
@REM         echo.
@REM     )
@REM )


echo [INFO] Fetching InteropServices to GitHub ...
echo.

for /f "delims=" %%i in ('git rev-parse --abbrev-ref HEAD') do set LLNET_MODULE_NOW_BRANCH=%%i
for /f "delims=" %%i in ('git describe --tags --always') do set LLNET_MODULE_NOW_TAG_LONG=%%i
for /f "delims=-" %%i in ('git describe --tags --always') do set LLNET_MODULE_NOW_TAG=%%i

echo LLNET_MODULE_NOW_BRANCH %LLNET_MODULE_NOW_BRANCH%
echo LLNET_MODULE_NOW_TAG_LONG %LLNET_MODULE_NOW_TAG_LONG%
echo LLNET_MODULE_NOW_TAG %LLNET_MODULE_NOW_TAG%
echo.

if not exist Modules/InteropServices/LiteLoader.NET.InteropServices.dll (
    echo [WARNING] InteropServices files no found. Pulling from remote...
    echo.
    git clone %LLNET_MODULE_REMOTE_PATH%
)

cd Modules
git fetch --all
git reset --hard origin/%LLNET_MODULE_NOW_BRANCH%
git checkout %LLNET_MODULE_NOW_BRANCH%
cd ..

echo.
echo [INFO] Fetching InteropServices to GitHub finished
echo.

@REM remove InteropServices directory in Modules
echo [INFO] Removing Modules\InteropServices
rd /s /q Modules\InteropServices

@REM copy InteropServices to Modules
xcopy /e /y /i /q src\LiteLoader.NET.InteropServices\bin\x64\Release\net7.0\LiteLoader.NET.InteropServices.dll Modules\InteropServices\
xcopy /e /y /i /q src\LiteLoader.NET.InteropServices\x64\Release\LiteLoader.NET.InteropServices.Native.dll Modules\InteropServices\

cd Modules
for /f "delims=" %%i in ('git status . -s') do set LLNET_MODULE_NOW_STATUS=%%i
if "%LLNET_MODULE_NOW_STATUS%" neq "" (
    echo [INFO] Modified files found.
    echo.
    git add .
    if "%LLNET_MODULE_NOW_BRANCH%" == "main" (
        git commit -m "From InteropServices %LLNET_MODULE_NOW_TAG%"
        if [%2] == [release] (
            git tag %LLNET_MODULE_NOW_TAG%
        )
    ) else (
        git commit -m "From InteropServices %LLNET_MODULE_NOW_TAG_LONG%"
    )
    echo.
    echo [INFO] Pushing to origin...
    echo.
    if [%1] neq [action] (
        git push origin %LLNET_MODULE_NOW_BRANCH%
        git push --tags origin %LLNET_MODULE_NOW_BRANCH%
    ) else (
        git push https://%USERNAME%:%REPO_KEY%@github.com/LiteLDev-NET/Modules.git %LLNET_MODULE_NOW_BRANCH%
        git push --tags https://%USERNAME%:%REPO_KEY%@github.com/LiteLDev-NET/Modules.git %LLNET_MODULE_NOW_BRANCH%
    )
    cd ..
    echo.
    echo [INFO] Upload finished.
    echo.
    goto Finish
) else (
    cd ..
    echo.
    echo.
    echo [INFO] No modified files found.
    echo [INFO] No need to Upgrade InteropServices.
    goto Finish
)

:Finish
if [%1]==[action] goto End
timeout /t 3 >nul
:End
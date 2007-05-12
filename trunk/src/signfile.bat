@rem echo off
if "%SIGNPDN%" == "1" (
    if exist "%PDNPFX%" (
        signtool sign /f "%PDNPFX%" /d "Paint.NET v3.0" /du "http://www.getpaint.net/" "%1"
        signtool timestamp /t http://timestamp.comodoca.com/authenticode /v "%1"
    )
)


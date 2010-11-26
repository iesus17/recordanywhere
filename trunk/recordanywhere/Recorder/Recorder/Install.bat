@echo off
if exist "Recorder.dll" "C:\Windows\Microsoft.NET\Framework\v2.0.50727\regasm" "Recorder.dll" GOTO :END
if exist "bin\Release\Recorder.dll" "C:\Windows\Microsoft.NET\Framework\v2.0.50727\regasm" "bin\Release\Recorder.dll" GOTO :END
if exist "bin\Debug\Recorder.dll" "C:\Windows\Microsoft.NET\Framework\v2.0.50727\regasm" "bin\Debug\Recorder.dll" GOTO :END
:END
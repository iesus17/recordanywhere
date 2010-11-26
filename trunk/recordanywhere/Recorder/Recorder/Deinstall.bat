@echo off
if exist "Recorder.dll" "C:\Windows\Microsoft.NET\Framework\v2.0.50727\regasm" /unregister "Recorder.dll"
if exist "bin\Debug\Recorder.dll" "C:\Windows\Microsoft.NET\Framework\v2.0.50727\regasm" /unregister "bin\Debug\Recorder.dll"
if exist "bin\Release\Recorder.dll" "C:\Windows\Microsoft.NET\Framework\v2.0.50727\regasm" /unregister "bin\Release\Recorder.dll"
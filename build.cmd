::
:: "Manual" build script that bypasses MSBuild and directly invokes the necessary tools.
:: Good to show how things get hooked up together, but redundant with the project file.
::
:: The tools are:
::
:: * CSC, the C# compiler
::   Opening a "x64 Native Tools Command Prompt for VS 2019" will place csc.exe on your PATH.
:: * ILC, the Native AOT compiler
::   If you use the project file to build this sample at least once, you can find ILC
::   in your NuGet cache. It will be somewhere like
::   C:\Users\username\.nuget\packages\runtime.win-x64.microsoft.dotnet.ilcompiler\7.0.0-alpha.1.21430.2
:: * Linker
::   This is the platform linker. "x64 Native Tools Command Prompt for VS 2019" will place
::   the linker on your PATH.
::

@set ILCPATH=%DROPPATH%\tools
@if not exist %ILCPATH%\ilc.exe (
  echo The DROPPATH environment variable not set.
  exit /B
)
@where csc >nul 2>&1
@if ERRORLEVEL 1 (
  echo CSC not on the PATH.
  exit /B
)

@del zerosharp.ilexe >nul 2>&1
@del zerosharp.obj >nul 2>&1
@del zerosharp.exe >nul 2>&1
@del zerosharp.map >nul 2>&1
@del zerosharp.pdb >nul 2>&1

@if "%1" == "clean" exit /B

csc /nologo /debug:embedded /noconfig /nostdlib /runtimemetadataversion:v4.0.30319 Program.cs WDK.cs CLibNatives/CFunctions.cs CLR/CompilerHelpers.cs CLR/CompilerServices.cs CLR/System.cs CLR/Runtime.cs /out:ZeroKernel.ilexe /langversion:latest /unsafe || goto Error
%ILCPATH%\ilc ZeroKernel.ilexe -o ZeroKernel.obj --systemmodule ZeroKernel --map ZeroKernel.map -O || goto Error
link "C:\Program Files (x86)\Windows Kits\10\Lib\10.0.19041.0\km\x64\ntoskrnl.lib" /nologo /subsystem:native /DRIVER:WDM ZeroKernel.obj /entry:DriverEntry /incremental:no /out:test.sys || goto Error

@goto :EOF

:Error
@echo Tool failed.
exit /B 1
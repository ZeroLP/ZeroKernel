# ZeroKernel (Experimental)
Bringing kernel driver to C# with MichalStrehovsky's zerosharp

A magical combination of https://github.com/MichalStrehovsky/zerosharp and https://github.com/dotnet/runtimelab/tree/feature/NativeAOT 

# Building
Run build.cmd from 'Developer Command Prompt for VS [Version] (2022 in my case)` for a get-go compilation. 

 ~~Right now the driver is loadable (with kdmapper) but without any executable code.~~
 It prints "Hello World!".

# WDK Credit
I personally thank and credit [VollRagm](https://github.com/VollRagm) for his work on extending ZeroKernel with his [KernelSharp](https://github.com/VollRagm/KernelSharp) project.
Crediting his work on porting WDK and CLR dummy classes.

This project contains parts of [KernelSharp](https://github.com/VollRagm/KernelSharp).

# TODOs
-  ~~Somehow magically implement runtime-less C# string marshalling.~~
-  ~~Build a native export driver that exports wdk functions that our driver can pinvoke to. (Most importantly DbgPrintEx for now)~~
-  ~~Get the driver to output "Hello World" string from a kernel debugger after solving TODO #2.~~
- Whatever is needed after TODO #3

![](https://github.com/ZeroLP/ZeroKernel/blob/master/HelloWorldPrint.JPG)
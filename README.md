# ZeroKernel (Experimental)
Bringing kernel driver to C# with MichalStrehovsky's zerosharp

A magical combination of https://github.com/MichalStrehovsky/zerosharp and https://github.com/dotnet/runtimelab/tree/feature/NativeAOT 

# Building
Run build.cmd for a get-go compilation.

Right now the driver is loadable (with kdmapper) but without any executable code.

# TODOs
- Somehow magically implement runtime-less C# string marshalling.
- Get the driver to output "Hello World" string from a kernel debugger after solving TODO #1.
- Whatever is needed after TODO #2


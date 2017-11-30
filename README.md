Esendex .Net SDK
================

The Esendex .Net SDK is an easy to use client for Esendex's REST API which you can use to integrate SMS and Voice messaging into any application built with C#, VB.NET, Managed C++, F# or any other language built on the .NET framework. 

It contains a set of services for sending SMS and Voice messages, receiving SMS, tracking the status of your sent messages and more.

Full details at http://developers.esendex.com/SDKs/DotNet-SDK


## Building

Building the SDK requires Visual Studio 2017 or its build tools, including the .NET Framework 3.5 build components.

First, restore NuGet packages. This must be done twice, or the .NET 3.5 test project's packages don't get restored:

- `msbuild source\com.esendex.sdk.sln /t:Restore`
- `.nuget\nuget.exe restore test\net35\com.esendex.sdk.test.net35.csproj -SolutionDirectory .`

Visual Studio 2017 will restore all the NuGet packages by itself.

Use the MSBuild scripts in the .solution directory to build. This will build .NET Standard 2.0 and .NET Framework 3.5 libraries.

- build.msbuild: builds unsigned assemblies
- buildsigned.local.msbuild: builds signed assemblies using the key in the .solution folder, for testing purposes
- buildsigned.msbuild: builds signed assemblies using the Esendex private key (not included in this repository)

To allow Visual Studio to build the tests, run MSBuild first to generate the BuildInfo file which grants access to the library internals to the unit test assemblies.
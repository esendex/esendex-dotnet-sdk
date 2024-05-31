Esendex .Net SDK
================

The Esendex .Net SDK is an easy to use client for Esendex's REST API which you can use to integrate SMS and Voice messaging into any application built with C#, VB.NET, Managed C++, F# or any other language built on the .NET framework. 

It contains a set of services for sending SMS and Voice messages, receiving SMS, tracking the status of your sent messages and more.

Full details at http://developers.esendex.com/SDKs/DotNet-SDK


## Building

Building the SDK requires Visual Studio 2017 or its build tools, including the .NET 8.0 build components.

First, restore NuGet packages.

- `dotnet restore`

Visual Studio 2017 will restore all the NuGet packages by itself.

To build the solution use the following commands:

- `dotnet build -c Debug`: builds unsigned assemblies
- `dotnet build -c Release --property:LOCAL=true`: builds signed assemblies using the key in the .solution folder, for testing purposes
- `dotnet build -c Release`: builds signed assemblies using the Esendex private key (not included in this repository)

To build a NuGet package, use the following command after building:

- `dotnet pack -c Release`

The package will be created in `source\bin\Release\esendex-dotnet-sdk.$version.nupkg`

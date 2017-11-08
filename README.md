Esendex .Net SDK
================

[![Build Status](https://travis-ci.org/esendex/esendex-dotnet-sdk.svg?branch=master)](https://travis-ci.org/esendex/esendex-dotnet-sdk)

The Esendex .Net SDK is an easy to use client for Esendex's REST API which you can use to integrate SMS and Voice messaging into any application built with C#, VB.NET, Managed C++, F# or any other language built on the .NET framework. 

It contains a set of services for sending SMS and Voice messages, receiving SMS, tracking the status of your sent messages and more.

Full details at http://developers.esendex.com/SDKs/DotNet-SDK


## Building

Building the SDK requires Visual Studio 2017 or its build tools, including the .NET Framework 3.5 build components.

Use the MSBuild scripts in the .solution directory to build. This will build .NET Standard 2.0 and .NET Framework 3.5 libraries.

- build.msbuild: builds unsigned assemblies
- buildsigned.local.msbuild: builds signed assemblies using the key in the .solution folder, for testing purposes
- buildsigned.msbuild: builds signed assemblies using the Esendex private key (not included in this repository)

To allow Visual Studio to build the tests, run MSBuild first to generate the BuildInfo file which grants access to the library internals to the unit test assemblies.

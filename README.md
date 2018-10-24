[![Build status](https://clearmeasurelabs.visualstudio.com/Onion-DevOps-Architecture/_apis/build/status/Onion-DevOps-Architecture-ASP.NET%20Core-CI)](https://clearmeasurelabs.visualstudio.com/Onion-DevOps-Architecture/_build/latest?definitionId=7)
# Introduction 
Demonstrates how to set up .Net Core for DevOps to Azure, with patterns including Onion Architecture
# Getting Started
Install SQL Express 2017 & Visual Studio 15.8

# Build and Test
Run .\build.ps1

# Tips
- Do not use .Net Core CI build steps - they compile your code multiple times and perform Nuget Restore 3 time over
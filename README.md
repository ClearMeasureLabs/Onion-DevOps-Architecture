[![Build status](https://clearmeasurelabs.visualstudio.com/Onion-DevOps-Architecture/_apis/build/status/Onion-DevOps-Architecture-ASP.NET%20Core-CI)](https://clearmeasurelabs.visualstudio.com/Onion-DevOps-Architecture/_build/latest?definitionId=7)
# Introduction 
Demonstrates how to set up .Net Core for DevOps to Azure, with professional patterns included:
 * Private build
 * Test-driven development
 * Onion Architecture solution reference structure
 * Infrastructure as Code
 * CQRS
 * DevOps diagnostics
# Getting Started
Install SQL Express 2017 (localhost\sql2017) & Visual Studio 15.8
Clone the repository

# Build and Test
Run .\build.ps1 OR click_to_build.bat

# Tips
- Do not use .Net Core CI build steps - they compile your code multiple times and perform Nuget Restore 3 time over



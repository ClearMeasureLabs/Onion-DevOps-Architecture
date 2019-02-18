[![Build status](https://dev.azure.com/clearmeasurelabs/Onion-DevOps-Architecture/_apis/build/status/Onion-DevOps-Architecture-CI)](https://dev.azure.com/clearmeasurelabs/Onion-DevOps-Architecture/_build/latest?definitionId=7)
![TDD status](https://vsrm.dev.azure.com/clearmeasurelabs/_apis/public/Release/badge/801ebfd3-bd0a-4c92-8080-1b73805b58d1/1/1)
![UAT status](https://vsrm.dev.azure.com/clearmeasurelabs/_apis/public/Release/badge/801ebfd3-bd0a-4c92-8080-1b73805b58d1/1/2)
![Prod status](https://vsrm.dev.azure.com/clearmeasurelabs/_apis/public/Release/badge/801ebfd3-bd0a-4c92-8080-1b73805b58d1/1/3)


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
Application has been upgraded for .Net Core 3 preview.

# Build and Test
Run .\build.ps1 OR click_to_build.bat

# Tips
- Do not use .Net Core CI build steps - they compile your code multiple times and perform Nuget Restore 3 time over

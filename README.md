# QA Assignment

Contains a simple microservice based on Azure Functions for storing and retrieving email logs.

The goal of this assignment is to cover the service with a few integration tests. Try to work with the assumption that the service will grow in the future. The test structure and framework created by you should make it easy to extend the test coverage for newly developed features.

You can use any framework to write the tests, although our preferred framework is xUnit.

Try to describe in a few sentences how you would include these tests in a CI/CD pipeline (at what stage they should run and how would it work).

## Project structure
Feel free to adjust the project structure to your needs.
- `src/LogService` - the Azure Function project for the microservice
- `src/LogService.Api` - an API wrapper for clients
- `src/LogService.Services` - implements the service logic
- `src/LogService.Client` - a simple example client to illustrate how the service works

## Prerequisites
- .NET 6 SDK
- Azure Functions Core Tools V4.x
- Azurite (Azure Storage Emulator)

### Run in Visual Studio 2022
1. Make sure you have the Azure Development workload installed.
2. Run the `LogService` project.

### Without Visual Studio
1. Run Azurite (Azure Storage Emulator)
2. Run `func start` in the `src/LogService` folder.
# LogService

A .NET service for managing and storing email logs. This service provides functionality to store, retrieve, and delete email communication logs.

## Features

- Add new email logs with unique IDs
- Retrieve all stored logs
- Delete specific logs by ID
- Prevents duplicate log entries
- Async operation support

## Getting Started

### Prerequisites

- .NET 6.0 or later
- A compatible storage solution (implementation details determined by your configuration)

### Installation

Restore NuGet packages
```dotnet restore```
Build the project
```dotnet build```


## Testing

The project includes integration tests using xUnit. To run the tests:

```bash
cd src/LogService.IntegrationTests
dotnet test
```

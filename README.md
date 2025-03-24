# LogService

Service for managing and storing email logs. This service provides functionality to store, get and delete email logs.

### Prerequisites

- .NET 6 SDK
- Azure Functions Core Tools V4.x
- Azurite (Azure Storage Emulator)

### Installation

Restore NuGet packages
```dotnet restore```
Build the project
```dotnet build```

## Run in Visual Studio 2022
    Make sure you have the Azure Development workload installed.
    Run the LogService project.

## Without Visual Studio
    Run Azurite (Azure Storage Emulator)
    Run func start in the src/LogService folder.

## Testing

The project includes integration tests using xUnit. To run the tests:

### Environment Configuration

1. **Configuration Files Structure**
   - `appsettings.json` - common configuration 
   - `appsettings.<ENVIRONMENT>.json` - environment specific settings
   - Additional environment files can be added as needed (e.g., `appsettings.Production.json`)

2. **Running Tests in Different Environments**
   ```bash
   # Set environment before running tests
   export TEST_ENVIRONMENT=Development
   dotnet test

   # Or
   TEST_ENVIRONMENT=Development dotnet test
   ```

### CI/CD Pipeline Flow

1. **Build Stage**
   - Restores dependencies
   - Builds the solution
   - Runs unit tests

2. **Integration Stage** 
   - Executes integration tests against a <specific> environment
   - Verifies system functionality with actual dependencies

3. **Deploy Stage** (runs only on main branch)
   - Deploys to Azure App Service
   - Can be extended to include multiple environments

## When to Run Integration Tests**

   - ✅ During local development
   - ✅ In CI/CD pipeline before deployment to production (multiple stages)
   - ✅ After deployment as smoke tests
   - ❌ Don't run on every commit/PR (might be time-consuming and resource-intensive)



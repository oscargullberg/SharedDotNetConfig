# SharedDotNetConfig

## Overview

This library simplifies the loading of base configuration from a common ancestor folder. Main use case is monorepos where multiple projects share a common configuration base.
Supports appsettings and dotenv.

## Usage

1. Put `appsettings.json` and `.env` in `{repoRoot}/development-config`

```csharp
IConfigurationBuilder builder = new ConfigurationBuilder();
builder.AddSharedConfig();
...
```

See [samples/GullbergSolutions.SharedDotNetConfig.AspNetDemo/Program.cs](samples/GullbergSolutions.SharedDotNetConfig.AspNetDemo/Program.cs) for usage in ASP.NET Web API project.

### Parameters

- `sharedConfigFolderName` (optional): Specifies the folder name where the shared configuration files are located. Default is `development-config`.
- `maxLevelsToSearch` (optional): Specifies the maximum number of directory levels to search upward from the application directory for the shared configuration folder. Default is `7`.

using Microsoft.Extensions.Configuration;

namespace GullbergSolutions.SharedDotNetConfig;

public static class ConfigurationBuilderExtensions
{
    private const string SharedDevelopmentConfigDirectoryName = "development-config";

    public static IConfigurationBuilder AddSharedConfig(
        this IConfigurationBuilder builder,
        string sharedConfigFolderName = SharedDevelopmentConfigDirectoryName,
        int maxLevelsToSearch = 7
    )
    {
        var sharedDevelopmentConfigPath = TryGetSharedConfigPath(
            sharedConfigFolderName,
            maxLevelsToSearch
        );
        if (sharedDevelopmentConfigPath == null)
        {
            return builder;
        }

        var sharedAppSettings = Path.Combine(sharedDevelopmentConfigPath, "appsettings.json");
        builder.AddJsonFile(sharedAppSettings, optional: true);

        var sharedDotEnv = Path.Combine(sharedDevelopmentConfigPath, ".env");
        DotNetEnv.Env.NoClobber().Load(sharedDotEnv);

        return builder;
    }

    /// <summary>
    /// Attempts to find shared development config by traversing ancestors of current directory
    /// </summary>
    /// <returns>Shared development config directory path if present</returns>
    private static string? TryGetSharedConfigPath(string targetDirectoryName, int maxLevelsToSearch)
    {
        var currentDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        var remainingLevels = maxLevelsToSearch;
        while (currentDirectory != null && remainingLevels > 0)
        {
            currentDirectory = currentDirectory.Parent;
            var match = currentDirectory
                ?.GetDirectories(targetDirectoryName, SearchOption.TopDirectoryOnly)
                .FirstOrDefault();
            if (match != null)
            {
                return match.FullName;
            }

            remainingLevels--;
        }

        return null;
    }
}

using System;
using Microsoft.Extensions.Configuration;

namespace Acc.Server.Results.Companion.Core.Services;

public static class Configuration
{
    private static IConfigurationRoot configuration;

    internal static IConfiguration CurrentConfig => configuration;

    internal static void Init()
    {
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.SetBasePath(PathProvider.AppFolderPath)
            .AddJsonFile(PathProvider.AppSettingsFileName, false, true)
            .AddEnvironmentVariables();
        configuration = configurationBuilder.Build();
    }

    public static IConfigurationSection GetSection(string key)
    {
        return configuration.GetSection(key);
    }
}
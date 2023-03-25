using System;
using System.IO;

namespace Acc.Server.Results.Companion.Core.Services;

public static class PathProvider
{
    public const string AppName = "ACC Server Results Companion";
    public const string AppSettingsFileName = "appsettings.json";
    private const string UserSettingsFileName = "userSettings.json";
    private const string LogFolderName = "Logs";
    private const string ResultsFolder = "Results";

    static PathProvider()
    {
        var localAppDataFolderPath =
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        AppDataFolderPath = Path.Combine(localAppDataFolderPath, AppName);
        AppSettingsFilePath = Path.Combine(AppDataFolderPath, AppSettingsFileName);
        var executionFolder = AppDomain.CurrentDomain.BaseDirectory;
        AppFolderPath = Path.GetDirectoryName(executionFolder)!;
        AppLogFolderPath = Path.Combine(AppDataFolderPath, LogFolderName);
        DefaultSettingsFilePath = Path.Combine(AppFolderPath!, AppSettingsFileName);
        UserSettingsFilePath = Path.Combine(AppDataFolderPath, UserSettingsFileName);
        DownloadedResultsFolderPath = Path.Combine(AppDataFolderPath, ResultsFolder);
    }

    public static string AppDataFolderPath { get; }
    public static string AppFolderPath { get; }
    public static string AppLogFolderPath { get; }
    public static string AppSettingsFilePath { get; }
    public static string DefaultSettingsFilePath { get; }
    public static string UserSettingsFilePath { get; }
    public static string DownloadedResultsFolderPath { get; }

    public static string GetLastFolderName(string path)
    {
        var folderPath = Path.HasExtension(path)? Path.GetDirectoryName(path): path;
        return new DirectoryInfo(folderPath!).Name;
    }
}
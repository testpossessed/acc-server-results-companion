using System;
using System.IO;
using NLog;
using NLog.Extensions.Logging;

namespace Acc.Server.Results.Companion.Core.Services
{

    internal static class LogWriter
    {
        private const string NLogInternalLogPath = @"C:\NLog\ACCServerResultsCompanion";
        private static Logger logger = null!;

        internal static void Init()
        {
            if(!Directory.Exists(NLogInternalLogPath))
            {
                Directory.CreateDirectory(NLogInternalLogPath);
            }

            logger = LogManager.Setup()
                               .LoadConfigurationFromSection(Configuration.CurrentConfig)
                               .GetCurrentClassLogger();
            LogManager.Configuration.Variables["appDataFolder"] = PathProvider.AppDataFolderPath;
        }

        internal static void LogInfo(string message)
        {
            logger.Log(LogLevel.Info, message);
        }

        internal static void LogDebug(string message)
        {
            logger.Log(LogLevel.Debug, message);
        }

        internal static void LogWarning(string message)
        {
            logger.Log(LogLevel.Warn, message);
        }

        internal static void LogError(Exception exception, string message)
        {
            logger.Log(LogLevel.Error, exception, message);
        }
    }
}

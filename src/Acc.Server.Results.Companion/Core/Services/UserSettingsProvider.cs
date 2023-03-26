using System;
using System.IO;
using Acc.Server.Results.Companion.Core.Models;
using Newtonsoft.Json;

namespace Acc.Server.Results.Companion.Core.Services
{
    internal static class UserSettingsProvider
    {
        private static UserSettings currentSettings; 

        internal static UserSettings GetSettings()
        {
            if(currentSettings == null)
            {
                currentSettings = new UserSettings();
                if(File.Exists(PathProvider.UserSettingsFilePath))
                {
                    var json = File.ReadAllText(PathProvider.UserSettingsFilePath);
                    if(!string.IsNullOrWhiteSpace(json))
                    {
                        currentSettings = JsonConvert.DeserializeObject<UserSettings>(json);
                    }
                }
            }

            return currentSettings;
        }

        internal static void SetLastServerId(int serverId)
        {
            GetSettings().LastServerId = serverId;
            SaveCurrentSettings();
        }

        private static void SaveCurrentSettings()
        {
            var json = JsonConvert.SerializeObject(currentSettings);
            File.WriteAllText(PathProvider.UserSettingsFilePath, json);
        }
    }
}
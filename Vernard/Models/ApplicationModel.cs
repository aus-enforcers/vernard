using System;
using Windows.Storage;

namespace Vernard.Models
{
    internal class ApplicationModel
    {
        internal int DefaultTime { get; set; }
        internal bool AlwaysOnTop { get; set; }
        internal bool OpenAtLogin { get; set; }
        internal bool PlayWarnings { get; set; }
        internal bool SendBeacon { get; set; }
        internal string BeaconUrl { get; set; }
        internal int BeaconInterval { get; set; }
        internal string BeaconIdentifier { get; set; }
        internal string BeaconToken { get; set; }

        internal event EventHandler Reload;

        internal ApplicationModel()
        {
            Load();
        }

        internal void Load()
        {
            DefaultTime = LoadSetting("defaultTime", 900);
            AlwaysOnTop = LoadSetting("alwaysOnTop", true);
            OpenAtLogin = LoadSetting("openAtLogin", true);
            PlayWarnings = LoadSetting("playWarnings", true);
            SendBeacon = LoadSetting("sendBeacon", false);
            BeaconUrl = LoadSetting("beaconUrl", "");
            BeaconInterval = LoadSetting("beaconInterval", 30);
            BeaconIdentifier = LoadSetting("beaconIdentifier", "");
            BeaconToken = LoadSetting("beaconToken", "");

            OnReload();
        }

        internal void Save()
        {
            SaveSetting("defaultTime", DefaultTime);
            SaveSetting("alwaysOnTop", AlwaysOnTop);
            SaveSetting("openAtLogin", OpenAtLogin);
            SaveSetting("playWarnings", PlayWarnings);
            SaveSetting("sendBeacon", SendBeacon);
            SaveSetting("beaconUrl", BeaconUrl);
            SaveSetting("beaconInterval", BeaconInterval);
            SaveSetting("beaconIdentifier", BeaconIdentifier);
            SaveSetting("beaconToken", BeaconToken);

            OnReload();
        }

        private T LoadSetting<T>(string settingKey, T defaultValue)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            return localSettings.Values.ContainsKey(settingKey) ? (T)localSettings.Values[settingKey] : defaultValue;
        }

        private void SaveSetting(string settingKey, object value)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[settingKey] = value;
        }

        private void OnReload()
        {
            Reload?.Invoke(this, EventArgs.Empty);
        }
    }
}

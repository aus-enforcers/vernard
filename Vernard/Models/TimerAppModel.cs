using System;
using Windows.Storage;

namespace Vernard.Models
{
    internal class TimerAppModel
    {
        internal int DefaultTime { get; set; }
        internal bool AlwaysOnTop { get; set; }
        internal bool OpenAtLogin { get; set; }
        internal bool PlayWarnings { get; set; }

        public event EventHandler OnLoad;

        internal TimerAppModel()
        {
            DefaultTime = 900;
            AlwaysOnTop = true;
            OpenAtLogin = true;
            PlayWarnings = true;
        }

        internal void Load()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("defaultTime"))
            {
                DefaultTime = (int)localSettings.Values["defaultTime"];
            }

            if (localSettings.Values.ContainsKey("alwaysOnTop"))
            {
                AlwaysOnTop = (bool)localSettings.Values["alwaysOnTop"];
            }

            if (localSettings.Values.ContainsKey("openAtLogin"))
            {
                OpenAtLogin = (bool)localSettings.Values["openAtLogin"];
            }

            if (localSettings.Values.ContainsKey("playWarnings"))
            {
                PlayWarnings = (bool)localSettings.Values["playWarnings"];
            }

            if (OnLoad != null)
            {
                OnLoad(this, EventArgs.Empty);
            }
        }

        internal void Save()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["defaultTime"] = DefaultTime;
            localSettings.Values["alwaysOnTop"] = AlwaysOnTop;
            localSettings.Values["openAtLogin"] = OpenAtLogin;
            localSettings.Values["playWarnings"] = PlayWarnings;
            Load();
        }
    }
}

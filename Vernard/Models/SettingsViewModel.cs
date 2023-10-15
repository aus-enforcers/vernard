using Microsoft.UI.Xaml;

namespace Vernard.Models
{
    internal class SettingsViewModel : BindableBase
    {
        private TimerAppModel ApplicationModel { get => (Application.Current as App).ApplicationModel; }

        private int m_defaultTime;
        internal int DefaultTime { get => m_defaultTime; set => SetProperty(ref m_defaultTime, value); }

        private bool m_alwaysOnTop;
        internal bool AlwaysOnTop { get => m_alwaysOnTop; set => SetProperty(ref m_alwaysOnTop, value); }

        private bool m_openAtLogin;
        internal bool OpenAtLogin { get => m_openAtLogin; set => SetProperty(ref m_openAtLogin, value); }

        private bool m_playWarnings;
        internal bool PlayWarnings { get => m_playWarnings; set => SetProperty(ref m_playWarnings, value); }

        internal SettingsViewModel()
        {
            Load();
        }

        internal void Load()
        {
            DefaultTime = ApplicationModel.DefaultTime;
            AlwaysOnTop = ApplicationModel.AlwaysOnTop;
            OpenAtLogin = ApplicationModel.OpenAtLogin;
            PlayWarnings = ApplicationModel.PlayWarnings;
        }

        internal void Save()
        {
            ApplicationModel.DefaultTime = DefaultTime;
            ApplicationModel.AlwaysOnTop = AlwaysOnTop;
            ApplicationModel.OpenAtLogin = OpenAtLogin;
            ApplicationModel.PlayWarnings = PlayWarnings;
            ApplicationModel.Save();
        }
    }
}

using Microsoft.UI.Xaml;

namespace Vernard.Models
{
    internal class SettingsViewModel : BindableBase
    {
        private int m_defaultTime;
        internal int DefaultTime { get => m_defaultTime; set => SetProperty(ref m_defaultTime, value); }

        private bool m_alwaysOnTop;
        internal bool AlwaysOnTop { get => m_alwaysOnTop; set => SetProperty(ref m_alwaysOnTop, value); }

        private bool m_openAtLogin;
        internal bool OpenAtLogin { get => m_openAtLogin; set => SetProperty(ref m_openAtLogin, value); }

        private bool m_playWarnings;
        internal bool PlayWarnings { get => m_playWarnings; set => SetProperty(ref m_playWarnings, value); }

        private bool m_sendBeacon;
        internal bool SendBeacon { get => m_sendBeacon; set => SetProperty(ref m_sendBeacon, value); }

        private string m_beaconUrl;
        internal string BeaconUrl { get => m_beaconUrl; set => SetProperty(ref m_beaconUrl, value); }

        private int m_beaconInterval;
        internal int BeaconInterval { get => m_beaconInterval; set => SetProperty(ref m_beaconInterval, value); }

        private string m_beaconIdentifier;
        internal string BeaconIdentifier { get => m_beaconIdentifier; set => SetProperty(ref m_beaconIdentifier, value); }

        private string m_beaconToken;
        internal string BeaconToken { get => m_beaconToken; set => SetProperty(ref m_beaconToken, value); }

        internal SettingsViewModel()
        {
            Load();
        }

        internal void Load()
        {
            var appModel = App.GetApplicationModel();
            DefaultTime = appModel.DefaultTime;
            AlwaysOnTop = appModel.AlwaysOnTop;
            OpenAtLogin = appModel.OpenAtLogin;
            PlayWarnings = appModel.PlayWarnings;
            SendBeacon = appModel.SendBeacon;
            BeaconUrl = appModel.BeaconUrl;
            BeaconInterval = appModel.BeaconInterval;
            BeaconIdentifier = appModel.BeaconIdentifier;
            BeaconToken = appModel.BeaconToken;
        }

        internal void Save()
        {
            var appModel = App.GetApplicationModel();
            appModel.DefaultTime = DefaultTime;
            appModel.AlwaysOnTop = AlwaysOnTop;
            appModel.OpenAtLogin = OpenAtLogin;
            appModel.PlayWarnings = PlayWarnings;
            appModel.SendBeacon = SendBeacon;
            appModel.BeaconUrl = BeaconUrl;
            appModel.BeaconInterval = BeaconInterval;
            appModel.BeaconIdentifier = BeaconIdentifier;
            appModel.BeaconToken = BeaconToken;
            appModel.Save();
        }
    }
}

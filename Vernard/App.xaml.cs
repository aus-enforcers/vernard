using Microsoft.UI.Xaml;
using System;
using Vernard.Models;
using Vernard.Utilities;
using Vernard.Views.Main;

namespace Vernard
{
    public partial class App : Application
    {
        internal ApplicationModel ApplicationModel { get; init; }

        public App()
        {
            this.InitializeComponent();
            ApplicationModel = new ApplicationModel();
            ApplicationModel.Load();
            ApplicationModel.Reload += ApplicationModel_OnReload;
        }

        private async void ApplicationModel_OnReload(object sender, EventArgs e)
        {
            if (await StartupTaskUtility.IsEnabled() != ApplicationModel.OpenAtLogin)
            {
                var openAtLogin = await StartupTaskUtility.Toggle(ApplicationModel.OpenAtLogin);
                if (openAtLogin != ApplicationModel.OpenAtLogin)
                {
                    ApplicationModel.OpenAtLogin = openAtLogin;
                    ApplicationModel.Save();
                }
            }
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Activate();
        }

        internal static ApplicationModel GetApplicationModel()
        {
            return (Current as App).ApplicationModel;
        }
    }
}

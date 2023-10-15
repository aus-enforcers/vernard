using Microsoft.UI.Xaml;
using Vernard.Models;
using Vernard.Views;

namespace Vernard
{
    public partial class App : Application
    {
        internal TimerAppModel ApplicationModel { get; init; }
     
        public App()
        {
            this.InitializeComponent();
            ApplicationModel = new TimerAppModel();
            ApplicationModel.Load();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Activate();
        }

    }
}

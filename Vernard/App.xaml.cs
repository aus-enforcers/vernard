using Microsoft.UI.Xaml;
using Vernard.Models;
using Vernard.Views;

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

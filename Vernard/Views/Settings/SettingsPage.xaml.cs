using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Diagnostics;
using System.Reflection;
using Vernard.Models;

namespace Vernard.Views.Settings
{
    internal sealed partial class SettingsPage : Page
    {
        private SettingsViewModel ViewModel { get; set; }
        private Action CloseAction { get; set; }
        private string ApplicationVersion { get; init; }
        private string ApplicationCopyright { get; init; }

        internal SettingsPage()
        {
            this.InitializeComponent();
            ViewModel = new SettingsViewModel();
            App.GetApplicationModel().Reload += ApplicationModel_OnReload;

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            ApplicationVersion = fileVersionInfo.ProductVersion;
            ApplicationCopyright = fileVersionInfo.LegalCopyright;
        }

        private void ApplicationModel_OnReload(object sender, EventArgs e)
        {
            ViewModel.Load();
        }

        private void Close()
        {
            App.GetApplicationModel().Reload -= ApplicationModel_OnReload;
            CloseAction();
        }

        protected override void OnNavigatedTo(NavigationEventArgs eventArgs)
        {
            CloseAction = (Action)eventArgs.Parameter;
            base.OnNavigatedTo(eventArgs);
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Save();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Save();
            Close();
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer != null)
            {
                var tag = args.InvokedItemContainer.Tag.ToString();

                foreach (var item in SettingsPanels.Children)
                {
                    item.Visibility = Visibility.Collapsed;
                }

                switch (tag)
                {
                    case "GeneralSettings":
                        NavigationView.Header = "General Settings";
                        GeneralSettings.Visibility = Visibility.Visible;
                        Actions.Visibility = Visibility.Visible;
                        break;

                    case "BeaconSettings":
                        NavigationView.Header = "Beacon Settings";
                        BeaconSettings.Visibility = Visibility.Visible;
                        Actions.Visibility = Visibility.Visible;
                        break;

                    case "About":
                        NavigationView.Header = "About";
                        About.Visibility = Visibility.Visible;
                        Actions.Visibility = Visibility.Collapsed;
                        break;

                    default:
                        throw new Exception($"Unknown tag {tag}");
                }
            }
        }
    }
}

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using Vernard.Models;

namespace Vernard.Views.Settings
{
    internal sealed partial class SettingsPage : Page
    {
        private SettingsViewModel ViewModel { get; set; }
        private Action CloseAction { get; set; }

        internal SettingsPage()
        {
            this.InitializeComponent();
            ViewModel = new SettingsViewModel();
            App.GetApplicationModel().OnReload += delegate { ViewModel.Load(); };
        }

        protected override void OnNavigatedTo(NavigationEventArgs eventArgs)
        {
            CloseAction = (Action)eventArgs.Parameter;
            base.OnNavigatedTo(eventArgs);
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseAction();
        }

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Save();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Save();
            CloseAction();
        }
    }
}

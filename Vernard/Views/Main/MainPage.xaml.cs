using Microsoft.UI.Xaml;
using System;
using System.Timers;
using Vernard.Models;
using Vernard.Views.Settings;

namespace Vernard.Views.Main
{
    internal sealed partial class MainPage
    {
        private SettingsWindow SettingsWindow { get; set; }
        private Timer Timer { get; set; }
        private MainViewModel ViewModel { get; set; }

        internal MainPage()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModel();
            App.GetApplicationModel().Reload += ApplicationModel_OnReload;
        }

        private void ApplicationModel_OnReload(object sender, EventArgs e)
        {
            ViewModel.Load();
        }

        private void CreateTimer()
        {
            DestroyTimer();
            Timer = new Timer(1000);
            Timer.Elapsed += TimerElapsed;
            Timer.AutoReset = true;
            Timer.Start();
        }

        private void DestroyTimer()
        {
            if (Timer != null)
            {
                Timer.Stop();
                Timer.Dispose();
                Timer = null;
            }
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            DispatcherQueue.TryEnqueue(() =>
            {
                if (!ViewModel.Tick())
                {
                    DestroyTimer();
                }
            });
        }

        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            CreateTimer();
            ViewModel.Play();
        }

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            DestroyTimer();
            ViewModel.Pause();
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            DestroyTimer();
            ViewModel.Stop();
        }

        private void ButtonAddMinute_Click(object sender, RoutedEventArgs e)
        {

            ViewModel.AddMinute();
        }

        private void ButtonRemoveMinute_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RemoveMinute();
        }

        private void ButtonUnsanitised_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Unsanitised();
        }

        private void ButtonSanitised_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Sanitised();
        }

        private void ButtonBroken_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Broken();
        }

        private void ButtonFixed_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Fixed();
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            if (SettingsWindow == null)
            {
                SettingsWindow = new SettingsWindow();
                SettingsWindow.Activate();
                SettingsWindow.Closed += delegate
                {
                    SettingsWindow = null;
                };
            }
            else
            {
                SettingsWindow.AppWindow.MoveInZOrderAtTop();
            }
        }
    }
}

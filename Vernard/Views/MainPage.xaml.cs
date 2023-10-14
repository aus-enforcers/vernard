using Microsoft.UI.Xaml;
using System.Timers;
using Vernard.Models;

namespace Vernard.Views
{
    public sealed partial class MainPage
    {
        private Timer Timer { get; set; }
        public TimerViewModel ViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = new TimerViewModel();
            ViewModel.Total = 900;
            ViewModel.Remaining = 900;
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
                if (!ViewModel.IsPlayable)
                {
                    DestroyTimer();
                }
                else
                {
                    ViewModel.Tick();
                }
            });
        }
        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            CreateTimer();
            ViewModel.State = TimerState.Playing;
        }
        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            DestroyTimer();
            ViewModel.State = TimerState.Paused;
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            DestroyTimer();
            ViewModel.Remaining = 900;
            ViewModel.Total = 900;
            ViewModel.State = TimerState.Ready;
        }

        private void ButtonAddMinute_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Remaining += 60;
            if (ViewModel.Remaining > ViewModel.Total)
            {
                ViewModel.Total = ViewModel.Remaining;
            }
        }

        private void ButtonRemoveMinute_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Remaining -= 60;
        }
    }
}

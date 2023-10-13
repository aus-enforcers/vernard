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
            ViewModel.TimeTotal = 900;
            ViewModel.TimeLeft = 900;
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
            DispatcherQueue.TryEnqueue(ViewModel.Tick);
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
            ViewModel.TimeLeft = 900;
            ViewModel.TimeTotal = 900;
            ViewModel.State = TimerState.Ready;
        }
    }
}

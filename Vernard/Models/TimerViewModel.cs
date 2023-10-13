using System.Diagnostics;

namespace Vernard.Models
{
    public class TimerViewModel : BindableBase
    {
        private int _timeLeft;
        public int TimeLeft
        {
            get => _timeLeft;
            set
            {
                SetProperty(ref _timeLeft, value <= 0 ? 0 : value);
                TimeLeftPercent = TimeTotal > 0 ? ((double)TimeLeft / (double)TimeTotal * 100) : 0;
            }
        }

        private int _timeTotal;
        public int TimeTotal
        {
            get => _timeTotal;
            set
            {
                SetProperty(ref _timeTotal, value <= 0 ? 0 : value);
                TimeLeftPercent = TimeTotal > 0 ? ((double)TimeLeft / (double)TimeTotal * 100) : 0;
            }
        }

        private double _timeLeftPercent;
        public double TimeLeftPercent
        {
            get => _timeLeftPercent;
            set
            {
                Debug.WriteLine($"{TimeLeft} / {TimeTotal} = {value}%");
                SetProperty(ref _timeLeftPercent, value <= 0 ? 0 : (value >= 100 ? 100 : value));
            }
        }

        private TimerState _state;
        public TimerState State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        internal TimerViewModel()
        {
            TimeLeft = 0;
            TimeTotal = 0;
            State = TimerState.Ready;
        }

        internal void Tick()
        {
            TimeLeft -= 1;
        }
    }
}

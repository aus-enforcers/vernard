using Microsoft.UI;
using Windows.UI;

namespace Vernard.Models
{
    public class TimerViewModel : BindableBase
    {
        public int DefaultTime { get; init; }

        private int m_remaining;
        public int Remaining
        {
            get => m_remaining;
            set
            {
                SetProperty(ref m_remaining, value <= 0 ? 0 : value);
                UpdateColor();
                UpdateProgress();
                UpdateProgressColor();
            }
        }

        private int m_total;
        public int Total
        {
            get => m_total;
            set
            {
                SetProperty(ref m_total, value <= 0 ? 0 : value);
                UpdateProgress();
            }
        }

        private double m_progress;
        public double Progress
        {
            get => m_progress;
            set => SetProperty(ref m_progress, value <= 0 ? 0 : (value >= 100 ? 100 : value));
        }

        private TimerState m_state;
        public TimerState State
        {
            get => m_state;
            set
            {
                SetProperty(ref m_state, value);
                UpdateColor();
                UpdateIsPlaying();
                UpdateIsNotPlaying();
                UpdateIsPlayable();
                UpdateIsUnsanitised();
                UpdateIsBroken();
            }
        }

        private bool m_isPlaying;
        public bool IsPlaying { get => m_isPlaying; set => SetProperty(ref m_isPlaying, value); }

        private bool m_isNotPlaying;
        public bool IsNotPlaying { get => m_isNotPlaying; set => SetProperty(ref m_isNotPlaying, value); }

        private bool m_isPlayable;
        public bool IsPlayable { get => m_isPlayable; set => SetProperty(ref m_isPlayable, value); }

        private bool m_isUnsanitised;
        public bool IsUnsanitised { get => m_isUnsanitised; set => SetProperty(ref m_isUnsanitised, value); }

        private bool m_isBroken;
        public bool IsBroken { get => m_isBroken; set => SetProperty(ref m_isBroken, value); }

        private Color m_color;
        public Color Color { get => m_color; set => SetProperty(ref m_color, value); }

        private Color m_progressColor;
        public Color ProgressColor { get => m_progressColor; set => SetProperty(ref m_progressColor, value); }

        internal TimerViewModel(int defaultTime)
        {
            DefaultTime = defaultTime;
            Ready();
        }

        internal void Play()
        {
            State = TimerState.Playing;
        }

        internal void Pause()
        {
            State = TimerState.Paused;
        }

        internal bool Tick()
        {
            Remaining -= 1;
            if (Remaining == 0)
            {
                Stop();
                return false;
            }
            return true;
        }

        internal void Stop()
        {
            Remaining = 0;
            Unsanitised();
        }

        internal void Ready()
        {
            Remaining = DefaultTime;
            Total = DefaultTime;
            State = TimerState.Ready;
        }

        internal void Unsanitised()
        {
            Remaining = 0;
            State = TimerState.Unsanitised;
        }

        internal void Sanitised()
        {
            Ready();
        }

        internal void Broken()
        {
            Remaining = 0;
            State = TimerState.Broken; 
        }

        internal void Fixed()
        {
            Unsanitised();
        }

        internal void AddMinute()
        {
            Remaining += 60;
            if (Remaining > Total)
            {
                Total = Remaining;
            }
        }

        internal void RemoveMinute()
        {
            Remaining -= 60;
        }

        private void UpdateProgress()
        {
            Progress = Total > 0 ? ((double)Remaining / (double)Total * 100) : 0;
        }

        private void UpdateIsPlaying()
        {
            IsPlaying = State == TimerState.Playing;
        }

        private void UpdateIsNotPlaying()
        {
            IsNotPlaying = State != TimerState.Playing;
        }

        private void UpdateIsPlayable()
        {
            IsPlayable = State == TimerState.Ready || State == TimerState.Playing || State == TimerState.Paused;
        }

        private void UpdateIsUnsanitised()
        {
            IsUnsanitised = State == TimerState.Unsanitised;
        }

        private void UpdateIsBroken()
        {
            IsBroken = State == TimerState.Broken;
        }

        private void UpdateColor()
        {
            switch (State)
            {
                case TimerState.Unsanitised:
                    Color = Colors.Red;
                    break;

                case TimerState.Broken:
                    Color = Colors.Purple;
                    break;

                case TimerState.Ready:
                    Color = Colors.Blue;
                    break;

                case TimerState.Playing:
                case TimerState.Paused:
                    Color = Remaining > 30 ? Colors.Green : Colors.Orange;
                    break;
            }
        }

        private void UpdateProgressColor()
        {
            ProgressColor = Remaining > 30 ? Colors.DarkGoldenrod : Colors.Red;
        }
    }
}

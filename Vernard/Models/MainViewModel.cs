using Windows.UI;
using Vernard.Styles;
using Microsoft.UI.Xaml;

namespace Vernard.Models
{
    internal class MainViewModel : BindableBase
    {
        private int DefaultTime { get; set; }

        private int m_remaining;
        internal int Remaining
        {
            get => m_remaining;
            set
            {
                SetProperty(ref m_remaining, value <= 0 ? 0 : value);
                UpdateColor();
                UpdateProgress();
                UpdateProgressForeground();
            }
        }

        private int m_total;
        internal int Total
        {
            get => m_total;
            set
            {
                SetProperty(ref m_total, value <= 0 ? 0 : value);
                UpdateProgress();
            }
        }

        private double m_progress;
        internal double Progress
        {
            get => m_progress;
            set => SetProperty(ref m_progress, value <= 0 ? 0 : (value >= 100 ? 100 : value));
        }

        private TimerState m_state;
        internal TimerState State
        {
            get => m_state;
            set
            {
                SetProperty(ref m_state, value);
                UpdateColor();
                UpdateProgressForeground();
                UpdateProgressBackground();
                UpdateIsReady();
                UpdateIsPlaying();
                UpdateIsNotPlaying();
                UpdateIsPlayable();
                UpdateIsPaused();
                UpdateIsUnsanitised();
                UpdateIsBroken();
            }
        }


        private bool m_isReady;
        internal bool IsReady { get => m_isReady; set => SetProperty(ref m_isReady, value); }

        private bool m_isPlaying;
        internal bool IsPlaying { get => m_isPlaying; set => SetProperty(ref m_isPlaying, value); }

        private bool m_isNotPlaying;
        internal bool IsNotPlaying { get => m_isNotPlaying; set => SetProperty(ref m_isNotPlaying, value); }

        private bool m_isPaused;
        internal bool IsPaused { get => m_isPaused; set => SetProperty(ref m_isPaused, value); }

        private bool m_isPlayable;
        internal bool IsPlayable { get => m_isPlayable; set => SetProperty(ref m_isPlayable, value); }

        private bool m_isUnsanitised;
        internal bool IsUnsanitised { get => m_isUnsanitised; set => SetProperty(ref m_isUnsanitised, value); }

        private bool m_isBroken;
        internal bool IsBroken { get => m_isBroken; set => SetProperty(ref m_isBroken, value); }

        private Color m_color;
        internal Color Color { get => m_color; set => SetProperty(ref m_color, value); }

        private Color m_progressForeground;
        internal Color ProgressForeground { get => m_progressForeground; set => SetProperty(ref m_progressForeground, value); }

        private Color m_progressBackground;
        internal Color ProgressBackground { get => m_progressBackground; set => SetProperty(ref m_progressBackground, value); }

        internal MainViewModel()
        {
            Load();
        }

        internal void Load()
        {
            DefaultTime = App.GetApplicationModel().DefaultTime;
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

        private void UpdateIsReady()
        {
            IsReady = State == TimerState.Ready;
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

        private void UpdateIsPaused()
        {
            IsPaused = State == TimerState.Paused;
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
                    Color = Colors.Amber;
                    break;

                case TimerState.Broken:
                    Color = Colors.Purple;
                    break;

                case TimerState.Ready:
                case TimerState.Paused:
                    Color = Colors.Blue;
                    break;

                case TimerState.Playing:
                    Color = Remaining > 30 ? Colors.Green : Colors.Red;
                    break;
            }
        }

        private void UpdateProgressForeground()
        {
            if (State == TimerState.Playing)
            {
                ProgressForeground = Remaining > 30 ? Colors.Green : Colors.Red;
            }
            else
            {
                ProgressForeground = Colors.Blue;
            }
        }

        private void UpdateProgressBackground()
        {
            switch (State)
            {
                case TimerState.Broken:
                    ProgressBackground = Colors.Purple;
                    break;

                case TimerState.Unsanitised:
                    ProgressBackground = Colors.Amber;
                    break;

                default:
                    ProgressBackground = Colors.Slate;
                    break;
            }
        }
    }
}

using Microsoft.UI.Windowing;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using System;
using Windows.Graphics;
using WinRT.Interop;
using Vernard.Models;

namespace Vernard.Views
{
    internal sealed partial class MainWindow : Window
    {
        private TimerAppModel ApplicationModel { get => (Application.Current as App).ApplicationModel; }
        internal MainWindow()
        {
            this.InitializeComponent();

            var appWindow = GetAppWindow();
            appWindow.SetIcon("Assets/icon.ico");
            appWindow.Resize(new SizeInt32(540, 560));

            var appPresenter = GetAppPresenter(appWindow);
            appPresenter.IsAlwaysOnTop = ApplicationModel.AlwaysOnTop;
            appPresenter.IsMaximizable = false;
            appPresenter.IsResizable = false;
            ApplicationModel.OnLoad += ApplicationModel_OnLoad;

            var displayArea = GetDisplayArea();
            if (displayArea is not null)
            {
                PointInt32 position = appWindow.Position;
                position.X = (displayArea.WorkArea.Width - appWindow.Size.Width);
                position.Y = (displayArea.WorkArea.Height - appWindow.Size.Height);
                appWindow.Move(position);
            }

            ApplicationFrame.Navigate(typeof(MainPage));
        }

        private void ApplicationModel_OnLoad(object sender, EventArgs e)
        {
            var appWindow = GetAppWindow();
            if (appWindow != null)
            {
                GetAppPresenter(appWindow).IsAlwaysOnTop = ApplicationModel.AlwaysOnTop;
            }
        }

        private AppWindow GetAppWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(wndId);
        }

        private OverlappedPresenter GetAppPresenter(AppWindow appWindow)
        {
            return appWindow.Presenter as OverlappedPresenter;
        }

        private DisplayArea GetDisplayArea()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return DisplayArea.GetFromWindowId(wndId, DisplayAreaFallback.Nearest);
        }

    }
}

using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using Windows.Graphics;
using WinRT.Interop;

namespace Vernard.Views.Main
{
    internal sealed partial class MainWindow : Window
    {
        internal MainWindow()
        {
            this.InitializeComponent();

            var appModel = App.GetApplicationModel();

            var appWindow = GetAppWindow();
            appWindow.SetIcon("Assets/icon.ico");
            appWindow.Resize(new SizeInt32(540, 560));

            var appPresenter = GetAppPresenter(appWindow);
            appPresenter.IsAlwaysOnTop = appModel.AlwaysOnTop;
            appPresenter.IsMaximizable = false;
            appPresenter.IsResizable = false;
            appModel.Reload += ApplicationModel_OnReload;

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

        private void ApplicationModel_OnReload(object sender, EventArgs e)
        {
            var appWindow = GetAppWindow();
            if (appWindow != null)
            {
                GetAppPresenter(appWindow).IsAlwaysOnTop = App.GetApplicationModel().AlwaysOnTop;
            }
        }

        private AppWindow GetAppWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(wndId);
        }

        private static OverlappedPresenter GetAppPresenter(AppWindow appWindow)
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

using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using Vernard.Models;
using WinRT.Interop;

namespace Vernard.Views
{
    internal sealed partial class SettingsWindow : Window
    {
        internal SettingsWindow()
        {
            this.InitializeComponent();
            var appWindow = GetAppWindow();
            appWindow.SetIcon("Assets/icon.ico");

            var appModel = App.GetApplicationModel();
            GetAppPresenter(appWindow).IsAlwaysOnTop = appModel.AlwaysOnTop;
            appModel.OnReload += ApplicationModel_OnLoad;
            
            ApplicationFrame.Navigate(typeof(SettingsPage), () =>
            {
                var appModel = App.GetApplicationModel();
                appModel.OnReload -= ApplicationModel_OnLoad;
                Close();
            });
        }

        private void ApplicationModel_OnLoad(object sender, EventArgs e)
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

        private OverlappedPresenter GetAppPresenter(AppWindow appWindow)
        {
            return appWindow.Presenter as OverlappedPresenter;
        }
    }
}

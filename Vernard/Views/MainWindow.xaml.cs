using Microsoft.UI.Windowing;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using System;
using Windows.Graphics;
using WinRT.Interop;

namespace Vernard.Views
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            AppWindow appWindow = AppWindow.GetFromWindowId(wndId);

            OverlappedPresenter appPresenter = appWindow.Presenter as OverlappedPresenter;
            // todo: uncomment
            //appPresenter.IsAlwaysOnTop = true;
            appPresenter.IsMaximizable = false;
            appPresenter.IsResizable = false;

            appWindow.SetIcon("Assets/icon.ico");
            appWindow.Resize(new SizeInt32(540, 560));

            ApplicationFrame.Navigate(typeof(MainPage));
        }
    }
}

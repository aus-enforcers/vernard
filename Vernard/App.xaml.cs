﻿using Microsoft.UI.Xaml;
using Vernard.Views;

namespace Vernard
{
    public partial class App : Application
    {
        private Window ApplicationWindow { get; set; }

        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            ApplicationWindow = new MainWindow();
            ApplicationWindow.Activate();
        }

    }
}

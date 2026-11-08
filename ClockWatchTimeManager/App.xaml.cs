using ClockWatchTimeManager;
using ClockWatchTimeManager.Services;
using BusinessApplicationModels.TimeManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ClockWatchTimeManager
{
    public partial class App : Application
    {
        private const int WINDOW_WIDTH = 960;
        private const int WINDOW_HEIGHT = 540;

        internal static readonly Logger logger = new("log", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PlaypenSoftware", "ClockWatchTimeManager"));
        internal static readonly TimeCardCatalog catalog = new();

        public App()
        {
            InitializeComponent();

            logger.LogEntry
                (
                    "Called App()",
                    catalog.ToString()
                );
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell())
            {
                Width = WINDOW_WIDTH,
                MinimumWidth = WINDOW_WIDTH,
                MaximumWidth = WINDOW_WIDTH,
                Height = WINDOW_HEIGHT,
                MinimumHeight = WINDOW_HEIGHT,
                MaximumHeight = WINDOW_HEIGHT,
                IsMaximizable = false
            };
        }
    }
}
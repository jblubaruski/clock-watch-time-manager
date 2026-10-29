using Clock_Watch_Time_Manager.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Clock_Watch_Time_Manager
{
    public partial class App : Application
    {
        private const int WINDOW_WIDTH = 960;
        private const int WINDOW_HEIGHT = 540;

        internal static readonly TimeCardCatalog catalog = new();

        public App()
        {
            InitializeComponent();
#if DEBUG
            Debug.WriteLine("Called App()");
            Debug.WriteLine(catalog);
#endif
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
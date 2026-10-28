using Clock_Watch_Time_Manager.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Clock_Watch_Time_Manager
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
using ClockWatchTimeManager;
using FileUtility.Text;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ClockWatchTimeManager.Tests")]

namespace ClockWatchTimeManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("TIMES.ttf", "TimesNewRoman");
                    fonts.AddFont("TIMESBD.ttf", "TimesNewRoman-Bold");
                    fonts.AddFont("BAHNSCHRIFT-Bold.ttf", "Bahnshcrift-Bold");
                    fonts.AddFont("BAHNSCHRIFT-Light.ttf", "Bahnshcrift-Light");
                    fonts.AddFont("BAHNSCHRIFT-Regular.ttf", "Bahnshcrift");
                });

            return builder.Build();
        }
    }
}

using ClockWatchTimeManager;
using ClockWatchTimeManager.Models.Logging;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ClockWatchTimeManager.Tests")]

namespace ClockWatchTimeManager
{
    public static class MauiProgram
    {
        internal static readonly LogWriter LogWriter = new("Clock Watch Time Manager");

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

#if DEBUG
    		builder.Logging.AddDebug();
            LogWriter.WriteLogFileNameToDebugConsole();
            Debug.WriteLine("Ran CreateMauiApp()");
#endif
            return builder.Build();
        }
    }
}

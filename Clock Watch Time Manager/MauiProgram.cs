using Clock_Watch_Time_Manager.Models;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Clock_Watch_Time_Manager
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

#if DEBUG
    		builder.Logging.AddDebug();
            Debug.WriteLine("Ran CreateMauiApp()");
#endif
            return builder.Build();
        }
    }
}

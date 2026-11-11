using System.Diagnostics;

namespace ClockWatchTimeManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        /*
         * Called when the app is about to navigate to another page
         */
        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);

            if (args.Current == null)
                App.logger.LogEntry($"Shell is navigating to {args.Target.Location} by {args.Source}");
            else if (args.Target == null)
                App.logger.LogEntry($"Shell is navigating from {args.Current.Location} by {args.Source}");
            else
                App.logger.LogEntry($"Shell is navigating from {args.Current.Location} to {args.Target.Location} by {args.Source}");
        }

        /*
         * Called when the app has finished navigating from one page to another
         */
        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);

            if (args.Current == null)
                App.logger.LogEntry($"Shell has navigated from {args.Previous.Location} by {args.Source}");
            else if (args.Previous == null)
                App.logger.LogEntry($"Shell has navigated to {args.Current.Location} by {args.Source}");
            else
                App.logger.LogEntry($"Shell has navigated to {args.Current.Location} from {args.Previous.Location} by {args.Source}");
        }
    }
}

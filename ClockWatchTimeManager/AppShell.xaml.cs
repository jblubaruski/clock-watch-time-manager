using System.Diagnostics;

namespace ClockWatchTimeManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

#if DEBUG
            Debug.WriteLine("Called AppShell()");
#endif
        }

        /*
         * Called when the app is about to navigate to another page
         */
        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);

#if DEBUG
            if (args.Current == null)
                Debug.WriteLine($"Shell is navigating to {args.Target.Location} by {args.Source}");
            else if (args.Target == null)
                Debug.WriteLine($"Shell is navigating from {args.Current.Location} by {args.Source}");
            else
                Debug.WriteLine($"Shell is navigating from {args.Current.Location} to {args.Target.Location} by {args.Source}");
#endif
        }

        /*
         * Called when the app has finished navigating from one page to another
         */
        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);

#if DEBUG
            if (args.Current == null)
                Debug.WriteLine($"Shell has navigated from {args.Previous.Location} by {args.Source}");
            else if (args.Previous == null)
                Debug.WriteLine($"Shell has navigated to {args.Current.Location} by {args.Source}");
            else
                Debug.WriteLine($"Shell has navigated to {args.Current.Location} from {args.Previous.Location} by {args.Source}");
#endif
        }
    }
}

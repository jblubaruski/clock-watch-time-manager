using System.Diagnostics;

namespace ClockWatchTimeManager.Views;

public partial class ShowTimeCardsPage : ContentPage
{
	public ShowTimeCardsPage()
	{
		InitializeComponent();

#if DEBUG
        Debug.WriteLine("Called ShowTimeCardsPage()");
#endif
    }
}
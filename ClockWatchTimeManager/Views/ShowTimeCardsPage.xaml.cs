using System.Diagnostics;

namespace ClockWatchTimeManager.Views;

public partial class ShowTimeCardsPage : ContentPage
{
	public ShowTimeCardsPage()
	{
		InitializeComponent();

        App.logger.LogEntry("Called ShowTimeCardsPage()");
    }
}
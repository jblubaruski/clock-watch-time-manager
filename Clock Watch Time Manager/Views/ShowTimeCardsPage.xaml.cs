using System.Diagnostics;

namespace Clock_Watch_Time_Manager.Views;

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
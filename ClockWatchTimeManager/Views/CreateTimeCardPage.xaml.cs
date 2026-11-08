using ClockWatchTimeManager.ViewModels;
using System.Diagnostics;
namespace ClockWatchTimeManager.Views;

public partial class CreateTimeCardPage : ContentPage
{
    public CreateTimeCardPage()
    {
        InitializeComponent();

#if DEBUG
        Debug.WriteLine("Called CreateTimeCardPage()");
#endif
    }
}

